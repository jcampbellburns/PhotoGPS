Partial Public Class FMain

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly

        With Me.MAP
            'config map
            .DisableFocusOnMouseEnter = True
            .CanDragMap = True
            .DragButton = MouseButtons.Left
            .MapProvider = MapProviders.GMapProviders.GoogleMap
            '.Position = MapProviders.GMapProviders.GoogleMap.GetPoint("Los Angeles, CA", Nothing) 'New PointLatLng(54.6961334816182, 25.2985095977783)
            .MinZoom = 0
            .MaxZoom = 24
            .Zoom = 9
            '.ShowCenter = False
        End With

        MAP.Overlays.Add(_LocationsOverlay)

        Me.RefreshLocationsLV(False)


        PhotoControls = {LVPhotos, TSPhotos}
        LocationControls = {LVLocations, TSLocations}
        MapControls = {MAP, TSMap, TSAddress, TSCoords}
        ProjectControls = {TSProject}
        AppControls = (DirectCast({TSMain}, Control()).Union(PhotoControls).Union(LocationControls).Union(MapControls).Union(ProjectControls)).ToArray
        ShowProgressControls = {SSPTaskProgress, SSBStop}

    End Sub

    Public Project As New Project

#Region "UI Prompts"
    Private Function AskRemovePhotoCorrelation() As Boolean
        Return MsgBox("Are you sure you want to remove all photos? You will lose any manual location correlations.", MsgBoxStyle.YesNo Or MsgBoxStyle.Critical Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes
    End Function
#End Region

#Region "Location Toolstrip"
    Private Sub TSBImportLocations_Click(sender As Object, e As EventArgs) Handles TSBImportLocations.Click
        ImportLocations()
    End Sub

    Private Sub TSBExportLocations_Click(sender As Object, e As EventArgs) Handles TSBExportLocations.Click
        ExportLocations()
    End Sub

    Private Sub TSBLocationsVisible_CheckedChanged(sender As Object, e As EventArgs) Handles TSBLocationsVisible.CheckedChanged
        _LocationsOverlay.IsVisibile = TSBLocationsVisible.Checked
    End Sub

    Private Sub TSBGetLocationCoords_Click(sender As Object, e As EventArgs) Handles TSBGetLocationCoords.Click
        Dim SelectedLocationsWithoutGPS = From i In Project.Locations Where Me._LocationLVItems.Contains(i.ListViewItem) And i.ListViewItem.Selected And i.GPS.HasValue = False

        Dim a = Threading.Tasks.Parallel.ForEach(Of Location)(SelectedLocationsWithoutGPS,
            Sub(i)
                Dim status As GeoCoderStatusCode

                Dim gps = MapProviders.GMapProviders.GoogleMap.GetPoint(i.Address, status)

                Select Case status
                    Case GeoCoderStatusCode.G_GEO_SUCCESS
                        i.GPS = gps
                        LVLocations.Invoke(Sub() i.UpdateListViewItem())
                    Case Else
                        Stop
                End Select
            End Sub)

        Me.LVLocations.Refresh()
    End Sub
#End Region

#Region "Locations Listview"
    Private Sub LVLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVLocations.SelectedIndexChanged
        Me.RefreshPhotosLV(False)
    End Sub

    Private Sub LVLocations_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles LVLocations.RetrieveVirtualItem

        e.Item = Me._LocationLVItems(e.ItemIndex)
    End Sub

#End Region

#Region "Photos Toolstrip"
    Private Sub TBSAddPhotos_ButtonClick(sender As Object, e As EventArgs) Handles TBSAddPhotos.ButtonClick
        AddPhotosFolder()
    End Sub

    Private Sub TSBAddPhotosFolder_Click(sender As Object, e As EventArgs) Handles TSBAddPhotosFolder.Click
        AddPhotosFolder()
    End Sub

    Private Sub TSBAddPhotosFile_Click(sender As Object, e As EventArgs) Handles TSBAddPhotosFile.Click
        AddPhotosFile()
    End Sub

    Private Sub TSBRemoveAllPhotos_Click(sender As Object, e As EventArgs)
        RemoveAllPhotos()
    End Sub

    Private Sub TSBRenamePhotoFiles_Click(sender As Object, e As EventArgs) Handles TSBRenamePhotoFiles.Click
        RenameAllPhotos()
    End Sub

    Private Sub TSBSortPhotos_ButtonClick(sender As Object, e As EventArgs) Handles TSBSortPhotos.ButtonClick
        SortPhotosToFolderByLocationName()
    End Sub

    Private Sub TSBSortPhotosByLocationName_Click(sender As Object, e As EventArgs) Handles TSBSortPhotosByLocationName.Click
        SortPhotosToFolderByLocationName()
    End Sub

    Private Sub TSBSortPhotosByTakenDate_Click(sender As Object, e As EventArgs) Handles TSBSortPhotosByTakenDate.Click
        SortPhotosToFolderByTakenDate()
    End Sub

    Private Sub TSBSortPhotosSameFolder_Click(sender As Object, e As EventArgs) Handles TSBSortPhotosSameFolder.Click
        SortPhotosToFolderSameFolder()
    End Sub


#End Region

#Region "Photos Listview"
    Private Sub LVPhotos_RetrieveVirtualItem(sender As Object, e As RetrieveVirtualItemEventArgs) Handles LVPhotos.RetrieveVirtualItem
        e.Item = Me._PhotoLVItems(e.ItemIndex)
    End Sub
#End Region

#Region "Progress bar system"
    Private PhotoControls() As Control
    Private LocationControls() As Control
    Private MapControls() As Control
    Private ProjectControls() As Control
    Private AppControls() As Control
    Private ShowProgressControls() As ToolStripItem

    Private CancelTask As Boolean

    ''' <summary>
    ''' Shows/Hides progress bar and cancel button. Disables/Enables controls in an array.
    ''' </summary>
    ''' <param name="ProgressControlState"><c>True</c>: Show the progress bar and cancel button. Disable all of the controls in <paramref name="ControlSet"/>. <c>False</c>: Hide the progress bar and cancel button. Enable all of the controls in <paramref name="ControlSet"/></param>
    ''' <param name="ControlSet">An array of <see cref="Windows.Forms.Control"/> to enable or disable.</param>
    Private Sub InitShowProgress(ProgressControlState As Boolean, ControlSet As Control())
        CancelTask = False

        Dim a = Sub()
                    For Each i In ControlSet
                        i.Enabled = Not ProgressControlState
                    Next

                    For Each i In ShowProgressControls
                        i.Visible = ProgressControlState
                    Next

                    If Not ProgressControlState Then
                        SSLStatus.Text = "Ready"
                        Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress)
                        Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(0, 100)
                    Else
                        Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal)
                    End If
                End Sub

        If Me.InvokeRequired Then
            Me.Invoke(a)
        Else
            a()
        End If
    End Sub

    Private Sub SetProgressStatus(Message As String, Percentage As Double, Optional Force As Boolean = False)
        Static n As Date

        Dim a = Sub()

                    If (Date.Now > n) Or Force Then
                        n = Date.Now.AddSeconds(0.1)
                        SSLStatus.Text = Message
                        SSPTaskProgress.Value = Percentage * 100

                        Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(Percentage * 100, 100)

                    End If

                End Sub

        If Me.InvokeRequired Then
            Me.Invoke(a)
        Else
            a()
        End If
    End Sub

    Private Sub SSBStop_Click(sender As Object, e As EventArgs) Handles SSBStop.Click
        CancelTask = True
    End Sub
#End Region

    Private Sub RunCorrelation()
        Dim a =
        Sub()
            InitShowProgress(True, LocationControls)

            InitShowProgress(True, PhotoControls)

            Dim locations = (From i In Project.Locations Where _LocationLVItems.Contains(i.ListViewItem)).ToList
            Dim current = 0
            Dim count = locations.Count

            Dim b = Threading.Tasks.Parallel.ForEach(Of Location)(locations,
                Sub(loc, LoopState)

                    If CancelTask Then LoopState.Break()
                    If Not LoopState.ShouldExitCurrentIteration Then
                        current += 1

                        SetProgressStatus(String.Format("Correlating photos to locations. {0} locations remaining.", count - current), current / count)

                        loc.Photos = loc.PhotosFromLocation.ToList

                        LVLocations.Invoke(Sub() loc.UpdateListViewItem())
                    End If
                End Sub)

            LVLocations.Invoke(
                Sub()
                    Me._AllFilesItem.SubItems(7).Text = Project.Photos.Count
                    Me._PhotosWithMultipleLocations.SubItems(7).Text = (From p In Project.Photos Where p.Locations.Count > 1).Count
                    Me._UnassociatedFilesItem.SubItems(7).Text = (From p In Project.Photos Where p.Locations.Count = 0).Count
                End Sub)
            InitShowProgress(False, LocationControls)
            InitShowProgress(False, PhotoControls)
        End Sub

        a.BeginInvoke(Nothing, Nothing)


    End Sub

    Private Sub TSBRemovePhotos_Click(sender As Object, e As EventArgs) Handles TSBRemovePhotos.Click
        RemoveAllPhotos()
    End Sub
End Class
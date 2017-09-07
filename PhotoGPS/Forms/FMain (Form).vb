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

    '''' <summary>
    '''' Updates a <see cref="ListView"/> control to contain items from a <see cref="List(Of LVItem)"/>, optionally allowing filtering.
    '''' </summary>
    '''' <typeparam name="T">The type of the base object being displayed.</typeparam>
    '''' <param name="ItemList">The list of the <see cref="LVItem(Of t)"/> being displayed.</param>
    '''' <param name="ListView">The <see cref="ListView"/> control in which to display the list.</param>
    '''' <param name="ClearListView">If <c>True</c>, the <see cref="ListView"/> control will be cleared before populating the items and if the user cancels the action. If <c>False</c>, the <see cref="ListView"/> control will notbe cleared before populating the items and if the user cancels the action.</param>
    '''' <param name="FilterFunction">A <see cref="Predicate(Of T)"/> which returns <c>True</c> if the item is to be displayed in the list or <c>False</c> if not. If this parameter is <c>null</c>, all items are displayed.</param>
    '''' <param name="PostbackFunction">A <see cref="WaitWindow.PostBack"/> to be called to indicate progress to the user and to allow cancellation.</param>

    '''' <summary>
    '''' Resize all of the columns in a <see cref="ListView"/> to fit the current content.
    '''' </summary>
    '''' <param name="lv">The <see cref="ListView"/> to resize the columns of.</param>
    'Private Sub AutosizeColumns(lv As ListView)
    '    'resize columns to fit
    '    For Each i As ColumnHeader In lv.Columns
    '        i.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
    '    Next
    'End Sub

    '''' <summary>
    '''' Updates the <see cref="Location.Photos"/> property of all elements in <see cref="_LocationLVItems"/> which are also visible in <see cref="LVLocations"/>.
    '''' </summary>
    '''' <param name="pb">Optional. If provided, this method will call it to update the user. If not specified, this method will establish a <see cref="WaitWindow.WaitForIt"/> on it's own.</param>
    'Private Sub UpdateLocationPhotosLists(Optional pb As WaitWindow.PostBack = Nothing)
    '    'initialize lists sub
    '    Dim InitializeLists =
    '        Sub()
    '            If (_LocationLVItems.Count > 0) Then
    '                _LocationLVItems.ForEach(Sub(i)
    '                                             If i.Item.Photos IsNot Nothing Then
    '                                                 i.Item.Photos.Clear()
    '                                             End If
    '                                         End Sub)

    '            End If

    '            If (_PhotoLVItems.Count > 0) Then
    '                _PhotoLVItems.ForEach(Sub(i)
    '                                          If i.Item.Locations IsNot Nothing Then
    '                                              i.Item.Locations.Clear()
    '                                          End If
    '                                      End Sub)
    '            End If
    '        End Sub

    '    'update special items sub
    '    Dim UpdateSpecialItems =
    '        Sub()
    '            Me._AllFilesItem.SubItems(7).Text = _PhotoLVItems.Count
    '            Me._UnassociatedFilesItem.SubItems(7).Text = (From i In _PhotoLVItems Where i.Item.LocationCount = 0).Count
    '            Me._PhotosWithMultipleLocations.SubItems(7).Text = (From i In _PhotoLVItems Where i.Item.LocationCount > 1).Count
    '        End Sub

    '    'correlation function sub
    '    Dim RunCorrelation =
    '        Sub(postback As WaitWindow.PostBack, ThrowException As Boolean)
    '            InitializeLists()

    '            Try
    '                If (_LocationLVItems.Count > 0) And (_PhotoLVItems.Count > 0) Then
    '                    Dim locations = (From i In _LocationLVItems Where (i.Item.GPS.HasValue) And (Me.LVLocations.Items.Contains(i.LVItem))).ToList

    '                    For Each currentLoc In locations
    '                        postback.Invoke("Finding photos which match location " & currentLoc.Item.LocationName, locations.IndexOf(currentLoc) / locations.Count)
    '                        Dim tmp = (From i In _PhotoLVItems Where currentLoc.Item.ComparePhoto(i.Item) = True).ToList

    '                        If tmp.Count > 0 Then
    '                            currentLoc.Item.Photos = (From i In tmp Select i.Item).ToList

    '                            tmp.ForEach(Sub(i)
    '                                            'add the current Location to each of this Location's photos' Locations list
    '                                            i.Item.Locations.Add(currentLoc.Item)


    '                                            'If a photo has 2 or more Locations, color it red in the listview
    '                                            If i.Item.LocationCount > 1 Then
    '                                                i.LVItem.ForeColor = Color.Red
    '                                            Else
    '                                                i.LVItem.ForeColor = i.LVItem.ListView.ForeColor
    '                                            End If
    '                                        End Sub)
    '                        End If

    '                        If currentLoc.LVItem.ListView.InvokeRequired Then
    '                            currentLoc.LVItem.ListView.Invoke(Sub() UpdateLocationLVItem(currentLoc.Item, currentLoc.LVItem))
    '                        Else
    '                            UpdateLocationLVItem(currentLoc.Item, currentLoc.LVItem)
    '                        End If

    '                    Next

    '                End If
    '            Catch ex As WaitWindow.DoItCanceledException
    '                'if the user cancels, we do not want a partial correlation so clear it
    '                InitializeLists()
    '                If ThrowException Then Throw 'we put this in since if pb is passed from an existing WaitWindow, we want to propigate the cancelled exception. If pb was created for this call, we don't want to propigate the exception any further than this sub (UpdateLocationPhotosLists) as the code would not be expecting it.
    '            Finally
    '                If LVLocations.InvokeRequired Then
    '                    LVLocations.Invoke(UpdateSpecialItems)
    '                Else
    '                    UpdateSpecialItems()
    '                End If
    '            End Try
    '        End Sub

    '    'initialize waitwindow if needed
    '    If pb Is Nothing Then
    '        WaitWindow.WaitForIt.DoIt("Correlating locations and photos", Sub(p) RunCorrelation(p, False), Me)
    '    Else
    '        RunCorrelation(pb, True)
    '    End If
    'End Sub

#Region "UI Prompts"
    Private Function AskRemovePhotoCorrelation(Optional PhotosToDelete As IEnumerable(Of Photo) = Nothing) As Boolean
        Dim a = (From l In Project.Locations From p In PhotosToDelete Where l.Photos.Contains(p) Select l)

        If a.Count > 0 Then
            If MsgBox("This will remove photos which have been manually associated with locations. Are you sure you want to do this?", MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then

                Threading.Tasks.Parallel.ForEach(Of Location)(a,
                    Sub(i)
                        i.Photos = i.Photos.Except(PhotosToDelete).ToList
                    End Sub)
                Return True
            Else
                Return False
            End If
        Else
            Return True
        End If
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
                        i.UpdateListViewItem()
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

    Private Sub TSBRemovePhotos_ButtonClick(sender As Object, e As EventArgs) Handles TSBRemovePhotos.ButtonClick
        RemoveSelectedPhotos()
    End Sub

    Private Sub TSBRemoveSelectedPhotos_Click(sender As Object, e As EventArgs) Handles TSBRemoveSelectedPhotos.Click
        RemoveSelectedPhotos()
    End Sub

    Private Sub TSBRemovePhotosNoLongerAvailable_Click(sender As Object, e As EventArgs) Handles TSBRemovePhotosNoLongerAvailable.Click
        RemoveDeadPhotos()
    End Sub

    Private Sub TSBRemoveAllPhotos_Click(sender As Object, e As EventArgs) Handles TSBRemoveAllPhotos.Click
        RemoveAllPhotos()
    End Sub

    Private Sub TSBRenamePhotoFiles_Click(sender As Object, e As EventArgs) Handles TSBRenamePhotoFiles.Click
        RenameAllPhotos
    End Sub

    Private Sub TSBSortPhotos_ButtonClick(sender As Object, e As EventArgs) Handles TSBSortPhotos.ButtonClick
        SortPhotosByLocationName()
    End Sub

    Private Sub TSBSortPhotosByLocationName_Click(sender As Object, e As EventArgs) Handles TSBSortPhotosByLocationName.Click
        SortPhotosByLocationName()
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

                    If ProgressControlState = False Then
                        SSLStatus.Text = "Ready"
                    End If
                End Sub

        If Me.InvokeRequired Then
            Me.Invoke(a)
        Else
            a()
        End If
    End Sub

    Private Sub SetProgressStatus(Message As String, Percentage As Double)
        Static n As Date

        Dim a = Sub()

                    If Date.Now > n Then
                        n = Date.Now.AddSeconds(0.1)
                        SSLStatus.Text = Message
                        SSPTaskProgress.Value = Percentage * 100
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



End Class
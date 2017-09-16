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

        'Add Special location items (All photos, photos with no locations, photos with multiple locations)
        Me.RefreshLocationsLV(False)

        'Used by: Progress Bar/Cancel system
        AppControls = {TSMain, LVPhotos, TSPhotos, LVLocations, TSLocations, MAP, TSMap, TSAddress, TSCoords, TSProject}
        ShowProgressControls = {SSPTaskProgress, SSBStop}

    End Sub

    ''' <summary>Object to store all instances of <see cref="Photo"/> and <see cref="Location"/>.</summary>
    Public Project As New Project

#Region "Control Eventhandlers"
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
        Dim SelectedLocationsWithoutGPS = From i In Project.Locations Where Me._LocationLVItems.Contains(i.ListViewItem) And i.Address <> String.Empty And i.GPS.HasValue = False

        For Each i In SelectedLocationsWithoutGPS
            Dim status As GeoCoderStatusCode

            'MapProviders.GMapProviders.BingMap.GetPoint()

            Dim gps = MapProviders.GMapProviders.GoogleMap.GetPoint(i.Address, status)

            Select Case status
                Case GeoCoderStatusCode.G_GEO_SUCCESS
                    i.GPS = gps
                    LVLocations.Invoke(Sub() i.UpdateListViewItem())
                Case GeoCoderStatusCode.ExceptionInCode
                Case Else


                    Diagnostics.Debug.Print("Address: {0}, StatusCode: {1}", i.Address, status.ToString)
                    LVLocations.Invoke(Sub() i.UpdateListViewItem())

            End Select
        Next

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

    Private Sub TSBRemovePhotos_Click(sender As Object, e As EventArgs) Handles TSBRemovePhotos.Click
        RemoveAllPhotos()
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
#End Region

#Region "Progress bar system"
    '''<summary>Array of all controls to be enabled/disabled when app is busy and displaying progress bar.</summary>
    Private AppControls() As Control 'Value is set in FMain.ctor()

    '''<summary>Array of <see cref="ToolStripItem"/> to be shown/hidden when app is busy and displaying progress bar.</summary>
    Private ShowProgressControls() As ToolStripItem 'Value is set in FMain.ctor()

    ''' <summary>Indicates the user has clicked the Stop button. this value is reset from within <see cref="InitShowProgress(Boolean, Control())"/></summary>
    Private CancelTask As Boolean 'Value is set in SSBStop_Click() and InitShowProgress()

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

    ''' <summary>
    ''' Update the current progress message and completion percentage.
    ''' </summary>
    ''' <param name="Message">Message to be displayed in the status bar.</param>
    ''' <param name="Percentage">The current percentage as a floating-point value between 0 and 1. A value of 0.75 indicates 75%.</param>
    ''' <param name="Force">The update should override the built-in time-wait. See remarks below.</param>
    ''' <remarks>This method is designed to be called from within an iteration loop for each iteration. The updates will occur no more frequently than 10 times in any second unless the <paramref name="Force"/> parameter is <c>True</c>. This method also updates the taskbar button with the specified progress percentage for the <see cref="FMain"/> instance from which it was called.</remarks>
    Private Sub SetProgressStatus(Message As String, Percentage As Double, Optional Force As Boolean = False)
        Static n As Date

        Dim a = Sub()

                    If (Date.Now > n) Or Force Then
                        n = Date.Now.AddSeconds(0.1)
                        SSLStatus.Text = Message
                        SSPTaskProgress.Value = Percentage * 100

                        Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(Percentage * 100, 100)

                        Application.DoEvents()
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

    ''' <summary>
    ''' Sets the <see cref="Photo.Locations"/> property based on all <see cref="Location"/> instances in <see cref="Project.Locations"/> for all <see cref="Photo"/> instances in <see cref="Project.Locations"/>. Also calls <see cref="Location.UpdateListviewItem()"/> for each <see cref="Location"/>.
    ''' </summary>
    Private Sub RunCorrelation()
        Dim a =
        Sub()
            InitShowProgress(True, AppControls)

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
                    Me._SpecialLocationItemAllFilesItem.SubItems(7).Text = Project.Photos.Count
                    Me._SpecialLocationItemPhotosWithMultipleLocations.SubItems(7).Text = (From p In Project.Photos Where p.Locations.Count > 1).Count
                    Me._SpecialLocationItemUnassociatedFilesItem.SubItems(7).Text = (From p In Project.Photos Where p.Locations.Count = 0).Count
                End Sub)
            InitShowProgress(False, AppControls)

        End Sub

        a.BeginInvoke(Nothing, Nothing)

    End Sub

#Region "UI Prompts"
    Private Function AskRemovePhotoCorrelation() As Boolean
        Return MsgBox("Are you sure you want to remove all photos? You will lose any manual location correlations.", MsgBoxStyle.YesNo Or MsgBoxStyle.Critical Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes
    End Function
#End Region

#Region "Locations"
    ''' <summary>Overlay to contain map markers for Locations</summary>
    Private _LocationsOverlay As New WindowsForms.GMapOverlay("Locations")

    ''' <summary>"All files" locations item</summary>
    Private _SpecialLocationItemAllFilesItem As New ListViewItem({"(All Files)", "", "", "", "", "", "", "Undefined"})

    ''' <summary>"Multiple Matches" locations item</summary>
    Private _SpecialLocationItemPhotosWithMultipleLocations As New ListViewItem({"(Multiple matches)", "", "", "", "", "", "", "Undefined"})

    ''' <summary>"No location match" locations item</summary>
    Private _SpecialLocationItemUnassociatedFilesItem As New ListViewItem({"(No location match)", "", "", "", "", "", "", "Undefined"})

    ''' <summary>Array containing all special location items</summary>
    Private _SpecialLocationItems() As ListViewItem = {_SpecialLocationItemAllFilesItem, _SpecialLocationItemPhotosWithMultipleLocations, _SpecialLocationItemUnassociatedFilesItem}

    ''' <summary>Array containing all <see cref="ListViewItem"/> instances to be displayed in <see cref="LVLocations"/></summary>
    Private _LocationLVItems() As ListViewItem = {}

    ''' <summary>
    ''' Prompt the user for a file. Use <see cref="CSVDeserializer(Of Location)"/> to deserialize a list of <see cref="Location"/> instances from the file. the use will be prompted to assign field mapping.
    ''' </summary>
    Private Sub ImportLocations()
        'load locations from CSV

        Dim pb As CSVDeserializer(Of Location).PostBack =
            Function(Message As String, Progress As Double, Force As Boolean) As Boolean
                SetProgressStatus(Message, Progress, Force)
                Return CancelTask
            End Function

        Me.InitShowProgress(True, Me.AppControls)

        Dim a = Sub()
                    Project.Locations = CSVSerializer.CSVDeserializer(Of Location).Deserialize(pb, Me)

                    Me.InitShowProgress(False, Me.AppControls)

                    If Project.Locations IsNot Nothing Then
                        For Each l In Project.Locations
                            l.Project = Project
                        Next

                        _LocationLVItems = (From i In Project.Locations Select i.ListViewItem).ToArray

                        LVLocations.Invoke(Sub() RefreshLocationsLV(True))
                    End If

                End Sub

        a.BeginInvoke(Nothing, Nothing)

    End Sub

    ''' <summary>
    ''' Prompt the user for a file. Use <see cref="CSVSerializer(Of Location)"/> to serialize a list of <see cref="Location"/> and saves the result to the file selected by the user.
    ''' </summary>
    Private Sub ExportLocations()
        'save locations to CSV



        Dim OpenWindow As New System.Windows.Forms.SaveFileDialog With {
                    .Filter = "Comma Separated Values|*.csv|All Files|*.*",
                    .DefaultExt = "CSV"}

        Dim owRes As System.Windows.Forms.DialogResult
        Me.Invoke(Sub() owRes = OpenWindow.ShowDialog(Me))

        If owRes = System.Windows.Forms.DialogResult.OK Then
            Dim a As New IO.FileInfo(OpenWindow.FileName)

            Dim csvData = CSVSerializer.CSVSerializer(Of Location).Serialize(Project.Locations)

            If csvData <> String.Empty Then
                Using s = a.CreateText()
                    s.Write(csvData)
                    s.Flush()
                    s.Close()
                End Using
            End If
        End If

    End Sub

    ''' <summary>
    ''' Updates the array from which <see cref="LVLocations"/> retrieves its items. This is to be called after any changes (single or multiple) are made to the contents of <see cref="Project.locations"/>.
    ''' </summary>
    ''' <param name="AlsoRunCorrelation">If <c>true</c>, this method will call <see cref="RunCorrelation()"/> after updating <see cref="LVLocations"/>.</param>
    Private Sub RefreshLocationsLV(AlsoRunCorrelation As Boolean)
        _LocationLVItems = _SpecialLocationItems.Union(From i In Project.Locations Select i.ListViewItem).ToArray

        Me.LVLocations.VirtualListSize = _LocationLVItems.Count

        If AlsoRunCorrelation Then RunCorrelation()

    End Sub
#End Region

#Region "Photos"
    ''' <summary>Overlay to contain map markers for Photos</summary>
    Private _PhotosOverlay As New WindowsForms.GMapOverlay("Photos")

    ''' <summary>Array containing all <see cref="ListViewItem"/> instances to be displayed in <see cref="LVPhotos"/></summary>
    Private _PhotoLVItems() As ListViewItem = {}

    ''' <summary>
    ''' Prompt user for folder. Create and add instances of <see cref="Photo"/> to <see cref="Project.Photos"/> for each file found which matches any of the extensions given in <see cref="Photo.SupportedExtensions"/> and which can be read by <see cref="ExifReader"/> and which have non-null value for <see cref="Photo.GPS"/> and <see cref="Photo.TakenDate"/>. The user is also prompted if files should be searched recursively.
    ''' </summary>
    Private Sub AddPhotosFolder()
        Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

        fb.IsFolderPicker = True

        If fb.ShowDialog() = DialogResult.OK Then
            Dim MsgBoxRecursiveResponse = MsgBox("Load all photos from sub-folders recursively as well?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question Or MsgBoxStyle.DefaultButton2)

            If MsgBoxRecursiveResponse <> MsgBoxResult.Cancel Then
                Dim folder = New IO.DirectoryInfo(fb.FileName)
                Dim filecount = 0
                Dim foldercount = 0

                Dim recursefiles As Func(Of IO.DirectoryInfo, IEnumerable(Of IO.FileInfo)) =
                    Function(f As IO.DirectoryInfo) As IEnumerable(Of IO.FileInfo)
                        If Not CancelTask Then

                            'TODO: implement the next line with a For...Each so the user can cancel it if needed
                            Dim res As IEnumerable(Of IO.FileInfo) = f.GetFiles()

                            filecount += res.Count

                            If MsgBoxRecursiveResponse = MsgBoxResult.Yes And Not CancelTask Then
                                For Each subfolder In f.GetDirectories()
                                    foldercount += 1
                                    res = res.Union(recursefiles.Invoke(subfolder))
                                Next
                            End If

                            SetProgressStatus(String.Format("Enumerating files. Found {0} files in {1} folders.", filecount, foldercount), 0)

                            Return res
                        Else
                            Return Enumerable.Empty(Of IO.FileInfo)
                        End If
                    End Function

                Dim a =
                    Sub()
                        InitShowProgress(True, AppControls)

                        Dim allfiles = recursefiles(folder)
                        AddPhotoFiles(allfiles)

                        InitShowProgress(False, AppControls)
                    End Sub

                a.BeginInvoke(Nothing, Nothing)

            End If
        End If
    End Sub

    ''' <summary>
    ''' Prompt user for file with file open window supporting multiple selections. Create and add instances of <see cref="Photo"/> to <see cref="Project.Photos"/> for each file found which matches any of the extensions given in <see cref="Photo.SupportedExtensions"/> and which can be read by <see cref="ExifReader"/> and which have non-null value for <see cref="Photo.GPS"/> and <see cref="Photo.TakenDate"/>.
    ''' </summary>
    Private Sub AddPhotosFile()
        Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

        fb.AllowNonFileSystemItems = False
        fb.Filters.Add(New Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter("Supported Photo Types", String.Join(";", Photo.SupportedExtensions)))
        fb.Filters.Add(New Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter("All Files", "*"))

        fb.Multiselect = True
        fb.EnsureFileExists = True
        fb.EnsurePathExists = True

        If fb.ShowDialog() = DialogResult.OK Then
            Dim a =
                Sub()
                    InitShowProgress(True, AppControls)
                    AddPhotoFiles(From i In fb.FileNames Select New IO.FileInfo(i))
                    InitShowProgress(False, AppControls)
                End Sub

            a.BeginInvoke(Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Create and add instances of <see cref="Photo"/> to <see cref="Project.Photos"/> for each file specified by <paramref name="AllFiles"/> which can be read by <see cref="ExifReader"/> and which have non-null value for <see cref="Photo.GPS"/> and <see cref="Photo.TakenDate"/>.
    ''' </summary>
    ''' <param name="AllFiles">An instance implementing the interface <see cref="IEnumerable(Of IO.IOFileinfo)"/> which contains files from which to create instances of <see cref="Photo"/>.</param>
    Private Sub AddPhotoFiles(AllFiles As IEnumerable(Of IO.FileInfo))
        Dim FilteredFiles = (From f In AllFiles From x In Photo.SupportedExtensions Where f.Extension.ToLower = x.ToLower Select f).ToList
        Dim AddedPhotos = New List(Of Photo)
        Dim count = FilteredFiles.Count
        Dim current = 0

        For Each f In FilteredFiles
            If CancelTask Then Exit For
            current += 1

            If Not CancelTask Then
                SetProgressStatus(String.Format("Adding photos. {0} remaining.", count - current), current / count)
                Dim NewPhoto = Photo.FromFile(f, Project)

                If NewPhoto IsNot Nothing Then
                    AddedPhotos.Add(NewPhoto)
                End If
            End If
        Next

        SetProgressStatus(String.Format("Adding photos. 0 remaining."), 1, True)

        Project.Photos = (From i In Project.Photos.Concat(AddedPhotos) Where i IsNot Nothing Order By i.TakenDate).Distinct(New PhotoComparer)
        Me.LVPhotos.Invoke(Sub() RefreshPhotosLV(True))

    End Sub

    ''' <summary>
    ''' Remove all photos from <see cref="Project.Photos"/>. The user is prompted before continuing.
    ''' </summary>
    Private Sub RemoveAllPhotos()
        If AskRemovePhotoCorrelation() Then
            Project.Photos = Enumerable.Empty(Of Photo)

            RefreshPhotosLV(True)
        End If
    End Sub

    ''' <summary>
    ''' Updates the array from which <see cref="LVPhotos"/> retrieves its items. This is to be called after any changes (single or multiple) are made to the contents of <see cref="Project.photos"/>.
    ''' </summary>
    ''' <param name="AlsoRunCorrelation">If <c>true</c>, this method will call <see cref="RunCorrelation()"/> after updating <see cref="LVPhotos"/>.</param>
    Private Sub RefreshPhotosLV(AlsoRunCorrelation As Boolean)
        If Project IsNot Nothing Then

            Dim specialItemSelectionCount = (From i In Me._SpecialLocationItems Where i.Selected).Count
            Dim selectedLocationItems = (From i In _LocationLVItems Where i.Selected).Except(_SpecialLocationItems)

            If (specialItemSelectionCount = 1) And (selectedLocationItems.Count = 0) Then
                'if a single special item and no location items are selected...
                Select Case True
                    Case Me._SpecialLocationItemAllFilesItem.Selected
                        'all photos
                        _PhotoLVItems = (From p In Project.Photos Select p.ListviewItem).ToArray
                    Case Me._SpecialLocationItemPhotosWithMultipleLocations.Selected
                        'only photos with multiple locations
                        _PhotoLVItems = (From p In Project.Photos Where p.LocationCount > 1 Select p.ListviewItem).ToArray
                    Case Me._SpecialLocationItemUnassociatedFilesItem.Selected
                        'photos with no locations
                        _PhotoLVItems = (From p In Project.Photos Where p.LocationCount = 0 Select p.ListviewItem).ToArray
                End Select
            ElseIf (specialItemSelectionCount = 0) And (selectedLocationItems.Count = 0) Then
                'if no locations are selected at all, display all photos
                _PhotoLVItems = (From p In Project.Photos Select p.ListviewItem).ToArray
            ElseIf (specialItemSelectionCount = 0) And (selectedLocationItems.Count > 0) Then
                'if one or more location items and no special items are selected, display photos associated with selected locations
                '_PhotoLVItems = (From l In Project.Locations From p In l.Photos Where l.ListViewItem.Selected Select p.ListviewItem).Distinct.ToArray

                _PhotoLVItems = (From l In (From l2 In Project.Locations Where selectedLocationItems.Contains(l2.ListViewItem)) From p In l.Photos Distinct Select p.ListviewItem).ToArray
            Else
                'if more than one special location item is selected or selection contains both special location items and normal locations, display no photo items
                _PhotoLVItems = {}
            End If

            Me.LVPhotos.VirtualListSize = _PhotoLVItems.Count

            If AlsoRunCorrelation Then RunCorrelation()

        End If

    End Sub

    ''' <summary>
    ''' Rename all of the files associated with <see cref="Photo"/> instances for which a <see cref="ListViewItem"/> is currently displayed in <see cref="LVPhotos"/>.
    ''' </summary>
    Private Sub RenameAllPhotos()
        If _PhotoLVItems.Count > 0 Then
            Dim selectedphotos = (From i In Project.Photos Where _PhotoLVItems.Contains(i.ListviewItem)).ToList

            Dim count = selectedphotos.Count
            Dim current = 0

            InitShowProgress(True, AppControls)

            Dim a =
                Sub()
                    For Each i In selectedphotos
                        If CancelTask Then Exit For
                        current += 1

                        i.RenameFile()
                        SetProgressStatus(String.Format("Renaming files. {0} remaining", count - current), current / count)
                    Next

                    InitShowProgress(False, AppControls)
                End Sub

            a.BeginInvoke(Nothing, Nothing)
        End If
    End Sub

#Region "Sort Photos to folder"
    ''' <summary>
    ''' Delegate function to be called by <see cref="SortPhotosToFolderGeneric"/>,
    ''' </summary>
    ''' <param name="Photo">The <see cref="photo"/> this instance is being called for.</param>
    ''' <param name="BaseFolder">The folder selected by the user when <see cref="SortPhotosToFolderGeneric"/> was first called.</param>
    ''' <remarks>The sorting function method is called on a different thread than the one <see cref="SortPhotosToFolderGeneric"/> was called from. Any interaction with the UI from within the sorting function will require a call to <see cref="Control.Invoke([Delegate])"/>. Calls to <see cref="Control.InvokeRequired"/> will return <c>True</c>.</remarks>
    Private Delegate Sub SortPhotoFunction(Photo As Photo, BaseFolder As String)

    ''' <summary>
    ''' Prompt the user for a folder. Calls <paramref name="SortFunction"/> for each photo in the current view passing the folder selected by the user.
    ''' </summary>
    ''' <param name="SortFunction">The function which will be called for each photo in the current view.</param>
    ''' <remarks>The sorting function method is called on a different thread than the one <see cref="SortPhotosToFolderGeneric"/> was called from. Any interaction with the UI from within the sorting function will require a call to <see cref="Control.Invoke([Delegate])"/>. Calls to <see cref="Control.InvokeRequired"/> will return <c>True</c>.</remarks>
    Private Sub SortPhotosToFolderGeneric(SortFunction As SortPhotoFunction)
        Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog
        fb.IsFolderPicker = True

        Dim dialogres = fb.ShowDialog()
        If dialogres = DialogResult.OK Then
            Dim selectedphotos = (From i In Project.Photos Where _PhotoLVItems.Contains(i.ListviewItem)).ToList 'all photos in current view

            Dim folder = fb.FileName

            Dim current = 0
            Dim count = selectedphotos.Count

            InitShowProgress(True, Me.AppControls)

            Dim a = Sub()
                        For Each i In selectedphotos

                            current += 1

                            If Not Me.CancelTask Then
                                If True Then
                                    SortFunction(i, folder)

                                    SetProgressStatus(String.Format("Sorting photo files into folder. {0} remaining.", count - current), current / count, False)
                                End If
                            End If
                        Next

                        InitShowProgress(False, Me.AppControls)
                    End Sub
            a.BeginInvoke(Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Prompt user for folder. Move all photos to sub-folders by location name.
    ''' </summary>
    ''' <remarks>
    ''' If a photo has multiple locations, the sub-folder will be a comma-space delimited list of the location names sorted textually ascending.
    ''' </remarks>
    Private Sub SortPhotosToFolderByLocationName()
        SortPhotosToFolderGeneric(Sub(i, folder)
                                      If i.LocationCount > 0 Then
                                          Dim proposedFolder = IO.Path.Combine(folder, String.Join(", ", (From l In i.Locations Order By l.LocationName Ascending Select l.LocationName).Distinct.ToArray))

                                          If Not IO.Directory.Exists(proposedFolder) Then IO.Directory.CreateDirectory(proposedFolder)

                                          i.MoveToFolder(proposedFolder)
                                      End If
                                  End Sub)

    End Sub

    ''' <summary>
    ''' Prompt user for folder. Move all photos to sub-folders named after the date the photo was taken in format "yyyy-MM-dd".
    ''' </summary>
    Private Sub SortPhotosToFolderByTakenDate()
        SortPhotosToFolderGeneric(Sub(i, folder)
                                      Dim proposedFolder = IO.Path.Combine(folder, i.TakenDate.ToString("yyyy-MM-dd"))

                                      If Not IO.Directory.Exists(proposedFolder) Then IO.Directory.CreateDirectory(proposedFolder)

                                      i.MoveToFolder(proposedFolder)
                                  End Sub)
    End Sub

    ''' <summary>
    ''' Prompt user for folder. Move all photos to this folder.
    ''' </summary>
    Private Sub SortPhotosToFolderSameFolder()
        SortPhotosToFolderGeneric(Sub(i, folder)
                                      i.MoveToFolder(folder)
                                  End Sub)
    End Sub

#End Region
#End Region


    Private Sub FMain_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CancelTask = True
    End Sub

End Class
#If DEBUG Then
Partial Public Class __KillDesigner
    'this is to prevent the file from opening as a blank form when double-clicked
End Class
#End If

'Photos listview methods
Partial Class FMain
    Private _PhotosOverlay As New WindowsForms.GMapOverlay("Photos")
    Private _PhotoLVItems() As ListViewItem = {}

    Private Sub AddPhotosFolder()
        Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

        fb.IsFolderPicker = True

        If fb.ShowDialog() = DialogResult.OK Then
            Dim MsgBoxRecursiveResponse = MsgBox("Load all photos from subfolders recursively as well?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question Or MsgBoxStyle.DefaultButton2)

            If MsgBoxRecursiveResponse <> MsgBoxResult.Cancel Then
                Dim folder = New IO.DirectoryInfo(fb.FileName)

                Dim AllFiles = From i In folder.EnumerateFiles("*", If(MsgBoxRecursiveResponse = MsgBoxResult.Yes, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))

                AddPhotoFiles(AllFiles)

            End If
        End If
    End Sub

    Private Sub AddPhotosFile()
        Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

        fb.AllowNonFileSystemItems = False
        fb.Filters.Add(New Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter("Supported Photo Types", String.Join(";", Photo.SupportedExtensions)))
        fb.Filters.Add(New Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter("All Files", "*"))

        fb.Multiselect = True
        fb.EnsureFileExists = True
        fb.EnsurePathExists = True

        If fb.ShowDialog() = DialogResult.OK Then
            AddPhotoFiles(From i In fb.FileNames Select New IO.FileInfo(i))
        End If
    End Sub

    Private Sub AddPhotoFiles(AllFiles As IEnumerable(Of IO.FileInfo))
        Dim a =
            Sub()
                Dim FilteredFiles = (From f In AllFiles From x In Photo.SupportedExtensions Where f.Extension.ToLower = x.ToLower Select f).ToList
                Dim AddedPhotos = New List(Of Photo)
                Dim count = FilteredFiles.Count
                Dim current = 0

                InitShowProgress(True, PhotoControls)

                Threading.Tasks.Parallel.ForEach(Of IO.FileInfo)(FilteredFiles,
                Sub(f, LoopState)
                    If CancelTask Then LoopState.Break()
                    If Not LoopState.ShouldExitCurrentIteration Then
                        current += 1
                        SetProgressStatus(String.Format("Adding photos. {0} remaining.", count - current), current / count)
                        Dim NewPhoto = Photo.FromFile(f, Project)
                        AddedPhotos.Add(NewPhoto)
                    End If
                End Sub)

                InitShowProgress(False, PhotoControls)
                Project.Photos = (From i In Project.Photos.Concat(AddedPhotos) Where i IsNot Nothing Order By i.TakenDate).Distinct(New PhotoComparer)
                Me.LVPhotos.Invoke(Sub() RefreshPhotosLV(True))
            End Sub

        a.BeginInvoke(Nothing, Nothing)
    End Sub

    Private Sub RemoveAllPhotos()
        If AskRemovePhotoCorrelation() Then
            Project.Photos = Enumerable.Empty(Of Photo)

            RefreshPhotosLV(True)
        End If
    End Sub

    Private Sub RefreshPhotosLV(AlsoRunCorrelation As Boolean)
        If Project IsNot Nothing Then

            Dim specialItemSelectionCount = (From i In Me._SpecialLocationItems Where i.Selected).Count
            Dim selectedLocationItems = (From i In _LocationLVItems Where i.Selected).Except(_SpecialLocationItems)

            If (specialItemSelectionCount = 1) And (selectedLocationItems.Count = 0) Then
                'if a single special item and no location items are selected...
                Select Case True
                    Case Me._AllFilesItem.Selected
                        'all photos
                        _PhotoLVItems = (From p In Project.Photos Select p.ListviewItem).ToArray
                    Case Me._PhotosWithMultipleLocations.Selected
                        'only photos with multiple locations
                        _PhotoLVItems = (From p In Project.Photos Where p.LocationCount > 1 Select p.ListviewItem).ToArray
                    Case Me._UnassociatedFilesItem.Selected
                        'photos with no locations
                        _PhotoLVItems = (From p In Project.Photos Where p.LocationCount = 0 Select p.ListviewItem).ToArray
                End Select
            ElseIf (specialItemSelectionCount = 0) And (selectedLocationItems.Count = 0) Then
                'if no locations are selected at all, display all photos
                _PhotoLVItems = (From p In Project.Photos Select p.ListviewItem).ToArray
            ElseIf (specialItemSelectionCount = 0) And (selectedLocationItems.Count > 0) Then
                'if one or more location items and no special items are selected, display photos associated with selected locations
                _PhotoLVItems = (From p In Project.Photos From l In Project.Locations Where l.Photos.Contains(p) And l.ListViewItem.Selected Select p.ListviewItem).Distinct.ToArray
            Else
                'if more than one special location item is selected or selection contains both special location items and normal locations, display no photo items
                _PhotoLVItems = {}
            End If

            Me.LVPhotos.VirtualListSize = _PhotoLVItems.Count

            If AlsoRunCorrelation Then RunCorrelation()

        End If


    End Sub

    Private Sub RenameAllPhotos()
        If _PhotoLVItems.Count > 0 Then

            Dim selectedphotos = From i In Project.Photos Where _PhotoLVItems.Contains(i.ListviewItem) And i.ListviewItem.Selected

            If selectedphotos.Count = 0 Then
                selectedphotos = From i In Project.Photos Where _PhotoLVItems.Contains(i.ListviewItem)
            End If

            Threading.Tasks.Parallel.ForEach(Of Photo)(selectedphotos,
                Sub(i)
                    i.RenameFile()
                End Sub)

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

            InitShowProgress(True, Me.PhotoControls)

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

                        InitShowProgress(False, Me.PhotoControls)
                    End Sub
            a.BeginInvoke(Nothing, Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Prompt user for folder. Move all photos to subfolders by location name.
    ''' </summary>
    ''' <remarks>
    ''' If a photo has multiple locations, the subfolder will be a comma-space delimited list of the location names sorted textually ascending.
    ''' </remarks>
    Private Sub SortPhotosToFolderByLocationName()
        SortPhotosToFolderGeneric(Sub(i, folder)
                                      Dim proposedFolder = IO.Path.Combine(folder, String.Join(", ", (From l In i.Locations Order By l.LocationName Ascending Select l.LocationName).Distinct.ToArray))

                                      If Not IO.Directory.Exists(proposedFolder) Then IO.Directory.CreateDirectory(proposedFolder)

                                      i.MoveToFolder(proposedFolder)
                                  End Sub)

    End Sub

    ''' <summary>
    ''' Prompt user for folder. Move all photos to subfolders named after the date the photowas taken in format "yyyy-MM-dd".
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

End Class



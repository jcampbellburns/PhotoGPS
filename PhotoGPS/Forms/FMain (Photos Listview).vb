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



    Private Sub RemoveSelectedPhotos()
        Dim selectedphotos = From i In Project.Photos Where _PhotoLVItems.Contains(i.ListviewItem) And i.ListviewItem.Selected

        If AskRemovePhotoCorrelation(selectedphotos) Then
            Project.Photos = Project.Photos.Except(selectedphotos)

            RefreshPhotosLV(True)
        End If
    End Sub

    Private Sub RemoveDeadPhotos()
        Dim selectedphotos = From i In Project.Photos Where IO.File.Exists(i.Filename) = False

        If AskRemovePhotoCorrelation(selectedphotos) Then
            Project.Photos = Project.Photos.Except(selectedphotos)

            RefreshPhotosLV(True)
        End If
    End Sub

    Private Sub RemoveAllPhotos()
        If AskRemovePhotoCorrelation() Then
            Project.Photos = Enumerable.Empty(Of Photo)

            RefreshPhotosLV(True)
        End If
    End Sub

    Private Sub RefreshPhotosLV(AlsoRunCorrelation As Boolean)
        Dim specialItemSelectionCount = (From i In Me._SpecialLocationItems Where i.Selected).Count

        Dim selectedLocationItems = (From i In _LocationLVItems Where i.Selected).Except(_SpecialLocationItems)

        If specialItemSelectionCount = 1 Then
            Select Case True
                Case Me._AllFilesItem.Selected
                    _PhotoLVItems = (From p In Project.Photos Select p.ListviewItem).ToArray
                Case Me._PhotosWithMultipleLocations.Selected
                    _PhotoLVItems = (From p In Project.Photos Where p.LocationCount > 1 Select p.ListviewItem).ToArray
                Case Me._UnassociatedFilesItem.Selected
                    _PhotoLVItems = (From p In Project.Photos Where p.LocationCount = 0 Select p.ListviewItem).ToArray
            End Select
        ElseIf (specialItemSelectionCount > 1) Or (selectedLocationItems.Count > 0) And (specialItemSelectionCount > 0) Then
            'if more than one special location item is selected or selection contains both special location items and normal locations
            _PhotoLVItems = {}
        ElseIf selectedLocationItems.Count = 0 Then
            'no locations are selected
            Me._AllFilesItem.Selected = True
        Else
            _PhotoLVItems = (From p In Project.Photos From l In Project.Locations Where l.Photos.Contains(p) And l.ListViewItem.Selected Select p.ListviewItem).Distinct.ToArray
        End If

        Me.LVPhotos.VirtualListSize = _PhotoLVItems.Count

        If AlsoRunCorrelation Then RunCorrelation()
    End Sub

    Private Sub RunCorrelation()
        Threading.Tasks.Parallel.ForEach(Of Location)(From i In Project.Locations Where _LocationLVItems.Contains(i.ListViewItem),
                                                      Sub(i)
                                                          Dim a = i.PhotoCount
                                                          i.UpdateListViewItem()
                                                      End Sub)

        LVLocations.Refresh()
        LVPhotos.Refresh()
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

    Private Sub SortPhotosByLocationName()
        Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog
        fb.IsFolderPicker = True
        If fb.ShowDialog() = DialogResult.OK Then
            Dim selectedphotos = From i In Project.Photos Where _PhotoLVItems.Contains(i.ListviewItem) 'all photos in current view

            Dim folder = fb.FileName

            Threading.Tasks.Parallel.ForEach(Of Photo)(selectedphotos,
                Sub(i)
                    Dim proposedFolder = IO.Path.Combine(folder, String.Join(", ", (From l In i.Locations Order By l.LocationName Ascending Select l.LocationName).Distinct.ToArray))

                    If Not IO.Directory.Exists(proposedFolder) Then IO.Directory.CreateDirectory(proposedFolder)

                    i.MoveToFolder(proposedFolder)
                End Sub)


        End If
    End Sub

    'Private Sub AddPhotoFiles(files As IEnumerable(Of String))

    '    WaitWindow.WaitForIt.DoIt("Loading photo information",
    '    Sub(pb)
    '        Try
    '            pb.Invoke("Enumerating files", -1)
    '            Dim FileInfos = (From i In files Select New IO.FileInfo(i)).ToList

    '            Dim count = FileInfos.Count

    '            For index = 0 To count - 1
    '                pb.Invoke(String.Format("Reading file info. {0} remaining", count - index), index / count)

    '                Dim p = Photo.FromFile(FileInfos(index))

    '                If p IsNot Nothing Then
    '                    Dim tmp = (From i In _PhotoLVItems Where p.Filename = i.Item.Filename)
    '                    Dim tmpc = tmp.Count

    '                    If tmpc = 0 Then
    '                        _PhotoLVItems.Add(New LVItem(Of Photo) With {.Item = p, .Marker = New WindowsForms.Markers.GMarkerGoogle(p.GPS, GMarkerGoogleType.blue), .LVItem = Me.UpdatePhotoLVItem(p)})
    '                    End If

    '                End If
    '            Next


    '            RefreshPhotos()
    '            UpdateLocationPhotosLists(pb)



    '        Catch ex As WaitWindow.DoItCanceledException
    '        End Try
    '    End Sub)

    'End Sub

    'Private Sub RefreshPhotos()

    '    Dim a =
    '        Sub()

    '            If (LVLocations.Items.Count >= Me._SpecialLocationItems.Count) And (LVLocations.SelectedItems.Count = 1) Then
    '                LVPhotos.BeginUpdate()

    '                'Filtering for special location items
    '                Select Case True
    '                    Case LVLocations.SelectedItems(0) Is _AllFilesItem
    '                        UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, True, Function(p) True)
    '                    Case LVLocations.SelectedItems(0) Is _UnassociatedFilesItem
    '                        UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, True, Function(p) p.LocationCount = 0)
    '                    Case LVLocations.SelectedItems(0) Is _PhotosWithMultipleLocations
    '                        UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, True, Function(p) p.LocationCount > 1)
    '                    Case Else
    '                        Dim SelectedLocation = (From i In _LocationLVItems Where i.LVItem.Selected).First
    '                        If (_LocationLVItems.Count > 0) And (_PhotoLVItems.Count > 0) Then

    '                            Dim SelectedLocationPhotosLVItems = From i In _PhotoLVItems Where i.Item.Locations.Contains(SelectedLocation.Item)
    '                            UpdateListView(Of Photo)(SelectedLocationPhotosLVItems, LVPhotos, True, Function(i) True)
    '                        End If
    '                End Select

    '                AutosizeColumns(LVPhotos)
    '                LVPhotos.EndUpdate()
    '            End If
    '        End Sub

    '    If LVPhotos.InvokeRequired Then
    '        LVPhotos.Invoke(a)
    '    Else
    '        a.Invoke
    '    End If


    'End Sub



    'Private Sub TSBRenamePhotoFiles_Click(sender As Object, e As EventArgs) Handles TSBRenamePhotoFiles.Click
    '    Dim s = (From i In _PhotoLVItems Where i.LVItem.Selected Select i.Item).ToList

    '    If Not s.Count = 0 Then
    '        WaitWindow.WaitForIt.DoIt("Renaming files",
    '    Sub(pb)
    '        Dim l = s.Count - 1
    '        Dim i = 0I

    '        Try
    '            For i = 0 To l
    '                pb.Invoke(String.Format("Renaming files. {0} remaining.", l - i), i / l)
    '                s(i).RenameFile()
    '            Next
    '        Catch ex As WaitWindow.DoItCanceledException
    '            MsgBox(String.Format("Canceled as requested by user. Please note only {0} of {1} files were renamed.", i, l))
    '        End Try
    '    End Sub, Me)
    '    End If
    'End Sub

    'Private Sub TSBRemoveAllPhotos_Click(sender As Object, e As EventArgs) Handles TSBRemoveAllPhotos.Click
    '    If MsgBox("This will remove all photos. Re-adding the photos will not restore manually associated locations. Continue?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
    '        _PhotoLVItems.Clear()

    '        UpdateLocationPhotosLists()
    '        RefreshPhotos()
    '    End If
    'End Sub

    'Private Sub TSBRemoveSelectedPhotos_Click(sender As Object, e As EventArgs) Handles TSBRemoveSelectedPhotos.Click
    '    _PhotoLVItems.RemoveAll(Function(i) i.LVItem.Selected)
    '    UpdateLocationPhotosLists()
    '    RefreshPhotos()
    'End Sub

    'Private Sub TSBRemovePhotosNoLongerAvailable_Click(sender As Object, e As EventArgs) Handles TSBRemovePhotosNoLongerAvailable.Click
    '    _PhotoLVItems.RemoveAll(Function(i) (IO.File.Exists(i.Item.Filename) = False))
    '    UpdateLocationPhotosLists()
    '    RefreshPhotos()
    'End Sub

    'Private Sub TSBSortPhotosSameFolder_Click(sender As Object, e As EventArgs) Handles TSBSortPhotosSameFolder.Click
    '    Dim photos = (From i In _PhotoLVItems Where i.LVItem.Selected).ToArray
    '    If photos.Count = 0 Then photos = (From i In _PhotoLVItems Where i.LVItem.ListView Is LVPhotos).ToArray

    '    Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

    '    fb.IsFolderPicker = True

    '    If fb.ShowDialog() = DialogResult.OK Then

    '        WaitWindow.WaitForIt.DoIt("Organizing Photos",
    '            Sub(pb)
    '                Try
    '                    For i = 0 To photos.Count - 1
    '                        pb.Invoke(String.Format("Moving photos. {0} remaining", photos.Count - i), i / photos.Count)

    '                        Dim p = photos(i)

    '                        p.Item.MoveToFolder(fb.FileName)

    '                    Next i
    '                Catch ex As WaitWindow.DoItCanceledException

    '                End Try
    '            End Sub, Me)
    '    End If
    'End Sub

    'Private Sub TSBSortPhotosByTakenDate_Click(sender As Object, e As EventArgs) Handles TSBSortPhotosByTakenDate.Click
    '    Dim photos = (From i In _PhotoLVItems Where i.LVItem.Selected).ToArray
    '    If photos.Count = 0 Then photos = (From i In _PhotoLVItems Where i.LVItem.ListView Is LVPhotos).ToArray

    '    Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

    '    fb.IsFolderPicker = True

    '    If fb.ShowDialog() = DialogResult.OK Then


    '        WaitWindow.WaitForIt.DoIt("Organizing Photos",
    '        Sub(pb)
    '            Try
    '                For i = 0 To photos.Count - 1
    '                    pb.Invoke(String.Format("Moving photos. {0} remaining", photos.Count - i), i / photos.Count)

    '                    Dim p = photos(i)

    '                    Dim subfolder = IO.Path.Combine(fb.FileName, p.Item.TakenDate.ToString("yyyy-MM-dd"))

    '                    If Not IO.Directory.Exists(subfolder) Then
    '                        IO.Directory.CreateDirectory(subfolder)
    '                    End If

    '                    p.Item.MoveToFolder(subfolder)

    '                Next i
    '            Catch ex As WaitWindow.DoItCanceledException

    '            End Try
    '        End Sub, Me)
    '    End If
    'End Sub

    'Private Sub TSBSortPhotosByLocationName_Click(sender As Object, e As EventArgs) Handles TSBSortPhotosByLocationName.Click
    '    Dim photos = (From i In _PhotoLVItems Where i.LVItem.Selected And i.Item.LocationCount > 0).ToArray
    '    If photos.Count = 0 Then photos = (From i In _PhotoLVItems Where i.LVItem.ListView Is LVPhotos And i.Item.LocationCount > 0).ToArray

    '    Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

    '    fb.IsFolderPicker = True

    '    If fb.ShowDialog() = DialogResult.OK Then


    '        WaitWindow.WaitForIt.DoIt("Organizing Photos",
    '        Sub(pb)
    '            Try
    '                For i = 0 To photos.Count - 1
    '                    pb.Invoke(String.Format("Moving photos. {0} remaining", photos.Count - i), i / photos.Count)

    '                    Dim p = photos(i)

    '                    Dim subfolder = IO.Path.Combine(fb.FileName, String.Join(", ", From l In p.Item.Locations Select l.LocationName Order By LocationName Ascending Distinct))

    '                    If Not IO.Directory.Exists(subfolder) Then
    '                        IO.Directory.CreateDirectory(subfolder)
    '                    End If

    '                    p.Item.MoveToFolder(subfolder)

    '                Next i
    '            Catch ex As WaitWindow.DoItCanceledException

    '            End Try
    '        End Sub, Me)
    '    End If
    'End Sub

    'Private Sub TSBRemovePhotos_Click(sender As Object, e As EventArgs) Handles TSBRemovePhotos.Click
    '    TSBRemoveSelectedPhotos_Click(sender, e)
    'End Sub

    '''' <summary>
    '''' Creates or updates a <see cref="ListViewItem"/> from a <see cref="Photo"/>.
    '''' </summary>
    '''' <param name="p">The <see cref="Photo"/> from which to create the <see cref="ListViewItem"/>.</param>
    '''' <param name="lvItem">Optional. The <see cref="ListViewItem"/> to update. If not specified, a new <see cref="ListViewItem"/> is created.</param>
    '''' <returns>Returns the <see cref="ListViewItem"/> that was updated or created.</returns>
    '''' <remarks>The listview is created with four subitems:
    '''' <list type="bullet">
    '''' <item><description><see cref="Photo.TakenDate"/></description></item>
    '''' <item><description><see cref="Photo.Lat"/> formatted to 6 digits of precision</description></item>
    '''' <item><description><see cref="Photo.Long"/> formatted to 6 digits of precision</description></item>
    '''' <item><description><see cref="Photo.LocationCount"/></description></item>
    '''' </list>
    '''' </remarks>
    'Private Function UpdatePhotoLVItem(p As Photo, Optional lvItem As ListViewItem = Nothing) As ListViewItem
    '    Dim lvi = If(lvItem, New ListViewItem)

    '    lvi.SubItems.Clear()
    '    lvi.Text = p.TakenDate
    '    lvi.SubItems.AddRange({
    '                          p.Lat.ToString("0.000000"),
    '                          p.Long.ToString("0.000000"),
    '                          p.LocationCount})


    '    Return lvi
    'End Function



End Class



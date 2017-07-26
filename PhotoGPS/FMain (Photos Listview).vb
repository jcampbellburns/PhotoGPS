#If DEBUG Then
Partial Public Class __KillDesigner
    'this is to prevent the file from opening as a blank form when double-clicked
End Class
#End If

'Photos listview methods
Partial Class FMain
    Private _PhotoLVItems As New List(Of LVItem(Of Photo))
    Private _PhotosOverlay As New WindowsForms.GMapOverlay("Photos")

    Private Sub TSBAddPhotosFolder_Click(sender As Object, e As EventArgs) Handles TSBAddPhotosFolder.Click

        Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

        fb.IsFolderPicker = True

        If fb.ShowDialog() = DialogResult.OK Then
            Dim MsgBoxRecursiveResponse = MsgBox("Load all photos from subfolders recursively as well?", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question Or MsgBoxStyle.DefaultButton2)

            If MsgBoxRecursiveResponse <> MsgBoxResult.Cancel Then
                Dim AllFiles = IO.Directory.EnumerateFiles(fb.FileName, "*", If(MsgBoxRecursiveResponse = MsgBoxResult.Yes, IO.SearchOption.AllDirectories, IO.SearchOption.TopDirectoryOnly))

                Dim FilteredFiles As New List(Of String)

                For Each ext In Photo.SupportedExtensions
                    FilteredFiles.AddRange(From i In AllFiles Where i Like ext)
                Next

                AddPhotoFiles(FilteredFiles.Distinct)

            End If
        End If

    End Sub

    Private Sub TSBAddPhotosFile_Click(sender As Object, e As EventArgs) Handles TSBAddPhotosFile.Click
        Dim fb As New Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog

        fb.AllowNonFileSystemItems = False
        fb.Filters.Add(New Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter("Supported Photo Types", String.Join(";", Photo.SupportedExtensions)))
        fb.Filters.Add(New Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogFilter("All Files", "*"))

        fb.Multiselect = True
        fb.EnsureFileExists = True
        fb.EnsurePathExists = True

        If fb.ShowDialog() = DialogResult.OK Then
            AddPhotoFiles(fb.FileNames)
        End If
    End Sub

    Private Sub AddPhotoFiles(files As IEnumerable(Of String))

        WaitWindow.WaitForIt.DoIt("Loading photo information",
        Sub(pb)
            Try
                pb.Invoke("Enumerating files", -1)
                Dim FileInfos = (From i In files Select New IO.FileInfo(i)).ToList

                Dim count = FileInfos.Count

                For index = 0 To count - 1
                    pb.Invoke(String.Format("Reading file info. {0} remaining", count - index), index / count)

                    Dim p = Photo.FromFile(FileInfos(index))

                    If p IsNot Nothing Then
                        Dim tmp = (From i In _PhotoLVItems Where p.Filename = i.Item.Filename)
                        Dim tmpc = tmp.Count

                        If tmpc = 0 Then
                            _PhotoLVItems.Add(New LVItem(Of Photo) With {.Item = p, .Marker = New WindowsForms.Markers.GMarkerGoogle(p.GPS, GMarkerGoogleType.blue), .LVItem = New ListViewItem({p.TakenDate, p.Lat.ToString("0.000000"), p.Long.ToString("0.000000")})})
                        End If

                    End If
                Next

                UpdateLocationPhotosLists()
                RefreshPhotos()


            Catch ex As WaitWindow.DoItCanceledException
            End Try
        End Sub)

    End Sub

    Private Sub RefreshPhotos()

        Dim a =
            Sub()

                If (LVLocations.Items.Count >= Me._SpecialLocationItems.Count) And (LVLocations.SelectedItems.Count = 1) Then
                    LVPhotos.BeginUpdate()

                    'Filtering for special location items
                    Select Case True
                        Case LVLocations.SelectedItems(0) Is _AllFilesItem
                            UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, True, Function(p) True)
                        Case LVLocations.SelectedItems(0) Is _UnassociatedFilesItem
                            UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, True, Function(p) p.LocationCount = 0)
                        Case LVLocations.SelectedItems(0) Is _PhotosWithMultipleLocations
                            UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, True, Function(p) p.LocationCount > 1)
                        Case Else
                            Dim SelectedLocation = (From i In _LocationLVItems Where i.LVItem.Selected).First
                            If (_LocationLVItems.Count > 0) And (_PhotoLVItems.Count > 0) Then

                                Dim SelectedLocationPhotosLVItems = From i In _PhotoLVItems Where i.Item.Locations.Contains(SelectedLocation.Item)
                                UpdateListView(Of Photo)(SelectedLocationPhotosLVItems, LVPhotos, True, Function(i) True)
                            End If
                    End Select

                    AutosizeColumns(LVPhotos)
                    LVPhotos.EndUpdate()
                End If
            End Sub

        If LVPhotos.InvokeRequired Then
            LVPhotos.Invoke(a)
        Else
            a.Invoke
        End If


    End Sub

    Private Sub TSBRenamePhotoFiles_Click(sender As Object, e As EventArgs) Handles TSBRenamePhotoFiles.Click
        Dim s = (From i In _PhotoLVItems Where i.LVItem.Selected Select i.Item).ToList

        If Not s.Count = 0 Then
            WaitWindow.WaitForIt.DoIt("Renaming files",
        Sub(pb)
            Dim l = s.Count - 1
            Dim i = 0I

            Try
                For i = 0 To l
                    pb.Invoke(String.Format("Renaming files. {0} remaining.", l - i), i / l)
                    s(i).RenameFile()
                Next
            Catch ex As WaitWindow.DoItCanceledException
                MsgBox(String.Format("Canceled as requested by user. Please note only {0} of {1} files were renamed.", i, l))
            End Try
        End Sub, Me)
        End If
    End Sub

    Private Sub TSBRemoveAllPhotos_Click(sender As Object, e As EventArgs) Handles TSBRemoveAllPhotos.Click
        If MsgBox("This will remove all photos. Re-adding the photos will not restore manually associated locations. Continue?", MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            _PhotoLVItems.Clear()

            UpdateLocationPhotosLists()
            RefreshPhotos()
        End If
    End Sub

    Private Sub TSBRemoveSelectedPhotos_Click(sender As Object, e As EventArgs) Handles TSBRemoveSelectedPhotos.Click
        _PhotoLVItems.RemoveAll(Function(i) i.LVItem.Selected)
        UpdateLocationPhotosLists()
        RefreshPhotos()
    End Sub

    Private Sub TSBRemovePhotosNoLongerAvailable_Click(sender As Object, e As EventArgs) Handles TSBRemovePhotosNoLongerAvailable.Click
        _PhotoLVItems.RemoveAll(Function(i) (IO.File.Exists(i.Item.Filename) = False))
        UpdateLocationPhotosLists()
        RefreshPhotos()
    End Sub

End Class



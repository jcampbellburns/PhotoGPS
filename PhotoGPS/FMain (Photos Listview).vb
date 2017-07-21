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
                UpdatePhotosListview()

            Catch ex As WaitWindow.DoItCanceledException
            End Try
        End Sub)

    End Sub

    Private Sub UpdatePhotosListview()

        Dim a =
            Sub()
                Dim Selection = (From i In LVLocations.SelectedItems).ToList

                If Selection.Count > 0 Then

                    LVPhotos.BeginUpdate()

                    'If Selection.Contains(_AllFilesItem) Then
                    If Selection.Intersect(_SpecialLocationItems).Count > 0 Then

                        If Selection.Count > 1 Then
                            'A selection in the Locations listview may contain either "(All Files)" or any combination of single or multiple locations which are not "(All Files)". If an attempt is made to select multiple and include "(All Files)", clear the selection and select only "(All Files)".
                            LVLocations.SelectedItems.Clear()
                            _AllFilesItem.Selected = True
                        Else
                            'If "(All Files)" is selected, show folders and files
                            If _AllFilesItem.Selected Then
                                UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, True, Function(p) True)
                            ElseIf _UnassociatedFilesItem.Selected Then
                                UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, True, Function(p) p.LocationCount = 0)
                            End If
                        End If
                            Else

                        'If one or more locations are selected, show photos relevant to all of them

                        If (_LocationLVItems.Count > 0) And (_PhotoLVItems.Count > 0) Then
                            'The code in this if...then block:
                            '   1: Get a list of the selected items in LocationsLV
                            '   2: Convert to list of LVItem(Of Location) from _LocationLVItems (this is to get the relevant Location from each ListViewItem
                            '   3: Get all of the photos from the list of LVItem(Of Location). The list is initially a List(Of Photo) for each location so the result of the query is List(Of List(Of Photo)).
                            '   4: Convert said list to a single List(Of Photo)
                            '   5: Since a photo might belong to more than one Location, remove duplicate items using List(Of Photo).Distinct.ToList (we need it to still be a List(of Photo) when we're done)
                            '   6: We need a List(Of LVItem(Of Photo)) to update the listview.
                            '   7: Update the ListView

                            'Step 1 is above where local variable Selection is defined

                            'Step 2
                            Dim SelectedLVItems = From i As ListViewItem In Selection From j In _LocationLVItems Where j.LVItem Is i Select j

                            'Step 3
                            Dim ListOfListOfPhotosFromSelectedLocations = (From i In SelectedLVItems Select i.Item.Photos).ToList

                            'Step 4
                            Dim ListOfPhotosFromSelectedLocations As New List(Of Photo)

                            ListOfListOfPhotosFromSelectedLocations.ForEach(
                                        Sub(i)
                                            If i IsNot Nothing Then
                                                ListOfPhotosFromSelectedLocations.AddRange(i)
                                            End If
                                        End Sub)

                            'Step 5
                            ListOfPhotosFromSelectedLocations = ListOfPhotosFromSelectedLocations.Distinct.ToList

                            'Step 6
                            Dim ListOfLVItemOfPhotoFromSelectedLocations = New List(Of LVItem(Of Photo))

                            ListOfPhotosFromSelectedLocations.ForEach(Sub(i) ListOfLVItemOfPhotoFromSelectedLocations.Add((From j In _PhotoLVItems Where i Is j.Item).First))

                            'Step 7
                            UpdateListView(Of Photo)(ListOfLVItemOfPhotoFromSelectedLocations, LVPhotos, True, Function(i) True)

                        End If
                    End If

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

End Class

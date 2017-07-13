#If DEBUG Then
Partial Public Class __KillDesigner
    'this is to prevent the file from opening as a blank form when double-clicked
End Class
#End If

'Photos listview methods
Partial Class FMain
    Private _currentFolder As String
    Private _PhotoLVItems As List(Of LVItem(Of Photo))
    Private _FolderLVItems As List(Of LVItem(Of IO.DirectoryInfo))
    Private _PhotosOverlay As New WindowsForms.GMapOverlay("Photos")

    Private Sub TBFolder_TextChanged(sender As Object, e As EventArgs) Handles TBFolder.TextChanged
        'give visual feedback if the typed folder is valid
        TBFolder.ForeColor = If(IO.Directory.Exists(TBFolder.Text), SystemColors.ControlText, Drawing.Color.Red)
    End Sub

    Private Sub TSBBrowseForFolder_Click(sender As Object, e As EventArgs) Handles TSBBrowseForFolder.Click
        'browse for folder
        Dim folderBrowseWindow As New FolderBrowserDialog With {
            .ShowNewFolderButton = False,
            .SelectedPath = If(IO.Directory.Exists(TBFolder.Text), TBFolder.Text, "")}

        If folderBrowseWindow.ShowDialog = DialogResult.OK Then
            CurrentFolder = folderBrowseWindow.SelectedPath
        End If
    End Sub

    Private Sub TSBParentFolder_Click(sender As Object, e As EventArgs) Handles TSBParentFolder.Click
        NavToParentFolder()
    End Sub

    Private Sub NavToParentFolder()
        'nav to parent folder
        Dim parentDirInfo = IO.Directory.GetParent(_currentFolder)

        If parentDirInfo IsNot Nothing Then
            CurrentFolder = parentDirInfo.FullName
        End If
    End Sub

    Private Sub RefreshFilesFromFolder(pb As WaitWindow.PostBack)
        'Enumerate all files in current folder
        'Look for metadata cache file
        '   If it exists, import it.
        '   Verify filedata and filesize for each file listed.
        '      If mismatch or missing file, delete the entry
        '   Determine which files are emissing from metadata cache vs. folder
        '   Pull metadate from files missing from metadate cache
        '   Write new metadata cache file
        '   Update listview

        If IO.Directory.Exists(CurrentFolder) Then

            Try
                Dim files = New IO.DirectoryInfo(CurrentFolder).EnumerateFiles
                Dim photos As List(Of Photo)
                Dim cancelled = False
                Dim mf = New IO.FileInfo(CurrentFolder & "\" & My.Settings.MetadataCacheFilename)

                If Not mf.Exists Then
                    photos = New List(Of Photo)
                Else
                    'deserialize it
                    photos = CSVSerializer.CSVDeserializer(Of Photo).Deserialize(mf, Me, True, pb)

                    'remove outdated entries
                    photos.RemoveAll(
                        Function(i)
                            If pb IsNot Nothing Then pb("Removing outdated photos from metadata cache.", (photos.IndexOf(i) / photos.Count))
                            i.Path = CurrentFolder
                            Dim fwp = i.FilenameWithPath

                            If IO.File.Exists(fwp) Then
                                Return (IO.File.GetLastWriteTime(fwp) <> i.Filedate) Or (New IO.FileInfo(fwp).Length <> i.FileSize)
                            Else
                                Return True
                            End If


                        End Function)

                End If

                'Get a list of files to be updated
                Dim UpToDateFiles = (From i In photos Select i.Filename.ToUpper).ToList
                Dim NotUpToDateFiles = (From i In files Where Not UpToDateFiles.Contains(i.Name.ToUpper)).ToList

                'read info from each file and add it (if the file is supported)
                For Each i In NotUpToDateFiles
                    If pb IsNot Nothing Then pb(String.Format("Reading info from files. {0} remaining.", NotUpToDateFiles.Count - NotUpToDateFiles.IndexOf(i)), (NotUpToDateFiles.IndexOf(i) / NotUpToDateFiles.Count))

                    Dim p = Photo.FromFile(i)

                    If p IsNot Nothing Then
                        photos.Add(p)
                    End If
                Next

                'serialize the list and save it in the folder
                If mf.Exists Then mf.Delete()

                If photos.Count <> 0 Then
                    Dim csvData = CSVSerializer.CSVSerializer(Of Photo).Serialize(photos, pb)

                    If csvData <> String.Empty Then

                        Using s = mf.CreateText()
                            s.Write(csvData)
                            s.Flush()
                            s.Close()
                        End Using
                    End If
                End If

                Me.Invoke(
                    Sub()
                        '-update the folder list-


                        _PhotoLVItems = New List(Of LVItem(Of Photo))
                        _FolderLVItems = New List(Of LVItem(Of IO.DirectoryInfo))

                        'add folders to _FolderLVItems
                        Dim folders = New IO.DirectoryInfo(CurrentFolder).EnumerateDirectories()

                        For Each f In folders
                            Dim lv As New ListViewItem(f.Name, 0)

                            _FolderLVItems.Add(New LVItem(Of IO.DirectoryInfo) With {.Item = f, .LVItem = lv})
                        Next

                        'add photos to _PhotoLVItems
                        For Each p In photos
                            Dim subitems() As String = {p.TakenDate, p.Lat.ToString("#.######"), p.Long.ToString("#.######"), p.Filename, p.Filedate}
                            Dim lv As New ListViewItem(subitems, 1)

                            _PhotoLVItems.Add(New LVItem(Of Photo) With {.Item = p, .LVItem = lv})
                        Next

                        _PhotoLVItems.Sort(Function(a, b) Date.Compare(a.Item.TakenDate, b.Item.TakenDate))
                        _FolderLVItems.Sort(Function(a, b) String.Compare(a.Item.Name, b.Item.Name, True))


                        'TODO: Enable filtering
                        UpdateListView(Of IO.DirectoryInfo)(_FolderLVItems, LVPhotos, True, Function(p) True, pb)
                        UpdateListView(Of Photo)(_PhotoLVItems, LVPhotos, False, Function(p) True, pb)


                        UpdateLocationPhotosLists(pb)

                        'resize columns
                        AutosizeColumns(Me.LVPhotos)
                    End Sub)
            Catch ex As WaitWindow.DoItCanceledException
                Me.Invoke(
                    Sub()
                        LVPhotos.Items.Clear()
                        _PhotoLVItems = Nothing
                        _FolderLVItems = Nothing
                    End Sub)
            End Try

        Else
            _PhotoLVItems.Clear()
            LVPhotos.Clear()
        End If
    End Sub

    Public Property CurrentFolder As String
        Get
            Return _currentFolder
        End Get
        Set(value As String)
            If IO.Directory.Exists(value) Then
                _currentFolder = value
                TBFolder.Text = value

                WaitWindow.WaitForIt.DoIt("Loading folder", Sub(pb) RefreshFilesFromFolder(pb), Me)
            End If
        End Set
    End Property

    Private Sub TBFolder_KeyDown(sender As Object, e As KeyEventArgs) Handles TBFolder.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IO.Directory.Exists(TBFolder.Text) Then
                CurrentFolder = TBFolder.Text

                e.Handled = True
            End If
        End If
    End Sub

    Private Sub LVPhotos_DoubleClick(sender As Object, e As EventArgs) Handles LVPhotos.DoubleClick
        Dim a = LVPhotos.PointToClient(Control.MousePosition)

        Dim item = LVPhotos.GetItemAt(a.X, a.Y)

        If item IsNot Nothing Then
            If item.ImageIndex = 0 Then 'there's no real easy way to tell whether the highlighted item is a folder or a photo. We need to know this to know whether we're going to search for the corresponding item in Me._PhotoLVItems or Me._FolderLVItems so we use the imageindex of the item.
                'folder
                Me.CurrentFolder = (From i In _FolderLVItems Where item Is i.LVItem).First.Item.FullName

                'Else 'right now we have no double-click functionality for photos
                '    'photo
                '    Dim p = (From i In _PhotoLVItems Where item Is i.LVItem).First

            End If
        End If

    End Sub

    Private Sub LVPhotos_KeyDown(sender As Object, e As KeyEventArgs) Handles LVPhotos.KeyDown
        If e.KeyCode = Keys.Back Then
            NavToParentFolder()
        ElseIf e.Control And (e.KeyCode = Keys.A) Then '<CTRL> + A
            SelectAllListviewItems(LVPhotos, True)
        End If
    End Sub

    Private Sub FMain_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.CurrentFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
    End Sub


End Class

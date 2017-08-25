#If DEBUG Then
Partial Public Class __KillDesigner
    'this is to prevent the file from opening as a blank form when double-clicked
End Class
#End If

'Locations listview methods
Partial Class FMain
    Private _LocationsOverlay As New WindowsForms.GMapOverlay("Locations")

    Private _AllFilesItem As New ListViewItem({"(All Files)", "", "", "", "", "", "", "Undefined"})
    Private _PhotosWithMultipleLocations As New ListViewItem({"(Multiple matches)", "", "", "", "", "", "", "Undefined"})
    Private _UnassociatedFilesItem As New ListViewItem({"(No location match)", "", "", "", "", "", "", "Undefined"})

    Private _SpecialLocationItems As List(Of ListViewItem) = {_AllFilesItem, _PhotosWithMultipleLocations, _UnassociatedFilesItem}.ToList

    Private _Locations As IEnumerable(Of Location)
    Private _LocationLVItems As ListViewItem()

    Private Sub AddLocations(Locations As List(Of Location), pb As WaitWindow.PostBack)
        If Locations IsNot Nothing Then
            Me.Invoke(
                Sub()
                    Try
                        'Initialize location lists (LVItems, and markers)
                        _LocationLVItems.Clear()
                        _LocationsOverlay.Markers.Clear()

                        'save current state of location overlay then hides it (speeds up populating the markers on the map)
                        Dim visibility = _LocationsOverlay.IsVisibile
                        _LocationsOverlay.IsVisibile = False

                        'go through the locations list, create LVItems, map markers, and listview items, populate to appropriate places.
                        For Each l In Locations
                            'do a postback for to indicate progress
                            pb.Invoke("Updating locations list (object model)", (_LocationLVItems.Count / Locations.Count))

                            AddLocation(l)
                        Next

                        'restore whatever previous visibility the overlay had
                        _LocationsOverlay.IsVisibile = visibility

                        RefreshLocations(pb)
                        UpdateLocationPhotosLists(pb)

                    Catch ex As WaitWindow.DoItCanceledException
                        'user clicked cancel re-initialize location lists.
                        LVLocations.Items.Clear()
                        _LocationLVItems.Clear()
                        _LocationsOverlay.Markers.Clear()

                    End Try
                End Sub)
        End If
    End Sub

    Private Sub RefreshLocations(Optional pb As WaitWindow.PostBack = Nothing)

        Try
            _LocationsOverlay.Markers.Clear()

            UpdateListView(Of Location)(Me._LocationLVItems, LVLocations,
                True, Function(i)
                          Return (Not Me.TSBLocationDateFilter.FilterEnabled) Or (i.Start <= Me.TSBLocationDateFilter.FilterEnd And i.End >= Me.TSBLocationDateFilter.FilterStart)
                      End Function, pb)


            Dim q = (From i In _SpecialLocationItems Order By i.Text Descending).ToList
            q.ForEach(Sub(i) LVLocations.Items.Insert(0, i))


            LVLocations.SelectedItems.Clear()
            _AllFilesItem.Selected = True
            AutosizeColumns(LVLocations)

        Catch ex As WaitWindow.DoItCanceledException
            _LocationsOverlay.Markers.Clear()
            LVLocations.Items.Clear()

            Throw

        End Try
    End Sub

    Private Sub TSBImportLocations_Click(sender As Object, e As EventArgs) Handles TSBImportLocations.Click
        'load loactions from csv
        Dim Locations As List(Of Location)

        WaitWindow.WaitForIt.DoIt("Importing Locations",
            Sub(pb)
                Locations = CSVSerializer.CSVDeserializer(Of Location).Deserialize(Me, False, pb)

                AddLocations(Locations, pb)
            End Sub, Me)
    End Sub

    Private Sub TSBLocationDateFilter_FilterUpdated(sender As Object, e As EventArgs) Handles TSBLocationDateFilter.FilterUpdated

        If _LocationLVItems.Count < 1000 Then
            RefreshLocations()
        Else
            WaitWindow.WaitForIt.DoIt("Filtering", Sub(pb) Me.Invoke(Sub() RefreshLocations(pb)), Me)
        End If

    End Sub

    Private Sub TSBExportLocations_Click(sender As Object, e As EventArgs) Handles TSBExportLocations.Click
        'save locations to csv

        WaitWindow.WaitForIt.DoIt("Exporting Locations",
            Sub(pb)
                pb.Invoke("Waiting on user.", -1)

                Dim OpenWindow As New System.Windows.Forms.SaveFileDialog With {
                    .Filter = "Comma Separated Values|*.csv|All Files|*.*",
                    .DefaultExt = "csv"}

                Dim owRes As System.Windows.Forms.DialogResult
                Me.Invoke(Sub() owRes = OpenWindow.ShowDialog(Me))

                If owRes = System.Windows.Forms.DialogResult.OK Then
                    Dim a As New IO.FileInfo(OpenWindow.FileName)

                    Dim csvData = CSVSerializer.CSVSerializer(Of Location).Serialize((From i In _LocationLVItems Select i.Item), pb)

                    If csvData <> String.Empty Then
                        Using s = a.CreateText()
                            s.Write(csvData)
                            s.Flush()
                            s.Close()
                        End Using
                    End If
                End If
            End Sub, Me)
    End Sub

    Private Sub TSBLocationsVisible_CheckedChanged(sender As Object, e As EventArgs) Handles TSBLocationsVisible.CheckedChanged
        _LocationsOverlay.IsVisibile = TSBLocationsVisible.Checked
    End Sub

    Private Sub TSBGetLocationCoords_Click(sender As Object, e As EventArgs) Handles TSBGetLocationCoords.Click

        WaitWindow.WaitForIt.DoIt("Retreiving GPS coordinates for locations without GPS",
        Sub(pb)
            Try
                Dim a As List(Of LVItem(Of Location)) = Nothing

                Me.Invoke(Sub() a = (From i In _LocationLVItems Where (Me.LVLocations.Items.Contains(i.LVItem)) And (i.Item.GPS.HasValue = False) And (String.IsNullOrWhiteSpace(i.Item.Address) = False)).ToList)

                a.ForEach(
                Sub(i)
#If DEBUG Then
                    i = i 'for some reason, a lack of this line causes a compiler warning that i.Item and i.LVItem are being used before it was assigned.
#End If
                    pb.Invoke("Getting coordinates for " & i.Item.Address, a.IndexOf(i) / a.Count)

                    Dim status As GeoCoderStatusCode
                    i.Item.GPS = MapProviders.GMapProviders.GoogleMap.GetPoint(i.Item.Address, status)

                    If i.Item.GPS.HasValue Then
                        Me.Invoke(Sub() UpdateLocationLVItem(i.Item, i.LVItem))
#If DEBUG Then
                    Else
                        If status <> GeoCoderStatusCode.Unknow Then
                            'I haven't encountered this yet. When it happens, I'll need to analyze.
                            Stop
                        End If
#End If
                    End If
                End Sub)

            Catch ex As WaitWindow.DoItCanceledException
                'cancel was clicked by the user
            Finally
                Me.Invoke(Sub() AutosizeColumns(LVLocations))

                UpdateLocationPhotosLists()
                RefreshPhotos()
            End Try

        End Sub, Me)

    End Sub

    Private Sub LVLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVLocations.SelectedIndexChanged
        RefreshPhotos()
    End Sub

    Private Sub TSBAddLocation_Click(sender As Object, e As EventArgs) Handles TSBAddLocation.Click
        Dim a As New FLocationEditor

        If a.ShowDialog(Me) = DialogResult.OK Then
            AddLocation(a.Value)
        End If
    End Sub

    Private Sub TSBEditLocation_Click(sender As Object, e As EventArgs) Handles TSBEditLocation.Click
        Dim SelectedLocation = (From i In _LocationLVItems Where i.LVItem.Selected)

        If SelectedLocation.Count > 0 Then
            Dim a As New FLocationEditor

            a.Value = SelectedLocation.First.Item

            If a.ShowDialog(Me) = DialogResult.OK Then
                RefreshLocations()
                UpdateLocationPhotosLists()
            End If
        End If
    End Sub

    Private Sub TSBRemoveLocations_Click(sender As Object, e As EventArgs) Handles TSBRemoveLocations.Click
        Dim a = (From i In _LocationLVItems Where i.LVItem.Selected).FirstOrDefault

        If _LocationLVItems.Contains(a) Then
            Dim selected = LVLocations.SelectedIndices(0)
            _LocationLVItems.Remove(a)

            LVLocations.BeginUpdate()

            RefreshLocations()
            UpdateLocationPhotosLists()

            Dim LVItemCount As Integer = LVLocations.Items.Count - 1
            LVLocations.SelectedIndices.Clear()

            If selected <= LVItemCount Then
                LVLocations.Items(selected).Selected = True
                LVLocations.EnsureVisible(selected)
            Else
                LVLocations.Items(LVItemCount).Selected = True
                LVLocations.EnsureVisible(LVItemCount)
            End If

            LVLocations.EndUpdate()
        End If

    End Sub

End Class

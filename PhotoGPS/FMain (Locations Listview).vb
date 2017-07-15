#If DEBUG Then
Partial Public Class __KillDesigner
    'this is to prevent the file from opening as a blank form when double-clicked
End Class
#End If

'Locations listview methods
Partial Class FMain
    Private _LocationLVItems As New List(Of LVItem(Of Location))
    Private _LocationsOverlay As New WindowsForms.GMapOverlay("Locations")

    Private _AllFilesItem As New ListViewItem("(All Files)")

    Private Sub LVLocations_KeyDown(sender As Object, e As KeyEventArgs) Handles LVLocations.KeyDown
        If e.Control And (e.KeyCode = Keys.A) Then '<CTRL> + A
            SelectAllListviewItems(LVLocations, True)
        End If
    End Sub

    Private Sub TSBImportLocations_Click(sender As Object, e As EventArgs) Handles TSBImportLocations.Click
        'load loactions from csv
        Dim Locations As List(Of Location)

        WaitWindow.WaitForIt.DoIt("Importing Locations",
            Sub(pb)
                Locations = CSVSerializer.CSVDeserializer(Of Location).Deserialize(Me, False, pb)

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

                                    Dim subitems() As String = {l.LocationName, If(l.Start, String.Empty), If(l.End, String.Empty), l.Address, If(l.Lat, String.Empty), If(l.Long, String.Empty), l.ID, "Undefined"}
                                    Dim lv As New ListViewItem(subitems)

                                    Dim item = New LVItem(Of Location) With {
                                                        .Item = l,
                                                        .LVItem = lv,
                                                        .Marker = If(l.GPS.HasValue, New WindowsForms.Markers.GMarkerGoogle(l.GPS, WindowsForms.Markers.GMarkerGoogleType.red), Nothing)}

                                    item.LVItem.Checked = True
                                    _LocationLVItems.Add(item)
                                Next

                                'restore whatever previous visibility the overlay had
                                _LocationsOverlay.IsVisibile = visibility

                                RefreshLocations(pb)

                                UpdateLocationPhotosLists()

                            Catch ex As WaitWindow.DoItCanceledException
                                'user clicked cancel re-initialize location lists.
                                LVLocations.Items.Clear()
                                _LocationLVItems.Clear()
                                _LocationsOverlay.Markers.Clear()

                            End Try
                        End Sub)
                End If

            End Sub, Me)
    End Sub

    Private Sub RefreshLocations(Optional pb As WaitWindow.PostBack = Nothing)

        Try
            _LocationsOverlay.Markers.Clear()

            UpdateListView(Of Location)(Me._LocationLVItems, LVLocations,
                True, Function(i)
                          Return (Not Me.TSBLocationDateFilter.FilterEnabled) Or (i.Start <= Me.TSBLocationDateFilter.FilterEnd And i.End >= Me.TSBLocationDateFilter.FilterStart)
                      End Function, pb)

            LVLocations.Items.Insert(0, _AllFilesItem)
            LVLocations.SelectedItems.Clear()
            _AllFilesItem.Selected = True
            AutosizeColumns(LVLocations)

        Catch ex As WaitWindow.DoItCanceledException
            _LocationsOverlay.Markers.Clear()
            LVLocations.Items.Clear()

            Throw
        End Try

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

        WaitWindow.WaitForIt.DoIt("Retreiving GPS coordinates for selected locations",
        Sub(pb)
            Try
                Dim a As List(Of LVItem(Of Location)) = Nothing

                Me.Invoke(Sub() a = (From i In _LocationLVItems Where (Me.LVLocations.Items.Contains(i.LVItem)) And (i.LVItem.Selected = True) And (i.Item.GPS.HasValue = False) And (String.IsNullOrWhiteSpace(i.Item.Address) = False)).ToList)

                a.ForEach(
                Sub(i)
#If DEBUG Then
                    i = i 'for some reason, a lack of this line causes a compiler warning that i.Item and i.LVItem are being used before it was assigned.
#End If
                    pb.Invoke("Getting coordinates for " & i.Item.Address, a.IndexOf(i) / a.Count)

                    Dim status As GeoCoderStatusCode
                    i.Item.GPS = MapProviders.GMapProviders.GoogleMap.GetPoint(i.Item.Address, status)

                    If i.Item.GPS.HasValue Then
                        Me.Invoke(
                        Sub()
                            i.LVItem.SubItems(4).Text = i.Item.Lat.Value.ToString("0.000000")
                            i.LVItem.SubItems(5).Text = i.Item.[Long].Value.ToString("0.000000")
                        End Sub)
                    Else
                        Stop
                    End If

                End Sub)

            Catch ex As WaitWindow.DoItCanceledException
                'cancel was clicked by the user
            Finally
                Me.Invoke(Sub() AutosizeColumns(LVLocations))
            End Try

        End Sub, Me)

    End Sub

    Private Sub LVLocations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LVLocations.SelectedIndexChanged
        UpdatePhotosListview()
    End Sub


End Class



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
        RefreshLocations()

        'NOTE: The CurrentFolder property is set in .Shown
    End Sub

    ''' <summary>
    ''' Updates a <see cref="ListView"/> control to contain items from a <see cref="List(Of LVItem)"/>, optionally allowing filtering.
    ''' </summary>
    ''' <typeparam name="T">The type of the base object being displayed.</typeparam>
    ''' <param name="ItemList">The list of the <see cref="LVItem(Of t)"/> being displayed.</param>
    ''' <param name="ListView">The <see cref="ListView"/> control in which to display the list.</param>
    ''' <param name="ClearListView">If <c>True</c>, the <see cref="ListView"/> control will be cleared before populating the items and if the user cancels the action. If <c>False</c>, the <see cref="ListView"/> control will notbe cleared before populating the items and if the user cancels the action.</param>
    ''' <param name="FilterFunction">A <see cref="Predicate(Of T)"/> which returns <c>True</c> if the item is to be displayed in the list or <c>False</c> if not. If this parameter is <c>null</c>, all items are displayed.</param>
    ''' <param name="PostbackFunction">A <see cref="WaitWindow.PostBack"/> to be called to indicate progress to the user and to allow cancellation.</param>
    Private Shared Sub UpdateListView(Of T)(ItemList As IEnumerable(Of LVItem(Of T)), ListView As ListView, ClearListView As Boolean, FilterFunction As Predicate(Of T), Optional PostbackFunction As WaitWindow.PostBack = Nothing)


        Dim lvitems As List(Of ListViewItem) = Nothing

        If ItemList IsNot Nothing Then

            If FilterFunction IsNot Nothing Then
                'apply filtering
                lvitems = (From i In ItemList Where FilterFunction(i.Item) = True Select i.LVItem).ToList
            Else
                'don't apply filtering, show all items
                lvitems = (From i In ItemList Select i.LVItem).ToList
            End If
        End If

        If ClearListView Then ListView.Items.Clear()

        Dim a = Sub()
                    'this is less than optimal. as a future optimization, maybe use addrange with batches of lvitems.Count/100 (doing it as a series of batches would allow us to still use the waitwindow to indicate progress and have it be cancelable.)

                    If lvitems IsNot Nothing Then

                        Try
                            ListView.BeginUpdate()

                            Dim e = lvitems.Count - 1

                            For i = 0 To e
                                ListView.Items.Add(lvitems(i))
                                If PostbackFunction IsNot Nothing Then PostbackFunction.Invoke("Updating list.", i / e)
                            Next

                        Catch ex As WaitWindow.DoItCanceledException
                            If ClearListView Then ListView.Items.Clear()
                            Throw
                        Finally
                            ListView.EndUpdate()
                        End Try
                    End If
                End Sub

        If ListView.InvokeRequired Then
            ListView.Invoke(a)
        Else
            a()
        End If



    End Sub
    ''' <summary>
    ''' This structure is used to correlate a <see cref="ListViewItem"/>, a <see cref="WindowsForms.GMapMarker"/>, and an object of type <c>T</c>. To be used with a <see cref="List(Of T)"/> and Linq queries.
    ''' </summary>
    ''' <typeparam name="t">The type of object to correlate with the <see cref="ListViewItem"/> and <see cref="WindowsForms.GMapMarker"/></typeparam>
    Private Structure LVItem(Of t)
        Public Item As t
        Public LVItem As ListViewItem
        Public Marker As WindowsForms.GMapMarker
    End Structure

    ''' <summary>
    ''' Resize all of the columns in a <see cref="ListView"/> to fit the current content.
    ''' </summary>
    ''' <param name="lv">The <see cref="ListView"/> to resize the columns of.</param>
    Private Sub AutosizeColumns(lv As ListView)
        'resize columns to fit
        For Each i As ColumnHeader In lv.Columns
            i.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
        Next
    End Sub

    ''' <summary>
    ''' Updates the <see cref="Location.Photos"/> property of all elements in <see cref="_LocationLVItems"/> which are also visible in <see cref="LVLocations"/>.
    ''' </summary>
    Private Sub UpdateLocationPhotosLists()

        If (_LocationLVItems.Count > 0) And (_PhotoLVItems.Count > 0) Then
            Dim locations = (From i In _LocationLVItems Where (i.Item.GPS.HasValue) And (Me.LVLocations.Items.Contains(i.LVItem))).ToList

            For Each currentLoc In locations
                Dim tmp = (From i In _PhotoLVItems Where currentLoc.Item.ComparePhoto(i.Item) = True Select i.Item).ToList

                If tmp.Count > 0 Then
                    currentLoc.Item.Photos = tmp

                    tmp.ForEach(
                        Sub(i)
                            i.Locations.Add(currentLoc.Item)
                        End Sub)
                End If

                If currentLoc.LVItem.ListView.InvokeRequired Then
                    currentLoc.LVItem.ListView.Invoke(Sub() UpdateLocationLVItem(currentLoc.Item, currentLoc.LVItem))
                Else
                    UpdateLocationLVItem(currentLoc.Item, currentLoc.LVItem)
                End If

            Next

        End If

        Dim b = Sub()
                    Me._AllFilesItem.SubItems(7).Text = _PhotoLVItems.Count
                    Me._UnassociatedFilesItem.SubItems(7).Text = (From i In _PhotoLVItems Where i.Item.LocationCount = 0).Count
                    Me._PhotosWithMultipleLocations.SubItems(7).Text = (From i In _PhotoLVItems Where i.Item.LocationCount > 1).Count
                End Sub

        If LVLocations.InvokeRequired Then
            LVLocations.Invoke(b)
        Else
            b()
        End If


    End Sub

End Class


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
    ''' Selects or deselects all items in a <see cref="ListView"/> control.
    ''' </summary>
    ''' <param name="lv">The <see cref="ListView"/> control in which to select or deslect all items.</param>
    ''' <param name="selectionState">If <c>True</c>, all items will be selected. If <c>False</c>, all items will be deslected.</param>
    ''' <remarks>This function will create a <c>WaitWindow</c> using the method <see cref="WaitWindow.WaitForIt.DoIt(String, Action(Of WaitWindow.PostBack), Form, WaitWindow.PostBack)"/>. <c>Cancel</c> results in a partial selection.</remarks>
    Private Shared Sub SelectAllListviewItems(lv As ListView, selectionState As Boolean)
        'There's no method nor a Windows message you can send to a Listview to select all items so we'll have to iterate through the items one at a time and select them. This takes time so we'll use a wait window if it's more than 1000 items.

        'Populate .Items to a List(Of ListViewItem) then iterate through that seems to be much quicker for some reason.
        Dim lvitems = (From i As ListViewItem In lv.Items).ToList

        Dim SelectionFunction =
            Sub(pb As WaitWindow.PostBack)
                Try
                    lv.BeginUpdate()

                    'Not sure why, but this is SLOW
                    'For Each i In LVLocations.Items
                    '    i.Selected = True
                    '    pb("Selecting all.", (LVLocations.Items.IndexOf(i) / (LVLocations.Items.Count)))
                    'Next

                    For Each i In lvitems
                        i.Selected = selectionState
                        If pb IsNot Nothing Then pb("Selecting all.", (lvitems.IndexOf(i) / (lvitems.Count)))
                    Next

                Catch ex As WaitWindow.DoItCanceledException
                    'User clicked cancel. In this case, we'll leave the user a partial selection
                Finally
                    lv.EndUpdate()
                End Try
            End Sub

        If lvitems.Count > 1000 Then
            'asynchronously with a progress bar and a Cancel button
            WaitWindow.WaitForIt.DoIt("User Interface", Sub(pb) lv.Invoke(Sub() SelectionFunction(pb)), lv.FindForm)
        Else
            'syncronously, locks up UI
            SelectionFunction(Nothing)
        End If


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
    ''' <param name="pb"></param>
    Private Sub UpdateLocationPhotosLists()

        If (_LocationLVItems.Count > 0) And (_PhotoLVItems.Count > 0) Then
                Dim locations = (From i In _LocationLVItems Where (i.Item.GPS.HasValue) And (Me.LVLocations.Items.Contains(i.LVItem))).ToList

                For Each currentLoc In locations
                    Dim tmp = (From i In _PhotoLVItems Where currentLoc.Item.ComparePhoto(i.Item) = True Select i.Item).ToList

                    If tmp.Count > 0 Then
                        currentLoc.Item.Photos = tmp

                        tmp.ForEach(
                        Sub(i)
                            If i.Locations Is Nothing Then
                                i.Locations = New List(Of Location)
                            End If

                            i.Locations.Add(currentLoc.Item)
                        End Sub)
                    End If


                If currentLoc.LVItem.ListView.InvokeRequired Then
                    currentLoc.LVItem.ListView.Invoke(Sub() currentLoc.LVItem.SubItems(7).Text = currentLoc.Item.PhotoCount)
                Else
                    currentLoc.LVItem.SubItems(7).Text = currentLoc.Item.PhotoCount
                End If

            Next

            End If

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs)
        'DEBUG: Starts debugger.
        'loc has the currently highlighted location (or the first if multiple selections)
        'photosdistances has the ALL of the photos with their distance to loc along with the result of loc.ComparePhoto

        Dim loc = (From i In _LocationLVItems Where i.LVItem.Selected Select i.Item).First

        Dim photosdistances = From i In _PhotoLVItems Select New With {.Photo = i, .Dist = i.Item.GPS.DistanceTo(loc.GPS.Value, DistanceToUnits.Miles), .CompareTo = loc.ComparePhoto(i.Item)}


        Stop
    End Sub


End Class




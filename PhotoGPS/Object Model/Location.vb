Public Class Location


    Public Project As Project
    <CSVField(CSVFieldName:="Latitude")> Public Lat As Double?
    <CSVField(CSVFieldName:="Longitude")> Public [Long] As Double?
    <CSVField()> Public Start As Date?
    <CSVField()> Public [End] As Date?
    <CSVField("Name")> Public LocationName As String
    <CSVField()> Public Address As String
    <CSVField()> Public ID As String

    Public Property GPS As GMap.NET.PointLatLng?
        Get
            If Lat.HasValue And [Long].HasValue Then
                Return New GMap.NET.PointLatLng(Lat.Value, [Long].Value)
            Else
                Return New GMap.NET.PointLatLng?
            End If
        End Get
        Set(value As GMap.NET.PointLatLng?)
            If value.HasValue Then
                Lat = value.Value.Lat
                [Long] = value.Value.Lng
            Else
                Lat = New Double?
                [Long] = New Double?
            End If

        End Set
    End Property

    <CSVField(CSVFieldName:="Photo Count", Readable:=False, Writeable:=True)> Public ReadOnly Property PhotoCount As Integer
        Get
            Return Photos.Count
        End Get
    End Property

    Private _Photos As List(Of Photo)
    Public Property Photos As List(Of Photo) 'we use a list instead of an iEnumerable as, while this will be initially set by a query, the user may change the contents at any time
        Get
            If _Photos Is Nothing Then _Photos = PhotosFromLocation().ToList

            Return _Photos
        End Get
        Set(value As List(Of Photo))
            _Photos = value
        End Set
    End Property

    Public Function PhotosFromLocation() As IEnumerable(Of Photo)
        Return From i In Project.Photos Where ComparePhoto(i)
    End Function

    Public Function ComparePhoto(Photo As Photo) As Boolean
        If GPS.HasValue Then
            Dim datesMatch As Boolean = False

            If HasDates Then
                'a location can start and end on the same day. Since a photo can occur at any time between the start and end of any day, we need to make sure to look between the beginning of Start and the end of End. Best way to do that is to make the range of Start and End 24-hours.
                Dim realEnd As Date = [End].Value.AddDays(1)

                If (Photo.TakenDate >= Start) And (Photo.TakenDate <= realEnd) Then
                    datesMatch = True
                Else
                    datesMatch = False
                End If
            Else
                datesMatch = True 'all photos match a location with no dates so long as the GPS DOES match
            End If

            Dim gpsMatch = (GPS.Value.DistanceTo(Photo.GPS, DistanceToUnits.Miles) < 0.5)
            Return datesMatch And gpsMatch
        Else
            Return False 'no photos match a location with no GPS
        End If
    End Function

    Public ReadOnly Property HasDates As Boolean
        Get
            Return (Start.HasValue And [End].HasValue)
        End Get
    End Property

    Private lvi As ListViewItem
    Public ReadOnly Property ListViewItem As ListViewItem
        Get
            If lvi Is Nothing Then
                lvi = New ListViewItem
                UpdateListViewItem()
            End If

            Return lvi
        End Get
    End Property

    Public Sub UpdateListViewItem()
        lvi.SubItems.Clear()
        lvi.Text = Me.LocationName

        lvi.SubItems.AddRange({New ListViewItem.ListViewSubItem(lvi, If(Me.Start.HasValue, Me.Start.Value.ToShortDateString, String.Empty)),
                              New ListViewItem.ListViewSubItem(lvi, If(Me.End.HasValue, Me.End.Value.ToShortDateString, String.Empty)),
                              New ListViewItem.ListViewSubItem(lvi, Me.Address),
                              New ListViewItem.ListViewSubItem(lvi, If(Me.Lat.HasValue, Me.Lat.Value.ToString("0.000000"), String.Empty)),
                              New ListViewItem.ListViewSubItem(lvi, If(Me.Long.HasValue, Me.Long.Value.ToString("0.000000"), String.Empty)),
                              New ListViewItem.ListViewSubItem(lvi, Me.ID),
                              New ListViewItem.ListViewSubItem(lvi, Me.PhotoCount.ToString)})
    End Sub

End Class
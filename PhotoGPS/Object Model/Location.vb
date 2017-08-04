Public Class Location
    Public Property GPS As GMap.NET.PointLatLng?
        Get
            If Lat.HasValue And [Long].HasValue Then
                Return New GMap.NET.PointLatLng(Lat, [Long])
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

    <CSVField(CSVFieldName:="Latitude")> Public Lat As Double?
    <CSVField(CSVFieldName:="Longitude")> Public [Long] As Double?
    <CSVField()> Public Start As Date?
    <CSVField()> Public [End] As Date?
    <CSVField("Name")> Public LocationName As String
    <CSVField()> Public Address As String
    <CSVField()> Public ID As String

    <CSVField(CSVFieldName:="Photo Count", Readable:=False, Writeable:=True)> Public ReadOnly Property PhotoCount As Integer
        Get
            Return If(Photos IsNot Nothing, Photos.Count, 0)
        End Get
    End Property

    Public Photos As List(Of Photo)

    Public Function ComparePhoto(Photo As Photo) As Boolean

        If GPS.HasValue Then
            Dim datesMatch As Boolean = False

            If HasDates Then
                'a location can start and end on the same day. Since a photo can occur at any time between the start and end of any day, we need to make sure to look between the begining of Start and the end of End. Best way to do that is to make the range of Start and End 24- hours.
                Dim realEnd As Date = [End].Value.AddDays(1)

                If (Photo.TakenDate >= Start) And (Photo.TakenDate <= realEnd) Then
                    datesMatch = True
                Else
                    datesMatch = False
                End If
            Else
                datesMatch = True 'all photos match a location with no dates so long as the gps DOES match
            End If

            Dim gpsMatch = (GPS.Value.DistanceTo(Photo.GPS, DistanceToUnits.Miles) < 0.5)
            Return datesMatch And gpsMatch
        Else
            Return False 'no photos match a location with no gps
        End If


    End Function

    Public ReadOnly Property HasDates As Boolean
        Get
            Return (Start.HasValue And [End].HasValue)
        End Get
    End Property

End Class
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

Public Class Photo
    Public Shared ReadOnly SupportedExtensions() As String = {"*.jpg", "*.jpeg", "*.jpe"} 'Best practices is to use a constant but you can't have a constant array...

    Public Property GPS As GMap.NET.PointLatLng
        Get
            Return New GMap.NET.PointLatLng(Lat, [Long])
        End Get
        Set(value As GMap.NET.PointLatLng)
            Lat = value.Lat
            [Long] = value.Lng
        End Set
    End Property

    Shared Function FromFile(File As IO.FileInfo) As Photo

        Select Case File.Extension.ToUpper
            Case ".JPG", ".JPEG", ".JPE"
                'pull metadata
                'instanciate Photo
                'return it

                Dim res As Photo = Nothing

                Try
                    Using reader = New ExifLib.ExifReader(File.FullName)
                        Dim latitudeDMS As Double() = {0, 0}
                        Dim longitudeDMS As Double() = {0, 0}
                        Dim latitudeRef = String.Empty
                        Dim longitudeRef = String.Empty
                        Dim takenDate As New Date


                        'reader.GetTagValue returns False if the tag is missing, True otherwise. We want to use a Nullable(Of ...) value to avoid photos without a GPS tag from showing up at 0, 0 on the map.
                        Dim HasGPS =
                            reader.GetTagValue(ExifTags.GPSLatitude, latitudeDMS) And
                            reader.GetTagValue(ExifTags.GPSLongitude, longitudeDMS) And
                            reader.GetTagValue(ExifTags.GPSLatitudeRef, latitudeRef) And
                            reader.GetTagValue(ExifTags.GPSLongitudeRef, longitudeRef)

                        Dim HasTakenDate = reader.GetTagValue(Of Date)(ExifLib.ExifTags.DateTimeDigitized, takenDate)

                        res = New Photo With {
                            .Filedate = File.LastWriteTime,
                            .FileSize = File.Length,
                            .Filename = File.FullName,
                            .Lat = If(HasGPS, If(latitudeRef = "N", 1, -1) * (latitudeDMS(0) + (latitudeDMS(1) / 60) + (latitudeDMS(2) / 3600)), New Double?),
                            .Long = If(HasGPS, If(longitudeRef = "E", 1, -1) * (longitudeDMS(0) + (longitudeDMS(1) / 60) + (longitudeDMS(2) / 3600)), New Double?)}

                        res.TakenDate = If(HasTakenDate, takenDate, New Date?)

                        If (HasGPS = False) Or (HasTakenDate = False) Then
                            'I want only photos that have gps and a taken date. If either are missing, don't do anything with the image.
                            Return Nothing
                        Else
                            Return res
                        End If

                    End Using

                Catch ex As ExifLibException
                    'if exiflib is unable to read the file, we consider the file as not supported.
                    Return Nothing
                End Try

            Case Else
                '"MOV" and "MP4" are not supported yet. GoPro has not made any information public about how gps data is stored in their files.
                Return Nothing
        End Select

    End Function

    <CSVField(CSVFieldName:="Latitude")> Public Lat As Double
    <CSVField(CSVFieldName:="Longitude")> Public [Long] As Double
    <CSVField()> Public Filename As String
    <CSVField()> Public Filedate As Date
    <CSVField()> Public TakenDate As Date
    <CSVField()> Public FileSize As ULong

    Public ReadOnly Property LocationCount As Integer
        Get
            If Locations IsNot Nothing Then
                Return Locations.Count
            Else
                Return 0
            End If
        End Get
    End Property

    Public Locations As List(Of Location)

End Class
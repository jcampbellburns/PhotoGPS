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

    ''' <summary>
    ''' Populate instance fields with metadata from a file.
    ''' </summary>
    ''' <param name="File">The <see cref="IO.FileInfo"/> from which to populate the <see cref="Photo"/> fields.</param>
    ''' <returns>On success, returns a reference to this instance. On failure, returns <c>Nothing</c>.</returns>
    ''' <remarks>This method is called by the static <see cref="photo.FromFile(IO.FileInfo)"/> method when creating a new <see cref="Photo"/> instance. It is also called when renaming or moving the file to update an existing instance.</remarks>
    Private Function RefreshMetadata(File As IO.FileInfo) As Photo
        Select Case File.Extension.ToUpper
            Case ".JPG", ".JPEG", ".JPE"
                'pull metadata
                'instanciate Photo
                'return it

                'Dim res As Photo = Nothing

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

                        With Me
                            .Filedate = File.LastWriteTime
                            .FileSize = File.Length
                            .Filename = File.FullName

                            If HasGPS Then
                                .Lat = If(latitudeRef = "N", 1, -1) * (latitudeDMS(0) + (latitudeDMS(1) / 60) + (latitudeDMS(2) / 3600))
                                .Long = If(longitudeRef = "E", 1, -1) * (longitudeDMS(0) + (longitudeDMS(1) / 60) + (longitudeDMS(2) / 3600))
                            End If

                            If HasTakenDate Then
                                .TakenDate = takenDate
                            End If

                        End With

                        If (HasGPS = False) Or (HasTakenDate = False) Then
                            'I want only photos that have gps and a taken date. If either are missing, don't do anything with the image.
                            Return Nothing
                        Else
                            Return Me
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

    ''' <summary>
    ''' Generate a <see cref="Photo"/> from a photo file.
    ''' </summary>
    ''' <param name="File">The <see cref="IO.FileInfo"/> from which to create the <see cref="Photo"/>.</param>
    ''' <returns>A <see cref="Photo"/> representing the file speciifed by <paramref name="File"/> or <c>Nothing</c> if the file metadate could not be read.</returns>
    ''' <remarks>This method currently uses the file's extension to differentiate file types. Supported extension are .JPG, .JPEG, and .JPE. While there are plans to include support for other formats, such as MP4, support is only available for JPEG photos at this time.</remarks>
    Shared Function FromFile(File As IO.FileInfo) As Photo
        Dim res As New Photo

        Return res.RefreshMetadata(File)
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

    Public Locations As New List(Of Location)

    ''' <summary>
    ''' Renames the file this <see cref="Photo"/> is associated with and updates the Creation and LastWrite dates to match <see cref="Photo.TakenDate"/>.
    ''' </summary>
    ''' <remarks>This method renames the file using the format "[Taken Date and Time] GPS=[Lat and Long].[Extension]. If this filename is already in use, _# is appended to the end of the name before the extension, replacing # with the lowest number which is not already in use, starting at 2.</remarks>
    Public Sub RenameFile()
        Dim a As New IO.FileInfo(Filename)
        Dim folder = a.DirectoryName
        Dim ext = a.Extension

        Filename = a.MoveTo(String.Format("{0}\{1} GPS={2}, {3}{4}", folder, Me.TakenDate.ToString("yyyy-MM-dd HH.mm.ss"), Me.Lat.ToString("0.000000"), Me.Long.ToString("0.000000"), ext), False)

        IO.File.SetCreationTime(Filename, TakenDate)
        IO.File.SetLastWriteTime(Filename, TakenDate)

        Me.RefreshMetadata(a)
    End Sub

    Public Sub MoveToFolder(Folder As String)
        Dim a As New IO.FileInfo(Filename)
        Dim fn = a.Name

        a.MoveTo(IO.Path.Combine(Folder, fn), True)

        Me.RefreshMetadata(a)
    End Sub

End Class
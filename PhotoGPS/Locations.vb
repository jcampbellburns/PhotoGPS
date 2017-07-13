
Friend Class FRMNewLocation
    Inherits Form
    Private WithEvents TextBox1 As AddressTextBox
    Private WithEvents DateTimePicker1 As DateTimePicker
    Private WithEvents DateTimePicker2 As DateTimePicker
    Private WithEvents TextBox3 As TextBox
    Private WithEvents TextBox4 As TextBox
    Private WithEvents Button2 As Button
    Private WithEvents TextBox2 As TextBox

    Sub New()
        InitializeComponent()
    End Sub

    Private _value As Location
    Public Property Value As Location
        Get
            Return _value
        End Get
        Set(value As Location)
            _value = value
        End Set
    End Property

    Private Sub InitializeComponent()
        Dim Button1 As System.Windows.Forms.Button
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New PhotoGPS.FRMNewLocation.AddressTextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Button1 = New System.Windows.Forms.Button()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button1
        '
        Button1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Button1.Location = New System.Drawing.Point(322, 178)
        Button1.Name = "Button1"
        Button1.Size = New System.Drawing.Size(75, 23)
        Button1.TabIndex = 9
        Button1.Text = "Cancel"
        Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(12, 48)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(48, 13)
        Label1.TabIndex = 2
        Label1.Text = "Address:"
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(12, 87)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(58, 13)
        Label2.TabIndex = 4
        Label2.Text = "Start Date:"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(166, 87)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(55, 13)
        Label3.TabIndex = 6
        Label3.Text = "End Date:"
        '
        'Label4
        '
        Label4.AutoSize = True
        Label4.Location = New System.Drawing.Point(12, 9)
        Label4.Name = "Label4"
        Label4.Size = New System.Drawing.Size(82, 13)
        Label4.TabIndex = 0
        Label4.Text = "Location Name:"
        '
        'Label5
        '
        Label5.AutoSize = True
        Label5.Location = New System.Drawing.Point(12, 126)
        Label5.Name = "Label5"
        Label5.Size = New System.Drawing.Size(48, 13)
        Label5.TabIndex = 10
        Label5.Text = "Latitude:"
        '
        'Label6
        '
        Label6.AutoSize = True
        Label6.Location = New System.Drawing.Point(166, 126)
        Label6.Name = "Label6"
        Label6.Size = New System.Drawing.Size(57, 13)
        Label6.TabIndex = 11
        Label6.Text = "Longitude:"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button2.Location = New System.Drawing.Point(241, 178)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "OK"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(15, 64)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(382, 20)
        Me.TextBox1.TabIndex = 3
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(15, 103)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(148, 20)
        Me.DateTimePicker1.TabIndex = 5
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(169, 103)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(148, 20)
        Me.DateTimePicker2.TabIndex = 7
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(15, 25)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(382, 20)
        Me.TextBox2.TabIndex = 1
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(15, 143)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(148, 20)
        Me.TextBox3.TabIndex = 12
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(169, 143)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(148, 20)
        Me.TextBox4.TabIndex = 13
        '
        'FRMNewLocation
        '
        Me.AcceptButton = Button1
        Me.CancelButton = Button1
        Me.ClientSize = New System.Drawing.Size(409, 213)
        Me.ControlBox = False
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Label6)
        Me.Controls.Add(Label5)
        Me.Controls.Add(Label4)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Label2)
        Me.Controls.Add(Label3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FRMNewLocation"
        Me.Text = "Input Location"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Class AddressTextBox
        Inherits TextBox

        Protected Overrides Sub WndProc(ByRef m As Message)
            'convert multi-line clipboard into single-line, separated by ", "
            'includes support for any combination of CR and LF
            Const WM_PASTE As Integer = &H302

            If m.Msg = WM_PASTE And Clipboard.ContainsText() Then
                Dim a = Clipboard.GetText.Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)

                Dim ret = String.Empty

                a.ToList.ForEach(Sub(i) ret &= i & If(i = a.Last, String.Empty, ", "))

                Me.SelectedText = ret

            Else
                MyBase.WndProc(m)
            End If
        End Sub
    End Class

    Private Sub TextBox3_Validating(sender As Object, e As CancelEventArgs) Handles TextBox3.Validating
        'check if the value can be parsed as a double
        Dim value = 0#
        If Double.TryParse(TextBox3.Text, value) Then
            'valid range is -90 to +90

            If (value >= -90) And (value <= 90) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub TextBox4_Validating(sender As Object, e As CancelEventArgs) Handles TextBox4.Validating
        'check if the value can be parsed as a double
        Dim value = 0#
        If Double.TryParse(TextBox4.Text, value) Then
            'valid range is -180 to +180

            If (value >= -180) And (value <= 180) Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub FRMNewLocation_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If Value IsNot Nothing Then
            Button2.Text = "Update"
        End If
    End Sub
End Class

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

    <CSVField()> Public Lat As Double?
    <CSVField()> Public [Long] As Double?
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
    Public Property GPS As GMap.NET.PointLatLng
        Get
            Return New GMap.NET.PointLatLng(Lat, [Long])
        End Get
        Set(value As GMap.NET.PointLatLng)
            Lat = value.Lat
            [Long] = value.Lng
        End Set
    End Property

    Public Property Path As String
        Get
            Return _path
        End Get
        Set(value As String)
            _path = value
        End Set
    End Property

    Public ReadOnly Property FilenameWithPath As String 'we don't serialize the full path since I want the user to be able to move the cache file between folders if needed.
        Get
            Return _path & "\" & Filename
        End Get
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
                            .Filename = File.Name,
                            .Path = File.Directory.FullName,
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

    Private _path As String

    <CSVField()> Public Lat As Double
    <CSVField()> Public [Long] As Double
    <CSVField()> Public Filename As String
    <CSVField()> Public Filedate As Date
    <CSVField()> Public TakenDate As Date
    <CSVField()> Public FileSize As ULong

    Public ReadOnly Property LocationCount As Integer
        Get
            Return Locations.Count
        End Get
    End Property

    Public Locations As List(Of Location)

End Class
Friend Class FLocationEditor

    Sub New()
        InitializeComponent()
    End Sub

    Private _updateRequired As Boolean
    Public ReadOnly Property UpdateRequired As Boolean
        Get
            Return _updateRequired
        End Get
    End Property

    Private _addressChanged As Boolean


    'Private Sub tLat_Validating(sender As Object, e As CancelEventArgs) Handles tLat.Validating
    '    'check if the value can be parsed as a double
    '    Dim value = 0#
    '    If Double.TryParse(tLat.Text, value) Then
    '        'valid range is -90 to +90

    '        If (value >= -90) And (value <= 90) Then
    '            e.Cancel = False
    '        Else
    '            e.Cancel = True
    '        End If
    '    Else
    '        e.Cancel = True
    '    End If
    'End Sub

    'Private Sub tLon_Validating(sender As Object, e As CancelEventArgs) Handles tLon.Validating
    '    'check if the value can be parsed as a double
    '    Dim value = 0#
    '    If Double.TryParse(tLon.Text, value) Then
    '        'valid range is -180 to +180

    '        If (value >= -180) And (value <= 180) Then
    '            e.Cancel = False
    '        Else
    '            e.Cancel = True
    '        End If
    '    Else
    '        e.Cancel = True
    '    End If
    'End Sub

    Private Class AddressTextBox
        Inherits TextBox

        Protected Overrides Sub WndProc(ByRef m As Message)
            'convert multi-line clipboard into single-line, separated by ", "
            'includes support for any combination of CR and LF
            Const WM_PASTE As Integer = &H302

            If m.Msg = WM_PASTE Then
                If Clipboard.ContainsText() Then


                    Dim a = Clipboard.GetText.Split({vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)

                    Dim ret = String.Empty

                    a.ToList.ForEach(Sub(i) ret &= i & If(i = a.Last, String.Empty, ", "))

                    Me.SelectedText = ret
                End If
            Else
                MyBase.WndProc(m)
            End If
        End Sub
    End Class

    Public Shared Function EditLocation(ByRef Location As Location) As FLocationEditor
        Dim a As New FLocationEditor

        If Location Is Nothing Then
            a.bOK.Text = "Add"
            a.Text = "Add new location"
        Else
            a.bOK.Text = "Update"
            a.Text = "Edit location"

            With Location
                a.tName.Text = .LocationName
                a.tAddress.Text = .Address
                a.tLat.Text = If(.Lat, "")
                a.tLon.Text = If(.Long, "")
                If .HasDates Then
                    a.cHasDates.Checked = True
                    a.dStart.Value = .Start
                    a.dEnd.Value = .End
                Else
                    a.cHasDates.Checked = False
                End If
                a.tID.Text = .ID
                a._updateRequired = False
                a._addressChanged = False
            End With
        End If

        If a.ShowDialog() = DialogResult.OK Then
            If Location Is Nothing Then
                Location = New Location
            End If

            With Location
                If a._addressChanged Then
                    .Address = a.tAddress.Text.Trim
                    .GPS = Nothing
                End If
                .LocationName = a.tName.Text

                If a.cHasDates.Checked Then
                    .Start = a.dStart.Value
                    .End = a.dEnd.Value
                Else
                    .Start = Nothing
                    .End = Nothing
                End If

                .ID = a.tID.Text.Trim
            End With
        End If

        Return a
    End Function

    Private Sub cEditId_CheckedChanged(sender As Object, e As EventArgs) Handles cEditId.CheckedChanged
        tID.Enabled = cEditId.Checked
        lID.Enabled = cEditId.Checked
    End Sub

    Private Sub tName_TextChanged(sender As Object, e As EventArgs) Handles tName.TextChanged
        _updateRequired = True
    End Sub

    Private Sub tAddress_TextChanged(sender As Object, e As EventArgs) Handles tAddress.TextChanged
        _updateRequired = True
        _addressChanged = True
    End Sub

    Private Sub dStart_ValueChanged(sender As Object, e As EventArgs) Handles dStart.ValueChanged
        _updateRequired = True
    End Sub

    Private Sub dEnd_ValueChanged(sender As Object, e As EventArgs) Handles dEnd.ValueChanged
        _updateRequired = True
    End Sub

    Private Sub tID_TextChanged(sender As Object, e As EventArgs) Handles tID.TextChanged
        _updateRequired = True
    End Sub

    Private Sub cHasDates_CheckedChanged(sender As Object, e As EventArgs) Handles cHasDates.CheckedChanged
        dStart.Enabled = cHasDates.Checked
        dEnd.Enabled = cHasDates.Checked
        lStart.Enabled = cHasDates.Checked
        lEnd.Enabled = cHasDates.Checked

        _updateRequired = True
    End Sub
End Class
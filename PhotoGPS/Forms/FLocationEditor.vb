Friend Class FLocationEditor

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
            Button2.Text = "Save"
        End If
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


End Class
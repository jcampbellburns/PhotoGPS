Friend Class FWaitWindow
    Public CancelRequested As Boolean = False

    Public Action As Action



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CancelRequested = True


    End Sub

    Private Sub FWaitWindow_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim a As New Task(Action)

        a.Start()
    End Sub


End Class
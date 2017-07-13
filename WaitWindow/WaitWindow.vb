''' <summary>
''' Updates the WaitWindow message and progress. Checks if the user has clicked the Cancel button.
''' </summary>
''' <param name="Message">The text to be displayed on the WaitWindow.</param>
''' <param name="Progress">The percentage to be displayed on the progress bar or a negative value to show a marquee instead. Value must be between 0.0 and 1.0. To convert a index in a count, use the formula <c>(Index / Count)</c>.</param>
''' <remarks>Always call this method from within a Try...Catch block. A <see cref="DoItCanceledException"/> is thrown when this method is called after the user has clicked the Cancel button.</remarks>
Public Delegate Sub PostBack(Message As String, Progress As Double)

''' <summary>
''' Contains a static method for running a processor intensive thread without locking up the application.
''' </summary>
Public NotInheritable Class WaitForIt
    Private Sub New()
    End Sub

    Private Shared f As New FWaitWindow

    ''' <summary>
    ''' Display a progress window and execute a processor intensive task which the user can cancel.
    ''' </summary>
    ''' <param name="Title">The title of the task being run. This is displayed to the user.</param>
    ''' <param name="Task">An <see cref="Action(Of PostBack)"/> containing the task to be run. A <see cref="PostBack"/> delegate function is passed to <c>Task</c> which is used to indicate status and allow the user to cancel the task. Once the user has clicked the <c>Cancel</c> button, any subsequent calls to <see cref="PostBack"/> will throw a <see cref="DoItCanceledException"/> within <c>Task</c>.</param>
    ''' <param name="OwnerForm">Optional. The form which has ownership of the progress window.</param>
    ''' <!--<param name="CallBack">Called after Task has completed and after the window has closed.</param>-->
    ''' <exception cref="DoItCanceledException">Thrown within the task on a call to the <see cref="PostBack"/> function if the user has clicked the <c>Cancel</c> button.</exception>
    Public Shared Sub DoIt(Title As String, Task As Action(Of PostBack), Optional OwnerForm As System.Windows.Forms.Form = Nothing, Optional OverridePostBack As PostBack = Nothing) ', Optional CallBack As Action(Of WaitForIt) = Nothing)
        Dim n As Date = Date.Now
        f.CancelRequested = False

        Dim _postback As PostBack

        _postback = If(OverridePostBack,
           Sub(Message As String, Progress As Double)
               If f.Visible Then
                   If (Progress < 0) Or (Date.Now > n.AddSeconds(0.1)) Then
                       n = Date.Now

                       f.Invoke(
                           Sub()
                               'ensures thread-safe operations
                               f.Label1.Text = Message
                               f.Label1.Refresh()

                               Windows.Forms.Application.DoEvents()

                               If Progress > -1 Then
                                   f.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Blocks
                                   f.ProgressBar1.Value = Progress * 100

                                   f.ProgressBar1.Refresh()
                               Else
                                   f.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee

                               End If

                               f.Update()
                           End Sub)
                   End If

                   If f.CancelRequested Then
                       f.Invoke(Sub() f.Hide())
                       Throw New WaitWindow.DoItCanceledException
                   End If

               End If

           End Sub)

        Dim cb =
            Sub()
                If OverridePostBack Is Nothing Then
                    If f.Visible Then

                        f.Invoke(Sub() f.Hide())
                    End If
                End If
            End Sub

        If OverridePostBack Is Nothing Then
            f.Action =
            Sub()
                Task.BeginInvoke(_postback, cb, Nothing)
            End Sub

            f.Text = "Processing: " & Title
            f.ShowDialog()
        End If

    End Sub
End Class

''' <summary>
''' Thrown by the function <see cref="PostBack"/> within the function specified as the <c>Task</c> parameter of <see cref="WaitForIt.DoIt(String, Action(Of PostBack), Windows.Forms.Form, PostBack)"/> when the user has clicked the <c>Cancel</c> button on the progress form.
''' </summary>
Public Class DoItCanceledException
    Inherits System.Exception

    Sub New()
        MyBase.New("The user cancelled the operation.")
    End Sub
End Class



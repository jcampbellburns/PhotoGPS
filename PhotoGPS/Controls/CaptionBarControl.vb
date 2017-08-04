
Public Class CaptionBarControl
    Inherits Label

    Private WithEvents _trackFocus As Control
    Private _autoSize As Boolean = True

    Sub New()
        MyBase.AutoSize = False
        AutoEllipsis = True
        BackColor = SystemColors.InactiveCaption
        ForeColor = SystemColors.InactiveCaptionText

    End Sub

    ''' <summary>
    ''' Which control this CaptionBarControl indicates focus for.
    ''' </summary>
    ''' <returns>The control which is having its focus tracked.</returns>
    ''' <remarks>If this is set to Nothing, this value will be changed to parent control of this CaptionBarControl when the parent control is changed.</remarks>
    Public Property TrackFocus As Control
        Get
            Return _trackFocus
        End Get
        Set(value As Control)
            _trackFocus = value
        End Set
    End Property

    Public Overrides Property AutoSize As Boolean
        'default Label functionality of AutoSize includes both Width and Height. We are overriding this to work with Docking. See the GetPreferredSize override.
        Get
            Return _autoSize
        End Get
        Set(value As Boolean)
            _autoSize = value
        End Set
    End Property

    Public Overrides Function GetPreferredSize(proposedSize As Size) As Size
        Dim res As Size = proposedSize

        If _autoSize Then
            If Parent IsNot Nothing Then 'this prevents NullReferenceExceptions in the designer
                res = TextRenderer.MeasureText(Me.Text, Font, proposedSize, TextFormatFlags.SingleLine Or TextFormatFlags.NoClipping)
                Select Case Dock
                    Case DockStyle.Bottom, DockStyle.Top, DockStyle.Fill
                        res.Width = Parent.Width
                    Case DockStyle.Left, DockStyle.Right, DockStyle.Fill
                        res.Height = Parent.Height
                End Select
            End If
        End If
        Return res
    End Function

    Protected Overrides Sub OnParentChanged(e As EventArgs)
        MyBase.OnParentChanged(e)

        If _trackFocus Is Nothing Then _trackFocus = Me.Parent
    End Sub

    Private Sub _trackFocus_Enter(sender As Object, e As EventArgs) Handles _trackFocus.Enter
        Me.BackColor = SystemColors.ActiveCaption
        Me.ForeColor = SystemColors.ActiveCaptionText
    End Sub

    Private Sub _trackFocus_Leave(sender As Object, e As EventArgs) Handles _trackFocus.Leave
        Me.BackColor = SystemColors.InactiveCaption
        Me.ForeColor = SystemColors.InactiveCaptionText
    End Sub
End Class

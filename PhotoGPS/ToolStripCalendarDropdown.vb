Public Class ToolStripCalendarDropdown
    Inherits ToolStripDropDownButton

    Private WithEvents _cal As MonthCalendar
    Private WithEvents _filterCB As CheckBox

    Public Property FilterEnabled As Boolean
        Get
            Return _filterCB.Checked
        End Get
        Set(value As Boolean)
            _filterCB.Checked = value
        End Set
    End Property

    Public ReadOnly Property FilterStart As Date
        Get
            Return _cal.SelectionStart.Date 'we use .Date as the Date structure includes time which will make integral comparison more difficult. The Start is assumed to be at Midnight
        End Get
        'Set(value As Date)
        '    _cal.SelectionStart = value.Date
        'End Set
    End Property

    Public ReadOnly Property FilterEnd As Date
        Get
            Return _cal.SelectionEnd.AddDays(1).Date 'this date represents the end of a date range. The range is from the beginning of FilterStart to the end of FilterEnd. This is so that if only one day is selected, for example, the difference is one day.
        End Get
        'Set(value As Date)
        '    _cal.SelectionEnd = value.Date.AddDays(-1).Date
        'End Set
    End Property

    Sub New()
        Dim DropDown = New ToolStripDropDown

        _cal = New MonthCalendar With {.CalendarDimensions = New Size(1, 1), .Padding = New Padding(10, 0, 10, 0), .MaxSelectionCount = 90}
        _filterCB = New CheckBox With {.Text = "Filter enabled?", .Padding = New Padding(10, 0, 10, 0)}

        Dim TSCHCal = New ToolStripControlHost(_cal)
        Dim TSCHFilterCB = New ToolStripControlHost(_filterCB)

        TSCHCal.AutoSize = False
        TSCHFilterCB.AutoSize = False

        DropDown.Items.Add(TSCHFilterCB)
        DropDown.Items.Add(TSCHCal)

        Me.DropDown = DropDown

        AddHandler Me.DropDownOpened, Sub()
                                          _filterCB.Width = _cal.PreferredSize.Width
                                      End Sub

        Dim e = Sub()
                    _cal.Enabled = _filterCB.Checked
                    _FilterChanged = True

                End Sub

        AddHandler _filterCB.CheckedChanged, e
        AddHandler _cal.DateChanged, e
        AddHandler DropDown.Closed, Sub()
                                        If _FilterChanged Then
                                            OnFilterUpdated()
                                            _FilterChanged = False
                                        End If
                                    End Sub
    End Sub

    Private _FilterChanged = False

    Public Event FilterUpdated(sender As Object, e As EventArgs)

    Protected Overridable Sub OnFilterUpdated()
        RaiseEvent FilterUpdated(Me, New EventArgs)
    End Sub

End Class
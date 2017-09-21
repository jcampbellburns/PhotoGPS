Partial Friend Class FLocationEditor
    Inherits System.Windows.Forms.Form


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim Label1 As System.Windows.Forms.Label
        Dim Label4 As System.Windows.Forms.Label
        Dim Label5 As System.Windows.Forms.Label
        Dim Label6 As System.Windows.Forms.Label
        Me.lStart = New System.Windows.Forms.Label()
        Me.lEnd = New System.Windows.Forms.Label()
        Me.lID = New System.Windows.Forms.Label()
        Me.bCancel = New System.Windows.Forms.Button()
        Me.bOK = New System.Windows.Forms.Button()
        Me.tAddress = New PhotoGPS.FLocationEditor.AddressTextBox()
        Me.dStart = New System.Windows.Forms.DateTimePicker()
        Me.tName = New System.Windows.Forms.TextBox()
        Me.tLat = New System.Windows.Forms.TextBox()
        Me.tLon = New System.Windows.Forms.TextBox()
        Me.dEnd = New System.Windows.Forms.DateTimePicker()
        Me.tID = New System.Windows.Forms.TextBox()
        Me.cEditId = New System.Windows.Forms.CheckBox()
        Me.cHasDates = New System.Windows.Forms.CheckBox()
        Label1 = New System.Windows.Forms.Label()
        Label4 = New System.Windows.Forms.Label()
        Label5 = New System.Windows.Forms.Label()
        Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
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
        'lStart
        '
        Me.lStart.AutoSize = True
        Me.lStart.Location = New System.Drawing.Point(12, 87)
        Me.lStart.Name = "lStart"
        Me.lStart.Size = New System.Drawing.Size(58, 13)
        Me.lStart.TabIndex = 4
        Me.lStart.Text = "Start Date:"
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
        'lEnd
        '
        Me.lEnd.AutoSize = True
        Me.lEnd.Location = New System.Drawing.Point(166, 87)
        Me.lEnd.Name = "lEnd"
        Me.lEnd.Size = New System.Drawing.Size(55, 13)
        Me.lEnd.TabIndex = 6
        Me.lEnd.Text = "End Date:"
        '
        'lID
        '
        Me.lID.AutoSize = True
        Me.lID.Enabled = False
        Me.lID.Location = New System.Drawing.Point(12, 166)
        Me.lID.Name = "lID"
        Me.lID.Size = New System.Drawing.Size(21, 13)
        Me.lID.TabIndex = 14
        Me.lID.Text = "ID:"
        '
        'bCancel
        '
        Me.bCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.bCancel.Location = New System.Drawing.Point(322, 183)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(75, 23)
        Me.bCancel.TabIndex = 9
        Me.bCancel.Text = "Cancel"
        Me.bCancel.UseVisualStyleBackColor = True
        '
        'bOK
        '
        Me.bOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.bOK.Location = New System.Drawing.Point(241, 183)
        Me.bOK.Name = "bOK"
        Me.bOK.Size = New System.Drawing.Size(75, 23)
        Me.bOK.TabIndex = 8
        Me.bOK.Text = "OK"
        Me.bOK.UseVisualStyleBackColor = True
        '
        'tAddress
        '
        Me.tAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tAddress.Location = New System.Drawing.Point(15, 64)
        Me.tAddress.Name = "tAddress"
        Me.tAddress.Size = New System.Drawing.Size(382, 20)
        Me.tAddress.TabIndex = 3
        '
        'dStart
        '
        Me.dStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dStart.Location = New System.Drawing.Point(15, 103)
        Me.dStart.Name = "dStart"
        Me.dStart.Size = New System.Drawing.Size(148, 20)
        Me.dStart.TabIndex = 5
        '
        'tName
        '
        Me.tName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tName.Location = New System.Drawing.Point(15, 25)
        Me.tName.Name = "tName"
        Me.tName.Size = New System.Drawing.Size(382, 20)
        Me.tName.TabIndex = 1
        '
        'tLat
        '
        Me.tLat.Location = New System.Drawing.Point(15, 143)
        Me.tLat.Name = "tLat"
        Me.tLat.ReadOnly = True
        Me.tLat.Size = New System.Drawing.Size(148, 20)
        Me.tLat.TabIndex = 12
        '
        'tLon
        '
        Me.tLon.Location = New System.Drawing.Point(169, 143)
        Me.tLon.Name = "tLon"
        Me.tLon.ReadOnly = True
        Me.tLon.Size = New System.Drawing.Size(148, 20)
        Me.tLon.TabIndex = 13
        '
        'dEnd
        '
        Me.dEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dEnd.Location = New System.Drawing.Point(169, 103)
        Me.dEnd.Name = "dEnd"
        Me.dEnd.Size = New System.Drawing.Size(148, 20)
        Me.dEnd.TabIndex = 7
        '
        'tID
        '
        Me.tID.Enabled = False
        Me.tID.Location = New System.Drawing.Point(15, 183)
        Me.tID.Name = "tID"
        Me.tID.Size = New System.Drawing.Size(148, 20)
        Me.tID.TabIndex = 15
        '
        'cEditId
        '
        Me.cEditId.AutoSize = True
        Me.cEditId.Location = New System.Drawing.Point(169, 183)
        Me.cEditId.Name = "cEditId"
        Me.cEditId.Size = New System.Drawing.Size(58, 17)
        Me.cEditId.TabIndex = 16
        Me.cEditId.Text = "Edit ID"
        Me.cEditId.UseVisualStyleBackColor = True
        '
        'cHasDates
        '
        Me.cHasDates.AutoSize = True
        Me.cHasDates.Checked = True
        Me.cHasDates.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cHasDates.Location = New System.Drawing.Point(322, 103)
        Me.cHasDates.Name = "cHasDates"
        Me.cHasDates.Size = New System.Drawing.Size(82, 17)
        Me.cHasDates.TabIndex = 17
        Me.cHasDates.Text = "Has Dates?"
        Me.cHasDates.UseVisualStyleBackColor = True
        '
        'FLocationEditor
        '
        Me.AcceptButton = Me.bOK
        Me.CancelButton = Me.bCancel
        Me.ClientSize = New System.Drawing.Size(411, 218)
        Me.ControlBox = False
        Me.Controls.Add(Me.cHasDates)
        Me.Controls.Add(Me.cEditId)
        Me.Controls.Add(Me.tID)
        Me.Controls.Add(Me.lID)
        Me.Controls.Add(Me.tLon)
        Me.Controls.Add(Me.tLat)
        Me.Controls.Add(Label6)
        Me.Controls.Add(Label5)
        Me.Controls.Add(Label4)
        Me.Controls.Add(Label1)
        Me.Controls.Add(Me.lStart)
        Me.Controls.Add(Me.lEnd)
        Me.Controls.Add(Me.tName)
        Me.Controls.Add(Me.tAddress)
        Me.Controls.Add(Me.dStart)
        Me.Controls.Add(Me.dEnd)
        Me.Controls.Add(Me.bOK)
        Me.Controls.Add(Me.bCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FLocationEditor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents tAddress As AddressTextBox
    Private WithEvents dStart As DateTimePicker
    Private WithEvents tLat As TextBox
    Private WithEvents tLon As TextBox
    Private WithEvents bOK As Button
    Private WithEvents tName As TextBox
    Private WithEvents dEnd As DateTimePicker
    Private WithEvents tID As TextBox
    Friend WithEvents cEditId As CheckBox
    Friend WithEvents bCancel As Button
    Friend WithEvents cHasDates As CheckBox
    Friend WithEvents lID As Label
    Friend WithEvents lStart As Label
    Friend WithEvents lEnd As Label
End Class

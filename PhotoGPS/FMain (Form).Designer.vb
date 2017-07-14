<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FMain
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim MENUMain As System.Windows.Forms.MenuStrip
        Dim FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Dim TestToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Dim PANELMap As System.Windows.Forms.Panel
        Dim TSAddress As System.Windows.Forms.ToolStrip
        Dim ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
        Dim ToolStripspringTextBox1 As PhotoGPS.ToolStripSpringTextBox
        Dim ToolStripButton8 As System.Windows.Forms.ToolStripButton
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FMain))
        Dim ToolStripButton9 As System.Windows.Forms.ToolStripButton
        Dim TSCoords As System.Windows.Forms.ToolStrip
        Dim ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
        Dim ToolStripspringTextBox2 As PhotoGPS.ToolStripSpringTextBox
        Dim ToolStripButton10 As System.Windows.Forms.ToolStripButton
        Dim TSMapTools As System.Windows.Forms.ToolStrip
        Dim ToolStripButton6 As System.Windows.Forms.ToolStripButton
        Dim ToolStripButton7 As System.Windows.Forms.ToolStripButton
        Dim CAPTIONMap As PhotoGPS.CaptionBarControl
        Dim SPLITTERMain As System.Windows.Forms.Splitter
        Dim PANELgps As System.Windows.Forms.Panel
        Dim ColumnHeader1 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader2 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader3 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader4 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader5 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader6 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader7 As System.Windows.Forms.ColumnHeader
        Dim ToolStrip2 As System.Windows.Forms.ToolStrip
        Dim ToolStripButton4 As System.Windows.Forms.ToolStripButton
        Dim ToolStripButton13 As System.Windows.Forms.ToolStripButton
        Dim ToolStripButton12 As System.Windows.Forms.ToolStripButton
        Dim ColumnHeader8 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader9 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader10 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader11 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader12 As System.Windows.Forms.ColumnHeader
        Dim ToolStrip6 As System.Windows.Forms.ToolStrip
        Dim CAPTIONgps As PhotoGPS.CaptionBarControl
        Dim TSMainTools As System.Windows.Forms.ToolStrip
        Dim ToolStripButton1 As System.Windows.Forms.ToolStripButton
        Dim ToolStripButton2 As System.Windows.Forms.ToolStripButton
        Dim STATUSMain As System.Windows.Forms.StatusStrip
        Dim ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
        Dim FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
        Me.MAP = New GMap.NET.WindowsForms.GMapControl()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LVLocations = New System.Windows.Forms.ListView()
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TSBImportLocations = New System.Windows.Forms.ToolStripButton()
        Me.TSBExportLocations = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBLocationsVisible = New System.Windows.Forms.ToolStripButton()
        Me.TSBGetLocationCoords = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBLocationDateFilter = New PhotoGPS.ToolStripCalendarDropdown()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LVPhotos = New System.Windows.Forms.ListView()
        Me.ILFolder = New System.Windows.Forms.ImageList(Me.components)
        Me.TSBParentFolder = New System.Windows.Forms.ToolStripButton()
        Me.TBFolder = New PhotoGPS.ToolStripSpringTextBox()
        Me.TSBBrowseForFolder = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripButton()
        MENUMain = New System.Windows.Forms.MenuStrip()
        FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        PANELMap = New System.Windows.Forms.Panel()
        TSAddress = New System.Windows.Forms.ToolStrip()
        ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        ToolStripspringTextBox1 = New PhotoGPS.ToolStripSpringTextBox()
        ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        TSCoords = New System.Windows.Forms.ToolStrip()
        ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        ToolStripspringTextBox2 = New PhotoGPS.ToolStripSpringTextBox()
        ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        TSMapTools = New System.Windows.Forms.ToolStrip()
        ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        CAPTIONMap = New PhotoGPS.CaptionBarControl()
        SPLITTERMain = New System.Windows.Forms.Splitter()
        PANELgps = New System.Windows.Forms.Panel()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ToolStrip2 = New System.Windows.Forms.ToolStrip()
        ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton13 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton12 = New System.Windows.Forms.ToolStripButton()
        ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ToolStrip6 = New System.Windows.Forms.ToolStrip()
        CAPTIONgps = New PhotoGPS.CaptionBarControl()
        TSMainTools = New System.Windows.Forms.ToolStrip()
        ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        STATUSMain = New System.Windows.Forms.StatusStrip()
        ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        MENUMain.SuspendLayout()
        PANELMap.SuspendLayout()
        TSAddress.SuspendLayout()
        TSCoords.SuspendLayout()
        TSMapTools.SuspendLayout()
        PANELgps.SuspendLayout()
        Me.Panel1.SuspendLayout()
        ToolStrip2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        ToolStrip6.SuspendLayout()
        TSMainTools.SuspendLayout()
        STATUSMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MENUMain
        '
        MENUMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {FileToolStripMenuItem})
        MENUMain.Location = New System.Drawing.Point(0, 0)
        MENUMain.Name = "MENUMain"
        MENUMain.Size = New System.Drawing.Size(1019, 24)
        MENUMain.TabIndex = 3
        MENUMain.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {TestToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        FileToolStripMenuItem.Text = "&File"
        '
        'TestToolStripMenuItem
        '
        TestToolStripMenuItem.Name = "TestToolStripMenuItem"
        TestToolStripMenuItem.Size = New System.Drawing.Size(93, 22)
        TestToolStripMenuItem.Text = "test"
        '
        'PANELMap
        '
        PANELMap.Controls.Add(Me.MAP)
        PANELMap.Controls.Add(TSAddress)
        PANELMap.Controls.Add(TSCoords)
        PANELMap.Controls.Add(TSMapTools)
        PANELMap.Controls.Add(CAPTIONMap)
        PANELMap.Dock = System.Windows.Forms.DockStyle.Fill
        PANELMap.Location = New System.Drawing.Point(742, 49)
        PANELMap.Name = "PANELMap"
        PANELMap.Size = New System.Drawing.Size(277, 516)
        PANELMap.TabIndex = 5
        '
        'MAP
        '
        Me.MAP.Bearing = 0!
        Me.MAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MAP.CanDragMap = True
        Me.MAP.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MAP.EmptyTileColor = System.Drawing.Color.Navy
        Me.MAP.GrayScaleMode = False
        Me.MAP.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow
        Me.MAP.LevelsKeepInMemmory = 5
        Me.MAP.Location = New System.Drawing.Point(0, 38)
        Me.MAP.MarkersEnabled = True
        Me.MAP.MaxZoom = 2
        Me.MAP.MinZoom = 2
        Me.MAP.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter
        Me.MAP.Name = "MAP"
        Me.MAP.NegativeMode = False
        Me.MAP.PolygonsEnabled = True
        Me.MAP.RetryLoadTile = 0
        Me.MAP.RoutesEnabled = True
        Me.MAP.ScaleMode = GMap.NET.WindowsForms.ScaleModes.[Integer]
        Me.MAP.SelectedAreaFillColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.MAP.ShowTileGridLines = False
        Me.MAP.Size = New System.Drawing.Size(277, 428)
        Me.MAP.TabIndex = 5
        Me.MAP.TabStop = False
        Me.MAP.Zoom = 0R
        '
        'TSAddress
        '
        TSAddress.Dock = System.Windows.Forms.DockStyle.Bottom
        TSAddress.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        TSAddress.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripLabel1, ToolStripspringTextBox1, ToolStripButton8, ToolStripButton9})
        TSAddress.Location = New System.Drawing.Point(0, 466)
        TSAddress.Name = "TSAddress"
        TSAddress.Size = New System.Drawing.Size(277, 25)
        TSAddress.Stretch = True
        TSAddress.TabIndex = 4
        TSAddress.Text = "ToolStrip5"
        '
        'ToolStripLabel1
        '
        ToolStripLabel1.Name = "ToolStripLabel1"
        ToolStripLabel1.Size = New System.Drawing.Size(92, 22)
        ToolStripLabel1.Text = "Address/Search:"
        '
        'ToolStripspringTextBox1
        '
        ToolStripspringTextBox1.AcceptsReturn = True
        ToolStripspringTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        ToolStripspringTextBox1.Name = "ToolStripspringTextBox1"
        ToolStripspringTextBox1.Size = New System.Drawing.Size(105, 25)
        '
        'ToolStripButton8
        '
        ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton8.Name = "ToolStripButton8"
        ToolStripButton8.Size = New System.Drawing.Size(23, 22)
        ToolStripButton8.Text = "ToolStripButton8"
        ToolStripButton8.ToolTipText = "Get address from current position"
        '
        'ToolStripButton9
        '
        ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton9.Image = CType(resources.GetObject("ToolStripButton9.Image"), System.Drawing.Image)
        ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton9.Name = "ToolStripButton9"
        ToolStripButton9.Size = New System.Drawing.Size(23, 22)
        ToolStripButton9.Text = "ToolStripButton9"
        ToolStripButton9.ToolTipText = "Open address in Google maps"
        '
        'TSCoords
        '
        TSCoords.Dock = System.Windows.Forms.DockStyle.Bottom
        TSCoords.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        TSCoords.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripLabel2, ToolStripspringTextBox2, ToolStripButton10})
        TSCoords.Location = New System.Drawing.Point(0, 491)
        TSCoords.Name = "TSCoords"
        TSCoords.Size = New System.Drawing.Size(277, 25)
        TSCoords.Stretch = True
        TSCoords.TabIndex = 3
        TSCoords.Text = "ToolStrip4"
        '
        'ToolStripLabel2
        '
        ToolStripLabel2.Name = "ToolStripLabel2"
        ToolStripLabel2.Size = New System.Drawing.Size(117, 22)
        ToolStripLabel2.Text = "Current Coordinates:"
        '
        'ToolStripspringTextBox2
        '
        ToolStripspringTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        ToolStripspringTextBox2.Name = "ToolStripspringTextBox2"
        ToolStripspringTextBox2.Size = New System.Drawing.Size(103, 25)
        '
        'ToolStripButton10
        '
        ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton10.Image = CType(resources.GetObject("ToolStripButton10.Image"), System.Drawing.Image)
        ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton10.Name = "ToolStripButton10"
        ToolStripButton10.Size = New System.Drawing.Size(23, 22)
        ToolStripButton10.Text = "ToolStripButton10"
        ToolStripButton10.ToolTipText = "Open coordinates in Google maps"
        '
        'TSMapTools
        '
        TSMapTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        TSMapTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripButton6, ToolStripButton7})
        TSMapTools.Location = New System.Drawing.Point(0, 13)
        TSMapTools.Name = "TSMapTools"
        TSMapTools.Size = New System.Drawing.Size(277, 25)
        TSMapTools.TabIndex = 1
        TSMapTools.Text = "ToolStrip3"
        '
        'ToolStripButton6
        '
        ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton6.Name = "ToolStripButton6"
        ToolStripButton6.Size = New System.Drawing.Size(23, 22)
        ToolStripButton6.Text = "ToolStripButton4"
        '
        'ToolStripButton7
        '
        ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
        ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton7.Name = "ToolStripButton7"
        ToolStripButton7.Size = New System.Drawing.Size(23, 22)
        ToolStripButton7.Text = "ToolStripButton5"
        '
        'CAPTIONMap
        '
        CAPTIONMap.AutoEllipsis = True
        CAPTIONMap.AutoSize = True
        CAPTIONMap.BackColor = System.Drawing.SystemColors.InactiveCaption
        CAPTIONMap.Dock = System.Windows.Forms.DockStyle.Top
        CAPTIONMap.ForeColor = System.Drawing.SystemColors.InactiveCaptionText
        CAPTIONMap.Location = New System.Drawing.Point(0, 0)
        CAPTIONMap.Name = "CAPTIONMap"
        CAPTIONMap.Size = New System.Drawing.Size(277, 13)
        CAPTIONMap.TabIndex = 0
        CAPTIONMap.Text = "Map"
        CAPTIONMap.TextAlign = System.Drawing.ContentAlignment.TopCenter
        CAPTIONMap.TrackFocus = PANELMap
        '
        'SPLITTERMain
        '
        SPLITTERMain.Location = New System.Drawing.Point(739, 49)
        SPLITTERMain.Name = "SPLITTERMain"
        SPLITTERMain.Size = New System.Drawing.Size(3, 516)
        SPLITTERMain.TabIndex = 4
        SPLITTERMain.TabStop = False
        '
        'PANELgps
        '
        PANELgps.Controls.Add(Me.Splitter1)
        PANELgps.Controls.Add(Me.Panel1)
        PANELgps.Controls.Add(Me.Panel2)
        PANELgps.Controls.Add(ToolStrip6)
        PANELgps.Controls.Add(CAPTIONgps)
        PANELgps.Dock = System.Windows.Forms.DockStyle.Left
        PANELgps.Location = New System.Drawing.Point(0, 49)
        PANELgps.Name = "PANELgps"
        PANELgps.Size = New System.Drawing.Size(739, 516)
        PANELgps.TabIndex = 3
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(505, 38)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 478)
        Me.Splitter1.TabIndex = 5
        Me.Splitter1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.LVLocations)
        Me.Panel1.Controls.Add(ToolStrip2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(508, 478)
        Me.Panel1.TabIndex = 4
        '
        'LVLocations
        '
        Me.LVLocations.AllowColumnReorder = True
        Me.LVLocations.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1, ColumnHeader2, ColumnHeader3, ColumnHeader4, ColumnHeader5, ColumnHeader6, ColumnHeader7, Me.ColumnHeader13})
        Me.LVLocations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LVLocations.FullRowSelect = True
        Me.LVLocations.HideSelection = False
        Me.LVLocations.Location = New System.Drawing.Point(0, 25)
        Me.LVLocations.Name = "LVLocations"
        Me.LVLocations.ShowGroups = False
        Me.LVLocations.Size = New System.Drawing.Size(508, 453)
        Me.LVLocations.TabIndex = 4
        Me.LVLocations.UseCompatibleStateImageBehavior = False
        Me.LVLocations.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        ColumnHeader1.Text = "Name"
        '
        'ColumnHeader2
        '
        ColumnHeader2.Text = "Start"
        '
        'ColumnHeader3
        '
        ColumnHeader3.Text = "End"
        '
        'ColumnHeader4
        '
        ColumnHeader4.Text = "Address"
        '
        'ColumnHeader5
        '
        ColumnHeader5.Text = "Lat"
        '
        'ColumnHeader6
        '
        ColumnHeader6.Text = "Long"
        '
        'ColumnHeader7
        '
        ColumnHeader7.Text = "ID"
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Photo Count"
        '
        'ToolStrip2
        '
        ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBImportLocations, Me.TSBExportLocations, Me.ToolStripSeparator1, Me.TSBLocationsVisible, Me.TSBGetLocationCoords, ToolStripButton4, ToolStripButton13, ToolStripButton12, Me.ToolStripSeparator2, Me.TSBLocationDateFilter})
        ToolStrip2.Location = New System.Drawing.Point(0, 0)
        ToolStrip2.Name = "ToolStrip2"
        ToolStrip2.Size = New System.Drawing.Size(508, 25)
        ToolStrip2.TabIndex = 1
        ToolStrip2.Text = "ToolStrip2"
        '
        'TSBImportLocations
        '
        Me.TSBImportLocations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBImportLocations.Image = CType(resources.GetObject("TSBImportLocations.Image"), System.Drawing.Image)
        Me.TSBImportLocations.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBImportLocations.Name = "TSBImportLocations"
        Me.TSBImportLocations.Size = New System.Drawing.Size(23, 22)
        Me.TSBImportLocations.Text = "ToolStripButton4"
        Me.TSBImportLocations.ToolTipText = "Import (CSV)"
        '
        'TSBExportLocations
        '
        Me.TSBExportLocations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBExportLocations.Image = CType(resources.GetObject("TSBExportLocations.Image"), System.Drawing.Image)
        Me.TSBExportLocations.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBExportLocations.Name = "TSBExportLocations"
        Me.TSBExportLocations.Size = New System.Drawing.Size(23, 22)
        Me.TSBExportLocations.Text = "ToolStripButton14"
        Me.TSBExportLocations.ToolTipText = "Export (CSV)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TSBLocationsVisible
        '
        Me.TSBLocationsVisible.Checked = True
        Me.TSBLocationsVisible.CheckOnClick = True
        Me.TSBLocationsVisible.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSBLocationsVisible.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBLocationsVisible.Image = CType(resources.GetObject("TSBLocationsVisible.Image"), System.Drawing.Image)
        Me.TSBLocationsVisible.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBLocationsVisible.Name = "TSBLocationsVisible"
        Me.TSBLocationsVisible.Size = New System.Drawing.Size(23, 22)
        Me.TSBLocationsVisible.Text = "ToolStripButton4"
        Me.TSBLocationsVisible.ToolTipText = "Show/Hide location markers"
        '
        'TSBGetLocationCoords
        '
        Me.TSBGetLocationCoords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBGetLocationCoords.Image = CType(resources.GetObject("TSBGetLocationCoords.Image"), System.Drawing.Image)
        Me.TSBGetLocationCoords.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBGetLocationCoords.Name = "TSBGetLocationCoords"
        Me.TSBGetLocationCoords.Size = New System.Drawing.Size(23, 22)
        Me.TSBGetLocationCoords.Text = "ToolStripButton10"
        Me.TSBGetLocationCoords.ToolTipText = "Get GPS coordinates for all/selected locations which have an address but no GPS c" &
    "oordinates"
        '
        'ToolStripButton4
        '
        ToolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton4.Name = "ToolStripButton4"
        ToolStripButton4.Size = New System.Drawing.Size(23, 22)
        ToolStripButton4.Text = "ToolStripButton4"
        '
        'ToolStripButton13
        '
        ToolStripButton13.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton13.Image = CType(resources.GetObject("ToolStripButton13.Image"), System.Drawing.Image)
        ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton13.Name = "ToolStripButton13"
        ToolStripButton13.Size = New System.Drawing.Size(23, 22)
        ToolStripButton13.Text = "ToolStripButton5"
        '
        'ToolStripButton12
        '
        ToolStripButton12.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        ToolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton12.Image = CType(resources.GetObject("ToolStripButton12.Image"), System.Drawing.Image)
        ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton12.Name = "ToolStripButton12"
        ToolStripButton12.Size = New System.Drawing.Size(23, 22)
        ToolStripButton12.Text = "ToolStripButton11"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TSBLocationDateFilter
        '
        Me.TSBLocationDateFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBLocationDateFilter.FilterEnabled = False
        Me.TSBLocationDateFilter.Image = CType(resources.GetObject("TSBLocationDateFilter.Image"), System.Drawing.Image)
        Me.TSBLocationDateFilter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBLocationDateFilter.Name = "TSBLocationDateFilter"
        Me.TSBLocationDateFilter.Size = New System.Drawing.Size(29, 22)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.LVPhotos)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(508, 38)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(231, 478)
        Me.Panel2.TabIndex = 6
        '
        'LVPhotos
        '
        Me.LVPhotos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader8, ColumnHeader9, ColumnHeader10, ColumnHeader11, ColumnHeader12})
        Me.LVPhotos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LVPhotos.FullRowSelect = True
        Me.LVPhotos.Location = New System.Drawing.Point(0, 0)
        Me.LVPhotos.Name = "LVPhotos"
        Me.LVPhotos.Size = New System.Drawing.Size(231, 478)
        Me.LVPhotos.SmallImageList = Me.ILFolder
        Me.LVPhotos.TabIndex = 4
        Me.LVPhotos.UseCompatibleStateImageBehavior = False
        Me.LVPhotos.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader8
        '
        ColumnHeader8.Text = "Taken Date/Time"
        '
        'ColumnHeader9
        '
        ColumnHeader9.Text = "Latitude"
        '
        'ColumnHeader10
        '
        ColumnHeader10.Text = "Longitude"
        '
        'ColumnHeader11
        '
        ColumnHeader11.Text = "Filename"
        '
        'ColumnHeader12
        '
        ColumnHeader12.Text = "File date/time"
        '
        'ILFolder
        '
        Me.ILFolder.ImageStream = CType(resources.GetObject("ILFolder.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ILFolder.TransparentColor = System.Drawing.Color.Transparent
        Me.ILFolder.Images.SetKeyName(0, "VSO_Folder_16x.png")
        Me.ILFolder.Images.SetKeyName(1, "VSO_Document_16x.png")
        '
        'ToolStrip6
        '
        ToolStrip6.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        ToolStrip6.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBParentFolder, Me.TBFolder, Me.TSBBrowseForFolder})
        ToolStrip6.Location = New System.Drawing.Point(0, 13)
        ToolStrip6.Name = "ToolStrip6"
        ToolStrip6.Size = New System.Drawing.Size(739, 25)
        ToolStrip6.TabIndex = 3
        '
        'TSBParentFolder
        '
        Me.TSBParentFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBParentFolder.Image = CType(resources.GetObject("TSBParentFolder.Image"), System.Drawing.Image)
        Me.TSBParentFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBParentFolder.Name = "TSBParentFolder"
        Me.TSBParentFolder.Size = New System.Drawing.Size(23, 22)
        Me.TSBParentFolder.Text = "ToolStripButton4"
        '
        'TBFolder
        '
        Me.TBFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.TBFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories
        Me.TBFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TBFolder.Name = "TBFolder"
        Me.TBFolder.Size = New System.Drawing.Size(659, 25)
        '
        'TSBBrowseForFolder
        '
        Me.TSBBrowseForFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBBrowseForFolder.Image = CType(resources.GetObject("TSBBrowseForFolder.Image"), System.Drawing.Image)
        Me.TSBBrowseForFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBBrowseForFolder.Name = "TSBBrowseForFolder"
        Me.TSBBrowseForFolder.Size = New System.Drawing.Size(23, 22)
        Me.TSBBrowseForFolder.Text = "ToolStripButton12"
        '
        'CAPTIONgps
        '
        CAPTIONgps.AutoEllipsis = True
        CAPTIONgps.AutoSize = True
        CAPTIONgps.BackColor = System.Drawing.SystemColors.ActiveCaption
        CAPTIONgps.Dock = System.Windows.Forms.DockStyle.Top
        CAPTIONgps.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        CAPTIONgps.Location = New System.Drawing.Point(0, 0)
        CAPTIONgps.Name = "CAPTIONgps"
        CAPTIONgps.Size = New System.Drawing.Size(739, 13)
        CAPTIONgps.TabIndex = 0
        CAPTIONgps.Text = "GPS Data"
        CAPTIONgps.TextAlign = System.Drawing.ContentAlignment.TopCenter
        CAPTIONgps.TrackFocus = PANELgps
        '
        'TSMainTools
        '
        TSMainTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        TSMainTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripButton1, ToolStripButton2})
        TSMainTools.Location = New System.Drawing.Point(0, 24)
        TSMainTools.Name = "TSMainTools"
        TSMainTools.Size = New System.Drawing.Size(1019, 25)
        TSMainTools.TabIndex = 0
        '
        'ToolStripButton1
        '
        ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton1.Name = "ToolStripButton1"
        ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        ToolStripButton1.Text = "ToolStripButton1"
        '
        'ToolStripButton2
        '
        ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        ToolStripButton2.Text = "ToolStripButton2"
        AddHandler ToolStripButton2.Click, AddressOf Me.ToolStripButton2_Click
        '
        'STATUSMain
        '
        STATUSMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripStatusLabel1, Me.ToolStripProgressBar1, Me.ToolStripDropDownButton1})
        STATUSMain.Location = New System.Drawing.Point(0, 565)
        STATUSMain.Name = "STATUSMain"
        STATUSMain.Size = New System.Drawing.Size(1019, 22)
        STATUSMain.TabIndex = 5
        STATUSMain.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New System.Drawing.Size(39, 17)
        ToolStripStatusLabel1.Text = "Ready"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(23, 20)
        Me.ToolStripDropDownButton1.Text = "ToolStripDropDownButton1"
        '
        'FMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 587)
        Me.Controls.Add(PANELMap)
        Me.Controls.Add(SPLITTERMain)
        Me.Controls.Add(PANELgps)
        Me.Controls.Add(STATUSMain)
        Me.Controls.Add(TSMainTools)
        Me.Controls.Add(MENUMain)
        Me.MainMenuStrip = MENUMain
        Me.MinimumSize = New System.Drawing.Size(570, 340)
        Me.Name = "FMain"
        Me.Text = "PhotoGPS"
        MENUMain.ResumeLayout(False)
        MENUMain.PerformLayout()
        PANELMap.ResumeLayout(False)
        PANELMap.PerformLayout()
        TSAddress.ResumeLayout(False)
        TSAddress.PerformLayout()
        TSCoords.ResumeLayout(False)
        TSCoords.PerformLayout()
        TSMapTools.ResumeLayout(False)
        TSMapTools.PerformLayout()
        PANELgps.ResumeLayout(False)
        PANELgps.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        ToolStrip2.ResumeLayout(False)
        ToolStrip2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        ToolStrip6.ResumeLayout(False)
        ToolStrip6.PerformLayout()
        TSMainTools.ResumeLayout(False)
        TSMainTools.PerformLayout()
        STATUSMain.ResumeLayout(False)
        STATUSMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MAP As GMap.NET.WindowsForms.GMapControl
    Friend WithEvents ILFolder As ImageList
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TSBParentFolder As ToolStripButton
    Friend WithEvents TBFolder As ToolStripSpringTextBox
    Friend WithEvents TSBBrowseForFolder As ToolStripButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents TSBImportLocations As ToolStripButton
    Friend WithEvents TSBExportLocations As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TSBLocationsVisible As ToolStripButton
    Friend WithEvents TSBGetLocationCoords As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents TSBLocationDateFilter As ToolStripCalendarDropdown
    Friend WithEvents LVLocations As ListView
    Friend WithEvents LVPhotos As ListView
    Friend WithEvents ToolStripProgressBar1 As ToolStripProgressBar
    Friend WithEvents ToolStripDropDownButton1 As ToolStripButton
    Friend WithEvents ColumnHeader13 As ColumnHeader
End Class

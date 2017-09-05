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
        Dim ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
        Dim ToolStripspringTextBox1 As PhotoGPS.ToolStripSpringTextBox
        Dim ToolStripButton8 As System.Windows.Forms.ToolStripButton
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FMain))
        Dim ToolStripButton9 As System.Windows.Forms.ToolStripButton
        Dim ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
        Dim ToolStripspringTextBox2 As PhotoGPS.ToolStripSpringTextBox
        Dim ToolStripButton10 As System.Windows.Forms.ToolStripButton
        Dim ToolStripButton6 As System.Windows.Forms.ToolStripButton
        Dim ToolStripButton7 As System.Windows.Forms.ToolStripButton
        Dim CAPTIONMap As PhotoGPS.CaptionBarControl
        Dim SPLITTERMain As System.Windows.Forms.Splitter
        Dim PANELProject As System.Windows.Forms.Panel
        Dim ColumnHeader1 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader2 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader3 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader4 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader5 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader6 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader7 As System.Windows.Forms.ColumnHeader
        Dim CAPTIONLocation As PhotoGPS.CaptionBarControl
        Dim ColumnHeader8 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader9 As System.Windows.Forms.ColumnHeader
        Dim ColumnHeader10 As System.Windows.Forms.ColumnHeader
        Dim CAPTIONPhotos As PhotoGPS.CaptionBarControl
        Dim CAPTIONProject As PhotoGPS.CaptionBarControl
        Dim ToolStripButton1 As System.Windows.Forms.ToolStripButton
        Dim ToolStripButton2 As System.Windows.Forms.ToolStripButton
        Dim STATUSMain As System.Windows.Forms.StatusStrip
        Me.MAP = New GMap.NET.WindowsForms.GMapControl()
        Me.TSAddress = New System.Windows.Forms.ToolStrip()
        Me.TSCoords = New System.Windows.Forms.ToolStrip()
        Me.TSMap = New System.Windows.Forms.ToolStrip()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.PANELLocations = New System.Windows.Forms.Panel()
        Me.LVLocations = New System.Windows.Forms.ListView()
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TSLocations = New System.Windows.Forms.ToolStrip()
        Me.TSBImportLocations = New System.Windows.Forms.ToolStripButton()
        Me.TSBExportLocations = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBLocationsVisible = New System.Windows.Forms.ToolStripButton()
        Me.TSBGetLocationCoords = New System.Windows.Forms.ToolStripButton()
        Me.TSBEditLocation = New System.Windows.Forms.ToolStripButton()
        Me.TSBRemoveLocations = New System.Windows.Forms.ToolStripButton()
        Me.TSBAddLocation = New System.Windows.Forms.ToolStripButton()
        Me.PANELPhotos = New System.Windows.Forms.Panel()
        Me.LVPhotos = New System.Windows.Forms.ListView()
        Me.TSPhotos = New System.Windows.Forms.ToolStrip()
        Me.TSBRemovePhotos = New System.Windows.Forms.ToolStripSplitButton()
        Me.TSBRemoveSelectedPhotos = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSBRemovePhotosNoLongerAvailable = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBRemoveAllPhotos = New System.Windows.Forms.ToolStripMenuItem()
        Me.TBSAddPhotos = New System.Windows.Forms.ToolStripSplitButton()
        Me.TSBAddPhotosFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSBAddPhotosFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSBRenamePhotoFiles = New System.Windows.Forms.ToolStripButton()
        Me.TSBSortPhotos = New System.Windows.Forms.ToolStripSplitButton()
        Me.TSBSortPhotosByLocationName = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSBSortPhotosByTakenDate = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSBSortPhotosSameFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSProject = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton14 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SSLStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SSPTaskProgress = New System.Windows.Forms.ToolStripProgressBar()
        Me.SSBStop = New System.Windows.Forms.ToolStripButton()
        Me.TSMain = New System.Windows.Forms.ToolStrip()
        Me.ILFolder = New System.Windows.Forms.ImageList(Me.components)
        MENUMain = New System.Windows.Forms.MenuStrip()
        FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        TestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        PANELMap = New System.Windows.Forms.Panel()
        ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        ToolStripspringTextBox1 = New PhotoGPS.ToolStripSpringTextBox()
        ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        ToolStripspringTextBox2 = New PhotoGPS.ToolStripSpringTextBox()
        ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        CAPTIONMap = New PhotoGPS.CaptionBarControl()
        SPLITTERMain = New System.Windows.Forms.Splitter()
        PANELProject = New System.Windows.Forms.Panel()
        ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CAPTIONLocation = New PhotoGPS.CaptionBarControl()
        ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        CAPTIONPhotos = New PhotoGPS.CaptionBarControl()
        CAPTIONProject = New PhotoGPS.CaptionBarControl()
        ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        STATUSMain = New System.Windows.Forms.StatusStrip()
        MENUMain.SuspendLayout()
        PANELMap.SuspendLayout()
        Me.TSAddress.SuspendLayout()
        Me.TSCoords.SuspendLayout()
        Me.TSMap.SuspendLayout()
        PANELProject.SuspendLayout()
        Me.PANELLocations.SuspendLayout()
        Me.TSLocations.SuspendLayout()
        Me.PANELPhotos.SuspendLayout()
        Me.TSPhotos.SuspendLayout()
        Me.TSProject.SuspendLayout()
        STATUSMain.SuspendLayout()
        Me.TSMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MENUMain
        '
        MENUMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {FileToolStripMenuItem})
        MENUMain.Location = New System.Drawing.Point(0, 0)
        MENUMain.Name = "MENUMain"
        MENUMain.Size = New System.Drawing.Size(1402, 24)
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
        PANELMap.Controls.Add(Me.TSAddress)
        PANELMap.Controls.Add(Me.TSCoords)
        PANELMap.Controls.Add(Me.TSMap)
        PANELMap.Controls.Add(CAPTIONMap)
        PANELMap.Dock = System.Windows.Forms.DockStyle.Fill
        PANELMap.Location = New System.Drawing.Point(742, 49)
        PANELMap.Name = "PANELMap"
        PANELMap.Size = New System.Drawing.Size(660, 516)
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
        Me.MAP.Size = New System.Drawing.Size(660, 428)
        Me.MAP.TabIndex = 5
        Me.MAP.TabStop = False
        Me.MAP.Zoom = 0R
        '
        'TSAddress
        '
        Me.TSAddress.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TSAddress.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TSAddress.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripLabel1, ToolStripspringTextBox1, ToolStripButton8, ToolStripButton9})
        Me.TSAddress.Location = New System.Drawing.Point(0, 466)
        Me.TSAddress.Name = "TSAddress"
        Me.TSAddress.Size = New System.Drawing.Size(660, 25)
        Me.TSAddress.Stretch = True
        Me.TSAddress.TabIndex = 4
        Me.TSAddress.Text = "ToolStrip5"
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
        ToolStripspringTextBox1.Size = New System.Drawing.Size(488, 25)
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
        Me.TSCoords.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TSCoords.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TSCoords.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripLabel2, ToolStripspringTextBox2, ToolStripButton10})
        Me.TSCoords.Location = New System.Drawing.Point(0, 491)
        Me.TSCoords.Name = "TSCoords"
        Me.TSCoords.Size = New System.Drawing.Size(660, 25)
        Me.TSCoords.Stretch = True
        Me.TSCoords.TabIndex = 3
        Me.TSCoords.Text = "ToolStrip4"
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
        ToolStripspringTextBox2.Size = New System.Drawing.Size(486, 25)
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
        'TSMap
        '
        Me.TSMap.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TSMap.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripButton6, ToolStripButton7})
        Me.TSMap.Location = New System.Drawing.Point(0, 13)
        Me.TSMap.Name = "TSMap"
        Me.TSMap.Size = New System.Drawing.Size(660, 25)
        Me.TSMap.TabIndex = 1
        Me.TSMap.Text = "ToolStrip3"
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
        CAPTIONMap.Size = New System.Drawing.Size(660, 13)
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
        'PANELProject
        '
        PANELProject.Controls.Add(Me.Splitter1)
        PANELProject.Controls.Add(Me.PANELLocations)
        PANELProject.Controls.Add(Me.PANELPhotos)
        PANELProject.Controls.Add(Me.TSProject)
        PANELProject.Controls.Add(CAPTIONProject)
        PANELProject.Dock = System.Windows.Forms.DockStyle.Left
        PANELProject.Location = New System.Drawing.Point(0, 49)
        PANELProject.Name = "PANELProject"
        PANELProject.Size = New System.Drawing.Size(739, 516)
        PANELProject.TabIndex = 3
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Splitter1.Location = New System.Drawing.Point(421, 38)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 478)
        Me.Splitter1.TabIndex = 5
        Me.Splitter1.TabStop = False
        '
        'PANELLocations
        '
        Me.PANELLocations.Controls.Add(Me.LVLocations)
        Me.PANELLocations.Controls.Add(Me.TSLocations)
        Me.PANELLocations.Controls.Add(CAPTIONLocation)
        Me.PANELLocations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PANELLocations.Location = New System.Drawing.Point(0, 38)
        Me.PANELLocations.Name = "PANELLocations"
        Me.PANELLocations.Size = New System.Drawing.Size(424, 478)
        Me.PANELLocations.TabIndex = 4
        '
        'LVLocations
        '
        Me.LVLocations.AllowColumnReorder = True
        Me.LVLocations.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader1, ColumnHeader2, ColumnHeader3, ColumnHeader4, ColumnHeader5, ColumnHeader6, ColumnHeader7, Me.ColumnHeader13})
        Me.LVLocations.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LVLocations.FullRowSelect = True
        Me.LVLocations.HideSelection = False
        Me.LVLocations.Location = New System.Drawing.Point(0, 38)
        Me.LVLocations.Name = "LVLocations"
        Me.LVLocations.ShowGroups = False
        Me.LVLocations.Size = New System.Drawing.Size(424, 440)
        Me.LVLocations.TabIndex = 4
        Me.LVLocations.UseCompatibleStateImageBehavior = False
        Me.LVLocations.View = System.Windows.Forms.View.Details
        Me.LVLocations.VirtualMode = True
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
        'TSLocations
        '
        Me.TSLocations.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TSLocations.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBImportLocations, Me.TSBExportLocations, Me.ToolStripSeparator1, Me.TSBLocationsVisible, Me.TSBGetLocationCoords, Me.TSBEditLocation, Me.TSBRemoveLocations, Me.TSBAddLocation})
        Me.TSLocations.Location = New System.Drawing.Point(0, 13)
        Me.TSLocations.Name = "TSLocations"
        Me.TSLocations.Size = New System.Drawing.Size(424, 25)
        Me.TSLocations.TabIndex = 1
        Me.TSLocations.Text = "ToolStrip2"
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
        'TSBEditLocation
        '
        Me.TSBEditLocation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TSBEditLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBEditLocation.Image = CType(resources.GetObject("TSBEditLocation.Image"), System.Drawing.Image)
        Me.TSBEditLocation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBEditLocation.Name = "TSBEditLocation"
        Me.TSBEditLocation.Size = New System.Drawing.Size(23, 22)
        Me.TSBEditLocation.Text = "ToolStripButton4"
        '
        'TSBRemoveLocations
        '
        Me.TSBRemoveLocations.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TSBRemoveLocations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBRemoveLocations.Image = CType(resources.GetObject("TSBRemoveLocations.Image"), System.Drawing.Image)
        Me.TSBRemoveLocations.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBRemoveLocations.Name = "TSBRemoveLocations"
        Me.TSBRemoveLocations.Size = New System.Drawing.Size(23, 22)
        Me.TSBRemoveLocations.Text = "ToolStripButton5"
        '
        'TSBAddLocation
        '
        Me.TSBAddLocation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TSBAddLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBAddLocation.Image = CType(resources.GetObject("TSBAddLocation.Image"), System.Drawing.Image)
        Me.TSBAddLocation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBAddLocation.Name = "TSBAddLocation"
        Me.TSBAddLocation.Size = New System.Drawing.Size(23, 22)
        Me.TSBAddLocation.Text = "ToolStripButton11"
        '
        'CAPTIONLocation
        '
        CAPTIONLocation.AutoEllipsis = True
        CAPTIONLocation.AutoSize = True
        CAPTIONLocation.BackColor = System.Drawing.SystemColors.ActiveCaption
        CAPTIONLocation.Dock = System.Windows.Forms.DockStyle.Top
        CAPTIONLocation.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        CAPTIONLocation.Location = New System.Drawing.Point(0, 0)
        CAPTIONLocation.Name = "CAPTIONLocation"
        CAPTIONLocation.Size = New System.Drawing.Size(424, 13)
        CAPTIONLocation.TabIndex = 5
        CAPTIONLocation.Text = "Locations"
        CAPTIONLocation.TextAlign = System.Drawing.ContentAlignment.TopCenter
        CAPTIONLocation.TrackFocus = Me.PANELLocations
        '
        'PANELPhotos
        '
        Me.PANELPhotos.Controls.Add(Me.LVPhotos)
        Me.PANELPhotos.Controls.Add(Me.TSPhotos)
        Me.PANELPhotos.Controls.Add(CAPTIONPhotos)
        Me.PANELPhotos.Dock = System.Windows.Forms.DockStyle.Right
        Me.PANELPhotos.Location = New System.Drawing.Point(424, 38)
        Me.PANELPhotos.Name = "PANELPhotos"
        Me.PANELPhotos.Size = New System.Drawing.Size(315, 478)
        Me.PANELPhotos.TabIndex = 6
        '
        'LVPhotos
        '
        Me.LVPhotos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {ColumnHeader8, ColumnHeader9, ColumnHeader10})
        Me.LVPhotos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LVPhotos.FullRowSelect = True
        Me.LVPhotos.Location = New System.Drawing.Point(0, 38)
        Me.LVPhotos.Name = "LVPhotos"
        Me.LVPhotos.Size = New System.Drawing.Size(315, 440)
        Me.LVPhotos.TabIndex = 4
        Me.LVPhotos.UseCompatibleStateImageBehavior = False
        Me.LVPhotos.View = System.Windows.Forms.View.Details
        Me.LVPhotos.VirtualMode = True
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
        'TSPhotos
        '
        Me.TSPhotos.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TSPhotos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBRemovePhotos, Me.TBSAddPhotos, Me.TSBRenamePhotoFiles, Me.TSBSortPhotos})
        Me.TSPhotos.Location = New System.Drawing.Point(0, 13)
        Me.TSPhotos.Name = "TSPhotos"
        Me.TSPhotos.Size = New System.Drawing.Size(315, 25)
        Me.TSPhotos.TabIndex = 3
        '
        'TSBRemovePhotos
        '
        Me.TSBRemovePhotos.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TSBRemovePhotos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBRemovePhotos.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBRemoveSelectedPhotos, Me.TSBRemovePhotosNoLongerAvailable, Me.ToolStripSeparator3, Me.TSBRemoveAllPhotos})
        Me.TSBRemovePhotos.Image = CType(resources.GetObject("TSBRemovePhotos.Image"), System.Drawing.Image)
        Me.TSBRemovePhotos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBRemovePhotos.Name = "TSBRemovePhotos"
        Me.TSBRemovePhotos.Size = New System.Drawing.Size(32, 22)
        Me.TSBRemovePhotos.Text = "ToolStripButton5"
        '
        'TSBRemoveSelectedPhotos
        '
        Me.TSBRemoveSelectedPhotos.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TSBRemoveSelectedPhotos.Name = "TSBRemoveSelectedPhotos"
        Me.TSBRemoveSelectedPhotos.Size = New System.Drawing.Size(260, 22)
        Me.TSBRemoveSelectedPhotos.Text = "Remove selected photos"
        '
        'TSBRemovePhotosNoLongerAvailable
        '
        Me.TSBRemovePhotosNoLongerAvailable.Name = "TSBRemovePhotosNoLongerAvailable"
        Me.TSBRemovePhotosNoLongerAvailable.Size = New System.Drawing.Size(260, 22)
        Me.TSBRemovePhotosNoLongerAvailable.Text = "Remove photos no longer available"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(257, 6)
        '
        'TSBRemoveAllPhotos
        '
        Me.TSBRemoveAllPhotos.Name = "TSBRemoveAllPhotos"
        Me.TSBRemoveAllPhotos.Size = New System.Drawing.Size(260, 22)
        Me.TSBRemoveAllPhotos.Text = "Remove all photos..."
        '
        'TBSAddPhotos
        '
        Me.TBSAddPhotos.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.TBSAddPhotos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TBSAddPhotos.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBAddPhotosFolder, Me.TSBAddPhotosFile})
        Me.TBSAddPhotos.Image = CType(resources.GetObject("TBSAddPhotos.Image"), System.Drawing.Image)
        Me.TBSAddPhotos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TBSAddPhotos.Name = "TBSAddPhotos"
        Me.TBSAddPhotos.Size = New System.Drawing.Size(32, 22)
        '
        'TSBAddPhotosFolder
        '
        Me.TSBAddPhotosFolder.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TSBAddPhotosFolder.Name = "TSBAddPhotosFolder"
        Me.TSBAddPhotosFolder.Size = New System.Drawing.Size(213, 22)
        Me.TSBAddPhotosFolder.Text = "Add photos from folder..."
        '
        'TSBAddPhotosFile
        '
        Me.TSBAddPhotosFile.Name = "TSBAddPhotosFile"
        Me.TSBAddPhotosFile.Size = New System.Drawing.Size(213, 22)
        Me.TSBAddPhotosFile.Text = "Add photos individually..."
        '
        'TSBRenamePhotoFiles
        '
        Me.TSBRenamePhotoFiles.Image = CType(resources.GetObject("TSBRenamePhotoFiles.Image"), System.Drawing.Image)
        Me.TSBRenamePhotoFiles.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBRenamePhotoFiles.Name = "TSBRenamePhotoFiles"
        Me.TSBRenamePhotoFiles.Size = New System.Drawing.Size(70, 22)
        Me.TSBRenamePhotoFiles.Text = "Rename"
        '
        'TSBSortPhotos
        '
        Me.TSBSortPhotos.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBSortPhotosByLocationName, Me.TSBSortPhotosByTakenDate, Me.TSBSortPhotosSameFolder, Me.ToolStripMenuItem1})
        Me.TSBSortPhotos.Image = CType(resources.GetObject("TSBSortPhotos.Image"), System.Drawing.Image)
        Me.TSBSortPhotos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBSortPhotos.Name = "TSBSortPhotos"
        Me.TSBSortPhotos.Size = New System.Drawing.Size(110, 22)
        Me.TSBSortPhotos.Text = "Sort to Folder"
        '
        'TSBSortPhotosByLocationName
        '
        Me.TSBSortPhotosByLocationName.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TSBSortPhotosByLocationName.Name = "TSBSortPhotosByLocationName"
        Me.TSBSortPhotosByLocationName.Size = New System.Drawing.Size(200, 22)
        Me.TSBSortPhotosByLocationName.Text = "Sort by Location Name"
        '
        'TSBSortPhotosByTakenDate
        '
        Me.TSBSortPhotosByTakenDate.Name = "TSBSortPhotosByTakenDate"
        Me.TSBSortPhotosByTakenDate.Size = New System.Drawing.Size(200, 22)
        Me.TSBSortPhotosByTakenDate.Text = "Sort by Taken Date"
        '
        'TSBSortPhotosSameFolder
        '
        Me.TSBSortPhotosSameFolder.Name = "TSBSortPhotosSameFolder"
        Me.TSBSortPhotosSameFolder.Size = New System.Drawing.Size(200, 22)
        Me.TSBSortPhotosSameFolder.Text = "All same folder"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(197, 6)
        '
        'CAPTIONPhotos
        '
        CAPTIONPhotos.AutoEllipsis = True
        CAPTIONPhotos.AutoSize = True
        CAPTIONPhotos.BackColor = System.Drawing.SystemColors.ActiveCaption
        CAPTIONPhotos.Dock = System.Windows.Forms.DockStyle.Top
        CAPTIONPhotos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        CAPTIONPhotos.Location = New System.Drawing.Point(0, 0)
        CAPTIONPhotos.Name = "CAPTIONPhotos"
        CAPTIONPhotos.Size = New System.Drawing.Size(315, 13)
        CAPTIONPhotos.TabIndex = 6
        CAPTIONPhotos.Text = "Photos"
        CAPTIONPhotos.TextAlign = System.Drawing.ContentAlignment.TopCenter
        CAPTIONPhotos.TrackFocus = Me.PANELPhotos
        '
        'TSProject
        '
        Me.TSProject.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TSProject.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton11, Me.ToolStripButton14, Me.ToolStripSeparator4})
        Me.TSProject.Location = New System.Drawing.Point(0, 13)
        Me.TSProject.Name = "TSProject"
        Me.TSProject.Size = New System.Drawing.Size(739, 25)
        Me.TSProject.TabIndex = 5
        Me.TSProject.Text = "ToolStrip1"
        '
        'ToolStripButton11
        '
        Me.ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton11.Image = CType(resources.GetObject("ToolStripButton11.Image"), System.Drawing.Image)
        Me.ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton11.Name = "ToolStripButton11"
        Me.ToolStripButton11.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton11.Text = "ToolStripButton4"
        Me.ToolStripButton11.ToolTipText = "Import (CSV)"
        '
        'ToolStripButton14
        '
        Me.ToolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton14.Image = CType(resources.GetObject("ToolStripButton14.Image"), System.Drawing.Image)
        Me.ToolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton14.Name = "ToolStripButton14"
        Me.ToolStripButton14.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton14.Text = "ToolStripButton14"
        Me.ToolStripButton14.ToolTipText = "Export (CSV)"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'CAPTIONProject
        '
        CAPTIONProject.AutoEllipsis = True
        CAPTIONProject.AutoSize = True
        CAPTIONProject.BackColor = System.Drawing.SystemColors.ActiveCaption
        CAPTIONProject.Dock = System.Windows.Forms.DockStyle.Top
        CAPTIONProject.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        CAPTIONProject.Location = New System.Drawing.Point(0, 0)
        CAPTIONProject.Name = "CAPTIONProject"
        CAPTIONProject.Size = New System.Drawing.Size(739, 13)
        CAPTIONProject.TabIndex = 0
        CAPTIONProject.Text = "Project"
        CAPTIONProject.TextAlign = System.Drawing.ContentAlignment.TopCenter
        CAPTIONProject.TrackFocus = PANELProject
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
        '
        'STATUSMain
        '
        STATUSMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SSLStatus, Me.SSPTaskProgress, Me.SSBStop})
        STATUSMain.Location = New System.Drawing.Point(0, 565)
        STATUSMain.Name = "STATUSMain"
        STATUSMain.Size = New System.Drawing.Size(1402, 22)
        STATUSMain.TabIndex = 5
        STATUSMain.Text = "StatusStrip1"
        '
        'SSLStatus
        '
        Me.SSLStatus.Name = "SSLStatus"
        Me.SSLStatus.Size = New System.Drawing.Size(39, 17)
        Me.SSLStatus.Text = "Ready"
        '
        'SSPTaskProgress
        '
        Me.SSPTaskProgress.Name = "SSPTaskProgress"
        Me.SSPTaskProgress.Size = New System.Drawing.Size(100, 16)
        Me.SSPTaskProgress.Visible = False
        '
        'SSBStop
        '
        Me.SSBStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SSBStop.Image = CType(resources.GetObject("SSBStop.Image"), System.Drawing.Image)
        Me.SSBStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SSBStop.Name = "SSBStop"
        Me.SSBStop.Size = New System.Drawing.Size(23, 20)
        Me.SSBStop.Text = "ToolStripDropDownButton1"
        Me.SSBStop.Visible = False
        '
        'TSMain
        '
        Me.TSMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TSMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {ToolStripButton1, ToolStripButton2})
        Me.TSMain.Location = New System.Drawing.Point(0, 24)
        Me.TSMain.Name = "TSMain"
        Me.TSMain.Size = New System.Drawing.Size(1402, 25)
        Me.TSMain.TabIndex = 0
        '
        'ILFolder
        '
        Me.ILFolder.ImageStream = CType(resources.GetObject("ILFolder.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ILFolder.TransparentColor = System.Drawing.Color.Transparent
        Me.ILFolder.Images.SetKeyName(0, "VSO_Folder_16x.png")
        Me.ILFolder.Images.SetKeyName(1, "VSO_Document_16x.png")
        '
        'FMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1402, 587)
        Me.Controls.Add(PANELMap)
        Me.Controls.Add(SPLITTERMain)
        Me.Controls.Add(PANELProject)
        Me.Controls.Add(STATUSMain)
        Me.Controls.Add(Me.TSMain)
        Me.Controls.Add(MENUMain)
        Me.MainMenuStrip = MENUMain
        Me.MinimumSize = New System.Drawing.Size(570, 340)
        Me.Name = "FMain"
        Me.Text = "PhotoGPS"
        MENUMain.ResumeLayout(False)
        MENUMain.PerformLayout()
        PANELMap.ResumeLayout(False)
        PANELMap.PerformLayout()
        Me.TSAddress.ResumeLayout(False)
        Me.TSAddress.PerformLayout()
        Me.TSCoords.ResumeLayout(False)
        Me.TSCoords.PerformLayout()
        Me.TSMap.ResumeLayout(False)
        Me.TSMap.PerformLayout()
        PANELProject.ResumeLayout(False)
        PANELProject.PerformLayout()
        Me.PANELLocations.ResumeLayout(False)
        Me.PANELLocations.PerformLayout()
        Me.TSLocations.ResumeLayout(False)
        Me.TSLocations.PerformLayout()
        Me.PANELPhotos.ResumeLayout(False)
        Me.PANELPhotos.PerformLayout()
        Me.TSPhotos.ResumeLayout(False)
        Me.TSPhotos.PerformLayout()
        Me.TSProject.ResumeLayout(False)
        Me.TSProject.PerformLayout()
        STATUSMain.ResumeLayout(False)
        STATUSMain.PerformLayout()
        Me.TSMain.ResumeLayout(False)
        Me.TSMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Friend WithEvents MAP As GMap.NET.WindowsForms.GMapControl
    Friend WithEvents ILFolder As ImageList
    Friend WithEvents PANELLocations As Panel
    Friend WithEvents PANELPhotos As Panel
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents TSBImportLocations As ToolStripButton
    Friend WithEvents TSBExportLocations As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TSBLocationsVisible As ToolStripButton
    Friend WithEvents TSBGetLocationCoords As ToolStripButton
    Friend WithEvents LVLocations As ListView
    Friend WithEvents LVPhotos As ListView
    Friend WithEvents SSPTaskProgress As ToolStripProgressBar
    Friend WithEvents SSBStop As ToolStripButton
    Friend WithEvents ColumnHeader13 As ColumnHeader
    Friend WithEvents TSBAddPhotosFolder As ToolStripMenuItem
    Friend WithEvents TSBAddPhotosFile As ToolStripMenuItem
    Friend WithEvents TSBRemoveSelectedPhotos As ToolStripMenuItem
    Friend WithEvents TSBRemovePhotosNoLongerAvailable As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents TSBRemoveAllPhotos As ToolStripMenuItem
    Friend WithEvents ToolStripButton11 As ToolStripButton
    Friend WithEvents ToolStripButton14 As ToolStripButton
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents TSBRenamePhotoFiles As ToolStripButton
    Friend WithEvents TSBEditLocation As ToolStripButton
    Friend WithEvents TSBRemoveLocations As ToolStripButton
    Friend WithEvents TSBAddLocation As ToolStripButton
    Friend WithEvents TBSAddPhotos As ToolStripSplitButton
    Friend WithEvents TSBRemovePhotos As ToolStripSplitButton
    Friend WithEvents TSBSortPhotos As ToolStripSplitButton
    Friend WithEvents TSBSortPhotosByLocationName As ToolStripMenuItem
    Friend WithEvents TSBSortPhotosByTakenDate As ToolStripMenuItem
    Friend WithEvents TSBSortPhotosSameFolder As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents TSPhotos As ToolStrip
    Friend WithEvents TSLocations As ToolStrip
    Friend WithEvents TSAddress As ToolStrip
    Friend WithEvents TSCoords As ToolStrip
    Friend WithEvents TSMap As ToolStrip
    Friend WithEvents TSProject As ToolStrip
    Friend WithEvents TSMain As ToolStrip
    Friend WithEvents SSLStatus As ToolStripStatusLabel
End Class

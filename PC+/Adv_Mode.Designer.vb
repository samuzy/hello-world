<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Adv_Mode

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim cmd_Client As System.Windows.Forms.Button
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Adv_Mode))
        Dim cmd_Collection As System.Windows.Forms.Button
        Me.TT = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmd_registry_pol = New System.Windows.Forms.Button()
        Me.cmd_GPO = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmd_load_Logs5 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmb_Logs5 = New System.Windows.Forms.ComboBox()
        Me.cmd_load_Logs4 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmb_Logs4 = New System.Windows.Forms.ComboBox()
        Me.cmd_load_Logs3 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmb_Logs3 = New System.Windows.Forms.ComboBox()
        Me.cmd_load_Logs2 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmb_Logs2 = New System.Windows.Forms.ComboBox()
        Me.txt_Description = New System.Windows.Forms.TextBox()
        Me.cmd_load_Logs1 = New System.Windows.Forms.Button()
        Me.lbl_description = New System.Windows.Forms.Label()
        Me.lbl_logs = New System.Windows.Forms.Label()
        Me.cmb_Logs1 = New System.Windows.Forms.ComboBox()
        Me.cmd_Add_SW = New System.Windows.Forms.Button()
        Me.cmd_Rebuilding_WMI = New System.Windows.Forms.Button()
        Me.cmd_Re_Registering = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.pic_redflag2 = New System.Windows.Forms.PictureBox()
        Me.pic_greenflag2 = New System.Windows.Forms.PictureBox()
        Me.cmd_Port_8009 = New System.Windows.Forms.Button()
        Me.txt_ListenPort = New System.Windows.Forms.TextBox()
        Me.txt_ConnectPort = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pic_redflag1 = New System.Windows.Forms.PictureBox()
        Me.pic_greenflag1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txt_Mac = New System.Windows.Forms.TextBox()
        Me.txt_cache_location = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_IP = New System.Windows.Forms.TextBox()
        Me.txt_Cache_Size = New System.Windows.Forms.TextBox()
        Me.txt_ComputerName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbl_abr_size = New System.Windows.Forms.Label()
        Me.cmd_Del_WMI = New System.Windows.Forms.Button()
        Me.cmd_WSUS_Download = New System.Windows.Forms.Button()
        Me.cmd_DataStore = New System.Windows.Forms.Button()
        Me.cmd_Client_Logs = New System.Windows.Forms.Button()
        Me.cmd_BITS_Location = New System.Windows.Forms.Button()
        Me.lbl_Version = New System.Windows.Forms.Label()
        Me.lbl_loading = New System.Windows.Forms.Label()
        cmd_Client = New System.Windows.Forms.Button()
        cmd_Collection = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.pic_redflag2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_greenflag2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_redflag1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_greenflag1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmd_Client
        '
        resources.ApplyResources(cmd_Client, "cmd_Client")
        cmd_Client.Name = "cmd_Client"
        cmd_Client.UseVisualStyleBackColor = True
        AddHandler cmd_Client.Click, AddressOf Me.cmd_Client_Click
        '
        'cmd_Collection
        '
        resources.ApplyResources(cmd_Collection, "cmd_Collection")
        cmd_Collection.Name = "cmd_Collection"
        cmd_Collection.UseVisualStyleBackColor = True
        AddHandler cmd_Collection.Click, AddressOf Me.cmd_Collection_Click
        '
        'TT
        '
        Me.TT.AutomaticDelay = 100
        Me.TT.AutoPopDelay = 5000
        Me.TT.InitialDelay = 100
        Me.TT.IsBalloon = True
        Me.TT.ReshowDelay = 20
        '
        'cmd_registry_pol
        '
        resources.ApplyResources(Me.cmd_registry_pol, "cmd_registry_pol")
        Me.cmd_registry_pol.Name = "cmd_registry_pol"
        Me.TT.SetToolTip(Me.cmd_registry_pol, resources.GetString("cmd_registry_pol.ToolTip"))
        Me.cmd_registry_pol.UseVisualStyleBackColor = True
        '
        'cmd_GPO
        '
        resources.ApplyResources(Me.cmd_GPO, "cmd_GPO")
        Me.cmd_GPO.Name = "cmd_GPO"
        Me.TT.SetToolTip(Me.cmd_GPO, resources.GetString("cmd_GPO.ToolTip"))
        Me.cmd_GPO.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(cmd_Collection)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.cmd_Add_SW)
        Me.Panel1.Controls.Add(cmd_Client)
        Me.Panel1.Controls.Add(Me.cmd_registry_pol)
        Me.Panel1.Controls.Add(Me.cmd_Rebuilding_WMI)
        Me.Panel1.Controls.Add(Me.cmd_Re_Registering)
        Me.Panel1.Controls.Add(Me.cmd_GPO)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.cmd_Del_WMI)
        Me.Panel1.Controls.Add(Me.cmd_WSUS_Download)
        Me.Panel1.Controls.Add(Me.cmd_DataStore)
        Me.Panel1.Controls.Add(Me.cmd_Client_Logs)
        Me.Panel1.Controls.Add(Me.cmd_BITS_Location)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmd_load_Logs5)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.cmb_Logs5)
        Me.GroupBox3.Controls.Add(Me.cmd_load_Logs4)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.cmb_Logs4)
        Me.GroupBox3.Controls.Add(Me.cmd_load_Logs3)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.cmb_Logs3)
        Me.GroupBox3.Controls.Add(Me.cmd_load_Logs2)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.cmb_Logs2)
        Me.GroupBox3.Controls.Add(Me.txt_Description)
        Me.GroupBox3.Controls.Add(Me.cmd_load_Logs1)
        Me.GroupBox3.Controls.Add(Me.lbl_description)
        Me.GroupBox3.Controls.Add(Me.lbl_logs)
        Me.GroupBox3.Controls.Add(Me.cmb_Logs1)
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'cmd_load_Logs5
        '
        resources.ApplyResources(Me.cmd_load_Logs5, "cmd_load_Logs5")
        Me.cmd_load_Logs5.Name = "cmd_load_Logs5"
        Me.cmd_load_Logs5.UseVisualStyleBackColor = True
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'cmb_Logs5
        '
        Me.cmb_Logs5.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs5, "cmb_Logs5")
        Me.cmb_Logs5.FormattingEnabled = True
        Me.cmb_Logs5.Items.AddRange(New Object() {resources.GetString("cmb_Logs5.Items"), resources.GetString("cmb_Logs5.Items1"), resources.GetString("cmb_Logs5.Items2"), resources.GetString("cmb_Logs5.Items3"), resources.GetString("cmb_Logs5.Items4"), resources.GetString("cmb_Logs5.Items5"), resources.GetString("cmb_Logs5.Items6"), resources.GetString("cmb_Logs5.Items7"), resources.GetString("cmb_Logs5.Items8"), resources.GetString("cmb_Logs5.Items9"), resources.GetString("cmb_Logs5.Items10"), resources.GetString("cmb_Logs5.Items11"), resources.GetString("cmb_Logs5.Items12"), resources.GetString("cmb_Logs5.Items13"), resources.GetString("cmb_Logs5.Items14")})
        Me.cmb_Logs5.Name = "cmb_Logs5"
        '
        'cmd_load_Logs4
        '
        resources.ApplyResources(Me.cmd_load_Logs4, "cmd_load_Logs4")
        Me.cmd_load_Logs4.Name = "cmd_load_Logs4"
        Me.cmd_load_Logs4.UseVisualStyleBackColor = True
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'cmb_Logs4
        '
        Me.cmb_Logs4.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs4, "cmb_Logs4")
        Me.cmb_Logs4.FormattingEnabled = True
        Me.cmb_Logs4.Items.AddRange(New Object() {resources.GetString("cmb_Logs4.Items"), resources.GetString("cmb_Logs4.Items1"), resources.GetString("cmb_Logs4.Items2"), resources.GetString("cmb_Logs4.Items3"), resources.GetString("cmb_Logs4.Items4"), resources.GetString("cmb_Logs4.Items5"), resources.GetString("cmb_Logs4.Items6"), resources.GetString("cmb_Logs4.Items7"), resources.GetString("cmb_Logs4.Items8"), resources.GetString("cmb_Logs4.Items9"), resources.GetString("cmb_Logs4.Items10"), resources.GetString("cmb_Logs4.Items11"), resources.GetString("cmb_Logs4.Items12"), resources.GetString("cmb_Logs4.Items13"), resources.GetString("cmb_Logs4.Items14"), resources.GetString("cmb_Logs4.Items15"), resources.GetString("cmb_Logs4.Items16"), resources.GetString("cmb_Logs4.Items17"), resources.GetString("cmb_Logs4.Items18"), resources.GetString("cmb_Logs4.Items19"), resources.GetString("cmb_Logs4.Items20"), resources.GetString("cmb_Logs4.Items21")})
        Me.cmb_Logs4.Name = "cmb_Logs4"
        '
        'cmd_load_Logs3
        '
        resources.ApplyResources(Me.cmd_load_Logs3, "cmd_load_Logs3")
        Me.cmd_load_Logs3.Name = "cmd_load_Logs3"
        Me.cmd_load_Logs3.UseVisualStyleBackColor = True
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'cmb_Logs3
        '
        Me.cmb_Logs3.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs3, "cmb_Logs3")
        Me.cmb_Logs3.FormattingEnabled = True
        Me.cmb_Logs3.Items.AddRange(New Object() {resources.GetString("cmb_Logs3.Items"), resources.GetString("cmb_Logs3.Items1"), resources.GetString("cmb_Logs3.Items2"), resources.GetString("cmb_Logs3.Items3"), resources.GetString("cmb_Logs3.Items4"), resources.GetString("cmb_Logs3.Items5"), resources.GetString("cmb_Logs3.Items6"), resources.GetString("cmb_Logs3.Items7")})
        Me.cmb_Logs3.Name = "cmb_Logs3"
        '
        'cmd_load_Logs2
        '
        resources.ApplyResources(Me.cmd_load_Logs2, "cmd_load_Logs2")
        Me.cmd_load_Logs2.Name = "cmd_load_Logs2"
        Me.cmd_load_Logs2.UseVisualStyleBackColor = True
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'cmb_Logs2
        '
        Me.cmb_Logs2.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs2, "cmb_Logs2")
        Me.cmb_Logs2.FormattingEnabled = True
        Me.cmb_Logs2.Items.AddRange(New Object() {resources.GetString("cmb_Logs2.Items"), resources.GetString("cmb_Logs2.Items1"), resources.GetString("cmb_Logs2.Items2"), resources.GetString("cmb_Logs2.Items3")})
        Me.cmb_Logs2.Name = "cmb_Logs2"
        '
        'txt_Description
        '
        Me.txt_Description.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.txt_Description, "txt_Description")
        Me.txt_Description.Name = "txt_Description"
        '
        'cmd_load_Logs1
        '
        resources.ApplyResources(Me.cmd_load_Logs1, "cmd_load_Logs1")
        Me.cmd_load_Logs1.Name = "cmd_load_Logs1"
        Me.cmd_load_Logs1.UseVisualStyleBackColor = True
        '
        'lbl_description
        '
        resources.ApplyResources(Me.lbl_description, "lbl_description")
        Me.lbl_description.Name = "lbl_description"
        '
        'lbl_logs
        '
        resources.ApplyResources(Me.lbl_logs, "lbl_logs")
        Me.lbl_logs.Name = "lbl_logs"
        '
        'cmb_Logs1
        '
        Me.cmb_Logs1.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs1, "cmb_Logs1")
        Me.cmb_Logs1.FormattingEnabled = True
        Me.cmb_Logs1.Items.AddRange(New Object() {resources.GetString("cmb_Logs1.Items"), resources.GetString("cmb_Logs1.Items1"), resources.GetString("cmb_Logs1.Items2"), resources.GetString("cmb_Logs1.Items3"), resources.GetString("cmb_Logs1.Items4"), resources.GetString("cmb_Logs1.Items5"), resources.GetString("cmb_Logs1.Items6"), resources.GetString("cmb_Logs1.Items7"), resources.GetString("cmb_Logs1.Items8"), resources.GetString("cmb_Logs1.Items9"), resources.GetString("cmb_Logs1.Items10"), resources.GetString("cmb_Logs1.Items11"), resources.GetString("cmb_Logs1.Items12"), resources.GetString("cmb_Logs1.Items13"), resources.GetString("cmb_Logs1.Items14"), resources.GetString("cmb_Logs1.Items15"), resources.GetString("cmb_Logs1.Items16"), resources.GetString("cmb_Logs1.Items17"), resources.GetString("cmb_Logs1.Items18"), resources.GetString("cmb_Logs1.Items19"), resources.GetString("cmb_Logs1.Items20"), resources.GetString("cmb_Logs1.Items21"), resources.GetString("cmb_Logs1.Items22"), resources.GetString("cmb_Logs1.Items23"), resources.GetString("cmb_Logs1.Items24"), resources.GetString("cmb_Logs1.Items25"), resources.GetString("cmb_Logs1.Items26"), resources.GetString("cmb_Logs1.Items27"), resources.GetString("cmb_Logs1.Items28"), resources.GetString("cmb_Logs1.Items29"), resources.GetString("cmb_Logs1.Items30"), resources.GetString("cmb_Logs1.Items31"), resources.GetString("cmb_Logs1.Items32"), resources.GetString("cmb_Logs1.Items33"), resources.GetString("cmb_Logs1.Items34"), resources.GetString("cmb_Logs1.Items35"), resources.GetString("cmb_Logs1.Items36"), resources.GetString("cmb_Logs1.Items37"), resources.GetString("cmb_Logs1.Items38"), resources.GetString("cmb_Logs1.Items39"), resources.GetString("cmb_Logs1.Items40"), resources.GetString("cmb_Logs1.Items41"), resources.GetString("cmb_Logs1.Items42"), resources.GetString("cmb_Logs1.Items43"), resources.GetString("cmb_Logs1.Items44"), resources.GetString("cmb_Logs1.Items45"), resources.GetString("cmb_Logs1.Items46"), resources.GetString("cmb_Logs1.Items47"), resources.GetString("cmb_Logs1.Items48"), resources.GetString("cmb_Logs1.Items49"), resources.GetString("cmb_Logs1.Items50"), resources.GetString("cmb_Logs1.Items51"), resources.GetString("cmb_Logs1.Items52"), resources.GetString("cmb_Logs1.Items53"), resources.GetString("cmb_Logs1.Items54"), resources.GetString("cmb_Logs1.Items55"), resources.GetString("cmb_Logs1.Items56"), resources.GetString("cmb_Logs1.Items57"), resources.GetString("cmb_Logs1.Items58")})
        Me.cmb_Logs1.Name = "cmb_Logs1"
        '
        'cmd_Add_SW
        '
        Me.cmd_Add_SW.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.cmd_Add_SW, "cmd_Add_SW")
        Me.cmd_Add_SW.Name = "cmd_Add_SW"
        Me.cmd_Add_SW.UseVisualStyleBackColor = False
        '
        'cmd_Rebuilding_WMI
        '
        resources.ApplyResources(Me.cmd_Rebuilding_WMI, "cmd_Rebuilding_WMI")
        Me.cmd_Rebuilding_WMI.Name = "cmd_Rebuilding_WMI"
        Me.cmd_Rebuilding_WMI.UseVisualStyleBackColor = True
        '
        'cmd_Re_Registering
        '
        resources.ApplyResources(Me.cmd_Re_Registering, "cmd_Re_Registering")
        Me.cmd_Re_Registering.Name = "cmd_Re_Registering"
        Me.cmd_Re_Registering.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.pic_redflag2)
        Me.GroupBox2.Controls.Add(Me.pic_greenflag2)
        Me.GroupBox2.Controls.Add(Me.cmd_Port_8009)
        Me.GroupBox2.Controls.Add(Me.txt_ListenPort)
        Me.GroupBox2.Controls.Add(Me.txt_ConnectPort)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.pic_redflag1)
        Me.GroupBox2.Controls.Add(Me.pic_greenflag1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'pic_redflag2
        '
        resources.ApplyResources(Me.pic_redflag2, "pic_redflag2")
        Me.pic_redflag2.Name = "pic_redflag2"
        Me.pic_redflag2.TabStop = False
        '
        'pic_greenflag2
        '
        resources.ApplyResources(Me.pic_greenflag2, "pic_greenflag2")
        Me.pic_greenflag2.Name = "pic_greenflag2"
        Me.pic_greenflag2.TabStop = False
        '
        'cmd_Port_8009
        '
        resources.ApplyResources(Me.cmd_Port_8009, "cmd_Port_8009")
        Me.cmd_Port_8009.Name = "cmd_Port_8009"
        Me.cmd_Port_8009.UseVisualStyleBackColor = True
        '
        'txt_ListenPort
        '
        Me.txt_ListenPort.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_ListenPort.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_ListenPort, "txt_ListenPort")
        Me.txt_ListenPort.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_ListenPort.Name = "txt_ListenPort"
        Me.txt_ListenPort.ReadOnly = True
        '
        'txt_ConnectPort
        '
        Me.txt_ConnectPort.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_ConnectPort.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_ConnectPort, "txt_ConnectPort")
        Me.txt_ConnectPort.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_ConnectPort.Name = "txt_ConnectPort"
        Me.txt_ConnectPort.ReadOnly = True
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'pic_redflag1
        '
        resources.ApplyResources(Me.pic_redflag1, "pic_redflag1")
        Me.pic_redflag1.Name = "pic_redflag1"
        Me.pic_redflag1.TabStop = False
        '
        'pic_greenflag1
        '
        resources.ApplyResources(Me.pic_greenflag1, "pic_greenflag1")
        Me.pic_greenflag1.Name = "pic_greenflag1"
        Me.pic_greenflag1.TabStop = False
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.txt_Mac)
        Me.GroupBox1.Controls.Add(Me.txt_cache_location)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txt_IP)
        Me.GroupBox1.Controls.Add(Me.txt_Cache_Size)
        Me.GroupBox1.Controls.Add(Me.txt_ComputerName)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lbl_abr_size)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'txt_Mac
        '
        Me.txt_Mac.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_Mac.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_Mac, "txt_Mac")
        Me.txt_Mac.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_Mac.Name = "txt_Mac"
        Me.txt_Mac.ReadOnly = True
        '
        'txt_cache_location
        '
        Me.txt_cache_location.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_cache_location.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.txt_cache_location, "txt_cache_location")
        Me.txt_cache_location.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_cache_location.Name = "txt_cache_location"
        Me.txt_cache_location.ReadOnly = True
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'txt_IP
        '
        Me.txt_IP.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_IP.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_IP, "txt_IP")
        Me.txt_IP.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_IP.Name = "txt_IP"
        Me.txt_IP.ReadOnly = True
        '
        'txt_Cache_Size
        '
        Me.txt_Cache_Size.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_Cache_Size.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_Cache_Size, "txt_Cache_Size")
        Me.txt_Cache_Size.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_Cache_Size.Name = "txt_Cache_Size"
        Me.txt_Cache_Size.ReadOnly = True
        '
        'txt_ComputerName
        '
        Me.txt_ComputerName.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_ComputerName.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_ComputerName, "txt_ComputerName")
        Me.txt_ComputerName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_ComputerName.Name = "txt_ComputerName"
        Me.txt_ComputerName.ReadOnly = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'lbl_abr_size
        '
        resources.ApplyResources(Me.lbl_abr_size, "lbl_abr_size")
        Me.lbl_abr_size.Name = "lbl_abr_size"
        '
        'cmd_Del_WMI
        '
        resources.ApplyResources(Me.cmd_Del_WMI, "cmd_Del_WMI")
        Me.cmd_Del_WMI.Name = "cmd_Del_WMI"
        Me.cmd_Del_WMI.UseVisualStyleBackColor = True
        '
        'cmd_WSUS_Download
        '
        resources.ApplyResources(Me.cmd_WSUS_Download, "cmd_WSUS_Download")
        Me.cmd_WSUS_Download.Name = "cmd_WSUS_Download"
        Me.cmd_WSUS_Download.UseVisualStyleBackColor = True
        '
        'cmd_DataStore
        '
        resources.ApplyResources(Me.cmd_DataStore, "cmd_DataStore")
        Me.cmd_DataStore.Name = "cmd_DataStore"
        Me.cmd_DataStore.UseVisualStyleBackColor = True
        '
        'cmd_Client_Logs
        '
        resources.ApplyResources(Me.cmd_Client_Logs, "cmd_Client_Logs")
        Me.cmd_Client_Logs.Name = "cmd_Client_Logs"
        Me.cmd_Client_Logs.UseVisualStyleBackColor = True
        '
        'cmd_BITS_Location
        '
        resources.ApplyResources(Me.cmd_BITS_Location, "cmd_BITS_Location")
        Me.cmd_BITS_Location.Name = "cmd_BITS_Location"
        Me.cmd_BITS_Location.UseVisualStyleBackColor = True
        '
        'lbl_Version
        '
        resources.ApplyResources(Me.lbl_Version, "lbl_Version")
        Me.lbl_Version.Name = "lbl_Version"
        '
        'lbl_loading
        '
        resources.ApplyResources(Me.lbl_loading, "lbl_loading")
        Me.lbl_loading.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_loading.Name = "lbl_loading"
        '
        'Adv_Mode
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.Controls.Add(Me.lbl_loading)
        Me.Controls.Add(Me.lbl_Version)
        Me.Controls.Add(Me.Panel1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "Adv_Mode"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.pic_redflag2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_greenflag2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_redflag1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_greenflag1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TT As System.Windows.Forms.ToolTip
    Friend WithEvents lbl_Version As System.Windows.Forms.Label
    Friend WithEvents lbl_loading As System.Windows.Forms.Label
    Friend WithEvents cmd_WSUS_Download As System.Windows.Forms.Button
    Friend WithEvents cmd_DataStore As System.Windows.Forms.Button
    Friend WithEvents cmd_Client_Logs As System.Windows.Forms.Button
    Friend WithEvents cmd_BITS_Location As System.Windows.Forms.Button
    Friend WithEvents lbl_abr_size As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_Cache_Size As System.Windows.Forms.TextBox
    Friend WithEvents cmd_Del_WMI As System.Windows.Forms.Button
    Friend WithEvents cmd_Port_8009 As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_ComputerName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_cache_location As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_ListenPort As System.Windows.Forms.TextBox
    Friend WithEvents txt_ConnectPort As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pic_redflag2 As System.Windows.Forms.PictureBox
    Friend WithEvents pic_greenflag2 As System.Windows.Forms.PictureBox
    Friend WithEvents pic_redflag1 As System.Windows.Forms.PictureBox
    Friend WithEvents pic_greenflag1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmd_GPO As System.Windows.Forms.Button
    Friend WithEvents txt_Mac As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_IP As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmd_Re_Registering As System.Windows.Forms.Button
    Friend WithEvents cmd_Rebuilding_WMI As System.Windows.Forms.Button
    Friend WithEvents cmd_registry_pol As System.Windows.Forms.Button
    Friend WithEvents cmd_Add_SW As System.Windows.Forms.Button
    Friend WithEvents cmd_load_Logs1 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_Description As System.Windows.Forms.TextBox
    Friend WithEvents lbl_description As System.Windows.Forms.Label
    Friend WithEvents lbl_logs As System.Windows.Forms.Label
    Friend WithEvents cmb_Logs1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_load_Logs5 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmb_Logs5 As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_load_Logs4 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmb_Logs4 As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_load_Logs3 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmb_Logs3 As System.Windows.Forms.ComboBox
    Friend WithEvents cmd_load_Logs2 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmb_Logs2 As System.Windows.Forms.ComboBox

End Class

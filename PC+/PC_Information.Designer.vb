<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PC_Info
    Inherits PC_.LocalizedForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PC_Info))
        Me.TT = New System.Windows.Forms.ToolTip(Me.components)
        Me.lbl_loading = New System.Windows.Forms.Label()
        Me.lbl_Version = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txt_SRU_Verimg = New System.Windows.Forms.TextBox()
        Me.lbl_SRUVerimg = New System.Windows.Forms.Label()
        Me.cmd_manage = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.MembershipListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.txt_last_reboot = New System.Windows.Forms.TextBox()
        Me.txt_language = New System.Windows.Forms.TextBox()
        Me.txt_img_ver = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txt_VerImg = New System.Windows.Forms.Label()
        Me.txt_IP = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txt_TypePC = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txt_OSCaption = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt_img_install_Date = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txt_CPU = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txt_RAM = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt_Version = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txt_Name = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt_Vendor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Domain = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txt_ComputerName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TT
        '
        Me.TT.AutoPopDelay = 5000
        Me.TT.InitialDelay = 500
        Me.TT.IsBalloon = True
        Me.TT.ReshowDelay = 5
        Me.TT.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'lbl_loading
        '
        resources.ApplyResources(Me.lbl_loading, "lbl_loading")
        Me.lbl_loading.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_loading.Name = "lbl_loading"
        '
        'lbl_Version
        '
        resources.ApplyResources(Me.lbl_Version, "lbl_Version")
        Me.lbl_Version.Name = "lbl_Version"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txt_SRU_Verimg)
        Me.Panel1.Controls.Add(Me.lbl_SRUVerimg)
        Me.Panel1.Controls.Add(Me.cmd_manage)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.PictureBox3)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.txt_last_reboot)
        Me.Panel1.Controls.Add(Me.txt_language)
        Me.Panel1.Controls.Add(Me.txt_img_ver)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txt_VerImg)
        Me.Panel1.Controls.Add(Me.txt_IP)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.txt_TypePC)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txt_OSCaption)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txt_img_install_Date)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.txt_CPU)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txt_RAM)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txt_Version)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txt_Name)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txt_Vendor)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txt_Domain)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txt_ComputerName)
        Me.Panel1.Controls.Add(Me.Label12)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'txt_SRU_Verimg
        '
        Me.txt_SRU_Verimg.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_SRU_Verimg.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_SRU_Verimg, "txt_SRU_Verimg")
        Me.txt_SRU_Verimg.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_SRU_Verimg.Name = "txt_SRU_Verimg"
        Me.txt_SRU_Verimg.ReadOnly = True
        '
        'lbl_SRUVerimg
        '
        resources.ApplyResources(Me.lbl_SRUVerimg, "lbl_SRUVerimg")
        Me.lbl_SRUVerimg.Name = "lbl_SRUVerimg"
        '
        'cmd_manage
        '
        resources.ApplyResources(Me.cmd_manage, "cmd_manage")
        Me.cmd_manage.Name = "cmd_manage"
        Me.cmd_manage.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.MembershipListView)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'MembershipListView
        '
        Me.MembershipListView.BackColor = System.Drawing.Color.AliceBlue
        Me.MembershipListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.MembershipListView.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.MembershipListView, "MembershipListView")
        Me.MembershipListView.ForeColor = System.Drawing.Color.MidnightBlue
        Me.MembershipListView.FullRowSelect = True
        Me.MembershipListView.GridLines = True
        Me.MembershipListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.MembershipListView.HideSelection = False
        Me.MembershipListView.MultiSelect = False
        Me.MembershipListView.Name = "MembershipListView"
        Me.MembershipListView.UseCompatibleStateImageBehavior = False
        Me.MembershipListView.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Tag = "ColumnHeader1"
        resources.ApplyResources(Me.ColumnHeader1, "ColumnHeader1")
        '
        'PictureBox3
        '
        resources.ApplyResources(Me.PictureBox3, "PictureBox3")
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.TabStop = False
        '
        'PictureBox1
        '
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'txt_last_reboot
        '
        Me.txt_last_reboot.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_last_reboot.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_last_reboot, "txt_last_reboot")
        Me.txt_last_reboot.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_last_reboot.Name = "txt_last_reboot"
        Me.txt_last_reboot.ReadOnly = True
        '
        'txt_language
        '
        Me.txt_language.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_language.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_language, "txt_language")
        Me.txt_language.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_language.Name = "txt_language"
        Me.txt_language.ReadOnly = True
        '
        'txt_img_ver
        '
        Me.txt_img_ver.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_img_ver.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_img_ver, "txt_img_ver")
        Me.txt_img_ver.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_img_ver.Name = "txt_img_ver"
        Me.txt_img_ver.ReadOnly = True
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.Name = "Label9"
        '
        'txt_VerImg
        '
        resources.ApplyResources(Me.txt_VerImg, "txt_VerImg")
        Me.txt_VerImg.Name = "txt_VerImg"
        '
        'txt_IP
        '
        Me.txt_IP.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_IP.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_IP, "txt_IP")
        Me.txt_IP.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_IP.Name = "txt_IP"
        Me.txt_IP.ReadOnly = True
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'txt_TypePC
        '
        Me.txt_TypePC.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_TypePC.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_TypePC, "txt_TypePC")
        Me.txt_TypePC.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_TypePC.Name = "txt_TypePC"
        Me.txt_TypePC.ReadOnly = True
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'txt_OSCaption
        '
        Me.txt_OSCaption.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_OSCaption.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_OSCaption, "txt_OSCaption")
        Me.txt_OSCaption.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_OSCaption.Name = "txt_OSCaption"
        Me.txt_OSCaption.ReadOnly = True
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'txt_img_install_Date
        '
        Me.txt_img_install_Date.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_img_install_Date.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_img_install_Date, "txt_img_install_Date")
        Me.txt_img_install_Date.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_img_install_Date.Name = "txt_img_install_Date"
        Me.txt_img_install_Date.ReadOnly = True
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        '
        'txt_CPU
        '
        Me.txt_CPU.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_CPU.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_CPU, "txt_CPU")
        Me.txt_CPU.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_CPU.Name = "txt_CPU"
        Me.txt_CPU.ReadOnly = True
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'txt_RAM
        '
        Me.txt_RAM.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_RAM.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_RAM, "txt_RAM")
        Me.txt_RAM.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_RAM.Name = "txt_RAM"
        Me.txt_RAM.ReadOnly = True
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'txt_Version
        '
        Me.txt_Version.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_Version.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_Version, "txt_Version")
        Me.txt_Version.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_Version.Name = "txt_Version"
        Me.txt_Version.ReadOnly = True
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'txt_Name
        '
        Me.txt_Name.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_Name.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_Name, "txt_Name")
        Me.txt_Name.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_Name.Name = "txt_Name"
        Me.txt_Name.ReadOnly = True
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'txt_Vendor
        '
        Me.txt_Vendor.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_Vendor.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_Vendor, "txt_Vendor")
        Me.txt_Vendor.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_Vendor.Name = "txt_Vendor"
        Me.txt_Vendor.ReadOnly = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'txt_Domain
        '
        Me.txt_Domain.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_Domain.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_Domain, "txt_Domain")
        Me.txt_Domain.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_Domain.Name = "txt_Domain"
        Me.txt_Domain.ReadOnly = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'txt_ComputerName
        '
        Me.txt_ComputerName.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_ComputerName.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_ComputerName, "txt_ComputerName")
        Me.txt_ComputerName.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_ComputerName.Name = "txt_ComputerName"
        Me.txt_ComputerName.ReadOnly = True
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.Name = "Label12"
        '
        'PC_Info
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
        Me.Name = "PC_Info"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TT As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Version As System.Windows.Forms.Label
    Friend WithEvents lbl_loading As System.Windows.Forms.Label
    Friend WithEvents txt_ComputerName As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txt_Name As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_Vendor As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_Domain As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt_Version As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_RAM As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_CPU As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_img_install_Date As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txt_TypePC As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txt_OSCaption As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt_IP As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txt_last_reboot As System.Windows.Forms.TextBox
    Friend WithEvents txt_language As System.Windows.Forms.TextBox
    Friend WithEvents txt_img_ver As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txt_VerImg As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents cmd_manage As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents MembershipListView As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents txt_SRU_Verimg As System.Windows.Forms.TextBox
    Friend WithEvents lbl_SRUVerimg As System.Windows.Forms.Label

End Class

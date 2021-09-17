<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Start_Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main_Start_Form))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.Menu_About = New System.Windows.Forms.ToolStripMenuItem()
        Me.ABOUTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.USERGUIDEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Option = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Francais = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_English = New System.Windows.Forms.ToolStripMenuItem()
        Me.pic_Ok = New System.Windows.Forms.PictureBox()
        Me.cmd_Check = New System.Windows.Forms.Button()
        Me.txt_PCName = New System.Windows.Forms.TextBox()
        Me.pic_rightArrow = New System.Windows.Forms.PictureBox()
        Me.pic_notOk = New System.Windows.Forms.PictureBox()
        Me.LBL_Main_Start_Form_1 = New System.Windows.Forms.Label()
        Me.cmd_Quit = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_Version = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DirectoryEntry1 = New System.DirectoryServices.DirectoryEntry()
        Me.DirectorySearcher1 = New System.DirectoryServices.DirectorySearcher()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.pic_Ok, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_rightArrow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_notOk, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem
        Me.MenuStrip1.BackColor = System.Drawing.Color.AliceBlue
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_About, Me.Menu_Option})
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.TabStop = True
        '
        'Menu_About
        '
        resources.ApplyResources(Me.Menu_About, "Menu_About")
        Me.Menu_About.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem
        Me.Menu_About.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Menu_About.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ABOUTToolStripMenuItem, Me.USERGUIDEToolStripMenuItem})
        Me.Menu_About.Name = "Menu_About"
        '
        'ABOUTToolStripMenuItem
        '
        Me.ABOUTToolStripMenuItem.Name = "ABOUTToolStripMenuItem"
        resources.ApplyResources(Me.ABOUTToolStripMenuItem, "ABOUTToolStripMenuItem")
        '
        'USERGUIDEToolStripMenuItem
        '
        Me.USERGUIDEToolStripMenuItem.Name = "USERGUIDEToolStripMenuItem"
        resources.ApplyResources(Me.USERGUIDEToolStripMenuItem, "USERGUIDEToolStripMenuItem")
        '
        'Menu_Option
        '
        resources.ApplyResources(Me.Menu_Option, "Menu_Option")
        Me.Menu_Option.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem
        Me.Menu_Option.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Menu_Option.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_Francais, Me.Menu_English})
        Me.Menu_Option.Name = "Menu_Option"
        '
        'Menu_Francais
        '
        Me.Menu_Francais.Name = "Menu_Francais"
        resources.ApplyResources(Me.Menu_Francais, "Menu_Francais")
        '
        'Menu_English
        '
        Me.Menu_English.Name = "Menu_English"
        resources.ApplyResources(Me.Menu_English, "Menu_English")
        '
        'pic_Ok
        '
        resources.ApplyResources(Me.pic_Ok, "pic_Ok")
        Me.pic_Ok.Name = "pic_Ok"
        Me.pic_Ok.TabStop = False
        '
        'cmd_Check
        '
        resources.ApplyResources(Me.cmd_Check, "cmd_Check")
        Me.cmd_Check.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonMenu
        Me.cmd_Check.Name = "cmd_Check"
        Me.cmd_Check.UseVisualStyleBackColor = True
        '
        'txt_PCName
        '
        Me.txt_PCName.AccessibleRole = System.Windows.Forms.AccessibleRole.Text
        Me.txt_PCName.BackColor = System.Drawing.Color.GhostWhite
        resources.ApplyResources(Me.txt_PCName, "txt_PCName")
        Me.txt_PCName.Name = "txt_PCName"
        '
        'pic_rightArrow
        '
        resources.ApplyResources(Me.pic_rightArrow, "pic_rightArrow")
        Me.pic_rightArrow.Name = "pic_rightArrow"
        Me.pic_rightArrow.TabStop = False
        '
        'pic_notOk
        '
        resources.ApplyResources(Me.pic_notOk, "pic_notOk")
        Me.pic_notOk.Name = "pic_notOk"
        Me.pic_notOk.TabStop = False
        '
        'LBL_Main_Start_Form_1
        '
        resources.ApplyResources(Me.LBL_Main_Start_Form_1, "LBL_Main_Start_Form_1")
        Me.LBL_Main_Start_Form_1.Name = "LBL_Main_Start_Form_1"
        '
        'cmd_Quit
        '
        Me.cmd_Quit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.cmd_Quit, "cmd_Quit")
        Me.cmd_Quit.Name = "cmd_Quit"
        Me.cmd_Quit.TabStop = False
        Me.cmd_Quit.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LBL_Main_Start_Form_1)
        Me.Panel1.Controls.Add(Me.pic_notOk)
        Me.Panel1.Controls.Add(Me.pic_Ok)
        Me.Panel1.Controls.Add(Me.pic_rightArrow)
        Me.Panel1.Controls.Add(Me.cmd_Check)
        Me.Panel1.Controls.Add(Me.txt_PCName)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'lbl_Version
        '
        resources.ApplyResources(Me.lbl_Version, "lbl_Version")
        Me.lbl_Version.Name = "lbl_Version"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.Color.DarkRed
        Me.Label1.Name = "Label1"
        '
        'DirectorySearcher1
        '
        Me.DirectorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01")
        '
        'Main_Start_Form
        '
        Me.AcceptButton = Me.cmd_Check
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.CancelButton = Me.cmd_Quit
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbl_Version)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cmd_Quit)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Main_Start_Form"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.pic_Ok, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_rightArrow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_notOk, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents Menu_Option As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Francais As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_English As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_About As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmd_Check As System.Windows.Forms.Button
    Friend WithEvents txt_PCName As System.Windows.Forms.TextBox
    Friend WithEvents pic_rightArrow As System.Windows.Forms.PictureBox
    Friend WithEvents pic_notOk As System.Windows.Forms.PictureBox
    Friend WithEvents LBL_Main_Start_Form_1 As System.Windows.Forms.Label
    Friend WithEvents cmd_Quit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Version As System.Windows.Forms.Label
    Friend WithEvents pic_Ok As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ABOUTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents USERGUIDEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DirectoryEntry1 As System.DirectoryServices.DirectoryEntry
    Friend WithEvents DirectorySearcher1 As System.DirectoryServices.DirectorySearcher
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Pack_Apps

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pack_Apps))
        Me.TT = New System.Windows.Forms.ToolTip(Me.components)
        Me.Tab_pkg_app = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader18 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader19 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader20 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader21 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ListView3 = New System.Windows.Forms.ListView()
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.ListView4 = New System.Windows.Forms.ListView()
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.ListView5 = New System.Windows.Forms.ListView()
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pic_arrow = New System.Windows.Forms.PictureBox()
        Me.lbl_Version = New System.Windows.Forms.Label()
        Me.lbl_loading = New System.Windows.Forms.Label()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmd_apps_refresh = New System.Windows.Forms.Button()
        Me.Tab_pkg_app.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        CType(Me.pic_arrow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TT
        '
        Me.TT.AutoPopDelay = 5000
        Me.TT.InitialDelay = 500
        Me.TT.IsBalloon = True
        Me.TT.ReshowDelay = 5
        '
        'Tab_pkg_app
        '
        Me.Tab_pkg_app.Controls.Add(Me.TabPage1)
        Me.Tab_pkg_app.Controls.Add(Me.TabPage2)
        Me.Tab_pkg_app.Controls.Add(Me.TabPage3)
        Me.Tab_pkg_app.Controls.Add(Me.TabPage4)
        Me.Tab_pkg_app.Controls.Add(Me.TabPage5)
        Me.Tab_pkg_app.Controls.Add(Me.TabPage6)
        resources.ApplyResources(Me.Tab_pkg_app, "Tab_pkg_app")
        Me.Tab_pkg_app.Name = "Tab_pkg_app"
        Me.Tab_pkg_app.SelectedIndex = 0
        Me.TT.SetToolTip(Me.Tab_pkg_app, resources.GetString("Tab_pkg_app.ToolTip"))
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ListView1)
        resources.ApplyResources(Me.TabPage1, "TabPage1")
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
        resources.ApplyResources(Me.ListView1, "ListView1")
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.HideSelection = False
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        resources.ApplyResources(Me.ColumnHeader1, "ColumnHeader1")
        '
        'ColumnHeader2
        '
        resources.ApplyResources(Me.ColumnHeader2, "ColumnHeader2")
        '
        'ColumnHeader3
        '
        resources.ApplyResources(Me.ColumnHeader3, "ColumnHeader3")
        '
        'ColumnHeader4
        '
        resources.ApplyResources(Me.ColumnHeader4, "ColumnHeader4")
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ListView2)
        resources.ApplyResources(Me.TabPage2, "TabPage2")
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ListView2
        '
        Me.ListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader17, Me.ColumnHeader18, Me.ColumnHeader19, Me.ColumnHeader20, Me.ColumnHeader21})
        resources.ApplyResources(Me.ListView2, "ListView2")
        Me.ListView2.FullRowSelect = True
        Me.ListView2.GridLines = True
        Me.ListView2.HideSelection = False
        Me.ListView2.MultiSelect = False
        Me.ListView2.Name = "ListView2"
        Me.ListView2.UseCompatibleStateImageBehavior = False
        Me.ListView2.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        resources.ApplyResources(Me.ColumnHeader5, "ColumnHeader5")
        '
        'ColumnHeader6
        '
        resources.ApplyResources(Me.ColumnHeader6, "ColumnHeader6")
        '
        'ColumnHeader17
        '
        resources.ApplyResources(Me.ColumnHeader17, "ColumnHeader17")
        '
        'ColumnHeader18
        '
        resources.ApplyResources(Me.ColumnHeader18, "ColumnHeader18")
        '
        'ColumnHeader19
        '
        resources.ApplyResources(Me.ColumnHeader19, "ColumnHeader19")
        '
        'ColumnHeader20
        '
        resources.ApplyResources(Me.ColumnHeader20, "ColumnHeader20")
        '
        'ColumnHeader21
        '
        resources.ApplyResources(Me.ColumnHeader21, "ColumnHeader21")
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ListView3)
        resources.ApplyResources(Me.TabPage3, "TabPage3")
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ListView3
        '
        Me.ListView3.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        resources.ApplyResources(Me.ListView3, "ListView3")
        Me.ListView3.FullRowSelect = True
        Me.ListView3.GridLines = True
        Me.ListView3.HideSelection = False
        Me.ListView3.MultiSelect = False
        Me.ListView3.Name = "ListView3"
        Me.ListView3.UseCompatibleStateImageBehavior = False
        Me.ListView3.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader7
        '
        resources.ApplyResources(Me.ColumnHeader7, "ColumnHeader7")
        '
        'ColumnHeader8
        '
        resources.ApplyResources(Me.ColumnHeader8, "ColumnHeader8")
        '
        'ColumnHeader9
        '
        resources.ApplyResources(Me.ColumnHeader9, "ColumnHeader9")
        '
        'ColumnHeader10
        '
        resources.ApplyResources(Me.ColumnHeader10, "ColumnHeader10")
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.ListView4)
        resources.ApplyResources(Me.TabPage4, "TabPage4")
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'ListView4
        '
        Me.ListView4.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader11, Me.ColumnHeader12, Me.ColumnHeader13})
        resources.ApplyResources(Me.ListView4, "ListView4")
        Me.ListView4.FullRowSelect = True
        Me.ListView4.GridLines = True
        Me.ListView4.HideSelection = False
        Me.ListView4.MultiSelect = False
        Me.ListView4.Name = "ListView4"
        Me.ListView4.UseCompatibleStateImageBehavior = False
        Me.ListView4.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader11
        '
        resources.ApplyResources(Me.ColumnHeader11, "ColumnHeader11")
        '
        'ColumnHeader12
        '
        resources.ApplyResources(Me.ColumnHeader12, "ColumnHeader12")
        '
        'ColumnHeader13
        '
        resources.ApplyResources(Me.ColumnHeader13, "ColumnHeader13")
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.ListView5)
        resources.ApplyResources(Me.TabPage5, "TabPage5")
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'ListView5
        '
        Me.ListView5.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader14, Me.ColumnHeader15, Me.ColumnHeader16})
        resources.ApplyResources(Me.ListView5, "ListView5")
        Me.ListView5.FullRowSelect = True
        Me.ListView5.GridLines = True
        Me.ListView5.HideSelection = False
        Me.ListView5.MultiSelect = False
        Me.ListView5.Name = "ListView5"
        Me.ListView5.UseCompatibleStateImageBehavior = False
        Me.ListView5.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader14
        '
        resources.ApplyResources(Me.ColumnHeader14, "ColumnHeader14")
        '
        'ColumnHeader15
        '
        resources.ApplyResources(Me.ColumnHeader15, "ColumnHeader15")
        '
        'ColumnHeader16
        '
        resources.ApplyResources(Me.ColumnHeader16, "ColumnHeader16")
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.Label2)
        Me.TabPage6.Controls.Add(Me.pic_arrow)
        resources.ApplyResources(Me.TabPage6, "TabPage6")
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'pic_arrow
        '
        resources.ApplyResources(Me.pic_arrow, "pic_arrow")
        Me.pic_arrow.Name = "pic_arrow"
        Me.pic_arrow.TabStop = False
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
        'ProgressBar
        '
        resources.ApplyResources(Me.ProgressBar, "ProgressBar")
        Me.ProgressBar.Name = "ProgressBar"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'cmd_apps_refresh
        '
        resources.ApplyResources(Me.cmd_apps_refresh, "cmd_apps_refresh")
        Me.cmd_apps_refresh.Name = "cmd_apps_refresh"
        Me.cmd_apps_refresh.UseVisualStyleBackColor = True
        '
        'Pack_Apps
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Tab_pkg_app)
        Me.Controls.Add(Me.lbl_loading)
        Me.Controls.Add(Me.lbl_Version)
        Me.Controls.Add(Me.cmd_apps_refresh)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "Pack_Apps"
        Me.Tab_pkg_app.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        CType(Me.pic_arrow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TT As System.Windows.Forms.ToolTip
    Friend WithEvents lbl_Version As System.Windows.Forms.Label
    Friend WithEvents lbl_loading As System.Windows.Forms.Label
    Friend WithEvents Tab_pkg_app As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ListView3 As System.Windows.Forms.ListView
    Friend WithEvents ListView4 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents ListView5 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmd_apps_refresh As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pic_arrow As System.Windows.Forms.PictureBox
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader21 As System.Windows.Forms.ColumnHeader

End Class

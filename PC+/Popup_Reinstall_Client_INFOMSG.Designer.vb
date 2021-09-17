<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Popup_Reinstall_Client_INFOMSG

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Popup_Reinstall_Client_INFOMSG))
        Me.TT = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txt_remote_time = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmd_ok = New System.Windows.Forms.Button()
        Me.txt_end = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_Start_Install = New System.Windows.Forms.TextBox()
        Me.txt_start_copie = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
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
        'Panel1
        '
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.BackColor = System.Drawing.Color.IndianRed
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txt_remote_time)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.cmd_ok)
        Me.Panel1.Controls.Add(Me.txt_end)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.txt_Start_Install)
        Me.Panel1.Controls.Add(Me.txt_start_copie)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Name = "Panel1"
        Me.TT.SetToolTip(Me.Panel1, resources.GetString("Panel1.ToolTip"))
        '
        'txt_remote_time
        '
        resources.ApplyResources(Me.txt_remote_time, "txt_remote_time")
        Me.txt_remote_time.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_remote_time.Name = "txt_remote_time"
        Me.txt_remote_time.ReadOnly = True
        Me.TT.SetToolTip(Me.txt_remote_time, resources.GetString("txt_remote_time.ToolTip"))
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Name = "Label6"
        Me.TT.SetToolTip(Me.Label6, resources.GetString("Label6.ToolTip"))
        '
        'cmd_ok
        '
        resources.ApplyResources(Me.cmd_ok, "cmd_ok")
        Me.cmd_ok.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmd_ok.Name = "cmd_ok"
        Me.TT.SetToolTip(Me.cmd_ok, resources.GetString("cmd_ok.ToolTip"))
        Me.cmd_ok.UseVisualStyleBackColor = True
        '
        'txt_end
        '
        resources.ApplyResources(Me.txt_end, "txt_end")
        Me.txt_end.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_end.Name = "txt_end"
        Me.txt_end.ReadOnly = True
        Me.TT.SetToolTip(Me.txt_end, resources.GetString("txt_end.ToolTip"))
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Name = "Label2"
        Me.TT.SetToolTip(Me.Label2, resources.GetString("Label2.ToolTip"))
        '
        'txt_Start_Install
        '
        resources.ApplyResources(Me.txt_Start_Install, "txt_Start_Install")
        Me.txt_Start_Install.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_Start_Install.Name = "txt_Start_Install"
        Me.txt_Start_Install.ReadOnly = True
        Me.TT.SetToolTip(Me.txt_Start_Install, resources.GetString("txt_Start_Install.ToolTip"))
        '
        'txt_start_copie
        '
        resources.ApplyResources(Me.txt_start_copie, "txt_start_copie")
        Me.txt_start_copie.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_start_copie.Name = "txt_start_copie"
        Me.txt_start_copie.ReadOnly = True
        Me.TT.SetToolTip(Me.txt_start_copie, resources.GetString("txt_start_copie.ToolTip"))
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Name = "Label1"
        Me.TT.SetToolTip(Me.Label1, resources.GetString("Label1.ToolTip"))
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Name = "Label5"
        Me.TT.SetToolTip(Me.Label5, resources.GetString("Label5.ToolTip"))
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Name = "Label3"
        Me.TT.SetToolTip(Me.Label3, resources.GetString("Label3.ToolTip"))
        '
        'Popup_Reinstall_Client_INFOMSG
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.DarkRed
        Me.CancelButton = Me.cmd_ok
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Popup_Reinstall_Client_INFOMSG"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.TT.SetToolTip(Me, resources.GetString("$this.ToolTip"))
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TT As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_end As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_Start_Install As System.Windows.Forms.TextBox
    Friend WithEvents txt_start_copie As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmd_ok As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_remote_time As System.Windows.Forms.TextBox

End Class

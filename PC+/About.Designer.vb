<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
    Inherits PC_.LocalizedForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.TextCopyright = New System.Windows.Forms.TextBox()
        Me.Lbl_CreePar = New System.Windows.Forms.Label()
        Me.TextVersion = New System.Windows.Forms.TextBox()
        Me.Lbl_Version = New System.Windows.Forms.Label()
        Me.TextProductName = New System.Windows.Forms.TextBox()
        Me.lbl_Product = New System.Windows.Forms.Label()
        Me.Text_BoxDescription = New System.Windows.Forms.TextBox()
        Me.Label_CompanyName = New System.Windows.Forms.Label()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextCopyright
        '
        resources.ApplyResources(Me.TextCopyright, "TextCopyright")
        Me.TextCopyright.Name = "TextCopyright"
        Me.TextCopyright.ReadOnly = True
        '
        'Lbl_CreePar
        '
        resources.ApplyResources(Me.Lbl_CreePar, "Lbl_CreePar")
        Me.Lbl_CreePar.Name = "Lbl_CreePar"
        '
        'TextVersion
        '
        resources.ApplyResources(Me.TextVersion, "TextVersion")
        Me.TextVersion.Name = "TextVersion"
        Me.TextVersion.ReadOnly = True
        '
        'Lbl_Version
        '
        resources.ApplyResources(Me.Lbl_Version, "Lbl_Version")
        Me.Lbl_Version.Name = "Lbl_Version"
        '
        'TextProductName
        '
        resources.ApplyResources(Me.TextProductName, "TextProductName")
        Me.TextProductName.Name = "TextProductName"
        Me.TextProductName.ReadOnly = True
        '
        'lbl_Product
        '
        resources.ApplyResources(Me.lbl_Product, "lbl_Product")
        Me.lbl_Product.Name = "lbl_Product"
        '
        'Text_BoxDescription
        '
        resources.ApplyResources(Me.Text_BoxDescription, "Text_BoxDescription")
        Me.Text_BoxDescription.Name = "Text_BoxDescription"
        Me.Text_BoxDescription.ReadOnly = True
        '
        'Label_CompanyName
        '
        resources.ApplyResources(Me.Label_CompanyName, "Label_CompanyName")
        Me.Label_CompanyName.Name = "Label_CompanyName"
        '
        'OKButton
        '
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.OKButton, "OKButton")
        Me.OKButton.Name = "OKButton"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'About
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TextCopyright)
        Me.Controls.Add(Me.Lbl_CreePar)
        Me.Controls.Add(Me.TextVersion)
        Me.Controls.Add(Me.Lbl_Version)
        Me.Controls.Add(Me.TextProductName)
        Me.Controls.Add(Me.lbl_Product)
        Me.Controls.Add(Me.Text_BoxDescription)
        Me.Controls.Add(Me.Label_CompanyName)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "About"
        Me.ShowInTaskbar = False
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextCopyright As System.Windows.Forms.TextBox
    Friend WithEvents Lbl_CreePar As System.Windows.Forms.Label
    Friend WithEvents TextVersion As System.Windows.Forms.TextBox
    Friend WithEvents Lbl_Version As System.Windows.Forms.Label
    Friend WithEvents TextProductName As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Product As System.Windows.Forms.Label
    Friend WithEvents Text_BoxDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label_CompanyName As System.Windows.Forms.Label
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox

End Class

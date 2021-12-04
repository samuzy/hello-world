Imports System.Globalization
Imports System.Reflection
Imports System.ComponentModel
Imports System.Text
Imports System.Text.RegularExpressions

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'      Cet forme est le module de démarrage par défaut du programme SCCM PC Admin il permet de saisir le nom de l'ordinateur et de choisir la langue d'affichage 
'      désirer, mais par défaut l'application sélection la langue en fonction de la langue du PC en cours
'
' *******************************************************************************************************************************************************

Public Class Main_Start_Form
    Inherits LocalizedForm

#Region "Singleton"
    Private Shared _instance As Main_Start_Form

    Public Shared ReadOnly Property Instance As Main_Start_Form
        Get
            If _instance Is Nothing Then
                _instance = New Main_Start_Form
            End If
            Return _instance
        End Get
    End Property

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If _instance Is Nothing Then
            ' TODO: Fix this violation of the singleton pattern, needed by the Application Framework Startup Form feature
            _instance = Me
        End If
    End Sub
#End Region

    Private Sub Main_Start_Form_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Main.Instance.Show()
        'Me.Close()

        'Mise par défault les Valeurs de démarrage
        PC_Status = False
        pic_Ok.Visible = False
        pic_notOk.Visible = False
        pic_rightArrow.Visible = True
        Err_Services_Acces = False
        Err_RemoteRegistry_Acces = False
        Err_MPSSVC_Acces = False
        Err_CCMEXEC_Acces = False
        Err_BITS_Acces = False
        Err_PeerDistSvc_Acces = False
        Err_wuauserv_Acces = False

        'recupere le nom du pc ou est exécuter l'application
        If ComputerName = "" Then ComputerName = System.Net.Dns.GetHostName.Trim
        txt_PCName.Text = ComputerName

        'vérifie la langue tu poste de travail pour afficher le programme dans la bonne langue au démarrage

        If CurrentCulture.Name() = "fr-CA" Or CurrentCulture.Name() = "fr-FR" Then
            GlobalUICulture = New CultureInfo("fr-CA")
        Else
            GlobalUICulture = New CultureInfo("en-CA")
        End If

        ResetLanguageMenuItems()
        ResetVersion()

        'Vérification de l'utilistateur connecter
        Username = RemoteUser.GetUserName()

        'Valide le fichier INI soit la 
        If CheckFileExists(INI_Files) Then
            'do this if is true

        Else
            'do that if is False
            MsgBox(My.Resources.Missing_INI_Files, MsgBoxStyle.Critical)
            Close()
        End If

        '''' WE don't need this popupa anymore other than to load some resorces, show the main form and close the pop-up immediately
        Main.Instance.Show()
        Me.Close()

    End Sub

    Protected Overrides Sub ApplyResources()
        MyBase.ApplyResources()
        ResetVersion()
    End Sub

    Private Sub ResetVersion()
        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)
    End Sub

    Private Sub Main_Start_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'exécuter lorsque la fenetre est fermer par le X
        _instance = Nothing
    End Sub

    Private Sub cmd_Quit_Click(sender As Object, e As EventArgs) Handles cmd_Quit.Click
        'Bouton cacher dans le Haut Gauche de l'écran pour activer la touche ESC
        Me.Close()
    End Sub

    Private Sub UILanguage_Click(sender As Object, e As EventArgs) Handles Menu_Francais.Click, Menu_English.Click
        Dim cultureName As String = GlobalUICulture.Name

        If sender.Equals(Menu_Francais) Then
            cultureName = "fr-CA"
        ElseIf sender.Equals(Menu_English) Then
            cultureName = "en-CA"
        End If

        If cultureName <> GlobalUICulture.Name Then
            GlobalUICulture = New CultureInfo(cultureName)
            ResetLanguageMenuItems()
            Me.Refresh()
        End If
    End Sub

    Private Sub cmd_Check_Click(sender As Object, e As EventArgs) Handles cmd_Check.Click

        'verify la fonction DEBUG
        If txt_PCName.Text = "DEBUG" Then
            DEBUG.ShowDialog()
            Exit Sub
        End If

        'Active le message de chargement
        Me.Label1.Visible = True

        'Active le chagement de la souris en mode attente
        Me.Cursor = Cursors.WaitCursor

        ComputerName = Trim(txt_PCName.Text)

        'corrige le bug du CHAR invisible a la fin quand on entre une IP
        ComputerName = Regex.Replace(ComputerName, "[^a-zA-Z0-9.]", "")

        If ComputerName = "" Then
            MsgBox(My.Resources.ConfirmComputerName, MsgBoxStyle.Critical)
            Me.Label1.Visible = False
            Me.Cursor = Cursors.Default
            txt_PCName.Select()
            Exit Sub
        End If

        User = ""

        'Vérification si le PC est "Online"
        PCName = New PC_CheckDevice()
        Main.Instance.Text = "SCCM PC Admin " & ComputerName
        Main.Instance.Show()

        If PC_Status = True Then
            Me.Close()
        Else
            pic_rightArrow.Visible = True
            pic_notOk.Visible = False
            Me.Label1.Visible = False

        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub ResetLanguageMenuItems()
        Menu_English.Checked = GlobalUICulture.Name = "en-CA"
        Menu_Francais.Checked = GlobalUICulture.Name = "fr-CA"

        'Menu
        Me.ABOUTToolStripMenuItem.Text = My.Resources.ToolStripMenuItem_About
        Me.USERGUIDEToolStripMenuItem.Text = My.Resources.ToolStripMenuItem_UserGuide

    End Sub

    Private Sub ABOUTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ABOUTToolStripMenuItem.Click
        Dim aboutForm As About = New About
        aboutForm.ShowDialog()
    End Sub

    Private Sub USERGUIDEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles USERGUIDEToolStripMenuItem.Click

        Const WebPageFR = "http://dialogue/grp/DS-SD/Shared%20Documents/Guides%20de%20l'utilisateur%20-%20User%20Guides/Guide%20d’utilisation%20de%20SCCM%20PC%20Admin_F.docx"
        Const WebPageEN = "http://dialogue/grp/DS-SD/Shared%20Documents/Guides%20de%20l'utilisateur%20-%20User%20Guides/SCCM%20PC%20Admin%20-%20User%20Guide_E.docx"

        Select Case GlobalUICulture.Name
            Case "fr-CA"
                Process.Start(WebPageFR)

            Case "en-CA"
                Process.Start(WebPageEN)
        End Select

    End Sub

End Class

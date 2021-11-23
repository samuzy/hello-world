
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Management
Imports System.Reflection
Imports System.ServiceProcess
Imports System.Threading
Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports Microsoft.Win32
Imports System.DirectoryServices.AccountManagement
Imports System.Text


' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'       Module Principal du programme meilleur résolution minimum est 1024*768 avec une DPI de 100%
' *******************************************************************************************************************************************************

Public Class Main
    Inherits LocalizedForm

#Region "Singleton"
    Private Shared _instance As Main
    Private lastCol As Integer = 0
    Private Tab_Select As Integer = 0
    Private loadExecutionPKGSTab As Integer = 0
    Private loadExecutionAPPSTab As Integer = 0
    Private loadRunningPKGSTab As Integer = 0
    Private loadAdvertisementsTab As Integer = 0
    Private loadInfoTab As Integer = 0
    Private loadServiceWindows As Integer = 0
    Private loadRunningWSUS_SCUP As Integer = 0
    Private loadProgramsAndFeaturesTab As Integer = 0
    Private loadSoftwareCacheTab As Integer = 0
    Private WithEvents MyProcess As Process
    Private Delegate Sub AppendOutputTextDelegate(ByVal text As String)
    Dim strComputer, strRService, strSName
    Const Adv_Clean_boot = 0   'ADS_SERVICE_BOOT_START
    Const Adv_Clean_system = 1   'ADS_SERVICE_SYSTEM_START
    Const Adv_Clean_auto = 2   'ADS_SERVICE_AUTO_START
    Const Adv_Clean_manual = 3   'ADS_SERVICE_DEMAND_START
    Const Adv_Clean_disabled = 4   'ADS_SERVICE_DISABLED
    Const strKeyPath = "SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate"
    Dim Check1 As Boolean = False
    Dim Check2 As Boolean = False
    Dim Check3 As Boolean = False
    Dim Check4 As Boolean = False
    Dim Check5 As Boolean = False
    Dim Check6 As Boolean = False
    Dim strMessage, strErrorMessage
    Dim strProgPath, strCommandLine, strArgs
    Dim quote, squote, command, errReturn
    Dim oCP, oService, strServiceState, strStartType, oShellExec
    Dim oReg, bSkipService, myCMDLine, myCMDLine2
    Dim bFirst As Boolean = False
    Dim fso = CreateObject("Scripting.FileSystemObject")
    Dim All As Boolean = False
    Dim myPwd = "HolyshiT2020!"


    Public Shared ReadOnly Property Instance() As Main
        Get
            If _instance Is Nothing Then
                _instance = New Main
            End If
            Return _instance
        End Get
    End Property

    Private Sub New()
        MyBase.New()
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ResetLanguage()
    End Sub
#End Region

#Region "LocalizedForm"

    Protected Overrides Sub ApplyResources()
        MyBase.ApplyResources()

        If User.Trim = String.Empty Then
            txtLoggedIn_NEW.Text = My.Resources.txt_logged_no_user
        End If

        ResetLanguage()
        ResetServiceWindows()
        ResetVersion()
    End Sub

    Private Sub ResetLanguage()

        If OSLanguage = "1036" Then
            txt_language_NEW.Text = My.Resources.txt_language_text_fr
        ElseIf OSLanguage = "1033" Then
            txt_language_NEW.Text = My.Resources.txt_language_text_en
        End If

        If m_strChassisTypes = "MOBILE_DEVICE" Then
            txt_ADSite_NEW.Text = My.Resources.txt_TypePC_text_mobile
        ElseIf m_strChassisTypes = "DESKTOP" Then
            txt_ADSite_NEW.Text = My.Resources.txt_TypePC_text_desktop
        Else
            txt_ADSite_NEW.Text = m_strChassisTypes
        End If

        txt_img_install_Date.Text = WMIDateConvert(str_InstallDate)
        txt_last_reboot_NEW.Text = WMIDateConvert(str_LastBootUpTime)

    End Sub

    Private Sub ResetLanguageMenuItems()
        Menu_English.Checked = GlobalUICulture.Name = "en-CA"
        Menu_Francais.Checked = GlobalUICulture.Name = "fr-CA"

        'Menu
        Me.AboutToolStripMenuItem.Text = My.Resources.ToolStripMenuItem_About
        Me.UserGuideToolStripMenuItem.Text = My.Resources.ToolStripMenuItem_UserGuide

    End Sub

    Private Sub ResetVersion()
        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)
    End Sub

#End Region

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        _instance = Nothing
    End Sub

    Private Sub Main_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        'CRTL+1 = Remote Logs
        If (e.KeyCode = Keys.D1 AndAlso e.Modifiers = Keys.Control) Then
            Try
                Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\Windows\CCM\Logs")
            Catch ex As Exception
                ' Gestion de l'erreur
            End Try
        End If

        'CRTL+E = Explorer++
        If (e.KeyCode = Keys.E AndAlso e.Modifiers = Keys.Control) Then
            Try
                Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$")
            Catch ex As Exception
                ' Gestion de l'erreur
            End Try
        End If

        'CRTL+M = Manage PC
        If (e.KeyCode = Keys.M AndAlso e.Modifiers = Keys.Control) Then
            Try
                Process.Start("c:\windows\system32\compmgmt.msc", "/computer:\\" & ComputerName)
            Catch ex As Exception
                ' Gestion de l'erreur
            End Try
        End If

    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        KeyPreview = True

        'Active le loading de la forme actuel
        Me.lbl_loading.Visible = True

        'Active le chagement de la souris en mode attente
        Me.Cursor = Cursors.WaitCursor

        'Masque par default l'icone VPN
        'Me.Pic_VPN.Visible = False

        ResetVersion()
        Affichage_Defaut()
        Connexion()

        'Remet le cursor en mode defaut
        Me.Cursor = Cursors.Default

        ResetLanguageMenuItems()
        Me.ServiceWindowsListView.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
        Me.txt_PCName_NEW.Select()
        btnCenterConsole2.Hide() ' need to hide this one at startup
        InitialLoadingSetLayoutDefaults()
    End Sub

    Private Sub InitialLoadingSetLayoutDefaults()
        'Throw New NotImplementedException()
    End Sub

    Friend Sub Connexion()

        ' clear all controls first
        ResetControls()
        Me.Cursor = Cursors.WaitCursor
        If Advance_mode = True Or User.Contains("saadi") Then
            Me.AdvancedMode_Menu.Visible = True
            AdvancedModeTab.Visible = True
        Else
            Me.AdvancedMode_Menu.Visible = False
            AdvancedModeTab.Visible = False
        End If

        'Vérification si le PC est "Online"
        ComputerName = Trim(txt_PCName_NEW.Text)
        'corrige le bug du CHAR invisible a la fin quand on entre une IP
        ComputerName = Regex.Replace(ComputerName, "[^a-zA-Z0-9.]", "")

        If Reboot_Send = True Then
            Reboot_Send = False
            ComputerName = ""
            Me.Cursor = Cursors.Default
            Exit Sub
        Else
            If ComputerName = "" Then
                ComputerName = "127.0.0.1"
                MsgBox(My.Resources.ConfirmComputerName, MsgBoxStyle.Critical)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If

        If CheckDNSAndIP() Then
            PCName = New PC_CheckDevice()

            Try
                PCName.Ping(ComputerName, PC_Status)
                If PC_Status = True Then
                    pic_rightArrow.Visible = True
                    pic_notOk.Visible = False
                    pic_Ok.Visible = True
                    Me.Text = "SCCM PC Admin " & ComputerName

                Else
                    pic_rightArrow.Visible = False
                    pic_notOk.Visible = True
                    pic_Ok.Visible = False
                    Me.Text = "SCCM PC Admin"

                    'Validation Du OU du PC qui ne ce retouve pas dans le OU Inactif ou Surplus
                    GetAD_Info(ComputerName)
                    If str_AD_computer = "NotFound" Then
                        MsgBox(My.Resources.ErrorUnreachableComputer, MsgBoxStyle.Critical)
                    Else
                        If str_AD_computer.Contains("Inactive") OrElse str_AD_computer.Contains("Surplus") OrElse str_AD_computer.Contains("Warehouse") Then
                            MsgBox(My.Resources.Error_BAD_AD & Chr(13) & Chr(10) & str_AD_computer, MsgBoxStyle.Critical)
                        Else
                            MsgBox(My.Resources.ErrorUnreachableComputer, MsgBoxStyle.Critical)
                        End If
                    End If

                    'ComputerName = System.Net.Dns.GetHostName.Trim
                    Main_Start_Form.Instance.txt_PCName.Text = ComputerName
                    Main_Start_Form.Instance.pic_rightArrow.Visible = False
                    Main_Start_Form.Instance.pic_notOk.Visible = True
                    Main_Start_Form.Instance.pic_Ok.Visible = False
                    Main_Start_Form.Instance.Text = "SCCM PC Admin"
                    Main_Start_Form.Instance.Show()
                    Me.Cursor = Cursors.Default
                    Main.Instance.Close()

                    Exit Sub
                End If

            Catch ex As Exception
                pic_rightArrow.Visible = False
                pic_notOk.Visible = True
                pic_Ok.Visible = False
                Me.Text = "SCCM PC Admin"

                ' En cas d'erreur il va vérifier ou est le PC 
                GetAD_Info(ComputerName)
                If str_AD_computer = "NotFound" Then
                    MsgBox(My.Resources.ErrorUnreachableComputer, MsgBoxStyle.Critical)
                Else
                    If str_AD_computer.Contains("Inactive") OrElse str_AD_computer.Contains("Surplus") OrElse str_AD_computer.Contains("Warehouse") Then
                        MsgBox(My.Resources.Error_BAD_AD & Chr(13) & Chr(10) & str_AD_computer, MsgBoxStyle.Critical)
                    Else
                        MsgBox(My.Resources.ErrorUnreachableComputer, MsgBoxStyle.Critical)
                    End If
                End If

                'ComputerName = System.Net.Dns.GetHostName.Trim
                Main_Start_Form.Instance.txt_PCName.Text = ComputerName
                Main_Start_Form.Instance.pic_rightArrow.Visible = False
                Main_Start_Form.Instance.pic_notOk.Visible = True
                Main_Start_Form.Instance.pic_Ok.Visible = False
                Main_Start_Form.Instance.Text = "SCCM PC Admin"
                Main_Start_Form.Instance.Show()
                Me.Cursor = Cursors.Default
                Main.Instance.Close()

                Exit Sub

            End Try

            'Post vérification maintenant qu on à l'assurance que le PC est à ON
            'Service RemoteRegistry  = Service du Registre à distance
            'Service que on est obliger de vérifier pour avoir acces au fonction ne peux pas etre mis uniquement dans la fonction de validation
            'Services.Service_Verification("RemoteRegistry")
            'Valide les services du PC a distance
            Services_verify()

            'Vérification si un utilisateur est connecter à distance
            RemoteUser.GetUser(ComputerName)
            If Trim(User) = "" Then
                'test un seconde fois pour etre sur que le délais de vérification est respecter
                Thread.Sleep(3000)
                RemoteUser.GetUser(ComputerName)
            End If
            If Not Trim(User) = "" Then
                txtLoggedIn_NEW.Text = User
                pic_Assitance.Cursor = Cursors.Hand
            Else
                txtLoggedIn_NEW.Text = My.Resources.txt_logged_no_user
                User = ""
                pic_Assitance.Cursor = Cursors.No
            End If

            'Fonction pour allez chercher les information de l'ordinateur au régistre
            Get_PC_Information.Get_PC_Info_REG()

            'Fonction pour allez chercher les information de l'ordinateur au régistre
            Get_PC_Information.Get_PC_Info_WMI()

            'Ajouter des valeur dans les champ requis

            txt_OSCaption_NEW.Text = OSName
            If OSName = "Microsoft Windows 10 Enterprise" Then
                lbl_img_ver_win10_NEW.Visible = True
                txt_img_ver_win10_NEW.Visible = True
                txt_img_ver_win10_NEW.Text = CORE_Image_version
            Else
                lbl_img_ver_win10_NEW.Visible = False
                txt_img_ver_win10_NEW.Visible = False
                txt_img_ver_win10_NEW.Text = ""
            End If

            txt_img_ver.Text = VerImg_data
            txt_SiteCode_result_NEW.Text = SiteCode
            txt_ManagementPoint_NEW.Text = ManagementPoint
            txt_Client_Version_Result_NEW.Text = ClientVer
            txt_SCCM_Catalogue_NEW.Text = SCCM_Catalogue_Number
            txt_WUA_NEW.Text = SCCM_WSUS_Server

            ResetLanguage()

            'Obtention de l'adresse IP si ces une adresse DNS qui est entrée
            If IsIpValid(ComputerName) = False Then
                'Va checher l'adress IP car le nom Entré et un nom DNS
                RemoteUser.IPAddress(ComputerName)
                txt_IP_NEW.Text = IPAddress_Value
            Else
                'Va checher le nom DNS ar le nom Entré et une Adresse IP
                IPAddress_Value = ComputerName
                txt_IP_NEW.Text = IPAddress_Value
                DNS_Name(ComputerName)
                txt_PCName_NEW.Text = DNS_Name_Value
                ComputerName = DNS_Name_Value
            End If

            'Validation pour l'activation du mode Avancé seulement pour le HRDC-DRHC.NET
            RemoteUser.GetGroups(Username)
            If Advance_mode = True Or User.Contains("saadi") Then
                Me.AdvancedMode_Menu.Visible = True
                AdvancedModeTab.Visible = True
            Else
                Me.AdvancedMode_Menu.Visible = False
                AdvancedModeTab.Visible = False
            End If

            'Validation si un reboot est nesséssaire
            If Need_Reboot = True Then
                txt_reboot_status.Visible = True
                pic_reboot_status.Visible = True
            Else
                txt_reboot_status.Visible = False
                pic_reboot_status.Visible = False
            End If

            'Validation Du OU du PC qui ne ce retouve pas dans le OU Inactif ou Surplus
            GetAD_Info(ComputerName)
            If str_AD_computer.Contains("Inactive") OrElse str_AD_computer.Contains("Surplus") OrElse str_AD_computer.Contains("Warehouse") Then
                If Not str_AD_computer.Contains("_STAGING") Then
                    'désactive les commandes
                    MsgBox(My.Resources.Error_BAD_AD & Chr(13) & Chr(10) & str_AD_computer, MsgBoxStyle.Critical)
                    'cmd_Event_Viewer.Enabled = False
                    'cmd_Computer_Management.Enabled = False
                    'cmd_Services.Enabled = False
                    cmd_pc_info.Enabled = False
                    cmd_pkg_apps.Enabled = False
                    cmd_SCCM_WSUS_SCUP_Approved.Enabled = False
                    cmdSoftware.Enabled = False
                    cmd_SCCM_Action.Enabled = False
                    cmd_Force_WSUS.Enabled = False
                    cmd_Force_Apps_update.Enabled = False
                    cmd_Reinstall_client.Enabled = False
                    cmd_Clear_cache_bits.Enabled = False
                    cmd_Show_SW.Enabled = False

                    pic_Reboot.Enabled = False
                    pic_Explorer.Enabled = False
                    pic_Assitance.Enabled = False
                    pic_remote.Enabled = False
                Else
                    'Active les commandes
                    'cmd_Event_Viewer.Enabled = True
                    'cmd_Computer_Management.Enabled = True
                    'cmd_Services.Enabled = True
                    cmd_pc_info.Enabled = True
                    cmd_pkg_apps.Enabled = True
                    cmd_SCCM_WSUS_SCUP_Approved.Enabled = True
                    cmdSoftware.Enabled = True
                    cmd_SCCM_Action.Enabled = True
                    cmd_Force_WSUS.Enabled = True
                    cmd_Force_Apps_update.Enabled = True
                    cmd_Reinstall_client.Enabled = True
                    cmd_Clear_cache_bits.Enabled = True
                    cmd_Show_SW.Enabled = True

                    pic_Reboot.Enabled = True
                    pic_Explorer.Enabled = True
                    pic_Assitance.Enabled = True
                    pic_remote.Enabled = True
                End If
            End If

            ' SAADI
            ' changed Type of equipment to load AD info (membership)

            'Membership du PC
            Dim strCommand = "nltest /server:" & ComputerName & " /dsgetsite"
            Dim strResults = ""
            Dim Newline As String
            Newline = System.Environment.NewLine

            'RunDosCommand(strCommand)
            CMDAutomate(strCommand, strResults)
            strResults = strResults.Replace(" & vbCrLf & vbCrLf & ", " & vbCrLf & ")
            Dim parts As String() = strResults.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            If (parts(4).Contains("VPN")) Or (parts(5).Contains("The command completed successfully")) Then
                txt_ADSite_NEW.Text = parts(4)
                pic_notOk.Visible = False
                pic_Ok.Visible = True
                pic_rightArrow.Visible = False

            Else
                pic_notOk.Visible = True
                pic_Ok.Visible = False
                pic_rightArrow.Visible = False

            End If


            ' Show Execution History
            'ShowExecutionHistory()


            LoadMorePcInfo()

            'Désactive les loading
            Main_Start_Form.Instance.Label1.Visible = False
            Me.lbl_loading.Visible = False
            Me.Cursor = Cursors.Default
        Else
            pic_notOk.Visible = True
            pic_Ok.Visible = False
            pic_rightArrow.Visible = False

        End If

    End Sub

    Function CheckDNSAndIP() As Boolean
        ' SAADI
        ' show results of nslookup
        Dim retVal As Boolean = False
        Dim strCommand = ""
        Dim strResults = ""
        Dim Newline As String
        Dim strListOfIPs As String = ""
        Newline = System.Environment.NewLine
        strCommand = "NSLOOKUP " & ComputerName
        strResults = ""
        CMDAutomate(strCommand, strResults)
        'send to log window
        txt_LogWindow.Text = txt_LogWindow.Text & vbCrLf & strResults
        Dim parts As String() = strResults.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
        If parts.Length > 10 Then
            'txt_NSLookup.Text = strCommand & Newline & parts(4) & Newline & parts(5) & Newline & parts(7) & Newline & parts(8)
            For i = 0 To UBound(parts)
                If (Not parts(i).Equals("")) Then
                    strListOfIPs = strListOfIPs & "###" & parts(i).Substring(9).Trim()
                    'Exit For
                End If
            Next

            'Run twice

            Dim arrayIPs = strListOfIPs.Split("###")
            strResults = ""


            For i = 0 To UBound(arrayIPs)
                If (Not arrayIPs(i).Equals("")) Then
                    strCommand = "NSLOOKUP " & arrayIPs(i)
                    CMDAutomate(strCommand, strResults)
                    'parts = strResults.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                    'For j = 0 To UBound(parts)
                    '    If (parts(i).Contains(ComputerName)) Then
                    '        retVal = True
                    '        Exit For
                    '    End If
                    'Next
                    If strResults.Contains(ComputerName) Then
                        'send to log window
                        txt_LogWindow.Text = txt_LogWindow.Text & vbCrLf & strResults
                        retVal = True
                        Exit For
                    End If
                End If
            Next

            'Dim strListedComputerName = parts(7)
            If Not retVal Then
                txt_PCName_NEW.Text = ""
                txt_PCName_NEW.Text = txt_PCName_NEW.Text & " - DNS ISSUE"
            End If
            'txt_NSLookup.Text = txt_NSLookup.Text & Newline & Newline & strCommand & Newline & parts(4) & Newline & parts(5) & Newline & parts(7) & Newline & parts(8)
        Else
            'txt_NSLookup.Text = "Not connected" & Newline & parts(4) & Newline & parts(5)
        End If

        Return retVal
    End Function

    Friend Sub Affichage_Defaut()
        'Active le loading
        Me.lbl_loading.Visible = True

        'Reset des valeurs par default
        txt_PCName_NEW.Text = ComputerName
        txt_Client_Version_Result_NEW.Text = "..."
        txt_img_ver.Text = "..."
        txt_img_install_Date.Text = "..."
        txt_language_NEW.Text = "..."
        txt_last_reboot_NEW.Text = "..."
        txt_SiteCode_result_NEW.Text = "..."
        txt_OSCaption_NEW.Text = "..."
        txt_ADSite_NEW.Text = "..."
        txtLoggedIn_NEW.Text = "..."
        txt_IP_NEW.Text = "..."
        User = ""
        txt_ManagementPoint_NEW.Text = "..."
        txt_SCCM_Catalogue_NEW.Text = "..."
        txt_WUA_NEW.Text = "..."


        pic_OFF_RemoteRegistry.Visible = True
        pic_ON_RemoteRegistry.Visible = False


        Pic_OFF_MPSSVC.Visible = True
        Pic_ON_MPSSVC.Visible = False


        Pic_OFF_CCMEXEC.Visible = True
        Pic_ON_CCMEXEC.Visible = False


        Pic_OFF_BITS.Visible = True
        Pic_ON_BITS.Visible = False


        Pic_OFF_PeerDistSvc.Visible = True
        Pic_ON_PeerDistSvc.Visible = False


        Pic_OFF_wuauserv.Visible = True
        Pic_ON_wuauserv.Visible = False

        ServiceWindowsListView.Items.Clear()
        Err_Services_Acces = False
        Err_RemoteRegistry_Acces = False
        Err_MPSSVC_Acces = False
        Err_CCMEXEC_Acces = False
        Err_BITS_Acces = False
        Err_PeerDistSvc_Acces = False
        Err_wuauserv_Acces = False
        cmd_multi_user.Visible = False
        MultiUser = 0

        pic_reboot_status.Visible = False
        txt_reboot_status.Visible = False

        'Pic_VPN.Visible = False

        lbl_img_ver_win10_NEW.Visible = False
        txt_img_ver_win10_NEW.Visible = False

        'Masque par default l'icone VPN
        'Me.Pic_VPN.Visible = False

        'Fait une mise a jour de l'affichage de la forme
        Me.Refresh()

    End Sub

    Private Sub cmd_Check_Click(sender As Object, e As EventArgs) Handles cmd_Check_NEW.Click
        'Modification du ComputerName
        ComputerName = txt_PCName_NEW.Text

        'Active le chagement de la souris en mode attente
        Me.Cursor = Cursors.WaitCursor
        Affichage_Defaut()
        Connexion()
        'Remet le cursor en mode defaut
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmdSoftware_Click(sender As Object, e As EventArgs) Handles cmdSoftware.Click
        Dim softwareForm As Software = New Software
        softwareForm.ShowDialog(Me)
    End Sub

    Private Sub pic_Assitance_Click(sender As Object, e As EventArgs) Handles pic_Assitance.Click, REMOTEASSISTANCEToolStripMenuItem.Click
        If pic_Assitance.Cursor = Cursors.Hand Then
            Me.Enabled = False
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\msra.exe", "/offerra " & ComputerName)
            Thread.Sleep(1000)
            Me.pic_Assitance.BorderStyle = BorderStyle.None
        End If
        Me.Enabled = True
    End Sub

    Private Sub pic_remote_Click(sender As Object, e As EventArgs) Handles pic_remote.Click, REMOTEDESKTOPToolStripMenuItem.Click
        'mstsc.exe
        Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\mstsc.exe", "/v: " & ComputerName)
        Thread.Sleep(1000)
        Me.pic_remote.BorderStyle = BorderStyle.None
    End Sub

    Private Sub pic_OFF_RemoteRegistry_Click(sender As Object, e As EventArgs) Handles pic_OFF_RemoteRegistry.Click
        ' Mes le service a ON
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "RemoteRegistry"
        Try
            sc.Start()
            sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)

            Me.pic_ON_RemoteRegistry.Visible = True
            Me.pic_OFF_RemoteRegistry.Visible = False
        Catch ex As Exception
            If Not sc.Status = ServiceControllerStatus.Stopped Then
                Me.pic_ON_RemoteRegistry.Visible = True
                Me.pic_OFF_RemoteRegistry.Visible = False
            End If
        End Try

    End Sub

    Private Sub pic_ON_RemoteRegistry_Click(sender As Object, e As EventArgs) Handles pic_ON_RemoteRegistry.Click
        ' Mes le service a OFF
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "RemoteRegistry"
        Try
            sc.Stop()
            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)

            Me.pic_ON_RemoteRegistry.Visible = False
            Me.pic_OFF_RemoteRegistry.Visible = True
        Catch ex As Exception
            If sc.Status = ServiceControllerStatus.Stopped Then
                Me.pic_ON_RemoteRegistry.Visible = False
                Me.pic_OFF_RemoteRegistry.Visible = True
            End If
        End Try

    End Sub

    Private Sub Pic_OFF_MPSSVC_Click(sender As Object, e As EventArgs) Handles Pic_OFF_MPSSVC.Click
        ' Mes le service a ON
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "MPSSVC"
        Try
            sc.Start()
            sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)

            Me.Pic_ON_MPSSVC.Visible = True
            Me.Pic_OFF_MPSSVC.Visible = False
        Catch ex As Exception
            If Not sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_MPSSVC.Visible = True
                Me.Pic_OFF_MPSSVC.Visible = False
            End If
        End Try
    End Sub

    Private Sub Pic_ON_MPSSVC_Click(sender As Object, e As EventArgs) Handles Pic_ON_MPSSVC.Click
        ' Mes le service a OFF
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "MPSSVC"
        Try
            sc.Stop()
            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)

            Me.Pic_ON_MPSSVC.Visible = False
            Me.Pic_OFF_MPSSVC.Visible = True
        Catch ex As Exception
            If sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_MPSSVC.Visible = False
                Me.Pic_OFF_MPSSVC.Visible = True
            End If
        End Try
    End Sub

    Private Sub Pic_ON_CCMEXEC_Click(sender As Object, e As EventArgs) Handles Pic_ON_CCMEXEC.Click
        ' Mes le service a OFF
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "CCMEXEC"
        Try
            sc.Stop()
            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)

            Me.Pic_ON_CCMEXEC.Visible = False
            Me.Pic_OFF_CCMEXEC.Visible = True
        Catch ex As Exception
            If sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_CCMEXEC.Visible = False
                Me.Pic_OFF_CCMEXEC.Visible = True
            End If
        End Try
    End Sub

    Private Sub Pic_OFF_CCMEXEC_Click(sender As Object, e As EventArgs) Handles Pic_OFF_CCMEXEC.Click
        ' Mes le service a ON
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "CCMEXEC"
        Try
            sc.Start()
            sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)

            Me.Pic_ON_CCMEXEC.Visible = True
            Me.Pic_OFF_CCMEXEC.Visible = False
        Catch ex As Exception
            Try
                If Not sc.Status = ServiceControllerStatus.Stopped Then
                    Me.Pic_ON_CCMEXEC.Visible = True
                    Me.Pic_OFF_CCMEXEC.Visible = False
                End If
            Catch ex2 As Exception
                Me.Pic_ON_CCMEXEC.Visible = False
                Me.Pic_OFF_CCMEXEC.Visible = True

            End Try

        End Try
    End Sub

    Private Sub Pic_ON_BITS_Click(sender As Object, e As EventArgs) Handles Pic_ON_BITS.Click
        ' Mes le service a OFF
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "BITS"
        Try
            sc.Stop()
            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)

            Me.Pic_ON_BITS.Visible = False
            Me.Pic_OFF_BITS.Visible = True
        Catch ex As Exception
            If sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_BITS.Visible = False
                Me.Pic_OFF_BITS.Visible = True
            End If
        End Try
    End Sub

    Private Sub Pic_OFF_BITS_Click(sender As Object, e As EventArgs) Handles Pic_OFF_BITS.Click
        ' Mes le service a ON
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "BITS"
        Try
            sc.Start()
            sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)

            Me.Pic_ON_BITS.Visible = True
            Me.Pic_OFF_BITS.Visible = False
        Catch ex As Exception
            If Not sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_BITS.Visible = True
                Me.Pic_OFF_BITS.Visible = False
            End If
        End Try
    End Sub

    Private Sub Pic_OFF_PeerDistSvc_Click(sender As Object, e As EventArgs) Handles Pic_OFF_PeerDistSvc.Click
        ' Mes le service a ON
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "PeerDistSvc"

        Try
            sc.Start()
            sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)

            Me.Pic_ON_PeerDistSvc.Visible = True
            Me.Pic_OFF_PeerDistSvc.Visible = False
        Catch ex As Exception
            If Not sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_PeerDistSvc.Visible = True
                Me.Pic_OFF_PeerDistSvc.Visible = False
            End If
        End Try
    End Sub

    Public Sub DisableBranchCache()
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "PeerDistSvc"
        Me.Pic_ON_PeerDistSvc.Visible = False
        Me.Pic_OFF_PeerDistSvc.Enabled = False
        Me.Pic_OFF_PeerDistSvc.Visible = True
        Me.Label8.Visible = True
        'Me.Label8.Text = Me.Label8.Text & " (disabled)"
        Try
            sc.Stop()
            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)

        Catch ex As Exception
            txtLoggedIn_NEW.Text = "problem stopping branch cache"
        End Try
    End Sub

    Public Sub EnableBranchCache()
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "PeerDistSvc"
        Me.Pic_ON_PeerDistSvc.Visible = True
        Me.Pic_OFF_PeerDistSvc.Visible = False
        Me.Label8.Visible = True
        'Me.Label8.ResetText()

        Try
            sc.Stop()
            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)

        Catch ex As Exception
            txtLoggedIn_NEW.Text = "problem stopping branch cache"
        End Try
    End Sub

    Private Sub Pic_ON_PeerDistSvc_Click(sender As Object, e As EventArgs) Handles Pic_ON_PeerDistSvc.Click
        ' Mes le service a OFF
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "PeerDistSvc"
        Try
            sc.Stop()
            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)

            Me.Pic_ON_PeerDistSvc.Visible = False
            Me.Pic_OFF_PeerDistSvc.Visible = True
        Catch ex As Exception
            If sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_PeerDistSvc.Visible = False
                Me.Pic_OFF_PeerDistSvc.Visible = True
            End If
        End Try
    End Sub

    Private Sub Pic_OFF_wuauserv_Click(sender As Object, e As EventArgs) Handles Pic_OFF_wuauserv.Click
        ' Mes le service a ON
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "wuauserv"
        Try
            sc.Start()
            sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)

            Me.Pic_ON_wuauserv.Visible = True
            Me.Pic_OFF_wuauserv.Visible = False
        Catch ex As Exception
            If sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_wuauserv.Visible = False
                Me.Pic_OFF_wuauserv.Visible = True
            End If
        End Try
    End Sub

    Private Sub Pic_ON_wuauserv_Click(sender As Object, e As EventArgs) Handles Pic_ON_wuauserv.Click
        ' Mes le service a OFF
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = "wuauserv"
        Try
            sc.Stop()
            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)

            Me.Pic_ON_wuauserv.Visible = False
            Me.Pic_OFF_wuauserv.Visible = True
        Catch ex As Exception
            If Not sc.Status = ServiceControllerStatus.Stopped Then
                Me.Pic_ON_wuauserv.Visible = True
                Me.Pic_OFF_wuauserv.Visible = False
            End If
        End Try
    End Sub

    Private Sub cmd_SCCM_WSUS_SCUP_Approved_Click(sender As Object, e As EventArgs) Handles cmd_SCCM_WSUS_SCUP_Approved.Click
        Dim wsusScupForm As WSUS_SCUP = New WSUS_SCUP
        wsusScupForm.ShowDialog(Me)
    End Sub

    Private Sub AdvancedMode_Menu_Click(sender As Object, e As EventArgs) Handles AdvancedMode_Menu.Click
        Dim advMode As Adv_Mode = New Adv_Mode
        advMode.ShowDialog(Me)
    End Sub

    Private Sub cmd_SCCM_Action_Click(sender As Object, e As EventArgs) Handles cmd_SCCM_Action.Click
        Dim sccmActionTools As SCCM_Action_Tools = New SCCM_Action_Tools
        sccmActionTools.ShowDialog(Me)
    End Sub

    Private Sub UILanguage_Click(sender As Object, e As EventArgs) Handles Menu_Francais.Click, Menu_English.Click, FRToolStripMenuItem.Click, ENToolStripMenuItem.Click
        Dim cultureName As String = GlobalUICulture.Name

        If sender.Equals(Menu_Francais) Or sender.Equals(FRToolStripMenuItem) Then
            cultureName = "fr-CA"
        ElseIf sender.Equals(Menu_English) Or sender.Equals(ENToolStripMenuItem) Then
            cultureName = "en-CA"
        End If

        If cultureName <> GlobalUICulture.Name Then
            GlobalUICulture = New CultureInfo(cultureName)
            ResetLanguageMenuItems()

            ' Vérification du bouton de multi user
            GetUser(ComputerName)

            'Procedure pour changer la langue du menustrip
            Dim resources = New ComponentResourceManager(GetType(Main))

            For Each c As Control In Me.Controls
                If TypeOf c Is MenuStrip Then
                    Dim menu_strip As MenuStrip = DirectCast(c, MenuStrip)
                    For Each child As ToolStripMenuItem In menu_strip.Items
                        resources.ApplyResources(child, child.Name, New CultureInfo(cultureName))
                        If Advance_mode = True Then
                            Me.AdvancedMode_Menu.Visible = True
                            AdvancedModeTab.Visible = True
                        Else
                            Me.AdvancedMode_Menu.Visible = False
                            AdvancedModeTab.Visible = False
                        End If
                    Next child
                End If
            Next c

            'Correction d'un BUG....Utiliser pour le reset des tooltips quand on change la langue
            Me.TT.SetToolTip(Me.pic_Assitance, My.Resources.ToolTip_Main_Assistance)
            Me.TT.SetToolTip(Me.pic_Explorer, My.Resources.ToolTip_Main_Explorer)
            Me.TT.SetToolTip(Me.pic_Reboot, My.Resources.ToolTip_Main_Reboot)
            Me.TT.SetToolTip(Me.pic_remote, My.Resources.ToolTip_Main_remote)
            Me.TT.SetToolTip(Me.txt_SiteCode_result_NEW, My.Resources.ToolTip_Main_SiteCode_result)
            Me.TT.SetToolTip(Me.GroupBoxMaintenanceWindow_NEW, My.Resources.ToolTip_Main_GroupBox2)
            'Me.TT.SetToolTip('Me.pic_UserGuide, My.Resources.ToolTip_UserGuide)

            Me.Refresh()
        End If
    End Sub

    Private Sub pic_Reboot_Click(sender As Object, e As EventArgs) Handles pic_Reboot.Click, REBOOTREMOTECOMPUTERToolStripMenuItem.Click
        Me.Cursor = Cursors.WaitCursor

        Dim Result_msg = MsgBox(My.Resources.ConfirmReboot & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.MessageReboot & " : " & ComputerName)

        If Result_msg = 6 Then
            Try
                If OSLanguage = "1033" Then
                    Process.Start("C:\Windows\System32\shutdown.exe", " /m \\" & ComputerName & " -r -f -t 30 -c " & Chr(34) & "A reboot was triggered by team IITB in 30 seconds" & Chr(34))
                    Reboot_Send = True
                    Thread.Sleep(1000)
                    MsgBox(My.Resources.SuccessReboot, MsgBoxStyle.Information)

                Else
                    Process.Start("C:\Windows\System32\shutdown.exe", " /m \\" & ComputerName & " -r -f -t 30 -c " & Chr(34) & "Un redémarrage a été déclenché par l'équipe de la DGIIT dans 30 secondes" & Chr(34))
                    Reboot_Send = True
                    Thread.Sleep(1000)
                    MsgBox(My.Resources.SuccessReboot, MsgBoxStyle.Information)

                End If
                Me.pic_Reboot.BorderStyle = BorderStyle.None
            Catch ex As Exception
                Me.pic_Reboot.BorderStyle = BorderStyle.None
            End Try
        End If

        'Reset de la forme car un reboot a été lancer
        If Reboot_Send = True Then
            'Modification du ComputerName
            ComputerName = ""
            txt_PCName_NEW.Text = "..."
            'Active le chagement de la souris en mode attente
            Me.Cursor = Cursors.WaitCursor
            Affichage_Defaut()
            Connexion()

            'Remet le cursor en mode defaut

            'ComputerName = System.Net.Dns.GetHostName.Trim
            Main_Start_Form.Instance.txt_PCName.Text = ComputerName
            Main_Start_Form.Instance.pic_rightArrow.Visible = False
            Main_Start_Form.Instance.pic_notOk.Visible = True
            Main_Start_Form.Instance.pic_Ok.Visible = False
            Main_Start_Form.Instance.Text = "SCCM PC Admin"
            Main_Start_Form.Instance.Show()
            Me.Cursor = Cursors.Default
            Main.Instance.Close()

        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim aboutForm As About = New About
        aboutForm.ShowDialog()
    End Sub

    Private Sub pic_UserGuide_MouseDown(sender As Object, e As MouseEventArgs)
        'Me.pic_UserGuide.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub pic_Reboot_MouseDown(sender As Object, e As MouseEventArgs) Handles pic_Reboot.MouseDown
        Me.pic_Reboot.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub pic_remote_MouseDown(sender As Object, e As MouseEventArgs) Handles pic_remote.MouseDown
        Me.pic_remote.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub pic_Assitance_MouseDown(sender As Object, e As MouseEventArgs) Handles pic_Assitance.MouseDown
        Me.pic_Assitance.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub pic_Explorer_MouseDown(sender As Object, e As MouseEventArgs) Handles pic_Explorer.MouseDown
        Me.pic_Explorer.BorderStyle = BorderStyle.Fixed3D
    End Sub

    Private Sub cmd_Clear_cache_bits_Click(sender As Object, e As EventArgs) Handles cmd_Clear_cache_bits.Click
        Dim advClean As Adv_Clean = New Adv_Clean
        advClean.ShowDialog(Me)
    End Sub

    Private Sub cmd_pkg_apps_Click(sender As Object, e As EventArgs) Handles cmd_pkg_apps.Click
        Dim packageApps As Pack_Apps = New Pack_Apps
        packageApps.ShowDialog(Me)
    End Sub

    Private Sub cmd_Force_WSUS_Click(sender As Object, e As EventArgs) Handles cmd_Force_WSUS.Click, FORCESECURITYUPDATEToolStripMenuItem.Click
        Dim popupRefreshWSUSForm As Popup_Refresh_WSUS = New Popup_Refresh_WSUS
        popupRefreshWSUSForm.ShowDialog(Me)
    End Sub

    Private Sub cmd_Force_Apps_update_Click(sender As Object, e As EventArgs) Handles cmd_Force_Apps_update.Click, FORCEAPPLICATIONUPDATEToolStripMenuItem.Click
        Dim popupRefreshApps As Popup_Refresh_Apps = New Popup_Refresh_Apps
        popupRefreshApps.ShowDialog(Me)
    End Sub

    Private Sub pic_Explorer_Click(sender As Object, e As EventArgs) Handles pic_Explorer.Click, EXPLORERToolStripMenuItem.Click
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$")
        Catch ex As Exception
            Me.pic_Explorer.BorderStyle = BorderStyle.None
            ' Gestion de l'erreur
        End Try
        Me.pic_Explorer.BorderStyle = BorderStyle.None
    End Sub

    Private Sub cmd_Reinstall_client_Click(sender As Object, e As EventArgs) Handles cmd_Reinstall_client.Click, REINSTALLSCCMCLIENTToolStripMenuItem1.Click, cmd_Reinstall_client_NEW.Click
        Dim strResult As String = ""
        'Valide que le fichier INI est la 
        If CheckFileExists(INI_Files) = False Then
            MsgBox(My.Resources.Message_file_ini_missing, MsgBoxStyle.Critical)
            INI_READ_ERROR = True
            Exit Sub
        Else
            'MsgBox(My.Resources.Message_long_wait_time, MsgBoxStyle.Critical)
            INI_READ_ERROR = False
            'CMDAutomate(INI_REINSTALLPATH, strResult)
        End If

        'Valide que l'ordinateur sois join Domain 

        If PC_Domain = "" Then
            MsgBox(My.Resources.Message_Domain_Missing, MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim Result_msg = MsgBox(My.Resources.WarningReinstallClient & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.MessageReinstallClient & " : " & ComputerName)

        If Result_msg = 6 Then

            'Mes le site et le cient version en mode vide
            txt_Client_Version_Result_NEW.Text = "?"
            txt_SiteCode_result_NEW.Text = "?"
            SiteCode = "?"
            ClientVer = "?"

            'Dim popupReinstallClientForm As Popup_Reinstall_Client = New Popup_Reinstall_Client
            'popupReinstallClientForm.ShowDialog(Me)

            'CMDAutomate(INI_REINSTALLPATH, strResult)
            Read_INI(UCase(PC_Domain))
            RunDosCommand(INI_REINSTALLPATH.Replace("PATH=", ""))

        End If
    End Sub

#Region "Service Windows"
    Private _serviceWindows As IEnumerable(Of ServiceWindow)
    Private loadInstalledSoftwareTab As Integer
    Private loadJavaTab As Integer
    Private loadProcessTab As Integer
    Private loadServiceTab As Integer

    Private Property ServiceWindows() As IEnumerable(Of ServiceWindow)
        Get
            Return _serviceWindows
        End Get
        Set(ByVal value As IEnumerable(Of ServiceWindow))
            _serviceWindows = value
        End Set
    End Property

    Private Function GetClienSDKServiceWindows() As HashSet(Of ServiceWindow)
        Dim WMI_Info As ManagementScope = New ManagementScope("\\" & ComputerName & "\ROOT\ccm\ClientSDK")
        Dim Query As SelectQuery = New SelectQuery("SELECT * FROM CCM_ServiceWindow")
        Dim clientSDKServiceWindows As HashSet(Of ServiceWindow) = New HashSet(Of ServiceWindow)

        Try
            For Each item As ManagementObject In New ManagementObjectSearcher(WMI_Info, Query).Get()
                Dim serviceWindow As ServiceWindow = New ServiceWindow
                serviceWindow.Duration = CInt(item("Duration"))
                serviceWindow.EndTime = ManagementDateTimeConverter.ToDateTime(item("EndTime")).ToUniversalTime
                serviceWindow.ID = "N" ' Pour empêcher de l'effacer
                serviceWindow.StartTime = ManagementDateTimeConverter.ToDateTime(item("StartTime")).ToUniversalTime
                serviceWindow.Type = CInt(item("Type"))

                If serviceWindow.Duration > 0 Then
                    clientSDKServiceWindows.Add(serviceWindow)
                End If
            Next
        Catch ex As Exception
        End Try

        Return clientSDKServiceWindows
    End Function

    Private Function GetActualConfigServiceWindows() As HashSet(Of ServiceWindow)
        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\Ccm\policy\machine\ActualConfig")
        Dim Query As New SelectQuery("SELECT * FROM CCM_ServiceWindow")
        Dim actualConfigServiceWindows As HashSet(Of ServiceWindow) = New HashSet(Of ServiceWindow)

        Try
            For Each item As ManagementObject In New ManagementObjectSearcher(WMI_Info, Query).Get()
                Dim serviceWindow As ServiceWindow = New ServiceWindow
                serviceWindow.ID = CStr(item("ServiceWindowID"))
                serviceWindow.Type = CStr(item("ServiceWindowType"))

                If Not serviceWindow.ID.Chars(0) = "{"c Then
                    If serviceWindow.Type = 1 Then
                        Dim Schedules = item("Schedules").Substring(0, 16)

                        If Not Schedules = "" Then
                            serviceWindow.StartTime = New Date(SMSSchedules.startyear(Schedules),
                                                               SMSSchedules.startmonth(Schedules),
                                                               SMSSchedules.startday(Schedules),
                                                               SMSSchedules.starthour(Schedules),
                                                               SMSSchedules.startminute(Schedules), 0, 0)

                            Dim Duration_Days As Integer = SMSSchedules.dayduration(Schedules)

                            If Duration_Days > 0 Then
                                serviceWindow.EndTime = serviceWindow.StartTime.AddDays(Duration_Days)
                            Else
                                Dim Duration_Hours As Integer = SMSSchedules.hourduration(Schedules)

                                If Duration_Hours > 0 Then
                                    serviceWindow.EndTime = serviceWindow.StartTime.AddHours(Duration_Hours)
                                Else
                                    Dim Duration_Min As Integer = SMSSchedules.minuteduration(Schedules)

                                    If Duration_Min > 0 Then
                                        serviceWindow.EndTime = serviceWindow.StartTime.AddMinutes(Duration_Min)
                                    End If
                                End If
                            End If

                            actualConfigServiceWindows.Add(serviceWindow)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
        End Try

        Return actualConfigServiceWindows
    End Function

    ' Called when list of service windows kept in memory needs to be refreshed by fetching data from remote computer
    Private Sub RefreshServiceWindows() Handles cmd_Show_SW.Click
        Me.Cursor = Cursors.WaitCursor
        Dim serviceWindowSet As HashSet(Of ServiceWindow) = GetActualConfigServiceWindows()
        serviceWindowSet.UnionWith(GetClienSDKServiceWindows()) ' Doubles from this second set don't have precedence, the ones from the deletable set will appear until deleted
        ServiceWindows = serviceWindowSet
        ResetServiceWindows()
        Me.Cursor = Cursors.Default
    End Sub

    ' Called when refreshing the list from remote computer or when changing the language/culture of the thread
    Private Sub ResetServiceWindows()

        ServiceWindowsListView.Items.Clear()


        If ServiceWindows IsNot Nothing Then

            For Each item As ServiceWindow In ServiceWindows
                ' TODO: Change below to use dates instead. Ran into problem with midnight: hour not showing.
                '       Problem with date strings in listview is that the sort function doesn't always work correctly
                '       Ex: '20/10/2016' and '01/11/2016'

                ' Probleme avec le trie en francais car il tris selon le jour
                Dim listItem As ListViewItem = New ListViewItem(item.StartTime.ToString(My.Resources.ServiceWindowDateTimeFormat))
                listItem.SubItems.Add(item.EndTime.ToString(My.Resources.ServiceWindowDateTimeFormat))


                listItem.SubItems.Add(item.ID)

                If item.ID <> "N" Then
                    listItem.BackColor = Color.DarkRed
                    listItem.ForeColor = Color.White
                End If

                ServiceWindowsListView.Items.Add(listItem)
            Next
        End If



    End Sub

    Private Sub cmd_Add_SW_NEW_Click(sender As Object, e As EventArgs)
        'Va demander au client de choisir la longueur de la fenetre de maintenance désirer
        'Dim popupMWTime As Popup_MW_Time = New Popup_MW_Time
        'popupMWTime.ShowDialog(Me)
        'If MW_Select = "NULL" Then Exit Sub
        'Me.Cursor = Cursors.WaitCursor

        Dim newSW As New SMSSchedules.SMS_ST_NonRecurring
        'Dim swDate As DateTime
        Dim ts As TimeSpan
        Dim objService As Object
        Dim instServiceWindow As Object = ""
        Dim clsServiceWindow As Object
        Dim Conv_Schedules As String = ""
        Dim CurrentDate As Date = Now
        Dim End_taskDate As Integer
        Dim Start_taskDate As Date

        Try

            Try
                'recherche le décalage du fuseau horraire du pc a distance
                TimeZone(ComputerName)
            Catch ex As Exception

            End Try

            Start_taskDate = CurrentDate.AddHours(TimeZone_PC)

            Select Case MW_Select

                Case 1 'Retourne 1h
                    End_taskDate = 1
                Case 2 'Retourne 2h
                    End_taskDate = 2
                Case 4 'Retourne 4h
                    End_taskDate = 4
                Case 8 'Retourne 8h
                    End_taskDate = 8
                Case 12 'Retourne 12h
                    End_taskDate = 12
                Case 24 'Retourne 24h
                    End_taskDate = 24
                Case 48 'Retourne 48h
                    End_taskDate = 48
                Case 72 'Retourne 72h
                    End_taskDate = 72

                Case Else
                    'Par default ajout si erreur une fenetre minimum de 1 heurs
                    End_taskDate = 1

            End Select

            Try
                'Conection a la classe du WMI 
                objService = GetObject("winmgmts:\\" & ComputerName & "\root\ccm\policy\machine\requestedconfig")
                clsServiceWindow = objService.Get("CCM_ServiceWindow")
                instServiceWindow = clsServiceWindow.SpawnInstance_

                'Convertie la Schedules pour la mettre en place au WMI
                newSW.StartTime = Start_taskDate
                newSW.IsGMT = False

                ts = New TimeSpan(End_taskDate, 0, 0)
                newSW.MinuteDuration = ts.Minutes
                newSW.HourDuration = ts.Hours
                newSW.DayDuration = ts.Days

                'Mes en place la nouvelle fenetre de maintenance manuelle

                instServiceWindow.PolicySource = "Local"
                instServiceWindow.ServiceWindowType = "1"
                instServiceWindow.Schedules = newSW.ScheduleID
                instServiceWindow.PolicyVersion = "1"
                instServiceWindow.ServiceWindowID = Guid.NewGuid.ToString

                instServiceWindow.Put_()

                'Prend une pause de 10 secondes
                Dim waitForm As Wait = New Wait(10)
                waitForm.ShowDialog(Me)
            Catch ex As Exception
                'Gestion des erreur
            End Try

        Catch ex As Exception
            'Gestion des erreur
            Me.Cursor = Cursors.Default
        End Try

        instServiceWindow = ""

        'renvoye un refres the l'affichage des MW
        RefreshServiceWindows()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ServiceWindowsListView.DoubleClick
        Dim ServiceWindowID
        Dim msg_reult

        'Récupere les valeur Sélectionner...
        Try
            ServiceWindowID = ServiceWindowsListView.SelectedItems.Item(0).SubItems(2).Text
        Catch ex As Exception
            ServiceWindowID = Nothing
            'MsgBox("   ;(    ", MsgBoxStyle.Information, "SCCM PC Admin")
        End Try

        'Vérifie ces une valeur manuel ajouter... si non sort sans rien faire
        If Not ServiceWindowID = "N" Then msg_reult = MsgBox(My.Resources.ConfirmRemoveMW, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin") Else Exit Sub

        Select Case msg_reult
            Case 6 ' Oui
                Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\ccm\Policy\Machine\RequestedConfig") 'ActualConfig or RequestedConfig
                Dim Query As New SelectQuery("SELECT * FROM CCM_ServiceWindow")
                Dim search As New ManagementObjectSearcher(WMI_Info, Query)

                Dim info As ManagementObject
                Try

                    ' valide si il y a des taches en histoique
                    For Each info In search.Get()
                        Dim SWT = info("ServiceWindowID")
                        If SWT = ServiceWindowID Then
                            If Mid(SWT, 1, 1) <> "{" Then
                                info.Delete()
                                ServiceWindowsListView.SelectedItems.Item(0).Remove()
                            End If
                        End If
                    Next

                    'Prend une pause de 10 secondes
                    Dim waitForm As Wait = New Wait(10)
                    waitForm.ShowDialog(Me)
                Catch ex As Exception
                    'Gestion de l'erreur
                End Try
            Case 7 ' Non
                ' Ne fait rien car le client a dit non
        End Select

        'renvoye un refres the l'affichage des MW
        RefreshServiceWindows()
    End Sub
#End Region

    'Private Sub cmd_pc_info_Click(sender As Object, e As EventArgs) Handles cmd_pc_info.Click
    '    Dim PC_Information As PC_Info = New PC_Info
    '    PC_Information.ShowDialog(Me)
    'End Sub

    Private Sub cmd_multi_user_Click(sender As Object, e As EventArgs) Handles cmd_multi_user.Click
        GetUser_Multi(ComputerName)
    End Sub

    Private Sub pic_UserGuide_Click(sender As Object, e As EventArgs) Handles UserGuideToolStripMenuItem1.Click

        Const WebPageFR = "http://dialogue/grp/DS-SD/Shared%20Documents/Guides%20de%20l'utilisateur%20-%20User%20Guides/Guide%20d’utilisation%20de%20SCCM%20PC%20Admin_F.docx"
        Const WebPageEN = "http://dialogue/grp/DS-SD/Shared%20Documents/Guides%20de%20l'utilisateur%20-%20User%20Guides/SCCM%20PC%20Admin%20-%20User%20Guide_E.docx"


        Select Case GlobalUICulture.Name
            Case "fr-CA"
                Process.Start(WebPageFR)

            Case "en-CA"
                Process.Start(WebPageEN)
        End Select

        'Me.pic_UserGuide.BorderStyle = BorderStyle.None
    End Sub

    Private Sub UserGuideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserGuideToolStripMenuItem.Click

        Const WebPageFR = "http://dialogue/grp/DS-SD/Shared%20Documents/Guides%20de%20l'utilisateur%20-%20User%20Guides/Guide%20d’utilisation%20de%20SCCM%20PC%20Admin_F.docx"
        Const WebPageEN = "http://dialogue/grp/DS-SD/Shared%20Documents/Guides%20de%20l'utilisateur%20-%20User%20Guides/SCCM%20PC%20Admin%20-%20User%20Guide_E.docx"


        Select Case GlobalUICulture.Name
            Case "fr-CA"
                Process.Start(WebPageFR)

            Case "en-CA"
                Process.Start(WebPageEN)
        End Select

    End Sub

    Private Sub Services_verify()

        Dim isRunning As Boolean = False
        'Mais le services d'acces au registre a distance a "On" et valide leur fonctionnements
        'Service RemoteRegistry  = Service du Registre à distance
        Services.Service_VerificationCheckOnly("RemoteRegistry", isRunning)

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_RemoteRegistry_Acces = True Or Err_Services_Acces = True Then
            pic_rightArrow.Visible = False
            pic_notOk.Visible = True
            pic_Ok.Visible = False
            'MsgBox(My.Resources.ErrorRegistryConnection, MsgBoxStyle.Critical, "SCCM PC Admin")
        Else
            Me.pic_ON_RemoteRegistry.Visible = True
            Me.pic_OFF_RemoteRegistry.Visible = False

        End If

        'Service MPSSVC  = Service du Firewall
        Services.Service_VerificationCheckOnly("MPSSVC", isRunning)

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_MPSSVC_Acces = True Or Err_Services_Acces = True Then

        Else
            Me.Pic_ON_MPSSVC.Visible = True
            Me.Pic_OFF_MPSSVC.Visible = False

        End If

        'Service RemoteRegistry  = Service du Registre à distance
        Services.Service_VerificationCheckOnly("CCMEXEC", isRunning)

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_CCMEXEC_Acces = True Or Err_Services_Acces = True Then

        Else
            Me.Pic_ON_CCMEXEC.Visible = True
            Me.Pic_OFF_CCMEXEC.Visible = False

        End If

        'Service BITS  = Service du Download de Windows
        Services.Service_VerificationCheckOnly("BITS", isRunning)

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_BITS_Acces = True Or Err_Services_Acces = True Then

        Else
            Me.Pic_ON_BITS.Visible = True
            Me.Pic_OFF_BITS.Visible = False

        End If

        'Service PeerDistSvc  = Service du cache de SCCM en mode partage
        Services.Service_VerificationCheckOnly("PeerDistSvc", isRunning)

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_PeerDistSvc_Acces = True Or Err_Services_Acces = True Then

        Else
            Me.Pic_ON_PeerDistSvc.Visible = True
            Me.Pic_OFF_PeerDistSvc.Visible = False

        End If

        'Service wuauserv  = Service de Windows Update
        Services.Service_VerificationCheckOnly("wuauserv", isRunning)

        'Varialble de retoure si erreur
        If Err_wuauserv_Acces = True Or Err_Services_Acces = True Then

        Else
            Me.Pic_ON_wuauserv.Visible = True
            Me.Pic_OFF_wuauserv.Visible = False

        End If
    End Sub


    Private Sub cmd_SCCM_report_Click(sender As Object, e As EventArgs)
        Dim WebPage = ""
        Select Case SiteCode
            Case "PE1"
                WebPage = "http://mlap3030.hrdc-drhc.net/Reports/Pages/Folder.aspx?ItemPath=%2fConfigMgr_PE1&ViewMode=Detail"
            Case "DV1"
                WebPage = "http://mlmsesd3881.hrdc-drhc.dev/Reports/Pages/Folder.aspx?ItemPath=%2fConfigMgr_DV1&ViewMode=Detail"
            Case "CE1"
                WebPage = "http://MNMS3274.caws-pasc.net/Reports/Pages/Folder.aspx?ItemPath=%2fConfigMgr_CE1&ViewMode=Detail"
        End Select
        Process.Start(WebPage)
    End Sub

    Private Sub cmd_Event_Viewer_Click(sender As Object, e As EventArgs)
        Dim proc As New Process
        Dim procSI As New ProcessStartInfo("eventvwr.exe", ComputerName)

        Try
            proc.StartInfo = procSI
            proc.Start()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmd_Computer_Management_Click(sender As Object, e As EventArgs)
        Try
            Process.Start("c:\windows\system32\compmgmt.msc", "/computer:\\" & ComputerName)
        Catch ex As Exception
            'Gestion de l'erreur
        End Try
    End Sub

    Private Sub cmd_Services_Click(sender As Object, e As EventArgs)
        Try
            Process.Start("c:\windows\system32\services.msc", "/computer:\\" & ComputerName)
        Catch ex As Exception
            'Gestion de l'erreur
        End Try
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub

    Private Sub RefreshServiceWindows(sender As Object, e As EventArgs) Handles cmd_Show_SW.Click
        RefreshServiceWindows()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub EventViewerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EventViewerToolStripMenuItem.Click
        Dim proc As New Process
        Dim procSI As New ProcessStartInfo("eventvwr.exe", ComputerName)

        Try
            proc.StartInfo = procSI
            proc.Start()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CMToolStripMenuItem.Click
        Try
            Process.Start("c:\windows\system32\compmgmt.msc", "/computer:\\" & ComputerName)
        Catch ex As Exception
            'Gestion de l'erreur
        End Try
    End Sub

    Private Sub ServicesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ServicesToolStripMenuItem.Click
        Try
            Process.Start("c:\windows\system32\services.msc", "/computer:\\" & ComputerName)
        Catch ex As Exception
            'Gestion de l'erreur
        End Try
    End Sub
    Private Sub WorkstationManagementToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WorkstationManagementToolStripMenuItem.Click
        Clipboard.SetText(ComputerName)
        MsgBox(My.Resources.MsgClipboard, MsgBoxStyle.Information)

        Dim WebPage = "http://workstationmgt/"
        Process.Start(WebPage)
        'Me.Close()
    End Sub



    Private Sub MainTab_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles MainTab.DrawItem
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = MainTab.TabPages(e.Index)
        Dim br As Brush
        Dim sf As New StringFormat

        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2)

        sf.Alignment = StringAlignment.Center

        Dim strTitle As String = tp.Text

        'If the current index is the Selected Index, change the color 
        If MainTab.SelectedIndex = e.Index And MainTab.SelectedIndex > 7 And Advance_mode = True Then

            'this is the background color of the tabpage header
            br = New SolidBrush(Color.Black) ' chnge to your choice
            g.FillRectangle(br, e.Bounds)

            'this is the foreground color of the text in the tab header
            br = New SolidBrush(Color.Black) ' change to your choice
            g.DrawString(strTitle, MainTab.Font, br, r, sf)
            tp.Text = ""
        ElseIf MainTab.SelectedIndex = e.Index Then

            'this is the background color of the tabpage header
            br = New SolidBrush(Color.Aquamarine) ' chnge to your choice
            g.FillRectangle(br, e.Bounds)

            'this is the foreground color of the text in the tab header
            br = New SolidBrush(Color.Black) ' change to your choice
            g.DrawString(strTitle, MainTab.Font, br, r, sf)
        Else

            'these are the colors for the unselected tab pages 
            br = New SolidBrush(Color.White) ' Change this to your preference
            g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, MainTab.Font, br, r, sf)

        End If
    End Sub

    Public Sub LoadTabPackageSubTab(selectedIndex As Integer)
        Me.Cursor = Cursors.WaitCursor
        Try
            Select Case selectedIndex
                Case 0
                    ''start page - do nothing
                Case 1
                    If loadExecutionAPPSTab = 0 Then

                        ShowExecutionHistoryAPPS()
                    End If

                Case 2
                    If loadExecutionPKGSTab = 0 Then

                        ShowExecutionHistoryPKGS()
                    End If

                Case 3
                    If loadRunningPKGSTab = 0 Then

                        ShowRunningPKGS()
                    End If

                Case 4
                    If loadAdvertisementsTab = 0 Then

                        ShowAdvertisements()
                    End If

                Case 5
                    If loadSoftwareCacheTab = 0 Then

                        RunESSetupInfo()
                    End If
            End Select

        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub Tab_pkg_app_SelectedIndexChanged(sendeer As Object, e As EventArgs) Handles Tab_pkg_app.SelectedIndexChanged
        LoadTabPackageSubTab(Tab_pkg_app.SelectedIndex)

    End Sub

    Public Sub MainTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MainTab.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Try
            Select Case MainTab.SelectedIndex
                Case 0 'TAB 1
                    'If loadExecutionPKGSTab < 1 Then
                    '    ShowExecutionHistoryPKGS()
                    'End If
                    'loadExecutionPKGSTab = loadExecutionPKGSTab + 1
                Case 1

                    'ShowExecutionHistoryPKGS()
                    'Me.Tab_pkg_app.SelectedIndex = Me.Tab_pkg_app.TabPages.IndexOf(START)
                    'Tab_pkg_app.SelectedTab = Me.Tab_pkg_app.Tabp

                    'ShowExecutionHistoryAPPS()
                    'loadExecutionAPPSTab = loadExecutionAPPSTab + 1
                Case 2
                    If loadRunningWSUS_SCUP < 1 Then
                        ShowRunningWSUS_SCUP()
                    End If
                    loadRunningPKGSTab = loadRunningPKGSTab + 1
                Case 3
                    'Me.ProgramsAndFeaturesSubTab.SelectedIndex = Me.ProgramsAndFeaturesSubTab.TabPages.IndexOf(PF_SUBTAB_OTHER)
                    Me.ListViewInstalledSoftware_NEW.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
                    Me.ListViewJava_NEW.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
                    Me.ListViewProcess_NEW.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
                    Me.ListViewServices_NEW.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)


                    'Affichage de la version du programme
                    Dim Version = Assembly.GetExecutingAssembly().GetName().Version
                    Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

                    Software_Hide = True
                    Me.Cursor = Cursors.Default
                    ShowProgramAndFeatures()

                    'Case 4
                    '    If loadInfoTab < 1 Then
                    '        ShowInfoTab()
                    '    End If
                    '    loadInfoTab = loadInfoTab + 1
                    '    'Case 5-8 go here
                    'Case 9
                    '    If loadServiceWindows < 10 Then
                    '        RefreshServiceWindows()
                    '    End If
                    '    loadServiceWindows = loadServiceWindows + 1
                Case 4
                    Dim Version = Assembly.GetExecutingAssembly().GetName().Version
                    Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)
                    All = False
                Case 5
                    TimerBar_Adv_clean = 0
                    TimerBar_Adv_clean_now = 0
                    bError = False
                    strComputer = UCase(ComputerName)

                    'Affichage de la version du programme
                    Dim Version = Assembly.GetExecutingAssembly().GetName().Version
                    Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

                    lbl_loading.Visible = False
                Case 6
                    RefreshServiceWindows()
                Case 7 ' RUN DOS COMMANDS
                    txt_ComputerName_NEW.Text = ComputerName

                    'Affichage de la version du programme
                    Dim Version = Assembly.GetExecutingAssembly().GetName().Version
                    Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

                    'Va chercher les info de la cache du client
                    Me.Cursor = Cursors.WaitCursor
                    SCache()
                    Me.Cursor = Cursors.Default

                    'Vérifie la configuration du BranchCache
                    Me.Cursor = Cursors.WaitCursor
                    SBranchCache()
                    Me.Cursor = Cursors.Default

                    'Affiche les imformation du Network du client
                    txt_IP_NEW.Text = IPAddress_Value
                    txt_MacAddress_NEW.Text = MacAddress(ComputerName)
                Case 8  ' LOG FILES
                    'groupBoxAdvMode1.Visible = True
                    'MW_Select = "NULL"
                    'If Advance_mode = True Or User.Contains("saadi") Then
                    'groupBoxAdvMode1.Visible = True

                    ' Else
                    'groupBoxAdvMode1.Visible = False
                    'End If
                Case 9 ' COLLECTION
                    ShowCollection()
            End Select

        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
        Me.Cursor = Cursors.Default
    End Sub



    Private Sub SCache()
        Me.Cursor = Cursors.WaitCursor

        Dim cache_size, cache_location
        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\ccm\SoftMgmtAgent")
        Dim Query As New SelectQuery("SELECT * FROM CacheConfig WHERE ConfigKey='Cache'")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)

        Dim info As ManagementObject

        cache_size = "0"
        cache_location = "0"

        Try
            For Each info In search.Get()
                cache_size = info("Size")
                cache_location = info("Location")
            Next
        Catch ex As Exception
            'Gestion de l'erreur
        End Try

        If cache_size <> 0 Then
            txt_Cache_Size_NEW.Text = Format(cache_size / 1024, "00.00")
            txt_cache_location_NEW.Text = cache_location
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub SBranchCache()
        Me.Cursor = Cursors.WaitCursor

        Dim regKey As RegistryKey
        Dim regSubKey As RegistryKey

        pic_greenflag1_NEW.Visible = False
        pic_greenflag2_NEW.Visible = False
        pic_redflag1_NEW.Visible = False
        pic_redflag2_NEW.Visible = False
        cmd_Port_8009_NEW.Enabled = False

        Try
            regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName)
            regSubKey = regKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\PeerDist\DownloadManager\Peers\Connection", True)

            If regSubKey Is Nothing Then
                'major problem, couldn't find the key
                regKey.CreateSubKey("Software\Microsoft\Windows NT\CurrentVersion\PeerDist\DownloadManager\Peers\Connection\")
                'open the newly created keypath
                regSubKey = regKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\PeerDist\DownloadManager\Peers\Connection", True)
                regSubKey.SetValue("ConnectPort", 8009, RegistryValueKind.DWord)
                regSubKey.SetValue("ListenPort", 8009, RegistryValueKind.DWord)

                Me.txt_ConnectPort_NEW.Text = regSubKey.GetValue("ConnectPort")
                Me.txt_ListenPort_NEW.Text = regSubKey.GetValue("ListenPort")
                If Not Me.txt_ConnectPort_NEW.Text = "8009" Then
                    Me.pic_redflag1_NEW.Visible = True
                    Me.pic_greenflag1_NEW.Visible = False

                Else
                    Me.pic_redflag1_NEW.Visible = False
                    Me.pic_greenflag1_NEW.Visible = True

                End If

                If Not Me.txt_ListenPort_NEW.Text = "8009" Then
                    Me.pic_redflag2_NEW.Visible = True
                    Me.pic_greenflag2_NEW.Visible = False

                Else
                    Me.pic_redflag2_NEW.Visible = False
                    Me.pic_greenflag2_NEW.Visible = True

                End If

                If Not Me.txt_ConnectPort_NEW.Text = "8009" Or Not Me.txt_ListenPort_NEW.Text = "8009" Then cmd_Port_8009_NEW.Enabled = True Else cmd_Port_8009_NEW.Enabled = False
            Else

                Me.txt_ConnectPort_NEW.Text = regSubKey.GetValue("ConnectPort")
                Me.txt_ListenPort_NEW.Text = regSubKey.GetValue("ListenPort")
                If Not Me.txt_ConnectPort_NEW.Text = "8009" Then
                    Me.pic_redflag1_NEW.Visible = True
                    Me.pic_greenflag1_NEW.Visible = False
                Else
                    Me.pic_redflag1_NEW.Visible = False
                    Me.pic_greenflag1_NEW.Visible = True
                End If

                If Not Me.txt_ListenPort_NEW.Text = "8009" Then
                    Me.pic_redflag2_NEW.Visible = True
                    Me.pic_greenflag2_NEW.Visible = False
                Else
                    Me.pic_redflag2_NEW.Visible = False
                    Me.pic_greenflag2_NEW.Visible = True
                End If

                If Not Me.txt_ConnectPort_NEW.Text = "8009" Or Not Me.txt_ListenPort_NEW.Text = "8009" Then cmd_Port_8009_NEW.Enabled = True Else cmd_Port_8009_NEW.Enabled = False
            End If

            regSubKey.Close()
            regKey.Close()

        Catch ex As Exception

        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Sub INSTALLED_SOFTWARE_TAB_DoubleClick(sender As Object, e As EventArgs) Handles INSTALLED_SOFTWARE_TAB.Click
        ShowInstalledSoftware()
    End Sub

    Sub ProgramsAndFeaturesSubTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProgramsAndFeaturesSubTab.SelectedIndexChanged, ProgramsAndFeaturesSubTab.DoubleClick
        Try
            Select Case ProgramsAndFeaturesSubTab.SelectedIndex
                Case 0 'TAB 1
                    If loadInstalledSoftwareTab < 1 Then
                        ShowInstalledSoftware()
                    End If

                Case 1
                    If loadJavaTab < 1 Then
                        ShowJava()
                    End If

                Case 2
                    If loadProcessTab < 1 Then
                        ShowProcess()
                    End If

                Case 3
                    If loadServiceTab < 1 Then
                        ShowService()
                    End If

                Case 4
                    'do nothing
            End Select

        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ShowService()
        onetime = 0
        Me.Refresh()
        If ListViewServices_NEW.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListViewServices_NEW.Items(0).Selected = True
            ListViewServices_NEW.Select()
            Label2.Visible = True
            'Exit Select
        End If
        ListViewServices_NEW.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True
        Label2.Visible = True

        Dim SName, SEtat, SType, SServiceName As String
        Dim SCount, count, countVal As Integer

        Try
            count = 0
            SCount = 0

            'Recherche l'utilisteur qui a partie le processus
            Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\CIMV2")
            Dim Query As New SelectQuery("SELECT * FROM Win32_Service")
            Dim search As New ManagementObjectSearcher(WMI_Info, Query)

            Dim info As ManagementObject

            For Each info In search.Get()
                SCount = SCount + 1
            Next

            Try
                For Each info In search.Get()
                    SName = info("Caption")
                    SEtat = info("State")
                    SType = info("StartMode")
                    SServiceName = info("Name")


                    'Ajout a la listview
                    ListViewServices_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                    Dim item As New ListViewItem(SName)
                    item.SubItems.Add(SEtat)
                    item.SubItems.Add(SType)
                    item.SubItems.Add(SServiceName)
                    ListViewServices_NEW.Items.Add(item)


                    count = count + 1

                    countVal = ((count + 1) / SCount) * 100
                    If countVal > 100 Or countVal < 0 Then countVal = 100
                    ProgressBar.Value = countVal
                Next
            Catch ex As Exception
                'Gestion de l'erreur
            End Try

        Catch ex As Exception
            'Gestion de l'erreur
        End Try

        ProgressBar.Visible = False
        onetime = 1

        'Commande pour le sort de la colonne
        Tab_Select = 4
        AddHandler Me.ListViewServices_NEW.ColumnClick, AddressOf ColumnClick
        If ListViewServices_NEW.Items.Count > 0 Then
            ListViewServices_NEW.Items(0).Selected = True
            ListViewServices_NEW.Select()
            loadServiceTab = 1
        End If
        Me.Refresh()
    End Sub

    Private Sub ShowProcess()
        onetime = 0
        Me.Refresh()
        If ListViewProcess_NEW.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListViewProcess_NEW.Items(0).Selected = True
            ListViewProcess_NEW.Select()
            Label2.Visible = True
            'Exit Select
        End If
        ListViewProcess_NEW.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True
        Label2.Visible = True

        Dim PName, PLocation, PUser, PID As String
        Dim PDate
        Dim PCount, count, countVal As Integer

        Try
            'recherche tout les processus sur l'ordinateur a distance et les additionne pour avoir le total
            PCount = Process.GetProcesses(ComputerName).Length
            count = 0

            'Recherche l'utilisteur qui a partie le processus
            Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\CIMV2")
            Dim Query As New SelectQuery("SELECT * FROM Win32_Process")
            Dim search As New ManagementObjectSearcher(WMI_Info, Query)

            Dim info As ManagementObject

            Try
                For Each info In search.Get()
                    PName = info("Caption")
                    PLocation = info("CommandLine")
                    PID = info("ProcessId")
                    PDate = WMIDateConvert(info("CreationDate"))

                    Try
                        Dim infoUser(2) As String
                        info.InvokeMethod("GetOwner", infoUser)
                        PUser = infoUser(0)
                    Catch ex As Exception
                        PUser = ""
                    End Try
                    'Ajout a la listview
                    ListViewProcess_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                    Dim item As New ListViewItem(PName)
                    item.SubItems.Add(PLocation)
                    item.SubItems.Add(PUser)
                    item.SubItems.Add(PDate)
                    item.SubItems.Add(PID)
                    ListViewProcess_NEW.Items.Add(item)
                    'Me.Refresh()

                    count = count + 1

                    countVal = ((count + 1) / PCount) * 100
                    If countVal > 100 Or countVal < 0 Then countVal = 100
                    ProgressBar.Value = countVal
                Next
            Catch ex As Exception
                'Gestion de l'erreur
            End Try

        Catch ex As Exception
            'Gestion de l'erreur
        End Try

        ProgressBar.Visible = False
        onetime = 1

        'Commande pour le sort de la colonne
        Tab_Select = 3
        AddHandler Me.ListViewProcess_NEW.ColumnClick, AddressOf ColumnClick

        'Active le autosize
        ColumnHeader6.Width = -2
        'ColumnHeader7.Width = -2
        ColumnHeader8.Width = -2
        ColumnHeader14.Width = -2
        ColumnHeader9.Width = -2

        If ListViewProcess_NEW.Items.Count > 0 Then
            ListViewProcess_NEW.Items(0).Selected = True
            ListViewProcess_NEW.Select()
            loadProcessTab = 1
        End If
        Me.Refresh()
    End Sub

    Private Sub ShowJava()
        'chk_ShowAll.Visible = False
        onetime = 0
        Me.Refresh()
        If ListViewJava_NEW.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListViewJava_NEW.Items(0).Selected = True
            ListViewJava_NEW.Select()
        End If
        ListViewJava_NEW.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True

        'Valide que ce se script ne passe que une fois
        If onetime = 1 Then Exit Sub

        Dim Key As RegistryKey = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(REG_Install_Programs_x86, False)
        Dim SubKeyName() As String = Key.GetSubKeyNames()
        Dim Index, count, countVal As Integer
        Dim SubKey As RegistryKey

        ProgressBar.Value = 1
        count = Key.SubKeyCount
        Me.Update()

        For Index = 0 To Key.SubKeyCount - 1
            SubKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(REG_Install_Programs_x86 + "\" + SubKeyName(Index), False)
            If Not SubKey.GetValue("Displayname", "") Is "" Then
                If Not CType(SubKey.GetValue("SystemComponent", ""), String) = "1" Then
                    If UCase(Microsoft.VisualBasic.Left(CType(SubKey.GetValue("Displayname", ""), String), 5)) = "JAVA " Then
                        Dim dispName As String = CType(SubKey.GetValue("Displayname", ""), String)
                        ListViewJava_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                        Dim item As New ListViewItem(dispName)
                        'item.SubItems.Add(ver)
                        ListViewJava_NEW.Items.Add(item)
                        Me.Update()
                    End If
                End If
            End If
            countVal = ((Index + 1) / count) * 100
            If countVal > 100 Or countVal < 0 Then countVal = 100
            ProgressBar.Value = countVal
        Next
        ProgressBar.Visible = False
        onetime = 1


        'Commande pour le sort de la colonne
        Tab_Select = 2
        AddHandler Me.ListViewJava_NEW.ColumnClick, AddressOf ColumnClick
        If ListViewJava_NEW.Items.Count > 0 Then
            ListViewJava_NEW.Items(0).Selected = True
            ListViewJava_NEW.Select()
            loadJavaTab = 1
        End If
        Me.Refresh()

    End Sub

    Private Sub ShowInstalledSoftware()
        'chk_ShowAll.Visible = True
        onetime = 0
        Me.Refresh()
        If ListViewInstalledSoftware_NEW.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListViewInstalledSoftware_NEW.Items(0).Selected = True
            ListViewInstalledSoftware_NEW.Select()
            'Label1lblProgFeatActivateTabMsg.Visible = True
            'Exit Select
        End If
        ListViewInstalledSoftware_NEW.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True

        Try
            'Valide que ce se script ne passe que une fois
            If onetime = 1 Then Exit Sub

            Dim Key_x86 As RegistryKey = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, ComputerName).OpenSubKey(REG_Install_Programs_x86, False)
            Dim SubKeyName_x86() As String = Key_x86.GetSubKeyNames()
            Dim SubKey_x86 As RegistryKey

            Dim Key_x64 As RegistryKey = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, ComputerName).OpenSubKey(REG_Install_Programs_x64, False)
            Dim SubKeyName_x64() As String = Key_x64.GetSubKeyNames()
            Dim SubKey_x64 As RegistryKey

            Dim Index_x86, Index_x64, count, countVal As Integer
            Dim AppName, AppVer, AppDate, AppVendor As String

            AppName = ""
            AppVer = ""
            AppDate = ""
            AppVendor = ""
            ProgressBar.Value = 1
            count = Key_x86.SubKeyCount + Key_x64.SubKeyCount
            Me.Update()

            'Gestion de la branche 32 Bits
            Try
                For Index_x86 = 0 To Key_x86.SubKeyCount - 1
                    SubKey_x86 = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(REG_Install_Programs_x86 + "\" + SubKeyName_x86(Index_x86), False)

                    If Not SubKey_x86.GetValue("DisplayName", "") Is "" Then
                        If Software_Hide = True Then
                            If CType(SubKey_x86.GetValue("SystemComponent", ""), String) = "1" Then
                                ' ne pas afficher car la case afficher tout n'a pas été activée
                            Else
                                AppName = CType(SubKey_x86.GetValue("DisplayName", ""), String)
                                AppVer = CType(SubKey_x86.GetValue("DisplayVersion", ""), String)
                                If Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 1, 1) = "2" Then AppDate = Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 1, 4) + "/" + Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 5, 2) + "/" + Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 7, 2) Else AppDate = ""
                                AppVendor = CType(SubKey_x86.GetValue("Publisher", ""), String)
                            End If
                        Else
                            If CType(SubKey_x86.GetValue("SystemComponent", ""), String) = "1" Then
                                AppName = CType(SubKey_x86.GetValue("DisplayName", ""), String) + " *"
                                AppVer = CType(SubKey_x86.GetValue("DisplayVersion", ""), String)
                                If Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 1, 1) = "2" Then AppDate = Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 1, 4) + "/" + Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 5, 2) + "/" + Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 7, 2) Else AppDate = ""
                                AppVendor = CType(SubKey_x86.GetValue("Publisher", ""), String)
                            Else
                                AppName = CType(SubKey_x86.GetValue("DisplayName", ""), String)
                                AppVer = CType(SubKey_x86.GetValue("DisplayVersion", ""), String)
                                If Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 1, 1) = "2" Then AppDate = Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 1, 4) + "/" + Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 5, 2) + "/" + Mid(CType(SubKey_x86.GetValue("InstallDate", ""), String), 7, 2) Else AppDate = ""
                                AppVendor = CType(SubKey_x86.GetValue("Publisher", ""), String)
                            End If
                        End If

                        ListViewInstalledSoftware_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                        Dim item As New ListViewItem(AppName)
                        item.SubItems.Add(AppVer)
                        item.SubItems.Add(AppDate)
                        item.SubItems.Add(AppVendor)
                        item.Name = AppName
                        'Validation des doublons
                        If ListViewInstalledSoftware_NEW.Items.Find(item.Name, False).Count >= 1 Then
                            'Doublons ne pas inscrire
                        Else
                            ListViewInstalledSoftware_NEW.Items.Add(item)
                        End If
                        Me.Update()

                    ElseIf Not SubKey_x86.GetValue("ParentDisplayName", "") Is "" Then

                        If Software_Hide = True Then
                            If CType(SubKey_x86.GetValue("SystemComponent", ""), String) = "1" Then
                                ' ne pas afficher car la case afficher tout n'a pas été activée
                            Else
                                AppName = CType(SubKey_x86.GetValue("ParentDisplayName", ""), String)
                                AppVer = ""
                                AppDate = ""
                                AppVendor = ""
                            End If
                        Else
                            If CType(SubKey_x86.GetValue("SystemComponent", ""), String) = "1" Then
                                AppName = CType(SubKey_x86.GetValue("ParentDisplayName", ""), String) + " *"
                                AppVer = ""
                                AppDate = ""
                                AppVendor = ""
                            Else
                                AppName = CType(SubKey_x86.GetValue("ParentDisplayName", ""), String)
                                AppVer = ""
                                AppDate = ""
                                AppVendor = ""
                            End If
                        End If

                        ListViewInstalledSoftware_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                        Dim item As New ListViewItem(AppName)
                        item.SubItems.Add(AppVer)
                        item.SubItems.Add(AppDate)
                        item.SubItems.Add(AppVendor)
                        item.Name = AppName
                        'Validation des doublons
                        If ListViewInstalledSoftware_NEW.Items.Find(item.Name, False).Count >= 1 Then
                            'Doublons ne pas inscrire
                        Else
                            ListViewInstalledSoftware_NEW.Items.Add(item)
                        End If
                        Me.Update()
                    Else
                        Dim AppName_temp As String = SubKeyName_x86(Index_x86).ToString
                        If Software_Hide = True Then
                            If CType(SubKey_x86.GetValue("SystemComponent", ""), String) = "1" Then
                                ' ne pas afficher car la case afficher tout n'a pas été activée
                            Else
                                Dim T1 = Mid(AppName_temp, 1, InStr(1, AppName_temp, "."))
                                If Not T1 = "" Then
                                    AppName = "Microsoft Windows Update " + AppName_temp.Replace(T1, "")
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = "Microsoft Corporation"
                                Else
                                    AppName = AppName_temp
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = ""
                                End If
                            End If
                        Else
                            If CType(SubKey_x86.GetValue("SystemComponent", ""), String) = "1" Then
                                Dim T1 = Mid(AppName_temp, 1, InStr(1, AppName_temp, "."))
                                If Not T1 = "" Then
                                    AppName = "Microsoft Windows Update " + AppName_temp.Replace(T1, "") + " *"
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = "Microsoft Corporation"
                                Else
                                    AppName = AppName_temp + " *"
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = ""
                                End If
                            Else
                                Dim T1 = Mid(AppName_temp, 1, InStr(1, AppName_temp, "."))
                                If Not T1 = "" Then
                                    AppName = "Microsoft Windows Update " + AppName_temp.Replace(T1, "")
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = "Microsoft Corporation"
                                Else
                                    AppName = AppName_temp
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = ""
                                End If
                            End If
                        End If

                        ListViewInstalledSoftware_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                        Dim item As New ListViewItem(AppName)
                        item.SubItems.Add(AppVer)
                        item.SubItems.Add(AppDate)
                        item.SubItems.Add(AppVendor)
                        item.Name = AppName
                        'Validation des doublons
                        If ListViewInstalledSoftware_NEW.Items.Find(item.Name, False).Count >= 1 Then
                            'Doublons ne pas inscrire
                        Else
                            ListViewInstalledSoftware_NEW.Items.Add(item)
                        End If
                        Me.Update()

                    End If
                    countVal = ((Index_x86 + Index_x64 + 1) / count) * 100
                    If countVal > 100 Or countVal < 0 Then countVal = 100
                    ProgressBar.Value = countVal
                Next
                onetime = 1
            Catch ex As Exception
                'Gestion de l'erreur 32 Bits
            End Try

            'Gestion de la branche 64 Bits
            Try
                For Index_x64 = 0 To Key_x64.SubKeyCount - 1
                    SubKey_x64 = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(REG_Install_Programs_x64 + "\" + SubKeyName_x64(Index_x64), False)

                    If Not SubKey_x64.GetValue("DisplayName", "") Is "" Then
                        If Software_Hide = True Then
                            If CType(SubKey_x64.GetValue("SystemComponent", ""), String) = "1" Then
                                ' ne pas afficher car la case afficher tout n'a pas été activée
                            Else
                                AppName = CType(SubKey_x64.GetValue("DisplayName", ""), String)
                                AppVer = CType(SubKey_x64.GetValue("DisplayVersion", ""), String)
                                If Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 1, 1) = "2" Then AppDate = Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 1, 4) + "/" + Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 5, 2) + "/" + Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 7, 2) Else AppDate = ""
                                AppVendor = CType(SubKey_x64.GetValue("Publisher", ""), String)
                            End If
                        Else
                            If CType(SubKey_x64.GetValue("SystemComponent", ""), String) = "1" Then
                                AppName = CType(SubKey_x64.GetValue("DisplayName", ""), String) + " *"
                                AppVer = CType(SubKey_x64.GetValue("DisplayVersion", ""), String)
                                If Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 1, 1) = "2" Then AppDate = Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 1, 4) + "/" + Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 5, 2) + "/" + Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 7, 2) Else AppDate = ""
                                AppVendor = CType(SubKey_x64.GetValue("Publisher", ""), String)
                            Else
                                AppName = CType(SubKey_x64.GetValue("DisplayName", ""), String)
                                AppVer = CType(SubKey_x64.GetValue("DisplayVersion", ""), String)
                                If Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 1, 1) = "2" Then AppDate = Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 1, 4) + "/" + Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 5, 2) + "/" + Mid(CType(SubKey_x64.GetValue("InstallDate", ""), String), 7, 2) Else AppDate = ""
                                AppVendor = CType(SubKey_x64.GetValue("Publisher", ""), String)
                            End If
                        End If

                        ListViewInstalledSoftware_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                        Dim item As New ListViewItem(AppName)
                        item.SubItems.Add(AppVer)
                        item.SubItems.Add(AppDate)
                        item.SubItems.Add(AppVendor)
                        item.Name = AppName
                        'Validation des doublons
                        If ListViewInstalledSoftware_NEW.Items.Find(item.Name, False).Count >= 1 Then
                            'Doublons ne pas inscrire
                        Else
                            ListViewInstalledSoftware_NEW.Items.Add(item)
                        End If
                        Me.Update()

                    ElseIf Not SubKey_x64.GetValue("ParentDisplayName", "") Is "" Then

                        If Software_Hide = True Then
                            If CType(SubKey_x64.GetValue("SystemComponent", ""), String) = "1" Then
                                ' ne pas afficher car la case afficher tout n'a pas été activée
                            Else
                                AppName = CType(SubKey_x64.GetValue("ParentDisplayName", ""), String)
                                AppVer = ""
                                AppDate = ""
                                AppVendor = ""
                            End If
                        Else
                            If CType(SubKey_x64.GetValue("SystemComponent", ""), String) = "1" Then
                                AppName = CType(SubKey_x64.GetValue("ParentDisplayName", ""), String) + " *"
                                AppVer = ""
                                AppDate = ""
                                AppVendor = ""
                            Else
                                AppName = CType(SubKey_x64.GetValue("ParentDisplayName", ""), String)
                                AppVer = ""
                                AppDate = ""
                                AppVendor = ""
                            End If
                        End If

                        ListViewInstalledSoftware_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                        Dim item As New ListViewItem(AppName)
                        item.SubItems.Add(AppVer)
                        item.SubItems.Add(AppDate)
                        item.SubItems.Add(AppVendor)
                        item.Name = AppName
                        'Validation des doublons
                        If ListViewInstalledSoftware_NEW.Items.Find(item.Name, False).Count >= 1 Then
                            'Doublons ne pas inscrire
                        Else
                            ListViewInstalledSoftware_NEW.Items.Add(item)
                        End If
                        Me.Update()
                    Else
                        Dim AppName_temp As String = SubKeyName_x64(Index_x64).ToString
                        If Software_Hide = True Then
                            If CType(SubKey_x64.GetValue("SystemComponent", ""), String) = "1" Then
                                ' ne pas afficher car la case afficher tout n'a pas été activée
                            Else
                                Dim T1 = Mid(AppName_temp, 1, InStr(1, AppName_temp, "."))
                                If Not T1 = "" Then
                                    AppName = "Microsoft Windows Update " + AppName_temp.Replace(T1, "")
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = "Microsoft Corporation"
                                Else
                                    AppName = AppName_temp
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = ""
                                End If
                            End If
                        Else
                            If CType(SubKey_x64.GetValue("SystemComponent", ""), String) = "1" Then
                                Dim T1 = Mid(AppName_temp, 1, InStr(1, AppName_temp, "."))
                                If Not T1 = "" Then
                                    AppName = "Microsoft Windows Update " + AppName_temp.Replace(T1, "") + " *"
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = "Microsoft Corporation"
                                Else
                                    AppName = AppName_temp + " *"
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = ""
                                End If
                            Else
                                Dim T1 = Mid(AppName_temp, 1, InStr(1, AppName_temp, "."))
                                If Not T1 = "" Then
                                    AppName = "Microsoft Windows Update " + AppName_temp.Replace(T1, "")
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = "Microsoft Corporation"
                                Else
                                    AppName = AppName_temp
                                    AppVer = ""
                                    AppDate = ""
                                    AppVendor = ""
                                End If
                            End If
                        End If

                        ListViewInstalledSoftware_NEW.Sorting = Windows.Forms.SortOrder.Ascending
                        Dim item As New ListViewItem(AppName)
                        item.SubItems.Add(AppVer)
                        item.SubItems.Add(AppDate)
                        item.SubItems.Add(AppVendor)
                        item.Name = AppName
                        'Validation des doublons
                        If ListViewInstalledSoftware_NEW.Items.Find(item.Name, False).Count >= 1 Then
                            'Doublons ne pas inscrire
                        Else
                            ListViewInstalledSoftware_NEW.Items.Add(item)
                        End If
                        Me.Update()

                    End If
                    countVal = ((Index_x86 + Index_x64 + 1) / count) * 100
                    If countVal > 100 Or countVal < 0 Then countVal = 100
                    ProgressBar.Value = countVal
                Next
                ProgressBar.Visible = False
                onetime = 1
            Catch ex As Exception
                'Gestion de l'erreur 64 Bits
            End Try
        Catch ex As Exception
            'Gestion de l'erreur
        End Try

        'Commande pour le sort de la colonne
        Tab_Select = 1
        AddHandler Me.ListViewInstalledSoftware_NEW.ColumnClick, AddressOf ColumnClick
        If ListViewInstalledSoftware_NEW.Items.Count > 0 Then
            ListViewInstalledSoftware_NEW.Items(0).Selected = True
            ListViewInstalledSoftware_NEW.Select()
            loadInstalledSoftwareTab = 1
        End If

        Me.Refresh()
    End Sub

    Private Sub ShowProgramAndFeatures()
        'Throw New NotImplementedException()
    End Sub

    Sub ShowRunningWSUS_SCUP()
        Me.Text = "SCCM PC Admin  " & ComputerName
        Me.Cursor = Cursors.WaitCursor

        'Active le tri par selection de colonne
        Me.ListViewWSUS_SCUP_NEW.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)

        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        onetime = 0
        ProgressBar.Value = 0

        chk_ApprovedPatch_NEW.Checked = True

        'ListViewWSUS_SCUP.Items.Clear()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ListViewWSUS_SCUP_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListViewWSUS_SCUP_NEW.ColumnClick
        ' Set the ListViewItemSorter property to a new ListViewItemComparer object.
        Dim sortIt As SortOrder

        If e.Column <> lastCol Then
            ListViewWSUS_SCUP_NEW.Sorting = SortOrder.Descending
            sortIt = SortOrder.Descending
        End If

        'ce souvin de la derniere colonne
        lastCol = e.Column

        If ListViewWSUS_SCUP_NEW.Sorting = SortOrder.Descending Then
            ListViewWSUS_SCUP_NEW.Sorting = SortOrder.Ascending
            sortIt = SortOrder.Ascending
        Else
            ListViewWSUS_SCUP_NEW.Sorting = SortOrder.Descending
            sortIt = SortOrder.Descending
        End If

        Me.ListViewWSUS_SCUP_NEW.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

    End Sub

    Private Sub Start_WSUS_Approved()
        Me.Cursor = Cursors.WaitCursor
        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\ccm\Policy\Machine\RequestedConfig")
        Dim Query As New SelectQuery("SELECT * FROM CCM_UpdateCIAssignment")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)

        Dim Index As Integer
        Dim AssignedCIs(), i

        i = 0
        ID = 0
        Index = 0

        'rechercher dans les deployements les updates approvées et les mais en variable pour les consulté
        'cette recherche permet de regarder que les patch approvée et relacher par SCCM
        Dim info As ManagementObject
        For Each info In search.Get()
            AssignedCIs = info("AssignedCIs")
            'calcule le nombre d'entrez et les recherches 1 a 1
            Do Until i = AssignedCIs.Length
                Dim str_temp, str_len, str_pos1
                str_temp = Trim(AssignedCIs(i))
                str_len = str_temp
                str_pos1 = InStr(1, str_temp, "ID") + 3
                str_WSUS_ID(ID) = Mid(str_temp, str_pos1, 36)
                i = i + 1
                ID = ID + 1
            Loop
            i = 0
        Next

        WSUS_Approved()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub WSUS_Approved()

        'Valide que se script ne passe que une fois
        If onetime = 1 Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\ccm\SoftwareUpdates\UpdatesStore")
        Dim Query As New SelectQuery("SELECT * FROM CCM_UpdateStatus")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)
        Dim Index, count, count_miss, count_WSUS_Approved, countVal, max_ID, i As Integer
        Dim Article, Bulletin, Status, AppName, GUI, UniqueID As String

        Dim info As ManagementObject
        Index = search.Get().Count
        count = 0
        count_miss = 0
        count_WSUS_Approved = 0
        max_ID = ID
        i = 0

        ' enum each entry
        ProgressBar.Value = 1
        Me.Update()

        For Each info In search.Get()
            UniqueID = info("UniqueId")
            Do Until i = max_ID
                If UniqueID = str_WSUS_ID(i) Then
                    Article = info("Article")
                    Bulletin = info("Bulletin")
                    Status = info("Status")
                    AppName = info("Title")
                    GUI = info("UpdateClassification")
                    If Not AppName Is "" Then
                        ListViewWSUS_SCUP_NEW.Sorting = Windows.Forms.SortOrder.Descending
                        Dim item As New ListViewItem(Article)
                        item.SubItems.Add(Bulletin)
                        item.SubItems.Add(Status)
                        item.SubItems.Add(AppName)

                        Select Case UCase(GUI)

                            Case "5C9376AB-8CE6-464A-B136-22113DD69801"
                                GUI = "Application"

                            Case "434DE588-ED14-48F5-8EED-A15E09A991F6"
                                GUI = "Connectors"

                            Case "E6CF1350-C01B-414D-A61F-263D14D133B4"
                                GUI = "CriticalUpdates"

                            Case "E0789628-CE08-4437-BE74-2495B842F43B"
                                GUI = "DefinitionUpdates"

                            Case "E140075D-8433-45C3-AD87-E72345B36078"
                                GUI = "DeveloperKits"

                            Case "B54E7D24-7ADD-428F-8B75-90A396FA584F"
                                GUI = "FeaturePacks"

                            Case "9511D615-35B2-47BB-927F-F73D8E9260BB"
                                GUI = "Guidance"

                            Case "0FA1201D-4330-4FA8-8AE9-B877473B6441"
                                GUI = "SecurityUpdates"

                            Case "68C5B0A3-D1A6-4553-AE49-01D3A7827828"
                                GUI = "ServicePacks"

                            Case "B4832BD8-E735-4761-8DAF-37F882276DAB"
                                GUI = "Tools"

                            Case "28BC880E-0592-4CBF-8F95-C79B17911D5F"
                                GUI = "UpdateRollups"

                            Case "CD5FFD1E-E932-4E3A-BF74-18BF0B1BBD83"
                                GUI = "Updates"

                            Case Else
                                GUI = "N/A"

                        End Select

                        item.SubItems.Add(GUI)
                        item.Name = AppName
                        count_WSUS_Approved = count_WSUS_Approved + 1
                        Me.Update()

                        'Validation des doublons
                        If ListViewWSUS_SCUP_NEW.Items.Find(item.Name, False).Count >= 1 Then
                            'Ne fait rien 
                        Else
                            If Status = "Missing" Then
                                'Mais la ligne en rouge si la valeur est manquante
                                item.BackColor = Color.DarkRed
                                item.ForeColor = Color.White
                                count_miss = count_miss + 1
                            End If
                            ListViewWSUS_SCUP_NEW.Items.Add(item)
                        End If
                        lbl_missing_NEW.Text = count_miss.ToString
                        lbl_patch_count_NEW.Text = count_WSUS_Approved.ToString

                    End If
                End If
                i = i + 1
            Loop
            i = 0
            count = count + 1
            countVal = (count / Index) * 100
            If countVal > 100 Or countVal < 0 Then countVal = 100
            ProgressBar.Value = countVal
            Me.Update()
        Next
        ProgressBar.Visible = False
        onetime = 1
        Me.Cursor = Cursors.Default

        If ListViewWSUS_SCUP_NEW.Items.Count > 0 Then
            ListViewWSUS_SCUP_NEW.Items(0).Selected = True
            ListViewWSUS_SCUP_NEW.Select()
        End If
        Me.Refresh()
    End Sub

    Private Sub WSUS_ALL()
        Me.Refresh()
        'Valide que ce se script ne passe que une fois
        If onetime = 1 Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\ccm\SoftwareUpdates\UpdatesStore")
        Dim Query As New SelectQuery("SELECT * FROM CCM_UpdateStatus")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)
        Dim Index, count, count2, count_miss, countVal As Integer
        Dim Article, Bulletin, Status, AppName, GUI As String
        count = 0
        count2 = 0
        count_miss = 0
        Index = 0

        Dim info As ManagementObject

        'count the numebr of record
        Index = search.Get().Count

        ' enum each entry
        ProgressBar.Value = 1
        Me.Update()
        For Each info In search.Get()
            Article = info("Article")
            Bulletin = info("Bulletin")
            Status = info("Status")
            AppName = info("Title")
            GUI = info("UpdateClassification")
            If Not AppName Is "" Then
                ListViewWSUS_SCUP_NEW.Sorting = Windows.Forms.SortOrder.Descending
                Dim item As New ListViewItem(Article)
                item.SubItems.Add(Bulletin)
                item.SubItems.Add(Status)
                item.SubItems.Add(AppName)

                Select Case UCase(GUI)

                    Case "5C9376AB-8CE6-464A-B136-22113DD69801"
                        GUI = "Application"

                    Case "434DE588-ED14-48F5-8EED-A15E09A991F6"
                        GUI = "Connectors"

                    Case "E6CF1350-C01B-414D-A61F-263D14D133B4"
                        GUI = "CriticalUpdates"

                    Case "E0789628-CE08-4437-BE74-2495B842F43B"
                        GUI = "DefinitionUpdates"

                    Case "E140075D-8433-45C3-AD87-E72345B36078"
                        GUI = "DeveloperKits"

                    Case "B54E7D24-7ADD-428F-8B75-90A396FA584F"
                        GUI = "FeaturePacks"

                    Case "9511D615-35B2-47BB-927F-F73D8E9260BB"
                        GUI = "Guidance"

                    Case "0FA1201D-4330-4FA8-8AE9-B877473B6441"
                        GUI = "SecurityUpdates"

                    Case "68C5B0A3-D1A6-4553-AE49-01D3A7827828"
                        GUI = "ServicePacks"

                    Case "B4832BD8-E735-4761-8DAF-37F882276DAB"
                        GUI = "Tools"

                    Case "28BC880E-0592-4CBF-8F95-C79B17911D5F"
                        GUI = "UpdateRollups"

                    Case "CD5FFD1E-E932-4E3A-BF74-18BF0B1BBD83"
                        GUI = "Updates"

                    Case Else
                        GUI = "N/A"

                End Select

                item.SubItems.Add(GUI)
                item.Name = AppName

                Me.Update()

                'Validation des doublons
                If ListViewWSUS_SCUP_NEW.Items.Find(item.Name, False).Count >= 1 Then
                    'Doublons ne pas inscrire mais augmenter la progression de la barre
                    count2 = count2 + 1
                Else
                    If Status = "Missing" Then
                        'Mais la ligne en rouge si la valeur est manquante
                        item.BackColor = Color.DarkRed
                        item.ForeColor = Color.White
                        count_miss = count_miss + 1
                    End If
                    ListViewWSUS_SCUP_NEW.Items.Add(item)
                    count = count + 1
                End If
                lbl_missing_NEW.Text = count_miss.ToString
                lbl_patch_count_NEW.Text = count.ToString

                countVal = ((count + count2) / Index) * 100
                If countVal > 100 Or countVal < 0 Then countVal = 100
                ProgressBar.Value = countVal

                Me.Update()
            End If
        Next
        ProgressBar.Visible = False
        onetime = 1

        Me.Cursor = Cursors.Default
        If ListViewWSUS_SCUP_NEW.Items.Count > 0 Then
            ListViewWSUS_SCUP_NEW.Items(0).Selected = True
            ListViewWSUS_SCUP_NEW.Select()
        End If
        Me.Refresh()
    End Sub

    Private Sub chk_ApprovedPatch_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ApprovedPatch_NEW.CheckedChanged
        onetime = 0
        ProgressBar.Value = 0
        ProgressBar.Visible = True
        ListViewWSUS_SCUP_NEW.Items.Clear()

        If chk_ApprovedPatch_NEW.CheckState = CheckState.Checked Then
            chk_ApprovedPatch_NEW.Checked = True
            Start_WSUS_Approved()
        Else
            chk_ApprovedPatch_NEW.Checked = False
            WSUS_ALL()
        End If

    End Sub

    Private Sub cmd_apps_refresh_Click(sender As Object, e As EventArgs) Handles cmd_apps_refresh_NEW.Click
        Dim popupRefreshWSUS As Popup_Refresh_WSUS = New Popup_Refresh_WSUS
        popupRefreshWSUS.ShowDialog(Me)
    End Sub

    Private Sub cmd_Refresh_Click(sender As Object, e As EventArgs) Handles cmd_Refresh_NEW.Click
        onetime = 0
        ProgressBar.Value = 0
        lstvw_ExecHistPkgs.Items.Clear()

        Me.Cursor = Cursors.WaitCursor
        chk_ApprovedPatch_NEW.Checked = True
        Start_WSUS_Approved()
        Me.Cursor = Cursors.Default

    End Sub

    Public Sub ShowExecutionHistoryPKGS()

        onetime = 0
        Me.Refresh()
        If lstvw_ExecHistPkgs.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            lstvw_ExecHistPkgs.Items(0).Selected = True
            lstvw_ExecHistPkgs.Select()
        End If
        lstvw_ExecHistPkgs.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True

        'Valide que ce se script ne passe que une fois
        If onetime = 1 Then Exit Sub

        Dim Key As RegistryKey = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, ComputerName).OpenSubKey(SCCM_PKG_HIST_REG_x86, False)
        Dim SubKeyName() As String = Key.GetSubKeyNames()
        Dim Index, count, countVal As Integer
        Dim SubKey As RegistryKey
        Dim SubLevel2_Name As String()
        Dim SubLevel2_Key As String
        Dim SubLevel3_Key As RegistryKey
        Dim Key_PkgName, Key_Date, Key_State As String
        Dim tasksequence As Boolean = False
        ProgressBar.Value = 1
        count = Key.SubKeyCount
        Me.Update()

        For Index = 0 To Key.SubKeyCount - 1
            SubKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(SCCM_PKG_HIST_REG_x86 + "\" + SubKeyName(Index), False)
            SubLevel2_Name = SubKey.GetSubKeyNames()
            For Each SubLevel2_Key In SubLevel2_Name
                SubLevel3_Key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(SCCM_PKG_HIST_REG_x86 + "\" + SubKeyName(Index) + "\" + SubLevel2_Key, False)


                Key_PkgName = SubLevel3_Key.GetValue("_ProgramID")

                'Vérification si ces un TS
                '***************************************************************************************************************
                If Key_PkgName = "*" Then
                    Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\root\Ccm\policy\machine\actualconfig")
                    Dim Query As New SelectQuery("SELECT * FROM CCM_SoftwareDistribution")
                    Dim search As New ManagementObjectSearcher(WMI_Info, Query)

                    Dim info As ManagementObject
                    For Each info In search.Get()
                        If SubKeyName(Index) = info("PKG_PackageID") Then
                            Key_PkgName = "(TS) " & info("PKG_Name")
                            tasksequence = True
                        End If
                    Next
                    If Key_PkgName = "*" Then
                        Key_PkgName = "(TS) " & "Task Sequence"
                        tasksequence = True
                    End If
                Else
                    Key_PkgName = SubLevel3_Key.GetValue("_ProgramID")
                    tasksequence = False
                End If
                '***************************************************************************************************************

                Key_Date = SubLevel3_Key.GetValue("_RunStartTime")
                Key_State = SubLevel3_Key.GetValue("_State")

                'lstvw_ExecHistPkgs.Sorting = Windows.Forms.SortOrder.Ascending
                Me.lstvw_ExecHistPkgs.Sorting = Windows.Forms.SortOrder.None

                Dim item As New ListViewItem(SubKeyName(Index))

                If tasksequence = True Then
                    item.BackColor = Color.LightBlue
                    item.ForeColor = Color.DarkBlue
                End If

                item.SubItems.Add(Key_PkgName)
                item.SubItems.Add(Key_State)

                If Key_State = "Failure" Then
                    item.BackColor = Color.DarkRed
                    item.ForeColor = Color.White
                End If

                item.SubItems.Add(Key_Date)
                lstvw_ExecHistPkgs.Items.Add(item)

                Me.Update()
                countVal = ((Index + 1) / count) * 100
                If countVal > 100 Or countVal < 0 Then countVal = 100
                ProgressBar.Value = countVal
            Next
        Next
        ProgressBar.Visible = False
        onetime = 1

        'Commande pour le sort de la colonne
        Tab_Select = 1
        AddHandler Me.lstvw_ExecHistPkgs.ColumnClick, AddressOf ColumnClick


        If lstvw_ExecHistPkgs.Items.Count > 0 Then
            lstvw_ExecHistPkgs.Items(0).Selected = True
            lstvw_ExecHistPkgs.Select()
        End If
        Me.Refresh()


    End Sub

    Private Sub ColumnClick(sender As Object, e As ColumnClickEventArgs)
        ' Set the ListViewItemSorter property to a new ListViewItemComparer object.
        Dim sortIt As SortOrder

        If e.Column <> lastCol Then

            Select Case Tab_Select
                Case 1
                    lstvw_ExecHistPkgs.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                Case 2
                    listvw_ExecHistApps.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                Case 3
                    ListView_RunningPackages_NEW.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                Case 4
                    ListView_ProgramsFeatures_NEW.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                    'Case 5
                    '    ListView5.Sorting = SortOrder.Descending
                    '    sortIt = SortOrder.Descending
            End Select

        End If

        'ce souvin de la derniere colonne
        lastCol = e.Column

        Select Case Tab_Select

            Case 1
                If lstvw_ExecHistPkgs.Sorting = SortOrder.Descending Then
                    lstvw_ExecHistPkgs.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    lstvw_ExecHistPkgs.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.lstvw_ExecHistPkgs.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

            Case 2
                If listvw_ExecHistApps.Sorting = SortOrder.Descending Then
                    listvw_ExecHistApps.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    listvw_ExecHistApps.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.listvw_ExecHistApps.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

            Case 3
                If ListView_RunningPackages_NEW.Sorting = SortOrder.Descending Then
                    ListView_RunningPackages_NEW.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    ListView_RunningPackages_NEW.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.ListView_RunningPackages_NEW.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

            Case 4
                If ListView_ProgramsFeatures_NEW.Sorting = SortOrder.Descending Then
                    ListView_ProgramsFeatures_NEW.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    ListView_ProgramsFeatures_NEW.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.ListView_ProgramsFeatures_NEW.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

                'Case 5
                '    If ListView5.Sorting = SortOrder.Descending Then
                '        ListView5.Sorting = SortOrder.Ascending
                '        sortIt = SortOrder.Ascending
                '    Else
                '        ListView5.Sorting = SortOrder.Descending
                '        sortIt = SortOrder.Descending
                '    End If

                '    Me.ListView5.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)
        End Select

    End Sub


    Sub ShowExecutionHistoryAPPS()
        onetime = 0
        Me.Refresh()

        If listvw_ExecHistApps.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            listvw_ExecHistApps.Items(0).Selected = True
            listvw_ExecHistApps.Select()
            'cmd_apps_refresh.Visible = True
            'Exit Select
        End If

        listvw_ExecHistApps.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True
        'cmd_apps_refresh.Visible = True

        Try
            'Valide que ce se script ne passe que une fois
            If onetime = 1 Then Exit Sub

            Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\root\ccm\ClientSDK")
            Dim Query As New SelectQuery("SELECT * FROM CCM_Application")
            Dim search As New ManagementObjectSearcher(WMI_Info, Query)
            Dim Index, count, countVal As Integer
            Dim AppName, AppStatus, Date_Start, Date_Dealine, Date_LastEvalTime, Date_LastInstallTime As String
            Dim EvaluationState = ""
            Dim EvaluationState_Code
            count = 0
            Index = 0

            'count numer of record
            Dim info As ManagementObject
            count = search.Get().Count

            ' enum each entry
            ProgressBar.Value = 1
            Me.Update()
            For Each info In search.Get()
                AppName = info("FullName")
                AppStatus = info("InstallState")
                EvaluationState_Code = info("EvaluationState")

                Date_Start = WMIDateConvert(info("StartTime"))
                Date_Dealine = WMIDateConvert(info("Deadline"))
                Date_LastEvalTime = WMIDateConvert(info("LastEvalTime"))
                Date_LastInstallTime = WMIDateConvert(info("LastInstallTime"))

                If Date_Start = "00:00:00" Or Date_Start = "12:00:00 AM" Then Date_Start = " "
                If Date_Dealine = "00:00:00" Or Date_Dealine = "12:00:00 AM" Then Date_Dealine = " "
                If Date_LastEvalTime = "00:00:00" Or Date_LastEvalTime = "12:00:00 AM" Then Date_LastEvalTime = " "
                If Date_LastInstallTime = "00:00:00" Or Date_LastInstallTime = "12:00:00 AM" Then Date_LastInstallTime = " "

                Try
                    EvaluationState = ""
                    If Not EvaluationState_Code = vbEmpty Then

                        Select Case EvaluationState_Code

                            Case 0
                                EvaluationState = My.Resources.EvaluationState0
                            Case 1
                                EvaluationState = My.Resources.EvaluationState1
                            Case 2
                                EvaluationState = My.Resources.EvaluationState2
                            Case 3
                                EvaluationState = My.Resources.EvaluationState3
                            Case 4
                                EvaluationState = My.Resources.EvaluationState4
                            Case 5
                                EvaluationState = My.Resources.EvaluationState5
                            Case 6
                                EvaluationState = My.Resources.EvaluationState6
                            Case 7
                                EvaluationState = My.Resources.EvaluationState7
                            Case 8
                                EvaluationState = My.Resources.EvaluationState8
                            Case 9
                                EvaluationState = My.Resources.EvaluationState9
                            Case 10
                                EvaluationState = My.Resources.EvaluationState10
                            Case 11
                                EvaluationState = My.Resources.EvaluationState11
                            Case 12
                                EvaluationState = My.Resources.EvaluationState12
                            Case 13
                                EvaluationState = My.Resources.EvaluationState13
                            Case 14
                                EvaluationState = My.Resources.EvaluationState14
                            Case 15
                                EvaluationState = My.Resources.EvaluationState15
                            Case 16
                                EvaluationState = My.Resources.EvaluationState16
                            Case 17
                                EvaluationState = My.Resources.EvaluationState17
                            Case 18
                                EvaluationState = My.Resources.EvaluationState18
                            Case 19
                                EvaluationState = My.Resources.EvaluationState19
                            Case 20
                                EvaluationState = My.Resources.EvaluationState20
                            Case 21
                                EvaluationState = My.Resources.EvaluationState21
                            Case 22
                                EvaluationState = My.Resources.EvaluationState22
                            Case 23
                                EvaluationState = My.Resources.EvaluationState23
                            Case 24
                                EvaluationState = My.Resources.EvaluationState24
                            Case 25
                                EvaluationState = My.Resources.EvaluationState25
                            Case 26
                                EvaluationState = My.Resources.EvaluationState26
                            Case 27
                                EvaluationState = My.Resources.EvaluationState27
                            Case 28
                                EvaluationState = My.Resources.EvaluationState28
                            Case Else
                                EvaluationState = My.Resources.EvaluationStateElse
                        End Select


                    End If
                Catch ex As Exception
                    EvaluationState = My.Resources.EvaluationState0
                End Try


                If Not AppName Is "" Then
                    'ListView2.Sorting = Windows.Forms.SortOrder.Ascending
                    Me.listvw_ExecHistApps.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(AppName)
                    item.SubItems.Add(AppStatus)
                    item.SubItems.Add(EvaluationState)
                    item.SubItems.Add(Date_Start)
                    item.SubItems.Add(Date_Dealine)
                    item.SubItems.Add(Date_LastEvalTime)
                    item.SubItems.Add(Date_LastInstallTime)
                    listvw_ExecHistApps.Items.Add(item)
                    Me.Update()
                End If
                countVal = ((Index + 1) / count) * 100
                If countVal > 100 Or countVal < 0 Then countVal = 100
                ProgressBar.Value = countVal
                Index = Index + 1
                Me.Update()
            Next
            ProgressBar.Visible = False
            onetime = 1
        Catch ex As Exception
            ProgressBar.Value = 100
            ProgressBar.Visible = False
            onetime = 1
            'cmd_apps_refresh.Visible = False
            'GEstion de l'erreur
        End Try

        'Commande pour le sort de la colonne
        Tab_Select = 2
        AddHandler Me.listvw_ExecHistApps.ColumnClick, AddressOf ColumnClick

        'Active le autosize
        ColumnHeader5.Width = -2
        ColumnHeader6.Width = -2
        ColumnHeader17.Width = -2
        ColumnHeader18.Width = -2
        ColumnHeader19.Width = -2
        ColumnHeader20.Width = -2
        ColumnHeader21.Width = -2

        If listvw_ExecHistApps.Items.Count > 0 Then
            listvw_ExecHistApps.Items(0).Selected = True
            listvw_ExecHistApps.Select()
            loadExecutionAPPSTab = 1
        End If
        Me.Refresh()

    End Sub

    Sub ShowRunningPKGS()
        onetime = 0
        Me.Refresh()

        If ListView_RunningPackages_NEW.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView_RunningPackages_NEW.Items(0).Selected = True
            ListView_RunningPackages_NEW.Select()
            'Exit Select
        End If

        ListView_RunningPackages_NEW.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True

        Try
            'Valide que ce se script ne passe que une fois
            If onetime = 1 Then Exit Sub

            Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\root\Ccm\softmgmtagent")
            Dim Query As New SelectQuery("SELECT * FROM CCM_ExecutionRequestEx")
            Dim search As New ManagementObjectSearcher(WMI_Info, Query)
            Dim Index, count, countVal As Integer
            Dim AppName, AppStatus, AppID, AppDate As String
            count = 0
            Index = 0

            Dim tasksequence As Boolean = False
            Dim info As ManagementObject
            count = search.Get().Count

            ' enum each entry
            ProgressBar.Value = 1
            Me.Update()
            For Each info In search.Get()
                AppName = info("ProgramID")
                AppID = info("ContentID")

                If info("ProgramID") = "*" Then
                    AppName = "(TS) " & info("MIFPackageName")
                    tasksequence = True
                Else
                    AppName = info("ProgramID")
                    tasksequence = False
                End If

                ' enum each entry
                AppStatus = info("State")
                AppDate = Mid(info("ReceivedTime"), 1, 4) + "/" + Mid(info("ReceivedTime"), 5, 2) + "/" + Mid(info("ReceivedTime"), 7, 2)

                If Not AppName Is "" Then
                    'ListView3.Sorting = Windows.Forms.SortOrder.Ascending
                    Me.ListView_RunningPackages_NEW.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(AppID)
                    item.SubItems.Add(AppName)
                    item.SubItems.Add(AppStatus)
                    item.SubItems.Add(AppDate)
                    If tasksequence = True Then
                        item.BackColor = Color.LightBlue
                        item.ForeColor = Color.DarkBlue
                    End If
                    ListView_RunningPackages_NEW.Items.Add(item)
                    Me.Update()
                End If
                countVal = ((Index + 1) / count) * 100
                If countVal > 100 Or countVal < 0 Then countVal = 100
                ProgressBar.Value = countVal
                Index = Index + 1
                Me.Update()
            Next
            ProgressBar.Visible = False
            onetime = 1
            loadRunningPKGSTab = 1
        Catch ex As Exception
            'GEstion de l'erreur
        End Try

        'Commande pour le sort de la colonne
        Tab_Select = 3
        AddHandler Me.ListView_RunningPackages_NEW.ColumnClick, AddressOf ColumnClick
        If ListView_RunningPackages_NEW.Items.Count > 0 Then
            ListView_RunningPackages_NEW.Items(0).Selected = True
            ListView_RunningPackages_NEW.Select()
        End If
        Me.Refresh()

    End Sub

    Sub ShowAdvertisements()
        onetime = 0
        Me.Refresh()

        If ListView_ProgramsFeatures_NEW.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView_ProgramsFeatures_NEW.Items(0).Selected = True
            ListView_ProgramsFeatures_NEW.Select()
            lbl_UserLoggedIn_NEW.Visible = True
            'Exit Select
        End If

        ListView_ProgramsFeatures_NEW.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True

        Try
            'Valide que ce se script ne passe que une fois
            If onetime = 1 Then Exit Sub

            Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\root\Ccm\policy\machine\actualconfig")
            Dim Query As New SelectQuery("SELECT * FROM CCM_SoftwareDistribution")
            Dim search As New ManagementObjectSearcher(WMI_Info, Query)
            Dim Index, count, countVal As Integer
            Dim AppName, AppID, AppAdv As String
            count = 0
            Index = 0

            'count numer of record
            Dim tasksequence As Boolean = False
            Dim info As ManagementObject
            count = search.Get().Count

            ' enum each entry
            ProgressBar.Value = 1
            Me.Update()
            For Each info In search.Get()
                AppAdv = info("ADV_AdvertisementID")
                AppID = info("PKG_PackageID")

                If info("PRG_ProgramID") = "*" Then
                    AppName = "(TS) " & info("PKG_Name")
                    tasksequence = True
                Else
                    AppName = info("PRG_ProgramID")
                    tasksequence = False
                End If


                If Not AppName Is "" Then
                    'ListView2.Sorting = Windows.Forms.SortOrder.Ascending
                    Me.ListView_ProgramsFeatures_NEW.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(AppID)
                    item.SubItems.Add(AppName)
                    item.SubItems.Add(AppAdv)
                    If tasksequence = True Then
                        item.BackColor = Color.LightBlue
                        item.ForeColor = Color.DarkBlue
                    End If

                    If Not Microsoft.VisualBasic.Left(info("PKG_Name"), 1) = "*" Then ListView_ProgramsFeatures_NEW.Items.Add(item)
                    Me.Update()
                End If
                countVal = ((Index + 1) / count) * 100
                If countVal > 100 Or countVal < 0 Then countVal = 100
                ProgressBar.Value = countVal
                Index = Index + 1
                Me.Update()
            Next
            ProgressBar.Visible = False
            onetime = 1
            loadAdvertisementsTab = 1
        Catch ex As Exception
            'GEstion de l'erreur
        End Try

        'Commande pour le sort de la colonne
        Tab_Select = 4
        AddHandler Me.ListViewWSUS_SCUP_NEW.ColumnClick, AddressOf ColumnClick
        lbl_UserLoggedIn_NEW.Visible = True
        If ListView_ProgramsFeatures_NEW.Items.Count > 0 Then
            ListView_ProgramsFeatures_NEW.Items(0).Selected = True
            ListView_ProgramsFeatures_NEW.Select()
        End If
        Me.Refresh()

    End Sub

    Sub ShowInfoTab()

        onetime = 0
        Me.Refresh()

        If ListView_SoftwareLocation_NEW.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView_SoftwareLocation_NEW.Items(0).Selected = True
            ListView_SoftwareLocation_NEW.Select()
            'Exit Select
        End If

        ListView_SoftwareLocation_NEW.Items.Clear()
        ProgressBar.Value = 0
        ProgressBar.Visible = True

        Try
            'Valide que ce se script ne passe que une fois
            If onetime = 1 Then Exit Sub

            Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\ccm\SoftMgmtAgent")
            Dim Query As New SelectQuery("SELECT * FROM CacheInfoEx")
            Dim search As New ManagementObjectSearcher(WMI_Info, Query)
            Dim Index, count, countVal As Integer
            Dim AppCacheID, AppContentID, AppLocation As String
            count = 0
            Index = 0

            'count numer of record
            Dim info As ManagementObject
            count = search.Get().Count

            ' enum each entry
            ProgressBar.Value = 1
            Me.Update()
            For Each info In search.Get()
                AppCacheID = info("CacheId")
                AppContentID = info("ContentId")
                AppLocation = info("Location")

                If Not AppCacheID Is "" Then
                    Me.ListView_SoftwareLocation_NEW.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(AppContentID)
                    item.SubItems.Add(AppLocation)
                    item.SubItems.Add(AppCacheID)
                    ListView_SoftwareLocation_NEW.Items.Add(item)
                    Me.Update()
                End If
                countVal = ((Index + 1) / count) * 100
                If countVal > 100 Or countVal < 0 Then countVal = 100
                ProgressBar.Value = countVal
                Index = Index + 1
                Me.Update()
            Next
            ProgressBar.Visible = False
            onetime = 1

        Catch ex As Exception
            'Gestion de l'erreur
        End Try

        Tab_Select = 5
        AddHandler Me.ListView_SoftwareLocation_NEW.ColumnClick, AddressOf ColumnClick
        'Label1.Visible = True
        If ListView_SoftwareLocation_NEW.Items.Count > 0 Then
            ListView_SoftwareLocation_NEW.Items(0).Selected = True
            ListView_SoftwareLocation_NEW.Select()
        End If
        Me.Refresh()
    End Sub

    Private Sub ListExecutionHistoryPKGS_DoubleClick(sender As Object, e As EventArgs) Handles Tab_pkg_app.DoubleClick
        Select Case Tab_pkg_app.SelectedIndex
            Case 0
                    ''start page - do nothing
            Case 1
                loadExecutionAPPSTab = 0
            Case 2
                loadExecutionPKGSTab = 0
            Case 3
                loadRunningPKGSTab = 0
            Case 4
                loadAdvertisementsTab = 0
            Case 5
                loadSoftwareCacheTab = 0
        End Select
        LoadTabPackageSubTab(Tab_pkg_app.SelectedIndex)
    End Sub




    Private Sub LoadMorePcInfo()
        Me.Cursor = Cursors.WaitCursor

        'reset first
        Me.Text = "SCCM PC Admin  " & ComputerName
        Me.lbl_Version.Text = ""
        txt_img_ver.Text = ""
        txt_SRU_Verimg.Text = ""
        'txt_last_reboot.Text = WMIDateConvert(str_LastBootUpTime)
        'txt_img_install_Date.Text = WMIDateConvert(str_InstallDate)
        txt_Domain_NEW.Text = ""
        txt_OSCaption_NEW.Text = ""
        txt_EquipmentType.Text = ""
        txt_Vendor.Text = ""
        txt_Name.Text = ""
        txt_RAM.Text = ""
        txt_CPU.Text = ""

        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Dim WMI_Info3 As New ManagementScope("\\" & ComputerName & "\ROOT\CIMV2")
        Dim Query3 As New SelectQuery("SELECT * FROM Win32_Processor")
        Dim search3 As New ManagementObjectSearcher(WMI_Info3, Query3)

        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        'txt_ComputerName.Text = ComputerName

        txt_img_ver.Text = VerImg_data
        txt_SRU_Verimg.Text = SRU_VerImg_data
        'txt_last_reboot.Text = WMIDateConvert(str_LastBootUpTime)
        'txt_img_install_Date.Text = WMIDateConvert(str_InstallDate)
        txt_Domain_NEW.Text = PC_Domain
        txt_OSCaption_NEW.Text = OSName
        'txt_IP.Text = IPAddress_Value

        If m_strChassisTypes = "MOBILE_DEVICE" Then
            txt_EquipmentType.Text = My.Resources.txt_TypePC_text_mobile
        ElseIf m_strChassisTypes = "DESKTOP" Then
            txt_EquipmentType.Text = My.Resources.txt_TypePC_text_desktop
        Else
            txt_EquipmentType.Text = m_strChassisTypes
        End If

        'If OSLanguage = "1036" Then
        '    txt_language.Text = My.Resources.txt_language_text_fr
        'ElseIf OSLanguage = "1033" Then
        '    txt_language.Text = My.Resources.txt_language_text_en
        'End If


        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\CIMV2")
        Dim Query As New SelectQuery("SELECT * FROM Win32_ComputerSystemProduct")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)

        Dim info As ManagementObject

        Try
            For Each info In search.Get()
                txt_Vendor.Text = info("Vendor")
                txt_Name.Text = info("Name")
                'txt_Version.Text = info("Version")
            Next
        Catch ex As Exception

        End Try

        txt_RAM.Text = PC_Mem_size

        Dim info3 As ManagementObject

        Try
            For Each info3 In search3.Get()
                txt_CPU.Text = info3("Name")
            Next

        Catch ex As Exception

        End Try

        ''Membership du PC

        Try
            Me.MembershipListView.Text = ""
            Dim Group_Val As String = ""
            Dim allMemberships As String = ""
            Using ctx As New PrincipalContext(ContextType.Domain)
                Using p = Principal.FindByIdentity(ctx, ComputerName)
                    If Not p Is Nothing Then
                        Dim groups = p.GetGroups()
                        For Each group In groups
                            Group_Val = group.DisplayName & vbCrLf
                            If Not Group_Val = "" Then
                                'ListView3.Sorting = Windows.Forms.SortOrder.Ascending
                                'Me.MembershipListView.Sorting = Windows.Forms.SortOrder.None
                                'Dim item As New ListViewItem(Group_Val)
                                'MembershipListView.Items.Add(item)
                                allMemberships = allMemberships & group.DisplayName & "##"
                                txt_LogWindow.Text = txt_LogWindow.Text & group.DisplayName

                            End If
                            Me.Update()
                        Next
                    End If
                End Using
            End Using
            Dim arrayMemberships = allMemberships.Split("##")
            Array.Sort(arrayMemberships)
            For Each membership In arrayMemberships
                If Not (membership.Equals("")) Then
                    Me.MembershipListView.Text = Me.MembershipListView.Text & membership & vbCrLf
                End If

            Next

            Me.Update()
        Catch ex As Exception
            'GEstion de l'erreur
        End Try


        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnCenterConsole_Click(sender As Object, e As EventArgs) Handles btnCenterConsole.Click

        MainTab.Width = 1437
        'SecondaryTab.Hide()
        MainTab.Refresh()
        btnCenterConsole.Hide()
        btnCenterConsole2.Show()
        Me.Refresh()
    End Sub

    Private Sub btnCenterConsole2_Click(sender As Object, e As EventArgs) Handles btnCenterConsole2.Click

        MainTab.Width = 773
        'SecondaryTab.Show()
        MainTab.Refresh()
        btnCenterConsole.Show()
        btnCenterConsole2.Hide()
        Me.Refresh()
    End Sub

    Private Sub UserGuideToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UserGuideToolStripMenuItem1.Click

    End Sub


    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim aboutForm As About = New About
        aboutForm.ShowDialog()
    End Sub

    Private Sub Menu_Option_Click(sender As Object, e As EventArgs) Handles Menu_Option.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txt_LogWindow.TextChanged

    End Sub

    Private Sub Lbl_CompName_NEW_Click(sender As Object, e As EventArgs) Handles lbl_PCName_NEW.Click

    End Sub

    Private Sub CompInfoGroupBox_Enter(sender As Object, e As EventArgs) Handles CompInfoGroupBox.Enter

    End Sub

    Private Sub Txt_img_install_Date_TextChanged(sender As Object, e As EventArgs) Handles txt_img_install_Date.TextChanged

    End Sub


    Private Sub FORCESECURITYUPDATEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FORCESECURITYUPDATEToolStripMenuItem.Click

    End Sub

    Private Sub FORCEAPPLICATIONUPDATEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FORCEAPPLICATIONUPDATEToolStripMenuItem.Click

    End Sub

    Private Sub REBOOTREMOTECOMPUTERToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles REBOOTREMOTECOMPUTERToolStripMenuItem.Click

    End Sub

    Private Sub REMOTEASSISTANCEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles REMOTEASSISTANCEToolStripMenuItem.Click

    End Sub

    Private Sub EXPLORERToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EXPLORERToolStripMenuItem.Click

    End Sub

    Private Sub REMOTEDESKTOPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles REMOTEDESKTOPToolStripMenuItem.Click

    End Sub

    Private Sub ListViewServices_NEW_DoubleClick(sender As Object, e As EventArgs) Handles ListViewServices_NEW.DoubleClick
        Dim PName, PServiceName, PStats

        PName = ListViewServices_NEW.SelectedItems.Item(0).SubItems(0).Text
        PServiceName = ListViewServices_NEW.SelectedItems.Item(0).SubItems(3).Text
        PStats = ListViewServices_NEW.SelectedItems.Item(0).SubItems(1).Text

        If PStats = "Running" Then
            Dim Result As Integer = MsgBox(My.Resources.ConfirmStopService & PName, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
            If Result = 6 Then
                Service_to_OFF(PServiceName, True)
                If Service_ON_OFF = "OFF" Then
                    Me.ListViewServices_NEW.SelectedItems.Item(0).SubItems(1).Text = "Stopped"
                End If
                Me.Refresh()
            Else
                ' ne fait rien car le client a dit NON
            End If

        ElseIf PStats = "Stopped" Then
            Dim Result As Integer = MsgBox(My.Resources.ConfirmStartService & PName, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
            If Result = 6 Then
                Service_to_ON(PServiceName, True)
                If Service_ON_OFF = "ON" Then
                    Me.ListViewServices_NEW.SelectedItems.Item(0).SubItems(1).Text = "Running"
                End If
                Me.Refresh()
            Else
                ' ne fait rien car le client a dit NON
            End If

        End If
    End Sub

    Private Sub ListViewProcess_NEW_DoubleClick(sender As Object, e As EventArgs) Handles ListViewProcess_NEW.DoubleClick
        Dim PID, PName
        PID = ListViewProcess_NEW.SelectedItems.Item(0).SubItems(4).Text
        PName = ListViewProcess_NEW.SelectedItems.Item(0).SubItems(0).Text

        Try
            Dim Result As Integer = MsgBox(My.Resources.ConfirmStopProcess & PName, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
            If Result = 6 Then

                'Dim remoteById As Process = Process.GetProcessById(PID, ComputerName)
                'remoteById.Kill()
                ' envoye par commande dos car le process .Kill ne marche pas sur un poste remote...
                Dim myCMDLine As String = "taskkill /s " & ComputerName & " /PID " & PID
                Shell(myCMDLine, AppWinStyle.Hide)
                Thread.Sleep(1000)
                ListViewProcess_NEW.SelectedItems.Item(0).Remove()
            Else
                ' ne fait rien car le client a dit NON
            End If

        Catch ex As Exception
            'Gestion de l'erreur
        End Try

    End Sub
    Private Sub GCProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GCProfileToolStripMenuItem.Click
        Dim WebPage = ("http://gcprofilelog?wsname=" & ComputerName)
        Process.Start(WebPage)
    End Sub

    Private Sub GCProfileInfoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GCProfileLogToolStripMenuItem.Click
        Dim WebPage = "http://gcprofilelog/"
        Process.Start(WebPage)
    End Sub

    Private Sub GCProfilePCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GCProfilePCToolStripMenuItem.Click
        Dim WebPage = ("http://gcprofilelog?wsname=" & ComputerName)
        Process.Start(WebPage)
    End Sub

    Private Sub GCProfileToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GCProfileUserToolStripMenuItem1.Click
        Dim WebPage = ("http://gcprofilelog?username=" & Trim(User))
        Process.Start(WebPage)
    End Sub

    Public Function executeCommand(ByVal serverName As String, ByVal username As String, ByVal password As String, ByVal domain As String, ByVal command As String) As String
        Try
            Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
            Dim startInfo As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo()
            startInfo.RedirectStandardOutput = True
            startInfo.UseShellExecute = False
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            startInfo.FileName = "cmd.exe"
            startInfo.CreateNoWindow = True
            Dim cmdSetup = " cmd.exe /c "

            If username IsNot Nothing Then

                If domain IsNot Nothing Then
                    startInfo.Arguments = "/C ""psexec.exe \\" & serverName & " -u " & domain & "\" & username & " -p " & password & " " & cmdSetup & command & """"
                Else
                    startInfo.Arguments = "/C ""psexec.exe \\" & serverName & " -u " & username & " -p " & password & " " & cmdSetup & command & """"
                End If
            Else
                startInfo.Arguments = "/C ""psexec.exe " & serverName & " " & cmdSetup & command & """"
            End If

            process.StartInfo = startInfo
            process.Start()
            Dim output = process.StandardOutput.ReadToEnd()
            process.WaitForExit()

            Return output

            'If process.ExitCode = 0 AndAlso process IsNot Nothing AndAlso process.HasExited Then
            'Return output
            'Else
            'Return "Error running the command : " & command
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub InitCommandWindow()
        Me.AcceptButton = btnCommandInput
        MyProcess = New Process
        MyProcess.EnableRaisingEvents = False
        With MyProcess.StartInfo
            .FileName = "Psexec.exe "
            .Arguments = " \\" & ComputerName & " -h cmd.exe /k " & txtCommandInput.Text ' -u hrdc-drhc.net\develop.sadi.sg.gebe -p " & myPwd & "  -h cmd.exe /k " '& txt         '" -u develop.sadi.sg.gebe -p HolyshiT2020! "
            .UseShellExecute = False
            .CreateNoWindow = True
            .RedirectStandardInput = True
            .RedirectStandardOutput = True
            .RedirectStandardError = True

        End With

        txt_LogWindow.Text = txt_LogWindow.Text & vbCrLf & "Starting Process " & MyProcess.ToString()
        MyProcess.Start()

        'MyProcess.BeginErrorReadLine()
        MyProcess.BeginOutputReadLine()
        AppendOutputText(MyProcess.StartTime.ToString)
    End Sub

    Private Sub BtnCommandInput_Click(sender As Object, e As EventArgs) Handles btnCommandInput.Click
        Cursor = Cursors.WaitCursor
        'RunDosCommand(txtCommandInput.Text)
        txtCommandOutput.Text = vbCrLf & executeCommand(ComputerName, "develop.sadi.sg.gebe", "HolyshiT2020!", "hrdc-drhc.net", txtCommandInput.Text)
        txt_LogWindow.Text = txt_LogWindow.Text & vbCrLf & "Running command " & txtCommandInput.Text & " on device: " & ComputerName
        Cursor = Cursors.Default
    End Sub
    Private Sub RunDosCommand(strCommand As String)
        If strCommand.Length > 1 Then
            InitCommandWindow()
            MyProcess.StandardInput.WriteLine(strCommand)
            MyProcess.StandardInput.Flush()
            'txtCommandInput.Text = ""
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        InitCommandWindow()
        MyProcess.StandardInput.WriteLine("EXIT") 'send an EXIT command to the Command Prompt
        MyProcess.StandardInput.Flush()
        MyProcess.Close()
    End Sub

    Private Sub MyProcess_ErrorDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs) Handles MyProcess.ErrorDataReceived
        AppendOutputText(vbCrLf & "Error: " & e.Data)
    End Sub

    Private Sub MyProcess_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs) Handles MyProcess.OutputDataReceived
        AppendOutputText(vbCrLf & e.Data)
    End Sub


    Private Sub AppendOutputText(ByVal text As String)
        If txtCommandOutput.InvokeRequired Then
            Dim myDelegate As New AppendOutputTextDelegate(AddressOf AppendOutputText)
            Me.Invoke(myDelegate, text)
        Else
            txtCommandOutput.AppendText(text)
        End If
    End Sub

    Private Sub BtnClearCommandWindow_Click(sender As Object, e As EventArgs) Handles btnClearCommandWindow.Click
        txtCommandOutput.Text = ""
    End Sub

    Private Sub BranchCache_Port_8009()
        Cursor = Cursors.WaitCursor

        Dim regKey As RegistryKey
        Dim regSubKey As RegistryKey

        'Arret du BrandCache
        RemoteExec("cmd /c netsh BranchCache set service mode=DISABLED")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 25
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100
        Thread.Sleep(2000)

        'Arret du BrandCache
        RemoteExec("cmd /c netsh BranchCache RESET")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 25
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100
        Thread.Sleep(2000)

        'Mise en place des valeur par default
        Me.lbl_loading.Visible = True

        Try
            regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName)
            regSubKey = regKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\PeerDist\DownloadManager\Peers\Connection", True)
            regSubKey.SetValue("ConnectPort", 8009, RegistryValueKind.DWord)
            regSubKey.SetValue("ListenPort", 8009, RegistryValueKind.DWord)
            TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 25
            ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Catch ex As Exception
            Cursor = Cursors.Default
            Me.lbl_loading.Visible = False
        End Try

        Try
            'Envoye de la comande pour le brandcache
            RemoteExec("cmd /c netsh BranchCache set service mode=DISTRIBUTED")
            TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 25
            ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100
            Thread.Sleep(2000)

        Catch ex As Exception
            MsgBox(My.Resources.ErrorTaskSchedule & " BranchCache", MsgBoxStyle.Critical, "SCCM PC Admin")
            Cursor = Cursors.Default
            Me.lbl_loading.Visible = False
        End Try

        Cursor = Cursors.Default
        Me.lbl_loading.Visible = False
    End Sub

    Private Sub DetectNow()

        Dim strCommand
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        'Envoie la command pour forcer la mise a jour de windows update
        'Send the command to force the update of windows update

        'Resetauthorization Option ("https://technet.microsoft.com/fr-fr/library/cc708617(v=ws.10).aspx")
        'WSUS uses a cookie on client computers to store various types of information, including computer group membership when client-side targeting is used. 
        'By default this cookie expires an hour after WSUS creates it. If you are using client-side targeting and change group membership, 
        'use this option in combination with detectnow to expire the cookie, initiate detection, and have WSUS update computer group membership. 

        strCommand = "wuauclt.exe /resetauthorization /DetectNow"
        RemoteExec(strCommand)

        'Envoye les commandes pour les trigger de SCCM (15 sec de delais)

        '{00000000-0000-0000-0000-000000000001} Hardware Inventory....
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000001}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000002} Software Inventory....
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000002}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000003} Discovery Inventory....
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000003}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000010} File Collection
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000010}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000021} Request Machine Assignments
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000021}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000022} Evaluate Machine Policies
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000022}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000121} Application manager policy action
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000121}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000031} Software Metering Generating Usage Report
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000031}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000108} Software Updates Assignments Evaluation Cycle
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000108}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000113} Scan by Update Source
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000113}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000040} Machine Policy Agent Cleanup
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000040}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000042} Policy Agent Validate Machine Policy / Assignment
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000042}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000114} Update Store Policy
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000114}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000111} Send Unsent State Message
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000111}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '{00000000-0000-0000-0000-000000000032} Source Update Message
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000032}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        'Hard Reset Policy
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000113}" & Chr(34) & " /NOINTERACTIVE"
        myCMDLine2 = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /Namespace:\\root\ccm path SMS_Client CALL ResetPolicy 1 /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Shell(myCMDLine2, AppWinStyle.Hide)
        Thread.Sleep(1000)


        'va a 100% de ce module
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 4
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100


    End Sub

    Private Sub Fix_80004015()

        Cursor = Cursors.WaitCursor

        Try
            If Not IsObject(oWMI) Then WMIConnect()
        Catch ex As Exception

        Finally
            TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 25
            ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100
        End Try

        Try
            If strOS = Nothing Then strOS = Main.Instance.txt_OSCaption_NEW.Text
            TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 25
            ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

            'Valide seulement pour Windows 7..... 
            If InStr(strOS, "7") > 0 Then
                'Fixing security descriptors for WUAUSERV and BITS - services security errors (80004015 errors)
                Exec("sc.exe \\" & strComputer & " sdset wuauserv D:(A;;CCLCSWRPWPDTLOCRRC;;;SY)(A;;CCDCLCSWRPWPDTLOCRSDRCWDWO;;;BA)(A;;CCLCSWLOCRRC;;;AU)(A;;CCLCSWRPWPDTLOCRRC;;;PU)")
                TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 25
                ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

                Exec("sc.exe \\" & strComputer & " sdset bits D:(A;;CCLCSWRPWPDTLOCRRC;;;SY)(A;;CCDCLCSWRPWPDTLOCRSDRCWDWO;;;BA)(A;;CCLCSWLOCRRC;;;AU)(A;;CCLCSWRPWPDTLOCRRC;;;PU)")
                TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 25
                ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100
            End If
        Catch ex As Exception
            Cursor = Cursors.Default
        End Try
        Cursor = Cursors.Default
    End Sub

    Private Sub Re_registering_DLLs()
        Cursor = Cursors.WaitCursor
        On Error Resume Next
        'Reregister DLLs silently on remote system
        RemoteExec("RegSvr32.exe /s wuapi.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 5
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wuaueng.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wuaueng1.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wucltui.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wups.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wups2.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wuweb.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s qmgr.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s qmgrprxy.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s atl.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s urlmon.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s mshtml.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s shdocvw.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s browseui.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s jscript.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s vbscript.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s scrrun.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s msxml.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s msxml3.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s msxml6.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s actxprxy.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s softpub.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wintrust.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s dssenh.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s rsaenh.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s gpkcsp.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s sccbase.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s slbcsp.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s cryptdlg.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s oleaut32.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s ole32.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s shell32.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s initpki.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 3
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wucltux.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 2
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s muweb.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("RegSvr32.exe /s wuwebv.dll")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Thread.Sleep(1000)


        On Error GoTo 0
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Del_Cache()

        Me.Cursor = Cursors.WaitCursor

        Service_to_OFF("CCMEXEC", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Service_to_OFF("BITS", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Service_to_OFF("wuauserv", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100


        '****** Suprimmer les entrées au WMI pour la cache
        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\ccm\SoftMgmtAgent")
        Dim Query As New SelectQuery("SELECT * FROM CacheInfoEx")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)

        Dim info As ManagementObject
        Try
            For Each info In search.Get()
                info.Delete()
            Next
        Catch ex As Exception
            'Gestion de l'erreur
        End Try

        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '****** Suprimmer les entrées au registre pour la cache
        Dim Reg As RegistryKey
        Dim Reg_Value As Object
        Dim openKey As Boolean
        Dim count_key As Integer
        Reg = Nothing

        Try
            Reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(SCCM_REG_x86 + "\Mobile Client\Software Distribution" + "\", True)
            Reg_Value = Reg.GetValueNames
            count_key = Reg.ValueCount
            openKey = True

            For Each key_name As String In Reg_Value
                Try
                    Reg.DeleteValue(key_name)
                Catch ex As Exception
                    'Bypass l'erreur
                End Try
                count_key = count_key - 1
            Next
        Catch ex As Exception
            'Erreur
        End Try

        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        '******* Suprimmer les fichiers de la cahche
        Dim strSDPath
        strSDPath = "\\" & strComputer & "\admin$\ccmcache"
        Try
            Dim folder = fso.GetFolder(strSDPath)


            For Each f In folder.Files
                Try
                    Name = f.name
                    f.Delete()
                Catch ex As Exception

                End Try
            Next

            For Each f In folder.SubFolders
                Try
                    Name = f.name
                    f.Delete()
                Catch ex As Exception

                End Try
            Next
        Catch ex As Exception

        End Try

        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Service_to_ON("BITS", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Service_to_ON("wuauserv", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Service_to_ON("CCMEXEC", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("bitsadmin.exe /reset /allusers")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Thread.Sleep(1000)

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Del_Bits()
        Cursor = Cursors.WaitCursor

        On Error Resume Next
        Dim strSDPath
        strSDPath = "\\" & strComputer & "\C$\ProgramData\Application Data\Microsoft\Network\Downloader"

        Service_to_OFF("bits", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Service_to_OFF("wuauserv", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        If fso.fileExists(strSDPath & "qmgr0.dat") Then
            fso.Deletefile(strSDPath & "qmgr0.dat")
        End If

        If fso.fileExists(strSDPath & "qmgr1.dat") Then
            fso.Deletefile(strSDPath & "qmgr2.dat")
        End If

        Service_to_ON("bits", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Service_to_ON("wuauserv", False)
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 6
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        RemoteExec("bitsadmin.exe /reset /allusers")
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 16
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        Thread.Sleep(1000)

        Cursor = Cursors.Default
    End Sub

    Private Sub Del_WSUS_Download()
        Cursor = Cursors.WaitCursor

        On Error Resume Next
        Dim strSDPath

        Service_to_OFF("wuauserv", False)

        strSDPath = "\\" & strComputer & "\admin$\SoftwareDistribution"
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 33
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        If fso.FolderExists(strSDPath & ".old") Then
            fso.DeleteFolder(strSDPath & ".old")
        End If

        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 33
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100

        If fso.FolderExists(strSDPath) Then
            fso.MoveFolder(strSDPath, strSDPath & ".old")
        End If
        On Error GoTo 0
        TimerBar_Adv_clean_now = TimerBar_Adv_clean_now + 34
        ProgressBar1.Value = (TimerBar_Adv_clean_now / TimerBar_Adv_clean) * 100
        Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Button1.Enabled = False
        ProgressBar1.Visible = True
        ProgressBar1.Value = TimerBar_Adv_clean_now

        'Vérification des option active
        If Check1 = True Then
            Me.Cursor = Cursors.WaitCursor
            lbl_loading.Visible = True
            DetectNow()
            CheckBox1.Enabled = False
            lbl_loading.Visible = False
            Me.Cursor = Cursors.Default
        End If

        If Check2 = True Then
            Me.Cursor = Cursors.WaitCursor
            lbl_loading.Visible = True
            Fix_80004015()
            CheckBox2.Enabled = False
            lbl_loading.Visible = False
            Me.Cursor = Cursors.Default
        End If

        If Check3 = True Then
            Me.Cursor = Cursors.WaitCursor
            lbl_loading.Visible = True
            Re_registering_DLLs()
            CheckBox3.Enabled = False
            lbl_loading.Visible = False
            Me.Cursor = Cursors.Default
        End If

        If Check4 = True Then
            Me.Cursor = Cursors.WaitCursor
            lbl_loading.Visible = True
            Del_Cache()
            Del_Bits()
            CheckBox4.Enabled = False
            lbl_loading.Visible = False
            Me.Cursor = Cursors.Default
        End If

        If Check5 = True Then
            Me.Cursor = Cursors.WaitCursor
            lbl_loading.Visible = True
            Del_WSUS_Download()
            CheckBox5.Enabled = False
            lbl_loading.Visible = False
            Me.Cursor = Cursors.Default
        End If

        If Check6 = True Then
            Me.Cursor = Cursors.WaitCursor
            lbl_loading.Visible = True
            BranchCache_Port_8009()
            CheckBox6.Enabled = False
            lbl_loading.Visible = False
            Me.Cursor = Cursors.Default
        End If

        Me.Cursor = Cursors.WaitCursor
        lbl_loading.Visible = False
        Thread.Sleep(3000)
        Services_Stats()
        Me.Cursor = Cursors.Default

        Button1.Enabled = True
        CheckBox6.Enabled = True
        CheckBox5.Enabled = True
        CheckBox4.Enabled = True
        CheckBox3.Enabled = True
        CheckBox2.Enabled = True
        CheckBox1.Enabled = True

        ProgressBar1.Visible = False
        ProgressBar1.Value = 0

        TimerBar_Adv_clean = 0
        TimerBar_Adv_clean_now = 0
        bError = False
        lbl_loading.Visible = False

        'Me.Close()
    End Sub

    Private Sub Services_Stats() 'Vérification de l'états des services

        Dim isRunning As Boolean = False
        'Service MPSSVC  = Service du Firewall
        'Services.Service_Verification("MPSSVC")
        Services.Service_VerificationCheckOnly("MPSSVC", isRunning)

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_MPSSVC_Acces = True Or Err_Services_Acces = True Then

        Else
            Main.Instance.Pic_ON_MPSSVC.Visible = True
            Main.Instance.Pic_OFF_MPSSVC.Visible = False
        End If

        'Service RemoteRegistry  = Service du Registre à distance
        Services.Service_VerificationCheckOnly("CCMEXEC", isRunning)
        'Services.Service_Verification("CCMEXEC")

        'Varialble de retoure si erreur
        If isRunning = False Then '  Err_CCMEXEC_Acces = True Or Err_Services_Acces = True Then

        Else
            Main.Instance.Pic_ON_CCMEXEC.Visible = True
            Main.Instance.Pic_OFF_CCMEXEC.Visible = False
        End If

        'Service BITS  = Service du Download de Windows
        Services.Service_VerificationCheckOnly("BITS", isRunning)
        'Services.Service_Verification("BITS")

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_BITS_Acces = True Or Err_Services_Acces = True Then

        Else
            Main.Instance.Pic_ON_BITS.Visible = True
            Main.Instance.Pic_OFF_BITS.Visible = False
        End If

        'Service PeerDistSvc  = Service du cache de SCCM en mode partage
        Services.Service_VerificationCheckOnly("PeerDistSvc", isRunning)
        'Services.Service_Verification("PeerDistSvc")

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_PeerDistSvc_Acces = True Or Err_Services_Acces = True Then

        Else
            Main.Instance.Pic_ON_PeerDistSvc.Visible = True
            Main.Instance.Pic_OFF_PeerDistSvc.Visible = False
        End If

        'Service wuauserv  = Service de Windows Update
        Services.Service_VerificationCheckOnly("wuauserv", isRunning)
        'Services.Service_Verification("wuauserv")

        'Varialble de retoure si erreur
        If isRunning = False Then ' Err_wuauserv_Acces = True Or Err_Services_Acces = True Then

        Else
            Main.Instance.Pic_ON_wuauserv.Visible = True
            Main.Instance.Pic_OFF_wuauserv.Visible = False
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then Check1 = True
        If CheckBox1.CheckState = CheckState.Unchecked Then Check1 = False

        TimerBar_Adv_clean = TimerBar_Adv_clean + 100 'Ajout de temp pour le calcule de la progression total
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Checked Then Check2 = True
        If CheckBox2.CheckState = CheckState.Unchecked Then Check2 = False

        TimerBar_Adv_clean = TimerBar_Adv_clean + 100 'Ajout de temp pour le calcule de la progression total
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.CheckState = CheckState.Checked Then Check3 = True
        If CheckBox3.CheckState = CheckState.Unchecked Then Check3 = False

        TimerBar_Adv_clean = TimerBar_Adv_clean + 100 'Ajout de temp pour le calcule de la progression total
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.CheckState = CheckState.Checked Then Check4 = True
        If CheckBox4.CheckState = CheckState.Unchecked Then Check4 = False

        TimerBar_Adv_clean = TimerBar_Adv_clean + 100 'Ajout de temp pour le calcule de la progression total
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.CheckState = CheckState.Checked Then Check5 = True
        If CheckBox5.CheckState = CheckState.Unchecked Then Check5 = False

        TimerBar_Adv_clean = TimerBar_Adv_clean + 100 'Ajout de temp pour le calcule de la progression total
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.CheckState = CheckState.Checked Then Check6 = True
        If CheckBox6.CheckState = CheckState.Unchecked Then Check6 = False

        TimerBar_Adv_clean = TimerBar_Adv_clean + 100 'Ajout de temp pour le calcule de la progression total
    End Sub

    ''''' SCCM ACTIONS TAB CLICK
    Private Sub Button4_Click()
        Button4.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000001}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck1.Visible = False
        pic_done1.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button2_Click()
        Button2.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000002}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck2.Visible = False
        pic_done2.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button3_Click()
        Button3.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000003}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck3.Visible = False
        pic_done3.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button10_Click()
        Button10.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000010}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck10.Visible = False
        pic_done10.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button21_Click()
        Button21.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000021}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck21.Visible = False
        pic_done21.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button22_Click()
        Button22.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000022}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck22.Visible = False
        pic_done22.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button121_Click()
        Button121.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000121}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck121.Visible = False
        pic_done121.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button31_Click()
        Button31.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000031}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck31.Visible = False
        pic_done31.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button108_Click()
        Button108.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000108}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck108.Visible = False
        pic_done108.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button113_Click()
        Button113.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000113}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck113.Visible = False
        pic_done113.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button40_Click()
        Button40.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000040}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck40.Visible = False
        pic_done40.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button42_Click()
        Button42.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000042}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck42.Visible = False
        pic_done42.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button114_Click()
        Button114.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000114}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck114.Visible = False
        pic_done114.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button111_Click()
        Button111.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000111}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck111.Visible = False
        pic_done111.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button32_Click()
        Button32.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000032}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        pic_uncheck32.Visible = False
        pic_done32.Visible = True
        If All = False Then
            Thread.Sleep(2000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub Button0_Click()
        Button0.Enabled = False
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000113}" & Chr(34) & " /NOINTERACTIVE"
        Dim myCMDLine2 As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /Namespace:\\root\ccm path SMS_Client CALL ResetPolicy 1 /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Shell(myCMDLine2, AppWinStyle.Hide)
        pic_uncheck0.Visible = False
        pic_done0.Visible = True
        If All = False Then
            Thread.Sleep(4000)
            Me.Refresh()
            lbl_warnnig.Visible = True
        End If
    End Sub

    Private Sub CMD_ALL_Click()

        Me.Cursor = Cursors.WaitCursor
        Me.Enabled = False
        lbl_warnnig.Visible = True
        All = True
        'Mais tou les button a OFF

        CMD_ALL.Enabled = False

        Button121.Enabled = False
        Button3.Enabled = False
        Button10.Enabled = False
        Button4.Enabled = False
        Button21.Enabled = False
        Button2.Enabled = False
        Button31.Enabled = False
        Button108.Enabled = False
        Button113.Enabled = False
        Button22.Enabled = False
        Button40.Enabled = False
        Button42.Enabled = False
        Button114.Enabled = False
        Button111.Enabled = False
        Button32.Enabled = False
        Button0.Enabled = False

        ' Action les button un apres l'autres

        Button121_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button3_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button10_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button4_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button21_Click()
        Thread.Sleep(2000)
        Me.Refresh()

        Button2_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button31_Click()

        Me.Refresh()
        Thread.Sleep(2000)

        Button108_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button113_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button22_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button40_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button42_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button114_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button111_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button32_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Button0_Click()
        Me.Refresh()
        Thread.Sleep(2000)

        Me.Enabled = True

        Me.Cursor = Cursors.Default
        Thread.Sleep(2000)

        'Me.Close()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Button4_Click()
    End Sub

    Private Sub Button121_Click(sender As Object, e As EventArgs) Handles Button121.Click
        Button121_Click()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button3_Click()
    End Sub

    Private Sub DdlDesiredLength_SelectedIndexChanged(sender As Object, e As EventArgs)
        MW_Select = ddlDesiredLength.SelectedItem
    End Sub

    Private Sub Cmd_GPO_NEW_Click(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.WaitCursor

        Dim Result_msg = MsgBox(My.Resources.ConfirmGPOUpdate & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.TitleGPUpdate & " : " & ComputerName)

        If Result_msg = 6 Then
            RemoteExec("cmd /c gpupdate /force /boot")
            Reboot_Send = True
            Thread.Sleep(5000)
            ComputerName = ""
            Main.Instance.txt_PCName_NEW.Text = "..."
            Me.Close()
            'Active le chagement de la souris en mode attente
            Main.Instance.Affichage_Defaut()
            Main.Instance.Connexion()
            'Remet le cursor en mode defaut
            Main_Start_Form.Instance.Show()
            Main.Instance.Close()
            Me.Cursor = Cursors.Default
            Main_Start_Form.Instance.Cursor = Cursors.Default
            Main_Start_Form.Instance.pic_rightArrow.Visible = True
            Main_Start_Form.Instance.pic_notOk.Visible = False
            Main_Start_Form.Instance.pic_Ok.Visible = False
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Cmd_Del_WMI_NEW_Click(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.WaitCursor

        'arret du service sccm
        Service_to_OFF("ccmexec", True)

        If Service_ON_OFF = "OFF" Then
            Try
                Dim objWMIService As Object
                Dim objItem As Object

                objWMIService = GetObject("winmgmts:" & "{impersonationLevel=impersonate}!\\" & ComputerName & "\root")
                objItem = objWMIService.Get("__Namespace.Name='CCM'")
                objItem.delete_()

                MsgBox(My.Resources.ConfirmRemoveWMINamespace, MsgBoxStyle.Information, "SCCM PC Admin")

            Catch ex As Exception

                MsgBox(My.Resources.ErrorUnexpected & " - " & Err.Number & " - " & Err.Description, MsgBoxStyle.Critical, "SCCM PC Admin")

            End Try
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Cmd_Rebuilding_WMI_Click(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.WaitCursor

        'arret du service Windows Management Instrumentation
        Cursor = Cursors.WaitCursor
        Service_to_ON("winmgmt", False)
        Service_to_OFF("winmgmt", True)
        Cursor = Cursors.Arrow

        '******* Suprimmer le répertoire "Repository" du WMI "C:\windows\system32\wbem\Repository"
        Try
            Cursor = Cursors.WaitCursor
            Dim strSDPath
            strSDPath = "\\" & ComputerName & "\admin$\System32\wbem\Repository"
            System.IO.Directory.Delete(strSDPath, True)
            Thread.Sleep(3000)
            Cursor = Cursors.Arrow
        Catch ex As Exception
            ' Gestion de l'erreur
            Cursor = Cursors.Arrow
        End Try

        'Démarrage du service Windows Management Instrumentation
        Cursor = Cursors.WaitCursor
        Service_to_ON("winmgmt", True)
        Cursor = Cursors.Arrow

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Cmd_Re_Registering_NEW_Click(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.WaitCursor

        'arret du service Windows Management Instrumentation
        Cursor = Cursors.WaitCursor
        Service_to_ON("winmgmt", False)
        Service_to_OFF("winmgmt", True)
        Cursor = Cursors.Arrow

        '******* Suprimmer le répertoire "Repository" du WMI "C:\windows\system32\wbem\Repository"
        Try
            Cursor = Cursors.WaitCursor
            Dim strSDPath
            strSDPath = "\\" & ComputerName & "\admin$\System32\wbem\Repository"
            System.IO.Directory.Delete(strSDPath, True)
            Thread.Sleep(3000)
            Cursor = Cursors.Arrow
        Catch ex As Exception
            ' Gestion de l'erreur
            Cursor = Cursors.Arrow
        End Try

        'Démarrage du service Windows Management Instrumentation
        Cursor = Cursors.WaitCursor
        Service_to_ON("winmgmt", True)
        Cursor = Cursors.Arrow

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Cmd_registry_pol_Click(sender As Object, e As EventArgs)
        Dim fso = CreateObject("Scripting.FileSystemObject")
        Me.Cursor = Cursors.WaitCursor

        Dim Result_msg = MsgBox(My.Resources.ConfirmGPOUpdate & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.TitleGPUpdate & " : " & ComputerName)
        If Result_msg = 6 Then

            '******* Suprimmer le fichier ..\Windows\System32\GroupPolicy\Machine\Registry.pol
            Dim strSDPath
            strSDPath = "\\" & ComputerName & "\admin$\System32\GroupPolicy\Machine"
            Try

                Dim folder = fso.GetFolder(strSDPath)

                For Each f In folder.Files
                    Try
                        Name = f.name
                        If Name = "Registry.pol" Then f.Delete()
                    Catch ex As Exception

                    End Try
                Next
            Catch ex As Exception

            End Try

            ' Envoye la comande du GPUpdate par la suite
            RemoteExec("cmd /c gpupdate /force /boot")
            Reboot_Send = True
            Thread.Sleep(5000)
            ComputerName = ""
            Main.Instance.txt_PCName_NEW.Text = "..."
            Me.Close()
            'Active le chagement de la souris en mode attente
            Main.Instance.Affichage_Defaut()
            Main.Instance.Connexion()
            'Remet le cursor en mode defaut
            Main_Start_Form.Instance.Show()
            Main.Instance.Close()
            Me.Cursor = Cursors.Default
            Main_Start_Form.Instance.Cursor = Cursors.Default
            Main_Start_Form.Instance.pic_rightArrow.Visible = True
            Main_Start_Form.Instance.pic_notOk.Visible = False
            Main_Start_Form.Instance.pic_Ok.Visible = False
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Cmd_WSUS_Download_NEW_Click(sender As Object, e As EventArgs)
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\Windows\SoftwareDistribution\Download")
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub Cmd_BITS_Location_NEW_Click(sender As Object, e As EventArgs)
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\ProgramData\Microsoft\Network\Downloader")
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub Cmd_DataStore_NEW_Click(sender As Object, e As EventArgs)
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\Windows\SoftwareDistribution\DataStore")
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub Cmd_Client_Logs_NEW_Click(sender As Object, e As EventArgs)
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\Windows\CCM\Logs")
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Lstv_Collection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstv_Collection.SelectedIndexChanged
        ShowCollection()
    End Sub

    Public Sub ShowCollection()
        'cherche le domain associer au SCCM Server et le site code
        Read_INI(UCase(PC_Domain))


        'Vérifie si le poste a été valider avec le nom de domaine (ex:xxxx.cips.communication.gc.ca)

        Dim ComputerName_Short As String
        Dim index As Integer = ComputerName.IndexOf(".")
        If index <> -1 Then
            ComputerName_Short = ComputerName.Substring(0, (index))
        Else
            ComputerName_Short = ComputerName
        End If

        '*************************************************************************************
        'Connection au serveur SQL 
        '************************************************************************************* 
        Try
            Dim sqlConnection1 As New SqlConnection("server=" & INI_SQL_Server & ";database=" & INI_SQL_Database & ";User ID=THMVACC;Password=Passw0rd1")
            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader

            cmd.CommandText = "Select C.CollectionID,C.Name,C.Comment from dbo.v_Collection C join dbo.v_FullCollectionMembership FCM on C.CollectionID = FCM.CollectionID Where FCM.Name = '" & ComputerName_Short & "'"
            cmd.CommandType = CommandType.Text
            cmd.Connection = sqlConnection1

            sqlConnection1.Open()
            reader = cmd.ExecuteReader()
            lstv_Collection.Items.Clear()
            ' lecture des data.....
            While reader.Read
                Me.lstv_Collection.Sorting = Windows.Forms.SortOrder.Ascending
                Dim ls As New ListViewItem(reader.Item("Name").ToString())
                ls.SubItems.Add(reader.Item("CollectionID").ToString())
                lstv_Collection.Items.Add(ls)
            End While

            'ColumnHeader1.Width = -1
            'ColumnHeader2.Width = -1
            lstv_Collection.Refresh()

            sqlConnection1.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RunESSetupInfo()

        Dim Result_msg = MsgBox(My.Resources.WarningRunSoftwareCache & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.WarningRunSoftwareCache & " : " & ComputerName)
        If Result_msg = 6 Then
            Try
                ProgressBar.Value = 0
                ProgressBar.Visible = True
                Label2.Visible = True
                Dim script = New StringBuilder()

                script.AppendLine("$results = get-childitem -recurse -depth 1 \\" & ComputerName & "\c$\windows\ccmcache -filter ES-Setup.exe ")
                script.AppendLine("  Write-Host('##START##')  ")
                script.AppendLine("  foreach($item in $results){  ")
                script.AppendLine("      Write-Host('||',$item.DirectoryName, '##', $item.VersionInfo.ProductName, '||' )  ")
                script.AppendLine("      }  ")
                script.AppendLine("  Write-Host('##END##')  ")

                Dim objProcess As New System.Diagnostics.Process()

                objProcess.StartInfo.FileName = "powershell.exe "
                objProcess.StartInfo.Arguments = script.ToString
                objProcess.StartInfo.RedirectStandardOutput = True
                objProcess.StartInfo.RedirectStandardError = True
                objProcess.StartInfo.UseShellExecute = False
                objProcess.StartInfo.CreateNoWindow = True
                objProcess.Start()

                Dim output As String = objProcess.StandardOutput.ReadToEnd()
                Dim errors As String = objProcess.StandardError.ReadToEnd()

                txtCommandOutput.Text += "Output:" + Environment.NewLine
                txtCommandOutput.Text += "-------" + Environment.NewLine
                txtCommandOutput.Text += output + Environment.NewLine
                txtCommandOutput.Text += Environment.NewLine
                txtCommandOutput.Text += "Errors:" + Environment.NewLine
                txtCommandOutput.Text += "-------" + Environment.NewLine
                txtCommandOutput.Text += errors + Environment.NewLine
                ShowESSetupInfoListView(output)

            Catch ex As Exception
                txtCommandOutput.Clear()
                txtCommandOutput.Text = "Problem encountered trying to run the ES setup Info Command"
                ProgressBar.Value = 0
                ProgressBar.Visible = False
            End Try
        Else
            loadSoftwareCacheTab = 0
        End If

        loadSoftwareCacheTab = 1


    End Sub


    Sub ShowESSetupInfoListView(data As String)

        onetime = 0
        Me.Refresh()

        If ListView_SoftwareLocation_NEW.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView_SoftwareLocation_NEW.Items(0).Selected = True
            ListView_SoftwareLocation_NEW.Select()
            'Exit Select
        End If

        ListView_SoftwareLocation_NEW.Items.Clear()

        Try
            'Valide que ce se script ne passe que une fois

            'Dim strSplit1 = data.Split("##START##")
            data = data.Replace("& vbLf &", "")
            Dim SearchWithinThis As String = data
            Dim StartSearch As String = "##START##"
            Dim FirstCharacterPos As Integer = SearchWithinThis.IndexOf(StartSearch)
            Dim EndSearch As String = "##END##"
            Dim LastCharacterPos As Integer = SearchWithinThis.IndexOf(EndSearch)

            data = data.Substring(FirstCharacterPos, LastCharacterPos - FirstCharacterPos).Replace("##START##", "").Replace(" vbLf &", "")

            'Dim phrase1 As String = data.Split("##START##")(1)
            'Dim phrase2 As String = phrase1.Split("##END##")(0)
            Dim strESResults = data.Split("||")

            For i As Integer = 0 To UBound(strESResults)

                If (strESResults(i).Length > 5) Then
                    Me.ListView_SoftwareLocation_NEW.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(strESResults(i).Split("##")(0)) '"ESEdit Info " & i.ToString)
                    'item.SubItems.Add(strESResults(i).Split("##")(0))
                    item.SubItems.Add(strESResults(i).Split("##")(2))

                    ListView_SoftwareLocation_NEW.Items.Add(item)
                    Me.Update()

                    ProgressBar.Value = i
                    Me.Update()
                End If
                'ListView3.Sorting = Windows.Forms.SortOrder.Ascending

            Next
            ProgressBar.Visible = False

        Catch ex As Exception
            'GEstion de l'erreur
            Console.WriteLine("exception splitting stuff " & ex.Message)
            ProgressBar.Visible = False
        End Try

        'Commande pour le sort de la colonne
        Tab_Select = 3
        AddHandler Me.ListView_SoftwareLocation_NEW.ColumnClick, AddressOf ColumnClick
        If ListView_SoftwareLocation_NEW.Items.Count > 0 Then
            ListView_SoftwareLocation_NEW.Items(0).Selected = True
            ListView_SoftwareLocation_NEW.Select()
        End If
        Me.Refresh()

    End Sub




    Private Sub Cmd_Client_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Cmd_Collection_Click(sender As Object, e As EventArgs)

    End Sub

    'Private Sub ListView5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView5.SelectedIndexChanged
    '    RunESSetupInfo()
    'End Sub

    Private Sub Listvw_ExecHistApps_SelectedIndexChanged(sender As Object, e As EventArgs) Handles listvw_ExecHistApps.SelectedIndexChanged

    End Sub

    Private Sub Cmd_load_Logs1_NEW_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs1_NEW.Click, cmb_Logs1_NEW.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs1_NEW.Text = "" Or cmb_Logs1_NEW.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs2_NEW.Text = ""
        cmb_Logs3_NEW.Text = ""
        cmb_Logs4_NEW.Text = ""
        cmb_Logs5_NEW.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs1_NEW.Text, True)
    End Sub

    Private Sub Cmd_load_Logs2_NEW_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs2_NEW.Click, cmb_Logs2_NEW.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs2_NEW.Text = "" Or cmb_Logs2_NEW.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1_NEW.Text = ""
        cmb_Logs3_NEW.Text = ""
        cmb_Logs4_NEW.Text = ""
        cmb_Logs5_NEW.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs2_NEW.Text, True)
    End Sub

    Private Sub Cmd_load_Logs3_NEW_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs3_NEW.Click, cmb_Logs3_NEW.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs3_NEW.Text = "" Or cmb_Logs3_NEW.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1_NEW.Text = ""
        cmb_Logs2_NEW.Text = ""
        cmb_Logs4_NEW.Text = ""
        cmb_Logs5_NEW.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs3_NEW.Text, True)
    End Sub

    Private Sub Cmd_load_Logs4_NEW_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs4_NEW.Click, cmb_Logs4_NEW.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs4_NEW.Text = "" Or cmb_Logs4_NEW.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1_NEW.Text = ""
        cmb_Logs2_NEW.Text = ""
        cmb_Logs3_NEW.Text = ""
        cmb_Logs5_NEW.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs4_NEW.Text, True)
    End Sub

    Private Sub Cmd_load_Logs5_NEW_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs5_NEW.Click, cmb_Logs5_NEW.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs5_NEW.Text = "" Or cmb_Logs5_NEW.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1_NEW.Text = ""
        cmb_Logs2_NEW.Text = ""
        cmb_Logs3_NEW.Text = ""
        cmb_Logs4_NEW.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs5_NEW.Text, True)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Button10_Click()
    End Sub

    Private Sub Cmd_Rebuilding_WMI_NEW_Click(sender As Object, e As EventArgs) Handles cmd_Rebuilding_WMI_NEW.Click

    End Sub

    Private Sub Cmd_BITS_Location_NEW_Click_1(sender As Object, e As EventArgs) Handles cmd_BITS_Location_NEW.Click

    End Sub

    Private Sub Cmd_WSUS_Download_NEW_Click_1(sender As Object, e As EventArgs) Handles cmd_WSUS_Download_NEW.Click

    End Sub

    Private Sub Lbl_img_ver_win10_NEW_Click(sender As Object, e As EventArgs) Handles lbl_img_ver_win10_NEW.Click

    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Button21_Click()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2_Click()
    End Sub

    Private Sub ListViewInstalledSoftware_NEW_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewInstalledSoftware_NEW.SelectedIndexChanged

    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        Button31_Click()
    End Sub

    Private Sub Button108_Click(sender As Object, e As EventArgs) Handles Button108.Click
        Button108_Click()
    End Sub

    Private Sub Button113_Click(sender As Object, e As EventArgs) Handles Button113.Click
        Button113_Click()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Button22_Click()
    End Sub

    Private Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        Button40_Click()
    End Sub

    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        Button42_Click()
    End Sub

    Private Sub Button114_Click(sender As Object, e As EventArgs) Handles Button114.Click
        Button114_Click()
    End Sub

    Private Sub Button111_Click(sender As Object, e As EventArgs) Handles Button111.Click
        Button111_Click()
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Button32_Click()
    End Sub

    Private Sub Button0_Click(sender As Object, e As EventArgs) Handles Button0.Click
        Button0_Click()
    End Sub

    Private Sub CMD_ALL_Click(sender As Object, e As EventArgs) Handles CMD_ALL.Click
        CMD_ALL_Click()
    End Sub
    Private Sub cmd_Add_SW_Click(sender As Object, e As EventArgs) Handles cmd_Add_SW.Click
        'Va demander au client de choisir la longueur de la fenetre de maintenance désirer
        Dim popupMWTime As Popup_MW_Time = New Popup_MW_Time
        popupMWTime.ShowDialog(Me)
        If MW_Select = "NULL" Then Exit Sub
        Me.Cursor = Cursors.WaitCursor

        Dim newSW As New SMSSchedules.SMS_ST_NonRecurring
        'Dim swDate As DateTime
        Dim ts As TimeSpan
        Dim objService As Object
        Dim instServiceWindow As Object = ""
        Dim clsServiceWindow As Object
        Dim Conv_Schedules As String = ""
        Dim CurrentDate As Date = Now
        Dim End_taskDate As Integer
        Dim Start_taskDate As Date

        Try

            Try
                'recherche le décalage du fuseau horraire du pc a distance
                TimeZone(ComputerName)
            Catch ex As Exception

            End Try

            Start_taskDate = CurrentDate.AddHours(TimeZone_PC)

            Select Case MW_Select

                Case 1 'Retourne 1h
                    End_taskDate = 1
                Case 2 'Retourne 2h
                    End_taskDate = 2
                Case 4 'Retourne 4h
                    End_taskDate = 4
                Case 8 'Retourne 8h
                    End_taskDate = 8
                Case 12 'Retourne 12h
                    End_taskDate = 12
                Case 24 'Retourne 24h
                    End_taskDate = 24
                Case 48 'Retourne 48h
                    End_taskDate = 48
                Case 72 'Retourne 72h
                    End_taskDate = 72

                Case Else
                    'Par default ajout si erreur une fenetre minimum de 1 heurs
                    End_taskDate = 1

            End Select

            Try
                'Conection a la classe du WMI 
                objService = GetObject("winmgmts:\\" & ComputerName & "\root\ccm\policy\machine\requestedconfig")
                clsServiceWindow = objService.Get("CCM_ServiceWindow")
                instServiceWindow = clsServiceWindow.SpawnInstance_

                'Convertie la Schedules pour la mettre en place au WMI
                newSW.StartTime = Start_taskDate
                newSW.IsGMT = False

                ts = New TimeSpan(End_taskDate, 0, 0)
                newSW.MinuteDuration = ts.Minutes
                newSW.HourDuration = ts.Hours
                newSW.DayDuration = ts.Days

                'Mes en place la nouvelle fenetre de maintenance manuelle

                instServiceWindow.PolicySource = "Local"
                instServiceWindow.ServiceWindowType = "1"
                instServiceWindow.Schedules = newSW.ScheduleID
                instServiceWindow.PolicyVersion = "1"
                instServiceWindow.ServiceWindowID = Guid.NewGuid.ToString

                instServiceWindow.Put_()

                'Prend une pause de 10 secondes
                Dim waitForm As Wait = New Wait(10)
                waitForm.ShowDialog(Me)
            Catch ex As Exception
                'Gestion des erreur
            End Try

        Catch ex As Exception
            'Gestion des erreur
            Me.Cursor = Cursors.Default
        End Try

        instServiceWindow = ""
        Me.Cursor = Cursors.Default
    End Sub


    Private Sub Logs_Call(logs_files As String, Open_Logs As Boolean)

        txt_Description.Text = ""

        Select Case logs_files

            Case "AppDiscovery.log"
                txt_Description.Text = My.Resources.AppDiscovery
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "AppEnforce.log"
                txt_Description.Text = My.Resources.AppEnforce
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "AppIntentEval.log"
                txt_Description.Text = My.Resources.AppIntentEval
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "AssetAdvisor.log"
                txt_Description.Text = My.Resources.AssetAdvisor
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "BgbHttpProxy.log"
                txt_Description.Text = My.Resources.BgbHttpProxy
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CAS.log"
                txt_Description.Text = My.Resources.CAS
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Ccm32BitLauncher.log"
                txt_Description.Text = My.Resources.Ccm32BitLauncher
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CcmEval.log"
                txt_Description.Text = My.Resources.CcmEval
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CcmEvalTask.log"
                txt_Description.Text = My.Resources.CcmEvalTask
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CcmExec.log"
                txt_Description.Text = My.Resources.CcmExec
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CcmMessaging.log"
                txt_Description.Text = My.Resources.CcmMessaging
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CCMNotificationAgent.log"
                txt_Description.Text = My.Resources.CCMNotificationAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Ccmperf.log"
                txt_Description.Text = My.Resources.Ccmperf
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CcmRepair.log"
                txt_Description.Text = My.Resources.CcmRepair
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CcmRestart.log"
                txt_Description.Text = My.Resources.CcmRestart
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CCMSDKProvider.log"
                txt_Description.Text = My.Resources.CCMSDKProvider
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Ccmsetup.log"
                txt_Description.Text = My.Resources.ccmsetup
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\ccmsetup\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Ccmsetup-ccmeval.log"
                txt_Description.Text = My.Resources.ccmsetup_ccmeval
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\ccmsetup\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CertificateMaintenance.log"
                txt_Description.Text = My.Resources.CertificateMaintenance
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CIAgent.log"
                txt_Description.Text = My.Resources.CIAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CIDownloader.log"
                txt_Description.Text = My.Resources.CIDownloader
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CITaskManager.log"
                txt_Description.Text = My.Resources.CITaskManager
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CITaskMgr.log"
                txt_Description.Text = My.Resources.CITaskMgr
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Client.msi.log"
                txt_Description.Text = My.Resources.client_msi
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\ccmsetup\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ClientAuth.log"
                txt_Description.Text = My.Resources.ClientAuth
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ClientIDManagerStartup.log"
                txt_Description.Text = My.Resources.ClientIDManagerStartup
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ClientLocation.log"
                txt_Description.Text = My.Resources.ClientLocation
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CMHttpsReadiness.log"
                txt_Description.Text = My.Resources.CMHttpsReadiness
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "CmRcService.log"
                txt_Description.Text = My.Resources.CmRcService
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ConfigMgrSoftwareCatalog.log"
                txt_Description.Text = My.Resources.ConfigMgrSoftwareCatalog
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ContentTransferManager.log"
                txt_Description.Text = My.Resources.ContentTransferManager
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "DataTransferService.log"
                txt_Description.Text = My.Resources.DataTransferService
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "DCMAgent.log"
                txt_Description.Text = My.Resources.DCMAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "DCMReporting.log"
                txt_Description.Text = My.Resources.DCMReporting
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "DcmWmiProvider.log"
                txt_Description.Text = My.Resources.DcmWmiProvider
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "EndpointProtectionAgent.log"
                txt_Description.Text = My.Resources.EndpointProtectionAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Execmgr.log"
                txt_Description.Text = My.Resources.execmgr
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ExpressionSolver.log"
                txt_Description.Text = My.Resources.ExpressionSolver
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ExternalEventAgent.log"
                txt_Description.Text = My.Resources.ExternalEventAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "FileBITS.log"
                txt_Description.Text = My.Resources.FileBITS
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "FileSystemFile.log"
                txt_Description.Text = My.Resources.FileSystemFile
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "FSPStateMessage.log"
                txt_Description.Text = My.Resources.FSPStateMessage
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "InternetProxy.log"
                txt_Description.Text = My.Resources.InternetProxy
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "InventoryAgent.log"
                txt_Description.Text = My.Resources.InventoryAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Loadstate.log"
                txt_Description.Text = My.Resources.loadstate
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "LocationCache.log"
                txt_Description.Text = My.Resources.LocationCache
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "LocationServices.log"
                txt_Description.Text = My.Resources.LocationServices
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "MaintenanceCoordinator.log"
                txt_Description.Text = My.Resources.MaintenanceCoordinator
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Mifprovider.log"
                txt_Description.Text = My.Resources.Mifprovider
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Mtrmgr.log"
                txt_Description.Text = My.Resources.mtrmgr
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "PolicyAgent.log"
                txt_Description.Text = My.Resources.PolicyAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "PolicyAgentProvider.log"
                txt_Description.Text = My.Resources.PolicyAgentProvider
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "PolicyEvaluator.log"
                txt_Description.Text = My.Resources.PolicyEvaluator
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "PolicyPlatformClient.log"
                txt_Description.Text = My.Resources.PolicyPlatformClient
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "PolicySdk.log"
                txt_Description.Text = My.Resources.PolicySdk
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Pwrmgmt.log"
                txt_Description.Text = My.Resources.Pwrmgmt
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "PwrProvider.log"
                txt_Description.Text = My.Resources.PwrProvider
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "RebootCoordinator.log"
                txt_Description.Text = My.Resources.RebootCoordinator
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ScanAgent.log"
                txt_Description.Text = My.Resources.ScanAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Scanstate.log"
                txt_Description.Text = My.Resources.scanstate
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Scheduler.log"
                txt_Description.Text = My.Resources.Scheduler
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "SdmAgent.log"
                txt_Description.Text = My.Resources.SdmAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "ServiceWindowManager.log"
                txt_Description.Text = My.Resources.ServiceWindowManager
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Setupact.log"
                txt_Description.Text = My.Resources.Setupact
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Setuperr.log"
                txt_Description.Text = My.Resources.Setuperr
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "SetupPolicyEvaluator.log"
                txt_Description.Text = My.Resources.setuppolicyevaluator
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Smpisapi.log"
                txt_Description.Text = My.Resources.smpisapi
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Smscliui.log"
                txt_Description.Text = My.Resources.smscliui
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Smsts.log"
                txt_Description.Text = My.Resources.Smsts
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "SmsWusHandler.log"
                txt_Description.Text = My.Resources.SmsWusHandler
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Setupapi.log"
                txt_Description.Text = My.Resources.Setupapi
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "SoftwareCatalogUpdateEndpoint.log"
                txt_Description.Text = My.Resources.SoftwareCatalogUpdateEndpoint
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "SoftwareCenterSystemTasks.log"
                txt_Description.Text = My.Resources.SoftwareCenterSystemTasks
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "SrcUpdateMgr.log"
                txt_Description.Text = My.Resources.SrcUpdateMgr
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "StateMessage.log"
                txt_Description.Text = My.Resources.StateMessage
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "StatusAgent.log"
                txt_Description.Text = My.Resources.StatusAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "SWMTRReportGen.log"
                txt_Description.Text = My.Resources.SWMTRReportGen
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "TSAgent.log"
                txt_Description.Text = My.Resources.TSAgent
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "UpdatesDeployment.log"
                txt_Description.Text = My.Resources.UpdatesDeployment
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "UpdatesHandler.log"
                txt_Description.Text = My.Resources.UpdatesHandler
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "UpdatesStore.log"
                txt_Description.Text = My.Resources.UpdatesStore
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "UserAffinity.log"
                txt_Description.Text = My.Resources.UserAffinity
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "VirtualApp.log"
                txt_Description.Text = My.Resources.VirtualApp
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Wakeprxy-install.log"
                txt_Description.Text = My.Resources.wakeprxy_install
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Wakeprxy-uninstall.log"
                txt_Description.Text = My.Resources.wakeprxy_uninstall
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "Wedmtrace.log"
                txt_Description.Text = My.Resources.Wedmtrace
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "WindowsUpdate.log"
                txt_Description.Text = My.Resources.WindowsUpdate
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

            Case "WUAHandler.log"
                txt_Description.Text = My.Resources.WUAHandler
                If Open_Logs = True Then
                    Try
                        Process.Start(CMTrace, " \\" & ComputerName & "\c$\Windows\CCM\Logs\" & logs_files)
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try
                End If

        End Select


    End Sub

    Private Sub ResetControls()

        ' CLEAR ALL LISTS
        listvw_ExecHistApps.Items.Clear()
        lstvw_ExecHistPkgs.Items.Clear()
        ListView_RunningPackages_NEW.Items.Clear()
        ListView_ProgramsFeatures_NEW.Items.Clear()
        ListView_SoftwareLocation_NEW.Items.Clear()
        ListViewWSUS_SCUP_NEW.Items.Clear()
        ListViewServices_NEW.Items.Clear()
        ServiceWindowsListView.Items.Clear()
        txtCommandOutput.Clear()
        txtCommandInput.Clear()
        lstv_Collection.Items.Clear()
        ListViewInstalledSoftware_NEW.Items.Clear()
        ListViewJava_NEW.Items.Clear()
        ListViewProcess_NEW.Items.Clear()
        ListViewServices_NEW.Items.Clear()
        MembershipListView.Text = ""



        ' RESET LOADING COUNTERS
        lastCol = 0
        Tab_Select = 0
        loadExecutionPKGSTab = 0
        loadExecutionAPPSTab = 0
        loadRunningPKGSTab = 0
        loadAdvertisementsTab = 0
        loadInfoTab = 0
        loadServiceWindows = 0
        loadRunningWSUS_SCUP = 0
        loadProgramsAndFeaturesTab = 0
        loadSoftwareCacheTab = 0
        loadInstalledSoftwareTab = 0
        loadJavaTab = 0
        loadProcessTab = 0
        loadServiceTab = 0

        'text fields
        txt_OSCaption_NEW.Text = ""
        txt_img_ver_win10_NEW.Text = ""
        txt_img_ver.Text = ""
        txt_SiteCode_result_NEW.Text = ""
        txt_ManagementPoint_NEW.Text = ""
        txt_Client_Version_Result_NEW.Text = ""
        txt_SCCM_Catalogue_NEW.Text = ""
        txt_WUA_NEW.Text = ""
        txtCommandOutput.Text = ""
        txtCommandInput.Text = ""
        txt_LogWindow.Text = ""
        txt_PCName_NEW.Text = ""
        txtLoggedIn_NEW.Text = ""
        txt_ADSite_NEW.Text = ""
        txt_img_ver.Text = ""
        txt_img_install_Date.Text = ""
        txt_last_reboot_NEW.Text = ""
        txt_img_ver_win10_NEW.Text = ""
        txt_language_NEW.Text = ""
        txt_IP_NEW.Text = ""
        txt_OSCaption_NEW.Text = ""
        txt_RAM.Text = ""
        txt_EquipmentType.Text = ""
        txt_Vendor.Text = ""
        txt_Name.Text = ""
        txt_CPU.Text = ""
        txt_SRU_Verimg.Text = ""
        txt_Domain_NEW.Text = ""
        txt_ManagementPoint_NEW.Text = ""
        txt_SiteCode_result_NEW.Text = ""
        txt_Client_Version_Result_NEW.Text = ""
        txt_WUA_NEW.Text = ""
        txt_SCCM_Catalogue_NEW.Text = ""


        'RESET SSCM ACTION BUTTONS
        CMD_ALL.Enabled = True
        Button121.Enabled = True
        Button3.Enabled = True
        Button10.Enabled = True
        Button1.Enabled = True
        Button21.Enabled = True
        Button2.Enabled = True
        Button31.Enabled = True
        Button108.Enabled = True
        Button113.Enabled = True
        Button22.Enabled = True
        Button40.Enabled = True
        Button42.Enabled = True
        Button114.Enabled = True
        Button111.Enabled = True
        Button32.Enabled = True
        Button0.Enabled = True

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        CheckBox6.Checked = False

        lblRunCmdMsg.Text = lblRunCmdMsg.Text & ": " & ComputerName

        Affichage_Defaut()
        Me.Refresh()

    End Sub


    Private Sub btn_apps_refresh_Click(sender As Object, e As EventArgs) Handles btn_apps_refresh.Click
        Dim popupRefreshApps As Popup_Refresh_Apps = New Popup_Refresh_Apps
        popupRefreshApps.ShowDialog(Me)
    End Sub

End Class

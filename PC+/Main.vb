
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
            txtLogged.Text = My.Resources.txt_logged_no_user
        End If

        ResetLanguage()
        ResetServiceWindows()
        ResetVersion()
    End Sub

    Private Sub ResetLanguage()

        If OSLanguage = "1036" Then
            txt_language.Text = My.Resources.txt_language_text_fr
        ElseIf OSLanguage = "1033" Then
            txt_language.Text = My.Resources.txt_language_text_en
        End If

        If m_strChassisTypes = "MOBILE_DEVICE" Then
            txt_TypePC.Text = My.Resources.txt_TypePC_text_mobile
        ElseIf m_strChassisTypes = "DESKTOP" Then
            txt_TypePC.Text = My.Resources.txt_TypePC_text_desktop
        Else
            txt_TypePC.Text = m_strChassisTypes
        End If

        txt_img_install_Date.Text = WMIDateConvert(str_InstallDate)
        txt_last_reboot.Text = WMIDateConvert(str_LastBootUpTime)

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

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _instance = Nothing
    End Sub

    Private Sub Main_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

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

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load

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
        Me.txt_PCName.Select()
        btnCenterConsole2.Hide() ' need to hide this one at startup
        InitialLoadingSetLayoutDefaults()
    End Sub

    Private Sub InitialLoadingSetLayoutDefaults()
        'Throw New NotImplementedException()
    End Sub

    Friend Sub Connexion()

        Me.Cursor = Cursors.WaitCursor

        'Vérification si le PC est "Online"
        ComputerName = Trim(txt_PCName.Text)
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
                    pic_rightArrow.Visible = False
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
                txtLogged.Text = User
                pic_Assitance.Cursor = Cursors.Hand
            Else
                txtLogged.Text = My.Resources.txt_logged_no_user
                User = ""
                pic_Assitance.Cursor = Cursors.No
            End If

            'Fonction pour allez chercher les information de l'ordinateur au régistre
            Get_PC_Information.Get_PC_Info_REG()

            'Fonction pour allez chercher les information de l'ordinateur au régistre
            Get_PC_Information.Get_PC_Info_WMI()

            'Ajouter des valeur dans les champ requis

            txt_OSCaption.Text = OSName
            If OSName = "Microsoft Windows 10 Enterprise" Then
                lbl_img_ver_win10.Visible = True
                txt_img_ver_win10.Visible = True
                txt_img_ver_win10.Text = CORE_Image_version
            Else
                lbl_img_ver_win10.Visible = False
                txt_img_ver_win10.Visible = False
                txt_img_ver_win10.Text = ""
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
                txt_IP.Text = IPAddress_Value
            Else
                'Va checher le nom DNS ar le nom Entré et une Adresse IP
                IPAddress_Value = ComputerName
                txt_IP.Text = IPAddress_Value
                DNS_Name(ComputerName)
                txt_PCName.Text = DNS_Name_Value
                ComputerName = DNS_Name_Value
            End If

            'Validation pour l'activation du mode Avancé seulement pour le HRDC-DRHC.NET
            RemoteUser.GetGroups(Username)
            If Advance_mode = True Then Me.AdvancedMode_Menu.Visible = True Else Me.AdvancedMode_Menu.Visible = False

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
            txt_TypePC.Text = parts(4)

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
                        retVal = True
                        Exit For
                    End If
                End If
            Next

            'Dim strListedComputerName = parts(7)
            If Not retVal Then
                txt_PCName.Text = ""
                txt_PCName.Text = txt_PCName.Text & " - DNS ISSUE"
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
        txt_PCName.Text = ComputerName
        txt_Client_Version_Result_NEW.Text = "..."
        txt_img_ver.Text = "..."
        txt_img_install_Date.Text = "..."
        txt_language.Text = "..."
        txt_last_reboot.Text = "..."
        txt_SiteCode_result_NEW.Text = "..."
        txt_OSCaption.Text = "..."
        txt_TypePC.Text = "..."
        txtLogged.Text = "..."
        txt_IP.Text = "..."
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

        lbl_img_ver_win10.Visible = False
        txt_img_ver_win10.Visible = False

        'Masque par default l'icone VPN
        'Me.Pic_VPN.Visible = False

        'Fait une mise a jour de l'affichage de la forme
        Me.Refresh()

    End Sub

    Private Sub cmd_Check_Click(sender As Object, e As EventArgs) Handles cmd_Check.Click
        'Modification du ComputerName
        ComputerName = txt_PCName.Text

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

    Private Sub pic_Assitance_Click(sender As Object, e As EventArgs) Handles pic_Assitance.Click
        If pic_Assitance.Cursor = Cursors.Hand Then
            Me.Enabled = False
            Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\msra.exe", "/offerra " & ComputerName)
            Thread.Sleep(1000)
            Me.pic_Assitance.BorderStyle = BorderStyle.None
        End If
        Me.Enabled = True
    End Sub

    Private Sub pic_remote_Click(sender As Object, e As EventArgs) Handles pic_remote.Click
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
            txtLogged.Text = "problem stopping branch cache"
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
            txtLogged.Text = "problem stopping branch cache"
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

    Private Sub UILanguage_Click(sender As Object, e As EventArgs) Handles Menu_Francais.Click, Menu_English.Click, ENToolStripMenuItem.Click, FRToolStripMenuItem.Click
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
                        If Advance_mode = True Then Me.AdvancedMode_Menu.Visible = True Else Me.AdvancedMode_Menu.Visible = False
                    Next child
                End If
            Next c

            'Correction d'un BUG....Utiliser pour le reset des tooltips quand on change la langue
            Me.TT.SetToolTip(Me.pic_Assitance, My.Resources.ToolTip_Main_Assistance)
            Me.TT.SetToolTip(Me.pic_Explorer, My.Resources.ToolTip_Main_Explorer)
            Me.TT.SetToolTip(Me.pic_Reboot, My.Resources.ToolTip_Main_Reboot)
            Me.TT.SetToolTip(Me.pic_remote, My.Resources.ToolTip_Main_remote)
            Me.TT.SetToolTip(Me.txt_SiteCode_result_NEW, My.Resources.ToolTip_Main_SiteCode_result)
            Me.TT.SetToolTip(Me.GroupBox2, My.Resources.ToolTip_Main_GroupBox2)
            'Me.TT.SetToolTip('Me.pic_UserGuide, My.Resources.ToolTip_UserGuide)

            Me.Refresh()
        End If
    End Sub

    Private Sub pic_Reboot_Click(sender As Object, e As EventArgs) Handles pic_Reboot.Click
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
            txt_PCName.Text = "..."
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

    Private Sub cmd_Force_WSUS_Click(sender As Object, e As EventArgs) Handles cmd_Force_WSUS.Click
        Dim popupRefreshWSUSForm As Popup_Refresh_WSUS = New Popup_Refresh_WSUS
        popupRefreshWSUSForm.ShowDialog(Me)
    End Sub

    Private Sub cmd_Force_Apps_update_Click(sender As Object, e As EventArgs) Handles cmd_Force_Apps_update.Click
        Dim popupRefreshApps As Popup_Refresh_Apps = New Popup_Refresh_Apps
        popupRefreshApps.ShowDialog(Me)
    End Sub

    Private Sub pic_Explorer_Click(sender As Object, e As EventArgs) Handles pic_Explorer.Click
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$")
        Catch ex As Exception
            Me.pic_Explorer.BorderStyle = BorderStyle.None
            ' Gestion de l'erreur
        End Try
        Me.pic_Explorer.BorderStyle = BorderStyle.None
    End Sub

    Private Sub cmd_Reinstall_client_Click(sender As Object, e As EventArgs) Handles cmd_Reinstall_client.Click

        'Valide que le fichier INI est la 
        If CheckFileExists(INI_Files) = False Then
            MsgBox(My.Resources.Message_file_ini_missing, MsgBoxStyle.Critical)
            INI_READ_ERROR = True
            Exit Sub
        Else
            INI_READ_ERROR = False
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

            Dim popupReinstallClientForm As Popup_Reinstall_Client = New Popup_Reinstall_Client
            popupReinstallClientForm.ShowDialog(Me)
        End If
    End Sub

#Region "Service Windows"
    Private _serviceWindows As IEnumerable(Of ServiceWindow)
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
            MsgBox(My.Resources.ErrorRegistryConnection, MsgBoxStyle.Critical, "SCCM PC Admin")
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

    Private Sub cmd_GCProfile_log_Click(sender As Object, e As EventArgs)
        Dim WebPage = "http://gcprofilelog/"
        Process.Start(WebPage)
    End Sub

    Private Sub cmd_GCProfile_PC_Click(sender As Object, e As EventArgs)
        Dim WebPage = ("http://gcprofilelog?wsname=" & ComputerName)
        Process.Start(WebPage)
    End Sub

    Private Sub cmd_GCProfile_User_Click(sender As Object, e As EventArgs)
        Dim WebPage = ("http://gcprofilelog?username=" & Trim(User))
        Process.Start(WebPage)
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
        Me.Close()
    End Sub

    Private Sub GCProfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GCProfileToolStripMenuItem.Click
        Dim WebPage = ("http://gcprofilelog?wsname=" & ComputerName)
        Process.Start(WebPage)
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
        If MainTab.SelectedIndex = e.Index Then

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

    Public Sub MainTab_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MainTab.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Try
            Select Case MainTab.SelectedIndex
                Case 0 'TAB 1
                    If loadExecutionPKGSTab < 1 Then
                        ShowExecutionHistoryPKGS()
                    End If
                    loadExecutionPKGSTab = loadExecutionPKGSTab + 1
                Case 1
                    If loadExecutionAPPSTab < 1 Then
                        ShowExecutionHistoryPKGS()
                    End If
                    ShowExecutionHistoryAPPS()
                    loadExecutionAPPSTab = loadExecutionAPPSTab + 1
                Case 2
                    If loadRunningPKGSTab < 1 Then
                        ShowRunningPKGS()
                    End If
                    loadRunningPKGSTab = loadRunningPKGSTab + 1
                Case 3
                    If loadAdvertisementsTab < 1 Then
                        ShowAdvertisements()
                    End If
                    loadAdvertisementsTab = loadAdvertisementsTab + 1
                Case 4
                    If loadInfoTab < 1 Then
                        ShowInfoTab()
                    End If
                    loadInfoTab = loadInfoTab + 1
                    'Case 5-8 go here
                Case 9
                    If loadServiceWindows < 10 Then
                        RefreshServiceWindows()
                    End If
                    loadServiceWindows = loadServiceWindows + 1
            End Select

        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
        Me.Cursor = Cursors.Default


    End Sub

    Public Sub ShowExecutionHistoryPKGS()

        onetime = 0
        Me.Refresh()
        If ListView1.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView1.Items(0).Selected = True
            ListView1.Select()
            Label1.Visible = True
            'Exit Select
        End If
        ListView1.Items.Clear()
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

                'ListView1.Sorting = Windows.Forms.SortOrder.Ascending
                Me.ListView1.Sorting = Windows.Forms.SortOrder.None

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
                ListView1.Items.Add(item)

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
        AddHandler Me.ListView1.ColumnClick, AddressOf ColumnClick
        Label1.Visible = True

        If ListView1.Items.Count > 0 Then
            ListView1.Items(0).Selected = True
            ListView1.Select()
        End If
        Me.Refresh()

    End Sub

    Private Sub ColumnClick(sender As Object, e As ColumnClickEventArgs)
        ' Set the ListViewItemSorter property to a new ListViewItemComparer object.
        Dim sortIt As SortOrder

        If e.Column <> lastCol Then

            Select Case Tab_Select
                Case 1
                    ListView1.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                Case 2
                    ListView2.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                Case 3
                    ListView3.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                Case 4
                    ListView4.Sorting = SortOrder.Descending
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
                If ListView1.Sorting = SortOrder.Descending Then
                    ListView1.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    ListView1.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

            Case 2
                If ListView2.Sorting = SortOrder.Descending Then
                    ListView2.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    ListView2.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.ListView2.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

            Case 3
                If ListView3.Sorting = SortOrder.Descending Then
                    ListView3.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    ListView3.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.ListView3.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

            Case 4
                If ListView4.Sorting = SortOrder.Descending Then
                    ListView4.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    ListView4.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.ListView4.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

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

        If ListView2.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView2.Items(0).Selected = True
            ListView2.Select()
            'cmd_apps_refresh.Visible = True
            'Exit Select
        End If

        ListView2.Items.Clear()
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
                    Me.ListView2.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(AppName)
                    item.SubItems.Add(AppStatus)
                    item.SubItems.Add(EvaluationState)
                    item.SubItems.Add(Date_Start)
                    item.SubItems.Add(Date_Dealine)
                    item.SubItems.Add(Date_LastEvalTime)
                    item.SubItems.Add(Date_LastInstallTime)
                    ListView2.Items.Add(item)
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
        AddHandler Me.ListView2.ColumnClick, AddressOf ColumnClick

        'Active le autosize
        ColumnHeader5.Width = -2
        ColumnHeader6.Width = -2
        ColumnHeader17.Width = -2
        ColumnHeader18.Width = -2
        ColumnHeader19.Width = -2
        ColumnHeader20.Width = -2
        ColumnHeader21.Width = -2

        If ListView2.Items.Count > 0 Then
            ListView2.Items(0).Selected = True
            ListView2.Select()
        End If
        Me.Refresh()

    End Sub

    Sub ShowRunningPKGS()
        onetime = 0
        Me.Refresh()

        If ListView3.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView3.Items(0).Selected = True
            ListView3.Select()
            'Exit Select
        End If

        ListView3.Items.Clear()
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
                    Me.ListView3.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(AppID)
                    item.SubItems.Add(AppName)
                    item.SubItems.Add(AppStatus)
                    item.SubItems.Add(AppDate)
                    If tasksequence = True Then
                        item.BackColor = Color.LightBlue
                        item.ForeColor = Color.DarkBlue
                    End If
                    ListView3.Items.Add(item)
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
            'GEstion de l'erreur
        End Try

        'Commande pour le sort de la colonne
        Tab_Select = 3
        AddHandler Me.ListView3.ColumnClick, AddressOf ColumnClick
        If ListView3.Items.Count > 0 Then
            ListView3.Items(0).Selected = True
            ListView3.Select()
        End If
        Me.Refresh()

    End Sub

    Sub ShowAdvertisements()
        onetime = 0
        Me.Refresh()

        If ListView4.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView4.Items(0).Selected = True
            ListView4.Select()
            Label1.Visible = True
            'Exit Select
        End If

        ListView4.Items.Clear()
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
                    Me.ListView4.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(AppID)
                    item.SubItems.Add(AppName)
                    item.SubItems.Add(AppAdv)
                    If tasksequence = True Then
                        item.BackColor = Color.LightBlue
                        item.ForeColor = Color.DarkBlue
                    End If

                    If Not Microsoft.VisualBasic.Left(info("PKG_Name"), 1) = "*" Then ListView4.Items.Add(item)
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
            'GEstion de l'erreur
        End Try

        'Commande pour le sort de la colonne
        Tab_Select = 4
        AddHandler Me.ListView4.ColumnClick, AddressOf ColumnClick
        Label1.Visible = True
        If ListView4.Items.Count > 0 Then
            ListView4.Items(0).Selected = True
            ListView4.Select()
        End If
        Me.Refresh()

    End Sub

    Sub ShowInfoTab()

        onetime = 0
        Me.Refresh()

        If ListView5.Items.Count <> 0 Then
            'La liste n'est pas vide donc bypass le Select
            ListView5.Items(0).Selected = True
            ListView5.Select()
            'Exit Select
        End If

        ListView5.Items.Clear()
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
                    Me.ListView5.Sorting = Windows.Forms.SortOrder.None
                    Dim item As New ListViewItem(AppContentID)
                    item.SubItems.Add(AppLocation)
                    item.SubItems.Add(AppCacheID)
                    ListView5.Items.Add(item)
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
        AddHandler Me.ListView5.ColumnClick, AddressOf ColumnClick
        'Label1.Visible = True
        If ListView5.Items.Count > 0 Then
            ListView5.Items(0).Selected = True
            ListView5.Select()
        End If
        Me.Refresh()
    End Sub

    Private Sub ListExecutionHistoryPKGS_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick, Tab_pkg_app.DoubleClick, ServiceWindowsListView.DoubleClick
        ShowExecutionHistoryPKGS()
    End Sub

    Private Sub LoadMorePcInfo()
        Me.Cursor = Cursors.WaitCursor

        Me.Text = "SCCM PC Admin  " & ComputerName

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
        txt_OSCaption.Text = OSName
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

            Dim Group_Val As String
            Using ctx As New PrincipalContext(ContextType.Domain)
                Using p = Principal.FindByIdentity(ctx, ComputerName)
                    If Not p Is Nothing Then
                        Dim groups = p.GetGroups()
                        For Each group In groups
                            Group_Val = group.DisplayName
                            If Not Group_Val = "" Then
                                'ListView3.Sorting = Windows.Forms.SortOrder.Ascending
                                Me.MembershipListView.Sorting = Windows.Forms.SortOrder.None
                                Dim item As New ListViewItem(Group_Val)
                                MembershipListView.Items.Add(item)
                                Me.Update()
                            End If
                            Me.Update()
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            'GEstion de l'erreur
        End Try


        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnCenterConsole_Click(sender As Object, e As EventArgs) Handles btnCenterConsole.Click

        MainTab.Width = 1437
        SecondaryTab.Hide()
        MainTab.Refresh()
        btnCenterConsole.Hide()
        btnCenterConsole2.Show()
        Me.Refresh()
    End Sub

    Private Sub btnCenterConsole2_Click(sender As Object, e As EventArgs) Handles btnCenterConsole2.Click

        MainTab.Width = 773
        SecondaryTab.Show()
        MainTab.Refresh()
        btnCenterConsole.Show()
        btnCenterConsole2.Hide()
        Me.Refresh()
    End Sub

    Private Sub UserGuideToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UserGuideToolStripMenuItem1.Click

    End Sub


    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click

    End Sub

    Private Sub Menu_Option_Click(sender As Object, e As EventArgs) Handles Menu_Option.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class

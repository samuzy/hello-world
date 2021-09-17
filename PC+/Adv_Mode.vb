
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.IO
Imports System.Management
Imports System.Reflection
Imports System.Threading 'Thread.Sleep(1000)
Imports Microsoft.Win32

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'       Ce module ne peux que est vu que par les gens qui appartienne au groupe de deploiment 'Public Function GetGroups' (ESDC CM12 Workstation Administrators)
'       Contient des fonction qui ne doivent etres exécuter que pas les menbre de ce groups car ce son des application plus précise dans le depanage d un probleme de client
'
' *******************************************************************************************************************************************************


Public Class Adv_Mode
    Inherits LocalizedForm
    Private Sub Adv_Mode_Load(sender As Object, e As EventArgs) Handles Me.Load
        txt_ComputerName.Text = ComputerName

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
        txt_IP.Text = IPAddress_Value
        txt_Mac.Text = MacAddress(ComputerName)

        lbl_loading.Visible = False
    End Sub

    Private Sub SBranchCache()
        Me.Cursor = Cursors.WaitCursor

        Dim regKey As RegistryKey
        Dim regSubKey As RegistryKey

        pic_greenflag1.Visible = False
        pic_greenflag2.Visible = False
        pic_redflag1.Visible = False
        pic_redflag2.Visible = False
        cmd_Port_8009.Enabled = False

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

                Me.txt_ConnectPort.Text = regSubKey.GetValue("ConnectPort")
                Me.txt_ListenPort.Text = regSubKey.GetValue("ListenPort")
                If Not Me.txt_ConnectPort.Text = "8009" Then
                    Me.pic_redflag1.Visible = True
                    Me.pic_greenflag1.Visible = False

                Else
                    Me.pic_redflag1.Visible = False
                    Me.pic_greenflag1.Visible = True

                End If

                If Not Me.txt_ListenPort.Text = "8009" Then
                    Me.pic_redflag2.Visible = True
                    Me.pic_greenflag2.Visible = False

                Else
                    Me.pic_redflag2.Visible = False
                    Me.pic_greenflag2.Visible = True

                End If

                If Not Me.txt_ConnectPort.Text = "8009" Or Not Me.txt_ListenPort.Text = "8009" Then cmd_Port_8009.Enabled = True Else cmd_Port_8009.Enabled = False
            Else

                Me.txt_ConnectPort.Text = regSubKey.GetValue("ConnectPort")
                Me.txt_ListenPort.Text = regSubKey.GetValue("ListenPort")
                If Not Me.txt_ConnectPort.Text = "8009" Then
                    Me.pic_redflag1.Visible = True
                    Me.pic_greenflag1.Visible = False
                Else
                    Me.pic_redflag1.Visible = False
                    Me.pic_greenflag1.Visible = True
                End If

                If Not Me.txt_ListenPort.Text = "8009" Then
                    Me.pic_redflag2.Visible = True
                    Me.pic_greenflag2.Visible = False
                Else
                    Me.pic_redflag2.Visible = False
                    Me.pic_greenflag2.Visible = True
                End If

                If Not Me.txt_ConnectPort.Text = "8009" Or Not Me.txt_ListenPort.Text = "8009" Then cmd_Port_8009.Enabled = True Else cmd_Port_8009.Enabled = False
            End If

            regSubKey.Close()
            regKey.Close()

        Catch ex As Exception

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
            txt_Cache_Size.Text = Format(cache_size / 1024, "00.00")
            txt_cache_location.Text = cache_location
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmd_Client_Logs_Click(sender As Object, e As EventArgs) Handles cmd_Client_Logs.Click
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\Windows\CCM\Logs")
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub cmd_WSUS_Download_Click(sender As Object, e As EventArgs) Handles cmd_WSUS_Download.Click
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\Windows\SoftwareDistribution\Download")
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub cmd_BITS_Location_Click(sender As Object, e As EventArgs) Handles cmd_BITS_Location.Click
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\ProgramData\Microsoft\Network\Downloader")
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub cmd_DataStore_Click(sender As Object, e As EventArgs) Handles cmd_DataStore.Click
        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$\Windows\SoftwareDistribution\DataStore")
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub cmd_Rebuilding_WMI_Click(sender As Object, e As EventArgs) Handles cmd_Rebuilding_WMI.Click
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

    Private Sub cmd_Re_Registering_Click(sender As Object, e As EventArgs) Handles cmd_Re_Registering.Click
        Me.Cursor = Cursors.WaitCursor

        'Reregister DLLs "C:\windows\system32\wbem"
        Dim strSDPath
        strSDPath = "\\" & ComputerName & "\admin$\System32\wbem"

        Dim fileEntries As String() = Directory.GetFiles(strSDPath)
        Dim fileName As String
        For Each fileName In fileEntries

            Dim Str_FileName = Mid(fileName, fileName.LastIndexOf("\") + 2)

            Select Case UCase(Microsoft.VisualBasic.Right(Str_FileName, 3))
                Case "DLL"
                    Cursor = Cursors.WaitCursor
                    RemoteExec("RegSvr32.exe /s " + Str_FileName)
                    Cursor = Cursors.Arrow
            End Select

            'Gestion des 6 fichiers a ré-enregistrer
            Select Case Str_FileName

                Case "scrcons.exe", "unsecapp.exe", "WMIADAP.exe", "WmiApSrv.exe", "WmiPrvSE.exe"
                    Cursor = Cursors.WaitCursor
                    RemoteExec(Str_FileName + " /RegServer.exe")
                    Cursor = Cursors.Arrow
            End Select

        Next fileName
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmd_Del_WMI_Click(sender As Object, e As EventArgs) Handles cmd_Del_WMI.Click
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

    Private Sub txt_cache_location_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles txt_cache_location.MouseDoubleClick

        Dim Path = Mid(txt_cache_location.Text, 3, Len(txt_cache_location.Text))

        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$" & Path)
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub txt_cache_location_MouseClick(sender As Object, e As MouseEventArgs) Handles txt_cache_location.MouseClick

        Dim Path = Mid(txt_cache_location.Text, 3, Len(txt_cache_location.Text))

        Try
            Process.Start("C:\utils-outils\Explorer++.exe", "\\" & ComputerName & "\c$" & Path)
        Catch ex As Exception
            ' Gestion de l'erreur
        End Try
    End Sub

    Private Sub cmd_Port_8009_Click(sender As Object, e As EventArgs) Handles cmd_Port_8009.Click
        Me.Cursor = Cursors.WaitCursor

        Dim regKey As RegistryKey
        Dim regSubKey As RegistryKey

        'Mise en place des valeur par default
        pic_greenflag1.Visible = False
        pic_greenflag2.Visible = False
        pic_redflag1.Visible = False
        pic_redflag2.Visible = False
        cmd_Port_8009.Enabled = False
        Me.lbl_loading.Visible = True

        'Arret du BrandCache
        RemoteExec("cmd /c netsh BranchCache set service mode=DISABLED")
        Thread.Sleep(2000)

        'Arret du BrandCache
        RemoteExec("cmd /c netsh BranchCache RESET")
        Thread.Sleep(2000)

        Try
            regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName)
            regSubKey = regKey.OpenSubKey("Software\Microsoft\Windows NT\CurrentVersion\PeerDist\DownloadManager\Peers\Connection", True)
            regSubKey.SetValue("ConnectPort", 8009, RegistryValueKind.DWord)
            regSubKey.SetValue("ListenPort", 8009, RegistryValueKind.DWord)


            Me.txt_ConnectPort.Text = regSubKey.GetValue("ConnectPort")
            Me.txt_ListenPort.Text = regSubKey.GetValue("ListenPort")
            pic_greenflag1.Visible = True
            pic_greenflag2.Visible = True
            pic_redflag1.Visible = False
            pic_redflag2.Visible = False
            cmd_Port_8009.Enabled = False

        Catch ex As Exception

            pic_greenflag1.Visible = False
            pic_greenflag2.Visible = False
            pic_redflag1.Visible = True
            pic_redflag2.Visible = True
            cmd_Port_8009.Enabled = True
            Cursor = Cursors.Arrow
            Me.lbl_loading.Visible = False

        End Try

        Try
            'Envoye de la comande pour le brandcache
            RemoteExec("cmd /c netsh BranchCache set service mode=DISTRIBUTED")
            Thread.Sleep(2000)

        Catch ex As Exception
            Cursor = Cursors.Default
            Me.lbl_loading.Visible = False
        End Try

        Cursor = Cursors.Default
        Me.lbl_loading.Visible = False
    End Sub

    Private Sub cmd_GPO_Click(sender As Object, e As EventArgs) Handles cmd_GPO.Click
        Me.Cursor = Cursors.WaitCursor

        Dim Result_msg = MsgBox(My.Resources.ConfirmGPOUpdate & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.TitleGPUpdate & " : " & ComputerName)

        If Result_msg = 6 Then
            RemoteExec("cmd /c gpupdate /force /boot")
            Reboot_Send = True
            Thread.Sleep(5000)
            ComputerName = ""
            Main.Instance.txt_PCName.Text = "..."
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

    Private Sub cmd_Client_Click(sender As Object, e As EventArgs)

        'Read_INI(UCase(PC_Domain))

        Dim advMode_SCCM_CLient As Adv_Mode_SCCM_CLient = New Adv_Mode_SCCM_CLient
        advMode_SCCM_CLient.ShowDialog(Me)

        'Me.Cursor = Cursors.WaitCursor
        ''arret du service sccm
        'Service_to_OFF("ccmexec", False)

        'Dim strSDPath
        'strSDPath = "\\" & ComputerName & "\admin$\ccmsetup\"
        'RemoteExec(strSDPath + "ccmsetup.exe /uninstall")

        ''Temp d'attente pour que le Uninstall Termine
        'Thread.Sleep(10000)

        'Dim UnInstall As Boolean = False
        'Do While UnInstall = False
        '    Dim strObject = "winmgmts://" & ComputerName
        '    For Each Process In GetObject(strObject).InstancesOf("win32_process")
        '        If UCase(Process.name) = UCase("CCMSETUP.EXE") Then
        '            UnInstall = False
        '            Exit For
        '        Else
        '            UnInstall = True
        '        End If
        '    Next
        'Loop

        ''*********** Suprimmer les répertoires du SCCM ***************"
        'Try
        '    strSDPath = "\\" & ComputerName & "\admin$\CCM"
        '    System.IO.Directory.Delete(strSDPath, True)
        'Catch ex As Exception
        '    'Gestion de l'erreur
        'End Try

        'Try
        '    strSDPath = "\\" & ComputerName & "\admin$\ccmcache"
        '    System.IO.Directory.Delete(strSDPath, True)
        'Catch ex As Exception
        '    'Gestion de l'erreur
        'End Try

        'Try
        '    strSDPath = "\\" & ComputerName & "\admin$\ccmsetup"
        '    System.IO.Directory.Delete(strSDPath, True)
        'Catch ex As Exception
        '    'Gestion de l'erreur
        'End Try


        ''Mes le site et le cient version en mode vide
        'Main.Instance.txt_Client_Version_Result.Text = "?"
        'Main.Instance.txt_SiteCode_result.Text = "?"
        'SiteCode = "?"
        'ClientVer = "?"


        'Me.Cursor = Cursors.Default
    End Sub

    Private Sub cmd_registry_pol_Click(sender As Object, e As EventArgs) Handles cmd_registry_pol.Click
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
            Main.Instance.txt_PCName.Text = "..."
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

    Private Sub cmd_load_Logs1_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs1.Click
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs1.Text = "" Or cmb_Logs1.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs2.Text = ""
        cmb_Logs3.Text = ""
        cmb_Logs4.Text = ""
        cmb_Logs5.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs1.Text, True)

    End Sub

    Private Sub cmd_load_Logs2_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs2.Click
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs2.Text = "" Or cmb_Logs2.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1.Text = ""
        cmb_Logs3.Text = ""
        cmb_Logs4.Text = ""
        cmb_Logs5.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs2.Text, True)
    End Sub

    Private Sub cmd_load_Logs3_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs3.Click
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs3.Text = "" Or cmb_Logs3.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1.Text = ""
        cmb_Logs2.Text = ""
        cmb_Logs4.Text = ""
        cmb_Logs5.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs3.Text, True)

    End Sub

    Private Sub cmd_load_Logs4_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs4.Click
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs4.Text = "" Or cmb_Logs4.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1.Text = ""
        cmb_Logs2.Text = ""
        cmb_Logs3.Text = ""
        cmb_Logs5.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs4.Text, True)

    End Sub

    Private Sub cmd_load_Logs5_Click(sender As Object, e As EventArgs) Handles cmd_load_Logs5.Click
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs5.Text = "" Or cmb_Logs5.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1.Text = ""
        cmb_Logs2.Text = ""
        cmb_Logs3.Text = ""
        cmb_Logs4.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs5.Text, True)

    End Sub

    Private Sub cmb_Logs1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Logs1.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs1.Text = "" Or cmb_Logs1.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs2.Text = ""
        cmb_Logs3.Text = ""
        cmb_Logs4.Text = ""
        cmb_Logs5.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs1.Text, False)

    End Sub

    Private Sub cmb_Logs2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Logs2.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs2.Text = "" Or cmb_Logs2.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1.Text = ""
        cmb_Logs3.Text = ""
        cmb_Logs4.Text = ""
        cmb_Logs5.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs2.Text, False)
    End Sub

    Private Sub cmb_Logs3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Logs3.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs3.Text = "" Or cmb_Logs3.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1.Text = ""
        cmb_Logs2.Text = ""
        cmb_Logs4.Text = ""
        cmb_Logs5.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs3.Text, False)
    End Sub

    Private Sub cmb_Logs4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Logs4.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs4.Text = "" Or cmb_Logs4.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1.Text = ""
        cmb_Logs2.Text = ""
        cmb_Logs3.Text = ""
        cmb_Logs5.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs4.Text, False)
    End Sub

    Private Sub cmb_Logs5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmb_Logs5.SelectedIndexChanged
        'Valide que la valleur vide ou ... ne sois pas pris en compte
        If cmb_Logs5.Text = "" Or cmb_Logs5.Text = "..." Then Exit Sub

        'Reset des autre Combobox
        cmb_Logs1.Text = ""
        cmb_Logs2.Text = ""
        cmb_Logs3.Text = ""
        cmb_Logs4.Text = ""

        'Envoye la valeur sélectionner pour ouvrir le fichier log
        Logs_Call(cmb_Logs5.Text, False)
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

    Private Sub cmd_Collection_Click(sender As Object, e As EventArgs)

        Dim advMode_SCCM_Collection As Adv_Mode_Collection = New Adv_Mode_Collection
        advMode_SCCM_Collection.ShowDialog(Me)

    End Sub
End Class

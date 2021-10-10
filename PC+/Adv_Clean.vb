
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

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
'       Module qui est utiliser pour réparrer les client divers opération de nettoyage, supression de répertoire, exécution de commande a distance 
'       Les temps d'attente (Sleep) son important du a des problème de réseau dans divers bureau
'
' *******************************************************************************************************************************************************


Public Class Adv_Clean
    Inherits LocalizedForm

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
    Dim strComputer, strRService, strSName
    Dim fso = CreateObject("Scripting.FileSystemObject")

    Private Sub Adv_Clean_Load(sender As Object, e As EventArgs) Handles Me.Load
        'place les valeur par default
        TimerBar_Adv_clean = 0
        TimerBar_Adv_clean_now = 0
        bError = False
        strComputer = UCase(ComputerName)

        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        lbl_loading.Visible = False
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
        Me.Close()
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

End Class

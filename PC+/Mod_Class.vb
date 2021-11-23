
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.IO
Imports System.Management
Imports System.Security.Principal
Imports System.ServiceProcess
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Xml
Imports Microsoft.Win32
Imports System.DirectoryServices
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Data.Sql
Imports System.Data


Module Modules
    'Variable pour Active Directory
    Public str_AD_computer As String

    'Variable pour le fichier de config
    Public INI_Files As String = "C:\Utils-outils\SCCMPCAdmin\SCCMPCAdmin.ini"  'cette commande ne marche pas quand on pass avec un RunAs DOS = Path.GetDirectoryName(Application.ExecutablePath) & "\SCCMPCAdmin.ini"
    Public INI_SiteCode As String
    Public INI_Primary_Site_Server As String
    Public INI_Version As String
    Public INI_PATH As String
    Public INI_Command As String
    Public INI_READ_ERROR As Boolean
    Public INI_SQL_Server As String
    Public INI_SQL_Database As String
    Public INI_REINSTALLPATH As String

    'Variable CMTrace.exe
    Public CMTrace As String = "C:\Utils-outils\SCCMPCAdmin\CMTrace.exe" 'cette commande ne marche pas quand on pass avec un RunAs DOS = Path.GetDirectoryName(Application.ExecutablePath) & "CMTrace.exe"

    'Variable pour le PC
    Public CORE_Image_version As String = "?"
    Public OSName As String = "?"
    Public VerImg_data As String = "?"
    Public SRU_VerImg_data As String = "?"
    Public OSLanguage As String = "?"
    Public str_LastBootUpTime As String = "?"
    Public str_InstallDate As String = "?"
    Public PC_Mem_size As String = "?"
    Public Need_Reboot As Boolean = False
    Public ComputerName As String
    Public IPAddress_Value As String
    Public IPSubNet_Value As String
    Public MacAddress_Value As String
    Public DNS_Name_Value As String
    Public PC_Domain As String = ""
    Public PC_Status As Boolean
    Public SiteCode As String
    Public ClientVer As String
    Public ManagementPoint As String
    Public SCCM_Catalogue_Number As String
    Public SCCM_WSUS_Server As String
    Public User As String
    Public User_Reg As String
    Public MultiUser As Integer = 0
    Public onetime As Integer
    Public Err_Services_Acces As Boolean
    Public Err_RemoteRegistry_Acces As Boolean
    Public Err_MPSSVC_Acces As Boolean
    Public Err_CCMEXEC_Acces As Boolean
    Public Err_BITS_Acces As Boolean
    Public Err_PeerDistSvc_Acces As Boolean
    Public Err_wuauserv_Acces As Boolean
    Public objOS As ManagementObjectSearcher
    Public objCS As ManagementObjectSearcher
    Public objMgmt As ManagementObject
    Public PCName As PC_CheckDevice
    Public Username As String
    Public Advance_mode_NET, Advance_mode_DEV, Advance_mode_CAWS, Advance_mode As Boolean
    Public Software_Hide As Boolean
    Public m_strChassisTypes
    Public ID As Integer
    Public str_WSUS_ID(1000) As String
    Public Process_return As Integer
    Public SDate
    Public Service_ON_OFF As String
    Public errorForceBatchFilesFlag As Boolean = False
    Public TimeZone_PC As Double
    Public Remote_TimeZone_PC As Date
    Public AddTimers_Select As Integer
    Public oProcess, colServiceList
    Public retval, oWMI, IsObject, bError, oProg, strOS
    Public MW_Select
    Public Const HKLM = &H80000002
    Public Const HKCU = &H80000001
    Public Const OVERRIDE_MAINTENANCE_WINDOWS = &H100000
    Public Const SCCM_MAX_DISPLAY_DAYS = 90
    Public Const PROG_RERUN_ALWAYS = "RerunAlways"
    Public Const PROG_RERUN_IF_FAIL = "RerunIfFail"
    Public Const SCCM_PKG_HIST_REG_x86 As String = "SOFTWARE\Microsoft\SMS\Mobile Client\Software Distribution\Execution History\System"
    Public Const SCCM_PKG_HIST_EXEC_PROG_x86 = "SOFTWARE\Microsoft\SMS\Mobile Client\Software Distribution\Execution History\System"
    Public Const SCCM_REG_x86 As String = "SOFTWARE\Microsoft\SMS"
    Public Const REG_Install_Programs_x64 As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"
    Public Const REG_Install_Programs_x86 As String = "SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
    Public Step_Install_time, Step_Uninstall_time, Step_Copie_time, Step_end
    Public timeoutMilliseconds = 30000
    Public Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds)
    Public TimerBar_Adv_clean_now As Integer = 0
    Public TimerBar_Adv_clean As Integer = 0
    Public Reboot_Send As Boolean = False
    Public Client_SCCM_Command_line As String = ""
End Module

Module SCCMServer
    'Cherche le Serveur principal SCCm pour la ré-installation du client SCCM


End Module

Module ActiveDirectory

    Public Sub GetAD_Info(PC As String)

        '("LDAP://DC=hrdc-drhc,DC=net")
        Try
            Dim dirsearch As New DirectorySearcher()
            Dim direntry As SearchResult
            dirsearch.Filter = "(&(ObjectClass=computer)(CN=" & ComputerName & "))"
            dirsearch.SearchScope = SearchScope.Subtree
            dirsearch.PropertiesToLoad.Add("distinguishedName")
            direntry = dirsearch.FindOne
            Dim str = direntry.Properties("distinguishedName")(0).ToString
            'Dim OU As String = str.Split(","c)(1)
            If Not direntry Is Nothing Then
                str_AD_computer = str
            Else
                str_AD_computer = "NotFound"
            End If
        Catch ex As Exception
            str_AD_computer = "NotFound"
        End Try


    End Sub

End Module

Module Get_PC_Information
    Public Sub Get_PC_Info_WMI()

        '*************************************************************************************
        'WMI = SELECT * FROM Win32_OperatingSystem
        '************************************************************************************* 
        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\root\cimv2")
        Dim Query As New SelectQuery("SELECT * FROM Win32_OperatingSystem")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)

        Try
            For Each info As ManagementObject In search.Get()
                OSLanguage = info("OSLanguage")
                str_LastBootUpTime = info("LastBootUpTime")
                OSName = info("Caption").ToString()
                str_InstallDate = info("InstallDate")
            Next
        Catch ex As Exception
        End Try

        '*************************************************************************************
        'WMI = SELECT * FROM Win32_ComputerSystem
        '*************************************************************************************
        Dim Query2 As New SelectQuery("SELECT * FROM Win32_ComputerSystem")
        Dim search2 As New ManagementObjectSearcher(WMI_Info, Query2)

        Try
            For Each info2 As ManagementObject In search2.Get()
                PC_Domain = info2("Domain")
                PC_Mem_size = Memory_Convert(info2("TotalPhysicalMemory"))
            Next
        Catch ex As Exception
        End Try

        '*************************************************************************************
        'WMI = SELECT * FROM Win32_PhysicalMemory (Chassis info via le type de mémoire)
        '*************************************************************************************
        Dim Query3 As New SelectQuery("Select * from Win32_PhysicalMemory")
        Dim search3 As New ManagementObjectSearcher(WMI_Info, Query3)

        Dim info3 As ManagementObject
        Try
            For Each info3 In search3.Get()
                m_strChassisTypes = info3("FormFactor")
            Next
        Catch ex As Exception
            m_strChassisTypes = "0"
        End Try

        If m_strChassisTypes = "12" Then
            m_strChassisTypes = "MOBILE_DEVICE"
        ElseIf m_strChassisTypes = "0" Then
            'm_strChassisTypes = "?"
            m_strChassisTypes = "LAPTOP"
        Else
            m_strChassisTypes = "DESKTOP"
        End If
        '*************************************************************************************
        'WMI = SELECT * FROM SMS_Authority
        '*************************************************************************************
        Dim WMI_Info_CCM As New ManagementScope("\\" & ComputerName & "\root\ccm")
        Dim Query4 As New SelectQuery("SELECT * FROM SMS_Authority")
        Dim search4 As New ManagementObjectSearcher(WMI_Info_CCM, Query4)

        Dim info4 As ManagementObject
        Try
            For Each info4 In search4.Get()
                SiteCode = Mid(info4("Name").ToString(), 5, 3)
                ManagementPoint = info4("CurrentManagementPoint").ToString()
            Next
        Catch ex As Exception
            SiteCode = "?"
            ManagementPoint = "?"
        End Try
        '*************************************************************************************
        'WMI = SELECT * FROM SMS_Client
        '*************************************************************************************
        Dim Query5 As New SelectQuery("SELECT * FROM SMS_Client")
        Dim search5 As New ManagementObjectSearcher(WMI_Info_CCM, Query5)

        Dim info5 As ManagementObject
        Try
            For Each info5 In search5.Get()
                ClientVer = info5("ClientVersion").ToString()
            Next
        Catch ex As Exception
            ClientVer = "?"
        End Try

        '*************************************************************************************
        'WMI = SELECT * FROM SMS_Client
        '*************************************************************************************
        Dim WMI_Info_WSUS As New ManagementScope("\\" & ComputerName & "\root\ccm\SoftwareUpdates\WUAHandler")
        Dim Query6 As New SelectQuery("SELECT * FROM CCM_UpdateSource")
        Dim search6 As New ManagementObjectSearcher(WMI_Info_WSUS, Query6)

        Dim info6 As ManagementObject
        Try
            For Each info6 In search6.Get()
                SCCM_Catalogue_Number = info6("ContentVersion").ToString()
                SCCM_WSUS_Server = info6("ContentLocation").ToString()
            Next
        Catch ex As Exception
            SCCM_Catalogue_Number = "?"
            SCCM_WSUS_Server = "?"
        End Try

    End Sub

    Public Sub Get_PC_Info_REG()
        Dim Reg As RegistryKey
        Dim KeyGOC As String
        Dim SubKey As RegistryKey

        Reg = Nothing

        'Clé pour les imformation de l'image
        KeyGOC = "SOFTWARE\GoC-GdC\Configuration\Image"

        Try
            Reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName)

            Try
                SubKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey("SOFTWARE\GoC-GdC\Configuration\Image" + "\", False)

                If Not SubKey.GetValue("Image Version", "") Is "" Then VerImg_data = CType(SubKey.GetValue("Image Version", ""), String)
                If Not SubKey.GetValue("Image Release Information", "") Is "" Then SRU_VerImg_data = CType(SubKey.GetValue("Image Release Information", ""), String)
                If Not SubKey.GetValue("CORE Image version", "") Is "" Then CORE_Image_version = CType(SubKey.GetValue("CORE Image version", ""), String)

            Catch ex As Exception
                'Erreur Level 2
            End Try
        Catch ex As Exception
            'Erreur Level 1
        End Try

        'Clé pour savoir si un reboot est en attente

        Try
            Reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName)
            SubKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired", False)
            If SubKey Is Nothing Then
                Need_Reboot = False
            Else
                Need_Reboot = True
            End If
        Catch ex As Exception
            'Erreur Level 1
        End Try

    End Sub
End Module

Module RemoteUser
    Private Declare Function inet_addr Lib "wsock32.dll" (ByVal cp As String) As Long
    Private Declare Function ntohl Lib "wsock32.dll" (ByVal netlong As Long) As Long

    Public Sub CMDAutomate(ByVal strCommand As String, ByRef Results As String)
        Dim myprocess As New Process
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        StartInfo.FileName = " cmd.exe " 'starts cmd window
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.UseShellExecute = False 'required to redirect
        StartInfo.CreateNoWindow = True 'creates no cmd window
        myprocess.StartInfo = StartInfo
        myprocess.Start()
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput
        SW.WriteLine(strCommand) 'the command you wish to run.....
        SW.WriteLine("exit") 'exits command prompt window
        Results = SR.ReadToEnd 'returns results of the command window
        Console.WriteLine("Results of running command : " & Results)
        SW.Close()
        SR.Close()
        'invokes Finished delegate, which updates textbox with the results text
        'Invoke(Finished)
    End Sub

    Public Sub RemoteExec(strCommand)
        Dim result, objInstance, processid
        processid = 0
        On Error Resume Next
        If Not IsObject(oProcess) Then WMIConnect()
        'Execute the command now on remote system 
        'command, working folder, startup config, PID
        result = oProcess.Create(strCommand, , , processid)
        If Not processid > 0 Then MsgBox("(" & strCommand & ") failed to run")
        On Error GoTo 0
    End Sub

    Public Sub Exec(strCommand)
        Dim wshShell = CreateObject("WScript.Shell")
        retval = wshShell.run(strCommand, 0, True)
    End Sub

    Public Sub WMIConnect()

        On Error Resume Next
        oWMI = GetObject("winmgmts://" & ComputerName & "/root/cimv2")
        If Err.Number <> 0 Then
            MsgBox(ComputerName & " (WMI Connection Error) " & Err.Description)
            Exit Sub
        End If
        ' Obtain the Win32_Process class of object.
        oProcess = oWMI.Get("Win32_Process")
        oProg = oProcess.Methods_("Create").InParameters.SpawnInstance_
        If Err.Number <> 0 Then MsgBox(ComputerName & " (WMI Error) " & Err.Description)
        On Error GoTo 0

        If bError Then Exit Sub
        Dim colProcess, objProcess
        colProcess = oWMI.ExecQuery("Select OSName from Win32_Process where " & "Name = 'winlogon.exe'")
        For Each objProcess In colProcess
            strOS = objProcess.OSName
        Next

    End Sub

    Public Function CheckFolderExists(ByVal folderPath As String) As Boolean
        CheckFolderExists = IO.Directory.Exists(folderPath)
    End Function

    Public Function CheckFileExists(ByVal filePath As String) As Boolean
        CheckFileExists = IO.File.Exists(filePath)
    End Function

    Public Function WMIDateConvert(ByVal incomingDate As String) As Date
        Try
            Return System.Management.ManagementDateTimeConverter.ToDateTime(incomingDate)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function Memory_Convert(PC_Mem_size_Value)
        Dim DoubleBytes As Double

        If PC_Mem_size_Value > 0 Then
            Try
                Select Case PC_Mem_size_Value
                    Case Is >= 1073741824
                        DoubleBytes = CDbl(PC_Mem_size_Value / 1073741824) 'GB
                        PC_Mem_size = FormatNumber(DoubleBytes, 0) & " GB"
                    Case 1048576 To 1073741823
                        DoubleBytes = CDbl(PC_Mem_size_Value / 1048576) 'MB
                        PC_Mem_size = FormatNumber(DoubleBytes, 0) & " MB"
                    Case 1024 To 1048575
                        DoubleBytes = CDbl(PC_Mem_size_Value / 1024) 'KB
                        PC_Mem_size = FormatNumber(DoubleBytes, 0) & " KB"
                    Case 0 To 1023
                        DoubleBytes = PC_Mem_size_Value ' bytes
                        PC_Mem_size = FormatNumber(DoubleBytes, 0) & " bytes"
                    Case Else
                        PC_Mem_size = "?"
                End Select
            Catch
                PC_Mem_size_Value = 0
            End Try
        End If

        Return PC_Mem_size
    End Function

    Public Function TimeZone(ByRef Computername As String)

        Dim objWMIService = GetObject("winmgmts:\\" & Computername & "\root\cimv2")
        Dim colItems = objWMIService.ExecQuery("Select * From Win32_LocalTime")


        For Each objItem In colItems

            'Gestion de la zone de temp de newfoundland .5 heurs

            Dim strRemote_Min = objItem.Minute
            Dim myMinute = DateTime.Now.Minute
            Dim strRemote_Hours = objItem.Hour
            Dim myHours = DateTime.Now.Hour
            Dim LocalTime As Date
            Dim RemoteTime As Date


            LocalTime = DateTime.Now.Hour & ":" & DateTime.Now.Minute
            RemoteTime = strRemote_Hours & ":" & strRemote_Min

            Dim GetTimeZone As TimeSpan = RemoteTime - LocalTime

            TimeZone_PC = GetTimeZone.TotalMinutes / 60

            Remote_TimeZone_PC = strRemote_Hours & ":" & strRemote_Min

        Next
        Return TimeZone_PC

    End Function

    Public Function IsIpValid(ByVal ipAddress As String)
        Dim expr As String = "^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$"
        Dim reg As Regex = New Regex(expr)
        If (reg.IsMatch(ipAddress)) Then
            Dim parts() As String = ipAddress.Split(".")
            If Convert.ToInt32(parts(0)) = 0 Then
                Return False
            ElseIf Convert.ToInt32(parts(3)) = 0 Then
                Return False
            End If
            For i As Integer = 1 To 4
                If i > 255 Then
                    Return False
                End If
            Next
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IPInRange(ByVal IP As String, ByVal IPStart As String, ByVal IPEnd As String) As Boolean
        Dim lngIP&, lngStart&, lngEnd&

        lngIP = ntohl(inet_addr(IP))
        lngStart = ntohl(inet_addr(IPStart))
        lngEnd = ntohl(inet_addr(IPEnd))

        If lngIP >= lngStart And lngIP <= lngEnd Then
            IPInRange = True
        Else
            IPInRange = False
        End If

        Exit Function

    End Function

    Public Function IPAddress(ByVal computername As String) As String
        'Main.Instance.Pic_VPN.Visible = False
        Dim mainObjInstance = Main.Instance
        Dim onVPN As Boolean = False

        Try
            Dim host = System.Net.Dns.GetHostEntry(computername)
            Dim ip = host.AddressList
            Dim index As Integer

            Dim VPN As Boolean = False
            'Moncton : 10.67.96.0/19 (VPN Client-to-Gate)
            Dim str_VPN_Moncton_Start As String = "10.67.96.1"
            Dim str_VPN_Moncton_End As String = "10.67.127.254"
            'Montréal : 10.57.96.0/19 (VPN Client-to-Gate)
            Dim str_VPN_Montreal_Start As String = "10.57.96.1"
            Dim str_VPN_Montreal_End As String = "10.57.127.254"
            'NHQ : 10.55.128.0/19 (VPN Client-to-Gate)
            Dim str_VPN_NHQ_Start As String = "10.55.0.0"
            Dim str_VPN_NHQ_End As String = "10.55.159.254"
            'Moncton: 10.65.64.0/19 (BranchOffice VPN Gate-to-Gate)
            Dim str_VPN_MonctonBranch_Start As String = "10.65.64.1"
            Dim str_VPN_MonctonBranch_End As String = "10.65.95.254"

            For index = 0 To ip.Length - 1
                'Bloc try pour géré les adresse IPv6
                Try
                    If Left(ip(index).MapToIPv4.ToString, 2) = "10" Then

                        'If (IPInRange(ip(index).MapToIPv4.ToString, str_VPN_Moncton_Start, str_VPN_Moncton_End)) Or (IPInRange(ip(index).MapToIPv4.ToString, str_VPN_Montreal_Start, str_VPN_Montreal_End)) Or (IPInRange(ip(index).MapToIPv4.ToString, str_VPN_NHQ_Start, str_VPN_NHQ_End)) Or (IPInRange(ip(index).MapToIPv4.ToString, str_VPN_MonctonBranch_Start, str_VPN_MonctonBranch_End)) Then
                        '    'VPN
                        '    MsgBox(computername & " " & My.Resources.WarningVPNSlowRequests, MsgBoxStyle.Exclamation, "SCCM PC Admin " & computername)
                        '    'Main.Instance.Pic_VPN.Visible = True
                        '    '            Me.Pic_ON_PeerDistSvc.Visible = False
                        '    '  Me.Pic_OFF_PeerDistSvc.Visible = True
                        '    'Main.Instance.Pic_OFF_PeerDistSvc.Visible = False
                        '    'Main.Instance.Pic_ON_PeerDistSvc.Visible = False
                        '    'mainObjInstance.DisableBranchCache()
                        '    onVPN = True

                        'End If
                        IPAddress_Value = ip(index).MapToIPv4.ToString
                    End If

                Catch ex As Exception
                    'Adresse IPv6
                    'Do nothing
                End Try
            Next index

            'If Not onVPN Then
            '    mainObjInstance.EnableBranchCache()
            'End If

        Catch ex As Exception
            IPAddress_Value = My.Resources.DNSError
            'Main.Instance.Pic_VPN.Visible = False
        End Try
        Return IPAddress_Value
    End Function

    Public Function MacAddress(ByVal computername As String) As String

        Dim theManagementScope As New ManagementScope("\\" & computername & "\ROOT\CIMV2")
        Dim theQueryString As New String("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1")

        Dim theObjectQuery As New ObjectQuery(theQueryString)

        Dim theSearcher As New ManagementObjectSearcher(theManagementScope, theObjectQuery)
        Dim theResultsCollection As ManagementObjectCollection = theSearcher.Get()

        For Each currentResult As ManagementObject In theResultsCollection
            Dim IP_Temp = currentResult("IPAddress")
            Dim IP_Val = IP_Temp(0)

            If IPAddress_Value = IP_Val Then
                MacAddress_Value = currentResult("MacAddress").ToString()
            End If
        Next

        If MacAddress_Value = "" Then MacAddress_Value = "?"

        Return MacAddress_Value
    End Function

    Public Function IPSubNet(ByVal computername As String) As String

        Dim theManagementScope As New ManagementScope("\\" & computername & "\ROOT\CIMV2")
        Dim theQueryString As New String("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1")

        Dim theObjectQuery As New ObjectQuery(theQueryString)

        Dim theSearcher As New ManagementObjectSearcher(theManagementScope, theObjectQuery)
        Dim theResultsCollection As ManagementObjectCollection = theSearcher.Get()

        For Each currentResult As ManagementObject In theResultsCollection
            Dim IP_Temp = currentResult("IPAddress")
            Dim IP_Val = IP_Temp(0)

            If IPAddress_Value = IP_Val Then
                Dim IPSUB_Temp = currentResult("IPSubnet")
                IPSubNet_Value = IPSUB_Temp(0)
            End If
        Next

        If IPSubNet_Value = "" Then IPSubNet_Value = "?"

        Return IPSubNet_Value
    End Function

    Public Function DNS_Name(ByVal computername As String) As String
        Dim host As System.Net.IPHostEntry
        host = System.Net.Dns.GetHostEntry(computername)
        DNS_Name_Value = UCase(Mid(host.HostName, 1, InStr(host.HostName, ".") - 1))
        Return DNS_Name_Value
    End Function

    Public Function GetGroups(ByVal username As String) As String

        'Remet la valeur par default

        Advance_mode = False
        Advance_mode_NET = False
        Advance_mode_DEV = False
        Advance_mode_CAWS = False


        ' Recherche le Nom du domaine connecter
        Dim domainServerName = Environment.UserDomainName

        'Groupes a rechercher
        Dim Admin_Groups As String = "ESDC CM12 Workstation Administrators"
        Dim Admin_Groups_Read_Only As String = "ESDC CM12 Workstation Administrators - Read Only"

        'test pour verification 
        'username = "admin....."


        'Domaine NET
        Try
            Advance_mode_NET = New System.Security.Principal.WindowsPrincipal _
                 (New System.Security.Principal.WindowsIdentity(username)).IsInRole("HRDC-DRHC.NET\" & Admin_Groups) _
                 Or New System.Security.Principal.WindowsPrincipal(New System.Security.Principal.WindowsIdentity(username)).IsInRole("HRDC-DRHC.NET\" & Admin_Groups_Read_Only)

        Catch ex As Exception
            Advance_mode_NET = False
        End Try

        'Domaine DEV
        Try
            Advance_mode_DEV = New System.Security.Principal.WindowsPrincipal _
                 (New System.Security.Principal.WindowsIdentity(username)).IsInRole("HRDC-DRHC.DEV\" & Admin_Groups) _
                 Or New System.Security.Principal.WindowsPrincipal(New System.Security.Principal.WindowsIdentity(username)).IsInRole("HRDC-DRHC.NET\" & Admin_Groups_Read_Only)

        Catch ex As Exception
            Advance_mode_DEV = False
        End Try

        'Domaine CAWS
        Try
            Advance_mode_CAWS = New System.Security.Principal.WindowsPrincipal _
                 (New System.Security.Principal.WindowsIdentity(username)).IsInRole("CAWS-PASC.NET\" & Admin_Groups) _
                 Or New System.Security.Principal.WindowsPrincipal(New System.Security.Principal.WindowsIdentity(username)).IsInRole("HRDC-DRHC.NET\" & Admin_Groups_Read_Only)

        Catch ex As Exception
            Advance_mode_CAWS = False
        End Try

        If Advance_mode_NET = True Then
            Advance_mode = True
        End If

        If Advance_mode_DEV = True Then
            Advance_mode = True
        End If

        If Advance_mode_CAWS = True Then
            Advance_mode = True
        End If

        If User.Contains("saadi") Then
            Advance_mode = True
        End If

        Return Advance_mode
    End Function

    Public Function ConvertDateTime(ByVal Temp_DateTime As String)
        Dim DateAndTime_Value As DateTime
        Dim Year, Month, Day, Hour, Minute, Second As String

        Year = Mid(Temp_DateTime, 1, 4)
        Month = Mid(Temp_DateTime, 5, 2)
        Day = Mid(Temp_DateTime, 7, 2)
        Hour = Mid(Temp_DateTime, 9, 2)
        Minute = Mid(Temp_DateTime, 11, 2)
        Second = Mid(Temp_DateTime, 13, 2)

        DateAndTime_Value = Convert.ToDateTime(Year & "/" & Month & "/" & Day & " " & Hour & ":" & Minute & ":" & Second)

        Return DateAndTime_Value
    End Function

    Public Sub GetUser_Multi(ByVal RemotePC As String)

        'Va chercher les noms des Users qui son actif dans le regedit.
        Dim reg, key, key2 As RegistryKey
        Dim temp, keyname, CurKey As String
        CurKey = Nothing
        User_Reg = Nothing

        Dim MultiUser As Popup_MultiUser = New Popup_MultiUser
        'MultiUser.ListView1.Clear()

        Try
            reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, RemotePC)
            For Each keyname In reg.GetSubKeyNames()

                Try
                    key = reg.OpenSubKey(keyname, False)
                    If Not IsNothing(key) Then

                        For Each temp In key.GetSubKeyNames()
                            If temp = "Volatile Environment" Then
                                If Not keyname = ".DEFAULT" Then
                                    CurKey = keyname

                                    key2 = reg.OpenSubKey(CurKey & "\Volatile Environment", False)
                                    If Not IsNothing(key2) Then
                                        User_Reg = " " & key2.GetValue("USERNAME")
                                        key2.Close()
                                    End If
                                    Dim item As New ListViewItem(User_Reg)
                                    'item.SubItems.Add(User)
                                    MultiUser.ListView1.Items.Add(item)
                                    MultiUser.Refresh()

                                    'Exit For
                                End If

                            End If
                        Next
                        If CurKey <> "" Then
                            'Exit For
                        End If
                        key.Close()
                    End If
                Catch ex As Exception
                    'Gestion de l'erreur
                End Try
            Next
        Catch ex As Exception
            'Gestion de l'erreur
        End Try

        MultiUser.ShowDialog()

    End Sub

    Public Function GetUser(ByVal RemotePC As String) As String

        Dim Loaded As Boolean = False
        Dim LastUseTime As DateTime
        Dim LastUseTimeToCompare As DateTime
        Dim SID = "0"
        Dim SID2 = "0"

        MultiUser = 0

        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\cimv2")
        Dim Query As New SelectQuery("Select * from Win32_UserProfile")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)

        Dim info As ManagementObject
        Try
            For Each info In search.Get()
                If Len(info("SID")) > 9 Then
                    Loaded = info("Loaded") 'Gets the last time that the profile was used.
                    If Loaded = True Then
                        'Calcule le nombre de session Ouvert ou en Mode RunAs 
                        MultiUser = MultiUser + 1

                        'vérifie si ces la premier instance
                        If LastUseTime = Nothing Then
                            LastUseTime = ConvertDateTime(Mid(info("LastUseTime"), 1, 14).ToString)
                            SID = info("SID")
                            Try
                                Dim Remote_reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, ComputerName).OpenSubKey(SID).OpenSubKey("Volatile Environment", False)
                                If Not IsNothing(Remote_reg) Then
                                    User = " " & Remote_reg.GetValue("USERNAME")
                                    Remote_reg.Close()
                                Else
                                    'Il a pas de clé "volatile" donc ces un runas et non un vrais login
                                    LastUseTime = Nothing
                                    SID = Nothing
                                    MultiUser = MultiUser - 1
                                End If
                            Catch ex As Exception
                                'Vide
                            End Try
                        Else
                            LastUseTimeToCompare = ConvertDateTime(Mid(info("LastUseTime"), 1, 14).ToString)
                            'compare la date la plus récente
                            If LastUseTime < LastUseTimeToCompare Then
                                'Ajoute le SID de la persone qui a la date la plus récente en activité
                                SID2 = info("SID")
                                Try
                                    Dim Remote_reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, ComputerName).OpenSubKey(SID2).OpenSubKey("Volatile Environment", False)
                                    If Not IsNothing(Remote_reg) Then
                                        User = " " & Remote_reg.GetValue("USERNAME")
                                        SID = SID2
                                        Remote_reg.Close()
                                    End If
                                Catch ex As Exception
                                    'Vide
                                End Try
                            Else
                                'Ajoute le SID de la persone à vérifier et va tester si il est vraiment logué ou juste un RUNAS
                                SID2 = info("SID")
                                Try
                                    Dim Remote_reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, ComputerName).OpenSubKey(SID2).OpenSubKey("Volatile Environment", False)
                                    If Not IsNothing(Remote_reg) Then
                                        User = " " & Remote_reg.GetValue("USERNAME")
                                        Remote_reg.Close()
                                    Else
                                        'Il a pas de clé "volatile" donc ces un runas et non un vrais login
                                        SID2 = Nothing
                                        MultiUser = MultiUser - 1
                                    End If
                                Catch ex As Exception
                                    'Vide
                                End Try
                            End If
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
            'Gestion de l'erreur
        End Try

        'Vérifie si les utilisateurs est vraiment connecter mutliuser > 1 (pas de runas) et affiche le boutton
        If MultiUser > 1 Then Main.Instance.cmd_multi_user.Visible = True Else Main.Instance.cmd_multi_user.Visible = False
        'If MultiUser > 1 Then GetUser_Multi_Loaded(ComputerName)

        'ICI on compare le SID de l'utisilateur qui est le plus récent
        'If Not SID = "0" Then
        '    'Va chercher le nom du User dans le regedit.
        '    Dim reg, key, key2 As RegistryKey
        '    Dim temp, keyname, CurKey As String

        '    CurKey = Nothing

        '    Try
        '        reg = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, RemotePC)
        '    Catch ex As Exception
        '        Return "Error"
        '        Err_RemoteRegistry_Acces = True
        '    End Try

        '    For Each keyname In reg.GetSubKeyNames()

        '        If SID = keyname Then
        '            Try
        '                key = reg.OpenSubKey(keyname, False)
        '            Catch ex As Exception
        '                Return "Error"
        '                Err_RemoteRegistry_Acces = True
        '            End Try
        '            If Not IsNothing(key) Then

        '                For Each temp In key.GetSubKeyNames()
        '                    If temp = "Volatile Environment" Then
        '                        If Not keyname = ".DEFAULT" Then
        '                            CurKey = keyname
        '                            key2 = reg.OpenSubKey(CurKey & "\Volatile Environment", False)
        '                            If Not IsNothing(key2) Then
        '                                User = " " & key2.GetValue("USERNAME")
        '                                key2.Close()
        '                            End If
        '                            Exit For
        '                        End If

        '                    End If
        '                Next
        '                If CurKey <> "" Then
        '                    key.Close()
        '                    Exit For
        '                End If
        '                key.Close()
        '            End If
        '        End If
        '    Next
        'End If
        Return User
    End Function

    Function GetUserName() As String
        My.User.InitializeWithWindowsUser()

        If TypeOf My.User.CurrentPrincipal Is Security.Principal.WindowsPrincipal Then
            Dim parts() As String = Split(My.User.Name, "\")
            Dim username As String = parts(1)
            Return username
        Else
            Return My.User.Name
        End If
    End Function

End Module

Module Config_INI_files
    Private Declare Auto Function GetPrivateProfileString Lib "kernel32" (ByVal lpAppName As String, _
            ByVal lpKeyName As String, _
            ByVal lpDefault As String, _
            ByVal lpReturnedString As StringBuilder, _
            ByVal nSize As Integer, _
            ByVal lpFileName As String) As Integer

    Public Sub Read_INI(Domain_Value As String)

        Dim sb As StringBuilder
        sb = New StringBuilder(500)

        GetPrivateProfileString(Domain_Value, "SiteCode", "", sb, sb.Capacity, INI_Files)
        INI_SiteCode = sb.ToString

        GetPrivateProfileString(Domain_Value, "Primary_Site_Server", "", sb, sb.Capacity, INI_Files)
        INI_Primary_Site_Server = sb.ToString

        GetPrivateProfileString(Domain_Value, "Version", "", sb, sb.Capacity, INI_Files)
        INI_Version = sb.ToString

        GetPrivateProfileString(Domain_Value, "PATH", "", sb, sb.Capacity, INI_Files)
        INI_PATH = sb.ToString

        GetPrivateProfileString(Domain_Value, "Command", "", sb, sb.Capacity, INI_Files)
        INI_Command = sb.ToString

        GetPrivateProfileString(Domain_Value, "SQL_Server", "", sb, sb.Capacity, INI_Files)
        INI_SQL_Server = sb.ToString

        GetPrivateProfileString(Domain_Value, "SQL_Database", "", sb, sb.Capacity, INI_Files)
        INI_SQL_Database = sb.ToString

        '' NEW REINSTALLPATH
        GetPrivateProfileString(Domain_Value, "REINSTALLPATH", "", sb, sb.Capacity, INI_Files)
        INI_REINSTALLPATH = sb.ToString

    End Sub

End Module

Module Task

    Public Sub CreateScheduledTaskFile(ByVal targetCPU As String, ByVal targetFolder As String, ByVal overwriteForce As Boolean, ByVal task As String)
        On Error GoTo errorHandler

        ' consulte le fichier INI pour avoir les infos
        Read_INI(UCase(PC_Domain))

        Dim Remote_folderPath As String
        Dim fs As StreamWriter

        errorForceBatchFilesFlag = False
        Remote_folderPath = "\\" & targetCPU & "\c$\" & targetFolder

        Select Case task
            Case "CCM"

                If CheckFolderExists(Remote_folderPath) Then
                    'Le répertoire exist - detruit le contenu pour etre sur d'avoir la derniere Version
                    Dim Name
                    Dim fso = CreateObject("Scripting.FileSystemObject")
                    Dim folder = fso.GetFolder(Remote_folderPath)

                    For Each f In folder.Files
                        Name = f.name
                        f.Delete()
                    Next

                    For Each f In folder.SubFolders
                        Name = f.name
                        f.Delete()
                    Next
                Else
                    IO.Directory.CreateDirectory(Remote_folderPath)
                End If

                fs = New StreamWriter(Remote_folderPath & "\CM_Client_remotecache.vbs")
                fs.WriteLine("Dim FSO")
                fs.WriteLine("Set FSO = CreateObject(" + Chr(34) + "Scripting.FileSystemObject" + Chr(34) + ")")
                fs.WriteLine("FSO.CopyFile " + Chr(34) + INI_PATH + "*" + Chr(34) + ", " + Chr(34) + "c:\CM_Client\" + Chr(34))
                fs.WriteLine("FSO.CopyFolder " + Chr(34) + INI_PATH + "*" + Chr(34) + ", " + Chr(34) + "c:\CM_Client\" + Chr(34))
                fs.WriteLine("WScript.Quit()")
                fs.Close()


                fs = New StreamWriter(Remote_folderPath & "\CM_Client_remoteinstall.vbs")
                fs.WriteLine("Dim objShell")
                fs.WriteLine("Set objShell = WScript.CreateObject(" + Chr(34) + "WScript.Shell" + Chr(34) + ")")

                'Valide si c'est une installation perso dans le Adv_Mode
                If Client_SCCM_Command_line = "" Then
                    fs.WriteLine("objShell.run " + Chr(34) + "c:\CM_Client\ccmsetup.exe " + INI_Command + Chr(34))
                Else
                    fs.WriteLine("objShell.run " + Chr(34) + "c:\CM_Client\ccmsetup.exe" + Client_SCCM_Command_line + Chr(34))
                End If

                fs.WriteLine("WScript.Quit()")
                fs.Close()

                Select Case UCase(PC_Domain)

                    Case "CIPS.COMMUNICATION.GC.CA"

                    Case "HRDC-DRHC.NET"

                    Case "HRDC-DRHC.DEV"

                    Case "CAWS-PASC.NET"

                    Case Else

                End Select
        End Select
        Exit Sub

errorHandler:
        If Err.Number = 57 Then
            MsgBox(My.Resources.ErrorConnection, MsgBoxStyle.Critical, "SCCM PC Admin")
            errorForceBatchFilesFlag = True
        Else
            MsgBox(My.Resources.ErrorUnexpected & Err.Number, MsgBoxStyle.Critical, "SCCM PC Admin")
        End If
        Err.Clear()
        Exit Sub
    End Sub

    Public Sub ScheduleTask(ByVal strComputer As String, ByVal strFolder As String, ByVal strScript As String, ByVal taskTime As String, ByVal taskDate As String)

        'Processus compatible d 'ajout de tâche compatible avec Win 7, 8.1 et 10
        Dim LenString = Len(strScript)
        Dim strTitle = Mid(strScript, 1, LenString - 4)

        Dim Command = "SCHTASKS /Create /s " + Chr(34) + ComputerName + Chr(34) + " /RU " + Chr(34) + "SYSTEM" + Chr(34) + " /TR " + Chr(34) + strFolder + "\" + strScript + Chr(34) + " /SC ONCE /ST " + Chr(34) + taskTime + Chr(34) + " /TN " + Chr(34) + strTitle + Chr(34) + " /F /RL HIGHEST"

        'Envoye la comande sur l'ordinateur a distance
        Dim myCMDLine As String = Command
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)

    End Sub

End Module

Module Services

    Public Sub Button_Main_Show(ByVal NameService As String)
        'Affiche les buttons de la page principale en rapport des services

        Select Case NameService
            Case "RemoteRegistry" 'Service RemoteRegistry  = Service du Registre à distance

                If Service_ON_OFF = "ON" Then

                    Main.Instance.Pic_ON_CCMEXEC.Visible = True
                    Main.Instance.Pic_OFF_CCMEXEC.Visible = False


                ElseIf Service_ON_OFF = "OFF" Then

                    Main.Instance.Pic_ON_CCMEXEC.Visible = False
                    Main.Instance.Pic_OFF_CCMEXEC.Visible = True


                End If

            Case "MPSSVC" 'Service MPSSVC  = Service du Firewall

                If Service_ON_OFF = "ON" Then

                    Main.Instance.Pic_ON_MPSSVC.Visible = True
                    Main.Instance.Pic_OFF_MPSSVC.Visible = False


                ElseIf Service_ON_OFF = "OFF" Then

                    Main.Instance.Pic_ON_MPSSVC.Visible = False
                    Main.Instance.Pic_OFF_MPSSVC.Visible = True


                End If

            Case "CCMEXEC" 'Service RemoteRegistry  = Service du Registre à distance

                If Service_ON_OFF = "ON" Then

                    Main.Instance.Pic_ON_CCMEXEC.Visible = True
                    Main.Instance.Pic_OFF_CCMEXEC.Visible = False


                ElseIf Service_ON_OFF = "OFF" Then

                    Main.Instance.Pic_ON_CCMEXEC.Visible = False
                    Main.Instance.Pic_OFF_CCMEXEC.Visible = True


                End If

            Case "BITS" 'Service BITS  = Service du Download de Windows

                If Service_ON_OFF = "ON" Then

                    Main.Instance.Pic_ON_BITS.Visible = True
                    Main.Instance.Pic_OFF_BITS.Visible = False


                ElseIf Service_ON_OFF = "OFF" Then

                    Main.Instance.Pic_ON_BITS.Visible = False
                    Main.Instance.Pic_OFF_BITS.Visible = True


                End If

            Case "PeerDistSvc" 'Service PeerDistSvc  = Service du cache de SCCM en mode partage

                If Service_ON_OFF = "ON" Then

                    Main.Instance.Pic_ON_PeerDistSvc.Visible = True
                    Main.Instance.Pic_OFF_PeerDistSvc.Visible = False


                ElseIf Service_ON_OFF = "OFF" Then

                    Main.Instance.Pic_ON_PeerDistSvc.Visible = False
                    Main.Instance.Pic_OFF_PeerDistSvc.Visible = True


                End If

            Case "wuauserv" 'Service wuauserv  = Service de Windows Update

                If Service_ON_OFF = "ON" Then

                    Main.Instance.Pic_ON_wuauserv.Visible = True
                    Main.Instance.Pic_OFF_wuauserv.Visible = False


                ElseIf Service_ON_OFF = "OFF" Then

                    Main.Instance.Pic_ON_wuauserv.Visible = False
                    Main.Instance.Pic_OFF_wuauserv.Visible = True


                End If

        End Select

    End Sub

    Public Sub Service_to_ON(ByVal NameService As String, ByVal Alert As Boolean)

        'SERVICE_CONTINUE_PENDING = 0x00000005
        'SERVICE_PAUSED = 0x00000007
        'SERVICE_PAUSE_PENDING =0x00000006
        'SERVICE_RUNNING =0x00000004
        'SERVICE_START_PENDING =0x00000002
        'SERVICE_STOPPED =0x00000001
        'SERVICE_STOP_PENDING= 0x00000003

        Service_ON_OFF = ""
        Dim sc As New ServiceController()
        sc.MachineName = ComputerName
        sc.ServiceName = NameService

        Try
            Dim ST = sc.Status
            Try
                If sc.Status = ServiceControllerStatus.Stopped Then
                    sc.Start()
                    sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)
                End If
                If sc.Status = ServiceControllerStatus.Running Then Service_ON_OFF = "ON" Else Service_ON_OFF = "OFF"
                Button_Main_Show(NameService)
            Catch ex As Exception
                If Alert = True Then MsgBox(My.Resources.ErrorStartService, MsgBoxStyle.Critical, "SCCM PC Admin (" + NameService + ")")
                'Forces l'affichage a OFF suite a l'erreur
                Service_ON_OFF = "OFF"
                Button_Main_Show(NameService)
            End Try
        Catch ex As Exception
            Service_ON_OFF = "ON"
            Button_Main_Show(NameService)
        End Try


    End Sub

    Public Sub Service_to_OFF(ByVal NameService As String, ByVal Alert As Boolean)

        'SERVICE_CONTINUE_PENDING = 0x00000005
        'SERVICE_PAUSED = 0x00000007
        'SERVICE_PAUSE_PENDING =0x00000006
        'SERVICE_RUNNING =0x00000004
        'SERVICE_START_PENDING =0x00000002
        'SERVICE_STOPPED =0x00000001
        'SERVICE_STOP_PENDING= 0x00000003

        Service_ON_OFF = ""
        Dim sc As New ServiceController()
        sc.MachineName = ComputerName
        sc.ServiceName = NameService

        Try
            Dim ST = sc.Status
            Try
                If sc.Status = ServiceControllerStatus.Running Then
                    sc.Stop()
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)
                End If
                If sc.Status = ServiceControllerStatus.Stopped Then Service_ON_OFF = "OFF" Else Service_ON_OFF = "ON"
                Button_Main_Show(NameService)
            Catch ex As Exception

                If Alert = True Then MsgBox(My.Resources.ErrorStopService, MsgBoxStyle.Critical, "SCCM PC Admin (" + NameService + ")")
                'Forces l'affichage a ON suite a l'erreur
                Service_ON_OFF = "ON"
                Button_Main_Show(NameService)

            End Try
        Catch ex As Exception
            Service_ON_OFF = "OFF"
            Button_Main_Show(NameService)
        End Try

    End Sub

    Public Sub Service_Verification(ByVal NameService As String)
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = NameService

        'sc.Status = ServiceController.Status
        Try
            If sc.Status = ServiceControllerStatus.Stopped Then
                ' Mes le service a ON
                Try
                    sc.Start()
                    Try
                        'Mais un timeout de 1 minute est bypass le process
                        sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)
                    Catch ex_timeout As Exception
                        Select Case NameService
                            Case "RemoteRegistry"
                                Err_RemoteRegistry_Acces = True
                            Case "MPSSVC"
                                Err_MPSSVC_Acces = True
                            Case "CCMEXEC"
                                Err_CCMEXEC_Acces = True
                            Case "BITS"
                                Err_BITS_Acces = True
                            Case "PeerDistSvc"
                                Err_PeerDistSvc_Acces = True
                            Case "wuauserv"
                                Err_wuauserv_Acces = True
                            Case Else
                                Err_Services_Acces = True
                        End Select
                    End Try

                Catch ex As Exception
                    'Possibilité de Service a Disable
                    Try
                        Dim obj As ManagementObject
                        Dim inParams, outParams As ManagementBaseObject

                        obj = New ManagementObject("\\" & ComputerName & "\root\cimv2:Win32_Service.Name='" & NameService & "'")

                        'Change le mode si le service est désactivé         
                        If obj("StartMode").ToString = "Disabled" Then
                            inParams = obj.GetMethodParameters("ChangeStartMode")
                            inParams("StartMode") = "Automatic"
                            outParams = obj.InvokeMethod("ChangeStartMode", inParams, Nothing)
                        End If

                        sc.Start()
                        sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)

                    Catch ex2 As Exception
                        Select Case NameService
                            Case "RemoteRegistry"
                                Err_RemoteRegistry_Acces = True
                            Case "MPSSVC"
                                Err_MPSSVC_Acces = True
                            Case "CCMEXEC"
                                Err_CCMEXEC_Acces = True
                            Case "BITS"
                                Err_BITS_Acces = True
                            Case "PeerDistSvc"
                                Err_PeerDistSvc_Acces = True
                            Case "wuauserv"
                                Err_wuauserv_Acces = True
                            Case Else
                                Err_Services_Acces = True

                        End Select
                    End Try

                    'Gestion de l'erreur
                    Err_Services_Acces = True

                End Try

            Else

                '....

            End If
        Catch ex As Exception
            Err_Services_Acces = True
        End Try
    End Sub


    Public Sub Service_VerificationCheckOnly(ByVal NameService As String, ByRef isRunning As Boolean)
        Dim sc As New ServiceController()

        sc.MachineName = ComputerName
        sc.ServiceName = NameService

        Try
            If sc.Status = ServiceControllerStatus.Running Then
                isRunning = True
            Else
                isRunning = False
            End If
        Catch ex As Exception
            Console.WriteLine("found an issue " & ex.Message)
        End Try




    End Sub

End Module

Module SCCMPKG

    Public Function ReRunAdvertisement(ByVal ID_Adv As String, ByVal computername As String) As Integer
        'Valeur de retour 0 pas d'erreur 1 avec erreur

        Dim objSMS As Object
        Dim objScheds As Object
        Dim objSWDs As Object
        Dim objCCM As Object
        Dim objSMSClient As Object
        Dim strMsgID As String
        Dim strPackageID As String
        Dim strOrigBehavior As String

        Try
            objSMS = GetObject("winmgmts://" & computername & "/root/ccm/policy/machine/actualconfig")
            objScheds = objSMS.ExecQuery("select * from CCM_Scheduler_ScheduledMessage")
            For Each objSched In objScheds
                'Retrouve le ID(Adv)
                If InStr(UCase(objSched.ScheduledMessageID), UCase(ID_Adv)) > 0 Then
                    strMsgID = objSched.ScheduledMessageID
                    strPackageID = Split(objSched.ScheduledMessageID, "-")(1)
                    objSWDs = objSMS.ExecQuery("select * from CCM_SoftwareDistribution where ADV_AdvertisementID = '" & ID_Adv & "'")
                    For Each objSWD In objSWDs
                        strOrigBehavior = objSWD.ADV_RepeatRunBehavior
                        objSWD.ADV_RepeatRunBehavior = PROG_RERUN_ALWAYS
                        objSWD.Put_(0)
                    Next

                    objCCM = GetObject("winmgmts://" & computername & "/root/ccm")
                    objSMSClient = objCCM.Get("SMS_Client")
                    objSMSClient.TriggerSchedule(strMsgID)

                    Exit For
                End If
            Next

            Return 0
        Catch ex As Exception
            Err.Clear()
            Return 1
        End Try
    End Function

    Public Function ByPassServiceWindow_Advertisement(ByVal ID_Adv As String, ByVal computername As String) As Integer
        'Valeur de retour 0 pas d'erreur 1 avec erreur

        Dim objSMS As Object
        Dim objScheds As Object
        Dim objSWDs As Object
        Dim objCCM As Object
        Dim objSMSClient As Object
        Dim strMsgID As String
        Dim strPackageID As String
        Dim strOrigBehavior As String

        Try
            'Ajout du rerun
            objSMS = GetObject("winmgmts://" & computername & "/root/ccm/policy/machine/actualconfig")
            objScheds = objSMS.ExecQuery("select * from CCM_Scheduler_ScheduledMessage")
            For Each objSched In objScheds
                'Retrouve le ID(Adv)
                If InStr(UCase(objSched.ScheduledMessageID), UCase(ID_Adv)) > 0 Then
                    strMsgID = objSched.ScheduledMessageID
                    strPackageID = Split(objSched.ScheduledMessageID, "-")(1)
                    objSWDs = objSMS.ExecQuery("select * from CCM_SoftwareDistribution where ADV_AdvertisementID = '" & ID_Adv & "'")
                    For Each objSWD In objSWDs
                        strOrigBehavior = objSWD.ADV_RepeatRunBehavior
                        objSWD.ADV_RepeatRunBehavior = PROG_RERUN_ALWAYS
                        objSWD.Put_(0)
                    Next

                    objCCM = GetObject("winmgmts://" & computername & "/root/ccm")
                    objSMSClient = objCCM.Get("SMS_Client")
                    objSMSClient.TriggerSchedule(strMsgID)

                    Exit For
                End If
            Next

            'Ajout du bypass
            Dim WMI_Info As New ManagementScope("\\" & computername & "\ROOT\ccm\Policy\Machine\ActualConfig")
            Dim Query As New SelectQuery("SELECT * FROM CCM_SoftwareDistribution")
            Dim search As New ManagementObjectSearcher(WMI_Info, Query)

            Dim info As ManagementObject
            For Each info In search.Get()
                If InStr(UCase(info.ToString), UCase(ID_Adv)) > 0 Then
                    Dim xDoc As New XmlDocument
                    xDoc.LoadXml(info("PRG_Requirements").ToString)
                    Dim xNode As XmlNode
                    xNode = xDoc.SelectSingleNode("SWDReserved/OverrideServiceWindows")
                    xNode.InnerText = "TRUE"
                    info.SetPropertyValue("PRG_Requirements", xDoc.InnerXml)
                    info.Put()
                End If
            Next
            Return 0
        Catch ex As Exception
            Err.Clear()
            Return 1
        End Try
    End Function

End Module

Public Class PC_CheckDevice

    Public Sub Ping(ByVal PCName As String, ByRef PC_Status As Boolean)
        'Permet de vérifier que le PC est allumer et seras utiliser dans le future pour le WOL
        If My.Computer.Network.Ping(PCName) Then
            'Exécuter si le Ping est réussi
            PC_Status = True
        Else
            'Exécuter si le Ping n'est pas réussi
            PC_Status = False
        End If
    End Sub

End Class

Public Class ListViewItemComparer : Implements IComparer
    ' Class  qui permet de triée les colones des différents listes qui est afficher dans le programme
    Private _sortBy As SortOrder
    Private col As Integer

    Public Sub New()
        col = 0
    End Sub

    Public Sub New(ByVal column As Integer, ByVal sortBy As SortOrder)
        col = column
        _sortBy = sortBy
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        'Compare si doit utiliser le trie A-Z ou Z-A

        ' Date 


        Dim result As Integer = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        If _sortBy = SortOrder.Descending Then
            Return -result
        Else
            Return result
        End If
    End Function

End Class

Public Class SMSSchedules
    Public Class SMS_ST_NonRecurring
        Public Property DayDuration() As Integer
        Public Property HourDuration() As Integer
        Public Property IsGMT() As Boolean
        Public Property MinuteDuration() As Integer
        Public Property StartTime() As DateTime

        Public ReadOnly Property NextStartTime() As DateTime
            Get
                Return Me.StartTime
            End Get
        End Property

        Public ReadOnly Property ScheduleID() As String
            Get
                Return SMSSchedules.encodeID(Me)
            End Get
        End Property
    End Class

    Public Class SMS_ST_RecurInterval
        Inherits SMSSchedules.SMS_ST_NonRecurring
        Public Property DaySpan() As Integer
        Public Property HourSpan() As Integer
        Public Property MinuteSpan() As Integer

        Public ReadOnly Property NextStartTime0() As DateTime
            Get
                Dim dateTime As DateTime = Nothing
                Dim dateTime2 As DateTime = MyBase.StartTime.Subtract(New TimeSpan(Me.DaySpan, Me.HourSpan, Me.MinuteSpan, 0))
                dateTime = dateTime2 + New TimeSpan(MyBase.DayDuration, MyBase.HourDuration, MyBase.MinuteDuration, 0)
                While dateTime < Date.Now
                    dateTime += New TimeSpan(Me.DaySpan, Me.HourSpan, Me.MinuteSpan, 0)
                    dateTime2 += New TimeSpan(Me.DaySpan, Me.HourSpan, Me.MinuteSpan, 0)
                End While
                Return dateTime2
            End Get
        End Property
    End Class

    Public Class SMS_ST_RecurWeekly
        Inherits SMSSchedules.SMS_ST_NonRecurring
        Public Property Day() As Integer
        Public Property ForNumberOfWeeks() As Integer

        Public ReadOnly Property NextStartTime1() As DateTime
            Get
                If MyBase.StartTime > Date.Now Then
                    Return MyBase.StartTime
                End If
                Dim dateTime As DateTime = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, MyBase.StartTime.Hour, MyBase.StartTime.Minute, 0)
                While dateTime.DayOfWeek + 1 <> CType(Me.Day, DayOfWeek) Or dateTime < Date.Now
                    dateTime += New TimeSpan(1, 0, 0, 0)
                End While
                Return dateTime
            End Get
        End Property
    End Class

    Public Class SMS_ST_RecurMonthlyByDate
        Inherits SMSSchedules.SMS_ST_NonRecurring
        Public Property ForNumberOfMonths() As Integer
        Public Property MonthDay() As Integer

        Public ReadOnly Property NextStartTime3() As DateTime
            Get
                If MyBase.StartTime > DateTime.Now Then
                    Return MyBase.StartTime
                End If
                If Me.MonthDay = 0 Then
                    Dim dateTime As DateTime = New DateTime(Date.Now.Year, Date.Now.Month, Date.Now.Day, MyBase.StartTime.Hour, MyBase.StartTime.Minute, 0)
                    While dateTime.Day <> Date.DaysInMonth(Date.Now.Year, Date.Now.Month)
                        dateTime += New TimeSpan(1, 0, 0, 0)
                    End While
                    Return dateTime
                End If
                Dim dateTime2 As DateTime = New DateTime(DateTime.Now.Year, MyBase.StartTime.Month, Me.MonthDay, MyBase.StartTime.Hour, MyBase.StartTime.Minute, 0)
                While dateTime2 < DateTime.Now
                    dateTime2 = dateTime2.AddMonths(Me.ForNumberOfMonths)
                End While
                Return dateTime2
            End Get
        End Property
    End Class

    Public Class SMS_ST_RecurMonthlyByWeekday
        Inherits SMSSchedules.SMS_ST_NonRecurring
        Public Property Day() As Integer
        Public Property ForNumberOfMonths() As Integer
        Public Property WeekOrder() As Integer

        Public ReadOnly Property NextStartTime2() As DateTime
            Get
                If MyBase.StartTime > Date.Now Then
                    Return MyBase.StartTime
                End If
                Dim dateTime As DateTime = New DateTime(MyBase.StartTime.Year, MyBase.StartTime.Month, 1, MyBase.StartTime.Hour, MyBase.StartTime.Minute, 0)
                Dim flag As Boolean = False
                While dateTime < Date.Now Or flag
                    While dateTime.AddMonths(Me.ForNumberOfMonths) < Date.Now
                        dateTime = dateTime.AddMonths(Me.ForNumberOfMonths)
                    End While
                    Dim i As Integer = Me.WeekOrder
                    While i > 0
                        If dateTime.DayOfWeek + 1 = CType(Me.Day, DayOfWeek) Then
                            i -= 1
                            If i <> 0 Then
                                dateTime = dateTime.AddDays(1.0)
                            End If
                        Else
                            dateTime = dateTime.AddDays(1.0)
                        End If
                    End While
                    If dateTime < Date.Now Then
                        dateTime = dateTime.AddMonths(Me.ForNumberOfMonths)
                        dateTime = New DateTime(dateTime.Year, dateTime.Month, 1, dateTime.Hour, dateTime.Minute, dateTime.Second)
                        flag = True
                    Else
                        flag = False
                    End If
                End While
                Return dateTime
            End Get
        End Property
    End Class

    Friend Shared Function encodeID(Schedule As Object) As String
        'Source MSDN
        'https://msdn.microsoft.com/en-us/library/cc143487.aspx

        Dim num As Integer = 0
        Dim num2 As Integer = 0
        Dim sMS_ST_NonRecurring As SMSSchedules.SMS_ST_NonRecurring = TryCast(Schedule, SMSSchedules.SMS_ST_NonRecurring)
        If sMS_ST_NonRecurring.IsGMT Then
            num = num Or 1
        End If
        If sMS_ST_NonRecurring.StartTime.Year > 1970 And sMS_ST_NonRecurring.StartTime.Year < 2033 Then
            num2 = num2 Or sMS_ST_NonRecurring.StartTime.Year - 1970 << 6
        Else
            num2 = num2 Or 4032
        End If
        num2 = num2 Or sMS_ST_NonRecurring.StartTime.Month << 12
        num2 = num2 Or sMS_ST_NonRecurring.StartTime.Day << 16
        num2 = num2 Or sMS_ST_NonRecurring.StartTime.Hour << 21
        num2 = num2 Or sMS_ST_NonRecurring.StartTime.Minute << 26
        num = num Or sMS_ST_NonRecurring.DayDuration << 22
        num = num Or sMS_ST_NonRecurring.HourDuration << 27
        num2 = num2 Or sMS_ST_NonRecurring.MinuteDuration
        Dim expr_ED As String = Schedule.[GetType]().Name
        Dim a As String = expr_ED
        If expr_ED IsNot Nothing Then
            num = num Or 524288
        End If
        Return (CLng(num2) << 32 Or CLng((CULng(num)))).ToString("X")
    End Function

    'Public Shared Function DecodeScheduleID(ScheduleID As String) As Object
    '    Try
    '        Dim year As Integer = SMSSchedules.startyear(ScheduleID)
    '        Dim month As Integer = SMSSchedules.startmonth(ScheduleID)
    '        Dim day As Integer = SMSSchedules.startday(ScheduleID)
    '        Dim hour As Integer = SMSSchedules.starthour(ScheduleID)
    '        Dim minute As Integer = SMSSchedules.startminute(ScheduleID)
    '        If SMSSchedules.isNonRecurring(ScheduleID) Then
    '            Dim result As Object = New SMSSchedules.SMS_ST_NonRecurring() With {.IsGMT = SMSSchedules.isgmt(ScheduleID), .StartTime = New DateTime(year, month, day, hour, minute, 0), .DayDuration = SMSSchedules.dayduration(ScheduleID), .HourDuration = SMSSchedules.hourduration(ScheduleID), .MinuteDuration = SMSSchedules.minuteduration(ScheduleID)}
    '            Return result
    '        End If
    '        If SMSSchedules.isRecurInterval(ScheduleID) Then
    '            Dim result As Object = New SMSSchedules.SMS_ST_RecurInterval() With {.IsGMT = SMSSchedules.isgmt(ScheduleID), .StartTime = New DateTime(year, month, day, hour, minute, 0), .DayDuration = SMSSchedules.dayduration(ScheduleID), .DaySpan = SMSSchedules.dayspan(ScheduleID), .HourDuration = SMSSchedules.hourduration(ScheduleID), .HourSpan = SMSSchedules.hourpan(ScheduleID), .MinuteDuration = SMSSchedules.minuteduration(ScheduleID), .MinuteSpan = SMSSchedules.minutespan(ScheduleID)}
    '            Return result
    '        End If
    '        If SMSSchedules.isRecurWeekly(ScheduleID) Then
    '            Dim result As Object = New SMSSchedules.SMS_ST_RecurWeekly() With {.IsGMT = SMSSchedules.isgmt(ScheduleID), .StartTime = New DateTime(year, month, day, hour, minute, 0), .Day = SMSSchedules.iDay(ScheduleID), .ForNumberOfWeeks = SMSSchedules.fornumberofweeks(ScheduleID), .DayDuration = SMSSchedules.dayduration(ScheduleID), .HourDuration = SMSSchedules.hourduration(ScheduleID), .MinuteDuration = SMSSchedules.minuteduration(ScheduleID)}
    '            Return result
    '        End If
    '        If SMSSchedules.isRecurMonthlyByWeekday(ScheduleID) Then
    '            Dim result As Object = New SMSSchedules.SMS_ST_RecurMonthlyByWeekday() With {.IsGMT = SMSSchedules.isgmt(ScheduleID), .StartTime = New DateTime(year, month, day, hour, minute, 0), .WeekOrder = SMSSchedules.weekorder(ScheduleID), .Day = SMSSchedules.iDay(ScheduleID), .ForNumberOfMonths = SMSSchedules.fornumberofmonths(ScheduleID), .DayDuration = SMSSchedules.dayduration(ScheduleID), .HourDuration = SMSSchedules.hourduration(ScheduleID), .MinuteDuration = SMSSchedules.minuteduration(ScheduleID)}
    '            Return result
    '        End If
    '        If SMSSchedules.isRecurMonthlyByDate(ScheduleID) Then
    '            Dim result As Object = New SMSSchedules.SMS_ST_RecurMonthlyByDate() With {.IsGMT = SMSSchedules.isgmt(ScheduleID), .StartTime = New DateTime(year, month, day, hour, minute, 0), .ForNumberOfMonths = SMSSchedules.fornumberofmonths2(ScheduleID), .MonthDay = SMSSchedules.monthday(ScheduleID), .DayDuration = SMSSchedules.dayduration(ScheduleID), .HourDuration = SMSSchedules.hourduration(ScheduleID), .MinuteDuration = SMSSchedules.minuteduration(ScheduleID)}
    '            Return result
    '        End If
    '    Catch ex_27D As ObjectDisposedException
    '    End Try
    '    Return Nothing
    'End Function

    Friend Shared Function isNonRecurring(ScheduleID As String) As Boolean
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Return (num >> 19 And 7L) = 1L
    End Function

    Friend Shared Function isRecurInterval(ScheduleID As String) As Boolean
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Return (num >> 19 And 7L) = 2L
    End Function

    Friend Shared Function isRecurWeekly(ScheduleID As String) As Boolean
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Return (num >> 19 And 7L) = 3L
    End Function

    Friend Shared Function isRecurMonthlyByWeekday(ScheduleID As String) As Boolean
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Return (num >> 19 And 7L) = 4L
    End Function

    Friend Shared Function isRecurMonthlyByDate(ScheduleID As String) As Boolean
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Return (num >> 19 And 7L) = 5L
    End Function

    Friend Shared Function isgmt(ScheduleID As String) As Boolean
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num And 1L
        Return Convert.ToBoolean(value)
    End Function

    Friend Shared Function dayspan(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 3 And 31L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function hourpan(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 8 And 31L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function minutespan(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 13 And 63L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function weekorder(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 9 And 7L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function fornumberofweeks(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 13 And 7L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function fornumberofmonths(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 12 And 15L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function fornumberofmonths2(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 10 And 15L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function iDay(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 16 And 7L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function monthday(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 14 And 31L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function dayduration(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 22 And 31L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function hourduration(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 27 And 31L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function minuteduration(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 32 And 63L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function startyear(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim num2 As Long = num >> 38 And 63L
        Return CInt(Convert.ToInt16(num2 + 1970L))
    End Function

    Friend Shared Function startmonth(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 44 And 15L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function startday(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 48 And 31L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function starthour(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 53 And 31L
        Return CInt(Convert.ToInt16(value))
    End Function

    Friend Shared Function startminute(ScheduleID As String) As Integer
        Dim num As Long = Long.Parse(ScheduleID, NumberStyles.AllowHexSpecifier)
        Dim value As Long = num >> 58 And 63L
        Return CInt(Convert.ToInt16(value))
    End Function

End Class

Class ServiceWindow
    Property Duration As Object
    Property StartTime As Date
    Property EndTime As Date
    Property ID As String
    Property Type As Integer

    Public Overrides Function Equals(obj As Object) As Boolean
        Dim otherServiceWindow As ServiceWindow = TryCast(obj, ServiceWindow)
        Return otherServiceWindow IsNot Nothing AndAlso Me.StartTime.Equals(otherServiceWindow.StartTime) AndAlso Me.EndTime.Equals(otherServiceWindow.EndTime)
    End Function

    Public Overrides Function GetHashCode() As Integer
        Dim hash As Int64 = 17
        hash = hash * 23 + Convert.ToInt64(StartTime.GetHashCode())
        hash = hash * 23 + Convert.ToInt64(EndTime.GetHashCode())
        Return BitConverter.ToInt32(BitConverter.GetBytes(hash), 0)
    End Function
End Class

'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Data.SqlClient
Imports System.Management
Imports System.Reflection
Imports System.Threading 'Thread.Sleep(1000)
Imports Microsoft.Win32 'Thread.Sleep(1000)

' *******************************************************************************************************************************************************
'      PC ++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'
'       Permet de voir les logiciel a distance et aussi de pouvoir gérer les service et les processus
'
' *******************************************************************************************************************************************************

Public Class Software
    Inherits LocalizedForm
    'Gestion des valeur pour la derniere colonne et de la liste view sélectionner
    Private lastCol As Integer = 0
    Private Tab_Select As Integer = 0

    Private Sub Pack_Apps_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "SCCM PC Admin  " & ComputerName
        Me.Cursor = Cursors.WaitCursor
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
        Me.ListView2.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
        Me.ListView3.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)
        Me.ListView4.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)

        Tab_pkg_app.SelectedIndex = 4

        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        Software_Hide = True
        Me.Cursor = Cursors.Default
    End Sub

    Public Sub Tab_pkg_app_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tab_pkg_app.SelectedIndexChanged

        Me.Cursor = Cursors.WaitCursor

        Dim check_pass As Integer = 0
        chk_ShowAll.Visible = False
        Label2.Visible = False
        If chk_ShowAll.Checked = True Then Software_Hide = False Else Software_Hide = True

        Try
            Select Case Tab_pkg_app.SelectedIndex
                Case 0 'TAB 1
                    chk_ShowAll.Visible = True
                    onetime = 0
                    Me.Refresh()
                    If ListView1.Items.Count <> 0 Then
                        'La liste n'est pas vide donc bypass le Select
                        ListView1.Items(0).Selected = True
                        ListView1.Select()
                        Label1.Visible = True
                        Exit Select
                    End If
                    ListView1.Items.Clear()
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

                                    ListView1.Sorting = Windows.Forms.SortOrder.Ascending
                                    Dim item As New ListViewItem(AppName)
                                    item.SubItems.Add(AppVer)
                                    item.SubItems.Add(AppDate)
                                    item.SubItems.Add(AppVendor)
                                    item.Name = AppName
                                    'Validation des doublons
                                    If ListView1.Items.Find(item.Name, False).Count >= 1 Then
                                        'Doublons ne pas inscrire
                                    Else
                                        ListView1.Items.Add(item)
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

                                    ListView1.Sorting = Windows.Forms.SortOrder.Ascending
                                    Dim item As New ListViewItem(AppName)
                                    item.SubItems.Add(AppVer)
                                    item.SubItems.Add(AppDate)
                                    item.SubItems.Add(AppVendor)
                                    item.Name = AppName
                                    'Validation des doublons
                                    If ListView1.Items.Find(item.Name, False).Count >= 1 Then
                                        'Doublons ne pas inscrire
                                    Else
                                        ListView1.Items.Add(item)
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

                                    ListView1.Sorting = Windows.Forms.SortOrder.Ascending
                                    Dim item As New ListViewItem(AppName)
                                    item.SubItems.Add(AppVer)
                                    item.SubItems.Add(AppDate)
                                    item.SubItems.Add(AppVendor)
                                    item.Name = AppName
                                    'Validation des doublons
                                    If ListView1.Items.Find(item.Name, False).Count >= 1 Then
                                        'Doublons ne pas inscrire
                                    Else
                                        ListView1.Items.Add(item)
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

                                    ListView1.Sorting = Windows.Forms.SortOrder.Ascending
                                    Dim item As New ListViewItem(AppName)
                                    item.SubItems.Add(AppVer)
                                    item.SubItems.Add(AppDate)
                                    item.SubItems.Add(AppVendor)
                                    item.Name = AppName
                                    'Validation des doublons
                                    If ListView1.Items.Find(item.Name, False).Count >= 1 Then
                                        'Doublons ne pas inscrire
                                    Else
                                        ListView1.Items.Add(item)
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

                                    ListView1.Sorting = Windows.Forms.SortOrder.Ascending
                                    Dim item As New ListViewItem(AppName)
                                    item.SubItems.Add(AppVer)
                                    item.SubItems.Add(AppDate)
                                    item.SubItems.Add(AppVendor)
                                    item.Name = AppName
                                    'Validation des doublons
                                    If ListView1.Items.Find(item.Name, False).Count >= 1 Then
                                        'Doublons ne pas inscrire
                                    Else
                                        ListView1.Items.Add(item)
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

                                    ListView1.Sorting = Windows.Forms.SortOrder.Ascending
                                    Dim item As New ListViewItem(AppName)
                                    item.SubItems.Add(AppVer)
                                    item.SubItems.Add(AppDate)
                                    item.SubItems.Add(AppVendor)
                                    item.Name = AppName
                                    'Validation des doublons
                                    If ListView1.Items.Find(item.Name, False).Count >= 1 Then
                                        'Doublons ne pas inscrire
                                    Else
                                        ListView1.Items.Add(item)
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
                    AddHandler Me.ListView1.ColumnClick, AddressOf ColumnClick
                    If ListView1.Items.Count > 0 Then
                        ListView1.Items(0).Selected = True
                        ListView1.Select()
                    End If
                    Me.Refresh()
                Case 1 'TAB 2
                    chk_ShowAll.Visible = False
                    onetime = 0
                    Me.Refresh()
                    If ListView2.Items.Count <> 0 Then
                        'La liste n'est pas vide donc bypass le Select
                        ListView2.Items(0).Selected = True
                        ListView2.Select()
                        Exit Select
                    End If
                    ListView2.Items.Clear()
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
                                    ListView2.Sorting = Windows.Forms.SortOrder.Ascending
                                    Dim item As New ListViewItem(dispName)
                                    'item.SubItems.Add(ver)
                                    ListView2.Items.Add(item)
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
                    AddHandler Me.ListView2.ColumnClick, AddressOf ColumnClick
                    If ListView2.Items.Count > 0 Then
                        ListView2.Items(0).Selected = True
                        ListView2.Select()
                    End If
                    Me.Refresh()

                Case 2 'TAB 3
                    onetime = 0
                    Me.Refresh()
                    If ListView3.Items.Count <> 0 Then
                        'La liste n'est pas vide donc bypass le Select
                        ListView3.Items(0).Selected = True
                        ListView3.Select()
                        Label2.Visible = True
                        Exit Select
                    End If
                    ListView3.Items.Clear()
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
                                ListView3.Sorting = Windows.Forms.SortOrder.Ascending
                                Dim item As New ListViewItem(PName)
                                item.SubItems.Add(PLocation)
                                item.SubItems.Add(PUser)
                                item.SubItems.Add(PDate)
                                item.SubItems.Add(PID)
                                ListView3.Items.Add(item)
                                Me.Refresh()

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
                    AddHandler Me.ListView3.ColumnClick, AddressOf ColumnClick

                    'Active le autosize
                    ColumnHeader6.Width = -2
                    'ColumnHeader7.Width = -2
                    ColumnHeader8.Width = -2
                    ColumnHeader14.Width = -2
                    ColumnHeader9.Width = -2

                    If ListView3.Items.Count > 0 Then
                        ListView3.Items(0).Selected = True
                        ListView3.Select()
                    End If
                    Me.Refresh()

                Case 3 'TAB 4
                    onetime = 0
                    Me.Refresh()
                    If ListView4.Items.Count <> 0 Then
                        'La liste n'est pas vide donc bypass le Select
                        ListView4.Items(0).Selected = True
                        ListView4.Select()
                        Label2.Visible = True
                        Exit Select
                    End If
                    ListView4.Items.Clear()
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
                                ListView4.Sorting = Windows.Forms.SortOrder.Ascending
                                Dim item As New ListViewItem(SName)
                                item.SubItems.Add(SEtat)
                                item.SubItems.Add(SType)
                                item.SubItems.Add(SServiceName)
                                ListView4.Items.Add(item)
                                Me.Refresh()

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
                    AddHandler Me.ListView4.ColumnClick, AddressOf ColumnClick
                    If ListView4.Items.Count > 0 Then
                        ListView4.Items(0).Selected = True
                        ListView4.Select()
                    End If
                    Me.Refresh()
            End Select

        Catch ex As Exception
            'Gestion de l'erreur
            Me.Cursor = Cursors.Default
        End Try
        Me.Cursor = Cursors.Default
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

        End Select

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick

    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick

    End Sub

    Private Sub ListView3_DoubleClick(sender As Object, e As EventArgs) Handles ListView3.DoubleClick
        Dim PID, PName
        PID = ListView3.SelectedItems.Item(0).SubItems(4).Text
        PName = ListView3.SelectedItems.Item(0).SubItems(0).Text

        Try
            Dim Result As Integer = MsgBox(My.Resources.ConfirmStopProcess & PName, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
            If Result = 6 Then

                'Dim remoteById As Process = Process.GetProcessById(PID, ComputerName)
                'remoteById.Kill()
                ' envoye par commande dos car le process .Kill ne marche pas sur un poste remote...
                Dim myCMDLine As String = "taskkill /s " & ComputerName & " /PID " & PID
                Shell(myCMDLine, AppWinStyle.Hide)
                Thread.Sleep(1000)
                ListView3.SelectedItems.Item(0).Remove()
            Else
                ' ne fait rien car le client a dit NON
            End If

        Catch ex As Exception
            'Gestion de l'erreur
        End Try

    End Sub

    Private Sub ListView4_DoubleClick(sender As Object, e As EventArgs) Handles ListView4.DoubleClick
        Dim PName, PServiceName, PStats

        PName = ListView4.SelectedItems.Item(0).SubItems(0).Text
        PServiceName = ListView4.SelectedItems.Item(0).SubItems(3).Text
        PStats = ListView4.SelectedItems.Item(0).SubItems(1).Text

        If PStats = "Running" Then
            Dim Result As Integer = MsgBox(My.Resources.ConfirmStopService & PName, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
            If Result = 6 Then
                Service_to_OFF(PServiceName, True)
                If Service_ON_OFF = "OFF" Then
                    Me.ListView4.SelectedItems.Item(0).SubItems(1).Text = "Stopped"
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
                    Me.ListView4.SelectedItems.Item(0).SubItems(1).Text = "Running"
                End If
                Me.Refresh()
            Else
                ' ne fait rien car le client a dit NON
            End If

        End If
    End Sub

    Private Sub Tab_pkg_app_DoubleClick(sender As Object, e As EventArgs) Handles Tab_pkg_app.DoubleClick
        'Force un refresh suite a une double clique
        Dim TabTemp = Tab_pkg_app.SelectedIndex

        Select Case TabTemp
            Case 0
                Me.ListView1.Items.Clear()
            Case 1
                Me.ListView2.Items.Clear()
            Case 2
                Me.ListView3.Items.Clear()
            Case 3
                Me.ListView4.Items.Clear()
        End Select

        Me.Tab_pkg_app.SelectedIndex = 6
        Me.Tab_pkg_app.SelectedIndex = TabTemp
    End Sub

    Private Sub chk_ShowAll_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ShowAll.CheckedChanged
        onetime = 0
        ProgressBar.Value = 0
        ProgressBar.Visible = True
        ListView1.Items.Clear()

        If chk_ShowAll.CheckState = CheckState.Checked Then
            'Veux voir tout les installation
            chk_ShowAll.Checked = True
            Tab_pkg_app_DoubleClick(Nothing, EventArgs.Empty)

        Else
            chk_ShowAll.Checked = False
            Tab_pkg_app_DoubleClick(Nothing, EventArgs.Empty)
        End If
    End Sub

End Class


'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Data.SqlClient
Imports System.Management
Imports System.Reflection
Imports System.Threading 'Thread.Sleep(1000)
Imports Microsoft.Win32
Imports System.ServiceProcess

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'       Ce module est utiliser pour afficher l'historique des Package déployer par SCCM et aussi de pouvoir voir maintenant voir les application et leur status 
'       qui sont installer sur le poste ou en cours d'installation
'       l'onget Software Cache location est masqué a l'utilisateur car na pas arriver a trouver comment décoder le ID
'       
' *******************************************************************************************************************************************************


Public Class Pack_Apps
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
        Me.ListView5.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Ascending)

        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        'Cache la tab5 car seras utiliser dans un develeoppement future
        Me.Tab_pkg_app.TabPages.Remove(Me.Tab_pkg_app.TabPages(4))
        Me.Cursor = Cursors.Default
        
        'Slectionne la tab 4 a cause que on cache une tab dans le Load de Pack_Apps
        Tab_pkg_app.SelectedIndex = 4 '5 est la valeur quand la tab n'est pas cacher
    End Sub

    Public Sub Tab_pkg_app_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Tab_pkg_app.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        Label1.Visible = False
        cmd_apps_refresh.Visible = False
        Try
            Select Case Tab_pkg_app.SelectedIndex
                Case 0 'TAB 1
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

                Case 1 'TAB 2
                    onetime = 0
                    Me.Refresh()

                    If ListView2.Items.Count <> 0 Then
                        'La liste n'est pas vide donc bypass le Select
                        ListView2.Items(0).Selected = True
                        ListView2.Select()
                        cmd_apps_refresh.Visible = True
                        Exit Select
                    End If

                    ListView2.Items.Clear()
                    ProgressBar.Value = 0
                    ProgressBar.Visible = True
                    cmd_apps_refresh.Visible = True

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
                        cmd_apps_refresh.Visible = False
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

                Case 2 'TAB 3
                    onetime = 0
                    Me.Refresh()

                    If ListView3.Items.Count <> 0 Then
                        'La liste n'est pas vide donc bypass le Select
                        ListView3.Items(0).Selected = True
                        ListView3.Select()
                        Exit Select
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

                Case 3 'TAB 4
                    onetime = 0
                    Me.Refresh()

                    If ListView4.Items.Count <> 0 Then
                        'La liste n'est pas vide donc bypass le Select
                        ListView4.Items(0).Selected = True
                        ListView4.Select()
                        Label1.Visible = True
                        Exit Select
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

                Case 4 'TAB5
                    onetime = 0
                    Me.Refresh()

                    If ListView5.Items.Count <> 0 Then
                        'La liste n'est pas vide donc bypass le Select
                        ListView5.Items(0).Selected = True
                        ListView5.Select()
                        Exit Select
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

            End Select

        Catch ex As Exception
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
                Case 5
                    ListView5.Sorting = SortOrder.Descending
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

            Case 5
                If ListView5.Sorting = SortOrder.Descending Then
                    ListView5.Sorting = SortOrder.Ascending
                    sortIt = SortOrder.Ascending
                Else
                    ListView5.Sorting = SortOrder.Descending
                    sortIt = SortOrder.Descending
                End If

                Me.ListView5.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)
        End Select

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Dim HistID_cache, HistPrg_Name_Cache
        Dim msg_reult

        'Récupere les valeur Sélectionner...

        HistID_cache = ListView1.SelectedItems.Item(0).SubItems(0).Text
        HistPrg_Name_Cache = ListView1.SelectedItems.Item(0).SubItems(1).Text

        msg_reult = MsgBox(My.Resources.ConfirmRemoveHistory, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")

        Select Case msg_reult

            Case 6 ' Oui

                Dim Key As RegistryKey = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, ComputerName).OpenSubKey(SCCM_PKG_HIST_REG_x86, True)
                Dim SubKeyName() As String = Key.GetSubKeyNames()
                Dim Index As Integer
                Dim SubKey As RegistryKey
                Dim SubLevel2_Name As String()
                Dim SubLevel2_Key As String
                Dim SubLevel3_Key As RegistryKey
                Dim Key_PkgName As String

                For Index = 0 To Key.SubKeyCount - 1
                    SubKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(SCCM_PKG_HIST_REG_x86 + "\" + SubKeyName(Index), True)
                    SubLevel2_Name = SubKey.GetSubKeyNames()
                    Try
                        For Each SubLevel2_Key In SubLevel2_Name
                            SubLevel3_Key = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ComputerName).OpenSubKey(SCCM_PKG_HIST_REG_x86 + "\" + SubKeyName(Index) + "\" + SubLevel2_Key, True)

                            Key_PkgName = SubLevel3_Key.GetValue("_ProgramID")

                            '* = pour le nom generic des tasksequance
                            If HistPrg_Name_Cache = Key_PkgName Or Key_PkgName = "*" Then
                                Try
                                    If SubKeyName(Index) = HistID_cache Then
                                        Key.DeleteSubKeyTree(SubKeyName(Index))
                                        ListView1.SelectedItems.Item(0).Remove()
                                        MsgBox(My.Resources.SuccessRemoveHistory, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SCCM PC Admin")
                                    End If

                                Catch ex As Exception
                                    MsgBox(My.Resources.ErrorRemoveHistory, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SCCM PC Admin")
                                End Try
                            End If

                        Next
                    Catch ex As Exception
                        'Gestion de l'erreur
                    End Try

                Next

            Case 7 ' Non

                ' Ne fait rien car le client a dit non

        End Select
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        'Dim App_Status, App_Name
        'Dim msg_reult

        ''Récupere les valeur Sélectionner...

        'App_Name = ListView2.SelectedItems.Item(0).SubItems(0).Text
        'App_Status = ListView2.SelectedItems.Item(0).SubItems(1).Text

        'msg_reult = MsgBox(My.Resources.ConfirmMWBypass, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")

        'Select Case msg_reult

        '    Case 6 ' Oui

        '        Dim result As Integer = SCCMPKG.ByPassServiceWindow_Advertisement_Apps(App_Name, ComputerName)

        '        If result = 0 Then
        '            MsgBox(My.Resources.SuccessPlannedInstall, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SCCM PC Admin")
        '        ElseIf result = 1 Then
        '            MsgBox(My.Resources.ErrorAnalyzeBeforeRetry, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SCCM PC Admin")
        '        End If

        '    Case 7 ' Non
        '        Exit Sub
        'End Select
    End Sub

    Private Sub ListView3_DoubleClick(sender As Object, e As EventArgs) Handles ListView3.DoubleClick
        Dim ID_Cache, Prg_Name_Cache
        Dim TS As Boolean = False
        Dim sc As New ServiceController()
        Dim objSMS As Object
        Dim objScheds As Object
        Dim msg_reult

        If Advance_mode = True Then

            ID_Cache = ListView3.SelectedItems.Item(0).SubItems(0).Text
            Prg_Name_Cache = ListView3.SelectedItems.Item(0).SubItems(1).Text

            If Microsoft.VisualBasic.Left(Prg_Name_Cache, 4) = "(TS)" Then TS = True

            If TS = True Then
                msg_reult = MsgBox(My.Resources.Message_Delete_TS, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")

                Select Case msg_reult

                    Case 6 ' Oui

                        objSMS = GetObject("winmgmts://" & ComputerName & "/root/ccm/SoftMgmtAgent")
                        objScheds = objSMS.ExecQuery("select * from CCM_TSExecutionRequest")
                        For Each objSched In objScheds

                            If InStr(UCase(objSched.ContentID), UCase(ID_Cache)) > 0 Then

                                Dim Inst = "CCM_TSExecutionRequest.RequestID=" & Chr(34) & objSched.RequestID.ToString & Chr(34)
                                Dim objWMIService As Object
                                Dim objItem As Object

                                objWMIService = GetObject("winmgmts:" & "{impersonationLevel=impersonate}!\\" & ComputerName & "\root\ccm\SoftMgmtAgent")
                                objItem = objWMIService.Get(Inst)
                                objItem.delete_()
                                ListView3.SelectedItems.Item(0).Remove()
                            End If
                        Next

                    Case 7 ' Non

                        ' Ne fait rien car le client a dit non

                End Select
            End If
        End If

    End Sub

    Private Sub ListView4_DoubleClick(sender As Object, e As EventArgs) Handles ListView4.DoubleClick
        Dim advID_cache, ID_Cache, Prg_Name_Cache
        Dim TS As Boolean = False
        Dim msg_reult
        Dim sc As New ServiceController()

        'Récupere les valeur Sélectionner...

        ID_Cache = ListView4.SelectedItems.Item(0).SubItems(0).Text
        Prg_Name_Cache = ListView4.SelectedItems.Item(0).SubItems(1).Text
        advID_cache = ListView4.SelectedItems.Item(0).SubItems(2).Text

        If Microsoft.VisualBasic.Left(Prg_Name_Cache, 4) = "(TS)" Then TS = True

        msg_reult = MsgBox(My.Resources.ConfirmMWBypass & Chr(13) & Chr(13) & My.Resources.ChoiceMWBypassYes & Chr(13) & Chr(13) & My.Resources.ChoiceMWBypassNo, MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel, "SCCM PC Admin")
        Me.Cursor = Cursors.WaitCursor
        Select Case msg_reult
            Case 6 ' Oui

                If TS = True Then ' Prepare les taches a faire avant de réexécuter la tache

                    'Retire l'historique de la tache 
                    Dim objSMS As Object
                    Dim objScheds As Object

                    Try
                        objSMS = GetObject("winmgmts://" & ComputerName & "/root/ccm/Scheduler")
                        objScheds = objSMS.ExecQuery("select * from CCM_Scheduler_History")
                        For Each objSched In objScheds
                            If InStr(UCase(objSched.ScheduleID), UCase(advID_cache)) > 0 Then

                                Dim Inst = "CCM_Scheduler_History.ScheduleID='" & objSched.ScheduleID.ToString & "',UserSID=" & Chr(34) & "Machine" & Chr(34)
                                Dim objWMIService As Object
                                Dim objItem As Object

                                objWMIService = GetObject("winmgmts:" & "{impersonationLevel=impersonate}!\\" & ComputerName & "\root\ccm\Scheduler")
                                objItem = objWMIService.Get(Inst)
                                objItem.delete_()
                            End If
                        Next

                        'restart CCMEXEC
                        sc.MachineName = ComputerName
                        sc.ServiceName = "CCMEXEC"

                        Try
                            sc.Stop()
                            sc.WaitForStatus(ServiceControllerStatus.Stopped, Timeout)
                            Thread.Sleep(3000)
                            sc.Start()
                            sc.WaitForStatus(ServiceControllerStatus.Running, Timeout)

                        Catch ex As Exception
                            'do nothing
                        End Try

                    Catch ex As Exception
                        'do nothing
                    End Try
                End If

                Thread.Sleep(5000)

                ' Continue ici le rerun du PKG
                Dim result As Integer = SCCMPKG.ByPassServiceWindow_Advertisement(advID_cache, ComputerName)

                If result = 0 Then
                    MsgBox(My.Resources.SuccessPlannedInstall, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SCCM PC Admin")
                ElseIf result = 1 Then
                    MsgBox(My.Resources.ErrorAnalyzeBeforeRetry, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SCCM PC Admin")
                End If

            Case 7 ' Non

                Dim result As Integer = SCCMPKG.ReRunAdvertisement(advID_cache, ComputerName)

                If result = 0 Then
                    MsgBox(My.Resources.SuccessPlannedInstall, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SCCM PC Admin")
                ElseIf result = 1 Then
                    MsgBox(My.Resources.ErrorAnalyzeBeforeRetry, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SCCM PC Admin")
                End If

            Case 2 ' Cancel
                Exit Sub
        End Select
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ListView5_DoubleClick(sender As Object, e As EventArgs) Handles ListView3.DoubleClick

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
            Case 4
                Me.ListView5.Items.Clear()
        End Select

        Me.Tab_pkg_app.SelectedIndex = 6
        Me.Tab_pkg_app.SelectedIndex = TabTemp
    End Sub

    Private Sub cmd_apps_refresh_Click(sender As Object, e As EventArgs) Handles cmd_apps_refresh.Click
        Dim popupRefreshApps As Popup_Refresh_Apps = New Popup_Refresh_Apps
        popupRefreshApps.ShowDialog(Me)
    End Sub

    Private Sub ListView5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView5.SelectedIndexChanged

    End Sub
End Class

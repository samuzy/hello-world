
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Data.SqlClient
Imports System.Management
Imports System.Reflection
Imports System.Threading

' *******************************************************************************************************************************************************
'      PC ++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'
'       Module qui affiche les mise a jour de sécurité qui son installer sur le poste a distance
'
' *******************************************************************************************************************************************************


Public Class WSUS_SCUP
    Inherits LocalizedForm
    'Gestion des valeur pour la derniere colonne et de la liste view sélectionner
    Private lastCol As Integer = 0

    Private Sub WSUS_SCUP_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "SCCM PC Admin  " & ComputerName
        Me.Cursor = Cursors.WaitCursor

        'Active le tri par selection de colonne
        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(0, SortOrder.Descending)

        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        onetime = 0
        ProgressBar.Value = 0

        ListView1.Items.Clear()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub WSUS_SCUP_Shown() Handles Me.Shown
        chk_ApprovedPatch.Checked = True
    End Sub

    Private Sub ListView1_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListView1.ColumnClick
        ' Set the ListViewItemSorter property to a new ListViewItemComparer object.
        Dim sortIt As SortOrder

        If e.Column <> lastCol Then
            ListView1.Sorting = SortOrder.Descending
            sortIt = SortOrder.Descending
        End If

        'ce souvin de la derniere colonne
        lastCol = e.Column

        If ListView1.Sorting = SortOrder.Descending Then
            ListView1.Sorting = SortOrder.Ascending
            sortIt = SortOrder.Ascending
        Else
            ListView1.Sorting = SortOrder.Descending
            sortIt = SortOrder.Descending
        End If

        Me.ListView1.ListViewItemSorter = New ListViewItemComparer(e.Column, sortIt)

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
                        ListView1.Sorting = Windows.Forms.SortOrder.Descending
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
                        If ListView1.Items.Find(item.Name, False).Count >= 1 Then
                            'Ne fait rien 
                        Else
                            If Status = "Missing" Then
                                'Mais la ligne en rouge si la valeur est manquante
                                item.BackColor = Color.DarkRed
                                item.ForeColor = Color.White
                                count_miss = count_miss + 1
                            End If
                            ListView1.Items.Add(item)
                        End If
                        lbl_missing.Text = count_miss.ToString
                        lbl_patch_count.Text = count_WSUS_Approved.ToString

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

        If ListView1.Items.Count > 0 Then
            ListView1.Items(0).Selected = True
            ListView1.Select()
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
                ListView1.Sorting = Windows.Forms.SortOrder.Descending
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
                If ListView1.Items.Find(item.Name, False).Count >= 1 Then
                    'Doublons ne pas inscrire mais augmenter la progression de la barre
                    count2 = count2 + 1
                Else
                    If Status = "Missing" Then
                        'Mais la ligne en rouge si la valeur est manquante
                        item.BackColor = Color.DarkRed
                        item.ForeColor = Color.White
                        count_miss = count_miss + 1
                    End If
                    ListView1.Items.Add(item)
                    count = count + 1
                End If
                lbl_missing.Text = count_miss.ToString
                lbl_patch_count.Text = count.ToString

                countVal = ((count + count2) / Index) * 100
                If countVal > 100 Or countVal < 0 Then countVal = 100
                ProgressBar.Value = countVal

                Me.Update()
            End If
        Next
        ProgressBar.Visible = False
        onetime = 1

        Me.Cursor = Cursors.Default
        If ListView1.Items.Count > 0 Then
            ListView1.Items(0).Selected = True
            ListView1.Select()
        End If
        Me.Refresh()
    End Sub

    Private Sub chk_ApprovedPatch_CheckedChanged(sender As Object, e As EventArgs) Handles chk_ApprovedPatch.CheckedChanged
        onetime = 0
        ProgressBar.Value = 0
        ProgressBar.Visible = True
        ListView1.Items.Clear()

        If chk_ApprovedPatch.CheckState = CheckState.Checked Then
            chk_ApprovedPatch.Checked = True
            Start_WSUS_Approved()
        Else
            chk_ApprovedPatch.Checked = False
            WSUS_ALL()
        End If

    End Sub

    Private Sub cmd_apps_refresh_Click(sender As Object, e As EventArgs) Handles cmd_apps_refresh.Click
        Dim popupRefreshWSUS As Popup_Refresh_WSUS = New Popup_Refresh_WSUS
        popupRefreshWSUS.ShowDialog(Me)
    End Sub

    Private Sub cmd_Refresh_Click(sender As Object, e As EventArgs) Handles cmd_Refresh.Click
        onetime = 0
        ProgressBar.Value = 0
        ListView1.Items.Clear()

        Me.Cursor = Cursors.WaitCursor
        chk_ApprovedPatch.Checked = True
        Start_WSUS_Approved()
        Me.Cursor = Cursors.Default

    End Sub
End Class


Imports System.IO
Imports System.Management
Imports System.Reflection
Imports System.Threading 'Thread.Sleep(1000)
Imports Microsoft.Win32
Imports System.Text.RegularExpressions

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


Public Class Adv_Mode_SCCM_CLient
    Inherits LocalizedForm

    Private Sub Adv_Mode_SCCM_CLient_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        '...
        Me.Cursor = Cursors.WaitCursor

        Me.Cursor = Cursors.Default

        lbl_loading.Visible = False
    End Sub

    Private Sub cmd_Start_Click(sender As Object, e As EventArgs) Handles cmd_Start.Click

        Client_SCCM_Command_line = ""

        'Ajout des commandes

        'Group1
        If CheckBox1.CheckState = CheckState.Checked Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /noservice"
        If CheckBox2.CheckState = CheckState.Checked Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /service"
        If CheckBox7.CheckState = CheckState.Checked Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /NoCRLCheck"
        If CheckBox4.CheckState = CheckState.Checked Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /logon"
        If CheckBox5.CheckState = CheckState.Checked Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /forcereboot"
        If CheckBox8.CheckState = CheckState.Checked Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /forceinstall"
        If Not TextBox2.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /downloadtimeout:" & TextBox2.Text
        If Not TextBox1.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /retry:" & TextBox1.Text
        If ComboBox1.SelectedIndex >= 1 Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /BITSPriority:" & ComboBox1.Text
        If ComboBox2.SelectedIndex >= 1 Then Client_SCCM_Command_line = Client_SCCM_Command_line & " /skipprereq:" & ComboBox2.Text

        'Group2
        If CheckBox6.CheckState = CheckState.Checked Then Client_SCCM_Command_line = Client_SCCM_Command_line & " CCMALLOWSILENTREBOOT"
        If ComboBox3.SelectedIndex >= 1 Then Client_SCCM_Command_line = Client_SCCM_Command_line & " CCMDEBUGLOGGING=" & ComboBox3.Text
        If Not TextBox6.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " CCMEVALINTERVAL=" & TextBox6.Text
        If Not TextBox7.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " CCMEVALHOUR=" & TextBox7.Text
        If Not TextBox9.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " CCMHTTPPORT=" & TextBox9.Text
        If Not TextBox10.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " CCMHTTPSPORT=" & TextBox10.Text
        If ComboBox4.SelectedIndex >= 1 Then Client_SCCM_Command_line = Client_SCCM_Command_line & " CCMLOGLEVEL=" & ComboBox4.Text
        If ComboBox5.SelectedIndex >= 1 Then Client_SCCM_Command_line = Client_SCCM_Command_line & " NOTIFYONLY=" & ComboBox5.Text
        If ComboBox6.SelectedIndex >= 1 Then Client_SCCM_Command_line = Client_SCCM_Command_line & " SITEREASSIGN=" & ComboBox6.Text
        If Not TextBox15.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " SMSSITECODE=" & UCase(TextBox15.Text)
        If Not TextBox13.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " SMSCACHESIZE=" & TextBox13.Text
        If ComboBox7.SelectedIndex >= 1 Then Client_SCCM_Command_line = Client_SCCM_Command_line & " SMSDIRECTORYLOOKUP=" & ComboBox7.Text
        If Not TextBox11.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " DNSSUFFIX=" & UCase(TextBox11.Text)
        If Not TextBox12.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " FSP=" & UCase(TextBox12.Text)
        If Not TextBox14.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " SMSMP=" & UCase(TextBox14.Text)
        If Not TextBox8.Text = "" Then Client_SCCM_Command_line = Client_SCCM_Command_line & " CCMHOSTNAME=" & UCase(TextBox8.Text)

        'Group3

        If Not TextBox3.Text = "" Then
            'Vide le Client_SCCM_Command_line au cas ou....
            Client_SCCM_Command_line = ""
            Client_SCCM_Command_line = " " & UCase(TextBox3.Text)
        End If


        'Group4
        If CheckBox3.CheckState = CheckState.Checked Then
            'Vide le Client_SCCM_Command_line au cas ou....
            Client_SCCM_Command_line = ""
            Client_SCCM_Command_line = Client_SCCM_Command_line & " /uninstall"
        End If

        MsgBox(Client_SCCM_Command_line)

        'Valide si on install ou pas
        Dim Result_msg = MsgBox(My.Resources.WarningReinstallClient & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.MessageReinstallClient & " : " & ComputerName)

        If Result_msg = 6 Then

            'Mes le site et le cient version en mode vide
            SiteCode = "?"
            ClientVer = "?"

            Dim popupReinstallClientForm As Popup_Reinstall_Client = New Popup_Reinstall_Client
            popupReinstallClientForm.ShowDialog(Me)
        End If


    End Sub

    'Private Sub force_uninstall_Client_Click()
    '    ' N'est plus utiliser.....

    '    Me.Cursor = Cursors.WaitCursor
    '    'arret du service sccm
    '    Service_to_OFF("ccmexec", False)

    '    Dim strSDPath
    '    strSDPath = "\\" & ComputerName & "\admin$\ccmsetup\"
    '    RemoteExec(strSDPath + "ccmsetup.exe /uninstall")

    '    'Temp d'attente pour que le Uninstall Termine
    '    Thread.Sleep(10000)

    '    Dim UnInstall As Boolean = False
    '    Do While UnInstall = False
    '        Dim strObject = "winmgmts://" & ComputerName
    '        For Each Process In GetObject(strObject).InstancesOf("win32_process")
    '            If UCase(Process.name) = UCase("CCMSETUP.EXE") Then
    '                UnInstall = False
    '                Exit For
    '            Else
    '                UnInstall = True
    '            End If
    '        Next
    '    Loop

    '    '*********** Suprimmer les répertoires du SCCM ***************"
    '    Try
    '        strSDPath = "\\" & ComputerName & "\admin$\CCM"
    '        System.IO.Directory.Delete(strSDPath, True)
    '    Catch ex As Exception
    '        'Gestion de l'erreur
    '    End Try

    '    Try
    '        strSDPath = "\\" & ComputerName & "\admin$\ccmcache"
    '        System.IO.Directory.Delete(strSDPath, True)
    '    Catch ex As Exception
    '        'Gestion de l'erreur
    '    End Try

    '    Try
    '        strSDPath = "\\" & ComputerName & "\admin$\ccmsetup"
    '        System.IO.Directory.Delete(strSDPath, True)
    '    Catch ex As Exception
    '        'Gestion de l'erreur
    '    End Try


    '    'Mes le site et le cient version en mode vide
    '    Main.Instance.txt_Client_Version_Result.Text = "?"
    '    Main.Instance.txt_SiteCode_result.Text = "?"
    '    SiteCode = "?"
    '    ClientVer = "?"


    '    Me.Cursor = Cursors.Default
    'End Sub

#Region "Mouse mouvement"
    Private Sub CheckBox1_MouseHover(sender As Object, e As EventArgs) Handles CheckBox1.MouseHover
        '/noservice
        txt_Description.Text = My.Resources.CCM_COMMAND_1
    End Sub

    Private Sub CheckBox1_MouseLeave(sender As Object, e As EventArgs) Handles CheckBox1.MouseLeave
        '/noservice
        txt_Description.Text = ""
    End Sub
    Private Sub CheckBox2_MouseHover(sender As Object, e As EventArgs) Handles CheckBox2.MouseHover
        '/service
        txt_Description.Text = My.Resources.CCM_COMMAND_2
    End Sub

    Private Sub CheckBox2_MouseLeave(sender As Object, e As EventArgs) Handles CheckBox2.MouseLeave
        '/NoCRLCheck
        txt_Description.Text = ""
    End Sub

    Private Sub CheckBox3_MouseHover(sender As Object, e As EventArgs) Handles CheckBox3.MouseHover
        '/uninstall
        txt_Description.Text = My.Resources.CCM_COMMAND_3
    End Sub

    Private Sub CheckBox3_MouseLeave(sender As Object, e As EventArgs) Handles CheckBox3.MouseLeave
        '/uninstall
        txt_Description.Text = ""
    End Sub

    Private Sub CheckBox4_MouseHover(sender As Object, e As EventArgs) Handles CheckBox4.MouseHover
        '/logon
        txt_Description.Text = My.Resources.CCM_COMMAND_4
    End Sub

    Private Sub CheckBox4_MouseLeave(sender As Object, e As EventArgs) Handles CheckBox4.MouseLeave
        '/logon
        txt_Description.Text = ""
    End Sub

    Private Sub CheckBox5_MouseHover(sender As Object, e As EventArgs) Handles CheckBox5.MouseHover
        '/forcereboot
        txt_Description.Text = My.Resources.CCM_COMMAND_5
    End Sub

    Private Sub CheckBox5_MouseLeave(sender As Object, e As EventArgs) Handles CheckBox5.MouseLeave
        '/forcereboot
        txt_Description.Text = ""
    End Sub

    Private Sub CheckBox6_MouseHover(sender As Object, e As EventArgs) Handles CheckBox6.MouseHover
        'CCMALLOWSILENTREBOOT
        txt_Description.Text = My.Resources.CCM_COMMAND_6
    End Sub

    Private Sub CheckBox6_MouseLeave(sender As Object, e As EventArgs) Handles CheckBox6.MouseLeave
        'CCMALLOWSILENTREBOOT
        txt_Description.Text = ""
    End Sub

    Private Sub CheckBox7_MouseHover(sender As Object, e As EventArgs) Handles CheckBox7.MouseHover
        '/NoCRLCheck
        txt_Description.Text = My.Resources.CCM_COMMAND_7
    End Sub

    Private Sub CheckBox7_MouseLeave(sender As Object, e As EventArgs) Handles CheckBox7.MouseLeave
        '/NoCRLCheck
        txt_Description.Text = ""
    End Sub
    Private Sub CheckBox8_MouseHover(sender As Object, e As EventArgs) Handles CheckBox8.MouseHover
        '/forceinstall
        txt_Description.Text = My.Resources.CCM_COMMAND_5
    End Sub

    Private Sub CheckBox8_MouseLeave(sender As Object, e As EventArgs) Handles CheckBox8.MouseLeave
        '/forceinstall
        txt_Description.Text = ""
    End Sub

    Private Sub Label1_MouseHover(sender As Object, e As EventArgs) Handles Label1.MouseHover
        '/retry:<Minutes>
        txt_Description.Text = My.Resources.CCM_COMMAND_9
    End Sub

    Private Sub Label1_MouseLeave(sender As Object, e As EventArgs) Handles Label1.MouseLeave
        '/retry:<Minutes>
        txt_Description.Text = ""
    End Sub

    Private Sub Label2_MouseHover(sender As Object, e As EventArgs) Handles Label2.MouseHover
        '/BITSPriority:
        txt_Description.Text = My.Resources.CCM_COMMAND_10
    End Sub

    Private Sub Label2_MouseLeave(sender As Object, e As EventArgs) Handles Label2.MouseLeave
        '/BITSPriority:
        txt_Description.Text = ""
    End Sub

    Private Sub Label3_MouseHover(sender As Object, e As EventArgs) Handles Label3.MouseHover
        '/downloadtimeout:<Minutes>
        txt_Description.Text = My.Resources.CCM_COMMAND_11
    End Sub

    Private Sub Label3_MouseLeave(sender As Object, e As EventArgs) Handles Label3.MouseLeave
        '/downloadtimeout:<Minutes>
        txt_Description.Text = ""
    End Sub

    Private Sub Label4_MouseHover(sender As Object, e As EventArgs) Handles Label4.MouseHover
        '/skipprereq:
        txt_Description.Text = My.Resources.CCM_COMMAND_12
    End Sub

    Private Sub Label4_MouseLeave(sender As Object, e As EventArgs) Handles Label4.MouseLeave
        '/skipprereq:
        txt_Description.Text = ""
    End Sub

    Private Sub Label5_MouseHover(sender As Object, e As EventArgs) Handles Label5.MouseHover
        'Client_SCCM_Command_line
        txt_Description.Text = My.Resources.CCM_COMMAND_13
    End Sub

    Private Sub Label5_MouseLeave(sender As Object, e As EventArgs) Handles Label5.MouseLeave
        'Client_SCCM_Command_line
        txt_Description.Text = ""
    End Sub

    Private Sub Label6_MouseHover(sender As Object, e As EventArgs) Handles Label6.MouseHover
        'SMSSITECODE
        txt_Description.Text = My.Resources.CCM_COMMAND_14
    End Sub

    Private Sub Label6_MouseLeave(sender As Object, e As EventArgs) Handles Label6.MouseLeave
        'SMSSITECODE
        txt_Description.Text = ""
    End Sub

    Private Sub Label7_MouseHover(sender As Object, e As EventArgs) Handles Label7.MouseHover
        'SMSMP
        txt_Description.Text = My.Resources.CCM_COMMAND_15
    End Sub

    Private Sub Label7_MouseLeave(sender As Object, e As EventArgs) Handles Label7.MouseLeave
        'SMSMP
        txt_Description.Text = ""
    End Sub

    Private Sub Label8_MouseHover(sender As Object, e As EventArgs) Handles Label8.MouseHover
        'CCMDEBUGLOGGING
        txt_Description.Text = My.Resources.CCM_COMMAND_16
    End Sub

    Private Sub Label8_MouseLeave(sender As Object, e As EventArgs) Handles Label8.MouseLeave
        'CCMDEBUGLOGGING
        txt_Description.Text = ""
    End Sub

    Private Sub Label9_MouseHover(sender As Object, e As EventArgs) Handles Label9.MouseHover
        'CCMEVALINTERVAL
        txt_Description.Text = My.Resources.CCM_COMMAND_17
    End Sub

    Private Sub Label9_MouseLeave(sender As Object, e As EventArgs) Handles Label9.MouseLeave
        'CCMEVALINTERVAL
        txt_Description.Text = ""
    End Sub

    Private Sub Label10_MouseHover(sender As Object, e As EventArgs) Handles Label10.MouseHover
        'CCMEVALHOUR
        txt_Description.Text = My.Resources.CCM_COMMAND_18
    End Sub

    Private Sub Label10_MouseLeave(sender As Object, e As EventArgs) Handles Label10.MouseLeave
        'CCMEVALHOUR
        txt_Description.Text = ""
    End Sub

    Private Sub Label11_MouseHover(sender As Object, e As EventArgs) Handles Label11.MouseHover
        'CCMHOSTNAME
        txt_Description.Text = My.Resources.CCM_COMMAND_19
    End Sub

    Private Sub Label11_MouseLeave(sender As Object, e As EventArgs) Handles Label11.MouseLeave
        'CCMHOSTNAME
        txt_Description.Text = ""
    End Sub

    Private Sub Label12_MouseHover(sender As Object, e As EventArgs) Handles Label12.MouseHover
        'CCMHTTPPORT
        txt_Description.Text = My.Resources.CCM_COMMAND_20
    End Sub

    Private Sub Label12_MouseLeave(sender As Object, e As EventArgs) Handles Label12.MouseLeave
        'CCMHTTPPORT
        txt_Description.Text = ""
    End Sub

    Private Sub Label13_MouseHover(sender As Object, e As EventArgs) Handles Label13.MouseHover
        'CCMHTTPSPORT
        txt_Description.Text = My.Resources.CCM_COMMAND_21
    End Sub

    Private Sub Label13_MouseLeave(sender As Object, e As EventArgs) Handles Label13.MouseLeave
        'CCMHTTPSPORT
        txt_Description.Text = ""
    End Sub

    Private Sub Label14_MouseHover(sender As Object, e As EventArgs) Handles Label14.MouseHover
        'CCMLOGLEVEL
        txt_Description.Text = My.Resources.CCM_COMMAND_22
    End Sub

    Private Sub Label14_MouseLeave(sender As Object, e As EventArgs) Handles Label14.MouseLeave
        'CCMLOGLEVEL
        txt_Description.Text = ""
    End Sub

    Private Sub Label15_MouseHover(sender As Object, e As EventArgs) Handles Label15.MouseHover
        'DNSSUFFIX
        txt_Description.Text = My.Resources.CCM_COMMAND_23
    End Sub

    Private Sub Label15_MouseLeave(sender As Object, e As EventArgs) Handles Label15.MouseLeave
        'DNSSUFFIX
        txt_Description.Text = ""
    End Sub

    Private Sub Label16_MouseHover(sender As Object, e As EventArgs) Handles Label16.MouseHover
        'FSP
        txt_Description.Text = My.Resources.CCM_COMMAND_24
    End Sub

    Private Sub Label16_MouseLeave(sender As Object, e As EventArgs) Handles Label16.MouseLeave
        'FSP
        txt_Description.Text = ""
    End Sub

    Private Sub Label17_MouseHover(sender As Object, e As EventArgs) Handles Label17.MouseHover
        'SITEREASSIGN
        txt_Description.Text = My.Resources.CCM_COMMAND_25
    End Sub

    Private Sub Label17_MouseLeave(sender As Object, e As EventArgs) Handles Label17.MouseLeave
        'SITEREASSIGN
        txt_Description.Text = ""
    End Sub

    Private Sub Label18_MouseHover(sender As Object, e As EventArgs) Handles Label18.MouseHover
        'NOTIFYONLY
        txt_Description.Text = My.Resources.CCM_COMMAND_26
    End Sub

    Private Sub Label18_MouseLeave(sender As Object, e As EventArgs) Handles Label18.MouseLeave
        'NOTIFYONLY
        txt_Description.Text = ""
    End Sub

    Private Sub Label19_MouseHover(sender As Object, e As EventArgs) Handles Label19.MouseHover
        'SMSCACHESIZE
        Me.txt_Description.Font = New Font("Microsoft Sans Serif", 8)
        txt_Description.Text = My.Resources.CCM_COMMAND_27
    End Sub

    Private Sub Label19_MouseLeave(sender As Object, e As EventArgs) Handles Label19.MouseLeave
        'SMSCACHESIZE
        Me.txt_Description.Font = New Font("Microsoft Sans Serif", 10)
        txt_Description.Text = ""
    End Sub

    Private Sub Label20_MouseHover(sender As Object, e As EventArgs) Handles Label20.MouseHover
        'SMSDIRECTORYLOOKUP
        Me.txt_Description.Font = New Font("Microsoft Sans Serif", 8.75)
        txt_Description.Text = My.Resources.CCM_COMMAND_28
    End Sub

    Private Sub Label20_MouseLeave(sender As Object, e As EventArgs) Handles Label20.MouseLeave
        'SMSDIRECTORYLOOKUP
        Me.txt_Description.Font = New Font("Microsoft Sans Serif", 10)
        txt_Description.Text = ""
    End Sub

    Private Sub GroupBox1_MouseHover(sender As Object, e As EventArgs) Handles GroupBox1.MouseHover
        Uninstall_Validation()
    End Sub

    Private Sub GroupBox1_MouseLeave(sender As Object, e As EventArgs) Handles GroupBox1.MouseLeave
        Uninstall_Validation()
    End Sub

    Private Sub GroupBox2_MouseHover(sender As Object, e As EventArgs) Handles GroupBox2.MouseHover
        Uninstall_Validation()
    End Sub

    Private Sub GroupBox2_MouseLeave(sender As Object, e As EventArgs) Handles GroupBox2.MouseLeave
        Uninstall_Validation()
    End Sub

    Private Sub GroupBox3_MouseHover(sender As Object, e As EventArgs) Handles GroupBox3.MouseHover
        Uninstall_Validation()
    End Sub

    Private Sub GroupBox3_MouseLeave(sender As Object, e As EventArgs) Handles GroupBox3.MouseLeave
        Uninstall_Validation()
    End Sub
#End Region

#Region "Validation des champs"

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        'Valide que ce soit des chifres
        Dim digitsOnly As Regex = New Regex("[^\d]")
        TextBox2.Text = digitsOnly.Replace(TextBox2.Text, "")
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'Valide que ce soit des chifres
        Dim digitsOnly As Regex = New Regex("[^\d]")
        TextBox1.Text = digitsOnly.Replace(TextBox1.Text, "")
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        'Désactive l'ensemble des autre commande si du texte ce retouve dans ce champ
        If TextBox3.Text = "" Then
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
            GroupBox4.Enabled = True
        Else
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
            GroupBox4.Enabled = False
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        'Valide que ce soit des chifres
        Dim digitsOnly As Regex = New Regex("[^\d]")
        TextBox6.Text = digitsOnly.Replace(TextBox6.Text, "")
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        'Valide que ce soit des chifres
        Dim digitsOnly As Regex = New Regex("[^\d]")
        TextBox7.Text = digitsOnly.Replace(TextBox7.Text, "")

        If TextBox7.Text <> "" Then
            Dim Num_val As Integer
            Num_val = TextBox7.Text

            'Valide que ce soit entre 1 et 23 (Max d'heure)
            If Num_val < 24 And Num_val > 0 Then
                ' Valeur Valide
                lbl_CCMVALHOUR_Warning.Visible = False
            Else
                'Enleve le dernier chifre entrer
                TextBox7.Text = ""
                lbl_CCMVALHOUR_Warning.Visible = True
            End If
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.CheckState = CheckState.Unchecked Then
            GroupBox1.Enabled = True
            GroupBox2.Enabled = True
            GroupBox3.Enabled = True
            'GroupBox4.Enabled = True
        Else
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
            GroupBox3.Enabled = False
            'GroupBox4.Enabled = False
        End If
    End Sub

    Private Sub Uninstall_Validation()
        If CheckBox1.CheckState = CheckState.Unchecked And
            _CheckBox1.CheckState = CheckState.Unchecked And
            _CheckBox2.CheckState = CheckState.Unchecked And
            _CheckBox7.CheckState = CheckState.Unchecked And
            _CheckBox4.CheckState = CheckState.Unchecked And
            _CheckBox5.CheckState = CheckState.Unchecked And
            _CheckBox8.CheckState = CheckState.Unchecked And
            _TextBox1.Text = "" And
            _TextBox2.Text = "" And
            _ComboBox1.SelectedIndex <= 0 And
            _ComboBox2.SelectedIndex <= 0 And
            _CheckBox6.CheckState = CheckState.Unchecked And
            _TextBox6.Text = "" And
            _TextBox7.Text = "" And
            _TextBox8.Text = "" And
            _TextBox9.Text = "" And
            _TextBox10.Text = "" And
            _TextBox11.Text = "" And
            _TextBox12.Text = "" And
            _TextBox13.Text = "" And
            _TextBox14.Text = "" And
            _TextBox15.Text = "" And
            _ComboBox3.SelectedIndex <= 0 And
            _ComboBox4.SelectedIndex <= 0 And
            _ComboBox5.SelectedIndex <= 0 And
            _ComboBox6.SelectedIndex <= 0 And
            _ComboBox6.SelectedIndex <= 0 And
            _TextBox3.Text = "" Then

            GroupBox4.Enabled = True
        Else
            GroupBox4.Enabled = False
        End If
    End Sub

#End Region

End Class

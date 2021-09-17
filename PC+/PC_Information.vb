
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Reflection
Imports System.Management
Imports System.DirectoryServices
Imports System.DirectoryServices.AccountManagement

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'       Ce module est utiliser pour regrouper les info du pc rechercher et de donner les membres qui son assigné a ce PC   
'
' *******************************************************************************************************************************************************


Public Class PC_Info
    Inherits LocalizedForm

    Private Sub PC_Information_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Cursor = Cursors.WaitCursor

        Me.Text = "SCCM PC Admin  " & ComputerName

        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        txt_ComputerName.Text = ComputerName

        txt_img_ver.Text = VerImg_data
        txt_SRU_Verimg.Text = SRU_VerImg_data
        txt_last_reboot.Text = WMIDateConvert(str_LastBootUpTime)
        txt_img_install_Date.Text = WMIDateConvert(str_InstallDate)
        txt_Domain.Text = PC_Domain
        txt_OSCaption.Text = OSName
        txt_IP.Text = IPAddress_Value

        If m_strChassisTypes = "MOBILE_DEVICE" Then
            txt_TypePC.Text = My.Resources.txt_TypePC_text_mobile
        ElseIf m_strChassisTypes = "DESKTOP" Then
            txt_TypePC.Text = My.Resources.txt_TypePC_text_desktop
        Else
            txt_TypePC.Text = m_strChassisTypes
        End If

        If OSLanguage = "1036" Then
            txt_language.Text = My.Resources.txt_language_text_fr
        ElseIf OSLanguage = "1033" Then
            txt_language.Text = My.Resources.txt_language_text_en
        End If

        GetInfoPC()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub GetInfoPC()

        Dim WMI_Info As New ManagementScope("\\" & ComputerName & "\ROOT\CIMV2")
        Dim Query As New SelectQuery("SELECT * FROM Win32_ComputerSystemProduct")
        Dim search As New ManagementObjectSearcher(WMI_Info, Query)

        Dim info As ManagementObject

        Try
            For Each info In search.Get()
                txt_Vendor.Text = info("Vendor")
                txt_Name.Text = info("Name")
                txt_Version.Text = info("Version")
            Next
        Catch ex As Exception

        End Try

        txt_RAM.Text = PC_Mem_size

        Dim WMI_Info3 As New ManagementScope("\\" & ComputerName & "\ROOT\CIMV2")
        Dim Query3 As New SelectQuery("SELECT * FROM Win32_Processor")
        Dim search3 As New ManagementObjectSearcher(WMI_Info3, Query3)

        Dim info3 As ManagementObject

        Try
            For Each info3 In search3.Get()
                txt_CPU.Text = info3("Name")
            Next

        Catch ex As Exception

        End Try

        'Membership du PC

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

    End Sub

    Private Sub cmd_manage_Click(sender As Object, e As EventArgs) Handles cmd_manage.Click
        Clipboard.SetText(ComputerName)
        MsgBox(My.Resources.MsgClipboard, MsgBoxStyle.Information)

        Dim WebPage = "http://workstationmgt/"
        Process.Start(WebPage)
        Me.Close()
    End Sub
End Class

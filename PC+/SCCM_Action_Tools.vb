
'TERMINER NE PLUS TOUCHER (Logiciel au stade BETA 3) Forme Valider...

Imports System.Reflection
Imports System.Threading 'Thread.Sleep(1000) 'Thread.Sleep(1000)

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'      Ce module permet d'envoyer a distance les action sccm divers
'      
' *******************************************************************************************************************************************************


Public Class SCCM_Action_Tools
    Inherits LocalizedForm
    Dim All As Boolean = False
    '{00000000-0000-0000-0000-000000000001} Hardware Inventory....
    '{00000000-0000-0000-0000-000000000002} Software Inventory....
    '{00000000-0000-0000-0000-000000000003} Discovery Inventory....
    '{00000000-0000-0000-0000-000000000010} File Collection
    '{00000000-0000-0000-0000-000000000021} Request Machine Assignments
    '{00000000-0000-0000-0000-000000000022} Evaluate Machine Policies
    '{00000000-0000-0000-0000-000000000023} Refresh Default MP Task
    '{00000000-0000-0000-0000-000000000024} LS (Location Service) Refresh Locations Task
    '{00000000-0000-0000-0000-000000000025} LS (Location Service) Timeout Refresh Task
    '{00000000-0000-0000-0000-000000000031} Software Metering Generating Usage Report
    '{00000000-0000-0000-0000-000000000032} Source Update Message
    '{00000000-0000-0000-0000-000000000040} Machine Policy Agent Cleanup
    '{00000000-0000-0000-0000-000000000042} Policy Agent Validate Machine Policy / Assignment
    '{00000000-0000-0000-0000-000000000051} Retrying/Refreshing certificates in AD on MP
    '{00000000-0000-0000-0000-000000000108} Software Updates Assignments Evaluation Cycle
    '{00000000-0000-0000-0000-000000000111} Send Unsent State Message
    '{00000000-0000-0000-0000-000000000112} State System policy cache cleanout
    '{00000000-0000-0000-0000-000000000113} Scan by Update Source
    '{00000000-0000-0000-0000-000000000114} Update Store Policy
    '{00000000-0000-0000-0000-000000000116} State system policy bulk send low
    '{00000000-0000-0000-0000-000000000120} AMT Status Check Policy
    '{00000000-0000-0000-0000-000000000121} Application manager policy action
    '{00000000-0000-0000-0000-000000000131} Power management start summarizer

    Private Sub SCCM_Action_Tools_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)
        All = False
    End Sub

    Private Sub Button1_Click()
        Button1.Enabled = False
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
        Button1.Enabled = False
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

        Button1_Click()
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

        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1_Click()
    End Sub

    Private Sub Button121_Click(sender As Object, e As EventArgs) Handles Button121.Click
        Button121_Click()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button3_Click()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Button10_Click()
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Button21_Click()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2_Click()
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
End Class

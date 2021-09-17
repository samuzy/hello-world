
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Reflection
Imports System.Threading

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'      Ce module est utiliser pour envoyer les action néssécaire pour mettre a jours le cathalogue de WSUS
'      
' *******************************************************************************************************************************************************


Public Class Popup_Refresh_WSUS
    Inherits LocalizedForm

    Private Sub Popup_Refresh_WSUS_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)
    End Sub

    Private Sub Popup_Refresh_WSUS_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor

        '**********************************************************************************
        '{00000000-0000-0000-0000-000000000113} Scan by Update Source
        Dim myCMDLine As String = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000113}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        pic_done1.Visible = True
        Refresh()
        Thread.Sleep(1000)

        '**********************************************************************************
        '{00000000-0000-0000-0000-000000000108} Software Updates Assignments Evaluation Cycle
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000108}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        pic_done2.Visible = True
        Refresh()
        Thread.Sleep(1000)

        ''**********************************************************************************
        ''{00000000-0000-0000-0000-000000000002} Software Inventory....
        'myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000002}" & Chr(34) & " /NOINTERACTIVE"
        'Shell(myCMDLine, AppWinStyle.Hide)
        'Thread.Sleep(1000)
        'pic_done3.Visible = True
        'Refresh()
        'Thread.Sleep(1000)

        '**********************************************************************************
        '{00000000-0000-0000-0000-000000000021} Request Machine Assignments
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000021}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)
        Thread.Sleep(1000)
        pic_done4.Visible = True
        Refresh()
        Thread.Sleep(1000)

        'Extra '{00000000-0000-0000-0000-000000000010} File Collection
        myCMDLine = "WMIC.exe /node:" & Chr(34) & ComputerName & Chr(34) & " /namespace:\\root\ccm path sms_client CALL TriggerSchedule " & Chr(34) & "{00000000-0000-0000-0000-000000000010}" & Chr(34) & " /NOINTERACTIVE"
        Shell(myCMDLine, AppWinStyle.Hide)

        lbl_warnnig.Visible = True
        lbl_loading.Visible = False
        Refresh()
        Thread.Sleep(5000)
        Me.Cursor = Cursors.Default
        Close()
    End Sub


End Class

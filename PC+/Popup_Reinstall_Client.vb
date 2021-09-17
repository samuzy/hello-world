
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Reflection
Imports System.Threading 'Thread.Sleep(1000) 'Thread.Sleep(1000)

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'     Ce module est utiliser pour mettre informé l'utilisateur dans les étapes lors des planification de la réinstallation du client SCCM
'     
' *******************************************************************************************************************************************************


Public Class Popup_Reinstall_Client
    Inherits LocalizedForm

    Private Sub Popup_Refresh_Apps_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)
    End Sub

    Private Sub Popup_Reinstall_Client_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Cursor = Cursors.WaitCursor

        Dim CurrentDate As Date = Now
        Dim taskDate As Date
        Dim Step1 As Integer = 0
        Dim Step2 As Integer = 0
        Dim Step3 As Integer = 0

        Dim SYear = DateTime.Now.ToString("yyyy")
        Dim SMonth = DateTime.Now.ToString("MM")
        Dim SDay = DateTime.Now.ToString("dd")
        Dim SHour = DateTime.Now.ToString("HH")
        Dim SMin = DateTime.Now.ToString("mm")

        Try

            Try
                TimeZone(ComputerName)
            Catch ex As Exception

            End Try

            Me.Label1.Visible = True
            Me.Refresh()
            'Arret du service ccmexec.exe
            Service_to_OFF("ccmexec", False)
            Me.pic_done1.Visible = True
            Me.Refresh()

            'Prépare la copie des fichiers à distance selon le site code du PC
            Me.Label2.Visible = True
            Me.Refresh()
            CreateScheduledTaskFile(ComputerName, "CM_Client", True, "CCM")
            Me.pic_done2.Visible = True
            Me.Refresh()

            'Prépare la tâches pour la copie des fichier d'installation du client
            Me.Label3.Visible = True
            Me.Refresh()
            taskDate = CurrentDate.AddHours(TimeZone_PC)
            taskDate = taskDate.AddMinutes(5)
            SHour = taskDate.ToString("HH")
            SMin = taskDate.ToString("mm")

            'planifie la taches
            ScheduleTask(ComputerName, "c:\CM_Client", "CM_Client_remotecache.vbs", SHour + ":" + SMin, (SYear & SMonth & SDay))
            Step_Copie_time = SHour + ":" + SMin
            Step1 = 1
            Me.pic_done3.Visible = True
            Me.Refresh()

            'Prépare la tâches pour la réinstallation du client
            Me.Label5.Visible = True
            Me.Refresh()
            taskDate = CurrentDate.AddHours(TimeZone_PC)
            taskDate = taskDate.AddMinutes(30)
            SHour = taskDate.ToString("HH")
            SMin = taskDate.ToString("mm")

            'planifie la taches
            ScheduleTask(ComputerName, "c:\CM_Client", "CM_Client_remoteinstall.vbs", SHour + ":" + SMin, (SYear & SMonth & SDay))
            Step_Install_time = SHour + ":" + SMin
            Step2 = 1
            Me.pic_done5.Visible = True
            Me.Refresh()

            Me.lbl_warnnig.Visible = True
            Me.lbl_loading.Visible = False
            Me.Refresh()
            Thread.Sleep(3000)
            Me.Close()

            'Heurs de fin prévu
            taskDate = CurrentDate.AddHours(TimeZone_PC)
            taskDate = taskDate.AddHours(1)
            SHour = taskDate.ToString("HH")
            SMin = taskDate.ToString("mm")
            Step_end = SHour + ":" + SMin

        Catch ex As Exception
            If Step1 = 1 Then
                MsgBox(My.Resources.ErrorTaskSchedule & " \\" & ComputerName & "\C$\CM_Client\CM_Client_remotecache.vbs", MsgBoxStyle.Critical, "SCCM PC Admin")
            ElseIf Step2 = 1 Then
                MsgBox(My.Resources.ErrorTaskSchedule & " \\" & ComputerName & "\C$\CM_Client\CM_Client_remoteinstall.vbs", MsgBoxStyle.Critical, "SCCM PC Admin")
            End If

            Me.lbl_loading.Visible = False
            Me.Refresh()
            Thread.Sleep(3000)

            Main.Instance.cmd_Reinstall_client.Enabled = True
            Cursor = Cursors.Arrow
        End Try

        Cursor = Cursors.Default

        Dim Popup_Reinstall_Client_msg As Popup_Reinstall_Client_INFOMSG = New Popup_Reinstall_Client_INFOMSG
        Popup_Reinstall_Client_msg.ShowDialog(Me)

    End Sub
End Class

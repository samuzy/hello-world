
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
'      Ce moduel est seulement pour aviser l'utilisateur quand les étapes serons exécuter sur le poste à distance
'      
' *******************************************************************************************************************************************************


Public Class Popup_Reinstall_Client_INFOMSG
    Inherits LocalizedForm

    Private Sub Popup_Refresh_Apps_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim _Step_Copie_time As Date = Step_Copie_time
        txt_start_copie.Text = _Step_Copie_time.ToString(My.Resources.ServiceWindowTimeFormat)

        Dim _Step_Install_time As Date = Step_Install_time
        txt_Start_Install.Text = _Step_Install_time.ToString(My.Resources.ServiceWindowTimeFormat)

        Dim _Step_end As Date = Step_end
        txt_end.Text = _Step_end.ToString(My.Resources.ServiceWindowTimeFormat)

        Dim _Remote_TimeZone_PC As Date = Remote_TimeZone_PC
        txt_remote_time.Text = _Remote_TimeZone_PC.ToString(My.Resources.ServiceWindowTimeFormat)
    End Sub

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click
        Me.Close()
    End Sub
End Class

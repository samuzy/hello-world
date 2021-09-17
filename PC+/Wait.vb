
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Reflection
Imports System.Threading 'Thread.Sleep(1000)

' *******************************************************************************************************************************************************
'      PC ++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'
'       Permet d'afficher une fenetre d'attente transparent
'       BUG**** en patage d'écran ou sur une vm le transparent est en problème
'
' *******************************************************************************************************************************************************

Public Class Wait
    Inherits LocalizedForm
    Dim waitTime As Integer

    Sub New(Optional waitTime As Integer = 10)
        InitializeComponent()
        Me.waitTime = waitTime
    End Sub

    Private Sub Wait_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Cursor.Current = Cursors.WaitCursor
        
        For i As Integer = waitTime To 1 Step -1
            Label1.Text = CStr(i)
            Me.Refresh()
            Thread.Sleep(1000)
            Me.Refresh()
        Next

        Cursor.Current = Cursors.Default
        Me.Close()
    End Sub
End Class

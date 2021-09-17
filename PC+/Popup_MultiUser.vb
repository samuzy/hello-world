
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
'      Ce Module est utiliser pour afficher les utilisateurs qui son connecter 
'      
' *******************************************************************************************************************************************************


Public Class Popup_MultiUser
    Inherits LocalizedForm

    Private Sub cmd_ok_Click(sender As Object, e As EventArgs) Handles cmd_ok.Click

        Me.Close()

    End Sub

End Class

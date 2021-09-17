'TERMINER NE PLUS TOUCHER (Valider Version 2.6 status OK)

Imports System.Reflection

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'
'       Module de remerciment et d'information sur le produit
'      
' *******************************************************************************************************************************************************

Public NotInheritable Class About
    Inherits LocalizedForm

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub About_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.TextVersion.Text = String.Format(Version.Major & "." & Version.Minor)
        'Me.TextVersion.Text = String.Format(Version.Major & "." & Version.Minor & "." & Version.Build & "." & Version.Revision & " (BETA)")
    End Sub
End Class

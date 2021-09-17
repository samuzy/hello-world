
'TERMINER NE PLUS TOUCHER (Logiciel au stade RC)

Imports System.Reflection

' *******************************************************************************************************************************************************
'      PC++ Créé en Janvier 2015, Mise a jour en 2016 (orienté vers SCCM) et renomée SCCM PC Admin
'      Logiciel qui permet de contrôler et vérifier plusieurs installation et/ou information émanant d'un ordinateur à distance sur le réseau interne
'      Créé par : Hugo Raymond 
'      Nom de l'application: SCCM PC Admin
'      Ce module est  utiliser pour :
'      
'      Ce module est utiliser pour demander au client combien de temp ca fenetre windows est requise 
'      
' *******************************************************************************************************************************************************


Public Class Popup_MW_Time
    Inherits LocalizedForm
    Private Sub Popup_MW_Time_Load(sender As Object, e As EventArgs) Handles Me.Load
        MW_Select = "NULL"
        'Affichage de la version du programme
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        MW_Select = ComboBox1.SelectedItem
        Me.Close()
    End Sub
End Class

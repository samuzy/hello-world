
Imports System.IO
Imports System.Management
Imports System.Reflection
Imports System.Threading 'Thread.Sleep(1000)
Imports Microsoft.Win32
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Data

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

Public Class Adv_Mode_Collection
    Inherits LocalizedForm

    Private Sub Adv_Mode_Collection_Load(sender As Object, e As EventArgs) Handles Me.Load

        'Affichage de la version du programme
        Me.Text = "SCCM PC Admin " & Me.Text & " - (" & ComputerName & ")"
        Dim Version = Assembly.GetExecutingAssembly().GetName().Version
        Me.lbl_Version.Text = String.Format(Me.lbl_Version.Text, Version.Major, Version.Minor, Version.Build, Version.Revision)

        '...
        Me.Cursor = Cursors.WaitCursor

        Show_Collection()

        Me.Cursor = Cursors.Default

        lbl_loading.Visible = False
    End Sub

    Private Sub Show_Collection()

        'cherche le domain associer au SCCM Server et le site code
        Read_INI(UCase(PC_Domain))


        'Vérifie si le poste a été valider avec le nom de domaine (ex:xxxx.cips.communication.gc.ca)

        Dim ComputerName_Short As String
        Dim index As Integer = ComputerName.IndexOf(".")
        If index <> -1 Then
            ComputerName_Short = ComputerName.Substring(0, (index))
        Else
            ComputerName_Short = ComputerName
        End If

        '*************************************************************************************
        'Connection au serveur SQL 
        '************************************************************************************* 
        Try
            Dim sqlConnection1 As New SqlConnection("server=" & INI_SQL_Server & ";database=" & INI_SQL_Database & ";User ID=THMVACC;Password=Passw0rd1")
            Dim cmd As New SqlCommand
            Dim reader As SqlDataReader

            cmd.CommandText = "Select C.CollectionID,C.Name,C.Comment from dbo.v_Collection C join dbo.v_FullCollectionMembership FCM on C.CollectionID = FCM.CollectionID Where FCM.Name = '" & ComputerName_Short & "'"
            cmd.CommandType = CommandType.Text
            cmd.Connection = sqlConnection1

            sqlConnection1.Open()
            reader = cmd.ExecuteReader()
            lstv_Collection.Items.Clear()
            ' lecture des data.....
            While reader.Read
                Me.lstv_Collection.Sorting = Windows.Forms.SortOrder.Ascending
                Dim ls As New ListViewItem(reader.Item("Name").ToString())
                ls.SubItems.Add(reader.Item("CollectionID").ToString())
                lstv_Collection.Items.Add(ls)
            End While

            'ColumnHeader1.Width = -1
            'ColumnHeader2.Width = -1
            lstv_Collection.Refresh()

            sqlConnection1.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
End Class

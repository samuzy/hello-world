Public Class DEBUG
    'Forms qui est utiliser seulement pour la capture des tous les messages qui peux apparaitre dasn les différentes forms 
    'Pour utiliser cet forme vous devez partir l'application et entrz en majuscule le nom de pc suivant : DEBUG




    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox(My.Resources.ErrorTaskSchedule & " BranchCache", MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox(My.Resources.ConfirmRemoveWMINamespace, MsgBoxStyle.Information, "SCCM PC Admin")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox(My.Resources.ErrorUnexpected & " - " & Err.Number & " - " & Err.Description, MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MsgBox(My.Resources.ConfirmGPOUpdate & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.TitleGPUpdate & " : " & ComputerName)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MsgBox(My.Resources.ConfirmComputerName, MsgBoxStyle.Critical)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        MsgBox(My.Resources.ErrorUnreachableComputer, MsgBoxStyle.Critical)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        MsgBox(My.Resources.ErrorRegistryConnection, MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        MsgBox(My.Resources.MsgClipboard, MsgBoxStyle.Information)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        MsgBox(My.Resources.ConfirmReboot & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.MessageReboot & " : " & ComputerName)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        MsgBox(My.Resources.SuccessReboot, MsgBoxStyle.Information)
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        MsgBox(My.Resources.WarningReinstallClient & Chr(13) & Chr(13) & My.Resources.ConfirmAction, MsgBoxStyle.Question + MsgBoxStyle.YesNo, My.Resources.MessageReinstallClient & " : " & ComputerName)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        MsgBox(My.Resources.ConfirmRemoveMW, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        MsgBox(My.Resources.ConfirmComputerName, MsgBoxStyle.Critical)
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        MsgBox(My.Resources.ErrorUnreachableComputer, MsgBoxStyle.Critical)
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        MsgBox(My.Resources.ConfirmRemoveHistory, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        MsgBox(My.Resources.SuccessRemoveHistory, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SCCM PC Admin")
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        MsgBox(My.Resources.ErrorRemoveHistory, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SCCM PC Admin")
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        MsgBox(My.Resources.ConfirmMWBypass & Chr(13) & Chr(13) & My.Resources.ChoiceMWBypassYes & Chr(13) & Chr(13) & My.Resources.ChoiceMWBypassNo, MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel, "SCCM PC Admin")
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        MsgBox(My.Resources.SuccessPlannedInstall, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SCCM PC Admin")
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        MsgBox(My.Resources.ErrorAnalyzeBeforeRetry, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "SCCM PC Admin")
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        MsgBox(My.Resources.MsgClipboard, MsgBoxStyle.Information)
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        MsgBox(My.Resources.ErrorTaskSchedule & " \\" & ComputerName & "\C$\CM_Client\CM_Client_remotecache.vbs", MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        MsgBox(My.Resources.ErrorTaskSchedule & " \\" & ComputerName & "\C$\CM_Client\CM_Client_remoteremoval.vbs", MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        MsgBox(My.Resources.ErrorTaskSchedule & " \\" & ComputerName & "\C$\CM_Client\CM_Client_remoteinstall.vbs", MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        MsgBox(My.Resources.ErrorSiteCode, MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        Dim PName = "******"
        MsgBox(My.Resources.ConfirmStopProcess & PName, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Dim PName = "******"
        MsgBox(My.Resources.ConfirmStopService & PName, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Dim PName = "******"
        MsgBox(My.Resources.ConfirmStartService & PName, MsgBoxStyle.Question + MsgBoxStyle.YesNo, "SCCM PC Admin")
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        Dim strCommand = "*****"
        MsgBox("(" & strCommand & ") failed to run")
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        MsgBox(ComputerName & " (WMI Connection Error) " & Err.Description)
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        MsgBox(ComputerName & " (WMI Error) " & Err.Description)
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        MsgBox(My.Resources.ErrorConnection, MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        MsgBox(My.Resources.ErrorUnexpected & Err.Number, MsgBoxStyle.Critical, "SCCM PC Admin")
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        MsgBox(ComputerName & My.Resources.WarningVPNSlowRequests, MsgBoxStyle.Exclamation, "SCCM PC Admin " & ComputerName)
    End Sub

    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Dim NameService = "*****"
        MsgBox(My.Resources.ErrorStartService, MsgBoxStyle.Critical, "SCCM PC Admin (" + NameService + ")")
    End Sub

    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Dim NameService = "*****"
        MsgBox(My.Resources.ErrorStopService, MsgBoxStyle.Critical, "SCCM PC Admin (" + NameService + ")")
    End Sub

    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        MsgBox(My.Resources.Message_Domain_Missing, MsgBoxStyle.Critical)
    End Sub

    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        MsgBox(My.Resources.Missing_INI_Files, MsgBoxStyle.Critical)
    End Sub
End Class
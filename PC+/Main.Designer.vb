<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.TT = New System.Windows.Forms.ToolTip(Me.components)
        Me.pic_Assitance = New System.Windows.Forms.PictureBox()
        Me.pic_Explorer = New System.Windows.Forms.PictureBox()
        Me.pic_Reboot = New System.Windows.Forms.PictureBox()
        Me.GroupBoxMaintenanceWindow_NEW = New System.Windows.Forms.GroupBox()
        Me.ServiceWindowsListView = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pic_remote = New System.Windows.Forms.PictureBox()
        Me.txt_SiteCode_result_NEW = New System.Windows.Forms.TextBox()
        Me.btnCenterConsole = New System.Windows.Forms.Button()
        Me.btnCenterConsole2 = New System.Windows.Forms.Button()
        Me.ProgramsAndFeaturesSubTab = New System.Windows.Forms.TabControl()
        Me.INSTALLED_SOFTWARE_TAB = New System.Windows.Forms.TabPage()
        Me.ListViewInstalledSoftware_NEW = New System.Windows.Forms.ListView()
        Me.ColumnHeader31 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader32 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader33 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader34 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.JAVA_TAB = New System.Windows.Forms.TabPage()
        Me.ListViewJava_NEW = New System.Windows.Forms.ListView()
        Me.ColumnHeader35 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PROCESS_TAB = New System.Windows.Forms.TabPage()
        Me.ListViewProcess_NEW = New System.Windows.Forms.ListView()
        Me.ColumnHeader36 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader37 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader38 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader39 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader40 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SERVICES_TAB = New System.Windows.Forms.TabPage()
        Me.ListViewServices_NEW = New System.Windows.Forms.ListView()
        Me.ColumnHeader41 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader42 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader43 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader44 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmd_GPO_NEW = New System.Windows.Forms.Button()
        Me.cmd_registry_pol_NEW = New System.Windows.Forms.Button()
        Me.DirectoryEntry1 = New System.DirectoryServices.DirectoryEntry()
        Me.DirectorySearcher1 = New System.DirectoryServices.DirectorySearcher()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip3 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmd_Check_NEW = New System.Windows.Forms.Button()
        Me.lbl_Version = New System.Windows.Forms.Label()
        Me.GroupBoxLogWindow_NEW = New System.Windows.Forms.GroupBox()
        Me.txt_LogWindow = New System.Windows.Forms.TextBox()
        Me.cmd_multi_user = New System.Windows.Forms.Button()
        Me.ProgressBar = New System.Windows.Forms.ProgressBar()
        Me.MainTab = New System.Windows.Forms.TabControl()
        Me.COMPUTER_INFORMATION_TAB = New System.Windows.Forms.TabPage()
        Me.AdvancedModeTab = New System.Windows.Forms.TabControl()
        Me.AdvancedModeTab1 = New System.Windows.Forms.TabPage()
        Me.btnAddMaintWindow_NEW = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblHour = New System.Windows.Forms.Label()
        Me.lblWindowDesiredLength_NEW = New System.Windows.Forms.Label()
        Me.ddlDesiredLength = New System.Windows.Forms.ComboBox()
        Me.lblChange15Minutes_NEW = New System.Windows.Forms.Label()
        Me.cmd_Rebuilding_WMI_NEW = New System.Windows.Forms.Button()
        Me.cmd_BITS_Location_NEW = New System.Windows.Forms.Button()
        Me.cmd_Re_Registering_NEW = New System.Windows.Forms.Button()
        Me.cmd_Client_Logs_NEW = New System.Windows.Forms.Button()
        Me.cmd_DataStore_NEW = New System.Windows.Forms.Button()
        Me.cmd_Del_WMI_NEW = New System.Windows.Forms.Button()
        Me.cmd_WSUS_Download_NEW = New System.Windows.Forms.Button()
        Me.AdvancedModeTab2 = New System.Windows.Forms.TabPage()
        Me.groupBoxAdvMode2_2 = New System.Windows.Forms.GroupBox()
        Me.CheckBox14 = New System.Windows.Forms.CheckBox()
        Me.lbl_CCMVALHOUR_Warning = New System.Windows.Forms.Label()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.TextBox15 = New System.Windows.Forms.TextBox()
        Me.ComboBox6 = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.TextBox14 = New System.Windows.Forms.TextBox()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.ComboBox7 = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.ComboBox5 = New System.Windows.Forms.ComboBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.CheckBox13 = New System.Windows.Forms.CheckBox()
        Me.groupBoxAdvMode2_3 = New System.Windows.Forms.GroupBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.groupBoxAdvMode2_4 = New System.Windows.Forms.GroupBox()
        Me.txt_Description = New System.Windows.Forms.TextBox()
        Me.groupBoxAdvMode2_1 = New System.Windows.Forms.GroupBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.CheckBox10 = New System.Windows.Forms.CheckBox()
        Me.CheckBox11 = New System.Windows.Forms.CheckBox()
        Me.CheckBox12 = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.groupBoxMembership_NEW = New System.Windows.Forms.GroupBox()
        Me.MembershipListView = New System.Windows.Forms.TextBox()
        Me.SCCM_INFORMATION_BOX = New System.Windows.Forms.GroupBox()
        Me.lblSiteCode_NEW = New System.Windows.Forms.Label()
        Me.txt_Client_Version_Result_NEW = New System.Windows.Forms.TextBox()
        Me.txt_WUA_NEW = New System.Windows.Forms.TextBox()
        Me.lbl_Management_Point_NEW = New System.Windows.Forms.Label()
        Me.lbl_WUPoint_NEW = New System.Windows.Forms.Label()
        Me.lblClientVersion_NEW = New System.Windows.Forms.Label()
        Me.lbl_CCM_UPDUSER_NEW = New System.Windows.Forms.Label()
        Me.txt_ManagementPoint_NEW = New System.Windows.Forms.TextBox()
        Me.txt_SCCM_Catalogue_NEW = New System.Windows.Forms.TextBox()
        Me.CompInfoGroupBox = New System.Windows.Forms.GroupBox()
        Me.lbl_Domain_NEW = New System.Windows.Forms.Label()
        Me.txt_Domain_NEW = New System.Windows.Forms.TextBox()
        Me.txt_SRU_Verimg = New System.Windows.Forms.TextBox()
        Me.txt_EquipmentType = New System.Windows.Forms.TextBox()
        Me.lbl_SRUVerimg = New System.Windows.Forms.Label()
        Me.txt_RAM = New System.Windows.Forms.TextBox()
        Me.txt_Name = New System.Windows.Forms.TextBox()
        Me.txt_img_ver_win10_NEW = New System.Windows.Forms.TextBox()
        Me.lbl_Name = New System.Windows.Forms.Label()
        Me.txt_CPU = New System.Windows.Forms.TextBox()
        Me.lbl_EquipmentType_NEW = New System.Windows.Forms.Label()
        Me.txt_ADSite_NEW = New System.Windows.Forms.TextBox()
        Me.lbl_img_ver_win10_NEW = New System.Windows.Forms.Label()
        Me.lbl_ADSite_NEW = New System.Windows.Forms.Label()
        Me.pic_rightArrow = New System.Windows.Forms.PictureBox()
        Me.lbl_Ram = New System.Windows.Forms.Label()
        Me.pic_notOk = New System.Windows.Forms.PictureBox()
        Me.pic_Ok = New System.Windows.Forms.PictureBox()
        Me.txt_img_install_Date = New System.Windows.Forms.TextBox()
        Me.lbl_PCName_NEW = New System.Windows.Forms.Label()
        Me.lbl_CPU = New System.Windows.Forms.Label()
        Me.lbl_UserLoggedIn_NEW = New System.Windows.Forms.Label()
        Me.lblOsInstallDate = New System.Windows.Forms.Label()
        Me.txt_PCName_NEW = New System.Windows.Forms.TextBox()
        Me.txt_Vendor = New System.Windows.Forms.TextBox()
        Me.txtLoggedIn_NEW = New System.Windows.Forms.TextBox()
        Me.lbl_Vendor = New System.Windows.Forms.Label()
        Me.txt_IP_NEW = New System.Windows.Forms.TextBox()
        Me.lbl_IPAddress_NEW = New System.Windows.Forms.Label()
        Me.txt_OSCaption_NEW = New System.Windows.Forms.TextBox()
        Me.lbl_OS_NEW = New System.Windows.Forms.Label()
        Me.txt_last_reboot_NEW = New System.Windows.Forms.TextBox()
        Me.txt_language_NEW = New System.Windows.Forms.TextBox()
        Me.txt_img_ver = New System.Windows.Forms.TextBox()
        Me.lbl_LastRestart_NEW = New System.Windows.Forms.Label()
        Me.lbl_OSLang_NEW = New System.Windows.Forms.Label()
        Me.lblImageVersion = New System.Windows.Forms.Label()
        Me.SCCM_PK_APPS_TAB = New System.Windows.Forms.TabPage()
        Me.btn_apps_refresh = New System.Windows.Forms.Button()
        Me.Tab_pkg_app = New System.Windows.Forms.TabControl()
        Me.START = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pic_arrow = New System.Windows.Forms.PictureBox()
        Me.EXEC_HIST_APPS = New System.Windows.Forms.TabPage()
        Me.listvw_ExecHistApps = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader18 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader19 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader20 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader21 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.EXEC_HIST_PKG_TAB = New System.Windows.Forms.TabPage()
        Me.lstvw_ExecHistPkgs = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.RUNNING_PKGS_TAB = New System.Windows.Forms.TabPage()
        Me.ListView_RunningPackages_NEW = New System.Windows.Forms.ListView()
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ADVERTISEMENTS_TAB = New System.Windows.Forms.TabPage()
        Me.ListView_ProgramsFeatures_NEW = New System.Windows.Forms.ListView()
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SoftwareCacheLocation_Tab = New System.Windows.Forms.TabPage()
        Me.btnESSetupInfo = New System.Windows.Forms.Button()
        Me.ListView_SoftwareLocation_NEW = New System.Windows.Forms.ListView()
        Me.ColumnHeader24 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader23 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SCCM_WSUS_SCUP_TAB = New System.Windows.Forms.TabPage()
        Me.cmd_Refresh_NEW = New System.Windows.Forms.Button()
        Me.cmd_apps_refresh_NEW = New System.Windows.Forms.Button()
        Me.ProgressBar1_NEW = New System.Windows.Forms.ProgressBar()
        Me.lbl_Missing_NEW2 = New System.Windows.Forms.Label()
        Me.lbl_missing_NEW = New System.Windows.Forms.Label()
        Me.lbl_PatchCount_NEW = New System.Windows.Forms.Label()
        Me.lbl_patch_count_NEW = New System.Windows.Forms.Label()
        Me.chk_ApprovedPatch_NEW = New System.Windows.Forms.CheckBox()
        Me.ListViewWSUS_SCUP_NEW = New System.Windows.Forms.ListView()
        Me.ColumnHeader26 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader27 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader28 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader29 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader30 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PROGRAMS_FEATURES_TAB = New System.Windows.Forms.TabPage()
        Me.SCCM_ACTIONS_TAB = New System.Windows.Forms.TabPage()
        Me.Button114 = New System.Windows.Forms.Button()
        Me.lbl_warnnig = New System.Windows.Forms.Label()
        Me.Button0 = New System.Windows.Forms.Button()
        Me.CMD_ALL = New System.Windows.Forms.Button()
        Me.pic_uncheck0 = New System.Windows.Forms.PictureBox()
        Me.Button121 = New System.Windows.Forms.Button()
        Me.pic_done0 = New System.Windows.Forms.PictureBox()
        Me.pic_done121 = New System.Windows.Forms.PictureBox()
        Me.Button111 = New System.Windows.Forms.Button()
        Me.Button32 = New System.Windows.Forms.Button()
        Me.Button40 = New System.Windows.Forms.Button()
        Me.pic_done3 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck32 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck111 = New System.Windows.Forms.PictureBox()
        Me.pic_done10 = New System.Windows.Forms.PictureBox()
        Me.pic_done32 = New System.Windows.Forms.PictureBox()
        Me.pic_done1 = New System.Windows.Forms.PictureBox()
        Me.pic_done111 = New System.Windows.Forms.PictureBox()
        Me.Button42 = New System.Windows.Forms.Button()
        Me.Button113 = New System.Windows.Forms.Button()
        Me.pic_done21 = New System.Windows.Forms.PictureBox()
        Me.Button22 = New System.Windows.Forms.Button()
        Me.pic_done2 = New System.Windows.Forms.PictureBox()
        Me.Button108 = New System.Windows.Forms.Button()
        Me.pic_done31 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck114 = New System.Windows.Forms.PictureBox()
        Me.pic_done108 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck42 = New System.Windows.Forms.PictureBox()
        Me.pic_done113 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck40 = New System.Windows.Forms.PictureBox()
        Me.Button31 = New System.Windows.Forms.Button()
        Me.pic_uncheck22 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck121 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck3 = New System.Windows.Forms.PictureBox()
        Me.Button21 = New System.Windows.Forms.Button()
        Me.pic_done114 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck10 = New System.Windows.Forms.PictureBox()
        Me.pic_done42 = New System.Windows.Forms.PictureBox()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.pic_done40 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck1 = New System.Windows.Forms.PictureBox()
        Me.pic_done22 = New System.Windows.Forms.PictureBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.pic_uncheck21 = New System.Windows.Forms.PictureBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.pic_uncheck2 = New System.Windows.Forms.PictureBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.pic_uncheck31 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck108 = New System.Windows.Forms.PictureBox()
        Me.pic_uncheck113 = New System.Windows.Forms.PictureBox()
        Me.REPAIR_CLEANING_TAB = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DISPLAY_MAINTENANCE_WINDOWS_TAB = New System.Windows.Forms.TabPage()
        Me.cmd_Add_SW = New System.Windows.Forms.Button()
        Me.cmd_Show_SW = New System.Windows.Forms.Button()
        Me.RUN_COMMAND_TAB = New System.Windows.Forms.TabPage()
        Me.lblRunCmdMsg = New System.Windows.Forms.Label()
        Me.cmd_Reinstall_client_NEW = New System.Windows.Forms.Button()
        Me.btnClearCommandWindow = New System.Windows.Forms.Button()
        Me.txtCommandOutput = New System.Windows.Forms.TextBox()
        Me.btnCommandInput = New System.Windows.Forms.Button()
        Me.txtCommandInput = New System.Windows.Forms.TextBox()
        Me.ADVANCE_MODE_TAB_1 = New System.Windows.Forms.TabPage()
        Me.Gr666 = New System.Windows.Forms.GroupBox()
        Me.cmd_load_Logs5_NEW = New System.Windows.Forms.Button()
        Me.lblWinUpdAgentWin10Serv_NEW = New System.Windows.Forms.Label()
        Me.cmb_Logs5_NEW = New System.Windows.Forms.ComboBox()
        Me.cmd_load_Logs4_NEW = New System.Windows.Forms.Button()
        Me.lblOSAndSoftwareUpdate_NEW = New System.Windows.Forms.Label()
        Me.cmb_Logs4_NEW = New System.Windows.Forms.ComboBox()
        Me.cmd_load_Logs3_NEW = New System.Windows.Forms.Button()
        Me.lblAppManagement_NEW = New System.Windows.Forms.Label()
        Me.cmb_Logs3_NEW = New System.Windows.Forms.ComboBox()
        Me.cmd_load_Logs2_NEW = New System.Windows.Forms.Button()
        Me.lblClientInstall_NEW = New System.Windows.Forms.Label()
        Me.cmb_Logs2_NEW = New System.Windows.Forms.ComboBox()
        Me.txt_Description_NEW = New System.Windows.Forms.TextBox()
        Me.cmd_load_Logs1_NEW = New System.Windows.Forms.Button()
        Me.lbl_description_NEW = New System.Windows.Forms.Label()
        Me.lbl_logs_NEW = New System.Windows.Forms.Label()
        Me.cmb_Logs1_NEW = New System.Windows.Forms.ComboBox()
        Me.groupBoxAdvancedMode_NEW = New System.Windows.Forms.GroupBox()
        Me.cmd_Port_8009_NEW = New System.Windows.Forms.Button()
        Me.pic_redflag2_NEW = New System.Windows.Forms.PictureBox()
        Me.txt_ListenPort_NEW = New System.Windows.Forms.TextBox()
        Me.txt_MacAddress_NEW = New System.Windows.Forms.TextBox()
        Me.txt_ConnectPort_NEW = New System.Windows.Forms.TextBox()
        Me.pic_greenflag2_NEW = New System.Windows.Forms.PictureBox()
        Me.txt_cache_location_NEW = New System.Windows.Forms.TextBox()
        Me.lblMacAddress_NEW = New System.Windows.Forms.Label()
        Me.lblTCPIP_NEW = New System.Windows.Forms.Label()
        Me.lblCacheLocation_NEW = New System.Windows.Forms.Label()
        Me.lblListenPort_NEW = New System.Windows.Forms.Label()
        Me.lblComputerName_NEW = New System.Windows.Forms.Label()
        Me.lblConnectPort_NEW = New System.Windows.Forms.Label()
        Me.pic_redflag1_NEW = New System.Windows.Forms.PictureBox()
        Me.txt_TCPIP_NEW = New System.Windows.Forms.TextBox()
        Me.pic_greenflag1_NEW = New System.Windows.Forms.PictureBox()
        Me.txt_Cache_Size_NEW = New System.Windows.Forms.TextBox()
        Me.txt_ComputerName_NEW = New System.Windows.Forms.TextBox()
        Me.lblCacheSize_NEW = New System.Windows.Forms.Label()
        Me.lbl_abr_size = New System.Windows.Forms.Label()
        Me.ADVANCE_MODE_TAB_4 = New System.Windows.Forms.TabPage()
        Me.lstv_Collection = New System.Windows.Forms.ListView()
        Me.ColumnHeader25 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader45 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lbl_loading = New System.Windows.Forms.Label()
        Me.cmd_pc_info = New System.Windows.Forms.Button()
        Me.cmd_Reinstall_client = New System.Windows.Forms.Button()
        Me.cmd_Force_Apps_update = New System.Windows.Forms.Button()
        Me.cmd_Force_WSUS = New System.Windows.Forms.Button()
        Me.cmd_pkg_apps = New System.Windows.Forms.Button()
        Me.cmd_SCCM_WSUS_SCUP_Approved = New System.Windows.Forms.Button()
        Me.cmd_Clear_cache_bits = New System.Windows.Forms.Button()
        Me.cmd_SCCM_Action = New System.Windows.Forms.Button()
        Me.cmdSoftware = New System.Windows.Forms.Button()
        Me.pic_reboot_status = New System.Windows.Forms.PictureBox()
        Me.txt_reboot_status = New System.Windows.Forms.Label()
        Me.Pic_OFF_wuauserv = New System.Windows.Forms.PictureBox()
        Me.Pic_ON_wuauserv = New System.Windows.Forms.PictureBox()
        Me.Pic_OFF_PeerDistSvc = New System.Windows.Forms.PictureBox()
        Me.Pic_ON_PeerDistSvc = New System.Windows.Forms.PictureBox()
        Me.Pic_OFF_BITS = New System.Windows.Forms.PictureBox()
        Me.Pic_ON_BITS = New System.Windows.Forms.PictureBox()
        Me.Pic_OFF_CCMEXEC = New System.Windows.Forms.PictureBox()
        Me.pic_OFF_RemoteRegistry = New System.Windows.Forms.PictureBox()
        Me.Pic_ON_CCMEXEC = New System.Windows.Forms.PictureBox()
        Me.Pic_OFF_MPSSVC = New System.Windows.Forms.PictureBox()
        Me.Pic_ON_MPSSVC = New System.Windows.Forms.PictureBox()
        Me.pic_ON_RemoteRegistry = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblRegedit1 = New System.Windows.Forms.Label()
        Me.lblWindowsFirewallMPSSVC = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.Menu_About = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserGuideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Option = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_Francais = New System.Windows.Forms.ToolStripMenuItem()
        Me.Menu_English = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdvancedMode_Menu = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GCProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GCProfileLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GCProfilePCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GCProfileUserToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.EventViewerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CMToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ServicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WorkstationManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.FORCESECURITYUPDATEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FORCEAPPLICATIONUPDATEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.REINSTALLSCCMCLIENTToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.REBOOTREMOTECOMPUTERToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.REMOTEASSISTANCEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EXPLORERToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.REMOTEDESKTOPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LangToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ENToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UserGuideToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.pic_Assitance, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_Explorer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_Reboot, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxMaintenanceWindow_NEW.SuspendLayout
        CType(Me.pic_remote, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ProgramsAndFeaturesSubTab.SuspendLayout
        Me.INSTALLED_SOFTWARE_TAB.SuspendLayout
        Me.JAVA_TAB.SuspendLayout
        Me.PROCESS_TAB.SuspendLayout
        Me.SERVICES_TAB.SuspendLayout
        Me.GroupBoxLogWindow_NEW.SuspendLayout
        Me.MainTab.SuspendLayout
        Me.COMPUTER_INFORMATION_TAB.SuspendLayout
        Me.AdvancedModeTab.SuspendLayout
        Me.AdvancedModeTab1.SuspendLayout
        Me.Panel3.SuspendLayout
        Me.AdvancedModeTab2.SuspendLayout
        Me.groupBoxAdvMode2_2.SuspendLayout
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBoxAdvMode2_3.SuspendLayout
        Me.groupBoxAdvMode2_4.SuspendLayout
        Me.groupBoxAdvMode2_1.SuspendLayout
        Me.Panel1.SuspendLayout
        Me.groupBoxMembership_NEW.SuspendLayout
        Me.SCCM_INFORMATION_BOX.SuspendLayout
        Me.CompInfoGroupBox.SuspendLayout
        CType(Me.pic_rightArrow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_notOk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_Ok, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCCM_PK_APPS_TAB.SuspendLayout
        Me.Tab_pkg_app.SuspendLayout
        Me.START.SuspendLayout
        CType(Me.pic_arrow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.EXEC_HIST_APPS.SuspendLayout
        Me.EXEC_HIST_PKG_TAB.SuspendLayout
        Me.RUNNING_PKGS_TAB.SuspendLayout
        Me.ADVERTISEMENTS_TAB.SuspendLayout
        Me.SoftwareCacheLocation_Tab.SuspendLayout
        Me.SCCM_WSUS_SCUP_TAB.SuspendLayout
        Me.PROGRAMS_FEATURES_TAB.SuspendLayout
        Me.SCCM_ACTIONS_TAB.SuspendLayout
        CType(Me.pic_uncheck0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done0, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done121, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done32, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done111, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck114, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done108, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done113, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck121, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done114, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done42, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done40, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_done22, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck21, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck31, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck108, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_uncheck113, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.REPAIR_CLEANING_TAB.SuspendLayout
        Me.Panel2.SuspendLayout
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DISPLAY_MAINTENANCE_WINDOWS_TAB.SuspendLayout
        Me.RUN_COMMAND_TAB.SuspendLayout
        Me.ADVANCE_MODE_TAB_1.SuspendLayout
        Me.Gr666.SuspendLayout
        Me.groupBoxAdvancedMode_NEW.SuspendLayout
        CType(Me.pic_redflag2_NEW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_greenflag2_NEW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_redflag1_NEW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_greenflag1_NEW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ADVANCE_MODE_TAB_4.SuspendLayout
        CType(Me.pic_reboot_status, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_OFF_wuauserv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_ON_wuauserv, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_OFF_PeerDistSvc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_ON_PeerDistSvc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_OFF_BITS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_ON_BITS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_OFF_CCMEXEC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_OFF_RemoteRegistry, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_ON_CCMEXEC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_OFF_MPSSVC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic_ON_MPSSVC, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic_ON_RemoteRegistry, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout
        Me.MenuStrip2.SuspendLayout
        Me.SuspendLayout
        '
        'TT
        '
        Me.TT.AutoPopDelay = 5000
        Me.TT.InitialDelay = 500
        Me.TT.IsBalloon = True
        Me.TT.ReshowDelay = 5
        '
        'pic_Assitance
        '
        Me.pic_Assitance.BackColor = System.Drawing.Color.Transparent
        Me.pic_Assitance.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pic_Assitance, "pic_Assitance")
        Me.pic_Assitance.Name = "pic_Assitance"
        Me.pic_Assitance.TabStop = False
        Me.TT.SetToolTip(Me.pic_Assitance, resources.GetString("pic_Assitance.ToolTip"))
        '
        'pic_Explorer
        '
        Me.pic_Explorer.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pic_Explorer, "pic_Explorer")
        Me.pic_Explorer.Name = "pic_Explorer"
        Me.pic_Explorer.TabStop = False
        Me.TT.SetToolTip(Me.pic_Explorer, resources.GetString("pic_Explorer.ToolTip"))
        '
        'pic_Reboot
        '
        Me.pic_Reboot.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pic_Reboot, "pic_Reboot")
        Me.pic_Reboot.Name = "pic_Reboot"
        Me.pic_Reboot.TabStop = False
        Me.TT.SetToolTip(Me.pic_Reboot, resources.GetString("pic_Reboot.ToolTip"))
        '
        'GroupBoxMaintenanceWindow_NEW
        '
        resources.ApplyResources(Me.GroupBoxMaintenanceWindow_NEW, "GroupBoxMaintenanceWindow_NEW")
        Me.GroupBoxMaintenanceWindow_NEW.Controls.Add(Me.ServiceWindowsListView)
        Me.GroupBoxMaintenanceWindow_NEW.Name = "GroupBoxMaintenanceWindow_NEW"
        Me.GroupBoxMaintenanceWindow_NEW.TabStop = False
        Me.TT.SetToolTip(Me.GroupBoxMaintenanceWindow_NEW, resources.GetString("GroupBoxMaintenanceWindow_NEW.ToolTip"))
        '
        'ServiceWindowsListView
        '
        Me.ServiceWindowsListView.BackColor = System.Drawing.Color.AliceBlue
        Me.ServiceWindowsListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ServiceWindowsListView.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.ServiceWindowsListView, "ServiceWindowsListView")
        Me.ServiceWindowsListView.ForeColor = System.Drawing.Color.MidnightBlue
        Me.ServiceWindowsListView.FullRowSelect = True
        Me.ServiceWindowsListView.GridLines = True
        Me.ServiceWindowsListView.HideSelection = False
        Me.ServiceWindowsListView.Name = "ServiceWindowsListView"
        Me.ServiceWindowsListView.TabStop = False
        Me.ServiceWindowsListView.UseCompatibleStateImageBehavior = False
        Me.ServiceWindowsListView.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Tag = "ColumnHeader1"
        resources.ApplyResources(Me.ColumnHeader1, "ColumnHeader1")
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Tag = "ColumnHeader2"
        resources.ApplyResources(Me.ColumnHeader2, "ColumnHeader2")
        '
        'pic_remote
        '
        Me.pic_remote.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.pic_remote, "pic_remote")
        Me.pic_remote.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pic_remote.Name = "pic_remote"
        Me.pic_remote.TabStop = False
        Me.TT.SetToolTip(Me.pic_remote, resources.GetString("pic_remote.ToolTip"))
        '
        'txt_SiteCode_result_NEW
        '
        Me.txt_SiteCode_result_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_SiteCode_result_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_SiteCode_result_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_SiteCode_result_NEW, "txt_SiteCode_result_NEW")
        Me.txt_SiteCode_result_NEW.Name = "txt_SiteCode_result_NEW"
        Me.txt_SiteCode_result_NEW.ReadOnly = True
        Me.TT.SetToolTip(Me.txt_SiteCode_result_NEW, resources.GetString("txt_SiteCode_result_NEW.ToolTip"))
        '
        'btnCenterConsole
        '
        Me.btnCenterConsole.BackColor = System.Drawing.Color.SteelBlue
        resources.ApplyResources(Me.btnCenterConsole, "btnCenterConsole")
        Me.btnCenterConsole.Name = "btnCenterConsole"
        Me.TT.SetToolTip(Me.btnCenterConsole, resources.GetString("btnCenterConsole.ToolTip"))
        Me.btnCenterConsole.UseVisualStyleBackColor = False
        '
        'btnCenterConsole2
        '
        Me.btnCenterConsole2.BackColor = System.Drawing.Color.SteelBlue
        resources.ApplyResources(Me.btnCenterConsole2, "btnCenterConsole2")
        Me.btnCenterConsole2.Name = "btnCenterConsole2"
        Me.TT.SetToolTip(Me.btnCenterConsole2, resources.GetString("btnCenterConsole2.ToolTip"))
        Me.btnCenterConsole2.UseVisualStyleBackColor = False
        '
        'ProgramsAndFeaturesSubTab
        '
        Me.ProgramsAndFeaturesSubTab.Controls.Add(Me.INSTALLED_SOFTWARE_TAB)
        Me.ProgramsAndFeaturesSubTab.Controls.Add(Me.JAVA_TAB)
        Me.ProgramsAndFeaturesSubTab.Controls.Add(Me.PROCESS_TAB)
        Me.ProgramsAndFeaturesSubTab.Controls.Add(Me.SERVICES_TAB)
        resources.ApplyResources(Me.ProgramsAndFeaturesSubTab, "ProgramsAndFeaturesSubTab")
        Me.ProgramsAndFeaturesSubTab.Name = "ProgramsAndFeaturesSubTab"
        Me.ProgramsAndFeaturesSubTab.SelectedIndex = 0
        Me.TT.SetToolTip(Me.ProgramsAndFeaturesSubTab, resources.GetString("ProgramsAndFeaturesSubTab.ToolTip"))
        '
        'INSTALLED_SOFTWARE_TAB
        '
        Me.INSTALLED_SOFTWARE_TAB.Controls.Add(Me.ListViewInstalledSoftware_NEW)
        resources.ApplyResources(Me.INSTALLED_SOFTWARE_TAB, "INSTALLED_SOFTWARE_TAB")
        Me.INSTALLED_SOFTWARE_TAB.Name = "INSTALLED_SOFTWARE_TAB"
        Me.INSTALLED_SOFTWARE_TAB.UseVisualStyleBackColor = True
        '
        'ListViewInstalledSoftware_NEW
        '
        Me.ListViewInstalledSoftware_NEW.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader31, Me.ColumnHeader32, Me.ColumnHeader33, Me.ColumnHeader34})
        resources.ApplyResources(Me.ListViewInstalledSoftware_NEW, "ListViewInstalledSoftware_NEW")
        Me.ListViewInstalledSoftware_NEW.FullRowSelect = True
        Me.ListViewInstalledSoftware_NEW.GridLines = True
        Me.ListViewInstalledSoftware_NEW.HideSelection = False
        Me.ListViewInstalledSoftware_NEW.MultiSelect = False
        Me.ListViewInstalledSoftware_NEW.Name = "ListViewInstalledSoftware_NEW"
        Me.ListViewInstalledSoftware_NEW.UseCompatibleStateImageBehavior = False
        Me.ListViewInstalledSoftware_NEW.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader31
        '
        resources.ApplyResources(Me.ColumnHeader31, "ColumnHeader31")
        '
        'ColumnHeader32
        '
        resources.ApplyResources(Me.ColumnHeader32, "ColumnHeader32")
        '
        'ColumnHeader33
        '
        resources.ApplyResources(Me.ColumnHeader33, "ColumnHeader33")
        '
        'ColumnHeader34
        '
        resources.ApplyResources(Me.ColumnHeader34, "ColumnHeader34")
        '
        'JAVA_TAB
        '
        Me.JAVA_TAB.Controls.Add(Me.ListViewJava_NEW)
        resources.ApplyResources(Me.JAVA_TAB, "JAVA_TAB")
        Me.JAVA_TAB.Name = "JAVA_TAB"
        Me.JAVA_TAB.UseVisualStyleBackColor = True
        '
        'ListViewJava_NEW
        '
        Me.ListViewJava_NEW.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader35})
        resources.ApplyResources(Me.ListViewJava_NEW, "ListViewJava_NEW")
        Me.ListViewJava_NEW.FullRowSelect = True
        Me.ListViewJava_NEW.GridLines = True
        Me.ListViewJava_NEW.HideSelection = False
        Me.ListViewJava_NEW.MultiSelect = False
        Me.ListViewJava_NEW.Name = "ListViewJava_NEW"
        Me.ListViewJava_NEW.UseCompatibleStateImageBehavior = False
        Me.ListViewJava_NEW.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader35
        '
        resources.ApplyResources(Me.ColumnHeader35, "ColumnHeader35")
        '
        'PROCESS_TAB
        '
        Me.PROCESS_TAB.Controls.Add(Me.ListViewProcess_NEW)
        resources.ApplyResources(Me.PROCESS_TAB, "PROCESS_TAB")
        Me.PROCESS_TAB.Name = "PROCESS_TAB"
        Me.PROCESS_TAB.UseVisualStyleBackColor = True
        '
        'ListViewProcess_NEW
        '
        Me.ListViewProcess_NEW.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader36, Me.ColumnHeader37, Me.ColumnHeader38, Me.ColumnHeader39, Me.ColumnHeader40})
        resources.ApplyResources(Me.ListViewProcess_NEW, "ListViewProcess_NEW")
        Me.ListViewProcess_NEW.FullRowSelect = True
        Me.ListViewProcess_NEW.GridLines = True
        Me.ListViewProcess_NEW.HideSelection = False
        Me.ListViewProcess_NEW.MultiSelect = False
        Me.ListViewProcess_NEW.Name = "ListViewProcess_NEW"
        Me.ListViewProcess_NEW.UseCompatibleStateImageBehavior = False
        Me.ListViewProcess_NEW.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader36
        '
        resources.ApplyResources(Me.ColumnHeader36, "ColumnHeader36")
        '
        'ColumnHeader37
        '
        resources.ApplyResources(Me.ColumnHeader37, "ColumnHeader37")
        '
        'ColumnHeader38
        '
        resources.ApplyResources(Me.ColumnHeader38, "ColumnHeader38")
        '
        'ColumnHeader39
        '
        resources.ApplyResources(Me.ColumnHeader39, "ColumnHeader39")
        '
        'ColumnHeader40
        '
        resources.ApplyResources(Me.ColumnHeader40, "ColumnHeader40")
        '
        'SERVICES_TAB
        '
        Me.SERVICES_TAB.Controls.Add(Me.ListViewServices_NEW)
        resources.ApplyResources(Me.SERVICES_TAB, "SERVICES_TAB")
        Me.SERVICES_TAB.Name = "SERVICES_TAB"
        Me.SERVICES_TAB.UseVisualStyleBackColor = True
        '
        'ListViewServices_NEW
        '
        Me.ListViewServices_NEW.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader41, Me.ColumnHeader42, Me.ColumnHeader43, Me.ColumnHeader44})
        resources.ApplyResources(Me.ListViewServices_NEW, "ListViewServices_NEW")
        Me.ListViewServices_NEW.FullRowSelect = True
        Me.ListViewServices_NEW.GridLines = True
        Me.ListViewServices_NEW.HideSelection = False
        Me.ListViewServices_NEW.MultiSelect = False
        Me.ListViewServices_NEW.Name = "ListViewServices_NEW"
        Me.ListViewServices_NEW.UseCompatibleStateImageBehavior = False
        Me.ListViewServices_NEW.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader41
        '
        resources.ApplyResources(Me.ColumnHeader41, "ColumnHeader41")
        '
        'ColumnHeader42
        '
        resources.ApplyResources(Me.ColumnHeader42, "ColumnHeader42")
        '
        'ColumnHeader43
        '
        resources.ApplyResources(Me.ColumnHeader43, "ColumnHeader43")
        '
        'ColumnHeader44
        '
        resources.ApplyResources(Me.ColumnHeader44, "ColumnHeader44")
        '
        'cmd_GPO_NEW
        '
        resources.ApplyResources(Me.cmd_GPO_NEW, "cmd_GPO_NEW")
        Me.cmd_GPO_NEW.Name = "cmd_GPO_NEW"
        Me.ToolTip1.SetToolTip(Me.cmd_GPO_NEW, resources.GetString("cmd_GPO_NEW.ToolTip"))
        Me.TT.SetToolTip(Me.cmd_GPO_NEW, resources.GetString("cmd_GPO_NEW.ToolTip1"))
        Me.cmd_GPO_NEW.UseVisualStyleBackColor = True
        '
        'cmd_registry_pol_NEW
        '
        resources.ApplyResources(Me.cmd_registry_pol_NEW, "cmd_registry_pol_NEW")
        Me.cmd_registry_pol_NEW.Name = "cmd_registry_pol_NEW"
        Me.cmd_registry_pol_NEW.UseVisualStyleBackColor = True
        '
        'DirectorySearcher1
        '
        Me.DirectorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01")
        Me.DirectorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01")
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 5000
        Me.ToolTip1.InitialDelay = 500
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ReshowDelay = 5
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'ToolTip2
        '
        Me.ToolTip2.AutoPopDelay = 5000
        Me.ToolTip2.InitialDelay = 500
        Me.ToolTip2.IsBalloon = True
        Me.ToolTip2.ReshowDelay = 5
        Me.ToolTip2.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'ToolTip3
        '
        Me.ToolTip3.AutoPopDelay = 5000
        Me.ToolTip3.InitialDelay = 500
        Me.ToolTip3.IsBalloon = True
        Me.ToolTip3.ReshowDelay = 5
        Me.ToolTip3.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'cmd_Check_NEW
        '
        resources.ApplyResources(Me.cmd_Check_NEW, "cmd_Check_NEW")
        Me.cmd_Check_NEW.Name = "cmd_Check_NEW"
        Me.cmd_Check_NEW.UseVisualStyleBackColor = True
        '
        'lbl_Version
        '
        resources.ApplyResources(Me.lbl_Version, "lbl_Version")
        Me.lbl_Version.Name = "lbl_Version"
        '
        'GroupBoxLogWindow_NEW
        '
        Me.GroupBoxLogWindow_NEW.Controls.Add(Me.txt_LogWindow)
        resources.ApplyResources(Me.GroupBoxLogWindow_NEW, "GroupBoxLogWindow_NEW")
        Me.GroupBoxLogWindow_NEW.Name = "GroupBoxLogWindow_NEW"
        Me.GroupBoxLogWindow_NEW.TabStop = False
        '
        'txt_LogWindow
        '
        resources.ApplyResources(Me.txt_LogWindow, "txt_LogWindow")
        Me.txt_LogWindow.Name = "txt_LogWindow"
        '
        'cmd_multi_user
        '
        resources.ApplyResources(Me.cmd_multi_user, "cmd_multi_user")
        Me.cmd_multi_user.ForeColor = System.Drawing.Color.DarkRed
        Me.cmd_multi_user.Name = "cmd_multi_user"
        Me.cmd_multi_user.UseVisualStyleBackColor = True
        '
        'ProgressBar
        '
        resources.ApplyResources(Me.ProgressBar, "ProgressBar")
        Me.ProgressBar.Name = "ProgressBar"
        '
        'MainTab
        '
        Me.MainTab.Controls.Add(Me.COMPUTER_INFORMATION_TAB)
        Me.MainTab.Controls.Add(Me.SCCM_PK_APPS_TAB)
        Me.MainTab.Controls.Add(Me.SCCM_WSUS_SCUP_TAB)
        Me.MainTab.Controls.Add(Me.PROGRAMS_FEATURES_TAB)
        Me.MainTab.Controls.Add(Me.SCCM_ACTIONS_TAB)
        Me.MainTab.Controls.Add(Me.REPAIR_CLEANING_TAB)
        Me.MainTab.Controls.Add(Me.DISPLAY_MAINTENANCE_WINDOWS_TAB)
        Me.MainTab.Controls.Add(Me.RUN_COMMAND_TAB)
        Me.MainTab.Controls.Add(Me.ADVANCE_MODE_TAB_1)
        Me.MainTab.Controls.Add(Me.ADVANCE_MODE_TAB_4)
        resources.ApplyResources(Me.MainTab, "MainTab")
        Me.MainTab.Name = "MainTab"
        Me.MainTab.SelectedIndex = 0
        '
        'COMPUTER_INFORMATION_TAB
        '
        Me.COMPUTER_INFORMATION_TAB.Controls.Add(Me.AdvancedModeTab)
        Me.COMPUTER_INFORMATION_TAB.Controls.Add(Me.Panel1)
        resources.ApplyResources(Me.COMPUTER_INFORMATION_TAB, "COMPUTER_INFORMATION_TAB")
        Me.COMPUTER_INFORMATION_TAB.Name = "COMPUTER_INFORMATION_TAB"
        Me.COMPUTER_INFORMATION_TAB.UseVisualStyleBackColor = True
        '
        'AdvancedModeTab
        '
        Me.AdvancedModeTab.Controls.Add(Me.AdvancedModeTab1)
        Me.AdvancedModeTab.Controls.Add(Me.AdvancedModeTab2)
        resources.ApplyResources(Me.AdvancedModeTab, "AdvancedModeTab")
        Me.AdvancedModeTab.Name = "AdvancedModeTab"
        Me.AdvancedModeTab.SelectedIndex = 0
        '
        'AdvancedModeTab1
        '
        Me.AdvancedModeTab1.Controls.Add(Me.btnAddMaintWindow_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_registry_pol_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.Panel3)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_GPO_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_Rebuilding_WMI_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_BITS_Location_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_Re_Registering_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_Client_Logs_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_DataStore_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_Del_WMI_NEW)
        Me.AdvancedModeTab1.Controls.Add(Me.cmd_WSUS_Download_NEW)
        resources.ApplyResources(Me.AdvancedModeTab1, "AdvancedModeTab1")
        Me.AdvancedModeTab1.Name = "AdvancedModeTab1"
        Me.AdvancedModeTab1.UseVisualStyleBackColor = True
        '
        'btnAddMaintWindow_NEW
        '
        resources.ApplyResources(Me.btnAddMaintWindow_NEW, "btnAddMaintWindow_NEW")
        Me.btnAddMaintWindow_NEW.Name = "btnAddMaintWindow_NEW"
        Me.btnAddMaintWindow_NEW.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.White
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.lblHour)
        Me.Panel3.Controls.Add(Me.lblWindowDesiredLength_NEW)
        Me.Panel3.Controls.Add(Me.ddlDesiredLength)
        Me.Panel3.Controls.Add(Me.lblChange15Minutes_NEW)
        resources.ApplyResources(Me.Panel3, "Panel3")
        Me.Panel3.Name = "Panel3"
        '
        'lblHour
        '
        resources.ApplyResources(Me.lblHour, "lblHour")
        Me.lblHour.Name = "lblHour"
        '
        'lblWindowDesiredLength_NEW
        '
        resources.ApplyResources(Me.lblWindowDesiredLength_NEW, "lblWindowDesiredLength_NEW")
        Me.lblWindowDesiredLength_NEW.Name = "lblWindowDesiredLength_NEW"
        '
        'ddlDesiredLength
        '
        Me.ddlDesiredLength.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.ddlDesiredLength, "ddlDesiredLength")
        Me.ddlDesiredLength.FormattingEnabled = True
        Me.ddlDesiredLength.Items.AddRange(New Object() {resources.GetString("ddlDesiredLength.Items"), resources.GetString("ddlDesiredLength.Items1"), resources.GetString("ddlDesiredLength.Items2"), resources.GetString("ddlDesiredLength.Items3"), resources.GetString("ddlDesiredLength.Items4"), resources.GetString("ddlDesiredLength.Items5"), resources.GetString("ddlDesiredLength.Items6"), resources.GetString("ddlDesiredLength.Items7")})
        Me.ddlDesiredLength.Name = "ddlDesiredLength"
        '
        'lblChange15Minutes_NEW
        '
        resources.ApplyResources(Me.lblChange15Minutes_NEW, "lblChange15Minutes_NEW")
        Me.lblChange15Minutes_NEW.Name = "lblChange15Minutes_NEW"
        '
        'cmd_Rebuilding_WMI_NEW
        '
        resources.ApplyResources(Me.cmd_Rebuilding_WMI_NEW, "cmd_Rebuilding_WMI_NEW")
        Me.cmd_Rebuilding_WMI_NEW.Name = "cmd_Rebuilding_WMI_NEW"
        Me.cmd_Rebuilding_WMI_NEW.UseVisualStyleBackColor = True
        '
        'cmd_BITS_Location_NEW
        '
        resources.ApplyResources(Me.cmd_BITS_Location_NEW, "cmd_BITS_Location_NEW")
        Me.cmd_BITS_Location_NEW.Name = "cmd_BITS_Location_NEW"
        Me.cmd_BITS_Location_NEW.UseVisualStyleBackColor = True
        '
        'cmd_Re_Registering_NEW
        '
        resources.ApplyResources(Me.cmd_Re_Registering_NEW, "cmd_Re_Registering_NEW")
        Me.cmd_Re_Registering_NEW.Name = "cmd_Re_Registering_NEW"
        Me.cmd_Re_Registering_NEW.UseVisualStyleBackColor = True
        '
        'cmd_Client_Logs_NEW
        '
        resources.ApplyResources(Me.cmd_Client_Logs_NEW, "cmd_Client_Logs_NEW")
        Me.cmd_Client_Logs_NEW.Name = "cmd_Client_Logs_NEW"
        Me.cmd_Client_Logs_NEW.UseVisualStyleBackColor = True
        '
        'cmd_DataStore_NEW
        '
        resources.ApplyResources(Me.cmd_DataStore_NEW, "cmd_DataStore_NEW")
        Me.cmd_DataStore_NEW.Name = "cmd_DataStore_NEW"
        Me.cmd_DataStore_NEW.UseVisualStyleBackColor = True
        '
        'cmd_Del_WMI_NEW
        '
        resources.ApplyResources(Me.cmd_Del_WMI_NEW, "cmd_Del_WMI_NEW")
        Me.cmd_Del_WMI_NEW.Name = "cmd_Del_WMI_NEW"
        Me.cmd_Del_WMI_NEW.UseVisualStyleBackColor = True
        '
        'cmd_WSUS_Download_NEW
        '
        resources.ApplyResources(Me.cmd_WSUS_Download_NEW, "cmd_WSUS_Download_NEW")
        Me.cmd_WSUS_Download_NEW.Name = "cmd_WSUS_Download_NEW"
        Me.cmd_WSUS_Download_NEW.UseVisualStyleBackColor = True
        '
        'AdvancedModeTab2
        '
        Me.AdvancedModeTab2.Controls.Add(Me.groupBoxAdvMode2_2)
        Me.AdvancedModeTab2.Controls.Add(Me.groupBoxAdvMode2_3)
        Me.AdvancedModeTab2.Controls.Add(Me.groupBoxAdvMode2_4)
        Me.AdvancedModeTab2.Controls.Add(Me.groupBoxAdvMode2_1)
        resources.ApplyResources(Me.AdvancedModeTab2, "AdvancedModeTab2")
        Me.AdvancedModeTab2.Name = "AdvancedModeTab2"
        Me.AdvancedModeTab2.UseVisualStyleBackColor = True
        '
        'groupBoxAdvMode2_2
        '
        Me.groupBoxAdvMode2_2.Controls.Add(Me.CheckBox14)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.lbl_CCMVALHOUR_Warning)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.PictureBox5)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox15)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.ComboBox6)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label18)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label31)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox14)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox7)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label19)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label32)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.ComboBox7)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label20)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox13)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label21)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.ComboBox5)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label22)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox12)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label23)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox11)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label24)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.ComboBox4)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label25)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox10)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label26)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox9)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label27)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox8)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label28)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.TextBox6)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label29)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.ComboBox3)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.Label30)
        Me.groupBoxAdvMode2_2.Controls.Add(Me.CheckBox13)
        resources.ApplyResources(Me.groupBoxAdvMode2_2, "groupBoxAdvMode2_2")
        Me.groupBoxAdvMode2_2.Name = "groupBoxAdvMode2_2"
        Me.groupBoxAdvMode2_2.TabStop = False
        '
        'CheckBox14
        '
        resources.ApplyResources(Me.CheckBox14, "CheckBox14")
        Me.CheckBox14.Name = "CheckBox14"
        Me.CheckBox14.UseVisualStyleBackColor = True
        '
        'lbl_CCMVALHOUR_Warning
        '
        resources.ApplyResources(Me.lbl_CCMVALHOUR_Warning, "lbl_CCMVALHOUR_Warning")
        Me.lbl_CCMVALHOUR_Warning.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_CCMVALHOUR_Warning.Name = "lbl_CCMVALHOUR_Warning"
        '
        'PictureBox5
        '
        resources.ApplyResources(Me.PictureBox5, "PictureBox5")
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.TabStop = False
        '
        'TextBox15
        '
        Me.TextBox15.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox15, "TextBox15")
        Me.TextBox15.Name = "TextBox15"
        '
        'ComboBox6
        '
        Me.ComboBox6.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.ComboBox6, "ComboBox6")
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Items.AddRange(New Object() {resources.GetString("ComboBox6.Items"), resources.GetString("ComboBox6.Items1"), resources.GetString("ComboBox6.Items2")})
        Me.ComboBox6.Name = "ComboBox6"
        '
        'Label18
        '
        resources.ApplyResources(Me.Label18, "Label18")
        Me.Label18.Name = "Label18"
        '
        'Label31
        '
        resources.ApplyResources(Me.Label31, "Label31")
        Me.Label31.Name = "Label31"
        '
        'TextBox14
        '
        Me.TextBox14.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox14, "TextBox14")
        Me.TextBox14.Name = "TextBox14"
        '
        'TextBox7
        '
        Me.TextBox7.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox7, "TextBox7")
        Me.TextBox7.Name = "TextBox7"
        '
        'Label19
        '
        resources.ApplyResources(Me.Label19, "Label19")
        Me.Label19.Name = "Label19"
        '
        'Label32
        '
        resources.ApplyResources(Me.Label32, "Label32")
        Me.Label32.Name = "Label32"
        '
        'ComboBox7
        '
        Me.ComboBox7.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.ComboBox7, "ComboBox7")
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Items.AddRange(New Object() {resources.GetString("ComboBox7.Items"), resources.GetString("ComboBox7.Items1"), resources.GetString("ComboBox7.Items2")})
        Me.ComboBox7.Name = "ComboBox7"
        '
        'Label20
        '
        resources.ApplyResources(Me.Label20, "Label20")
        Me.Label20.Name = "Label20"
        '
        'TextBox13
        '
        Me.TextBox13.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox13, "TextBox13")
        Me.TextBox13.Name = "TextBox13"
        '
        'Label21
        '
        resources.ApplyResources(Me.Label21, "Label21")
        Me.Label21.Name = "Label21"
        '
        'ComboBox5
        '
        Me.ComboBox5.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.ComboBox5, "ComboBox5")
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Items.AddRange(New Object() {resources.GetString("ComboBox5.Items"), resources.GetString("ComboBox5.Items1"), resources.GetString("ComboBox5.Items2")})
        Me.ComboBox5.Name = "ComboBox5"
        '
        'Label22
        '
        resources.ApplyResources(Me.Label22, "Label22")
        Me.Label22.Name = "Label22"
        '
        'TextBox12
        '
        Me.TextBox12.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox12, "TextBox12")
        Me.TextBox12.Name = "TextBox12"
        '
        'Label23
        '
        resources.ApplyResources(Me.Label23, "Label23")
        Me.Label23.Name = "Label23"
        '
        'TextBox11
        '
        Me.TextBox11.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox11, "TextBox11")
        Me.TextBox11.Name = "TextBox11"
        '
        'Label24
        '
        resources.ApplyResources(Me.Label24, "Label24")
        Me.Label24.Name = "Label24"
        '
        'ComboBox4
        '
        Me.ComboBox4.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.ComboBox4, "ComboBox4")
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Items.AddRange(New Object() {resources.GetString("ComboBox4.Items"), resources.GetString("ComboBox4.Items1"), resources.GetString("ComboBox4.Items2"), resources.GetString("ComboBox4.Items3"), resources.GetString("ComboBox4.Items4")})
        Me.ComboBox4.Name = "ComboBox4"
        '
        'Label25
        '
        resources.ApplyResources(Me.Label25, "Label25")
        Me.Label25.Name = "Label25"
        '
        'TextBox10
        '
        Me.TextBox10.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox10, "TextBox10")
        Me.TextBox10.Name = "TextBox10"
        '
        'Label26
        '
        resources.ApplyResources(Me.Label26, "Label26")
        Me.Label26.Name = "Label26"
        '
        'TextBox9
        '
        Me.TextBox9.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox9, "TextBox9")
        Me.TextBox9.Name = "TextBox9"
        '
        'Label27
        '
        resources.ApplyResources(Me.Label27, "Label27")
        Me.Label27.Name = "Label27"
        '
        'TextBox8
        '
        Me.TextBox8.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox8, "TextBox8")
        Me.TextBox8.Name = "TextBox8"
        '
        'Label28
        '
        resources.ApplyResources(Me.Label28, "Label28")
        Me.Label28.Name = "Label28"
        '
        'TextBox6
        '
        Me.TextBox6.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox6, "TextBox6")
        Me.TextBox6.Name = "TextBox6"
        '
        'Label29
        '
        resources.ApplyResources(Me.Label29, "Label29")
        Me.Label29.Name = "Label29"
        '
        'ComboBox3
        '
        Me.ComboBox3.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.ComboBox3, "ComboBox3")
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {resources.GetString("ComboBox3.Items"), resources.GetString("ComboBox3.Items1"), resources.GetString("ComboBox3.Items2")})
        Me.ComboBox3.Name = "ComboBox3"
        '
        'Label30
        '
        resources.ApplyResources(Me.Label30, "Label30")
        Me.Label30.Name = "Label30"
        '
        'CheckBox13
        '
        resources.ApplyResources(Me.CheckBox13, "CheckBox13")
        Me.CheckBox13.Name = "CheckBox13"
        Me.CheckBox13.UseVisualStyleBackColor = True
        '
        'groupBoxAdvMode2_3
        '
        Me.groupBoxAdvMode2_3.Controls.Add(Me.TextBox3)
        Me.groupBoxAdvMode2_3.Controls.Add(Me.Label17)
        resources.ApplyResources(Me.groupBoxAdvMode2_3, "groupBoxAdvMode2_3")
        Me.groupBoxAdvMode2_3.Name = "groupBoxAdvMode2_3"
        Me.groupBoxAdvMode2_3.TabStop = False
        '
        'TextBox3
        '
        Me.TextBox3.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox3, "TextBox3")
        Me.TextBox3.Name = "TextBox3"
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.Name = "Label17"
        '
        'groupBoxAdvMode2_4
        '
        Me.groupBoxAdvMode2_4.Controls.Add(Me.txt_Description)
        resources.ApplyResources(Me.groupBoxAdvMode2_4, "groupBoxAdvMode2_4")
        Me.groupBoxAdvMode2_4.Name = "groupBoxAdvMode2_4"
        Me.groupBoxAdvMode2_4.TabStop = False
        '
        'txt_Description
        '
        Me.txt_Description.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.txt_Description, "txt_Description")
        Me.txt_Description.Name = "txt_Description"
        '
        'groupBoxAdvMode2_1
        '
        Me.groupBoxAdvMode2_1.Controls.Add(Me.ComboBox2)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.Label13)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.CheckBox8)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.TextBox2)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.Label14)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.ComboBox1)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.CheckBox7)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.CheckBox9)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.CheckBox10)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.CheckBox11)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.CheckBox12)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.Label15)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.TextBox1)
        Me.groupBoxAdvMode2_1.Controls.Add(Me.Label16)
        resources.ApplyResources(Me.groupBoxAdvMode2_1, "groupBoxAdvMode2_1")
        Me.groupBoxAdvMode2_1.Name = "groupBoxAdvMode2_1"
        Me.groupBoxAdvMode2_1.TabStop = False
        '
        'ComboBox2
        '
        Me.ComboBox2.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.ComboBox2, "ComboBox2")
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {resources.GetString("ComboBox2.Items"), resources.GetString("ComboBox2.Items1"), resources.GetString("ComboBox2.Items2"), resources.GetString("ComboBox2.Items3"), resources.GetString("ComboBox2.Items4"), resources.GetString("ComboBox2.Items5"), resources.GetString("ComboBox2.Items6"), resources.GetString("ComboBox2.Items7"), resources.GetString("ComboBox2.Items8"), resources.GetString("ComboBox2.Items9"), resources.GetString("ComboBox2.Items10"), resources.GetString("ComboBox2.Items11")})
        Me.ComboBox2.Name = "ComboBox2"
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.Name = "Label13"
        '
        'CheckBox8
        '
        resources.ApplyResources(Me.CheckBox8, "CheckBox8")
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox2, "TextBox2")
        Me.TextBox2.Name = "TextBox2"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.ComboBox1, "ComboBox1")
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {resources.GetString("ComboBox1.Items"), resources.GetString("ComboBox1.Items1"), resources.GetString("ComboBox1.Items2"), resources.GetString("ComboBox1.Items3"), resources.GetString("ComboBox1.Items4")})
        Me.ComboBox1.Name = "ComboBox1"
        '
        'CheckBox7
        '
        resources.ApplyResources(Me.CheckBox7, "CheckBox7")
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'CheckBox9
        '
        resources.ApplyResources(Me.CheckBox9, "CheckBox9")
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.UseVisualStyleBackColor = True
        '
        'CheckBox10
        '
        resources.ApplyResources(Me.CheckBox10, "CheckBox10")
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.UseVisualStyleBackColor = True
        '
        'CheckBox11
        '
        resources.ApplyResources(Me.CheckBox11, "CheckBox11")
        Me.CheckBox11.Name = "CheckBox11"
        Me.CheckBox11.UseVisualStyleBackColor = True
        '
        'CheckBox12
        '
        resources.ApplyResources(Me.CheckBox12, "CheckBox12")
        Me.CheckBox12.Name = "CheckBox12"
        Me.CheckBox12.UseVisualStyleBackColor = False
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.Name = "Label15"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.TextBox1, "TextBox1")
        Me.TextBox1.Name = "TextBox1"
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.groupBoxMembership_NEW)
        Me.Panel1.Controls.Add(Me.SCCM_INFORMATION_BOX)
        Me.Panel1.Controls.Add(Me.CompInfoGroupBox)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'groupBoxMembership_NEW
        '
        resources.ApplyResources(Me.groupBoxMembership_NEW, "groupBoxMembership_NEW")
        Me.groupBoxMembership_NEW.CausesValidation = False
        Me.groupBoxMembership_NEW.Controls.Add(Me.MembershipListView)
        Me.groupBoxMembership_NEW.Name = "groupBoxMembership_NEW"
        Me.groupBoxMembership_NEW.TabStop = False
        '
        'MembershipListView
        '
        Me.MembershipListView.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.MembershipListView, "MembershipListView")
        Me.MembershipListView.Name = "MembershipListView"
        '
        'SCCM_INFORMATION_BOX
        '
        Me.SCCM_INFORMATION_BOX.BackColor = System.Drawing.Color.AliceBlue
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.lblSiteCode_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.txt_Client_Version_Result_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.txt_SiteCode_result_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.txt_WUA_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.lbl_Management_Point_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.lbl_WUPoint_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.lblClientVersion_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.lbl_CCM_UPDUSER_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.txt_ManagementPoint_NEW)
        Me.SCCM_INFORMATION_BOX.Controls.Add(Me.txt_SCCM_Catalogue_NEW)
        resources.ApplyResources(Me.SCCM_INFORMATION_BOX, "SCCM_INFORMATION_BOX")
        Me.SCCM_INFORMATION_BOX.Name = "SCCM_INFORMATION_BOX"
        Me.SCCM_INFORMATION_BOX.TabStop = False
        '
        'lblSiteCode_NEW
        '
        resources.ApplyResources(Me.lblSiteCode_NEW, "lblSiteCode_NEW")
        Me.lblSiteCode_NEW.Name = "lblSiteCode_NEW"
        '
        'txt_Client_Version_Result_NEW
        '
        Me.txt_Client_Version_Result_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_Client_Version_Result_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_Client_Version_Result_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_Client_Version_Result_NEW, "txt_Client_Version_Result_NEW")
        Me.txt_Client_Version_Result_NEW.Name = "txt_Client_Version_Result_NEW"
        Me.txt_Client_Version_Result_NEW.ReadOnly = True
        '
        'txt_WUA_NEW
        '
        Me.txt_WUA_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_WUA_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_WUA_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_WUA_NEW, "txt_WUA_NEW")
        Me.txt_WUA_NEW.Name = "txt_WUA_NEW"
        Me.txt_WUA_NEW.ReadOnly = True
        '
        'lbl_Management_Point_NEW
        '
        resources.ApplyResources(Me.lbl_Management_Point_NEW, "lbl_Management_Point_NEW")
        Me.lbl_Management_Point_NEW.Name = "lbl_Management_Point_NEW"
        '
        'lbl_WUPoint_NEW
        '
        resources.ApplyResources(Me.lbl_WUPoint_NEW, "lbl_WUPoint_NEW")
        Me.lbl_WUPoint_NEW.Name = "lbl_WUPoint_NEW"
        '
        'lblClientVersion_NEW
        '
        resources.ApplyResources(Me.lblClientVersion_NEW, "lblClientVersion_NEW")
        Me.lblClientVersion_NEW.Name = "lblClientVersion_NEW"
        '
        'lbl_CCM_UPDUSER_NEW
        '
        resources.ApplyResources(Me.lbl_CCM_UPDUSER_NEW, "lbl_CCM_UPDUSER_NEW")
        Me.lbl_CCM_UPDUSER_NEW.Name = "lbl_CCM_UPDUSER_NEW"
        '
        'txt_ManagementPoint_NEW
        '
        Me.txt_ManagementPoint_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_ManagementPoint_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_ManagementPoint_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_ManagementPoint_NEW, "txt_ManagementPoint_NEW")
        Me.txt_ManagementPoint_NEW.Name = "txt_ManagementPoint_NEW"
        Me.txt_ManagementPoint_NEW.ReadOnly = True
        '
        'txt_SCCM_Catalogue_NEW
        '
        Me.txt_SCCM_Catalogue_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_SCCM_Catalogue_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_SCCM_Catalogue_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_SCCM_Catalogue_NEW, "txt_SCCM_Catalogue_NEW")
        Me.txt_SCCM_Catalogue_NEW.Name = "txt_SCCM_Catalogue_NEW"
        Me.txt_SCCM_Catalogue_NEW.ReadOnly = True
        '
        'CompInfoGroupBox
        '
        Me.CompInfoGroupBox.BackColor = System.Drawing.Color.WhiteSmoke
        resources.ApplyResources(Me.CompInfoGroupBox, "CompInfoGroupBox")
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_Domain_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_Domain_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_SRU_Verimg)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_EquipmentType)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_SRUVerimg)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_RAM)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_Name)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_img_ver_win10_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_Name)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_CPU)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_EquipmentType_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_ADSite_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_img_ver_win10_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_ADSite_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.pic_rightArrow)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_Ram)
        Me.CompInfoGroupBox.Controls.Add(Me.pic_notOk)
        Me.CompInfoGroupBox.Controls.Add(Me.pic_Ok)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_img_install_Date)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_PCName_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_CPU)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_UserLoggedIn_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lblOsInstallDate)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_PCName_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_Vendor)
        Me.CompInfoGroupBox.Controls.Add(Me.txtLoggedIn_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_Vendor)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_IP_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_IPAddress_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.cmd_Check_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_OSCaption_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_OS_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_last_reboot_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_language_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.txt_img_ver)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_LastRestart_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lbl_OSLang_NEW)
        Me.CompInfoGroupBox.Controls.Add(Me.lblImageVersion)
        Me.CompInfoGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CompInfoGroupBox.Name = "CompInfoGroupBox"
        Me.CompInfoGroupBox.TabStop = False
        '
        'lbl_Domain_NEW
        '
        resources.ApplyResources(Me.lbl_Domain_NEW, "lbl_Domain_NEW")
        Me.lbl_Domain_NEW.Name = "lbl_Domain_NEW"
        '
        'txt_Domain_NEW
        '
        Me.txt_Domain_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_Domain_NEW, "txt_Domain_NEW")
        Me.txt_Domain_NEW.Name = "txt_Domain_NEW"
        '
        'txt_SRU_Verimg
        '
        Me.txt_SRU_Verimg.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_SRU_Verimg.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_SRU_Verimg.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_SRU_Verimg, "txt_SRU_Verimg")
        Me.txt_SRU_Verimg.Name = "txt_SRU_Verimg"
        Me.txt_SRU_Verimg.ReadOnly = True
        '
        'txt_EquipmentType
        '
        Me.txt_EquipmentType.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_EquipmentType.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_EquipmentType.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_EquipmentType, "txt_EquipmentType")
        Me.txt_EquipmentType.Name = "txt_EquipmentType"
        Me.txt_EquipmentType.ReadOnly = True
        '
        'lbl_SRUVerimg
        '
        resources.ApplyResources(Me.lbl_SRUVerimg, "lbl_SRUVerimg")
        Me.lbl_SRUVerimg.Name = "lbl_SRUVerimg"
        '
        'txt_RAM
        '
        resources.ApplyResources(Me.txt_RAM, "txt_RAM")
        Me.txt_RAM.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_RAM.Name = "txt_RAM"
        '
        'txt_Name
        '
        Me.txt_Name.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_Name, "txt_Name")
        Me.txt_Name.Name = "txt_Name"
        '
        'txt_img_ver_win10_NEW
        '
        resources.ApplyResources(Me.txt_img_ver_win10_NEW, "txt_img_ver_win10_NEW")
        Me.txt_img_ver_win10_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_img_ver_win10_NEW.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Me.txt_img_ver_win10_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_img_ver_win10_NEW.Name = "txt_img_ver_win10_NEW"
        Me.txt_img_ver_win10_NEW.ReadOnly = True
        Me.txt_img_ver_win10_NEW.UseWaitCursor = True
        '
        'lbl_Name
        '
        resources.ApplyResources(Me.lbl_Name, "lbl_Name")
        Me.lbl_Name.Name = "lbl_Name"
        '
        'txt_CPU
        '
        resources.ApplyResources(Me.txt_CPU, "txt_CPU")
        Me.txt_CPU.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_CPU.Name = "txt_CPU"
        '
        'lbl_EquipmentType_NEW
        '
        resources.ApplyResources(Me.lbl_EquipmentType_NEW, "lbl_EquipmentType_NEW")
        Me.lbl_EquipmentType_NEW.Name = "lbl_EquipmentType_NEW"
        '
        'txt_ADSite_NEW
        '
        Me.txt_ADSite_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_ADSite_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_ADSite_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_ADSite_NEW, "txt_ADSite_NEW")
        Me.txt_ADSite_NEW.Name = "txt_ADSite_NEW"
        Me.txt_ADSite_NEW.ReadOnly = True
        '
        'lbl_img_ver_win10_NEW
        '
        resources.ApplyResources(Me.lbl_img_ver_win10_NEW, "lbl_img_ver_win10_NEW")
        Me.lbl_img_ver_win10_NEW.Name = "lbl_img_ver_win10_NEW"
        Me.lbl_img_ver_win10_NEW.UseWaitCursor = True
        '
        'lbl_ADSite_NEW
        '
        resources.ApplyResources(Me.lbl_ADSite_NEW, "lbl_ADSite_NEW")
        Me.lbl_ADSite_NEW.Name = "lbl_ADSite_NEW"
        '
        'pic_rightArrow
        '
        resources.ApplyResources(Me.pic_rightArrow, "pic_rightArrow")
        Me.pic_rightArrow.Name = "pic_rightArrow"
        Me.pic_rightArrow.TabStop = False
        '
        'lbl_Ram
        '
        resources.ApplyResources(Me.lbl_Ram, "lbl_Ram")
        Me.lbl_Ram.Name = "lbl_Ram"
        '
        'pic_notOk
        '
        resources.ApplyResources(Me.pic_notOk, "pic_notOk")
        Me.pic_notOk.Name = "pic_notOk"
        Me.pic_notOk.TabStop = False
        '
        'pic_Ok
        '
        Me.pic_Ok.Image = Global.PC_.My.Resources.Resources.pic_Ok_mage
        resources.ApplyResources(Me.pic_Ok, "pic_Ok")
        Me.pic_Ok.Name = "pic_Ok"
        Me.pic_Ok.TabStop = False
        '
        'txt_img_install_Date
        '
        Me.txt_img_install_Date.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_img_install_Date.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_img_install_Date, "txt_img_install_Date")
        Me.txt_img_install_Date.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_img_install_Date.Name = "txt_img_install_Date"
        Me.txt_img_install_Date.ReadOnly = True
        '
        'lbl_PCName_NEW
        '
        resources.ApplyResources(Me.lbl_PCName_NEW, "lbl_PCName_NEW")
        Me.lbl_PCName_NEW.Name = "lbl_PCName_NEW"
        '
        'lbl_CPU
        '
        resources.ApplyResources(Me.lbl_CPU, "lbl_CPU")
        Me.lbl_CPU.Name = "lbl_CPU"
        '
        'lbl_UserLoggedIn_NEW
        '
        resources.ApplyResources(Me.lbl_UserLoggedIn_NEW, "lbl_UserLoggedIn_NEW")
        Me.lbl_UserLoggedIn_NEW.BackColor = System.Drawing.Color.Transparent
        Me.lbl_UserLoggedIn_NEW.Name = "lbl_UserLoggedIn_NEW"
        '
        'lblOsInstallDate
        '
        resources.ApplyResources(Me.lblOsInstallDate, "lblOsInstallDate")
        Me.lblOsInstallDate.Name = "lblOsInstallDate"
        '
        'txt_PCName_NEW
        '
        Me.txt_PCName_NEW.BackColor = System.Drawing.Color.GhostWhite
        resources.ApplyResources(Me.txt_PCName_NEW, "txt_PCName_NEW")
        Me.txt_PCName_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_PCName_NEW.Name = "txt_PCName_NEW"
        '
        'txt_Vendor
        '
        Me.txt_Vendor.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_Vendor, "txt_Vendor")
        Me.txt_Vendor.Name = "txt_Vendor"
        '
        'txtLoggedIn_NEW
        '
        Me.txtLoggedIn_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txtLoggedIn_NEW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txtLoggedIn_NEW, "txtLoggedIn_NEW")
        Me.txtLoggedIn_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txtLoggedIn_NEW.Name = "txtLoggedIn_NEW"
        Me.txtLoggedIn_NEW.ReadOnly = True
        '
        'lbl_Vendor
        '
        resources.ApplyResources(Me.lbl_Vendor, "lbl_Vendor")
        Me.lbl_Vendor.Name = "lbl_Vendor"
        '
        'txt_IP_NEW
        '
        Me.txt_IP_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_IP_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_IP_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_IP_NEW, "txt_IP_NEW")
        Me.txt_IP_NEW.Name = "txt_IP_NEW"
        Me.txt_IP_NEW.ReadOnly = True
        '
        'lbl_IPAddress_NEW
        '
        resources.ApplyResources(Me.lbl_IPAddress_NEW, "lbl_IPAddress_NEW")
        Me.lbl_IPAddress_NEW.Name = "lbl_IPAddress_NEW"
        '
        'txt_OSCaption_NEW
        '
        Me.txt_OSCaption_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_OSCaption_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_OSCaption_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_OSCaption_NEW, "txt_OSCaption_NEW")
        Me.txt_OSCaption_NEW.Name = "txt_OSCaption_NEW"
        Me.txt_OSCaption_NEW.ReadOnly = True
        '
        'lbl_OS_NEW
        '
        resources.ApplyResources(Me.lbl_OS_NEW, "lbl_OS_NEW")
        Me.lbl_OS_NEW.Name = "lbl_OS_NEW"
        '
        'txt_last_reboot_NEW
        '
        Me.txt_last_reboot_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_last_reboot_NEW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_last_reboot_NEW, "txt_last_reboot_NEW")
        Me.txt_last_reboot_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_last_reboot_NEW.Name = "txt_last_reboot_NEW"
        Me.txt_last_reboot_NEW.ReadOnly = True
        '
        'txt_language_NEW
        '
        Me.txt_language_NEW.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_language_NEW.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_language_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        resources.ApplyResources(Me.txt_language_NEW, "txt_language_NEW")
        Me.txt_language_NEW.Name = "txt_language_NEW"
        Me.txt_language_NEW.ReadOnly = True
        '
        'txt_img_ver
        '
        resources.ApplyResources(Me.txt_img_ver, "txt_img_ver")
        Me.txt_img_ver.BackColor = System.Drawing.Color.GhostWhite
        Me.txt_img_ver.Cursor = System.Windows.Forms.Cursors.Default
        Me.txt_img_ver.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_img_ver.Name = "txt_img_ver"
        Me.txt_img_ver.ReadOnly = True
        '
        'lbl_LastRestart_NEW
        '
        resources.ApplyResources(Me.lbl_LastRestart_NEW, "lbl_LastRestart_NEW")
        Me.lbl_LastRestart_NEW.Name = "lbl_LastRestart_NEW"
        '
        'lbl_OSLang_NEW
        '
        resources.ApplyResources(Me.lbl_OSLang_NEW, "lbl_OSLang_NEW")
        Me.lbl_OSLang_NEW.Name = "lbl_OSLang_NEW"
        '
        'lblImageVersion
        '
        resources.ApplyResources(Me.lblImageVersion, "lblImageVersion")
        Me.lblImageVersion.Name = "lblImageVersion"
        '
        'SCCM_PK_APPS_TAB
        '
        Me.SCCM_PK_APPS_TAB.Controls.Add(Me.btn_apps_refresh)
        Me.SCCM_PK_APPS_TAB.Controls.Add(Me.Tab_pkg_app)
        resources.ApplyResources(Me.SCCM_PK_APPS_TAB, "SCCM_PK_APPS_TAB")
        Me.SCCM_PK_APPS_TAB.Name = "SCCM_PK_APPS_TAB"
        Me.SCCM_PK_APPS_TAB.UseVisualStyleBackColor = True
        '
        'btn_apps_refresh
        '
        resources.ApplyResources(Me.btn_apps_refresh, "btn_apps_refresh")
        Me.btn_apps_refresh.Name = "btn_apps_refresh"
        Me.btn_apps_refresh.UseVisualStyleBackColor = True
        '
        'Tab_pkg_app
        '
        Me.Tab_pkg_app.Controls.Add(Me.START)
        Me.Tab_pkg_app.Controls.Add(Me.EXEC_HIST_APPS)
        Me.Tab_pkg_app.Controls.Add(Me.EXEC_HIST_PKG_TAB)
        Me.Tab_pkg_app.Controls.Add(Me.RUNNING_PKGS_TAB)
        Me.Tab_pkg_app.Controls.Add(Me.ADVERTISEMENTS_TAB)
        Me.Tab_pkg_app.Controls.Add(Me.SoftwareCacheLocation_Tab)
        resources.ApplyResources(Me.Tab_pkg_app, "Tab_pkg_app")
        Me.Tab_pkg_app.Name = "Tab_pkg_app"
        Me.Tab_pkg_app.SelectedIndex = 0
        '
        'START
        '
        Me.START.Controls.Add(Me.Label2)
        Me.START.Controls.Add(Me.pic_arrow)
        resources.ApplyResources(Me.START, "START")
        Me.START.Name = "START"
        Me.START.UseVisualStyleBackColor = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'pic_arrow
        '
        resources.ApplyResources(Me.pic_arrow, "pic_arrow")
        Me.pic_arrow.Name = "pic_arrow"
        Me.pic_arrow.TabStop = False
        '
        'EXEC_HIST_APPS
        '
        resources.ApplyResources(Me.EXEC_HIST_APPS, "EXEC_HIST_APPS")
        Me.EXEC_HIST_APPS.Controls.Add(Me.listvw_ExecHistApps)
        Me.EXEC_HIST_APPS.Name = "EXEC_HIST_APPS"
        Me.EXEC_HIST_APPS.UseVisualStyleBackColor = True
        '
        'listvw_ExecHistApps
        '
        Me.listvw_ExecHistApps.AllowColumnReorder = True
        Me.listvw_ExecHistApps.AutoArrange = False
        Me.listvw_ExecHistApps.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader17, Me.ColumnHeader18, Me.ColumnHeader19, Me.ColumnHeader20, Me.ColumnHeader21})
        Me.listvw_ExecHistApps.FullRowSelect = True
        Me.listvw_ExecHistApps.GridLines = True
        Me.listvw_ExecHistApps.HideSelection = False
        resources.ApplyResources(Me.listvw_ExecHistApps, "listvw_ExecHistApps")
        Me.listvw_ExecHistApps.Name = "listvw_ExecHistApps"
        Me.listvw_ExecHistApps.UseCompatibleStateImageBehavior = False
        Me.listvw_ExecHistApps.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        resources.ApplyResources(Me.ColumnHeader5, "ColumnHeader5")
        '
        'ColumnHeader6
        '
        resources.ApplyResources(Me.ColumnHeader6, "ColumnHeader6")
        '
        'ColumnHeader17
        '
        resources.ApplyResources(Me.ColumnHeader17, "ColumnHeader17")
        '
        'ColumnHeader18
        '
        resources.ApplyResources(Me.ColumnHeader18, "ColumnHeader18")
        '
        'ColumnHeader19
        '
        resources.ApplyResources(Me.ColumnHeader19, "ColumnHeader19")
        '
        'ColumnHeader20
        '
        resources.ApplyResources(Me.ColumnHeader20, "ColumnHeader20")
        '
        'ColumnHeader21
        '
        resources.ApplyResources(Me.ColumnHeader21, "ColumnHeader21")
        '
        'EXEC_HIST_PKG_TAB
        '
        Me.EXEC_HIST_PKG_TAB.Controls.Add(Me.lstvw_ExecHistPkgs)
        resources.ApplyResources(Me.EXEC_HIST_PKG_TAB, "EXEC_HIST_PKG_TAB")
        Me.EXEC_HIST_PKG_TAB.Name = "EXEC_HIST_PKG_TAB"
        Me.EXEC_HIST_PKG_TAB.UseVisualStyleBackColor = True
        '
        'lstvw_ExecHistPkgs
        '
        Me.lstvw_ExecHistPkgs.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9})
        resources.ApplyResources(Me.lstvw_ExecHistPkgs, "lstvw_ExecHistPkgs")
        Me.lstvw_ExecHistPkgs.FullRowSelect = True
        Me.lstvw_ExecHistPkgs.GridLines = True
        Me.lstvw_ExecHistPkgs.HideSelection = False
        Me.lstvw_ExecHistPkgs.MultiSelect = False
        Me.lstvw_ExecHistPkgs.Name = "lstvw_ExecHistPkgs"
        Me.lstvw_ExecHistPkgs.UseCompatibleStateImageBehavior = False
        Me.lstvw_ExecHistPkgs.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        resources.ApplyResources(Me.ColumnHeader4, "ColumnHeader4")
        '
        'ColumnHeader7
        '
        resources.ApplyResources(Me.ColumnHeader7, "ColumnHeader7")
        '
        'ColumnHeader8
        '
        resources.ApplyResources(Me.ColumnHeader8, "ColumnHeader8")
        '
        'ColumnHeader9
        '
        resources.ApplyResources(Me.ColumnHeader9, "ColumnHeader9")
        '
        'RUNNING_PKGS_TAB
        '
        Me.RUNNING_PKGS_TAB.Controls.Add(Me.ListView_RunningPackages_NEW)
        resources.ApplyResources(Me.RUNNING_PKGS_TAB, "RUNNING_PKGS_TAB")
        Me.RUNNING_PKGS_TAB.Name = "RUNNING_PKGS_TAB"
        Me.RUNNING_PKGS_TAB.UseVisualStyleBackColor = True
        '
        'ListView_RunningPackages_NEW
        '
        Me.ListView_RunningPackages_NEW.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader10, Me.ColumnHeader14, Me.ColumnHeader15, Me.ColumnHeader16})
        resources.ApplyResources(Me.ListView_RunningPackages_NEW, "ListView_RunningPackages_NEW")
        Me.ListView_RunningPackages_NEW.FullRowSelect = True
        Me.ListView_RunningPackages_NEW.GridLines = True
        Me.ListView_RunningPackages_NEW.HideSelection = False
        Me.ListView_RunningPackages_NEW.MultiSelect = False
        Me.ListView_RunningPackages_NEW.Name = "ListView_RunningPackages_NEW"
        Me.ListView_RunningPackages_NEW.UseCompatibleStateImageBehavior = False
        Me.ListView_RunningPackages_NEW.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader10
        '
        resources.ApplyResources(Me.ColumnHeader10, "ColumnHeader10")
        '
        'ColumnHeader14
        '
        resources.ApplyResources(Me.ColumnHeader14, "ColumnHeader14")
        '
        'ColumnHeader15
        '
        resources.ApplyResources(Me.ColumnHeader15, "ColumnHeader15")
        '
        'ColumnHeader16
        '
        resources.ApplyResources(Me.ColumnHeader16, "ColumnHeader16")
        '
        'ADVERTISEMENTS_TAB
        '
        Me.ADVERTISEMENTS_TAB.Controls.Add(Me.ListView_ProgramsFeatures_NEW)
        resources.ApplyResources(Me.ADVERTISEMENTS_TAB, "ADVERTISEMENTS_TAB")
        Me.ADVERTISEMENTS_TAB.Name = "ADVERTISEMENTS_TAB"
        Me.ADVERTISEMENTS_TAB.UseVisualStyleBackColor = True
        '
        'ListView_ProgramsFeatures_NEW
        '
        Me.ListView_ProgramsFeatures_NEW.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader11, Me.ColumnHeader12, Me.ColumnHeader13})
        resources.ApplyResources(Me.ListView_ProgramsFeatures_NEW, "ListView_ProgramsFeatures_NEW")
        Me.ListView_ProgramsFeatures_NEW.FullRowSelect = True
        Me.ListView_ProgramsFeatures_NEW.GridLines = True
        Me.ListView_ProgramsFeatures_NEW.HideSelection = False
        Me.ListView_ProgramsFeatures_NEW.MultiSelect = False
        Me.ListView_ProgramsFeatures_NEW.Name = "ListView_ProgramsFeatures_NEW"
        Me.ListView_ProgramsFeatures_NEW.UseCompatibleStateImageBehavior = False
        Me.ListView_ProgramsFeatures_NEW.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader11
        '
        resources.ApplyResources(Me.ColumnHeader11, "ColumnHeader11")
        '
        'ColumnHeader12
        '
        resources.ApplyResources(Me.ColumnHeader12, "ColumnHeader12")
        '
        'ColumnHeader13
        '
        resources.ApplyResources(Me.ColumnHeader13, "ColumnHeader13")
        '
        'SoftwareCacheLocation_Tab
        '
        Me.SoftwareCacheLocation_Tab.Controls.Add(Me.btnESSetupInfo)
        Me.SoftwareCacheLocation_Tab.Controls.Add(Me.ListView_SoftwareLocation_NEW)
        resources.ApplyResources(Me.SoftwareCacheLocation_Tab, "SoftwareCacheLocation_Tab")
        Me.SoftwareCacheLocation_Tab.Name = "SoftwareCacheLocation_Tab"
        Me.SoftwareCacheLocation_Tab.UseVisualStyleBackColor = True
        '
        'btnESSetupInfo
        '
        resources.ApplyResources(Me.btnESSetupInfo, "btnESSetupInfo")
        Me.btnESSetupInfo.Name = "btnESSetupInfo"
        Me.btnESSetupInfo.UseVisualStyleBackColor = True
        '
        'ListView_SoftwareLocation_NEW
        '
        Me.ListView_SoftwareLocation_NEW.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader24, Me.ColumnHeader23})
        Me.ListView_SoftwareLocation_NEW.FullRowSelect = True
        Me.ListView_SoftwareLocation_NEW.GridLines = True
        Me.ListView_SoftwareLocation_NEW.HideSelection = False
        resources.ApplyResources(Me.ListView_SoftwareLocation_NEW, "ListView_SoftwareLocation_NEW")
        Me.ListView_SoftwareLocation_NEW.Name = "ListView_SoftwareLocation_NEW"
        Me.ListView_SoftwareLocation_NEW.UseCompatibleStateImageBehavior = False
        Me.ListView_SoftwareLocation_NEW.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader24
        '
        resources.ApplyResources(Me.ColumnHeader24, "ColumnHeader24")
        '
        'ColumnHeader23
        '
        resources.ApplyResources(Me.ColumnHeader23, "ColumnHeader23")
        '
        'SCCM_WSUS_SCUP_TAB
        '
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.cmd_Refresh_NEW)
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.cmd_apps_refresh_NEW)
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.ProgressBar1_NEW)
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.lbl_Missing_NEW2)
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.lbl_missing_NEW)
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.lbl_PatchCount_NEW)
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.lbl_patch_count_NEW)
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.chk_ApprovedPatch_NEW)
        Me.SCCM_WSUS_SCUP_TAB.Controls.Add(Me.ListViewWSUS_SCUP_NEW)
        resources.ApplyResources(Me.SCCM_WSUS_SCUP_TAB, "SCCM_WSUS_SCUP_TAB")
        Me.SCCM_WSUS_SCUP_TAB.Name = "SCCM_WSUS_SCUP_TAB"
        Me.SCCM_WSUS_SCUP_TAB.UseVisualStyleBackColor = True
        '
        'cmd_Refresh_NEW
        '
        resources.ApplyResources(Me.cmd_Refresh_NEW, "cmd_Refresh_NEW")
        Me.cmd_Refresh_NEW.Name = "cmd_Refresh_NEW"
        Me.cmd_Refresh_NEW.UseVisualStyleBackColor = True
        '
        'cmd_apps_refresh_NEW
        '
        resources.ApplyResources(Me.cmd_apps_refresh_NEW, "cmd_apps_refresh_NEW")
        Me.cmd_apps_refresh_NEW.Name = "cmd_apps_refresh_NEW"
        Me.cmd_apps_refresh_NEW.UseVisualStyleBackColor = True
        '
        'ProgressBar1_NEW
        '
        resources.ApplyResources(Me.ProgressBar1_NEW, "ProgressBar1_NEW")
        Me.ProgressBar1_NEW.Name = "ProgressBar1_NEW"
        '
        'lbl_Missing_NEW2
        '
        resources.ApplyResources(Me.lbl_Missing_NEW2, "lbl_Missing_NEW2")
        Me.lbl_Missing_NEW2.Name = "lbl_Missing_NEW2"
        '
        'lbl_missing_NEW
        '
        resources.ApplyResources(Me.lbl_missing_NEW, "lbl_missing_NEW")
        Me.lbl_missing_NEW.Name = "lbl_missing_NEW"
        '
        'lbl_PatchCount_NEW
        '
        resources.ApplyResources(Me.lbl_PatchCount_NEW, "lbl_PatchCount_NEW")
        Me.lbl_PatchCount_NEW.Name = "lbl_PatchCount_NEW"
        '
        'lbl_patch_count_NEW
        '
        resources.ApplyResources(Me.lbl_patch_count_NEW, "lbl_patch_count_NEW")
        Me.lbl_patch_count_NEW.Name = "lbl_patch_count_NEW"
        '
        'chk_ApprovedPatch_NEW
        '
        resources.ApplyResources(Me.chk_ApprovedPatch_NEW, "chk_ApprovedPatch_NEW")
        Me.chk_ApprovedPatch_NEW.Name = "chk_ApprovedPatch_NEW"
        Me.chk_ApprovedPatch_NEW.UseVisualStyleBackColor = True
        '
        'ListViewWSUS_SCUP_NEW
        '
        Me.ListViewWSUS_SCUP_NEW.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader26, Me.ColumnHeader27, Me.ColumnHeader28, Me.ColumnHeader29, Me.ColumnHeader30})
        Me.ListViewWSUS_SCUP_NEW.FullRowSelect = True
        Me.ListViewWSUS_SCUP_NEW.GridLines = True
        Me.ListViewWSUS_SCUP_NEW.HideSelection = False
        resources.ApplyResources(Me.ListViewWSUS_SCUP_NEW, "ListViewWSUS_SCUP_NEW")
        Me.ListViewWSUS_SCUP_NEW.MultiSelect = False
        Me.ListViewWSUS_SCUP_NEW.Name = "ListViewWSUS_SCUP_NEW"
        Me.ListViewWSUS_SCUP_NEW.UseCompatibleStateImageBehavior = False
        Me.ListViewWSUS_SCUP_NEW.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader26
        '
        resources.ApplyResources(Me.ColumnHeader26, "ColumnHeader26")
        '
        'ColumnHeader27
        '
        resources.ApplyResources(Me.ColumnHeader27, "ColumnHeader27")
        '
        'ColumnHeader28
        '
        resources.ApplyResources(Me.ColumnHeader28, "ColumnHeader28")
        '
        'ColumnHeader29
        '
        resources.ApplyResources(Me.ColumnHeader29, "ColumnHeader29")
        '
        'ColumnHeader30
        '
        resources.ApplyResources(Me.ColumnHeader30, "ColumnHeader30")
        '
        'PROGRAMS_FEATURES_TAB
        '
        Me.PROGRAMS_FEATURES_TAB.Controls.Add(Me.ProgramsAndFeaturesSubTab)
        resources.ApplyResources(Me.PROGRAMS_FEATURES_TAB, "PROGRAMS_FEATURES_TAB")
        Me.PROGRAMS_FEATURES_TAB.Name = "PROGRAMS_FEATURES_TAB"
        Me.PROGRAMS_FEATURES_TAB.UseVisualStyleBackColor = True
        '
        'SCCM_ACTIONS_TAB
        '
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button114)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.lbl_warnnig)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button0)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.CMD_ALL)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck0)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button121)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done0)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done121)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button111)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button32)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button40)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done3)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck32)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck111)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done10)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done32)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done1)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done111)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button42)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button113)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done21)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button22)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done2)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button108)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done31)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck114)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done108)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck42)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done113)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck40)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button31)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck22)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck121)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck3)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button21)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done114)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck10)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done42)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button10)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done40)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck1)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_done22)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button3)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck21)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button2)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck2)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.Button4)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck31)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck108)
        Me.SCCM_ACTIONS_TAB.Controls.Add(Me.pic_uncheck113)
        resources.ApplyResources(Me.SCCM_ACTIONS_TAB, "SCCM_ACTIONS_TAB")
        Me.SCCM_ACTIONS_TAB.Name = "SCCM_ACTIONS_TAB"
        Me.SCCM_ACTIONS_TAB.UseVisualStyleBackColor = True
        '
        'Button114
        '
        resources.ApplyResources(Me.Button114, "Button114")
        Me.Button114.Name = "Button114"
        Me.Button114.UseVisualStyleBackColor = True
        '
        'lbl_warnnig
        '
        resources.ApplyResources(Me.lbl_warnnig, "lbl_warnnig")
        Me.lbl_warnnig.Name = "lbl_warnnig"
        '
        'Button0
        '
        resources.ApplyResources(Me.Button0, "Button0")
        Me.Button0.Name = "Button0"
        Me.Button0.UseVisualStyleBackColor = True
        '
        'CMD_ALL
        '
        resources.ApplyResources(Me.CMD_ALL, "CMD_ALL")
        Me.CMD_ALL.Name = "CMD_ALL"
        Me.CMD_ALL.UseVisualStyleBackColor = True
        '
        'pic_uncheck0
        '
        resources.ApplyResources(Me.pic_uncheck0, "pic_uncheck0")
        Me.pic_uncheck0.Name = "pic_uncheck0"
        Me.pic_uncheck0.TabStop = False
        '
        'Button121
        '
        resources.ApplyResources(Me.Button121, "Button121")
        Me.Button121.Name = "Button121"
        Me.Button121.UseVisualStyleBackColor = True
        '
        'pic_done0
        '
        resources.ApplyResources(Me.pic_done0, "pic_done0")
        Me.pic_done0.Name = "pic_done0"
        Me.pic_done0.TabStop = False
        '
        'pic_done121
        '
        resources.ApplyResources(Me.pic_done121, "pic_done121")
        Me.pic_done121.Name = "pic_done121"
        Me.pic_done121.TabStop = False
        '
        'Button111
        '
        resources.ApplyResources(Me.Button111, "Button111")
        Me.Button111.Name = "Button111"
        Me.Button111.UseVisualStyleBackColor = True
        '
        'Button32
        '
        resources.ApplyResources(Me.Button32, "Button32")
        Me.Button32.Name = "Button32"
        Me.Button32.UseVisualStyleBackColor = True
        '
        'Button40
        '
        resources.ApplyResources(Me.Button40, "Button40")
        Me.Button40.Name = "Button40"
        Me.Button40.UseVisualStyleBackColor = True
        '
        'pic_done3
        '
        resources.ApplyResources(Me.pic_done3, "pic_done3")
        Me.pic_done3.Name = "pic_done3"
        Me.pic_done3.TabStop = False
        '
        'pic_uncheck32
        '
        resources.ApplyResources(Me.pic_uncheck32, "pic_uncheck32")
        Me.pic_uncheck32.Name = "pic_uncheck32"
        Me.pic_uncheck32.TabStop = False
        '
        'pic_uncheck111
        '
        resources.ApplyResources(Me.pic_uncheck111, "pic_uncheck111")
        Me.pic_uncheck111.Name = "pic_uncheck111"
        Me.pic_uncheck111.TabStop = False
        '
        'pic_done10
        '
        resources.ApplyResources(Me.pic_done10, "pic_done10")
        Me.pic_done10.Name = "pic_done10"
        Me.pic_done10.TabStop = False
        '
        'pic_done32
        '
        resources.ApplyResources(Me.pic_done32, "pic_done32")
        Me.pic_done32.Name = "pic_done32"
        Me.pic_done32.TabStop = False
        '
        'pic_done1
        '
        resources.ApplyResources(Me.pic_done1, "pic_done1")
        Me.pic_done1.Name = "pic_done1"
        Me.pic_done1.TabStop = False
        '
        'pic_done111
        '
        resources.ApplyResources(Me.pic_done111, "pic_done111")
        Me.pic_done111.Name = "pic_done111"
        Me.pic_done111.TabStop = False
        '
        'Button42
        '
        resources.ApplyResources(Me.Button42, "Button42")
        Me.Button42.Name = "Button42"
        Me.Button42.UseVisualStyleBackColor = True
        '
        'Button113
        '
        resources.ApplyResources(Me.Button113, "Button113")
        Me.Button113.Name = "Button113"
        Me.Button113.UseVisualStyleBackColor = True
        '
        'pic_done21
        '
        resources.ApplyResources(Me.pic_done21, "pic_done21")
        Me.pic_done21.Name = "pic_done21"
        Me.pic_done21.TabStop = False
        '
        'Button22
        '
        resources.ApplyResources(Me.Button22, "Button22")
        Me.Button22.Name = "Button22"
        Me.Button22.UseVisualStyleBackColor = True
        '
        'pic_done2
        '
        resources.ApplyResources(Me.pic_done2, "pic_done2")
        Me.pic_done2.Name = "pic_done2"
        Me.pic_done2.TabStop = False
        '
        'Button108
        '
        resources.ApplyResources(Me.Button108, "Button108")
        Me.Button108.Name = "Button108"
        Me.Button108.UseVisualStyleBackColor = True
        '
        'pic_done31
        '
        resources.ApplyResources(Me.pic_done31, "pic_done31")
        Me.pic_done31.Name = "pic_done31"
        Me.pic_done31.TabStop = False
        '
        'pic_uncheck114
        '
        resources.ApplyResources(Me.pic_uncheck114, "pic_uncheck114")
        Me.pic_uncheck114.Name = "pic_uncheck114"
        Me.pic_uncheck114.TabStop = False
        '
        'pic_done108
        '
        resources.ApplyResources(Me.pic_done108, "pic_done108")
        Me.pic_done108.Name = "pic_done108"
        Me.pic_done108.TabStop = False
        '
        'pic_uncheck42
        '
        resources.ApplyResources(Me.pic_uncheck42, "pic_uncheck42")
        Me.pic_uncheck42.Name = "pic_uncheck42"
        Me.pic_uncheck42.TabStop = False
        '
        'pic_done113
        '
        resources.ApplyResources(Me.pic_done113, "pic_done113")
        Me.pic_done113.Name = "pic_done113"
        Me.pic_done113.TabStop = False
        '
        'pic_uncheck40
        '
        resources.ApplyResources(Me.pic_uncheck40, "pic_uncheck40")
        Me.pic_uncheck40.Name = "pic_uncheck40"
        Me.pic_uncheck40.TabStop = False
        '
        'Button31
        '
        resources.ApplyResources(Me.Button31, "Button31")
        Me.Button31.Name = "Button31"
        Me.Button31.UseVisualStyleBackColor = True
        '
        'pic_uncheck22
        '
        resources.ApplyResources(Me.pic_uncheck22, "pic_uncheck22")
        Me.pic_uncheck22.Name = "pic_uncheck22"
        Me.pic_uncheck22.TabStop = False
        '
        'pic_uncheck121
        '
        resources.ApplyResources(Me.pic_uncheck121, "pic_uncheck121")
        Me.pic_uncheck121.Name = "pic_uncheck121"
        Me.pic_uncheck121.TabStop = False
        '
        'pic_uncheck3
        '
        resources.ApplyResources(Me.pic_uncheck3, "pic_uncheck3")
        Me.pic_uncheck3.Name = "pic_uncheck3"
        Me.pic_uncheck3.TabStop = False
        '
        'Button21
        '
        resources.ApplyResources(Me.Button21, "Button21")
        Me.Button21.Name = "Button21"
        Me.Button21.UseVisualStyleBackColor = True
        '
        'pic_done114
        '
        resources.ApplyResources(Me.pic_done114, "pic_done114")
        Me.pic_done114.Name = "pic_done114"
        Me.pic_done114.TabStop = False
        '
        'pic_uncheck10
        '
        resources.ApplyResources(Me.pic_uncheck10, "pic_uncheck10")
        Me.pic_uncheck10.Name = "pic_uncheck10"
        Me.pic_uncheck10.TabStop = False
        '
        'pic_done42
        '
        resources.ApplyResources(Me.pic_done42, "pic_done42")
        Me.pic_done42.Name = "pic_done42"
        Me.pic_done42.TabStop = False
        '
        'Button10
        '
        resources.ApplyResources(Me.Button10, "Button10")
        Me.Button10.Name = "Button10"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'pic_done40
        '
        resources.ApplyResources(Me.pic_done40, "pic_done40")
        Me.pic_done40.Name = "pic_done40"
        Me.pic_done40.TabStop = False
        '
        'pic_uncheck1
        '
        resources.ApplyResources(Me.pic_uncheck1, "pic_uncheck1")
        Me.pic_uncheck1.Name = "pic_uncheck1"
        Me.pic_uncheck1.TabStop = False
        '
        'pic_done22
        '
        resources.ApplyResources(Me.pic_done22, "pic_done22")
        Me.pic_done22.Name = "pic_done22"
        Me.pic_done22.TabStop = False
        '
        'Button3
        '
        resources.ApplyResources(Me.Button3, "Button3")
        Me.Button3.Name = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'pic_uncheck21
        '
        resources.ApplyResources(Me.pic_uncheck21, "pic_uncheck21")
        Me.pic_uncheck21.Name = "pic_uncheck21"
        Me.pic_uncheck21.TabStop = False
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'pic_uncheck2
        '
        resources.ApplyResources(Me.pic_uncheck2, "pic_uncheck2")
        Me.pic_uncheck2.Name = "pic_uncheck2"
        Me.pic_uncheck2.TabStop = False
        '
        'Button4
        '
        resources.ApplyResources(Me.Button4, "Button4")
        Me.Button4.Name = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'pic_uncheck31
        '
        resources.ApplyResources(Me.pic_uncheck31, "pic_uncheck31")
        Me.pic_uncheck31.Name = "pic_uncheck31"
        Me.pic_uncheck31.TabStop = False
        '
        'pic_uncheck108
        '
        resources.ApplyResources(Me.pic_uncheck108, "pic_uncheck108")
        Me.pic_uncheck108.Name = "pic_uncheck108"
        Me.pic_uncheck108.TabStop = False
        '
        'pic_uncheck113
        '
        resources.ApplyResources(Me.pic_uncheck113, "pic_uncheck113")
        Me.pic_uncheck113.Name = "pic_uncheck113"
        Me.pic_uncheck113.TabStop = False
        '
        'REPAIR_CLEANING_TAB
        '
        Me.REPAIR_CLEANING_TAB.Controls.Add(Me.Panel2)
        Me.REPAIR_CLEANING_TAB.Controls.Add(Me.Label9)
        Me.REPAIR_CLEANING_TAB.Controls.Add(Me.Label10)
        resources.ApplyResources(Me.REPAIR_CLEANING_TAB, "REPAIR_CLEANING_TAB")
        Me.REPAIR_CLEANING_TAB.Name = "REPAIR_CLEANING_TAB"
        Me.REPAIR_CLEANING_TAB.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.CheckBox6)
        Me.Panel2.Controls.Add(Me.CheckBox5)
        Me.Panel2.Controls.Add(Me.CheckBox4)
        Me.Panel2.Controls.Add(Me.CheckBox3)
        Me.Panel2.Controls.Add(Me.CheckBox2)
        Me.Panel2.Controls.Add(Me.CheckBox1)
        Me.Panel2.Controls.Add(Me.PictureBox3)
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Controls.Add(Me.PictureBox4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.ProgressBar1)
        Me.Panel2.Controls.Add(Me.Button1)
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Name = "Panel2"
        '
        'CheckBox6
        '
        resources.ApplyResources(Me.CheckBox6, "CheckBox6")
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        resources.ApplyResources(Me.CheckBox5, "CheckBox5")
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        resources.ApplyResources(Me.CheckBox4, "CheckBox4")
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        resources.ApplyResources(Me.CheckBox3, "CheckBox3")
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        resources.ApplyResources(Me.CheckBox2, "CheckBox2")
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        resources.ApplyResources(Me.CheckBox1, "CheckBox1")
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'PictureBox3
        '
        resources.ApplyResources(Me.PictureBox3, "PictureBox3")
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.TabStop = False
        '
        'PictureBox2
        '
        resources.ApplyResources(Me.PictureBox2, "PictureBox2")
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.TabStop = False
        '
        'PictureBox4
        '
        resources.ApplyResources(Me.PictureBox4, "PictureBox4")
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.TabStop = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Name = "Label3"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.BackColor = System.Drawing.Color.White
        resources.ApplyResources(Me.ProgressBar1, "ProgressBar1")
        Me.ProgressBar1.Name = "ProgressBar1"
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.ForeColor = System.Drawing.Color.DarkRed
        Me.Label9.Name = "Label9"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'DISPLAY_MAINTENANCE_WINDOWS_TAB
        '
        Me.DISPLAY_MAINTENANCE_WINDOWS_TAB.Controls.Add(Me.GroupBoxMaintenanceWindow_NEW)
        Me.DISPLAY_MAINTENANCE_WINDOWS_TAB.Controls.Add(Me.cmd_Add_SW)
        Me.DISPLAY_MAINTENANCE_WINDOWS_TAB.Controls.Add(Me.cmd_Show_SW)
        resources.ApplyResources(Me.DISPLAY_MAINTENANCE_WINDOWS_TAB, "DISPLAY_MAINTENANCE_WINDOWS_TAB")
        Me.DISPLAY_MAINTENANCE_WINDOWS_TAB.Name = "DISPLAY_MAINTENANCE_WINDOWS_TAB"
        Me.DISPLAY_MAINTENANCE_WINDOWS_TAB.UseVisualStyleBackColor = True
        '
        'cmd_Add_SW
        '
        Me.cmd_Add_SW.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.cmd_Add_SW, "cmd_Add_SW")
        Me.cmd_Add_SW.Name = "cmd_Add_SW"
        Me.cmd_Add_SW.TabStop = False
        Me.cmd_Add_SW.UseVisualStyleBackColor = False
        '
        'cmd_Show_SW
        '
        Me.cmd_Show_SW.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.cmd_Show_SW, "cmd_Show_SW")
        Me.cmd_Show_SW.Name = "cmd_Show_SW"
        Me.cmd_Show_SW.UseVisualStyleBackColor = False
        '
        'RUN_COMMAND_TAB
        '
        Me.RUN_COMMAND_TAB.Controls.Add(Me.lblRunCmdMsg)
        Me.RUN_COMMAND_TAB.Controls.Add(Me.cmd_Reinstall_client_NEW)
        Me.RUN_COMMAND_TAB.Controls.Add(Me.btnClearCommandWindow)
        Me.RUN_COMMAND_TAB.Controls.Add(Me.txtCommandOutput)
        Me.RUN_COMMAND_TAB.Controls.Add(Me.btnCommandInput)
        Me.RUN_COMMAND_TAB.Controls.Add(Me.txtCommandInput)
        resources.ApplyResources(Me.RUN_COMMAND_TAB, "RUN_COMMAND_TAB")
        Me.RUN_COMMAND_TAB.Name = "RUN_COMMAND_TAB"
        Me.RUN_COMMAND_TAB.UseVisualStyleBackColor = True
        '
        'lblRunCmdMsg
        '
        resources.ApplyResources(Me.lblRunCmdMsg, "lblRunCmdMsg")
        Me.lblRunCmdMsg.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.lblRunCmdMsg.Name = "lblRunCmdMsg"
        '
        'cmd_Reinstall_client_NEW
        '
        resources.ApplyResources(Me.cmd_Reinstall_client_NEW, "cmd_Reinstall_client_NEW")
        Me.cmd_Reinstall_client_NEW.Name = "cmd_Reinstall_client_NEW"
        Me.cmd_Reinstall_client_NEW.UseVisualStyleBackColor = True
        '
        'btnClearCommandWindow
        '
        resources.ApplyResources(Me.btnClearCommandWindow, "btnClearCommandWindow")
        Me.btnClearCommandWindow.Name = "btnClearCommandWindow"
        Me.btnClearCommandWindow.UseVisualStyleBackColor = True
        '
        'txtCommandOutput
        '
        Me.txtCommandOutput.BackColor = System.Drawing.Color.Black
        Me.txtCommandOutput.ForeColor = System.Drawing.Color.White
        resources.ApplyResources(Me.txtCommandOutput, "txtCommandOutput")
        Me.txtCommandOutput.Name = "txtCommandOutput"
        Me.txtCommandOutput.ReadOnly = True
        '
        'btnCommandInput
        '
        resources.ApplyResources(Me.btnCommandInput, "btnCommandInput")
        Me.btnCommandInput.Name = "btnCommandInput"
        Me.btnCommandInput.UseVisualStyleBackColor = True
        '
        'txtCommandInput
        '
        resources.ApplyResources(Me.txtCommandInput, "txtCommandInput")
        Me.txtCommandInput.Name = "txtCommandInput"
        '
        'ADVANCE_MODE_TAB_1
        '
        Me.ADVANCE_MODE_TAB_1.Controls.Add(Me.Gr666)
        Me.ADVANCE_MODE_TAB_1.Controls.Add(Me.groupBoxAdvancedMode_NEW)
        resources.ApplyResources(Me.ADVANCE_MODE_TAB_1, "ADVANCE_MODE_TAB_1")
        Me.ADVANCE_MODE_TAB_1.Name = "ADVANCE_MODE_TAB_1"
        Me.ADVANCE_MODE_TAB_1.UseVisualStyleBackColor = True
        '
        'Gr666
        '
        Me.Gr666.Controls.Add(Me.cmd_load_Logs5_NEW)
        Me.Gr666.Controls.Add(Me.lblWinUpdAgentWin10Serv_NEW)
        Me.Gr666.Controls.Add(Me.cmb_Logs5_NEW)
        Me.Gr666.Controls.Add(Me.cmd_load_Logs4_NEW)
        Me.Gr666.Controls.Add(Me.lblOSAndSoftwareUpdate_NEW)
        Me.Gr666.Controls.Add(Me.cmb_Logs4_NEW)
        Me.Gr666.Controls.Add(Me.cmd_load_Logs3_NEW)
        Me.Gr666.Controls.Add(Me.lblAppManagement_NEW)
        Me.Gr666.Controls.Add(Me.cmb_Logs3_NEW)
        Me.Gr666.Controls.Add(Me.cmd_load_Logs2_NEW)
        Me.Gr666.Controls.Add(Me.lblClientInstall_NEW)
        Me.Gr666.Controls.Add(Me.cmb_Logs2_NEW)
        Me.Gr666.Controls.Add(Me.txt_Description_NEW)
        Me.Gr666.Controls.Add(Me.cmd_load_Logs1_NEW)
        Me.Gr666.Controls.Add(Me.lbl_description_NEW)
        Me.Gr666.Controls.Add(Me.lbl_logs_NEW)
        Me.Gr666.Controls.Add(Me.cmb_Logs1_NEW)
        resources.ApplyResources(Me.Gr666, "Gr666")
        Me.Gr666.Name = "Gr666"
        Me.Gr666.TabStop = False
        '
        'cmd_load_Logs5_NEW
        '
        resources.ApplyResources(Me.cmd_load_Logs5_NEW, "cmd_load_Logs5_NEW")
        Me.cmd_load_Logs5_NEW.Name = "cmd_load_Logs5_NEW"
        Me.cmd_load_Logs5_NEW.UseVisualStyleBackColor = True
        '
        'lblWinUpdAgentWin10Serv_NEW
        '
        resources.ApplyResources(Me.lblWinUpdAgentWin10Serv_NEW, "lblWinUpdAgentWin10Serv_NEW")
        Me.lblWinUpdAgentWin10Serv_NEW.Name = "lblWinUpdAgentWin10Serv_NEW"
        '
        'cmb_Logs5_NEW
        '
        Me.cmb_Logs5_NEW.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs5_NEW, "cmb_Logs5_NEW")
        Me.cmb_Logs5_NEW.FormattingEnabled = True
        Me.cmb_Logs5_NEW.Items.AddRange(New Object() {resources.GetString("cmb_Logs5_NEW.Items"), resources.GetString("cmb_Logs5_NEW.Items1"), resources.GetString("cmb_Logs5_NEW.Items2"), resources.GetString("cmb_Logs5_NEW.Items3"), resources.GetString("cmb_Logs5_NEW.Items4"), resources.GetString("cmb_Logs5_NEW.Items5"), resources.GetString("cmb_Logs5_NEW.Items6"), resources.GetString("cmb_Logs5_NEW.Items7"), resources.GetString("cmb_Logs5_NEW.Items8"), resources.GetString("cmb_Logs5_NEW.Items9"), resources.GetString("cmb_Logs5_NEW.Items10"), resources.GetString("cmb_Logs5_NEW.Items11"), resources.GetString("cmb_Logs5_NEW.Items12"), resources.GetString("cmb_Logs5_NEW.Items13"), resources.GetString("cmb_Logs5_NEW.Items14")})
        Me.cmb_Logs5_NEW.Name = "cmb_Logs5_NEW"
        '
        'cmd_load_Logs4_NEW
        '
        resources.ApplyResources(Me.cmd_load_Logs4_NEW, "cmd_load_Logs4_NEW")
        Me.cmd_load_Logs4_NEW.Name = "cmd_load_Logs4_NEW"
        Me.cmd_load_Logs4_NEW.UseVisualStyleBackColor = True
        '
        'lblOSAndSoftwareUpdate_NEW
        '
        resources.ApplyResources(Me.lblOSAndSoftwareUpdate_NEW, "lblOSAndSoftwareUpdate_NEW")
        Me.lblOSAndSoftwareUpdate_NEW.Name = "lblOSAndSoftwareUpdate_NEW"
        '
        'cmb_Logs4_NEW
        '
        Me.cmb_Logs4_NEW.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs4_NEW, "cmb_Logs4_NEW")
        Me.cmb_Logs4_NEW.FormattingEnabled = True
        Me.cmb_Logs4_NEW.Items.AddRange(New Object() {resources.GetString("cmb_Logs4_NEW.Items"), resources.GetString("cmb_Logs4_NEW.Items1"), resources.GetString("cmb_Logs4_NEW.Items2"), resources.GetString("cmb_Logs4_NEW.Items3"), resources.GetString("cmb_Logs4_NEW.Items4"), resources.GetString("cmb_Logs4_NEW.Items5"), resources.GetString("cmb_Logs4_NEW.Items6"), resources.GetString("cmb_Logs4_NEW.Items7"), resources.GetString("cmb_Logs4_NEW.Items8"), resources.GetString("cmb_Logs4_NEW.Items9"), resources.GetString("cmb_Logs4_NEW.Items10"), resources.GetString("cmb_Logs4_NEW.Items11"), resources.GetString("cmb_Logs4_NEW.Items12"), resources.GetString("cmb_Logs4_NEW.Items13"), resources.GetString("cmb_Logs4_NEW.Items14"), resources.GetString("cmb_Logs4_NEW.Items15"), resources.GetString("cmb_Logs4_NEW.Items16"), resources.GetString("cmb_Logs4_NEW.Items17"), resources.GetString("cmb_Logs4_NEW.Items18"), resources.GetString("cmb_Logs4_NEW.Items19"), resources.GetString("cmb_Logs4_NEW.Items20"), resources.GetString("cmb_Logs4_NEW.Items21")})
        Me.cmb_Logs4_NEW.Name = "cmb_Logs4_NEW"
        '
        'cmd_load_Logs3_NEW
        '
        resources.ApplyResources(Me.cmd_load_Logs3_NEW, "cmd_load_Logs3_NEW")
        Me.cmd_load_Logs3_NEW.Name = "cmd_load_Logs3_NEW"
        Me.cmd_load_Logs3_NEW.UseVisualStyleBackColor = True
        '
        'lblAppManagement_NEW
        '
        resources.ApplyResources(Me.lblAppManagement_NEW, "lblAppManagement_NEW")
        Me.lblAppManagement_NEW.Name = "lblAppManagement_NEW"
        '
        'cmb_Logs3_NEW
        '
        Me.cmb_Logs3_NEW.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs3_NEW, "cmb_Logs3_NEW")
        Me.cmb_Logs3_NEW.FormattingEnabled = True
        Me.cmb_Logs3_NEW.Items.AddRange(New Object() {resources.GetString("cmb_Logs3_NEW.Items"), resources.GetString("cmb_Logs3_NEW.Items1"), resources.GetString("cmb_Logs3_NEW.Items2"), resources.GetString("cmb_Logs3_NEW.Items3"), resources.GetString("cmb_Logs3_NEW.Items4"), resources.GetString("cmb_Logs3_NEW.Items5"), resources.GetString("cmb_Logs3_NEW.Items6"), resources.GetString("cmb_Logs3_NEW.Items7")})
        Me.cmb_Logs3_NEW.Name = "cmb_Logs3_NEW"
        '
        'cmd_load_Logs2_NEW
        '
        resources.ApplyResources(Me.cmd_load_Logs2_NEW, "cmd_load_Logs2_NEW")
        Me.cmd_load_Logs2_NEW.Name = "cmd_load_Logs2_NEW"
        Me.cmd_load_Logs2_NEW.UseVisualStyleBackColor = True
        '
        'lblClientInstall_NEW
        '
        resources.ApplyResources(Me.lblClientInstall_NEW, "lblClientInstall_NEW")
        Me.lblClientInstall_NEW.Name = "lblClientInstall_NEW"
        '
        'cmb_Logs2_NEW
        '
        Me.cmb_Logs2_NEW.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs2_NEW, "cmb_Logs2_NEW")
        Me.cmb_Logs2_NEW.FormattingEnabled = True
        Me.cmb_Logs2_NEW.Items.AddRange(New Object() {resources.GetString("cmb_Logs2_NEW.Items"), resources.GetString("cmb_Logs2_NEW.Items1"), resources.GetString("cmb_Logs2_NEW.Items2"), resources.GetString("cmb_Logs2_NEW.Items3")})
        Me.cmb_Logs2_NEW.Name = "cmb_Logs2_NEW"
        '
        'txt_Description_NEW
        '
        Me.txt_Description_NEW.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.txt_Description_NEW, "txt_Description_NEW")
        Me.txt_Description_NEW.Name = "txt_Description_NEW"
        '
        'cmd_load_Logs1_NEW
        '
        resources.ApplyResources(Me.cmd_load_Logs1_NEW, "cmd_load_Logs1_NEW")
        Me.cmd_load_Logs1_NEW.Name = "cmd_load_Logs1_NEW"
        Me.cmd_load_Logs1_NEW.UseVisualStyleBackColor = True
        '
        'lbl_description_NEW
        '
        resources.ApplyResources(Me.lbl_description_NEW, "lbl_description_NEW")
        Me.lbl_description_NEW.Name = "lbl_description_NEW"
        '
        'lbl_logs_NEW
        '
        resources.ApplyResources(Me.lbl_logs_NEW, "lbl_logs_NEW")
        Me.lbl_logs_NEW.Name = "lbl_logs_NEW"
        '
        'cmb_Logs1_NEW
        '
        Me.cmb_Logs1_NEW.BackColor = System.Drawing.Color.AliceBlue
        resources.ApplyResources(Me.cmb_Logs1_NEW, "cmb_Logs1_NEW")
        Me.cmb_Logs1_NEW.FormattingEnabled = True
        Me.cmb_Logs1_NEW.Items.AddRange(New Object() {resources.GetString("cmb_Logs1_NEW.Items"), resources.GetString("cmb_Logs1_NEW.Items1"), resources.GetString("cmb_Logs1_NEW.Items2"), resources.GetString("cmb_Logs1_NEW.Items3"), resources.GetString("cmb_Logs1_NEW.Items4"), resources.GetString("cmb_Logs1_NEW.Items5"), resources.GetString("cmb_Logs1_NEW.Items6"), resources.GetString("cmb_Logs1_NEW.Items7"), resources.GetString("cmb_Logs1_NEW.Items8"), resources.GetString("cmb_Logs1_NEW.Items9"), resources.GetString("cmb_Logs1_NEW.Items10"), resources.GetString("cmb_Logs1_NEW.Items11"), resources.GetString("cmb_Logs1_NEW.Items12"), resources.GetString("cmb_Logs1_NEW.Items13"), resources.GetString("cmb_Logs1_NEW.Items14"), resources.GetString("cmb_Logs1_NEW.Items15"), resources.GetString("cmb_Logs1_NEW.Items16"), resources.GetString("cmb_Logs1_NEW.Items17"), resources.GetString("cmb_Logs1_NEW.Items18"), resources.GetString("cmb_Logs1_NEW.Items19"), resources.GetString("cmb_Logs1_NEW.Items20"), resources.GetString("cmb_Logs1_NEW.Items21"), resources.GetString("cmb_Logs1_NEW.Items22"), resources.GetString("cmb_Logs1_NEW.Items23"), resources.GetString("cmb_Logs1_NEW.Items24"), resources.GetString("cmb_Logs1_NEW.Items25"), resources.GetString("cmb_Logs1_NEW.Items26"), resources.GetString("cmb_Logs1_NEW.Items27"), resources.GetString("cmb_Logs1_NEW.Items28"), resources.GetString("cmb_Logs1_NEW.Items29"), resources.GetString("cmb_Logs1_NEW.Items30"), resources.GetString("cmb_Logs1_NEW.Items31"), resources.GetString("cmb_Logs1_NEW.Items32"), resources.GetString("cmb_Logs1_NEW.Items33"), resources.GetString("cmb_Logs1_NEW.Items34"), resources.GetString("cmb_Logs1_NEW.Items35"), resources.GetString("cmb_Logs1_NEW.Items36"), resources.GetString("cmb_Logs1_NEW.Items37"), resources.GetString("cmb_Logs1_NEW.Items38"), resources.GetString("cmb_Logs1_NEW.Items39"), resources.GetString("cmb_Logs1_NEW.Items40"), resources.GetString("cmb_Logs1_NEW.Items41"), resources.GetString("cmb_Logs1_NEW.Items42"), resources.GetString("cmb_Logs1_NEW.Items43"), resources.GetString("cmb_Logs1_NEW.Items44"), resources.GetString("cmb_Logs1_NEW.Items45"), resources.GetString("cmb_Logs1_NEW.Items46"), resources.GetString("cmb_Logs1_NEW.Items47"), resources.GetString("cmb_Logs1_NEW.Items48"), resources.GetString("cmb_Logs1_NEW.Items49"), resources.GetString("cmb_Logs1_NEW.Items50"), resources.GetString("cmb_Logs1_NEW.Items51"), resources.GetString("cmb_Logs1_NEW.Items52"), resources.GetString("cmb_Logs1_NEW.Items53"), resources.GetString("cmb_Logs1_NEW.Items54"), resources.GetString("cmb_Logs1_NEW.Items55"), resources.GetString("cmb_Logs1_NEW.Items56"), resources.GetString("cmb_Logs1_NEW.Items57"), resources.GetString("cmb_Logs1_NEW.Items58")})
        Me.cmb_Logs1_NEW.Name = "cmb_Logs1_NEW"
        '
        'groupBoxAdvancedMode_NEW
        '
        resources.ApplyResources(Me.groupBoxAdvancedMode_NEW, "groupBoxAdvancedMode_NEW")
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.cmd_Port_8009_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.pic_redflag2_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.txt_ListenPort_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.txt_MacAddress_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.txt_ConnectPort_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.pic_greenflag2_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.txt_cache_location_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.lblMacAddress_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.lblTCPIP_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.lblCacheLocation_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.lblListenPort_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.lblComputerName_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.lblConnectPort_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.pic_redflag1_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.txt_TCPIP_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.pic_greenflag1_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.txt_Cache_Size_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.txt_ComputerName_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.lblCacheSize_NEW)
        Me.groupBoxAdvancedMode_NEW.Controls.Add(Me.lbl_abr_size)
        Me.groupBoxAdvancedMode_NEW.Name = "groupBoxAdvancedMode_NEW"
        Me.groupBoxAdvancedMode_NEW.TabStop = False
        '
        'cmd_Port_8009_NEW
        '
        resources.ApplyResources(Me.cmd_Port_8009_NEW, "cmd_Port_8009_NEW")
        Me.cmd_Port_8009_NEW.Name = "cmd_Port_8009_NEW"
        Me.cmd_Port_8009_NEW.UseVisualStyleBackColor = True
        '
        'pic_redflag2_NEW
        '
        resources.ApplyResources(Me.pic_redflag2_NEW, "pic_redflag2_NEW")
        Me.pic_redflag2_NEW.Name = "pic_redflag2_NEW"
        Me.pic_redflag2_NEW.TabStop = False
        '
        'txt_ListenPort_NEW
        '
        Me.txt_ListenPort_NEW.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_ListenPort_NEW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_ListenPort_NEW, "txt_ListenPort_NEW")
        Me.txt_ListenPort_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_ListenPort_NEW.Name = "txt_ListenPort_NEW"
        Me.txt_ListenPort_NEW.ReadOnly = True
        '
        'txt_MacAddress_NEW
        '
        Me.txt_MacAddress_NEW.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_MacAddress_NEW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_MacAddress_NEW, "txt_MacAddress_NEW")
        Me.txt_MacAddress_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_MacAddress_NEW.Name = "txt_MacAddress_NEW"
        Me.txt_MacAddress_NEW.ReadOnly = True
        '
        'txt_ConnectPort_NEW
        '
        Me.txt_ConnectPort_NEW.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_ConnectPort_NEW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_ConnectPort_NEW, "txt_ConnectPort_NEW")
        Me.txt_ConnectPort_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_ConnectPort_NEW.Name = "txt_ConnectPort_NEW"
        Me.txt_ConnectPort_NEW.ReadOnly = True
        '
        'pic_greenflag2_NEW
        '
        resources.ApplyResources(Me.pic_greenflag2_NEW, "pic_greenflag2_NEW")
        Me.pic_greenflag2_NEW.Name = "pic_greenflag2_NEW"
        Me.pic_greenflag2_NEW.TabStop = False
        '
        'txt_cache_location_NEW
        '
        Me.txt_cache_location_NEW.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_cache_location_NEW.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.txt_cache_location_NEW, "txt_cache_location_NEW")
        Me.txt_cache_location_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_cache_location_NEW.Name = "txt_cache_location_NEW"
        Me.txt_cache_location_NEW.ReadOnly = True
        '
        'lblMacAddress_NEW
        '
        resources.ApplyResources(Me.lblMacAddress_NEW, "lblMacAddress_NEW")
        Me.lblMacAddress_NEW.Name = "lblMacAddress_NEW"
        '
        'lblTCPIP_NEW
        '
        resources.ApplyResources(Me.lblTCPIP_NEW, "lblTCPIP_NEW")
        Me.lblTCPIP_NEW.Name = "lblTCPIP_NEW"
        '
        'lblCacheLocation_NEW
        '
        resources.ApplyResources(Me.lblCacheLocation_NEW, "lblCacheLocation_NEW")
        Me.lblCacheLocation_NEW.Name = "lblCacheLocation_NEW"
        '
        'lblListenPort_NEW
        '
        resources.ApplyResources(Me.lblListenPort_NEW, "lblListenPort_NEW")
        Me.lblListenPort_NEW.Name = "lblListenPort_NEW"
        '
        'lblComputerName_NEW
        '
        resources.ApplyResources(Me.lblComputerName_NEW, "lblComputerName_NEW")
        Me.lblComputerName_NEW.Name = "lblComputerName_NEW"
        '
        'lblConnectPort_NEW
        '
        resources.ApplyResources(Me.lblConnectPort_NEW, "lblConnectPort_NEW")
        Me.lblConnectPort_NEW.Name = "lblConnectPort_NEW"
        '
        'pic_redflag1_NEW
        '
        resources.ApplyResources(Me.pic_redflag1_NEW, "pic_redflag1_NEW")
        Me.pic_redflag1_NEW.Name = "pic_redflag1_NEW"
        Me.pic_redflag1_NEW.TabStop = False
        '
        'txt_TCPIP_NEW
        '
        Me.txt_TCPIP_NEW.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_TCPIP_NEW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_TCPIP_NEW, "txt_TCPIP_NEW")
        Me.txt_TCPIP_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_TCPIP_NEW.Name = "txt_TCPIP_NEW"
        Me.txt_TCPIP_NEW.ReadOnly = True
        '
        'pic_greenflag1_NEW
        '
        resources.ApplyResources(Me.pic_greenflag1_NEW, "pic_greenflag1_NEW")
        Me.pic_greenflag1_NEW.Name = "pic_greenflag1_NEW"
        Me.pic_greenflag1_NEW.TabStop = False
        '
        'txt_Cache_Size_NEW
        '
        Me.txt_Cache_Size_NEW.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_Cache_Size_NEW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_Cache_Size_NEW, "txt_Cache_Size_NEW")
        Me.txt_Cache_Size_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_Cache_Size_NEW.Name = "txt_Cache_Size_NEW"
        Me.txt_Cache_Size_NEW.ReadOnly = True
        '
        'txt_ComputerName_NEW
        '
        Me.txt_ComputerName_NEW.BackColor = System.Drawing.Color.AliceBlue
        Me.txt_ComputerName_NEW.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.txt_ComputerName_NEW, "txt_ComputerName_NEW")
        Me.txt_ComputerName_NEW.ForeColor = System.Drawing.Color.MidnightBlue
        Me.txt_ComputerName_NEW.Name = "txt_ComputerName_NEW"
        Me.txt_ComputerName_NEW.ReadOnly = True
        '
        'lblCacheSize_NEW
        '
        resources.ApplyResources(Me.lblCacheSize_NEW, "lblCacheSize_NEW")
        Me.lblCacheSize_NEW.Name = "lblCacheSize_NEW"
        '
        'lbl_abr_size
        '
        resources.ApplyResources(Me.lbl_abr_size, "lbl_abr_size")
        Me.lbl_abr_size.Name = "lbl_abr_size"
        '
        'ADVANCE_MODE_TAB_4
        '
        Me.ADVANCE_MODE_TAB_4.Controls.Add(Me.lstv_Collection)
        resources.ApplyResources(Me.ADVANCE_MODE_TAB_4, "ADVANCE_MODE_TAB_4")
        Me.ADVANCE_MODE_TAB_4.Name = "ADVANCE_MODE_TAB_4"
        Me.ADVANCE_MODE_TAB_4.UseVisualStyleBackColor = True
        '
        'lstv_Collection
        '
        Me.lstv_Collection.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader25, Me.ColumnHeader45})
        Me.lstv_Collection.FullRowSelect = True
        Me.lstv_Collection.GridLines = True
        Me.lstv_Collection.HideSelection = False
        resources.ApplyResources(Me.lstv_Collection, "lstv_Collection")
        Me.lstv_Collection.Name = "lstv_Collection"
        Me.lstv_Collection.UseCompatibleStateImageBehavior = False
        Me.lstv_Collection.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader25
        '
        resources.ApplyResources(Me.ColumnHeader25, "ColumnHeader25")
        '
        'ColumnHeader45
        '
        resources.ApplyResources(Me.ColumnHeader45, "ColumnHeader45")
        '
        'lbl_loading
        '
        resources.ApplyResources(Me.lbl_loading, "lbl_loading")
        Me.lbl_loading.ForeColor = System.Drawing.Color.DarkRed
        Me.lbl_loading.Name = "lbl_loading"
        '
        'cmd_pc_info
        '
        resources.ApplyResources(Me.cmd_pc_info, "cmd_pc_info")
        Me.cmd_pc_info.Name = "cmd_pc_info"
        Me.cmd_pc_info.UseVisualStyleBackColor = True
        '
        'cmd_Reinstall_client
        '
        resources.ApplyResources(Me.cmd_Reinstall_client, "cmd_Reinstall_client")
        Me.cmd_Reinstall_client.Name = "cmd_Reinstall_client"
        Me.cmd_Reinstall_client.UseVisualStyleBackColor = True
        '
        'cmd_Force_Apps_update
        '
        resources.ApplyResources(Me.cmd_Force_Apps_update, "cmd_Force_Apps_update")
        Me.cmd_Force_Apps_update.Name = "cmd_Force_Apps_update"
        Me.cmd_Force_Apps_update.UseVisualStyleBackColor = True
        '
        'cmd_Force_WSUS
        '
        resources.ApplyResources(Me.cmd_Force_WSUS, "cmd_Force_WSUS")
        Me.cmd_Force_WSUS.Name = "cmd_Force_WSUS"
        Me.cmd_Force_WSUS.UseVisualStyleBackColor = True
        '
        'cmd_pkg_apps
        '
        resources.ApplyResources(Me.cmd_pkg_apps, "cmd_pkg_apps")
        Me.cmd_pkg_apps.Name = "cmd_pkg_apps"
        Me.cmd_pkg_apps.UseVisualStyleBackColor = True
        '
        'cmd_SCCM_WSUS_SCUP_Approved
        '
        resources.ApplyResources(Me.cmd_SCCM_WSUS_SCUP_Approved, "cmd_SCCM_WSUS_SCUP_Approved")
        Me.cmd_SCCM_WSUS_SCUP_Approved.Name = "cmd_SCCM_WSUS_SCUP_Approved"
        Me.cmd_SCCM_WSUS_SCUP_Approved.UseVisualStyleBackColor = True
        '
        'cmd_Clear_cache_bits
        '
        resources.ApplyResources(Me.cmd_Clear_cache_bits, "cmd_Clear_cache_bits")
        Me.cmd_Clear_cache_bits.Name = "cmd_Clear_cache_bits"
        Me.cmd_Clear_cache_bits.UseVisualStyleBackColor = True
        '
        'cmd_SCCM_Action
        '
        resources.ApplyResources(Me.cmd_SCCM_Action, "cmd_SCCM_Action")
        Me.cmd_SCCM_Action.Name = "cmd_SCCM_Action"
        Me.cmd_SCCM_Action.UseVisualStyleBackColor = True
        '
        'cmdSoftware
        '
        resources.ApplyResources(Me.cmdSoftware, "cmdSoftware")
        Me.cmdSoftware.Name = "cmdSoftware"
        Me.cmdSoftware.UseVisualStyleBackColor = True
        '
        'pic_reboot_status
        '
        resources.ApplyResources(Me.pic_reboot_status, "pic_reboot_status")
        Me.pic_reboot_status.Name = "pic_reboot_status"
        Me.pic_reboot_status.TabStop = False
        '
        'txt_reboot_status
        '
        resources.ApplyResources(Me.txt_reboot_status, "txt_reboot_status")
        Me.txt_reboot_status.Name = "txt_reboot_status"
        '
        'Pic_OFF_wuauserv
        '
        Me.Pic_OFF_wuauserv.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_OFF_wuauserv, "Pic_OFF_wuauserv")
        Me.Pic_OFF_wuauserv.Name = "Pic_OFF_wuauserv"
        Me.Pic_OFF_wuauserv.TabStop = False
        '
        'Pic_ON_wuauserv
        '
        Me.Pic_ON_wuauserv.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_ON_wuauserv, "Pic_ON_wuauserv")
        Me.Pic_ON_wuauserv.Name = "Pic_ON_wuauserv"
        Me.Pic_ON_wuauserv.TabStop = False
        '
        'Pic_OFF_PeerDistSvc
        '
        Me.Pic_OFF_PeerDistSvc.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_OFF_PeerDistSvc, "Pic_OFF_PeerDistSvc")
        Me.Pic_OFF_PeerDistSvc.Name = "Pic_OFF_PeerDistSvc"
        Me.Pic_OFF_PeerDistSvc.TabStop = False
        '
        'Pic_ON_PeerDistSvc
        '
        Me.Pic_ON_PeerDistSvc.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_ON_PeerDistSvc, "Pic_ON_PeerDistSvc")
        Me.Pic_ON_PeerDistSvc.Name = "Pic_ON_PeerDistSvc"
        Me.Pic_ON_PeerDistSvc.TabStop = False
        '
        'Pic_OFF_BITS
        '
        Me.Pic_OFF_BITS.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_OFF_BITS, "Pic_OFF_BITS")
        Me.Pic_OFF_BITS.Name = "Pic_OFF_BITS"
        Me.Pic_OFF_BITS.TabStop = False
        '
        'Pic_ON_BITS
        '
        Me.Pic_ON_BITS.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_ON_BITS, "Pic_ON_BITS")
        Me.Pic_ON_BITS.Name = "Pic_ON_BITS"
        Me.Pic_ON_BITS.TabStop = False
        '
        'Pic_OFF_CCMEXEC
        '
        Me.Pic_OFF_CCMEXEC.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_OFF_CCMEXEC, "Pic_OFF_CCMEXEC")
        Me.Pic_OFF_CCMEXEC.Name = "Pic_OFF_CCMEXEC"
        Me.Pic_OFF_CCMEXEC.TabStop = False
        '
        'pic_OFF_RemoteRegistry
        '
        Me.pic_OFF_RemoteRegistry.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pic_OFF_RemoteRegistry, "pic_OFF_RemoteRegistry")
        Me.pic_OFF_RemoteRegistry.Name = "pic_OFF_RemoteRegistry"
        Me.pic_OFF_RemoteRegistry.TabStop = False
        '
        'Pic_ON_CCMEXEC
        '
        Me.Pic_ON_CCMEXEC.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_ON_CCMEXEC, "Pic_ON_CCMEXEC")
        Me.Pic_ON_CCMEXEC.Name = "Pic_ON_CCMEXEC"
        Me.Pic_ON_CCMEXEC.TabStop = False
        '
        'Pic_OFF_MPSSVC
        '
        Me.Pic_OFF_MPSSVC.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_OFF_MPSSVC, "Pic_OFF_MPSSVC")
        Me.Pic_OFF_MPSSVC.Name = "Pic_OFF_MPSSVC"
        Me.Pic_OFF_MPSSVC.TabStop = False
        '
        'Pic_ON_MPSSVC
        '
        Me.Pic_ON_MPSSVC.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Pic_ON_MPSSVC, "Pic_ON_MPSSVC")
        Me.Pic_ON_MPSSVC.Name = "Pic_ON_MPSSVC"
        Me.Pic_ON_MPSSVC.TabStop = False
        '
        'pic_ON_RemoteRegistry
        '
        Me.pic_ON_RemoteRegistry.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.pic_ON_RemoteRegistry, "pic_ON_RemoteRegistry")
        Me.pic_ON_RemoteRegistry.Name = "pic_ON_RemoteRegistry"
        Me.pic_ON_RemoteRegistry.TabStop = False
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.Name = "Label11"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'lblRegedit1
        '
        resources.ApplyResources(Me.lblRegedit1, "lblRegedit1")
        Me.lblRegedit1.Name = "lblRegedit1"
        '
        'lblWindowsFirewallMPSSVC
        '
        resources.ApplyResources(Me.lblWindowsFirewallMPSSVC, "lblWindowsFirewallMPSSVC")
        Me.lblWindowsFirewallMPSSVC.Name = "lblWindowsFirewallMPSSVC"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.AliceBlue
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_About, Me.Menu_Option, Me.AdvancedMode_Menu})
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.TabStop = True
        '
        'Menu_About
        '
        Me.Menu_About.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Menu_About.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.UserGuideToolStripMenuItem})
        Me.Menu_About.Name = "Menu_About"
        resources.ApplyResources(Me.Menu_About, "Menu_About")
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
        '
        'UserGuideToolStripMenuItem
        '
        Me.UserGuideToolStripMenuItem.Name = "UserGuideToolStripMenuItem"
        resources.ApplyResources(Me.UserGuideToolStripMenuItem, "UserGuideToolStripMenuItem")
        '
        'Menu_Option
        '
        Me.Menu_Option.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Menu_Option.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Menu_Francais, Me.Menu_English})
        Me.Menu_Option.Name = "Menu_Option"
        resources.ApplyResources(Me.Menu_Option, "Menu_Option")
        '
        'Menu_Francais
        '
        Me.Menu_Francais.Name = "Menu_Francais"
        resources.ApplyResources(Me.Menu_Francais, "Menu_Francais")
        '
        'Menu_English
        '
        Me.Menu_English.Name = "Menu_English"
        resources.ApplyResources(Me.Menu_English, "Menu_English")
        '
        'AdvancedMode_Menu
        '
        Me.AdvancedMode_Menu.Name = "AdvancedMode_Menu"
        resources.ApplyResources(Me.AdvancedMode_Menu, "AdvancedMode_Menu")
        '
        'MenuStrip2
        '
        resources.ApplyResources(Me.MenuStrip2, "MenuStrip2")
        Me.MenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolsStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip2.Name = "MenuStrip2"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        resources.ApplyResources(Me.FileToolStripMenuItem, "FileToolStripMenuItem")
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        resources.ApplyResources(Me.ExitToolStripMenuItem, "ExitToolStripMenuItem")
        '
        'ToolsStripMenuItem
        '
        Me.ToolsStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GCProfileToolStripMenuItem, Me.ToolStripSeparator2, Me.EventViewerToolStripMenuItem, Me.CMToolStripMenuItem, Me.ServicesToolStripMenuItem, Me.WorkstationManagementToolStripMenuItem, Me.ToolStripSeparator1, Me.FORCESECURITYUPDATEToolStripMenuItem, Me.FORCEAPPLICATIONUPDATEToolStripMenuItem, Me.REINSTALLSCCMCLIENTToolStripMenuItem1, Me.ToolStripSeparator3, Me.REBOOTREMOTECOMPUTERToolStripMenuItem, Me.REMOTEASSISTANCEToolStripMenuItem, Me.EXPLORERToolStripMenuItem, Me.REMOTEDESKTOPToolStripMenuItem})
        Me.ToolsStripMenuItem.Name = "ToolsStripMenuItem"
        resources.ApplyResources(Me.ToolsStripMenuItem, "ToolsStripMenuItem")
        '
        'GCProfileToolStripMenuItem
        '
        Me.GCProfileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GCProfileLogToolStripMenuItem, Me.GCProfilePCToolStripMenuItem, Me.GCProfileUserToolStripMenuItem1})
        Me.GCProfileToolStripMenuItem.Name = "GCProfileToolStripMenuItem"
        resources.ApplyResources(Me.GCProfileToolStripMenuItem, "GCProfileToolStripMenuItem")
        '
        'GCProfileLogToolStripMenuItem
        '
        Me.GCProfileLogToolStripMenuItem.Name = "GCProfileLogToolStripMenuItem"
        resources.ApplyResources(Me.GCProfileLogToolStripMenuItem, "GCProfileLogToolStripMenuItem")
        '
        'GCProfilePCToolStripMenuItem
        '
        Me.GCProfilePCToolStripMenuItem.Name = "GCProfilePCToolStripMenuItem"
        resources.ApplyResources(Me.GCProfilePCToolStripMenuItem, "GCProfilePCToolStripMenuItem")
        '
        'GCProfileUserToolStripMenuItem1
        '
        Me.GCProfileUserToolStripMenuItem1.Name = "GCProfileUserToolStripMenuItem1"
        resources.ApplyResources(Me.GCProfileUserToolStripMenuItem1, "GCProfileUserToolStripMenuItem1")
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'EventViewerToolStripMenuItem
        '
        Me.EventViewerToolStripMenuItem.Name = "EventViewerToolStripMenuItem"
        resources.ApplyResources(Me.EventViewerToolStripMenuItem, "EventViewerToolStripMenuItem")
        '
        'CMToolStripMenuItem
        '
        Me.CMToolStripMenuItem.Name = "CMToolStripMenuItem"
        resources.ApplyResources(Me.CMToolStripMenuItem, "CMToolStripMenuItem")
        '
        'ServicesToolStripMenuItem
        '
        Me.ServicesToolStripMenuItem.Name = "ServicesToolStripMenuItem"
        resources.ApplyResources(Me.ServicesToolStripMenuItem, "ServicesToolStripMenuItem")
        '
        'WorkstationManagementToolStripMenuItem
        '
        Me.WorkstationManagementToolStripMenuItem.Name = "WorkstationManagementToolStripMenuItem"
        resources.ApplyResources(Me.WorkstationManagementToolStripMenuItem, "WorkstationManagementToolStripMenuItem")
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        resources.ApplyResources(Me.ToolStripSeparator1, "ToolStripSeparator1")
        '
        'FORCESECURITYUPDATEToolStripMenuItem
        '
        Me.FORCESECURITYUPDATEToolStripMenuItem.Name = "FORCESECURITYUPDATEToolStripMenuItem"
        resources.ApplyResources(Me.FORCESECURITYUPDATEToolStripMenuItem, "FORCESECURITYUPDATEToolStripMenuItem")
        '
        'FORCEAPPLICATIONUPDATEToolStripMenuItem
        '
        Me.FORCEAPPLICATIONUPDATEToolStripMenuItem.Name = "FORCEAPPLICATIONUPDATEToolStripMenuItem"
        resources.ApplyResources(Me.FORCEAPPLICATIONUPDATEToolStripMenuItem, "FORCEAPPLICATIONUPDATEToolStripMenuItem")
        '
        'REINSTALLSCCMCLIENTToolStripMenuItem1
        '
        Me.REINSTALLSCCMCLIENTToolStripMenuItem1.Name = "REINSTALLSCCMCLIENTToolStripMenuItem1"
        resources.ApplyResources(Me.REINSTALLSCCMCLIENTToolStripMenuItem1, "REINSTALLSCCMCLIENTToolStripMenuItem1")
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'REBOOTREMOTECOMPUTERToolStripMenuItem
        '
        Me.REBOOTREMOTECOMPUTERToolStripMenuItem.Name = "REBOOTREMOTECOMPUTERToolStripMenuItem"
        resources.ApplyResources(Me.REBOOTREMOTECOMPUTERToolStripMenuItem, "REBOOTREMOTECOMPUTERToolStripMenuItem")
        '
        'REMOTEASSISTANCEToolStripMenuItem
        '
        Me.REMOTEASSISTANCEToolStripMenuItem.Name = "REMOTEASSISTANCEToolStripMenuItem"
        resources.ApplyResources(Me.REMOTEASSISTANCEToolStripMenuItem, "REMOTEASSISTANCEToolStripMenuItem")
        '
        'EXPLORERToolStripMenuItem
        '
        Me.EXPLORERToolStripMenuItem.Name = "EXPLORERToolStripMenuItem"
        resources.ApplyResources(Me.EXPLORERToolStripMenuItem, "EXPLORERToolStripMenuItem")
        '
        'REMOTEDESKTOPToolStripMenuItem
        '
        Me.REMOTEDESKTOPToolStripMenuItem.Name = "REMOTEDESKTOPToolStripMenuItem"
        resources.ApplyResources(Me.REMOTEDESKTOPToolStripMenuItem, "REMOTEDESKTOPToolStripMenuItem")
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem1, Me.LangToolStripMenuItem, Me.UserGuideToolStripMenuItem1})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        resources.ApplyResources(Me.AboutToolStripMenuItem1, "AboutToolStripMenuItem1")
        '
        'LangToolStripMenuItem
        '
        Me.LangToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ENToolStripMenuItem, Me.FRToolStripMenuItem})
        Me.LangToolStripMenuItem.Name = "LangToolStripMenuItem"
        resources.ApplyResources(Me.LangToolStripMenuItem, "LangToolStripMenuItem")
        '
        'ENToolStripMenuItem
        '
        Me.ENToolStripMenuItem.Name = "ENToolStripMenuItem"
        resources.ApplyResources(Me.ENToolStripMenuItem, "ENToolStripMenuItem")
        '
        'FRToolStripMenuItem
        '
        Me.FRToolStripMenuItem.Name = "FRToolStripMenuItem"
        resources.ApplyResources(Me.FRToolStripMenuItem, "FRToolStripMenuItem")
        '
        'UserGuideToolStripMenuItem1
        '
        Me.UserGuideToolStripMenuItem1.CheckOnClick = True
        Me.UserGuideToolStripMenuItem1.Name = "UserGuideToolStripMenuItem1"
        resources.ApplyResources(Me.UserGuideToolStripMenuItem1, "UserGuideToolStripMenuItem1")
        '
        'Main
        '
        Me.AcceptButton = Me.cmd_Check_NEW
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.AliceBlue
        Me.Controls.Add(Me.pic_Assitance)
        Me.Controls.Add(Me.lbl_Version)
        Me.Controls.Add(Me.GroupBoxLogWindow_NEW)
        Me.Controls.Add(Me.cmd_multi_user)
        Me.Controls.Add(Me.ProgressBar)
        Me.Controls.Add(Me.MainTab)
        Me.Controls.Add(Me.cmdSoftware)
        Me.Controls.Add(Me.cmd_SCCM_Action)
        Me.Controls.Add(Me.cmd_Clear_cache_bits)
        Me.Controls.Add(Me.cmd_SCCM_WSUS_SCUP_Approved)
        Me.Controls.Add(Me.cmd_pkg_apps)
        Me.Controls.Add(Me.cmd_Force_WSUS)
        Me.Controls.Add(Me.cmd_Force_Apps_update)
        Me.Controls.Add(Me.cmd_Reinstall_client)
        Me.Controls.Add(Me.cmd_pc_info)
        Me.Controls.Add(Me.pic_reboot_status)
        Me.Controls.Add(Me.pic_remote)
        Me.Controls.Add(Me.txt_reboot_status)
        Me.Controls.Add(Me.pic_Explorer)
        Me.Controls.Add(Me.lbl_loading)
        Me.Controls.Add(Me.pic_Reboot)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Pic_OFF_wuauserv)
        Me.Controls.Add(Me.lblRegedit1)
        Me.Controls.Add(Me.lblWindowsFirewallMPSSVC)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Pic_ON_wuauserv)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Pic_ON_PeerDistSvc)
        Me.Controls.Add(Me.pic_ON_RemoteRegistry)
        Me.Controls.Add(Me.pic_OFF_RemoteRegistry)
        Me.Controls.Add(Me.Pic_ON_CCMEXEC)
        Me.Controls.Add(Me.Pic_OFF_MPSSVC)
        Me.Controls.Add(Me.Pic_ON_MPSSVC)
        Me.Controls.Add(Me.Pic_OFF_CCMEXEC)
        Me.Controls.Add(Me.Pic_ON_BITS)
        Me.Controls.Add(Me.Pic_OFF_BITS)
        Me.Controls.Add(Me.Pic_OFF_PeerDistSvc)
        Me.Controls.Add(Me.MenuStrip2)
        Me.Controls.Add(Me.btnCenterConsole)
        Me.Controls.Add(Me.btnCenterConsole2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MainMenuStrip = Me.MenuStrip2
        Me.MaximizeBox = False
        Me.Name = "Main"
        CType(Me.pic_Assitance, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_Explorer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_Reboot, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxMaintenanceWindow_NEW.ResumeLayout(False)
        CType(Me.pic_remote, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ProgramsAndFeaturesSubTab.ResumeLayout(False)
        Me.INSTALLED_SOFTWARE_TAB.ResumeLayout(False)
        Me.JAVA_TAB.ResumeLayout(False)
        Me.PROCESS_TAB.ResumeLayout(False)
        Me.SERVICES_TAB.ResumeLayout(False)
        Me.GroupBoxLogWindow_NEW.ResumeLayout(False)
        Me.GroupBoxLogWindow_NEW.PerformLayout
        Me.MainTab.ResumeLayout(False)
        Me.COMPUTER_INFORMATION_TAB.ResumeLayout(False)
        Me.AdvancedModeTab.ResumeLayout(False)
        Me.AdvancedModeTab1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.AdvancedModeTab2.ResumeLayout(False)
        Me.groupBoxAdvMode2_2.ResumeLayout(False)
        Me.groupBoxAdvMode2_2.PerformLayout
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBoxAdvMode2_3.ResumeLayout(False)
        Me.groupBoxAdvMode2_3.PerformLayout
        Me.groupBoxAdvMode2_4.ResumeLayout(False)
        Me.groupBoxAdvMode2_4.PerformLayout
        Me.groupBoxAdvMode2_1.ResumeLayout(False)
        Me.groupBoxAdvMode2_1.PerformLayout
        Me.Panel1.ResumeLayout(False)
        Me.groupBoxMembership_NEW.ResumeLayout(False)
        Me.groupBoxMembership_NEW.PerformLayout
        Me.SCCM_INFORMATION_BOX.ResumeLayout(False)
        Me.SCCM_INFORMATION_BOX.PerformLayout
        Me.CompInfoGroupBox.ResumeLayout(False)
        Me.CompInfoGroupBox.PerformLayout
        CType(Me.pic_rightArrow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_notOk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_Ok, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCCM_PK_APPS_TAB.ResumeLayout(False)
        Me.Tab_pkg_app.ResumeLayout(False)
        Me.START.ResumeLayout(False)
        CType(Me.pic_arrow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.EXEC_HIST_APPS.ResumeLayout(False)
        Me.EXEC_HIST_PKG_TAB.ResumeLayout(False)
        Me.RUNNING_PKGS_TAB.ResumeLayout(False)
        Me.ADVERTISEMENTS_TAB.ResumeLayout(False)
        Me.SoftwareCacheLocation_Tab.ResumeLayout(False)
        Me.SCCM_WSUS_SCUP_TAB.ResumeLayout(False)
        Me.SCCM_WSUS_SCUP_TAB.PerformLayout
        Me.PROGRAMS_FEATURES_TAB.ResumeLayout(False)
        Me.SCCM_ACTIONS_TAB.ResumeLayout(False)
        CType(Me.pic_uncheck0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done0, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done121, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done32, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done111, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck114, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done108, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done113, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck121, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done114, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done42, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done40, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_done22, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck21, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck31, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck108, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_uncheck113, System.ComponentModel.ISupportInitialize).EndInit()
        Me.REPAIR_CLEANING_TAB.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DISPLAY_MAINTENANCE_WINDOWS_TAB.ResumeLayout(False)
        Me.RUN_COMMAND_TAB.ResumeLayout(False)
        Me.RUN_COMMAND_TAB.PerformLayout
        Me.ADVANCE_MODE_TAB_1.ResumeLayout(False)
        Me.Gr666.ResumeLayout(False)
        Me.Gr666.PerformLayout
        Me.groupBoxAdvancedMode_NEW.ResumeLayout(False)
        Me.groupBoxAdvancedMode_NEW.PerformLayout
        CType(Me.pic_redflag2_NEW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_greenflag2_NEW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_redflag1_NEW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_greenflag1_NEW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ADVANCE_MODE_TAB_4.ResumeLayout(False)
        CType(Me.pic_reboot_status, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_OFF_wuauserv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_ON_wuauserv, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_OFF_PeerDistSvc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_ON_PeerDistSvc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_OFF_BITS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_ON_BITS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_OFF_CCMEXEC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_OFF_RemoteRegistry, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_ON_CCMEXEC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_OFF_MPSSVC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic_ON_MPSSVC, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic_ON_RemoteRegistry, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout
        Me.MenuStrip2.ResumeLayout(False)
        Me.MenuStrip2.PerformLayout
        Me.ResumeLayout(False)
        Me.PerformLayout

    End Sub
    Friend WithEvents txt_PCName_NEW As System.Windows.Forms.TextBox
    Friend WithEvents pic_notOk As System.Windows.Forms.PictureBox
    Friend WithEvents pic_rightArrow As System.Windows.Forms.PictureBox
    Friend WithEvents cmdSoftware As System.Windows.Forms.Button
    Friend WithEvents txtLoggedIn_NEW As System.Windows.Forms.TextBox
    Friend WithEvents pic_Assitance As System.Windows.Forms.PictureBox
    Friend WithEvents pic_remote As System.Windows.Forms.PictureBox
    Friend WithEvents TT As System.Windows.Forms.ToolTip
    Friend WithEvents cmd_SCCM_Action As System.Windows.Forms.Button
    Friend WithEvents cmd_Clear_cache_bits As System.Windows.Forms.Button
    Friend WithEvents cmd_SCCM_WSUS_SCUP_Approved As System.Windows.Forms.Button
    Friend WithEvents lbl_UserLoggedIn_NEW As System.Windows.Forms.Label
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents Menu_About As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Option As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_Francais As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Menu_English As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdvancedMode_Menu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbl_PCName_NEW As System.Windows.Forms.Label
    Friend WithEvents cmd_Check_NEW As System.Windows.Forms.Button
    Friend WithEvents CompInfoGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents lblImageVersion As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Pic_OFF_PeerDistSvc As System.Windows.Forms.PictureBox
    Friend WithEvents lblRegedit1 As System.Windows.Forms.Label
    Friend WithEvents Pic_ON_PeerDistSvc As System.Windows.Forms.PictureBox
    Friend WithEvents lblWindowsFirewallMPSSVC As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Pic_OFF_BITS As System.Windows.Forms.PictureBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Pic_ON_BITS As System.Windows.Forms.PictureBox
    Friend WithEvents Pic_OFF_CCMEXEC As System.Windows.Forms.PictureBox
    Friend WithEvents pic_OFF_RemoteRegistry As System.Windows.Forms.PictureBox
    Friend WithEvents Pic_ON_CCMEXEC As System.Windows.Forms.PictureBox
    Friend WithEvents Pic_OFF_MPSSVC As System.Windows.Forms.PictureBox
    Friend WithEvents Pic_ON_MPSSVC As System.Windows.Forms.PictureBox
    Friend WithEvents pic_ON_RemoteRegistry As System.Windows.Forms.PictureBox
    Friend WithEvents lbl_OSLang_NEW As System.Windows.Forms.Label
    Friend WithEvents lbl_LastRestart_NEW As System.Windows.Forms.Label
    Friend WithEvents txt_img_ver As System.Windows.Forms.TextBox
    Friend WithEvents txt_last_reboot_NEW As System.Windows.Forms.TextBox
    Friend WithEvents txt_language_NEW As System.Windows.Forms.TextBox
    Friend WithEvents Pic_OFF_wuauserv As System.Windows.Forms.PictureBox
    Friend WithEvents Pic_ON_wuauserv As System.Windows.Forms.PictureBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lbl_Version As System.Windows.Forms.Label
    Friend WithEvents pic_Reboot As System.Windows.Forms.PictureBox
    Friend WithEvents txt_OSCaption_NEW As System.Windows.Forms.TextBox
    Friend WithEvents lbl_OS_NEW As System.Windows.Forms.Label
    Friend WithEvents txt_ADSite_NEW As System.Windows.Forms.TextBox
    Friend WithEvents lbl_ADSite_NEW As System.Windows.Forms.Label
    Friend WithEvents lbl_loading As System.Windows.Forms.Label
    Friend WithEvents cmd_pkg_apps As System.Windows.Forms.Button
    Friend WithEvents txt_IP_NEW As System.Windows.Forms.TextBox
    Friend WithEvents lbl_IPAddress_NEW As System.Windows.Forms.Label
    Friend WithEvents cmd_Force_Apps_update As System.Windows.Forms.Button
    Friend WithEvents cmd_Force_WSUS As System.Windows.Forms.Button
    Friend WithEvents pic_Explorer As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBoxMaintenanceWindow_NEW As System.Windows.Forms.GroupBox
    Friend WithEvents cmd_Show_SW As System.Windows.Forms.Button
    Friend WithEvents cmd_Reinstall_client As System.Windows.Forms.Button
    Friend WithEvents ServiceWindowsListView As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmd_Add_SW As System.Windows.Forms.Button
    Friend WithEvents pic_Ok As System.Windows.Forms.PictureBox
    Friend WithEvents txt_img_install_Date As System.Windows.Forms.TextBox
    Friend WithEvents lblOsInstallDate As System.Windows.Forms.Label
    Friend WithEvents cmd_pc_info As System.Windows.Forms.Button
    Friend WithEvents cmd_multi_user As System.Windows.Forms.Button
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UserGuideToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txt_reboot_status As System.Windows.Forms.Label
    Friend WithEvents pic_reboot_status As System.Windows.Forms.PictureBox
    Friend WithEvents txt_img_ver_win10_NEW As System.Windows.Forms.TextBox
    Friend WithEvents lbl_img_ver_win10_NEW As System.Windows.Forms.Label
    Friend WithEvents DirectoryEntry1 As System.DirectoryServices.DirectoryEntry
    Friend WithEvents DirectorySearcher1 As System.DirectoryServices.DirectorySearcher
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsStripMenuItem As ToolStripMenuItem
    Friend WithEvents GCProfileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EventViewerToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CMToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ServicesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WorkstationManagementToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MainTab As TabControl
    Friend WithEvents COMPUTER_INFORMATION_TAB As TabPage
    Friend WithEvents SCCM_PK_APPS_TAB As TabPage
    Friend WithEvents SCCM_WSUS_SCUP_TAB As TabPage
    Friend WithEvents PROGRAMS_FEATURES_TAB As TabPage
    Friend WithEvents SCCM_ACTIONS_TAB As TabPage
    Friend WithEvents DISPLAY_MAINTENANCE_WINDOWS_TAB As TabPage
    Friend WithEvents REPAIR_CLEANING_TAB As TabPage
    Friend WithEvents Tab_pkg_app As TabControl
    Friend WithEvents START As TabPage
    Friend WithEvents EXEC_HIST_PKG_TAB As TabPage
    Friend WithEvents EXEC_HIST_APPS As TabPage
    Friend WithEvents RUNNING_PKGS_TAB As TabPage
    Friend WithEvents ADVERTISEMENTS_TAB As TabPage
    Friend WithEvents pic_arrow As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents ListView_ProgramsFeatures_NEW As ListView
    Friend WithEvents ColumnHeader11 As ColumnHeader
    Friend WithEvents ColumnHeader12 As ColumnHeader
    Friend WithEvents ColumnHeader13 As ColumnHeader
    Friend WithEvents listvw_ExecHistApps As ListView
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader17 As ColumnHeader
    Friend WithEvents ColumnHeader18 As ColumnHeader
    Friend WithEvents ColumnHeader19 As ColumnHeader
    Friend WithEvents ColumnHeader20 As ColumnHeader
    Friend WithEvents ColumnHeader21 As ColumnHeader
    Friend WithEvents lstvw_ExecHistPkgs As ListView
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents ListView_RunningPackages_NEW As ListView
    Friend WithEvents ColumnHeader10 As ColumnHeader
    Friend WithEvents ColumnHeader14 As ColumnHeader
    Friend WithEvents ColumnHeader15 As ColumnHeader
    Friend WithEvents ColumnHeader16 As ColumnHeader
    Friend WithEvents ProgressBar As ProgressBar
    Friend WithEvents SoftwareCacheLocation_Tab As TabPage
    Friend WithEvents ListView_SoftwareLocation_NEW As ListView
    Friend WithEvents ColumnHeader23 As ColumnHeader
    Friend WithEvents ColumnHeader24 As ColumnHeader
    Friend WithEvents lbl_Domain_NEW As Label
    Friend WithEvents txt_Domain_NEW As TextBox
    Friend WithEvents lblSiteCode_NEW As Label
    Friend WithEvents txt_WUA_NEW As TextBox
    Friend WithEvents lbl_WUPoint_NEW As Label
    Friend WithEvents lbl_CCM_UPDUSER_NEW As Label
    Friend WithEvents txt_SCCM_Catalogue_NEW As TextBox
    Friend WithEvents txt_ManagementPoint_NEW As TextBox
    Friend WithEvents lblClientVersion_NEW As Label
    Friend WithEvents lbl_Management_Point_NEW As Label
    Friend WithEvents txt_SiteCode_result_NEW As TextBox
    Friend WithEvents txt_Client_Version_Result_NEW As TextBox
    Friend WithEvents SCCM_INFORMATION_BOX As GroupBox
    Friend WithEvents txt_Vendor As TextBox
    Friend WithEvents lbl_Vendor As Label
    Friend WithEvents txt_Name As TextBox
    Friend WithEvents lbl_Name As Label
    Friend WithEvents btnCenterConsole As Button
    Friend WithEvents btnCenterConsole2 As Button
    Friend WithEvents groupBoxMembership_NEW As GroupBox
    Friend WithEvents txt_RAM As TextBox
    Friend WithEvents txt_CPU As TextBox
    Friend WithEvents txt_EquipmentType As TextBox
    Friend WithEvents txt_SRU_Verimg As TextBox
    Friend WithEvents lbl_SRUVerimg As Label
    Friend WithEvents lbl_Ram As Label
    Friend WithEvents lbl_EquipmentType_NEW As Label
    Friend WithEvents lbl_CPU As Label
    Friend WithEvents txt_LogWindow As TextBox
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents LangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UserGuideToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ENToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FRToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ListViewWSUS_SCUP_NEW As ListView
    Friend WithEvents ColumnHeader26 As ColumnHeader
    Friend WithEvents ColumnHeader27 As ColumnHeader
    Friend WithEvents ColumnHeader28 As ColumnHeader
    Friend WithEvents ColumnHeader29 As ColumnHeader
    Friend WithEvents ColumnHeader30 As ColumnHeader
    Friend WithEvents cmd_Refresh_NEW As Button
    Friend WithEvents cmd_apps_refresh_NEW As Button
    Friend WithEvents ProgressBar1_NEW As ProgressBar
    Friend WithEvents lbl_Missing_NEW2 As Label
    Friend WithEvents lbl_missing_NEW As Label
    Friend WithEvents lbl_PatchCount_NEW As Label
    Friend WithEvents lbl_patch_count_NEW As Label
    Friend WithEvents chk_ApprovedPatch_NEW As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents ProgramsAndFeaturesSubTab As TabControl
    Friend WithEvents INSTALLED_SOFTWARE_TAB As TabPage
    Friend WithEvents ListViewInstalledSoftware_NEW As ListView
    Friend WithEvents ColumnHeader31 As ColumnHeader
    Friend WithEvents ColumnHeader32 As ColumnHeader
    Friend WithEvents ColumnHeader33 As ColumnHeader
    Friend WithEvents ColumnHeader34 As ColumnHeader
    Friend WithEvents JAVA_TAB As TabPage
    Friend WithEvents ListViewJava_NEW As ListView
    Friend WithEvents ColumnHeader35 As ColumnHeader
    Friend WithEvents PROCESS_TAB As TabPage
    Friend WithEvents ListViewProcess_NEW As ListView
    Friend WithEvents ColumnHeader36 As ColumnHeader
    Friend WithEvents ColumnHeader37 As ColumnHeader
    Friend WithEvents ColumnHeader38 As ColumnHeader
    Friend WithEvents ColumnHeader39 As ColumnHeader
    Friend WithEvents ColumnHeader40 As ColumnHeader
    Friend WithEvents SERVICES_TAB As TabPage
    Friend WithEvents ListViewServices_NEW As ListView
    Friend WithEvents ColumnHeader41 As ColumnHeader
    Friend WithEvents ColumnHeader42 As ColumnHeader
    Friend WithEvents ColumnHeader43 As ColumnHeader
    Friend WithEvents ColumnHeader44 As ColumnHeader
    Friend WithEvents FORCESECURITYUPDATEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FORCEAPPLICATIONUPDATEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GroupBoxLogWindow_NEW As GroupBox
    Friend WithEvents REBOOTREMOTECOMPUTERToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents REMOTEASSISTANCEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EXPLORERToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents REMOTEDESKTOPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ADVANCE_MODE_TAB_1 As TabPage
    Friend WithEvents Gr666 As GroupBox
    Friend WithEvents cmd_load_Logs5_NEW As Button
    Friend WithEvents lblWinUpdAgentWin10Serv_NEW As Label
    Friend WithEvents cmb_Logs5_NEW As ComboBox
    Friend WithEvents cmd_load_Logs4_NEW As Button
    Friend WithEvents lblOSAndSoftwareUpdate_NEW As Label
    Friend WithEvents cmb_Logs4_NEW As ComboBox
    Friend WithEvents cmd_load_Logs3_NEW As Button
    Friend WithEvents lblAppManagement_NEW As Label
    Friend WithEvents cmb_Logs3_NEW As ComboBox
    Friend WithEvents cmd_load_Logs2_NEW As Button
    Friend WithEvents lblClientInstall_NEW As Label
    Friend WithEvents cmb_Logs2_NEW As ComboBox
    Friend WithEvents txt_Description_NEW As TextBox
    Friend WithEvents cmd_load_Logs1_NEW As Button
    Friend WithEvents lbl_description_NEW As Label
    Friend WithEvents lbl_logs_NEW As Label
    Friend WithEvents cmb_Logs1_NEW As ComboBox
    Friend WithEvents groupBoxAdvancedMode_NEW As GroupBox
    Friend WithEvents txt_MacAddress_NEW As TextBox
    Friend WithEvents txt_cache_location_NEW As TextBox
    Friend WithEvents lblMacAddress_NEW As Label
    Friend WithEvents lblTCPIP_NEW As Label
    Friend WithEvents lblCacheLocation_NEW As Label
    Friend WithEvents lblComputerName_NEW As Label
    Friend WithEvents txt_TCPIP_NEW As TextBox
    Friend WithEvents txt_Cache_Size_NEW As TextBox
    Friend WithEvents txt_ComputerName_NEW As TextBox
    Friend WithEvents lblCacheSize_NEW As Label
    Friend WithEvents lbl_abr_size As Label
    Friend WithEvents pic_redflag2_NEW As PictureBox
    Friend WithEvents txt_ListenPort_NEW As TextBox
    Friend WithEvents txt_ConnectPort_NEW As TextBox
    Friend WithEvents pic_greenflag2_NEW As PictureBox
    Friend WithEvents lblListenPort_NEW As Label
    Friend WithEvents lblConnectPort_NEW As Label
    Friend WithEvents pic_redflag1_NEW As PictureBox
    Friend WithEvents pic_greenflag1_NEW As PictureBox
    Friend WithEvents cmd_Port_8009_NEW As Button
    Friend WithEvents MembershipListView As TextBox
    Friend WithEvents GCProfileLogToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GCProfilePCToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GCProfileUserToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents REINSTALLSCCMCLIENTToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents RUN_COMMAND_TAB As TabPage
    Friend WithEvents txtCommandOutput As TextBox
    Friend WithEvents btnCommandInput As Button
    Friend WithEvents txtCommandInput As TextBox
    Friend WithEvents btnClearCommandWindow As Button
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents Panel2 As Panel
    Friend WithEvents CheckBox6 As CheckBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Button1 As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Button114 As Button
    Friend WithEvents lbl_warnnig As Label
    Friend WithEvents Button0 As Button
    Friend WithEvents CMD_ALL As Button
    Friend WithEvents pic_uncheck0 As PictureBox
    Friend WithEvents Button121 As Button
    Friend WithEvents pic_done0 As PictureBox
    Friend WithEvents pic_done121 As PictureBox
    Friend WithEvents Button111 As Button
    Friend WithEvents Button32 As Button
    Friend WithEvents Button40 As Button
    Friend WithEvents pic_done3 As PictureBox
    Friend WithEvents pic_uncheck32 As PictureBox
    Friend WithEvents pic_uncheck111 As PictureBox
    Friend WithEvents pic_done10 As PictureBox
    Friend WithEvents pic_done32 As PictureBox
    Friend WithEvents pic_done1 As PictureBox
    Friend WithEvents pic_done111 As PictureBox
    Friend WithEvents Button42 As Button
    Friend WithEvents Button113 As Button
    Friend WithEvents pic_done21 As PictureBox
    Friend WithEvents Button22 As Button
    Friend WithEvents pic_done2 As PictureBox
    Friend WithEvents Button108 As Button
    Friend WithEvents pic_done31 As PictureBox
    Friend WithEvents pic_uncheck114 As PictureBox
    Friend WithEvents pic_done108 As PictureBox
    Friend WithEvents pic_uncheck42 As PictureBox
    Friend WithEvents pic_done113 As PictureBox
    Friend WithEvents pic_uncheck40 As PictureBox
    Friend WithEvents Button31 As Button
    Friend WithEvents pic_uncheck22 As PictureBox
    Friend WithEvents pic_uncheck121 As PictureBox
    Friend WithEvents pic_uncheck3 As PictureBox
    Friend WithEvents Button21 As Button
    Friend WithEvents pic_done114 As PictureBox
    Friend WithEvents pic_uncheck10 As PictureBox
    Friend WithEvents pic_done42 As PictureBox
    Friend WithEvents Button10 As Button
    Friend WithEvents pic_done40 As PictureBox
    Friend WithEvents pic_uncheck1 As PictureBox
    Friend WithEvents pic_done22 As PictureBox
    Friend WithEvents Button3 As Button
    Friend WithEvents pic_uncheck21 As PictureBox
    Friend WithEvents Button2 As Button
    Friend WithEvents pic_uncheck2 As PictureBox
    Friend WithEvents Button4 As Button
    Friend WithEvents pic_uncheck31 As PictureBox
    Friend WithEvents pic_uncheck108 As PictureBox
    Friend WithEvents pic_uncheck113 As PictureBox
    Friend WithEvents ToolTip2 As ToolTip
    Friend WithEvents ToolTip3 As ToolTip
    Friend WithEvents ADVANCE_MODE_TAB_4 As TabPage
    Friend WithEvents lstv_Collection As ListView
    Friend WithEvents ColumnHeader25 As ColumnHeader
    Friend WithEvents ColumnHeader45 As ColumnHeader
    Friend WithEvents btnESSetupInfo As Button
    Friend WithEvents cmd_Reinstall_client_NEW As Button
    Friend WithEvents AdvancedModeTab As TabControl
    Friend WithEvents AdvancedModeTab1 As TabPage
    Friend WithEvents btnAddMaintWindow_NEW As Button
    Friend WithEvents cmd_registry_pol_NEW As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents lblHour As Label
    Friend WithEvents lblWindowDesiredLength_NEW As Label
    Friend WithEvents ddlDesiredLength As ComboBox
    Friend WithEvents lblChange15Minutes_NEW As Label
    Friend WithEvents cmd_GPO_NEW As Button
    Friend WithEvents cmd_Rebuilding_WMI_NEW As Button
    Friend WithEvents cmd_BITS_Location_NEW As Button
    Friend WithEvents cmd_Re_Registering_NEW As Button
    Friend WithEvents cmd_Client_Logs_NEW As Button
    Friend WithEvents cmd_DataStore_NEW As Button
    Friend WithEvents cmd_Del_WMI_NEW As Button
    Friend WithEvents cmd_WSUS_Download_NEW As Button
    Friend WithEvents AdvancedModeTab2 As TabPage
    Friend WithEvents groupBoxAdvMode2_2 As GroupBox
    Friend WithEvents CheckBox14 As CheckBox
    Friend WithEvents lbl_CCMVALHOUR_Warning As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents TextBox15 As TextBox
    Friend WithEvents ComboBox6 As ComboBox
    Friend WithEvents Label18 As Label
    Friend WithEvents Label31 As Label
    Friend WithEvents TextBox14 As TextBox
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents ComboBox7 As ComboBox
    Friend WithEvents Label20 As Label
    Friend WithEvents TextBox13 As TextBox
    Friend WithEvents Label21 As Label
    Friend WithEvents ComboBox5 As ComboBox
    Friend WithEvents Label22 As Label
    Friend WithEvents TextBox12 As TextBox
    Friend WithEvents Label23 As Label
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents Label24 As Label
    Friend WithEvents ComboBox4 As ComboBox
    Friend WithEvents Label25 As Label
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents Label26 As Label
    Friend WithEvents TextBox9 As TextBox
    Friend WithEvents Label27 As Label
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents Label28 As Label
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents Label29 As Label
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents Label30 As Label
    Friend WithEvents CheckBox13 As CheckBox
    Friend WithEvents groupBoxAdvMode2_3 As GroupBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents groupBoxAdvMode2_4 As GroupBox
    Friend WithEvents txt_Description As TextBox
    Friend WithEvents groupBoxAdvMode2_1 As GroupBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents CheckBox8 As CheckBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents CheckBox7 As CheckBox
    Friend WithEvents CheckBox9 As CheckBox
    Friend WithEvents CheckBox10 As CheckBox
    Friend WithEvents CheckBox11 As CheckBox
    Friend WithEvents CheckBox12 As CheckBox
    Friend WithEvents Label15 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents lblRunCmdMsg As Label
    Friend WithEvents btn_apps_refresh As Button
End Class

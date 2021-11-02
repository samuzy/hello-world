$info = @()
$Remote_PC = read-host -Prompt 'Enter the PC name / Entrez le nom du PC'
$results = get-childitem -recurse -depth 1 \\$Remote_PC\c$\windows\ccmcache -filter ES-Setup.exe
foreach ($item in $results){
    $objCCM = New-Object psobject
    $objCCM | add-member noteproperty Directory ($item.DirectoryName)
    $objCCM | add-member noteproperty SRU ($item.VersionInfo.ProductName)
    $info += $objCCM
    }

$info | Out-GridView -wait -title "$Remote_PC SCCM Cache Location Info" 

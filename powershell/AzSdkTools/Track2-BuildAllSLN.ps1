$netRepoSdkFolder   = "D:\repo\azure-sdk-for-net\sdk"
$SuccessRPs = @("x","aaa")
$FailedRPs = @("x","aaa")

$sdkFolder = Get-ChildItem $netRepoSdkFolder
$sdkFolder  | ForEach-Object {
    $curFolderPRs = Get-ChildItem($_)
    foreach ($item in $curFolderPRs) {
        if ($item.Name.Contains("Azure.ResourceManager.")) {
            & cd $item
            & dotnet build
            if($?){
                $SuccessRPs += $item.Name 
            }
            else{
                $FailedRPs += $item.Name 
            }
        }
    }
}

$FailedRPs  | ForEach-Object {
    Write-Host $_
}

Write-Host "done"
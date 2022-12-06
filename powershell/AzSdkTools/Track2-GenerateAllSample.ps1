$netRepoSdkFolder = "D:\repo\azure-sdk-for-net\sdk"

$SuccessedRP = @("XX")
$FailedRP = @("HH")

$sdkFolder = Get-ChildItem $netRepoSdkFolder
$sdkFolder  | ForEach-Object {
    $curFolderPRs = Get-ChildItem($_)
    foreach ($item in $curFolderPRs) {
        if ($item.Name.Contains("Azure.ResourceManager.")) {
            $testFolder = $item.ToString() + "\tests"
            Copy-Item -Path D:\autorest.tests.md -Destination $testFolder\autorest.tests.md
            & cd $testFolder
            & dotnet build /t:GenerateTest
            if ($?) {
                & dotnet build
                if ($?) {
                    $SuccessedRP += $item.name
                }
                else {
                    $FailedRP += $item.name
                }
            }
        }
    }
}

foreach ($item in $SuccessedRP) {
    Write-Host $item
}

Write-Host "done"
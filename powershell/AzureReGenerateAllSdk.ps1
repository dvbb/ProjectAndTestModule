
function Show-Result([array]$list) {
    $i = 0
    $result = @()
    foreach ($item in $list) {
        if (($i % 10 -eq 0) -and ($i -ne 0)) {
            $result = $result -join "  "
            Write-Host $result
            $result = @()
        }
        $i += 1
        $result += $item
    }
    $result = $result -join "  "
    Write-Host $result
    Write-Host ""
}

function Update-AllGeneratedCode([string]$path, [string]$autorestVersion) {
    $count = $path.IndexOf("ResourceManager.")
    $RPName = $path.Substring($count, $path.Length - $count).Replace("ResourceManager.", "")
    $srcFolder = Join-Path $path  "src"
    $Script:candidateRPs += $RPName
    Write-Host "`n`nStart Generate $RPName"

    # Remove the [Generated] folders in src.
    $generatedFolder = Join-Path $srcFolder "Generated"
    Write-Host $generatedFolder
    if (Test-Path $generatedFolder) {
        & rm -r $generatedFolder
        Write-Host $generatedFolder "has been removed"
    }

    # Generate src code
    & cd $srcFolder
    & dotnet build /t:GenerateCode
    if ($?) {
        Write-Host "$RPName Src Generate Successed"
        $Script:srcGenerateSuccessedRps += $RPName
    }
    else {
        Write-Host "$RPName Src Generate Failed"
        $Script:srcGenerateErrorRps += $RPName
        return
    }
    
    # Build src code
    & dotnet build
    if ($?) {
        Write-Host "$RPName Src Build Successed"
        $Script:srcBuildSuccessedRps += $RPName
    }
    else {
        Write-Host "$RPName Src Build Failed"
        $Script:srcBuildErrorRps += $RPName
        return
    }
}

function  MockTestInit {
    param(
        [Parameter()]
        [string]$netRepoRoot
    )
    begin {
        Write-Host "Re-Generate all exists Sdk."
        $Script:allTrack2Sdk = 0
        $Script:newGenerateSdk = 0
        $Script:candidateRPs = @()
        $Script:srcGenerateSuccessedRps = @()
        $Script:srcBuildSuccessedRps = @()

        $Script:srcGenerateErrorRps = @()
        $Script:srcBuildErrorRps = @()
    }
    process {
        $netRepoSdkFolder = Join-Path $netRepoRoot "sdk"

        # Init All Track2 Sdk
        $sdkFolder = Get-ChildItem $netRepoSdkFolder
        $sdkFolder  | ForEach-Object {
            $curFolderPRs = Get-ChildItem($_)
            foreach ($item in $curFolderPRs) {
                if ($item.Name.Contains("Azure.ResourceManager.")) {
                    # Create mocktests folder if it not exist
                    $Script:allTrack2Sdk++
                    Update-AllGeneratedCode -path $item.FullName -autorestVersion $AutorestVersion 
                }
            }
        }
    }
    end{
        # All Successed Output statistical results
        Write-Host "`n`n"
        Write-Host "================================================================================="
        Write-Host "=============================== Generate & Build  ==============================="
        Write-Host "================================================================================="
        Write-Host "Track2 SDK Total: $Script:allTrack2Sdk"
        Write-Host "candidateRPs:   "$Script:candidateRPs.Count
        Show-Result($Script:candidateRPs) 
        Write-Host "srcGenerateSuccessedRps: "$Script:srcGenerateSuccessedRps.Count
        Show-Result($Script:srcGenerateSuccessedRps) 
        Write-Host "srcBuildSuccessedRps: "$Script:srcBuildSuccessedRps.Count 
        Show-Result($Script:srcBuildSuccessedRps) 
        Write-Host "Src generate error RPs: "$Script:srcGenerateErrorRps.Count 
        Show-Result($Script:srcGenerateErrorRps) 
        Write-Host "Src build error RPs: "$Script:srcBuildErrorRps.Count 
        Show-Result($Script:srcBuildErrorRps) 
        Write-Host "`n"
    }
}

# Re-Generate All SDK
$AzureSdkRepoRoot = "D:\repo\azure-sdk-for-net"
MockTestInit -netRepoRoot $AzureSdkRepoRoot
function  Find-SolutionPaths {
    param(
        [Parameter()]
        [string]$sdkRootPath = ""
    )
    process {
        $filterList = Get-ChildItem -Path $sdkRootPath -Recurse -Filter CHANGELOG.md | % { $_.FullName }
        $paths = $filterList | % {
            if ($_.Contains("Azure.ResourceManager")) { $_.Replace("CHANGELOG.md", "") }
        }
        return $paths
    }
}

function  Invoke-MGMTScript {
    param(
        [Parameter()]
        [array]$cmds, 
        [Parameter()]
        [string]$fileStorePath = "C:\Users\v-cruan\Documents\GitHub\doc\Script"
    )
    process {
        $paths = Find-SolutionPaths
        $SuccessRPs = @()
        $FailedRPs = @()
        foreach ($path in $paths) {
            Set-Location $path
            foreach ($cmd in $cmds) {
                Invoke-Expression $cmd
            }
            if ($?) {
                $SuccessRPs += $path
                $SuccessRPs | Out-File -FilePath "$fileStorePath\SuccessRPs.txt"
            }
            else {
                $FailedRPs += $path
                $FailedRPs | Out-File -FilePath "$fileStorePath\FailedRPs.txt"
            }
        }
    }
}

$test = @(
    "dotnet test -f net7.0"
)
$buildAllSolution = @(
    "dotnet build"
)
$buildAllSamples = @(
    "cd samples",
    "dotnet build"
)
$buildAllSrc = @(
    "cd src",
    "dotnet build"
)
$restoreAllRecordingFiles = @(
    "test-proxy restore -a ./assets.json"
)
Invoke-MGMTScript -cmds $restoreAllRecordingFiles

function GenerateTSPFiles_SpecRepo() {
    param(
        [Parameter()]
        [string]$codegenVersion,
        [Parameter()]
        [string]$specRepoRoot = "C:\Users\v-cruan\Documents\GitHub\azure-rest-api-specs\specification",
        [Parameter()]
        [string]$convertScriptRoot = "C:\Users\v-cruan\Documents\GitHub\doc\TypeSpec\convert.ps1"
    )
    begin {
        $succeed = @()
        $failed = @()
        $succeedLogRoot = Join-Path $PSScriptRoot "succeed.txt"
        $failedLogRoot = Join-Path $PSScriptRoot "faild.txt"
        # Find all RP mgmt readme.md
        $mgmtReadmes = Get-ChildItem -Path $specRepoRoot -Recurse -Filter readme.md | % { $_.FullName } | % { if ($_.Contains("resource-manager\readme.md")) { $_ } } 

        foreach ($reamdme in $mgmtReadmes) {
            # 1. Update tspReadme.md
            $tspReadme = Join-Path $PSScriptRoot "TspReadme.md"
            $store = @()
            $store += "``````yaml"
            $store += "require: " + $reamdme
            $store += "isAzureSpec: true"
            $store += "isArm: true"
            $store += "``````"
            $store | Out-File -FilePath $tspReadme

            # 2. Set output folder
            $outputFolder = $reamdme.Replace("resource-manager\readme.md", "TspOutput")
            if (Test-Path $outputFolder) {
                Remove-Item -Path $outputFolder  -Recurse -Force
            }
            New-Item -ItemType Directory -Path $outputFolder 

            # 3. Generate TSP files
            & pwsh $convertScriptRoot -swaggerConfigFile $tspReadme -converterCodegen  $codegenVersion  -outputFolder $outputFolder

            # 4. Log
            if ($?) {
                $succeed += $reamdme
                $succeed | Out-File  -Path $succeedLogRoot
            }
            else {
                $failed += $reamdme
                $failed | Out-File   -Path $failedLogRoot
            }
        }
    }
}

function GenerateTSPFiles_NetRepo() {
    param(
        [Parameter()]
        [string]$codegenVersion,
        [Parameter()]
        [string]$specRepoRoot = "C:\Users\v-cruan\Documents\GitHub\azure-rest-api-specs",
        [Parameter()]
        [string]$netRepoRoot = "C:\Users\v-cruan\Documents\GitHub\azure-sdk-for-net\sdk",
        [Parameter()]
        [string]$convertScriptRoot = "C:\Users\v-cruan\Documents\GitHub\doc\TypeSpec\convert.ps1"
    )
    begin {
        $succeed = @()
        $failed = @()
        $UnnecessaryConfigs = @(
            "# Generated code configuration",
            "Run ``dotnet build /t:GenerateCode`` to generate code.",
            "azure-arm: true",
            "csharp: true",
            "output-folder: `$(this-folder)/Generated",
            "clear-output-folder: true",
            "sample-gen:",
            "  output-folder: `$(this-folder)/../samples/Generated",
            "  clear-output-folder: true",
            "  sample: false #true",
            "  sample: false"
        )
        $succeedLogRoot = Join-Path $PSScriptRoot "succeed.txt"
        $failedLogRoot = Join-Path $PSScriptRoot "faild.txt"
        # Find all RP mgmt autorest.md
        $mgmtAutorestmds = Get-ChildItem -Path $netRepoRoot -Recurse -Filter autorest.md | % { $_.FullName } | % { if ($_.Contains("Azure.ResourceManager.")) { $_ } } 
        
        foreach ($autorestmd in $mgmtAutorestmds) {

            $content = Get-Content $autorestmd
            $specPath = $content | % { if ($_.Contains("require: ")) { return $_ } }

            # 1. Set output folder
            $RPName = ($autorestmd.Substring($autorestmd.IndexOf("Azure.ResourceManager."))).Replace("Azure.ResourceManager.", "").Replace("\src\autorest.md", "")
            $RPName

            if (!$RPName -eq "MySql") {
                <# Action to perform if the condition is true #>
            }

            $localSpecRepoReadmeLocation = Join-Path $specRepoRoot $specPath.Substring($specPath.IndexOf("/specification"))
            if ($localSpecRepoReadmeLocation.Contains("resource-manager\readme.md")) {
                $outputFolder = $localSpecRepoReadmeLocation.Replace("resource-manager\readme.md", "$RPName.Management")
            }
            else {
                $outputFolder = $localSpecRepoReadmeLocation.Replace("readme.md", "$RPName.Management")
            }
            
            if (Test-Path $outputFolder) {
                Remove-Item -Path $outputFolder  -Recurse -Force
            }
            New-Item -ItemType Directory -Path $outputFolder 

            # 2. Update tspReadme.md
            $store = @()
            foreach ($line in $content) {
                if ($line.contains("require: ")) {
                    $store += "isAzureSpec: true"
                    $store += "isArm: true"
                }
                if (!$UnnecessaryConfigs.Contains($line)) {
                    $store += $line
                }
            }
            $tspReadme = Join-Path $outputFolder "TspReadme.md"
            $store | Out-File -FilePath $tspReadme

            # 3. Generate TSP files
            Write-Host "Generate $RPName..."
            $result = & pwsh $convertScriptRoot -swaggerConfigFile $tspReadme -converterCodegen  $codegenVersion  -outputFolder $outputFolder

            # 4. Log
            if ($?) {
                Write-Host "$RPname succeed"
                $succeed += $RPName
                $succeed | Out-File  -Path $succeedLogRoot
            }
            else {
                Write-Host "$RPname failed"
                $failed += $RPName
                $failed | Out-File   -Path $failedLogRoot
                # Add error result and generate info
                $result += "`n`n###Generate Info###`nRelated net RP: $autorestmd `n $specPath"
                $result | Out-File   -Path (Join-Path $outputFolder "ErrorResult.txt")
            }
        }
    }
}

$codegenVersion = "https://artprodcus3.artifacts.visualstudio.com/A0fb41ef4-5012-48a9-bf39-4ee3de03ee35/29ec6040-b234-4e31-b139-33dc4287b756/_apis/artifact/cGlwZWxpbmVhcnRpZmFjdDovL2F6dXJlLXNkay9wcm9qZWN0SWQvMjllYzYwNDAtYjIzNC00ZTMxLWIxMzktMzNkYzQyODdiNzU2L2J1aWxkSWQvMzMxNzc5Ni9hcnRpZmFjdE5hbWUvcGFja2FnZXM1/content?format=file&subPath=%2Fautorest-openapi-to-cadl-0.7.0-ci.f8d10f268.tgz"
$convertScriptRoot = "C:\Users\v-cruan\Documents\GitHub\doc\TypeSpec\convert.ps1"
$specRepoRoot = "C:\Users\v-cruan\Documents\GitHub\azure-rest-api-specs\specification"
$netRepoRoot = "C:\Users\v-cruan\Documents\GitHub\azure-sdk-for-net\sdk"

GenerateTSPFiles_NetRepo -codegenVersion $codegenVersion -convertScriptRoot $convertScriptRoot



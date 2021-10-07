# execute: '10.0.0.0','6.3.0.0' | .\format-windowsversion.ps1
# execute: '10.0.0.0','6.3.0.0' | .\format-windowsversion.ps1 -showbuild $true
param 
(
    $showBuild
)

begin {
    Write-Host 'Process Start'
}
Process {
    $version = [Version] $_
    $os = switch ($version.Major, $version.minor -join '.') {
        '10.0' { 'Windows 10/Windows Server 2016' }
        '6.3' { 'Windows 8.1/Windows Server 2012R2' }
        '6.2' { 'Windows 8/Windows Server 2012' }
        '6.1' { 'Windows 7/Windows Server 2008R2' }
        '6.0' { 'Windows vista/Windows Server 2008' }
        '5.2' { 'Windows XP Professional/Windows Server 2003R2' }
        Default { 'Windows XP/Windows Server 2003 or older' }
    }

    if ($showBuild) {
        $os + " build: " + $version.Build
    }else {
        $os
    }
}
end {
    Write-Host 'Process Completed'
}

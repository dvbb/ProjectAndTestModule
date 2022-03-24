function Get-Manga {
    param(
        [Parameter(Mandatory)]
        [string] $ResourceUri,

        [Parameter(Mandatory)]
        [string] $StoreFolder,

        [Parameter(Mandatory)]
        [int] $total,

        [Parameter()]
        [bool] $ClearOutFolder = $false
    )

    begin{
        Write-Host "Process Start"
        $header = @{
            'Accept'                    = 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3'
            'Accept-Encoding'           = 'gzip, deflate, br'
            'Accept-Language'           = 'en-US,en;q=0.9,zh-CN;q=0.8,zh;q=0.7'
            'Upgrade-Insecure-Requests' = '1'
            'User-Agent'                = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.86 Safari/537.36'

        }
    }
    process{
        if ($ClearOutFolder -eq $true) {
            Get-ChildItem $StoreFolder | Remove-Item -Recurse
        }
        for ($i = 1; $i -lt $count; $i++) {
            $curUri = $ResourceUri + "${i}.png"
            $fileName = $StoreFolder + "\${i}.png"
            Invoke-WebRequest -Uri $curUri -PassThru -TimeoutSec 10000 -OutFile $fileName -ErrorAction SilentlyContinue -Headers $header
        }
    }
    end{
        Write-Host "Process Done"
    }
}

$ResourceUri = ""
$StoreFolder = ""
$total = 20
Get-Manga -ResourceUri $ResourceUri -StoreFolder $StoreFolder -total $total
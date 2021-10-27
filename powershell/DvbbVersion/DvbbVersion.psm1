function Get-RandomName {
    param(
        [Parameter(Mandatory = $true)]
        [int]$count
    )
    # Ascii 65-90  is A-Z 
    # Ascii 97-122 is a-z 
    $fileName = -join ([char[]](65..90 + 97..122) | Get-Random -Count $count)
    $fileName
}

function Get-IPV6Address {
    param (
        [Parameter()]
        [int] $total
    )
    begin {
        Write-Host 'Process Start.'
    }
    process {
        for ($i = 0; $i -lt $total; $i++) {
            $num = for ($j = 0; $j -lt 6; $j++) {
                '{0:x}{1:x}' -f (Get-Random -Minimum 0 -Maximum 15), (Get-Random -Minimum 0 -Maximum 15)
            }
            $num -join ':'
        } 
    }
    end {
        Write-Host 'Process End.'
    }
}

function Get-IPV4Address {
    param (
        [Parameter()]
        [int] $total
    )
    begin {
        Write-Host 'Process Start.'
    }
    process {
        for ($i = 0; $i -lt $total; $i++) {
            $num = 0..4 | foreach-object { Get-Random -Minimum 0 -Maximum 255 }
            $num -join '.'
        }
    }
    end {
        Write-Host 'Process End.'
    }
}

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

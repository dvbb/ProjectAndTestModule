$folder = Get-ChildItem $PSScriptRoot
$folder | ForEach-Object{
    $_.Name
}
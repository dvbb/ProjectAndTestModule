$ht = @{}
$ht += @{dvbb = "male"}
$ht += @{eric = "female"}
$ht.Keys -contains "dvbb"
Write-Host $ht.Keys

foreach($item in $ht.Keys)
{
    $show = $item.ToString() +"-"+ $ht[$item]
    Write-Host  $show
}
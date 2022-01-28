# ref: https://docs.microsoft.com/en-us/powershell/scripting/learn/deep-dives/everything-about-arrays?view=powershell-7.2
Write-Host -ForegroundColor Green "Start."
$data = 'Zero','One','Two','Three'
$data = Write-Output Zero One Two Three four five six seven eight nine
$data[0]
Write-Host $data[0,2,3]
Write-Host $data[1..3]
Write-Host $data[2..0]
Write-Host $data[-1]
Write-Host $data[2..-2]
$null.count
Start-Sleep 1
Write-Host -ForegroundColor Green "Get the max index number:"
Write-Host "GetUpperBound(0): " $data.GetUpperBound(0)
Write-Host "index [GetUpperBound(0)]: " $data[$data.GetUpperBound(0)]
Write-Host "index [-1]: " $data[-1]
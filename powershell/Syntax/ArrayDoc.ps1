# ref: https://docs.microsoft.com/en-us/powershell/scripting/learn/deep-dives/everything-about-arrays?view=powershell-7.2
Write-Host -ForegroundColor Green "`nStart."
$data = 'Zero','One','Two','Three'
$data = Write-Output Zero One Two Three four five six seven eight nine
$data[0]
Write-Host $data[0,2,3]
Write-Host $data[1..3]
Write-Host $data[2..0]
Write-Host $data[-1]
Write-Host $data[2..-2]
$null.count

# Start-Sleep 1
Write-Host -ForegroundColor Green "`nGet the max index number:"
Write-Host "GetUpperBound(0): " $data.GetUpperBound(0)
Write-Host "index [GetUpperBound(0)]: " $data[$data.GetUpperBound(0)]
Write-Host "index [-1]: " $data[-1]

Write-Host -ForegroundColor Green "`nForEach & Select & Where"
$data = @(
    @{FirstName='john';LastName='Locke'}
    @{FirstName='Sawyer'; LastName='Ford'}
)
Write-Host ($data | ForEach-Object {$_.FirstName , $_.LastName , "  "})
Write-Host ($data | Where-Object {$_.LastName -eq "Locke"})
Write-Host ($data | Where LastName -eq Locke)



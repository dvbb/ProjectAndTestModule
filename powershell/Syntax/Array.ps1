$array = @()
$array += "hello"
$array += 2
Write-Host $array
Write-Host $array.Count
Write-Host $array.Length
$flag = $array.Length -eq $array.Count
Write-Host $flag

# Containe()
$array2 = @("dvbb",1,2,$null,$false)
Write-Host "array.Contains(2): " $array.Contains(2)
Write-Host "array2.Contains(2): " $array2.Contains(2)
Write-Host "array2.Contains(array): " $array2.Contains($array)
Write-Host "array2.Contains(array[2]): " $array2.Contains($array[2])

$str = "dvbb"
$str.Contains("dv")
$str -contains "dv"
$str -contains "dvbb"
$str -contains "DVbb"

$array = $str -csplit "v"
Write-Host "array: $array"
# $array.GetType()
$array2 = $str.Split("v")
Write-Host "array2: $array2"
$flag = $array -eq $array2 
Write-Host $flag

$str = "Hello World"
$str
$str.ToUpper()
$str.ToLower()

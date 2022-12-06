$file = "./Enviroment.txt"
$content = Get-Content $file

$store = ""
foreach($item in $content){
    if ($item.ToString() -eq "") {
        continue
    }
    $store += $item.ToString()
    $store += "`n"
}
$store | Out-File -FilePath ".\NewTxt.txt"
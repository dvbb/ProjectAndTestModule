# Record high frequency English words
# EXECUTE: "Word" | .\WordBook.ps1

process 
{
    $array = Get-Content .\WordBook.txt
    $append = $false
    for ($i = 0; $i -lt $array.Count; $i++) 
    {
        $strlist = $array[$i].Split(":")
        if ($strlist[0].Trim(" ") -eq $_.Trim(" ")) 
        {
            $append = $true
            $array[$i] = $strlist[0] + ":" + (1 + $strlist[1])
            BREAK
        }
    }
    if ($append) 
    {
        $array | Out-File -FilePath WordBook.txt 
    }
    else
    {
        ($_.Trim(" "))+":1" |  Out-File -FilePath WordBook.txt  -Append
    }
}


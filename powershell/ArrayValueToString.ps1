# Before execute:
# "aatw"
# "aatx"
# "aaty"
# "aatz"
# "aauw"
# "aaux"
# "aauy"
# "aauz"
# "aavw"
# "aavx"

# After execute
# "aatw","aatx","aaty","aatz","aauw","aaux","aauy","aauz","aavw","aavx",

process 
{
    $array = Get-Content .\array.txt
    $str = ""
    for ($i = 0; $i -lt $array.Count; $i++) 
    {
        $str += $array[$i] + ","
    }
    $str | out-file -FilePath .\str.txt 
}


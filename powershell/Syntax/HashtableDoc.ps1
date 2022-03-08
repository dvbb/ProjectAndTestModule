# hashtable documentation
# ref: https://docs.microsoft.com/en-us/powershell/scripting/learn/deep-dives/everything-about-hashtable?view=powershell-7.2
$environments = @{
    Prod = 'SrvProd05'
    QA   = 'SrvQA02'
    Dev  = 'SrvDev12'
}
Write-Host -ForegroundColor Green "`nMultiselection:"
Write-Host  $environments[@('QA','DEV')]
Write-Host $environments[('QA','DEV')]
Write-Host $environments['QA','DEV']

Write-Host -ForegroundColor Green "`nMeasure:"
$result = $environments | Measure-Object 
Write-Host "object count: "$result.count
Write-Host "pairs count: "$environments.Count

Write-Host -ForegroundColor Green "`nGetEnumerator():"
$environments.GetEnumerator() | ForEach-Object{
    $message = '{0} is {1} .' -f $_.key, $_.value
    Write-Output $message
}

# BadEnumeration
# region The code of  below will fail
# 因为遍历的时候不能改变遍历对象(collection)
# sample1:
# $environments.Keys | ForEach-Object {
#     $environments[$_] = 'SrvDev03'
# }
# sample2:
# foreach($key in $environments.keys) {
#     $environments[$key] = 'SrvDev03'
# }
# endregion
$environments.Keys.Clone() | ForEach-Object {
    $environments[$_] = 'SrvDev03'
}
Write-Host -ForegroundColor Green "`nUpdate value of hashtable:"
Write-Host $environments.Values
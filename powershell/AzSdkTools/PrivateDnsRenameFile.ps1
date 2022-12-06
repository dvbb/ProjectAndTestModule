
function PrivateDnsFileRename {
    param (
        $FileFolder = ""
    )
    $RecordType = "Aaaa", "A", "Cname", "MX", "Srv", "Txt", "Ptr", "Soa"
    $RecordTypeWithSuffix = @()
    foreach ($item in $RecordType) {
        $RecordTypeWithSuffix += $item + "Record"
    }
    foreach ($item in $RecordTypeWithSuffix) {
        $files = Get-ChildItem $fileFolder
    
        $matchList = @()
        foreach ($file in $files) {
            if ($file.ToString().Contains($item) -and $file.ToString().Contains(".cs")) {
                $matchList += $file
            }
        }
    
        foreach ($matchItem in $matchList) {
            $fileName = $matchItem.PSChildName.Replace($item, "PrivateDns" + $item)
            $newFilePath = $matchItem.PSParentPath + "\" + $fileName
            Copy-Item -Path $matchItem -Destination $newFilePath
            remove-item $matchItem
        }
    }
}

PrivateDnsFileRename -FileFolder "D:\repo\azure-sdk-for-net\sdk\privatedns\Azure.ResourceManager.PrivateDns\src\Customization\Models"
PrivateDnsFileRename -FileFolder "D:\repo\azure-sdk-for-net\sdk\privatedns\Azure.ResourceManager.PrivateDns\src\Customization"

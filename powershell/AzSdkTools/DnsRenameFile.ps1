
function DnsChangeFileName {
    param (
        [parameter()]
        $FileFolder = "",

        [parameter()]
        $Prefix = "Dns"
    )
    $RecordType = "Aaaa", "A", "Caa", "Cname", "MX", "NS", "Srv", "Txt", "Ptr", "Soa"
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
            $fileName = $matchItem.PSChildName.Replace($item, $Prefix + $item)
            $newFilePath = $matchItem.PSParentPath + "\" + $fileName
            Copy-Item -Path $matchItem -Destination $newFilePath
            remove-item $matchItem
        }
    }
}

DnsChangeFileName -FileFolder "D:\repo\azure-sdk-for-net\sdk\dns\Azure.ResourceManager.Dns\src\Customization"
DnsChangeFileName -FileFolder "D:\repo\azure-sdk-for-net\sdk\dns\Azure.ResourceManager.Dns\src\Customization\Models"

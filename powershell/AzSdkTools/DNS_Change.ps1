$RecordType = "Aaaa","A","Caa","Cname","MX","NS","Srv","Txt","Ptr","Soa"

#  D:\repo\azure-sdk-for-net\sdk\dns\Azure.ResourceManager.Dns\src\Customization\AaaaRecordSetData.cs
foreach($item in $RecordType){
    $file  = "D:\repo\azure-sdk-for-net\sdk\dns\Azure.ResourceManager.Dns\src\Customization\$item"+"RecordSetData.cs"
    $fileContent = Get-Content $file
    $store = ""
    $total = $fileContent.Length
    $count = 0
    foreach ($item in $fileContent) {
        $count++
        if ($count -eq $total) {
            $store += "}"
            break
        }
        if ($item.ToString().Equals("            Ttl = ttl;")) {
            $store += $item.ToString().Replace("Ttl ","TtlInSenconds ")
            $store += "`n"
            continue
        }
        if ($item.ToString().Equals("        public long? Ttl { get; set; }")) {
            $store += $item.ToString().Replace("Ttl ","TtlInSenconds ")
            $store += "`n"
            continue
        }
        $store += $item.ToString()
        $store += "`n"
    }
    $store | Out-File -FilePath $file
}

# D:\repo\azure-sdk-for-net\sdk\dns\Azure.ResourceManager.Dns\src\Customization\Models\AaaaRecordSetData.Serialization.cs
foreach($item in $RecordType){
    $file  = "D:\repo\azure-sdk-for-net\sdk\dns\Azure.ResourceManager.Dns\src\Customization\Models\$item"+"RecordSetData.Serialization.cs"
    $fileContent = Get-Content $file
    $store = ""
    $total = $fileContent.Length
    $count = 0
    foreach ($item in $fileContent) {
        $count++
        if ($count -eq $total) {
            $store += "}"
            break
        }
        if ($item.ToString().Equals("            if (Optional.IsDefined(Ttl))")) {
            $store += $item.ToString().Replace("(Ttl)","(TtlInSenconds)")
            $store += "`n"
            continue
        }
        if ($item.ToString().Equals("                writer.WriteNumberValue(Ttl.Value);")) {
            $store += $item.ToString().Replace("Ttl.Value","TtlInSenconds.Value")
            $store += "`n"
            continue
        }
        $store += $item.ToString()
        $store += "`n"
    }
    $store | Out-File -FilePath $file
}

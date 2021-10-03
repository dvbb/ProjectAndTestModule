function RenameFile {
    param (
        [string]$name = 'default_'
    )
    $num = 0
    Get-ChildItem -path .\ -filter *.png |
    foreach-object {
        $extension = $_.Extension
        $newName = '{0}{1}{2}' -f $default, $num, $extension
        $num++
        $_.FullName
        Rename-Item -Path $_.FullName -NewName $newName
    }
}




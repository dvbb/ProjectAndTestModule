[void][reflection.assembly]::Load('System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089') 

function Click-MouseButton {
    param(
        [string]$Button
    )
    $signature = @' 
        [DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
'@ 

    $SendMouseClick = Add-Type -memberDefinition $signature -name "Win32MouseEventNew" -namespace Win32Functions -passThru 
    if ($Button -eq "left") {
        $SendMouseClick::mouse_event(0x00000002, 0, 0, 0, 0);
        $SendMouseClick::mouse_event(0x00000004, 0, 0, 0, 0);
    }
    if ($Button -eq "right") {
        $SendMouseClick::mouse_event(0x00000008, 0, 0, 0, 0);
        $SendMouseClick::mouse_event(0x00000010, 0, 0, 0, 0);
    }
    if ($Button -eq "middle") {
        $SendMouseClick::mouse_event(0x00000020, 0, 0, 0, 0);
        $SendMouseClick::mouse_event(0x00000040, 0, 0, 0, 0);
    }
}

# Get mouse current position
$mousePosition = [System.Windows.Forms.Cursor]::Position	
Write-Output $mousePosition
Write-Output $mousePosition.X
Write-Output $mousePosition.Y

# sleep 5seconds
Start-Sleep -Seconds 2

for( $count = 0;$count -ne 100; $count++){
    Click-MouseButton -Button "left"
}
# or Add-Type -AssemblyName System.Windows.Forms
[void][reflection.assembly]::Load('System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089') 

# 获取鼠标当前位置
$mousePosition = [System.Windows.Forms.Cursor]::Position	
Write-Output $mousePosition
Write-Output $mousePosition.X
Write-Output $mousePosition.Y

# 操作键盘
$wshell = New-Object -ComObject wscript.shell

# 操作鼠标
function Click-MouseButton
{
  param([string]$Button, [switch]$help)
  $HelpInfo = @'
  DDDD，BDDDBD
'@ 

  if ($help -or (!$Button))
  {
      write-host $HelpInfo
      return
  }
  else
  {
      $signature=@' 
        [DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
'@ 

      $SendMouseClick = Add-Type -memberDefinition $signature -name "Win32MouseEventNew" -namespace Win32Functions -passThru 
      if($Button -eq "left")
      {
          $SendMouseClick::mouse_event(0x00000002, 0, 0, 0, 0);
          $SendMouseClick::mouse_event(0x00000004, 0, 0, 0, 0);
      }
      if($Button -eq "right")
      {
          $SendMouseClick::mouse_event(0x00000008, 0, 0, 0, 0);
          $SendMouseClick::mouse_event(0x00000010, 0, 0, 0, 0);
      }
      if($Button -eq "middle")
      {
          $SendMouseClick::mouse_event(0x00000020, 0, 0, 0, 0);
          $SendMouseClick::mouse_event(0x00000040, 0, 0, 0, 0);
      }
  }
}

# 创建窗口
Add-Type -AssemblyName System.Windows.Forms
$form = New-Object system.Windows.Forms.Form


$oriPosition = [System.Windows.Forms.Cursor]::Position
# 循环
for($index = 0;;$index++){
    Write-Output "$index ============================================="
    Get-Date
    # sleep 240 秒
    Start-Sleep -Seconds 5
    $mousePosition = [System.Windows.Forms.Cursor]::Position
    Write-Output $mousePosition
    Write-Output $oriPosition
    $notMove = $oriPosition.X -eq $mousePosition.X -and $oriPosition.Y -eq $mousePosition.Y
    Write-Output "没有移动: $notMove"
    # $wshell.SendKeys("{END}")
    if ($notMove){
        Click-MouseButton "right"
    }
    $oriPosition = $mousePosition
}

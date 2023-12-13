$failedLogPath = "C:\Users\v-cruan\Documents\GitHub\doc\TSP_Transfer\BuildResult\0failed.txt"
$excelPath = "C:\Users\v-cruan\Desktop\MyTSP.xlsx"

$failedRps = @()
$content = Get-Content $failedLogPath
foreach ($item in $content) {
    $failedRps += $item
}

# 创建一个Excel应用程序对象
$excel = New-Object -ComObject Excel.Application

# 打开一个现有的Excel文件
$workbook = $excel.Workbooks.Open($excelPath)

# 获取第一个工作表
$worksheet = $workbook.Worksheets.Item(3)

# 定义要查找的范围
$range = $worksheet.Range("A2:A179")

# 遍历范围内的单元格
foreach ($cell in $range) {
    $cellValue = $cell.Value2
    if ($cellValue -ne $null -and $failedRps -contains $cellValue) {
        # 获取当前单元格所在的行号
        $row = $cell.Row
        # 将同行的 F 列单元格颜色修改为红色
        $red = 255
        $green = 153
        $blue = 153
        $rgbColor = $red -bor ($green -shl 8) -bor ($blue -shl 16)
        $worksheet.Cells.Item($row, 6).Interior.Color = $rgbColor
    }
}

# 保存修改并关闭Excel文件
$workbook.Save()
$workbook.Close()
$excel.Quit()

# 释放 COM 对象
[System.Runtime.Interopservices.Marshal]::ReleaseComObject($worksheet) | Out-Null
[System.Runtime.Interopservices.Marshal]::ReleaseComObject($workbook) | Out-Null
[System.Runtime.Interopservices.Marshal]::ReleaseComObject($excel) | Out-Null
[System.GC]::Collect()
[System.GC]::WaitForPendingFinalizers()
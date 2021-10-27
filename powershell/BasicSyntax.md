# Basic syntax

## Arrays

```
$array1= @(1, 2, 3, 4)
$array2= @("s1", "s2", "s3", "s4")
for ($i = 0; $i -lt $array1.Count; $i++) {
    $array1[$i]
}
foreach ($item in $array2) {
    $item
}
```

## Hash table

```
$hashTable1 = @{ name = "dvbb" ; sex = "male"}
$hashTable1['name']
$hashTable1['sex']
$hashTable1.name
$hashTable1.sex 
```

## Loop structure
`for ($i = 0; $i -lt $array.Count; $i++){...}`

`foreach ($item in $collection){...}`

`While([Judgment]){...}`

`Do{...}While([Judgment])`

`Do{...}Until([Judgment])`

## Class

--new-object

key word:

-class

-enum

-static

```
class Person {
    [int]$age = 0
    [string] $name = 'default'

    Person() {
        $this.age = 0
        $this.name = 'default'
    }
    
    Person($age, $name) {
        $this.age = $age
        $this.name = $name
    }

    show() {
        Write-Host  $this.name, age $this.age
    }

    [int]YearsPassed([int]$years) {
        return $this.age + $years
    }
}

$p1 = new-object person(22,"dvbb")
$p1.show()
$p1.YearsPassed(25)
```

## Cmdlets & Pipeline

what is Commandlets: 

Executable function in Powershell

[verb-noun] [-parameter value] [-parameter value] ...

what is Pipeline:

function1 | function2 | ... | ...

also can use:

```
begin{
    #init, only execute once
    ...
}
process{
    ...
} 
end{
    #init, only execute once
    ...
}
```

example:
```
"you", "me" | foreach-object { "sya $_" }
"you", "me" | where-object { $_ -match "u" }
```

## Extension

[CmdletBinding()]

[Parameter(Mandatory = $true)]

## Moduele
  
Import-Module <name/path> # 将模块导入当前上下文

$PSModulePath

Get-Module

在.ps1中加入了新的function，需要从新`Import-Module ./temp.ps1` 来加载Module
Get-Module可以查看当前的Module List，然后就可以调用 `"string" | FuncName` 来调用(可Tab补齐)

## Create Module List

1. create a folder of <font color=#006644>[ModuleName]</font>
2. craete a file of <font color=#006644>[ModuleName].psm1</font>
3. coding function in <font color=#006644>.psm1</font>
4. <font color=#e56000>new-modulemanifest</font> -Path <font color=#0055a0>'.\[ModuleName].psm1'</font> -Author <font color=#0055a0>'author'</font> -CompanyName <font color=#0055a0>'[CompanyName]'</font>-Description <font color=#0055a0>'[Description]'</font>
5. [ModuleName].psd1 has been created, open it
6. `RootModule = '` change to `RootModule = '.\[ModuleName].psm1'`
   `FunctionsToExport = @()` change to `FunctionsToExport = @('func1','func2','func3',...)`
7. <font color=#e56000>Import-Module</font> .\ [ModuleName].psd1 -force
8. The module has been import to current context

##### Module Import to Global
- $env:PSModulePath
- copy [YourModuleFolder] to [Modules] folder


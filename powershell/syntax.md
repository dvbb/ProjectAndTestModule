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
### Command
新增： dotnet new -i .\template
卸载： dotnet new --uninstall .\template
查看： dotnet new -l
执行： dotnet new azmgmt -n [shortName]

### Config
location:
```
\template
    \.template.config
        template.json
    Some folders
    Some files
```
--template.json:
```json
{
  "$schema": "http://json.schemastore.org/template",
  "author": "chengming",
  "classifications": [ "Console" ],
  "name": "azmgmt",
  "identity": "azmgmt", //模板唯一标识
  "groupIdentity": "azmgmt", 
  "shortName": "azmgmt", //【修改】短名称，使用 dotnet new <shortName> 安装模板时的名称
  "tags": {
    "language": "C#", 
    "type": "project" 
  },
  "sourceName": "Template", //【修改】在使用 -n 选项时，会替换模板中项目的名字
  "preferNameDirectory": true
}
```

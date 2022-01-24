# The command returns a list of verbs that most commands adhere to
$verblist = Get-Verb

# This command retrieves a list of all commands installed on your machine.
$commandlist = Get-Command
$commandlist = Get-Command -Name '*Process'
$commandlist = Get-Command -Verb 'Get'
$commandlist = Get-Command -Noun U*
$commandlist = Get-Command -Verb Get -Noun U*
$commandlist = (Get-Process | Where-Object { $_.ProcessName -Like "p*" })

# `Get-Member` operates on object based output and is able to discover what object, properties and methods are available for a command.
# Get-Service | Get-Member
Get-Process | Get-Member

# Invoking this command with the name of a command as an argument displays a help page describing various parts of a command.
$helpdoc = Get-Help

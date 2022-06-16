& git clone https://github.com/dvbb/MgmtTemplate.git $projRoot\MgmtTemplate
& cd $projRoot\MgmtTemplate
$commitHash = (git rev-parse HEAD).Substring(0,40)

write-host $commitHash

# $versionDatePart = [System.DateTime]::Now.ToString('yyyyMMdd.HHmmss')
# $version = "1.$versionDatePart-git-$commitHash"
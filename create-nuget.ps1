Set-StrictMode -Version Latest
$templateName = "template"
$templatePath =     "./$templateName/mtr"
$contentDirectory = "./$templateName/mtr/content"
$nugetPath = "./$templateName/nuget.exe"
$nugetOut =  "./$templateName/nuget"
$nugetUrl = "https://dist.nuget.org/win-x86-commandline/v4.6.0-preview3/nuget.exe"


Write-Output "Copy WebApiNet5 template"
Copy-Item -Path "./src/WebApiNet5Template" -Recurse -Destination "$contentDirectory/WebApiNet5Template" -Container

Write-Output "Copy HostedServiceNet5RabbitConsumer template"
Copy-Item -Path "./src/HostedServiceNet5RabbitConsumerTemplate" -Recurse -Destination "$contentDirectory/HostedServiceNet5RabbitConsumerTemplate" -Container

Write-Output "Copy nuspec"
Copy-item -Force -Recurse "MyTechRamblings.Templates.nuspec" -Destination $templatePath

Write-Output "Download nuget.exe from $nugetUrl"
Invoke-WebRequest -Uri $nugetUrl -OutFile $nugetPath

Write-Output "Pack nuget"
$cmdArgList = @( "pack", "$templatePath\MyTechRamblings.Templates.nuspec",
				 "-OutputDirectory", "$nugetOut", "-NoDefaultExcludes")
& $nugetPath $cmdArgList 
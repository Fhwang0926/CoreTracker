# set-executionpolicy unrestricted
Write-Output "build nsi file start"
Write-Output "arch : x86_x64"
$version = ""
foreach($line in (git tag | Select-Object -last 1)) { if($line -ne "") { $version = $line } }
if($version -eq "") { Write-Output "no have"; break; }
Write-Output $version

$nsi = Get-Content "sample.nsi"
$data = $nsi.Replace("%VERSION%", $version)

Set-Content "build.$version.nsi" $data
code "build.$version.nsi"
Write-Output "build nsi file done"

Write-Output "build delete pre file start"
if (Test-Path ..\bin\Release\x32\Newtonsoft.x32.Json.dll) { Remove-Item ..\bin\Release\x32\Newtonsoft.x32.Json.dll; Write-Output "Deleted Newtonsoft.x32.Json.dll"; }
if (Test-Path ..\bin\Release\x32\CoreTracker.x32.exe) { Remove-Item ..\bin\Release\x32\CoreTracker.x32.exe; Write-Output "Deleted CoreTracker.x32.exe"; }
if (Test-Path ..\bin\Release\x32\OpenHardwareMonitorLib.x32.dll) { Remove-Item ..\bin\Release\x32\OpenHardwareMonitorLib.x32.dll; Write-Output "Deleted OpenHardwareMonitorLib.x32.dll"; }
if (Test-Path ..\bin\Release\x64\Newtonsoft.x64.Json.dll) { Remove-Item ..\bin\Release\x64\Newtonsoft.x64.Json.dll; Write-Output "Deleted Newtonsoft.x64.Json.dll"; }
if (Test-Path ..\bin\Release\x64\CoreTracker.x64.exe) { Remove-Item ..\bin\Release\x64\CoreTracker.x64.exe; Write-Output "Deleted CoreTracker.x64.Json"; }
if (Test-Path ..\bin\Release\x64\OpenHardwareMonitorLib.x64.dll) { Remove-Item ..\bin\Release\x64\OpenHardwareMonitorLib.x64.dll; Write-Output "Deleted OpenHardwareMonitorLib.x64.dll"; }
Write-Output "build delete pre file done"

Write-Output "build rename to install files start"
Copy-Item ..\bin\Release\x32\Newtonsoft.Json.dll ..\bin\Release\x32\Newtonsoft.Json.x32.dll -Force
Copy-Item ..\bin\Release\x32\OpenHardwareMonitorLib.dll ..\bin\Release\x32\OpenHardwareMonitorLib.x32.dll -Force
Copy-Item ..\bin\Release\x32\CoreTracker.exe ..\bin\Release\x32\CoreTracker.x32.exe -Force
Copy-Item ..\bin\Release\x64\Newtonsoft.Json.dll ..\bin\Release\x64\Newtonsoft.Json.x64.dll -Force
Copy-Item ..\bin\Release\x64\OpenHardwareMonitorLib.dll ..\bin\Release\x64\OpenHardwareMonitorLib.x64.dll -Force
Copy-Item ..\bin\Release\x64\CoreTracker.exe ..\bin\Release\x64\CoreTracker.x64.exe -Force
Write-Output "build rename to install files done"




# start upload relase
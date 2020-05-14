# set-executionpolicy unrestricted
Write-Output "build nsi file start"
Write-Output "arch : x86_x64"
$version = ""
$msbuild_option = "CoreTracker.csproj /t:Build /p:Configuration=Release"
foreach($line in (git tag | Select-Object -last 1)) { if($line -ne "") { $version = $line } }
if($version -eq "") { Write-Output "no have"; break; }
Write-Output $version

# start build
$msbuild = $args[0]
if (([string]::IsNullOrEmpty($msbuild)))
{
  Write-Output "ss"  
  $msbuild = "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin";
}

if (!(Test-Path $msbuild)) {
  Write-Output $msbuild
  Write-Output "plz, set msbuild bin path first"  
  exit
}

Write-Output "set build version start : $msbuild"
Set-Location ..
((Get-Content -path ./Form1.cs -Raw) -replace '(VERSION = )"v[0-9].[0-9].[0-9]"', ('$1'+'"'+$version+'"')) | Set-Content -Path ./Form1.cs
Write-Output "version set"
Start-Process -FilePath "msbuild.exe" -WorkingDirectory "$msbuild" -Wait -ArgumentList "$msbuild_option;Platform=x64"
Write-Output "compile done x64"
Start-Process -FilePath "msbuild.exe" -WorkingDirectory "$msbuild" -Wait -ArgumentList "$msbuild_option;Platform=x86"
Write-Output "compile done x86"
Set-Location .\installer
Write-Output "set build version end"
# end build


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
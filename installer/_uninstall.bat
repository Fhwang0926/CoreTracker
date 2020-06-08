echo off
cls
echo start uninstall script
SCHTASKS /Delete /TN "CoreTracker" /F
tasklist /fi "imagename eq CoreTracker.exe" |find ":" > nul
if errorlevel 1 wmic process where "name='CoreTracker.exe'" delete
ping 127.0.0.1 -n 3 > nul
pushd "%~dp0"
CoreTrackerHelper.exe
echo CoreTrackerHelper cleanup
ping 127.0.0.1 -n 2 > nul
echo done
exit 1
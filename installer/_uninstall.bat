echo off
cls
echo start uninstall script
TaskKill /IM CoreTracker.exe
ping 127.0.0.1 -n 3 > NULL
tasklist /fi "imagename eq CoreTracker.exe" |find ":" > nul
if errorlevel 1 taskkill /f /im "CoreTracker.exe"
ping 127.0.0.1 -n 2 > NULL
CoreTrackerHelper.exe
del NULL
echo done
exit 1
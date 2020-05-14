echo off
cls
echo start uninstall script
tasklist /fi "imagename eq CoreTracker.exe" |find ":" > nul
if errorlevel 1 wmic process where "name='CoreTracker.exe'" delete
ping 127.0.0.1 -n 3 > NULL
CoreTrackerHelper.exe
del NULL
echo done
exit 1
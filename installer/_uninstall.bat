echo off
cls
echo start uninstall script
net stop CoreTrackerHelper
sc delete CoreTrackerHelper
TaskKill /IM CoreTracker.exe
ping 127.0.0.1 -n 3 > NULL
echo done
exit 1
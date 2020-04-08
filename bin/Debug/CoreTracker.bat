cd %~dp0
echo off
cls
echo start update : CoreTracker
taskkill /IM CoreTracker.exe /F
timeout 3 > NUL
move /Y CoreTracker_new.exe CoreTracker.exe
timeout 1 > NUL
START /B CoreTracker.exe
del %0

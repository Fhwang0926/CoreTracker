cd %~dp0
echo off
cls
echo start update : CoreTracker
timeout 3 > NUL
xcopy CoreTracker_new.exe CoreTracker.exe /K /D /H /Y
del /Y CoreTracker.exe
START /B CoreTracker.exe

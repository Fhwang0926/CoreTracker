; Script generated by the HM NIS Edit Script Wizard. 
 
; HM NIS Edit Wizard helper defines 
!define PRODUCT_NAME "CoreTracker" 
!define PRODUCT_VERSION "%VERSION%" 
!define PRODUCT_PUBLISHER "helloFhwang, Inc." 
!define PRODUCT_WEB_SITE "https://github.com/Fhwang0926/CoreTracker" 
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\${PRODUCT_NAME}.exe" 
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" 
!define PRODUCT_UNINST_ROOT_KEY "HKLM" 
 
; MUI 1.67 compatible ------ 
!include "MUI.nsh" 
 
; MUI Settings 
!define MUI_ABORTWARNING 
!define MUI_ICON "Properties\icon\form.ico" 
!define MUI_UNICON "Properties\icon\form.ico" 
 
; Language Selection Dialog Settings 
!define MUI_LANGDLL_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}" 
!define MUI_LANGDLL_REGISTRY_KEY "${PRODUCT_UNINST_KEY}" 
!define MUI_LANGDLL_REGISTRY_VALUENAME "NSIS:Language" 
 
; Welcome page 
!insertmacro MUI_PAGE_WELCOME 
; License page 
!insertmacro MUI_PAGE_LICENSE "License" 
; Directory page  
!insertmacro MUI_PAGE_DIRECTORY 
  
!insertmacro MUI_PAGE_INSTFILES 
  
; Finish page 
!define MUI_FINISHPAGE_RUN "$INSTDIR\${PRODUCT_NAME}.exe" 
;!define MUI_FINISHPAGE_SHOWREADME "$INSTDIR\README.md" 
!insertmacro MUI_PAGE_FINISH 
 
; Uninstaller pages 
!insertmacro MUI_UNPAGE_INSTFILES 
 
; Language files 
!insertmacro MUI_LANGUAGE "English" 
 
; MUI end ------ 
 
Name "${PRODUCT_NAME} ${PRODUCT_VERSION}" 
OutFile "CoreTracker_Installer_x86_x64_%VERSION%.exe" 
InstallDir "$PROGRAMFILES\${PRODUCT_NAME}" 
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" "" 
ShowInstDetails show 
ShowUnInstDetails show 
 
Function .onInit 
ReadRegStr $R0 HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}" "UninstallString" 
  StrCmp $R0 "" done 
   
  MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION "${PRODUCT_NAME} is already installed. $\n$\nClick `OK` to remove the previous version or `Cancel` to cancel this upgrade." IDOK uninst 
  Abort 
    
  ;Run the uninstaller 
  uninst: 
    ClearErrors 
    ExecWait '$R0 _?=$INSTDIR' ;Do not copy the uninstaller to a temp file 
    
    IfErrors no_remove_uninstaller done 
      ;You can either use Delete /REBOOTOK in the uninstaller or add some code 
      ;here to remove the uninstaller. Use a registry key to check 
      ;whether the user has chosen to uninstall. If you are using an uninstaller 
      ;components page, make sure all sections are uninstalled. 
    no_remove_uninstaller: 
    
  done: 
  !insertmacro MUI_LANGDLL_DISPLAY 
FunctionEnd 
 
Section "MainSection" SEC01 
  SetOutPath "$INSTDIR" 
  SetOverwrite ifnewer 
  ; packing file
  File "bin\Release\x32\Newtonsoft.Json.x32.dll"
  File "bin\Release\x32\OpenHardwareMonitorLib.x32.dll"
  File "bin\Release\x32\CoreTracker.x32.exe"
  File "bin\Release\x64\Newtonsoft.Json.x64.dll"
  File "bin\Release\x64\OpenHardwareMonitorLib.x64.dll"
  File "bin\Release\x64\CoreTracker.x64.exe"
  File "installer\_install.bat" 
  File "installer\_uninstall.bat"
  File "installer\CoreTrackerHelper.x32.exe"
  File "installer\CoreTrackerHelper.x64.exe"
  File "README.md" 
  CreateDirectory "$SMPROGRAMS\CoreTracker" 
  CreateShortCut "$SMPROGRAMS\CoreTracker\CoreTracker.lnk" "$INSTDIR\CoreTracker.exe" 
  CreateShortCut "$DESKTOP\CoreTracker.lnk" "$INSTDIR\CoreTracker.exe" 
SectionEnd 
 
Section -AdditionalIcons 
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}" 
  CreateShortCut "$SMPROGRAMS\CoreTracker\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url" 
  CreateShortCut "$SMPROGRAMS\CoreTracker\Uninstall.lnk" "$INSTDIR\uninst.exe" 
SectionEnd 
 
Section -Post
  ExecWait "$INSTDIR\_install.bat" 
  WriteUninstaller "$INSTDIR\uninst.exe" 
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\CoreTracker.exe" 
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)" 
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe" 
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\CoreTracker.exe" 
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}" 
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}" 
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}" 
SectionEnd 
 
 
Function un.onUninstSuccess 
  HideWindow 
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer." 
FunctionEnd 
 
Function un.onInit 
!insertmacro MUI_UNGETLANGUAGE 
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2 
  Abort 
FunctionEnd 
 
Section Uninstall 
  ExecWait "$INSTDIR\_uninstall.bat" 
  Delete "$INSTDIR\${PRODUCT_NAME}.url" 
  Delete "$INSTDIR\uninst.exe" 
  Delete "$INSTDIR\README.md" 
  Delete "$INSTDIR\_install.bat" 
  Delete "$INSTDIR\_uninstall.bat" 
  Delete "$INSTDIR\CoreTracker.exe" 
  Delete "$INSTDIR\Newtonsoft.Json.dll" 
  Delete "$INSTDIR\OpenHardwareMonitorLib.dll"  
  Delete "$INSTDIR\CoreTracker.x64.exe" 
  Delete "$INSTDIR\OpenHardwareMonitorLib.x64.dll" 
  Delete "$INSTDIR\Newtonsoft.Json.x64.dll" 
  Delete "$INSTDIR\CoreTracker.x32.exe" 
  Delete "$INSTDIR\Newtonsoft.Json.x32.dll" 
  Delete "$INSTDIR\OpenHardwareMonitorLib.x32.dll" 
  Delete "$INSTDIR\NULL" 
  Delete "$INSTDIR\CoreTrackerHelper.x32.exe"
  Delete "$INSTDIR\CoreTrackerHelper.x64.exe"

  Delete "$SMPROGRAMS\CoreTracker\Uninstall.lnk" 
  Delete "$SMPROGRAMS\CoreTracker\Website.lnk" 
  Delete "$DESKTOP\CoreTracker.lnk" 
  Delete "$SMPROGRAMS\CoreTracker\CoreTracker.lnk" 
 
  RMDir "$SMPROGRAMS\CoreTracker" 
  RMDir "$INSTDIR"
  SetOutPath $INSTDIR
  RMDir /r /REBOOTOK $INSTDIR
 
  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" 
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}" 
  SetAutoClose true 
SectionEnd 

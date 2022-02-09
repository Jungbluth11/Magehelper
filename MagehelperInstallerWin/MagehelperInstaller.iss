#define MyAppName "Magehelper"
#define MyAppVersion "3.0.0"
#define MyAppPublisher "Jungbluth"
#define MyAppURL "https://www.orkenspalter.de/index.php?thread/22038-dsa-magehelper/"
#define MyAppExeName "magehelper.exe"

[Setup]
AppId={{4A7C697F-124E-4E46-949F-015D6407671B}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
UsedUserAreasWarning=no
OutputBaseFilename=MagehelperInstaller
Compression=lzma
SolidCompression=yes
WizardStyle=modern
ChangesAssociations=yes

[Languages]
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}";
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Dirs]
Name: "{localappdata}\magehelper\updaterDownloads"
Name: "{localappdata}\magehelper\config"

[Files]
Source: "..\MagehelperCore\bin\Release\net5.0\MagehelperCore.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperCore\bin\Release\net5.0\MagehelperCore.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperCore\bin\Release\net5.0\MagehelperCore.deps"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperCore\bin\Release\net5.0\MagehelperCore.runtimeconfig"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\DSAUtils.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperWPF\bin\Release\net5.0-windows\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperWPF\bin\Release\net5.0-windows\magehelper.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperWPF\bin\Release\net5.0-windows\magehelper.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperWPF\bin\Release\net5.0-windows\magehelper.deps"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperWPF\bin\Release\net5.0-windows\magehelper.runtimeconfig"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperWPF\bin\Release\net5.0-windows\NumericUpDownLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperWPF\bin\Release\net5.0-windows\BaseSettings\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\MagehelperUpdater\bin\Release\net5.0-windows\updater.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperUpdater\bin\Release\net5.0-windows\updater.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperUpdater\bin\Release\net5.0-windows\updater.deps"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\MagehelperUpdater\bin\Release\net5.0-windows\updater.runtimeconfig"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\files.ico"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall

[Registry]
Root: HKLM; Subkey: "Software\Classes\.magehelper"; ValueType: string; ValueName: ""; ValueData: "magehelper.file"; Flags: uninsdeletevalue
Root: HKLM; Subkey: "Software\Classes\magehelper.file"; ValueType: string; ValueName: ""; ValueData: "Magehelper-Datei"; Flags: uninsdeletekey
Root: HKLM; Subkey: "Software\Classes\magehelper.file\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKLM; Subkey: "Software\Classes\magehelper.file\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\files.ico" 
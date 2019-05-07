; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "PdfPasswordRemover"
#define MyAppVersion "1.0.0.1"
#define MyAppPublisher "H�mmer Electronics"
#define MyAppURL "softwareload24.de.tl"
#define MyAppExeName "PdfPasswordRemover.exe"
#define MyPath "C:\Users\tim\Desktop\Updaten_Snyk\PdfPasswordRemover"
#define MyCopyRight "Copyright (�) H�mmer Electronics"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{C8AC9811-8843-44AB-8CD4-01A262AAB3A9}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
VersionInfoVersion={#MyAppVersion}
VersionInfoProductVersion={#MyAppVersion}
AppCopyright={#MyCopyRight}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile={#MyPath}\License.txt
OutputDir={#MyPath}\Setup
OutputBaseFilename=PdfPasswordRemover-Setup
SetupIconFile={#MyPath}\Icon.ico
UninstallDisplayIcon={#MyPath}\Icon.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "{#MyPath}\PdfPasswordRemover\bin\Debug\PdfPasswordRemover.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyPath}\PdfPasswordRemover\bin\Debug\itextsharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyPath}\PdfPasswordRemover\bin\Debug\itextsharp.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyPath}\PdfPasswordRemover\bin\Debug\Languages.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyPath}\PdfPasswordRemover\bin\Debug\languages\*"; DestDir: "{app}\languages\"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent


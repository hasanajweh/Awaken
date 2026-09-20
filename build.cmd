@echo off
setlocal

set "CSC=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\csc.exe"

if not exist "%CSC%" (
  echo Error: the Windows .NET Framework compiler is unavailable.
  exit /b 1
)

if not exist "publish" mkdir "publish"

"%CSC%" /nologo /target:winexe /platform:x86 /optimize+ /debug- /warn:4 /out:"publish\Awaken.exe" "Program.cs"
exit /b %ERRORLEVEL%

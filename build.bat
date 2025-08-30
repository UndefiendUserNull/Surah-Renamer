@echo off

set NAME=QuranRenamer
set DIST=bin

REM Clean old %DIST% folder
if exist %DIST% rmdir /s /q %DIST%
mkdir %DIST%

REM Windows build (exe)
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o %DIST%
rename %DIST%\%NAME%.exe %NAME%-win.exe

REM Linux build (no extension)
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o %DIST%
rename %DIST%\%NAME% %NAME%-linux

REM Mac build (no extension)
dotnet publish -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o %DIST%
rename %DIST%\%NAME% %NAME%-mac

echo.
echo ✅ Builds complete! Check the "%DIST%" folder:
echo   %NAME%-win.exe
echo   %NAME%-linux
echo   %NAME%-mac

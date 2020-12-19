@REM Set current directory if started "Run as administrator"
@setlocal enableextensions
@cd /d "%~dp0"

@REM Call Utility
.\IntermediatesRemoverCli.exe -r "C:\Projects"
PAUSE
@ECHO OFF
IF EXIST %1 GOTO FILEDROP
ECHO Please drop directory

GOTO EXIT

:FILEDROP
cd %1
@ECHO ON 
for /R %%i in (*.moc) do copy %%i %%i.bytes
for /R %%i in (*.mtn) do copy %%i %%i.bytes

:EXIT
pause
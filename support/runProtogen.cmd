@echo off

SET TOOL=protogen.exe
SET PROPS=-p:lightFramework
SET SRCDIR=".\mesos"
SET OUTDIR=".\out"

REM prepare dirs...
RMDIR /S %OUTDIR%
MKDIR %OUTDIR%

REM generate...
%TOOL% -i:%SRCDIR%\mesos.proto -o:%OUTDIR%\mesos.cs %PROPS%
%TOOL% -i:%SRCDIR%\scheduler\scheduler.proto -o:%OUTDIR%\scheduler.cs %PROPS%
%TOOL% -i:%SRCDIR%\executor\executor.proto -o:%OUTDIR%\executor.cs %PROPS%
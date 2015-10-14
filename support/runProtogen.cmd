@echo off

SET TOOL=protogen.exe
SET SRCDIR=".\mesos"
SET OUTDIR=".\out"

REM prepare dirs...
RMDIR /S %OUTDIR%
MKDIR %OUTDIR%
MKDIR %OUTDIR%\v1

REM generate...
%TOOL% -i:%SRCDIR%\mesos.proto -o:%OUTDIR%\mesos.cs
%TOOL% -i:%SRCDIR%\scheduler\scheduler.proto -o:%OUTDIR%\scheduler.cs
%TOOL% -i:%SRCDIR%\executor\executor.proto -o:%OUTDIR%\executor.cs
%TOOL% -i:%SRCDIR%\v1\mesos.proto -o:%OUTDIR%\v1\mesos.cs
%TOOL% -i:%SRCDIR%\v1\scheduler\scheduler.proto -o:%OUTDIR%\v1\scheduler.cs
%TOOL% -i:%SRCDIR%\v1\executor\executor.proto -o:%OUTDIR%\v1\executor.cs
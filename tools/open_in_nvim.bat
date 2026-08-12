@echo off
setlocal

set "NVIM=C:\Program Files\Neovim\bin\nvim.exe"

rem Self-locate the project root as the parent of this script's directory
rem (this script must live at <project>\tools\open_in_nvim.bat) instead of
rem relying on Godot's {project} placeholder, which is unreliable for the
rem Dotnet/C# external editor (godotengine/godot#81845).
for %%I in ("%~dp0..") do set "PROJECT=%%~fI"

rem Windows named pipes live in the \\.\pipe\ namespace, not the regular
rem filesystem, so a raw drive-letter path fails with "permission denied".
rem This sanitization must match godot.lua's so both sides agree.
set "PIPENAME=%PROJECT: =-%"
set "PIPENAME=%PIPENAME::=-%"
set "PIPENAME=%PIPENAME:\=-%"
set "PIPE=\\.\pipe\godot-nvim-%PIPENAME%"

set "FILE=%~1"
set "LINE=%~2"
set "COL=%~3"

rem Vim's :e splits on unescaped spaces, so escape them for the path in the command.
set "VIMFILE=%FILE: =\ %"

"%NVIM%" --server "%PIPE%" --remote-send "<C-\><C-N>:e %VIMFILE%<CR>:call cursor(%LINE%+1,%COL%)<CR>"

endlocal

#!/usr/bin/env bash
# Linux/macOS port of open_in_nvim.bat
set -euo pipefail

NVIM="nvim"

# Self-locate the project root as the parent of this script's directory
# (this script must live at <project>/tools/open_in_nvim.sh) instead of
# relying on Godot's {project} placeholder, which is unreliable for the
# Dotnet/C# external editor (godotengine/godot#81845).
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT="$(cd "$SCRIPT_DIR/.." && pwd)"

# On Unix, godot.lua starts its server on a socket file directly inside the
# project directory (no sanitization needed), so match that here.
PIPE="${PROJECT}/server.pipe"

FILE="$1"
LINE="$2"
COL="$3"

# Vim's :e splits on unescaped spaces, so escape them for the path in the command.
VIMFILE="${FILE// /\\ }"

"$NVIM" --server "$PIPE" --remote-send "<C-\\><C-N>:e ${VIMFILE}<CR>:call cursor($((LINE+1)),${COL})<CR>"

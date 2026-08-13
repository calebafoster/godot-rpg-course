# Dungeon RPG

Godot 4.7 project using C# (.NET, Forward+ renderer). Early-stage scaffold — most
directories currently have only placeholder content.

## Structure

- `project.godot` — Godot project config (assembly name `Dungeon RPG`, icon at
  `res://Assets/Icon.png`).
- `Dungeon RPG.csproj` / `Dungeon RPG.sln` — .NET project (`net8.0`, root
  namespace `DungeonRPG`).
- `Scenes/` — `.tscn` scene files (e.g. `Characters/Player/player.tscn`,
  `Levels/main.tscn`).
- `Scripts/` — C# scripts, mirroring the `Scenes/` layout (e.g.
  `Levels/Main.cs`).
- `Assets/` — art/model assets, including a large bundled dungeon asset pack
  under `Assets/Models/Dungeon/`.
- `tools/` — helper scripts (see below).

## tools/open_in_nvim.{bat,sh}

External-editor hook for Godot's C# editor integration: Godot invokes this
script with the file path (and optionally line/col) and it tells an already-running
Neovim instance to open it, via `nvim --server ... --remote-send`. Note: on Linux,
Godot's dotnet custom editor only passes the file path — line and col are not sent.

This only works alongside the Neovim-side config at
`~/AppData/Local/nvim/lua/config/godot.lua` (Windows) — that file detects a
Godot project (by finding `project.godot` in `.` or `..`) and starts a
server for it. **Both sides must agree on the server address**, and the
scheme differs by OS:

- **Windows**: named pipe at `\\.\pipe\godot-nvim-<sanitized-project-path>`,
  where sanitization replaces spaces, `:`, and `\` with `-`. Implemented in
  `open_in_nvim.bat` and the `win32` branch of `godot.lua`.
- **Unix**: a plain socket file at `<project-path>/server.pipe` — no name
  sanitization needed. Implemented in `open_in_nvim.sh` and the `else`
  branch of `godot.lua`.

`open_in_nvim.sh` was tested and confirmed working on Linux as of 2026-08-12.
Neovim must already be running from the project root (so `godot.lua` creates
`server.pipe`) before clicking a script in Godot.

If the Linux pipe scheme ever changes, update both `open_in_nvim.sh` and the
Unix branch of `godot.lua` together — they must stay in sync.

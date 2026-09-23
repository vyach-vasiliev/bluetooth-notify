# Common commands

Run from repository root in PowerShell:

- Build: `dotnet build BluetoothNotify.slnx`
- Tests: `dotnet test BluetoothNotify.slnx`
- Run the app: `dotnet run --project src/BluetoothNotify.App/BluetoothNotify.App.csproj -- --show`
- Build website: `npm ci; npm run build` with working directory `site`.
- Build installer: `./tools/Build-Installer.ps1` (requires Inno Setup 6; pass `-InnoSetupCompiler 'C:\path\ISCC.exe'` if it is not in the script's standard locations).

The installer build script also runs tests unless `-SkipTests` is supplied.
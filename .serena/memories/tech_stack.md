# Tech stack

- Desktop: C# / .NET 10 WPF, Windows App SDK 1.8, win-x64; nullable and implicit usings enabled. NuGet versions are centralized in `Directory.Packages.props`.
- Tests: xUnit in `tests/BluetoothNotify.Tests`.
- Website: Vite and npm under `site`; static hosting configuration uses that directory as root.
- Installer: Inno Setup 6 script at `installer/BluetoothNotify.iss`; `tools/Build-Installer.ps1` orchestrates release build, tests, publish, and installer compilation.
- GitHub Actions release workflow runs only on pushed `v*` tags and validates the tag against the central version in `Directory.Build.props`.
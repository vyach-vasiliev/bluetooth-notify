# Completion and verification

For code changes, inspect the final diff and use `git diff --check`. Do not add/run tests unless the user asks for verification; when verification is requested, use the narrowest relevant build/test command.

Release packaging: `./tools/Build-Installer.ps1` runs restore, Release build, tests, win-x64 publish, and Inno Setup compilation. Ensure the pushed tag is exactly `v` plus the version in `Directory.Build.props`; the release workflow builds only on version-tag pushes and attaches the installer to the GitHub Release.

Before completing Serena onboarding, check the saved memories with `serena memories check` if the Serena CLI is available.
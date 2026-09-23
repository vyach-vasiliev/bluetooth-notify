# Project conventions

- Keep UI behavior in MVVM view models and bind from WPF views; follow existing ObservableObject, RelayCommand, and AsyncCommand patterns.
- Shared visual styles and theme resources belong in `src/BluetoothNotify.App/Themes/Controls.xaml` and related theme dictionaries. Prefer existing dynamic theme keys.
- User-visible strings must be localized in all existing `Strings*.resx` files and exposed through `Properties/Strings.cs`.
- Keep Windows shell/native integration inside the existing service layer.
- Keep the app's version in `Directory.Build.props` so packaging and release tags share one source of truth.
- Do not commit machine-specific Serena state, cache, credentials, or generated build artifacts.
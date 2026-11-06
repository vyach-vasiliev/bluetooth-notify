using System.Diagnostics;
using BluetoothNotify.App.Infrastructure;
using BluetoothNotify.App.Services;

namespace BluetoothNotify.App.ViewModels;

public sealed class UpdateViewModel : ObservableObject
{
    private readonly IUpdateService _service;
    private readonly Action<Uri> _openDownload;
    private readonly SemaphoreSlim _checkGate = new(1, 1);
    private UpdateRelease? _release;
    private CheckState _state;

    public UpdateViewModel(IUpdateService service, Action<Uri>? openDownload = null)
    {
        _service = service;
        _openDownload = openDownload ?? (uri => Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true }));
        CheckCommand = new AsyncCommand(() => CheckAsync(), () => CanCheck);
        DownloadCommand = new RelayCommand(OpenDownload, () => CanDownload);
    }

    public AsyncCommand CheckCommand { get; }
    public RelayCommand DownloadCommand { get; }
    public bool CanCheck => _state != CheckState.Checking;
    public bool CanDownload => _release is not null;
    public string CurrentVersionText => string.Format(Properties.Strings.CurrentVersion, _service.CurrentVersion);
    public string AvailableVersionText => string.Format(Properties.Strings.UpdateAvailable, _release?.Version);
    public string HeaderStatusText => CanDownload ? AvailableVersionText
        : _state == CheckState.UpToDate ? Properties.Strings.AppUpToDate : string.Empty;
    public bool ShowStatusDetails => _state is not (CheckState.Available or CheckState.UpToDate);
    public string StatusText => _state switch
    {
        CheckState.Checking => Properties.Strings.CheckingForUpdates,
        CheckState.Available => AvailableVersionText,
        CheckState.UpToDate => Properties.Strings.AppUpToDate,
        CheckState.Failed => Properties.Strings.UpdateCheckFailed,
        CheckState.OpenFailed => Properties.Strings.UpdateDownloadOpenFailed,
        _ => Properties.Strings.UpdateCheckDescription
    };

    public async Task CheckAsync(CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested || !_checkGate.Wait(0)) return;
        SetState(CheckState.Checking);
        try
        {
            var release = await _service.CheckAsync(cancellationToken);
            _release = release;
            OnPropertyChanged(nameof(CanDownload));
            OnPropertyChanged(nameof(AvailableVersionText));
            DownloadCommand.RaiseCanExecuteChanged();
            SetState(release is null ? CheckState.UpToDate : CheckState.Available);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            SetState(_release is null ? CheckState.Idle : CheckState.Available);
        }
        catch (Exception)
        {
            SetState(CheckState.Failed);
        }
        finally { _checkGate.Release(); }
    }

    private void OpenDownload()
    {
        if (_release is null) return;
        try { _openDownload(_release.InstallerUri); }
        catch (Exception) { SetState(CheckState.OpenFailed); }
    }

    private void SetState(CheckState state)
    {
        _state = state;
        OnPropertyChanged(nameof(CanCheck));
        OnPropertyChanged(nameof(StatusText));
        OnPropertyChanged(nameof(HeaderStatusText));
        OnPropertyChanged(nameof(ShowStatusDetails));
        CheckCommand.RaiseCanExecuteChanged();
    }

    private enum CheckState { Idle, Checking, Available, UpToDate, Failed, OpenFailed }
}

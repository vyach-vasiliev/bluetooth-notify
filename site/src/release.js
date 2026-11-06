// Update this version for every new stable release (see /AGENTS.md).
const version = '1.3.2'
const repositoryUrl = 'https://github.com/vyach-vasiliev/bluetooth-notify'
const installerName = `BluetoothNotify-Setup-${version}-x64.exe`

export const latestRelease = {
  version,
  installerName,
  downloadUrl: `${repositoryUrl}/releases/download/v${version}/${installerName}`,
  releaseUrl: `${repositoryUrl}/releases/tag/v${version}`,
}

import { localeOptions, translations } from './i18n.js'
import { latestRelease } from './release.js'

export const softwareApplicationSchemaId = 'bluetooth-notify-software-application'
const siteUrl = 'https://bluetooth-notify.onrender.com/'

export function createSoftwareApplicationSchema(locale = 'en') {
  const copy = translations[locale] ?? translations.en
  return {
    '@context': 'https://schema.org',
    '@type': 'SoftwareApplication',
    '@id': `${siteUrl}#software-application`,
    name: 'Bluetooth Notify',
    url: siteUrl,
    description: copy.meta.description,
    applicationCategory: 'UtilitiesApplication',
    operatingSystem: 'Windows 11 22H2 or newer',
    processorRequirements: 'x64',
    softwareRequirements: '.NET Desktop Runtime 10 x64; Windows App Runtime 1.8 x64',
    softwareVersion: latestRelease.version,
    downloadUrl: latestRelease.downloadUrl,
    releaseNotes: latestRelease.releaseUrl,
    license: 'https://www.gnu.org/licenses/agpl-3.0.html',
    author: {
      '@type': 'Person',
      name: 'Vasilev Viacheslav',
      url: 'https://github.com/vyach-vasiliev/',
    },
    image: `${siteUrl}screenshots/main_light.png`,
    screenshot: ['main_light.png', 'settings_light.png', 'main_dark.png']
      .map((file) => `${siteUrl}screenshots/${file}`),
    inLanguage: localeOptions.map((option) => option.htmlLang),
    isAccessibleForFree: true,
    offers: {
      '@type': 'Offer',
      price: 0,
      priceCurrency: 'USD',
      url: latestRelease.downloadUrl,
    },
  }
}

export function serializeSoftwareApplicationSchema(locale = 'en') {
  return JSON.stringify(createSoftwareApplicationSchema(locale)).replace(/</g, '\\u003c')
}

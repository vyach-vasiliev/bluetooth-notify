<script setup>
import { computed, nextTick, onMounted, onUnmounted, ref, watch } from 'vue'
import {
  ArrowRight,
  BatteryCharging,
  BellRing,
  Bluetooth,
  Check,
  ChevronDown,
  ChevronRight,
  CircleCheck,
  Copy,
  Eye,
  Languages,
  Monitor,
  Moon,
  Settings2,
  ShieldCheck,
  Sun,
} from '@lucide/vue'
import { localeOptions, resolveLocale, translations } from './i18n'

const previewDetails = [
  { id: 'panel', image: '/screenshots/main_light.png' },
  { id: 'settings', image: '/screenshots/settings_light.png' },
  { id: 'dark', image: '/screenshots/main_dark.png' },
]

const requestedLocale = new URLSearchParams(window.location.search).get('lang')
const initialLocale = [requestedLocale, ...(navigator.languages ?? [navigator.language])]
  .map((value) => resolveLocale(value, null))
  .find(Boolean) ?? 'en'

const locale = ref(initialLocale)
const activePreview = ref('panel')
const copied = ref(false)
const cursorAura = ref(null)
const languageSwitcher = ref(null)
const languageTrigger = ref(null)
const languageOptionElements = ref([])
const languageMenuOpen = ref(false)
const copy = computed(() => translations[locale.value])
const currentLocale = computed(() => localeOptions.find((option) => option.code === locale.value))
const currentLocaleIndex = computed(() => localeOptions.findIndex((option) => option.code === locale.value))
const previews = computed(() => previewDetails.map((preview, index) => ({
  ...preview,
  ...copy.value.interface.previews[index],
})))
const currentPreview = computed(() => previews.value.find((item) => item.id === activePreview.value))
const buildCommand = 'dotnet run --project .\\src\\BluetoothNotify.App\\BluetoothNotify.App.csproj -c Release'

watch(locale, (value) => {
  const selectedLocale = localeOptions.find((option) => option.code === value) ?? localeOptions[0]
  document.documentElement.lang = selectedLocale.htmlLang
  document.title = copy.value.meta.title
  document.querySelector('meta[name="description"]')?.setAttribute('content', copy.value.meta.description)

  const url = new URL(window.location.href)
  url.searchParams.set('lang', value)
  window.history.replaceState(null, '', `${url.pathname}${url.search}${url.hash}`)
  copied.value = false
}, { immediate: true })

let cursorFrame = 0
let pointerX = 0
let pointerY = 0
let cursorEnabled = false

function renderCursor() {
  if (cursorAura.value) {
    cursorAura.value.style.transform = `translate3d(${pointerX - 170}px, ${pointerY - 170}px, 0)`
    cursorAura.value.style.opacity = '1'
  }
  cursorFrame = 0
}

function handlePointerMove(event) {
  pointerX = event.clientX
  pointerY = event.clientY
  if (!cursorFrame) cursorFrame = window.requestAnimationFrame(renderCursor)
}

function handlePointerOut(event) {
  if (!event.relatedTarget && cursorAura.value) cursorAura.value.style.opacity = '0'
}

function setLanguageOptionElement(element, index) {
  if (element) languageOptionElements.value[index] = element
}

async function openLanguageMenu() {
  languageMenuOpen.value = true
  await nextTick()
  languageOptionElements.value[Math.max(0, currentLocaleIndex.value)]?.focus()
}

async function closeLanguageMenu(restoreFocus = false) {
  languageMenuOpen.value = false
  if (restoreFocus) {
    await nextTick()
    languageTrigger.value?.focus()
  }
}

function toggleLanguageMenu() {
  if (languageMenuOpen.value) closeLanguageMenu()
  else openLanguageMenu()
}

function selectLanguage(code) {
  locale.value = code
  closeLanguageMenu(true)
}

function moveLanguageFocus(offset) {
  const focusedIndex = languageOptionElements.value.indexOf(document.activeElement)
  const startIndex = focusedIndex >= 0 ? focusedIndex : Math.max(0, currentLocaleIndex.value)
  const nextIndex = (startIndex + offset + localeOptions.length) % localeOptions.length
  languageOptionElements.value[nextIndex]?.focus()
}

function handleLanguageMenuKeydown(event) {
  if (event.key === 'ArrowDown') {
    event.preventDefault()
    moveLanguageFocus(1)
  } else if (event.key === 'ArrowUp') {
    event.preventDefault()
    moveLanguageFocus(-1)
  } else if (event.key === 'Home') {
    event.preventDefault()
    languageOptionElements.value[0]?.focus()
  } else if (event.key === 'End') {
    event.preventDefault()
    languageOptionElements.value.at(-1)?.focus()
  } else if (event.key === 'Escape') {
    event.preventDefault()
    closeLanguageMenu(true)
  }
}

function handleLanguageTriggerKeydown(event) {
  if (event.key === 'ArrowDown' || event.key === 'ArrowUp') {
    event.preventDefault()
    openLanguageMenu()
  }
}

function handleLanguageFocusOut() {
  nextTick(() => {
    if (!languageSwitcher.value?.contains(document.activeElement)) closeLanguageMenu()
  })
}

function handleDocumentPointerDown(event) {
  if (languageMenuOpen.value && !languageSwitcher.value?.contains(event.target)) closeLanguageMenu()
}

onMounted(() => {
  cursorEnabled = window.matchMedia('(hover: hover) and (pointer: fine)').matches
    && !window.matchMedia('(prefers-reduced-motion: reduce)').matches

  if (cursorEnabled) {
    window.addEventListener('pointermove', handlePointerMove, { passive: true })
    window.addEventListener('pointerout', handlePointerOut)
  }
  document.addEventListener('pointerdown', handleDocumentPointerDown)
})

onUnmounted(() => {
  window.removeEventListener('pointermove', handlePointerMove)
  window.removeEventListener('pointerout', handlePointerOut)
  document.removeEventListener('pointerdown', handleDocumentPointerDown)
  if (cursorFrame) window.cancelAnimationFrame(cursorFrame)
})

async function copyCommand() {
  try {
    await navigator.clipboard.writeText(buildCommand)
    copied.value = true
    window.setTimeout(() => (copied.value = false), 1800)
  } catch {
    copied.value = false
  }
}
</script>

<template>
  <div class="site-stage min-h-screen overflow-hidden bg-[var(--paper)] text-[var(--ink)]">
    <div class="signal-grid" aria-hidden="true"></div>
    <div class="background-signal signal-a" aria-hidden="true"></div>
    <div class="background-signal signal-b" aria-hidden="true"></div>
    <div class="background-signal signal-c" aria-hidden="true"></div>
    <div ref="cursorAura" class="cursor-aura" aria-hidden="true"></div>

    <header class="fixed inset-x-0 top-0 z-50 border-b-2 border-[var(--ink)] bg-[var(--paper)]/95 backdrop-blur-md">
      <div class="page-shell flex h-18 items-center justify-between">
        <a href="#top" class="brand-link">
          <span class="brand-mark"><Bluetooth :size="21" :stroke-width="2" aria-hidden="true" /></span>
          <span class="header-brand-name text-[15px] font-extrabold uppercase tracking-[-0.02em] sm:text-base">Bluetooth Notify</span>
        </a>

        <nav :aria-label="copy.navigationLabel" class="hidden items-center gap-1 lg:flex">
          <a class="nav-block" href="#features">{{ copy.nav.features }}</a>
          <a class="nav-block" href="#interface">{{ copy.nav.interface }}</a>
          <a class="nav-block" href="#requirements">{{ copy.nav.requirements }}</a>
        </nav>

        <div class="header-actions">
          <div ref="languageSwitcher" class="language-switcher" @focusout="handleLanguageFocusOut">
            <button
              ref="languageTrigger"
              type="button"
              class="language-trigger"
              :class="{ 'language-trigger-open': languageMenuOpen }"
              aria-haspopup="listbox"
              aria-controls="language-menu"
              :aria-expanded="languageMenuOpen"
              :aria-label="`${copy.languageLabel}: ${currentLocale.label}`"
              @click="toggleLanguageMenu"
              @keydown="handleLanguageTriggerKeydown"
            >
              <Languages :size="17" :stroke-width="2" aria-hidden="true" />
              <span class="language-label-full">{{ currentLocale.label }}</span>
              <span class="language-label-short">{{ currentLocale.short }}</span>
              <ChevronDown class="language-chevron" :size="16" :stroke-width="2" aria-hidden="true" />
            </button>

            <Transition name="language-menu">
              <div
                v-if="languageMenuOpen"
                id="language-menu"
                class="language-menu"
                role="listbox"
                :aria-label="copy.languageLabel"
                @keydown="handleLanguageMenuKeydown"
              >
                <button
                  v-for="(option, index) in localeOptions"
                  :key="option.code"
                  :ref="(element) => setLanguageOptionElement(element, index)"
                  type="button"
                  class="language-option"
                  :class="{ 'language-option-selected': option.code === locale }"
                  role="option"
                  tabindex="-1"
                  :aria-selected="option.code === locale"
                  @click="selectLanguage(option.code)"
                >
                  <span class="language-option-code">{{ option.short }}</span>
                  <span class="language-option-name">{{ option.label }}</span>
                  <Check
                    class="language-option-check"
                    :class="{ 'language-option-check-hidden': option.code !== locale }"
                    :size="18"
                    :stroke-width="2.2"
                    aria-hidden="true"
                  />
                </button>
              </div>
            </Transition>
          </div>

          <a href="#build" class="header-cta">
            <span class="hidden sm:inline">{{ copy.header.build }}</span>
            <span class="sm:hidden">{{ copy.header.buildShort }}</span>
            <ArrowRight :size="17" :stroke-width="2" aria-hidden="true" />
          </a>
        </div>
      </div>
    </header>

    <main id="top" class="relative z-10">
      <section class="hero-section">
        <div class="page-shell grid items-center gap-16 lg:grid-cols-[0.92fr_1.08fr] lg:gap-12">
          <div class="relative z-10 max-w-2xl">
            <div class="signal-label">
              <span class="signal-bars" aria-hidden="true"><i></i><i></i><i></i></span>
              {{ copy.hero.eyebrow }}
            </div>

            <h1 class="hero-title mt-7">
              {{ copy.hero.title[0] }}<br />
              <span class="hero-title-accent">{{ copy.hero.title[1] }}</span>
            </h1>
            <p class="mt-7 max-w-xl text-pretty text-lg leading-8 text-slate-700 sm:text-xl">
              {{ copy.hero.description }}
            </p>

            <div class="mt-9 flex flex-col gap-3 sm:flex-row">
              <a href="#interface" class="action-primary">
                {{ copy.hero.explore }}
                <ArrowRight :size="19" :stroke-width="2" aria-hidden="true" />
              </a>
              <a href="#features" class="action-ghost">{{ copy.hero.why }}</a>
            </div>

            <div class="hero-meta mt-11">
              <div v-for="(item, index) in copy.hero.meta" :key="item"><span>0{{ index + 1 }}</span>{{ item }}</div>
            </div>
          </div>

          <div class="hero-rig">
            <div class="orbit orbit-one" aria-hidden="true"></div>
            <div class="orbit orbit-two" aria-hidden="true"></div>
            <div class="axis-mark axis-top" aria-hidden="true">BT / 90%</div>
            <div class="axis-mark axis-bottom" aria-hidden="true">{{ copy.hero.signalLive }}</div>

            <div class="app-shot-card">
              <div class="shot-toolbar">
                <span>BLUETOOTH_NOTIFY.EXE</span>
                <span class="flex items-center gap-2"><i></i> {{ copy.hero.live }}</span>
              </div>
              <img
                src="/screenshots/main_light.png"
                :alt="copy.hero.mainAlt"
                width="874"
                height="746"
                class="image-outline block w-full rounded-[10px]"
              />
            </div>

            <div class="tray-cutout">
              <span>{{ copy.hero.trayCaption }}</span>
              <img
                src="/screenshots/tray_light.png"
                :alt="copy.hero.trayAlt"
                width="478"
                height="92"
                class="image-outline mt-2 block w-full rounded-md"
              />
            </div>

            <div class="charge-stamp">
              <span class="charge-stamp-number tabular-nums">90</span>
              <span class="charge-stamp-copy">%<br />{{ copy.hero.charged }}</span>
            </div>
          </div>
        </div>
      </section>

      <div class="ticker" aria-hidden="true">
        <div class="ticker-track">
          <span v-for="index in 8" :key="index">
            <template v-for="(part, partIndex) in copy.ticker.split('✦')" :key="partIndex">
              {{ part }}<i v-if="partIndex < copy.ticker.split('✦').length - 1">✦</i>
            </template>
          </span>
        </div>
      </div>

      <section id="features" class="section-block scroll-mt-20">
        <div class="page-shell">
          <div class="section-heading-grid">
            <p class="section-index">{{ copy.features.index }}</p>
            <h2 class="section-title">{{ copy.features.title[0] }}<br />{{ copy.features.title[1] }}</h2>
            <p class="section-intro">
              {{ copy.features.intro }}
            </p>
          </div>

          <div class="feature-grid mt-16">
            <article class="feature-block feature-blue">
              <div class="feature-topline"><span>01</span><Eye :size="25" :stroke-width="1.8" aria-hidden="true" /></div>
              <h3>{{ copy.features.overview.title }}</h3>
              <p>{{ copy.features.overview.body }}</p>
              <div class="feature-foot">{{ copy.features.overview.foot }} <ArrowRight :size="17" aria-hidden="true" /></div>
            </article>

            <article class="feature-block feature-lime">
              <div class="feature-topline"><span>02</span><BellRing :size="25" :stroke-width="1.8" aria-hidden="true" /></div>
              <h3>{{ copy.features.alerts.title }}</h3>
              <p>{{ copy.features.alerts.body }}</p>
              <div class="level-meter" :aria-label="copy.features.alerts.meterLabel">
                <div class="level-fill"></div>
                <span class="tabular-nums">30%</span>
              </div>
            </article>

            <article class="feature-block feature-white">
              <div class="feature-topline"><span>03</span><Monitor :size="25" :stroke-width="1.8" aria-hidden="true" /></div>
              <h3>{{ copy.features.native.title }}</h3>
              <p>{{ copy.features.native.body }}</p>
              <div class="feature-foot"><ShieldCheck :size="17" aria-hidden="true" /> {{ copy.features.native.foot }}</div>
            </article>
          </div>
        </div>
      </section>

      <section id="interface" class="interface-section scroll-mt-18">
        <div class="page-shell">
          <div class="section-heading-grid section-heading-light">
            <p class="section-index">{{ copy.interface.index }}</p>
            <h2 class="section-title">{{ copy.interface.title[0] }}<br />{{ copy.interface.title[1] }}</h2>
            <p class="section-intro">{{ copy.interface.intro }}</p>
          </div>

          <div class="interface-console mt-14">
            <div class="preview-menu" role="tablist" :aria-label="copy.interface.tabsLabel">
              <button
                v-for="preview in previews"
                :key="preview.id"
                type="button"
                role="tab"
                :aria-selected="activePreview === preview.id"
                :aria-controls="`${preview.id}-panel`"
                class="preview-button"
                :class="{ 'preview-button-active': activePreview === preview.id }"
                @click="activePreview = preview.id"
              >
                <span>{{ preview.label }}</span>
                <ChevronRight :size="20" :stroke-width="2" aria-hidden="true" />
              </button>

              <div class="preview-copy">
                <span>{{ currentPreview.tag }}</span>
                <h3>{{ currentPreview.title }}</h3>
                <p>{{ currentPreview.description }}</p>
              </div>

              <div class="theme-badges">
                <span><Sun :size="16" aria-hidden="true" /> {{ copy.interface.light }}</span>
                <span><Moon :size="16" aria-hidden="true" /> {{ copy.interface.dark }}</span>
                <span><Languages :size="16" aria-hidden="true" /> {{ copy.interface.languages }}</span>
              </div>
            </div>

            <div :id="`${currentPreview.id}-panel`" role="tabpanel" class="preview-stage">
              <span class="preview-coordinate coordinate-one">X: 082</span>
              <span class="preview-coordinate coordinate-two">Y: 114</span>
              <Transition name="preview" mode="out-in">
                <img
                  :key="currentPreview.id"
                  :src="currentPreview.image"
                  :alt="currentPreview.alt"
                  width="882"
                  height="886"
                  class="image-outline preview-image"
                />
              </Transition>
            </div>
          </div>
        </div>
      </section>

      <section id="requirements" class="section-block scroll-mt-20">
        <div class="page-shell grid items-center gap-16 lg:grid-cols-[0.85fr_1.15fr]">
          <div>
            <p class="section-index">{{ copy.requirements.index }}</p>
            <h2 class="section-title mt-6">{{ copy.requirements.title }}</h2>
            <p class="mt-6 max-w-xl text-pretty text-lg leading-8 text-slate-700">
              {{ copy.requirements.description }}
            </p>

            <div class="requirement-list mt-10">
              <div><CircleCheck aria-hidden="true" /><span>{{ copy.requirements.windows }}</span><b>x64</b></div>
              <div>
                <CircleCheck aria-hidden="true" />
                <span>.NET Desktop Runtime 10 x64</span>
                <a href="https://dotnet.microsoft.com/en-us/download/dotnet/10.0" target="_blank" rel="noreferrer">{{ copy.requirements.download }}</a>
              </div>
              <div>
                <CircleCheck aria-hidden="true" />
                <span>Windows App Runtime 1.8 x64</span>
                <a href="https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/downloads-archive#version-18" target="_blank" rel="noreferrer">{{ copy.requirements.download }}</a>
              </div>
            </div>
          </div>

          <div class="settings-rig">
            <div class="settings-backdrop" aria-hidden="true">{{ copy.requirements.ready[0] }}<br />{{ copy.requirements.ready[1] }}</div>
            <div class="settings-shot">
              <div class="shot-toolbar"><span>{{ copy.requirements.toolbar }}</span><span>03</span></div>
              <img
                src="/screenshots/settings_light.png"
                :alt="copy.requirements.settingsAlt"
                width="880"
                height="884"
                class="image-outline block w-full rounded-[10px]"
              />
            </div>
            <div class="theme-sticker">
              <Settings2 :size="22" aria-hidden="true" />
              <div><strong>{{ copy.requirements.themeAware }}</strong><span>{{ copy.requirements.themes }}</span></div>
            </div>
          </div>
        </div>
      </section>

      <section id="build" class="build-section scroll-mt-20">
        <div class="page-shell">
          <div class="build-layout">
            <div>
              <div class="build-icon"><BatteryCharging :size="27" :stroke-width="1.8" aria-hidden="true" /></div>
              <p class="section-index mt-7 text-blue-200">{{ copy.build.index }}</p>
              <h2 class="build-title mt-5">{{ copy.build.title[0] }}<br />{{ copy.build.title[1] }}</h2>
              <p class="mt-6 max-w-xl text-pretty text-lg leading-8 text-blue-100/80">
                {{ copy.build.description }}
              </p>
            </div>

            <div class="command-card">
              <div class="command-label"><span>POWERSHELL</span><span>{{ copy.build.line }}</span></div>
              <code>{{ buildCommand }}</code>
              <button type="button" class="copy-command" :aria-label="copied ? copy.build.copiedAria : copy.build.copyAria" @click="copyCommand">
                <span class="copy-icon-stack" aria-hidden="true">
                  <Copy :class="{ 'copy-icon-hidden': copied }" :size="19" />
                  <Check :class="{ 'copy-icon-hidden': !copied }" class="absolute" :size="19" />
                </span>
                {{ copied ? copy.build.copied : copy.build.copy }}
              </button>
            </div>
          </div>
        </div>
      </section>
    </main>

    <footer class="relative z-10 border-t-2 border-[var(--ink)] bg-[var(--paper)]">
      <div class="page-shell flex flex-col gap-5 py-8 text-sm lg:flex-row lg:items-center lg:justify-between">
        <a href="#top" class="brand-link">
          <span class="brand-mark"><Bluetooth :size="18" aria-hidden="true" /></span>
          <span class="font-extrabold uppercase">Bluetooth Notify</span>
        </a>
        <nav class="legal-links" :aria-label="copy.footer.legalLabel">
          <a :href="`/legal/index.html?lang=${locale}#privacy`">{{ copy.footer.privacy }}</a>
          <a :href="`/legal/index.html?lang=${locale}#terms`">{{ copy.footer.terms }}</a>
          <a :href="`/legal/index.html?lang=${locale}#disclaimer`">{{ copy.footer.disclaimer }}</a>
          <a href="/legal/LICENSE.md">{{ copy.footer.license }}</a>
        </nav>
        <p class="font-semibold text-slate-600">{{ copy.footer.tagline }}</p>
      </div>
    </footer>
  </div>
</template>

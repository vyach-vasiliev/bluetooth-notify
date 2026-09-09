<script setup>
import { computed, onMounted, onUnmounted, ref } from 'vue'
import {
  ArrowRight,
  BatteryCharging,
  BellRing,
  Bluetooth,
  Check,
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

const previews = [
  {
    id: 'panel',
    label: '01 / Panel',
    tag: 'Live status',
    title: 'Every paired device in one clean view.',
    description:
      'See real connection state and the latest battery level Windows can report—without leaving your current task.',
    image: '/screenshots/main_light.png',
    alt: 'Bluetooth Notify light panel showing two paired headphones and their battery levels',
  },
  {
    id: 'settings',
    label: '02 / Settings',
    tag: 'Your thresholds',
    title: 'Useful alerts, tuned by you.',
    description:
      'Choose separate medium and low battery thresholds, switch notifications on or off, and apply changes instantly.',
    image: '/screenshots/settings_light.png',
    alt: 'Bluetooth Notify settings with language, theme, and battery notification controls',
  },
  {
    id: 'dark',
    label: '03 / Dark mode',
    tag: 'System-aware',
    title: 'Looks right in every Windows theme.',
    description:
      'Follow the system appearance or select light and dark mode yourself. The panel and tray icon stay easy to read.',
    image: '/screenshots/main_dark.png',
    alt: 'Bluetooth Notify panel in dark mode',
  },
]

const activePreview = ref('panel')
const copied = ref(false)
const cursorAura = ref(null)
const currentPreview = computed(() => previews.find((item) => item.id === activePreview.value))
const buildCommand = 'dotnet run --project .\\src\\BluetoothNotify.App\\BluetoothNotify.App.csproj -c Release'

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

onMounted(() => {
  cursorEnabled = window.matchMedia('(hover: hover) and (pointer: fine)').matches
    && !window.matchMedia('(prefers-reduced-motion: reduce)').matches

  if (cursorEnabled) {
    window.addEventListener('pointermove', handlePointerMove, { passive: true })
    window.addEventListener('pointerout', handlePointerOut)
  }
})

onUnmounted(() => {
  window.removeEventListener('pointermove', handlePointerMove)
  window.removeEventListener('pointerout', handlePointerOut)
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
          <span class="text-[15px] font-extrabold uppercase tracking-[-0.02em] sm:text-base">Bluetooth Notify</span>
        </a>

        <nav aria-label="Main navigation" class="hidden items-center gap-1 lg:flex">
          <a class="nav-block" href="#features">Features</a>
          <a class="nav-block" href="#interface">Interface</a>
          <a class="nav-block" href="#requirements">Requirements</a>
        </nav>

        <a href="#build" class="header-cta">
          <span class="hidden sm:inline">Build from source</span>
          <span class="sm:hidden">Build</span>
          <ArrowRight :size="17" :stroke-width="2" aria-hidden="true" />
        </a>
      </div>
    </header>

    <main id="top" class="relative z-10">
      <section class="hero-section">
        <div class="page-shell grid items-center gap-16 lg:grid-cols-[0.92fr_1.08fr] lg:gap-12">
          <div class="relative z-10 max-w-2xl">
            <div class="signal-label">
              <span class="signal-bars" aria-hidden="true"><i></i><i></i><i></i></span>
              Windows 11 tray utility
            </div>

            <h1 class="hero-title mt-7">
              Battery status.<br />
              <span class="hero-title-accent">Zero guesswork.</span>
            </h1>
            <p class="mt-7 max-w-xl text-pretty text-lg leading-8 text-slate-700 sm:text-xl">
              Bluetooth Notify puts device levels, connection state, and native low-battery alerts exactly where they belong: in your system tray.
            </p>

            <div class="mt-9 flex flex-col gap-3 sm:flex-row">
              <a href="#interface" class="action-primary">
                Explore the interface
                <ArrowRight :size="19" :stroke-width="2" aria-hidden="true" />
              </a>
              <a href="#features" class="action-ghost">Why it helps</a>
            </div>

            <div class="hero-meta mt-11">
              <div><span>01</span>Native alerts</div>
              <div><span>02</span>No account</div>
              <div><span>03</span>EN + RU</div>
            </div>
          </div>

          <div class="hero-rig">
            <div class="orbit orbit-one" aria-hidden="true"></div>
            <div class="orbit orbit-two" aria-hidden="true"></div>
            <div class="axis-mark axis-top" aria-hidden="true">BT / 90%</div>
            <div class="axis-mark axis-bottom" aria-hidden="true">SIGNAL: LIVE</div>

            <div class="app-shot-card">
              <div class="shot-toolbar">
                <span>BLUETOOTH_NOTIFY.EXE</span>
                <span class="flex items-center gap-2"><i></i> LIVE</span>
              </div>
              <img
                src="/screenshots/main_light.png"
                alt="Bluetooth Notify showing connected headphones with a 90 percent battery level"
                width="874"
                height="746"
                class="image-outline block w-full rounded-[10px]"
              />
            </div>

            <div class="tray-cutout">
              <span>One click away</span>
              <img
                src="/screenshots/tray_light.png"
                alt="Bluetooth Notify icon in the Windows system tray"
                width="478"
                height="92"
                class="image-outline mt-2 block w-full rounded-md"
              />
            </div>

            <div class="charge-stamp">
              <span class="charge-stamp-number tabular-nums">90</span>
              <span class="charge-stamp-copy">%<br />CHARGED</span>
            </div>
          </div>
        </div>
      </section>

      <div class="ticker" aria-hidden="true">
        <div class="ticker-track">
          <span v-for="index in 8" :key="index">REAL BATTERY DATA <i>✦</i> NATIVE WINDOWS ALERTS <i>✦</i> ALWAYS IN THE TRAY <i>✦</i></span>
        </div>
      </div>

      <section id="features" class="section-block scroll-mt-20">
        <div class="page-shell">
          <div class="section-heading-grid">
            <p class="section-index">[ 01 — FEATURES ]</p>
            <h2 class="section-title">Small app.<br />Strong signal.</h2>
            <p class="section-intro">
              A focused native companion that stays quiet until you need it—then tells you exactly what Windows knows.
            </p>
          </div>

          <div class="feature-grid mt-16">
            <article class="feature-block feature-blue">
              <div class="feature-topline"><span>01</span><Eye :size="25" :stroke-width="1.8" aria-hidden="true" /></div>
              <h3>One-click overview</h3>
              <p>Paired devices, current connection state, and the latest available battery reading in a compact panel.</p>
              <div class="feature-foot">Refreshes while open <ArrowRight :size="17" aria-hidden="true" /></div>
            </article>

            <article class="feature-block feature-lime">
              <div class="feature-topline"><span>02</span><BellRing :size="25" :stroke-width="1.8" aria-hidden="true" /></div>
              <h3>Threshold alerts</h3>
              <p>Set separate medium and low levels. Each warning fires once as the device crosses your threshold.</p>
              <div class="level-meter" aria-label="Example battery level at 30 percent">
                <div class="level-fill"></div>
                <span class="tabular-nums">30%</span>
              </div>
            </article>

            <article class="feature-block feature-white">
              <div class="feature-topline"><span>03</span><Monitor :size="25" :stroke-width="1.8" aria-hidden="true" /></div>
              <h3>Native behavior</h3>
              <p>Hover or click to open, use the tray menu, and receive standard Windows app notifications.</p>
              <div class="feature-foot"><ShieldCheck :size="17" aria-hidden="true" /> Settings stay local</div>
            </article>
          </div>
        </div>
      </section>

      <section id="interface" class="interface-section scroll-mt-18">
        <div class="page-shell">
          <div class="section-heading-grid section-heading-light">
            <p class="section-index">[ 02 — INTERFACE ]</p>
            <h2 class="section-title">Three views.<br />No clutter.</h2>
            <p class="section-intro">Switch between the live panel, notification settings, and the system-aware dark appearance.</p>
          </div>

          <div class="interface-console mt-14">
            <div class="preview-menu" role="tablist" aria-label="App screenshots">
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
                <span><Sun :size="16" aria-hidden="true" /> Light</span>
                <span><Moon :size="16" aria-hidden="true" /> Dark</span>
                <span><Languages :size="16" aria-hidden="true" /> 9 Languages</span>
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
            <p class="section-index">[ 03 — SYSTEM ]</p>
            <h2 class="section-title mt-6">At home on Windows.</h2>
            <p class="mt-6 max-w-xl text-pretty text-lg leading-8 text-slate-700">
              Bluetooth Notify reads standard Windows device data and uses native tray behavior. This framework-dependent build requires both runtimes below before first launch.
            </p>

            <div class="requirement-list mt-10">
              <div><CircleCheck aria-hidden="true" /><span>Windows 11 22H2 or newer</span><b>x64</b></div>
              <div>
                <CircleCheck aria-hidden="true" />
                <span>.NET Desktop Runtime 10 x64</span>
                <a href="https://dotnet.microsoft.com/en-us/download/dotnet/10.0" target="_blank" rel="noreferrer">Download ↗</a>
              </div>
              <div>
                <CircleCheck aria-hidden="true" />
                <span>Windows App Runtime 1.8 x64</span>
                <a href="https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/downloads-archive#version-18" target="_blank" rel="noreferrer">Download ↗</a>
              </div>
            </div>
          </div>

          <div class="settings-rig">
            <div class="settings-backdrop" aria-hidden="true">SYSTEM<br />READY</div>
            <div class="settings-shot">
              <div class="shot-toolbar"><span>SETTINGS / APPEARANCE</span><span>03</span></div>
              <img
                src="/screenshots/settings_light.png"
                alt="Bluetooth Notify light appearance settings"
                width="880"
                height="884"
                class="image-outline block w-full rounded-[10px]"
              />
            </div>
            <div class="theme-sticker">
              <Settings2 :size="22" aria-hidden="true" />
              <div><strong>Theme-aware</strong><span>Light / Dark / System</span></div>
            </div>
          </div>
        </div>
      </section>

      <section id="build" class="build-section scroll-mt-20">
        <div class="page-shell">
          <div class="build-layout">
            <div>
              <div class="build-icon"><BatteryCharging :size="27" :stroke-width="1.8" aria-hidden="true" /></div>
              <p class="section-index mt-7 text-blue-200">[ 04 — RUN IT ]</p>
              <h2 class="build-title mt-5">Ready to keep<br />an eye on it?</h2>
              <p class="mt-6 max-w-xl text-pretty text-lg leading-8 text-blue-100/80">
                The installer checks both prerequisites and opens the official Microsoft download page if either one is missing. Developers can also launch the app from source.
              </p>
            </div>

            <div class="command-card">
              <div class="command-label"><span>POWERSHELL</span><span>01 LINE</span></div>
              <code>{{ buildCommand }}</code>
              <button type="button" class="copy-command" :aria-label="copied ? 'Command copied' : 'Copy build command'" @click="copyCommand">
                <span class="copy-icon-stack" aria-hidden="true">
                  <Copy :class="{ 'copy-icon-hidden': copied }" :size="19" />
                  <Check :class="{ 'copy-icon-hidden': !copied }" class="absolute" :size="19" />
                </span>
                {{ copied ? 'Copied' : 'Copy command' }}
              </button>
            </div>
          </div>
        </div>
      </section>
    </main>

    <footer class="relative z-10 border-t-2 border-[var(--ink)] bg-[var(--paper)]">
      <div class="page-shell flex flex-col gap-5 py-8 text-sm sm:flex-row sm:items-center sm:justify-between">
        <a href="#top" class="brand-link">
          <span class="brand-mark"><Bluetooth :size="18" aria-hidden="true" /></span>
          <span class="font-extrabold uppercase">Bluetooth Notify</span>
        </a>
        <p class="font-semibold text-slate-600">Focused Bluetooth battery status for Windows 11.</p>
      </div>
    </footer>
  </div>
</template>

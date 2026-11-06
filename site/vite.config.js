import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import tailwindcss from '@tailwindcss/vite'
import { serializeSoftwareApplicationSchema, softwareApplicationSchemaId } from './src/structured-data.js'

function rewriteLegalDirectoryIndex(request, _response, next) {
  if (request.url) {
    const [path, query = ''] = request.url.split('?', 2)
    if (path === '/legal' || path === '/legal/') {
      request.url = `/legal/index.html${query ? `?${query}` : ''}`
    }
  }
  next()
}

function legalDirectoryIndex() {
  return {
    name: 'legal-directory-index',
    configureServer(server) {
      server.middlewares.use(rewriteLegalDirectoryIndex)
    },
    configurePreviewServer(server) {
      server.middlewares.use(rewriteLegalDirectoryIndex)
    },
  }
}

function softwareApplicationMarkup() {
  return {
    name: 'software-application-json-ld',
    transformIndexHtml(_html, context) {
      if (!['/', '/index.html'].includes(context.path.split('?')[0])) return []
      return [{
        tag: 'script',
        attrs: { id: softwareApplicationSchemaId, type: 'application/ld+json' },
        children: serializeSoftwareApplicationSchema(),
        injectTo: 'head',
      }]
    },
  }
}

export default defineConfig({
  plugins: [legalDirectoryIndex(), softwareApplicationMarkup(), vue(), tailwindcss()],
})

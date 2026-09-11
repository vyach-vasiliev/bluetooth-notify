import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import tailwindcss from '@tailwindcss/vite'

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

export default defineConfig({
  plugins: [legalDirectoryIndex(), vue(), tailwindcss()],
})

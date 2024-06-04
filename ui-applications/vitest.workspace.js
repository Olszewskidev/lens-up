import { defineWorkspace } from 'vitest/config'

export default defineWorkspace([
  "./packages/shared-components/vite.config.ts",
  "./packages/photo-collector-ui/vite.config.ts",
  "./packages/gallery-ui/vite.config.ts",
  "./packages/back-office-ui/vite.config.ts"
])

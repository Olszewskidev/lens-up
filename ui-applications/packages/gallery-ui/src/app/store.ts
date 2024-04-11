import { configureStore } from '@reduxjs/toolkit'
import { galleryApi } from '../services/GalleryApi'

export const store = configureStore({
  reducer: {
    [galleryApi.reducerPath]: galleryApi.reducer
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(galleryApi.middleware),
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
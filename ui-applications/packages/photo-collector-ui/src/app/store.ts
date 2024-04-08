import { configureStore } from '@reduxjs/toolkit'
import { photoCollectorApi } from '../services/PhotoCollectorApi'

export const store = configureStore({
  reducer: {
    [photoCollectorApi.reducerPath]: photoCollectorApi.reducer
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(photoCollectorApi.middleware),
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
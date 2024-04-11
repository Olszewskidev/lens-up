import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const baseQuery = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_GALLERY_SERVICE_URL,
});

export const galleryApi = createApi({
    reducerPath: 'galleryApi',
    baseQuery: baseQuery,
    endpoints: (builder) => ({
    }),
})

export const { } = galleryApi
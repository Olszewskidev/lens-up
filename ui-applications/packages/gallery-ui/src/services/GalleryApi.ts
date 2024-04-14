import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const baseQuery = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_GALLERY_SERVICE_URL,
});

export const galleryApi = createApi({
    reducerPath: 'galleryApi',
    baseQuery: baseQuery,
    endpoints: (builder) => ({
        loginToGallery: builder.mutation<{ enterCode: string, galleryId: string }, { enterCode: string }>({
            query: (payload) => ({
                method: 'POST',
                url: `login`,
                body: payload,
            }),
        }),
    }),
})

export const { useLoginToGalleryMutation } = galleryApi
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const baseQuery = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_PHOTO_COLLECTOR_SERVICE_URL,
});

export const photoCollectorApi = createApi({
    reducerPath: 'photoCollectorApi',
    baseQuery: baseQuery,
    endpoints: (builder) => ({
        addPhotoToGallery: builder.mutation<void, { enterCode: string, formData: FormData, author: string }>({
            query: (payload) => ({
                method: 'POST',
                url: `upload-photo/${payload.enterCode}`,
                body: payload.formData,
                formData: true
            }),
        }),
    }),
})

export const { useAddPhotoToGalleryMutation } = photoCollectorApi
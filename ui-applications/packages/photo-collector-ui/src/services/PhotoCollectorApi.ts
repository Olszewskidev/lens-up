import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const baseQuery = fetchBaseQuery({
    baseUrl: 'https://localhost:7147',
});

export const photoCollectorApi = createApi({
    reducerPath: 'photoCollectorApi',
    baseQuery: baseQuery,
    endpoints: (builder) => ({
        addPhotoToGallery: builder.mutation<void, { enterCode: number, formData: FormData }>({
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
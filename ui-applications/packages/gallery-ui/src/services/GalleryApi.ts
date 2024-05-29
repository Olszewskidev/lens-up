import { HubConnectionBuilder } from '@microsoft/signalr';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import { LoginToGalleryPayload, LoginToGalleryResponse, PhotoItem } from '../types/GalleryApiTypes';

const baseQuery = fetchBaseQuery({
    baseUrl: import.meta.env.VITE_GALLERY_SERVICE_URL,
});

export const galleryApi = createApi({
    reducerPath: 'galleryApi',
    baseQuery: baseQuery,
    endpoints: (builder) => ({
        loginToGallery: builder.mutation<LoginToGalleryResponse, LoginToGalleryPayload>({
            query: (payload) => ({
                method: 'POST',
                url: `login`,
                body: payload,
            }),
        }),
        getGalleryPhotos: builder.query<PhotoItem[], string>({
            queryFn: () => ({ data: [] }),
            async onCacheEntryAdded(galleryId, { cacheDataLoaded, cacheEntryRemoved, updateCachedData },) {
                try {
                    await cacheDataLoaded;

                    const socket = new HubConnectionBuilder().withUrl(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/hubs/gallery?galleryId=${galleryId}`).build();

                    await socket.start().then(() => {
                        console.log("Connected to socket.")
                    }).catch(() => {
                        console.error("Failed during socket connection.")
                    })

                    socket.on("PhotoUploadedToGallery", (message: PhotoItem) => {
                        console.log("PhotoUploadedToGallery notification")
                        updateCachedData((draft) => {
                            return [...draft, message];
                        });
                    });

                    await cacheEntryRemoved;

                    socket.off('connect');
                    socket.off("PhotoUploadedToGallery");
                } catch {
                    console.error("Failed during socket connection.")
                }
            }
        })
    }),
})

export const { useLoginToGalleryMutation, useGetGalleryPhotosQuery } = galleryApi
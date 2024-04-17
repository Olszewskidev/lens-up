export interface LoginToGalleryPayload {
    enterCode: string,
}

export interface LoginToGalleryResponse {
    enterCode: string,
    galleryId: string
}

export interface PhotoItem {
    id: string
    url: string
}
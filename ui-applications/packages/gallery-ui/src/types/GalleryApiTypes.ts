export interface LoginToGalleryPayload {
    enterCode: string,
}

export interface LoginToGalleryResponse {
    enterCode: string,
    galleryId: string,
    qrCodeUrl: string
}

export interface PhotoItem {
    id: string
    url: string
    authorName: string
    wishesText: string
}
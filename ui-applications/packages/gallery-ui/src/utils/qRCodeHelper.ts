import { LocalStorageKeys } from "./constants"

export const saveQRCode = (qRCodeUrl: string) => {
    localStorage.setItem(LocalStorageKeys.QRCodeUrl, qRCodeUrl)
}

export const getQRCodeUrl = () => {
    const url = localStorage.getItem(LocalStorageKeys.QRCodeUrl)
    if (!url) {
        console.error("QR code url not found in local storage.")
        return
    }

    return url
}
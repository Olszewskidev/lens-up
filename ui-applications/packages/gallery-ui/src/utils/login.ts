import { NavigateFunction } from "react-router-dom";
import { galleryApi, useLoginToGalleryMutation } from "../services/GalleryApi";
import { saveQRCode } from "./qRCodeHelper";
import { ChangeEvent } from "react";
import { LoginPage } from "../pages/Login/LoginPage";

const [loginToGallery, { isLoading }] = useLoginToGalleryMutation();

export const handleLoginToGallery = async (enterCode: string | undefined, navigate: NavigateFunction) => {
    // TODO: Add validation
    if (!enterCode) {
        return;
    }

    loginToGallery({ enterCode: enterCode })
        .unwrap()
        .then((payload) => {
            console.log(payload)
            saveQRCode(payload.qrCodeUrl)
            return navigate(`/gallery/${payload.galleryId}`)
        })
}

/*
const handleEnterCodeInputChange = (event: ChangeEvent<HTMLInputElement>) => {
    // TODO: Validation
    const enterCodeValue = event.target.value;
    setEnterCode(enterCodeValue);
};*/
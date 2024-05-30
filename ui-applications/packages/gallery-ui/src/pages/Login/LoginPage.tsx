import { ChangeEvent, useState } from "react";
import LoginForm from "./components/LoginForm"
import { useLoginToGalleryMutation } from "../../services/GalleryApi";
import { useNavigate } from "react-router-dom";
import { saveQRCode } from "../../utils/qRCodeHelper";

export const LoginPage = () => {
    const navigate = useNavigate();
    const [enterCode, setEnterCode] = useState<string>();

    const [loginToGallery, { isLoading }] = useLoginToGalleryMutation();

    const handleFormSubmit = () => {
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

    const handleEnterCodeInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        // TODO: Validation
        const enterCodeValue = event.target.value;
        setEnterCode(enterCodeValue);
    };

    return (
        <div className="container mx-auto flex flex-wrap flex-col md:flex-row justify-center h-screen items-center">
            <LoginForm handleFormSubmit={handleFormSubmit} enterCode={enterCode} handleEnterCodeInputChange={handleEnterCodeInputChange} isLoading={isLoading} />
        </div>
    )
}

export default LoginPage
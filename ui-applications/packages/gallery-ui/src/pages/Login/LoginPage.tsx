import { ChangeEvent, FormEvent, useState } from "react";
import LoginForm from "./components/LoginForm"
import { useLoginToGalleryMutation } from "../../services/GalleryApi";
import { useNavigate } from "react-router-dom";

const LoginPage = () => {
    const navigate = useNavigate();
    const [enterCode, setEnterCode] = useState<string>();

    const [loginToGallery] = useLoginToGalleryMutation();

    const handleFormSubmit = (event: FormEvent) => {
        event.preventDefault()
        // TODO: Add validation
        if (!enterCode) {
            return;
        }

        loginToGallery({ enterCode: enterCode })
            .unwrap()
            .then((payload) => {
                navigate(`/gallery/${payload.galleryId}`)
            })
    }

    const handleEnterCodeInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        // TODO: Validation
        const enterCodeValue = event.target.value;
        setEnterCode(enterCodeValue);
    };

    return (
        <div className="min-h-screen bg-black">
            <div className="container mx-auto flex flex-wrap flex-col md:flex-row justify-center h-screen items-center">
                <LoginForm handleFormSubmit={handleFormSubmit} enterCode={enterCode} handleEnterCodeInputChange={handleEnterCodeInputChange} />
            </div>
        </div>
    )
}

export default LoginPage
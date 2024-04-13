import { ChangeEvent, useState } from "react";
import LoginForm from "./components/LoginForm"

const LoginPage = () => {
    const [enterCode, setEnterCode] = useState<string>();

    const handleFormSubmit = () => {
        // TODO: Add validation
        if (!enterCode) {
            return;
        }

        console.log(enterCode)
    }

    const handleEnterCodeInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        // TODO: Validation
        const enterCodeValue = event.target.value;
        if (!enterCodeValue) {
            return;
        }

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
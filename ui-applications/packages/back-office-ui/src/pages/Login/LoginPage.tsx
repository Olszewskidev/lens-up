import { useNavigate } from "react-router-dom";
import { LoginForm } from "./components";
import { AppRoutes } from "../../utils/constants";

const LoginPage = () => {
    const navigate = useNavigate();

    const handleFormSubmit = () => {
        navigate(AppRoutes.Home)
    }

    return (
        <div className="min-h-screen bg-black">
            <div className="container mx-auto flex flex-wrap flex-col md:flex-row justify-center h-screen items-center">
                <LoginForm handleFormSubmit={handleFormSubmit} />
            </div>
        </div>

    )
}

export default LoginPage
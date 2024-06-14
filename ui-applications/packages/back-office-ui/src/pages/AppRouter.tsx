import { createBrowserRouter } from "react-router-dom";
import HomePage from "./Home/HomePage";
import LoginPage from "./Login/LoginPage";
import { AppRoutes } from "../utils/constants"

const AppRouter = createBrowserRouter([
    {
        path: AppRoutes.Default,
        errorElement: <div>Error page</div>,
        children: [
            {
                path: AppRoutes.Default,
                element: <LoginPage />
            },
            {
                path: AppRoutes.Login,
                element: <LoginPage />
            },
            {
                path: AppRoutes.Home,
                element: <HomePage />
            },
        ],
    },
]);

export default AppRouter
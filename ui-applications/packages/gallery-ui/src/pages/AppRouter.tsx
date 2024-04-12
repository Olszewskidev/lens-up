import { createBrowserRouter } from "react-router-dom";
import HomePage from "./Home/HomePage";
import LoginPage from "./Login/LoginPage";

enum AppRoutes {
    HOME = "/gallery/:enterCode",
    LOGIN = "/login"
}

const AppRouter = createBrowserRouter([
    {
        path: "/",
        errorElement: <div>error</div>,
        children: [
            {
                path: AppRoutes.HOME,
                element: <HomePage />
            },
            {
                path: AppRoutes.LOGIN,
                element: <LoginPage />
            }
        ],
    },
]);

export default AppRouter
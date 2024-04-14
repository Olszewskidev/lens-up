import { createBrowserRouter } from "react-router-dom";
import HomePage from "./Home/HomePage";
import LoginPage from "./Login/LoginPage";

enum AppRoutes {
    HOME = "/gallery/:enterCode",
    LOGIN = "/login",
    DEFAULT = "/"
}

const AppRouter = createBrowserRouter([
    {
        path: AppRoutes.DEFAULT,
        errorElement: <div>Error page</div>,
        children: [
            {
                path: AppRoutes.DEFAULT,
                element: <LoginPage />
            },
            {
                path: AppRoutes.LOGIN,
                element: <LoginPage />
            },
            {
                path: AppRoutes.HOME,
                element: <HomePage />
            },
        ],
    },
]);

export default AppRouter
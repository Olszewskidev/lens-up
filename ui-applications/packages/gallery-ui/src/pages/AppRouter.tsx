import { createBrowserRouter } from "react-router-dom";
import HomePage from "./Home/HomePage";
import LoginPage from "./Login/LoginPage";
import { PageRoot } from "../components";

enum AppRoutes {
    HOME = "/gallery/:enterCode",
    LOGIN = "/login"
}

const AppRouter = createBrowserRouter([
    {
        path: "/",
        element: <PageRoot />,
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
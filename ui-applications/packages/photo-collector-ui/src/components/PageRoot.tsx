import { Outlet } from "react-router-dom";
import AppContainer from "./AppContainer";

const PageRoot = () => {
    return (
        <div className="min-h-screen bg-black">
            <AppContainer>
                <Outlet />
            </AppContainer>
        </div>
    )
}

export default PageRoot

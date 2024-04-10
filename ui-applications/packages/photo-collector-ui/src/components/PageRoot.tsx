import { NavbarComponent } from "@lens-up/shared-components";
import { Outlet } from "react-router-dom";
import AppContainer from "./AppContainer";

const PageRoot = () => {
    return (
        <>
            <NavbarComponent />
            <AppContainer>
                <Outlet />
            </AppContainer>
        </>
    )
}

export default PageRoot

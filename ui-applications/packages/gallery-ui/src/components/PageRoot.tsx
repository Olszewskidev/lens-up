import { Outlet } from "react-router-dom";

const PageRoot = () => {
    return (
        <div className="min-h-screen bg-black">
            <Outlet />
        </div>
    )
}

export default PageRoot

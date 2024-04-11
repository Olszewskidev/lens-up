import { Outlet } from "react-router-dom"

const PageRoot = () => {
    return (
        <>
            <div>Page Root</div>
            <Outlet />
        </>
    )
}

export default PageRoot
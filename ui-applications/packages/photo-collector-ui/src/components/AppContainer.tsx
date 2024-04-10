interface IAppContainerProps {
    children: React.ReactNode
}

const AppContainer = ({ children }: IAppContainerProps) => {
    return (
        <div className="flex items-center justify-center bg-gray-100 h-screen">
            {children}
        </div>
    )
}

export default AppContainer

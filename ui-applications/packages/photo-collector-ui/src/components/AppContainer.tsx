interface IAppContainerProps {
    children: React.ReactNode
}

const AppContainer = ({ children }: IAppContainerProps) => {
    return (
        <div className="flex items-center justify-center h-screen">
            {children}
        </div>
    )
}

export default AppContainer

interface ICardProps {
    children: React.ReactNode
}

const CardComponent: React.FC<ICardProps> = ({ children }) => {
    return (
        <div className="bg-white rounded-lg shadow-md overflow-hidden items-center">
            <div className="px-16 py-14">
                {children}
            </div>
        </div>
    )
}

export default CardComponent;
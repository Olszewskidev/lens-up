interface IAddPhotoFormContainerProps {
    children: React.ReactNode
}

const AddPhotoFormContainer = ({ children }: IAddPhotoFormContainerProps) => {
    return (
        <div className="container w-full mx-auto items-center py-32">
            <div className="max-w-sm mx-auto overflow-hidden items-center">
                {
                    children
                }
            </div>
        </div>
    )
}

export default AddPhotoFormContainer

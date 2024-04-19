interface IPhotoCardProps {
    photoUrl: string
    caption?: string
}

const PhotoCard = ({ photoUrl, caption }: IPhotoCardProps) => {
    return (
        <div className="bg-white p-4 shadow-md">
            <img className="w-full h-auto" src={photoUrl} />
            {
                caption && (
                    <div className="text-center text-lg leading-10">
                        {caption}
                    </div>
                )
            }
        </div>
    )
}

export default PhotoCard
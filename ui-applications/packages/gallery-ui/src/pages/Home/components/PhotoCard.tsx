import { PhotoItem } from "../../../types/GalleryApiTypes"

interface IPhotoCardProps {
    photoItem: PhotoItem
}

const PhotoCard = ({ photoItem }: IPhotoCardProps) => {
    return (
        <img
            key={`img-${photoItem.id}`}
            src={photoItem.url}
            alt={photoItem.id}
            className="w-4/5 h-105 rounded-8 object-cover bg-white p-4 shadow-md m-auto"
        />
    )
}

export default PhotoCard
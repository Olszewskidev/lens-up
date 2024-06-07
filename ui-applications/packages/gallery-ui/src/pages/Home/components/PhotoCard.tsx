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
            className="h-full w-full object-contain "
        />
    )
}

export default PhotoCard
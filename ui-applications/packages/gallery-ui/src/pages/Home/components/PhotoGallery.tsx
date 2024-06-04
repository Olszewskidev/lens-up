import { PhotoItem } from "../../../types/GalleryApiTypes"

import PhotoCarousel from "./PhotoCarousel";
import PhotoCard from "./PhotoCard";

interface IPhotoGalleryProps {
    photoItems: PhotoItem[]
}

const PhotoGallery = ({ photoItems }: IPhotoGalleryProps) => {
    return (
        <div className="bg-black h-screen justify-center flex items-center flex-col">
            <div className="container mx-auto px-2 py-2 lg:px-4 lg:pt-8">
                {
                    photoItems.length === 1 && (
                        <PhotoCard photoItem={photoItems[0]} />)
                }
                {
                    photoItems.length !== 1 && (
                        <PhotoCarousel photoItems={photoItems} />
                    )
                }
            </div>
        </div>
    )
}

export default PhotoGallery
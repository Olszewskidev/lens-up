import { PhotoItem } from "../../../types/GalleryApiTypes"

import PhotoCarousel from "./PhotoCarousel";
import PhotoCard from "./PhotoCard";

interface IPhotoGalleryProps {
    photoItems: PhotoItem[]
}

const PhotoGallery = ({ photoItems }: IPhotoGalleryProps) => {
    return (
        <div className="bg-black max-h-full justify-center flex items-center">
            <div className="container mx-auto h-screen w-screen flex justify-center items-center overflow-hidden">
                {
                    photoItems.length === 1 && (
                        <div className="h-4/5 w-4/5 object-contain items-center justify-center">
                            <PhotoCard photoItem={photoItems[0]} />
                        </div>
                    )
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
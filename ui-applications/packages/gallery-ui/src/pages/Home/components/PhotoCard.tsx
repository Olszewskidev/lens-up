import { PhotoItem } from "../../../types/GalleryApiTypes"

interface IPhotoCardProps {
    photoItem: PhotoItem
}

const PhotoCard = ({ photoItem }: IPhotoCardProps) => {
    return (
        <div className="container h-full w-full text-center">
            <img
                key={`img-${photoItem.id}`}
                src={photoItem.url}
                alt={photoItem.id}
                className="h-full w-full object-contain"
            />
            <div className="absolute bottom-20 right-28 bg-white py-6 px-6 rounded-3xl w-64 my-4 shadow-xl">
                <div className="text-white flex items-center absolute rounded-full py-4 px-4 shadow-xl bg-gray-300 left-4 -top-6">
                    <p className=" text-xl">
                        &#128204;
                    </p>
                </div>
                <div className="mt-8">
                    <p className="text-xl font-semibold my-2">&#10080; {photoItem.wishesText} &#10078;</p>
                    <div className="border-t-2"></div>
                    <p className="font-semibold text-base my-2 mb-2 float-right">{photoItem.authorName}</p>
                </div>
            </div>
        </div>

    )
}

export default PhotoCard
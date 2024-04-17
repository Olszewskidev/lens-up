import { useParams } from "react-router-dom";
import { useGetGalleryPhotosQuery } from "../../services/GalleryApi";

const HomePage = () => {
    const { galleryId } = useParams();
    console.log(galleryId)
    const { } = useGetGalleryPhotosQuery(galleryId || "", { skip: !galleryId });

    return (
        <h1 className="text-3xl font-bold underline">
            Hello Home page!
        </h1>
    )
}

export default HomePage
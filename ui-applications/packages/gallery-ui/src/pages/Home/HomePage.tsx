import { useParams } from "react-router-dom";
import { useGetGalleryPhotosQuery } from "../../services/GalleryApi";
import PhotoGallery from "./components/PhotoGallery";

const HomePage = () => {
    const { galleryId } = useParams();
    const { data } = useGetGalleryPhotosQuery(galleryId || "", { skip: !galleryId });

    return (
        <div className="min-h-screen bg-black">
            {
                data && data.length > 0 && (<PhotoGallery photoItems={data} />)
            }
        </div>
    )
}

export default HomePage
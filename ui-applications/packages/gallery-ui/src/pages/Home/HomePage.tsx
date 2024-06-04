import { useParams } from "react-router-dom";
import { useGetGalleryPhotosQuery } from "../../services/GalleryApi";
import { getQRCodeUrl } from "../../utils/qRCodeHelper";
import { PhotoGalleryWithCarousel, QRCodeCard } from "./components";

const HomePage = () => {
    const { galleryId } = useParams();
    const { data } = useGetGalleryPhotosQuery(galleryId || "", { skip: !galleryId });
    const qRCodeUrl = getQRCodeUrl();

    const hasPhotos = data && data.length > 0
    return (
        <>
            {
                hasPhotos && (<PhotoGalleryWithCarousel photoItems={data} />)
            }
            {
                qRCodeUrl && <QRCodeCard qRCodeUrl={qRCodeUrl} hasPhotos={hasPhotos} />
            }
        </>
    )
}

export default HomePage
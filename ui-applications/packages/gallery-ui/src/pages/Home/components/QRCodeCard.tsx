import { memo } from "react"

interface IQRCodeCardProps {
    qRCodeUrl: string
    hasPhotos?: boolean
}

const QRCodeCard = ({ qRCodeUrl, hasPhotos }: IQRCodeCardProps) => {
    return (
        <div className={`absolute ${hasPhotos ? 'bottom-0 end-5' : 'top-1/2 left-1/2 transform -translate-x-1/2'}`}>
            <figure>
                <img className={`rounded-lg ${hasPhotos ? 'w-32 h-32' : 'w-64 h-64'}`} src={qRCodeUrl} alt="QR code" />
                <figcaption className="font-bold text-base mb-2 text-center text-white">Scan this and join.</figcaption>
            </figure>
        </div>
    )
}

export default memo(QRCodeCard)
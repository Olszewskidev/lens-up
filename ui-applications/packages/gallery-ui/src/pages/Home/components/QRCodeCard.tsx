interface IQRCodeCardProps {
    qRCodeUrl: string
    hasPhotos?: boolean
}

const QRCodeCard = ({ qRCodeUrl, hasPhotos }: IQRCodeCardProps) => {
    return (
        <>
            {
                hasPhotos && <img className="bottom-0 end-0 absolute rounded-lg w-64 h-64 " src={qRCodeUrl} alt="QR code" />
            }
            {
                !hasPhotos && <img className="top-1/2 left-1/2 absolute transform -translate-x-1/2 rounded-lg w-64 h-64 " src={qRCodeUrl} alt="QR code" />
            }
        </>
    )
}

export default QRCodeCard
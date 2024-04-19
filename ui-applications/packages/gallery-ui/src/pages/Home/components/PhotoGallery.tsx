import { useEffect, useState } from "react";
import { PhotoItem } from "../../../types/GalleryApiTypes"
import { motion } from "framer-motion";

interface IPhotoGalleryProps {
    photoItems: PhotoItem[]
}

const PhotoGallery = ({ photoItems }: IPhotoGalleryProps) => {

    const [positionIndexes, setPositionIndexes] = useState([0, 1, 2, 3, 4]);
    const handleNext = () => {
        setPositionIndexes((prevIndexes) => {
            const updatedIndexes = prevIndexes.map(
                (prevIndex) => (prevIndex + 1) % photoItems.length
            );
            return updatedIndexes;
        });
    };

    useEffect(() => {
        const interval = setInterval(() => {
            handleNext();
        }, 5000);

        return () => {
            clearInterval(interval);
        };
    }, []);

    const positions = ["center", "left1", "left", "right", "right1"];

    const imageVariants = {
        center: { x: "0%", scale: 1, zIndex: 5 },
        left1: { x: "-50%", scale: 0.7, zIndex: 3 },
        left: { x: "-90%", scale: 0.5, zIndex: 2 },
        right: { x: "90%", scale: 0.5, zIndex: 1 },
        right1: { x: "50%", scale: 0.7, zIndex: 3 },
    };

    return (
        <div className="bg-black h-screen justify-center flex items-center flex-col">
            {photoItems.map((image, index) => (
                <motion.img
                    key={image.id}
                    src={image.url}
                    alt={image.id}
                    className="bg-white p-4 shadow-md"
                    initial="center"
                    animate={positions[positionIndexes[index]]}
                    variants={imageVariants}
                    transition={{ duration: 0.75 }}
                    style={{ width: "40%", position: "absolute" }}
                />
            ))}
        </div>
    )
}

export default PhotoGallery
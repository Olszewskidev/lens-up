import { useEffect, useMemo, useState } from "react";
import { PhotoItem } from "../../../types/GalleryApiTypes"
import { motion } from "framer-motion";
import { getPhotoVariants } from "../utils/photoGalleryHelper";
import { Position } from "../utils/constants";

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


    const variants = useMemo(() => {
        return getPhotoVariants(photoItems.length);
    }, [photoItems])
    const positions = Object.keys(variants);

    return (
        <div className="bg-black h-screen justify-center flex items-center flex-col">
            {photoItems.map((image, index) => (
                <motion.img
                    key={image.id}
                    src={image.url}
                    alt={image.id}
                    className="bg-white p-4 shadow-md"
                    initial={Position.Center}
                    animate={positions[positionIndexes[index]]}
                    variants={variants}
                    transition={{ duration: 0.75 }}
                    style={{ width: "40%", position: "absolute" }}
                />
            ))}
        </div>
    )
}

export default PhotoGallery
import { PhotoItem } from "../../../types/GalleryApiTypes"
import { motion } from "framer-motion";

interface IPhotoGalleryProps {
    photoItems: PhotoItem[]
}

const PhotoGallery = ({ photoItems }: IPhotoGalleryProps) => {
    return (
        <div className="bg-black h-screen justify-center flex items-center flex-col">
            <div className="container mx-auto px-2 py-2 lg:px-4 lg:pt-8">
                <motion.ul className="-m-1 flex flex-wrap md:-m-2">
                    {photoItems.map((image) => (

                        <motion.li className="flex w-1/4 flex-wrap"
                            layoutId={image.id}
                            key={image.id}
                            animate="visible"
                            exit="hidden"
                            transition={{ duration: 1 }}
                            initial={{
                                opacity: 0,
                            }}
                            whileInView={{
                                opacity: 1,
                                transition: {
                                    duration: 1
                                }
                            }}
                            viewport={{ once: true }}
                        >
                            <div className="w-full p-1 md:p-2">
                                <img
                                    src={image.url}
                                    alt={image.id}
                                    className="bg-white p-4 shadow-md"
                                    style={{ width: "80%" }}
                                />
                            </div>
                        </motion.li>
                    )
                    )}
                </motion.ul>
            </div>
        </div>
    )
}

export default PhotoGallery
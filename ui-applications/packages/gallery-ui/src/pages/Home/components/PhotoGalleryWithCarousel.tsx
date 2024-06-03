import { useEffect, useState } from "react";
import { PhotoItem } from "../../../types/GalleryApiTypes"

import 'swiper/css';
import 'swiper/css/effect-coverflow';

import { Swiper, SwiperSlide } from 'swiper/react';
import { Autoplay } from 'swiper/modules';
import { carouselOptions } from "../utils/constants";

interface IPhotoGalleryProps {
    photoItems: PhotoItem[]
}

const PhotoGallery = ({ photoItems }: IPhotoGalleryProps) => {
    const [loop, setLoop] = useState(false);
    useEffect(() => {
        if (photoItems.length >= 2) {
            setLoop(true)
        }
    }, [photoItems]);

    return (
        <div className="bg-black h-screen justify-center flex items-center flex-col">
            <div className="container mx-auto px-2 py-2 lg:px-4 lg:pt-8">
                <Swiper
                    spaceBetween={30}
                    slidesPerView={1}
                    loop={loop}
                    autoplay={{
                        delay: carouselOptions.delay,
                        disableOnInteraction: false,
                    }}
                    speed={carouselOptions.speed}
                    modules={[Autoplay]}
                >
                    {photoItems.map((image) => (
                        <SwiperSlide key={image.id}>
                            <img
                                key={`img-${image.id}`}
                                src={image.url}
                                alt={image.id}
                                className="bg-white p-4 shadow-md"
                                style={{ width: "80%" }}
                            />
                        </SwiperSlide>))}
                </Swiper>
            </div>
        </div>
    )
}

export default PhotoGallery
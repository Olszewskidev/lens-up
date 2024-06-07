import { PhotoItem } from "../../../types/GalleryApiTypes"

import 'swiper/css';
import 'swiper/css/effect-coverflow';

import { Swiper, SwiperSlide } from 'swiper/react';
import { Autoplay, EffectCoverflow } from 'swiper/modules';
import { carouselOptions } from "../utils/constants";
import PhotoCard from "./PhotoCard";

interface IPhotoGalleryProps {
    photoItems: PhotoItem[]
}

const PhotoCarousel = ({ photoItems }: IPhotoGalleryProps) => {
    return (
        <Swiper
            slidesPerView={1}
            loop={true}
            autoplay={{
                delay: carouselOptions.delay,
                disableOnInteraction: false,
            }}
            speed={carouselOptions.speed}
            modules={[Autoplay, EffectCoverflow]}
            coverflowEffect={{
                rotate: 0,
                stretch: 0,
                depth: 100,
                modifier: 2.5,
            }}
            className="h-4/5 w-4/5 object-contain items-center justify-center" 
        >
            {photoItems.map((image) => (
                <SwiperSlide key={image.id}>
                    <PhotoCard photoItem={image} />
                </SwiperSlide>))}
        </Swiper>
    )
}

export default PhotoCarousel
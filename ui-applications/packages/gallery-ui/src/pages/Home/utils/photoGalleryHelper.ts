import { PhotoVariant, Position, imageVariants } from "./constants";

export const getPhotoVariants = (photoArrayLength: number): Partial<Record<Position, PhotoVariant>> => {
    const positionsArray = Object.values(Position).slice(0, photoArrayLength) as Position[];
    const variants: Partial<Record<Position, PhotoVariant>> = {};
    positionsArray.forEach(position => {
        variants[position] = imageVariants[position];
    });
    return variants;
};
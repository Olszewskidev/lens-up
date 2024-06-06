import { PhotoItem } from "../../../types/GalleryApiTypes"

interface IPhotoCardProps {
    photoItem: PhotoItem
}

const PhotoCard = ({ photoItem }: IPhotoCardProps) => {
    return (
        <figure className="w-10/12 border-2 border-silver min-w-[250px] bg-white my-3 mx-auto p-4">
            <img
                key={`img-${photoItem.id}`}
                src={photoItem.url}
                alt={photoItem.id}
                className="object-contain h-full w-full block shadow-[0_0_12px_3px_silver]"
            />
            <figcaption className="h-[2.5em] text-xl m-0 p-1.5">
                <div className="relative mx-0 italic text-[1.2rem] font-['var(--type-quote)'] indent-[1.6em]">
                    <blockquote className="relative text-center">
                        <div className="absolute z-10 left-1/2 top-[-2px] transform translate-x-[-50%] translate-y-[-50%] w-[1.3em] h-[1.3em] bg-white shadow-[0_4px_5px_-1px_hsla(0,0%,0%,0.2)] rounded-full grid place-content-center text-[36px] text-[var(--accent-color)] not-italic indent-0">
                            &#x270C;
                        </div>
                        <p className="pt-6">
                            <span className="font-bold">{`„${photoItem.wishesText}”`}</span> - {photoItem.authorName}
                        </p>
                    </blockquote>
                </div>
            </figcaption>
        </figure>
    )
}

export default PhotoCard
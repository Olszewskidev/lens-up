import { ChangeEvent, useState } from "react";
import { useAddPhotoToGalleryMutation } from "../../services/PhotoCollectorApi";
import { AddPhotoForm } from "./components"
import { ErrorCardComponent, SuccessCardComponent } from "@lens-up/shared-components";
import { useParams } from "react-router-dom";

const AddPhotoToGalleryPage = () => {
    const { enterCode } = useParams();
    const [addPhoto, { isError, isSuccess, isUninitialized, isLoading }] = useAddPhotoToGalleryMutation();
    const [photo, setPhoto] = useState<File>();
    const [author, setAuthor] = useState<string>();
    const [wishes, setWhishes] = useState<string>();

    const handlePhotoInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        // TODO: Validation
        const fileList = event.target.files
        const isEmpty = !fileList || fileList.length === 0;
        if (isEmpty) {
            return
        }
        const photo = fileList[0];
        setPhoto(photo)
    };

    const handleAuthorInputChange = (event: ChangeEvent<HTMLInputElement>) => {
        // TODO: Validation
        const author = event.target.value;
        setAuthor(author);
    };

    const handleWishesTextareaChange = (event: ChangeEvent<HTMLTextAreaElement>) => {
        // TODO: Validation
        const wishes = event.target.value;
        setWhishes(wishes);
    };

    const handleFormSubmit = () => {
        // TODO: Validation
        if (!photo || !enterCode || !author || !wishes) {
            return
        }
        const formData = new FormData()
        formData.append('File', photo)
        formData.append('AuthorName', author)
        formData.append('WishesText', wishes)
        addPhoto({ enterCode, formData })
    }

    return (
        <>
            {
                isUninitialized && <AddPhotoForm
                    handleFormSubmit={handleFormSubmit}
                    handlePhotoInputChange={handlePhotoInputChange}
                    handleAuthorInputChange={handleAuthorInputChange}
                    handleWishesTextareaChange={handleWishesTextareaChange}
                    wishes={wishes}
                    author={author}
                    photo={photo}
                    isLoading={isLoading} />
            }
            {
                isError && <ErrorCardComponent />
            }
            {
                isSuccess && <SuccessCardComponent title="Congratulations!" text="You just joined to the party." />
            }
        </>
    )
}

export default AddPhotoToGalleryPage

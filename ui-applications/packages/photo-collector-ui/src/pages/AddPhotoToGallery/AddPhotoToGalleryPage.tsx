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
        const enterCodeValue = event.target.value;
        setAuthor(enterCodeValue);
    };

    const handleFormSubmit = () => {
        // TODO: Validation
        if (!photo || !enterCode || !author) {
            return
        }
        const formData = new FormData();
        formData.append('File', photo);
        addPhoto({ enterCode, formData, author })
        console.log({ enterCode, formData, author })
    }

    return (
        <>
            {
                isUninitialized && <AddPhotoForm
                    handleFormSubmit={handleFormSubmit}
                    handlePhotoInputChange={handlePhotoInputChange}
                    handleAuthorInputChange={handleAuthorInputChange}
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

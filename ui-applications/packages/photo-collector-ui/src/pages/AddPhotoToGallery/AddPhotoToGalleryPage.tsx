import { ChangeEvent, useState } from "react";
import { useAddPhotoToGalleryMutation } from "../../services/PhotoCollectorApi";
import { AddPhotoForm, AddPhotoFormSkeleton } from "./components"
import { ErrorCardComponent, SuccessAlertComponent } from "@lens-up/shared-components";
import { useParams } from "react-router-dom";

const AddPhotoToGalleryPage = () => {
    const { enterCode } = useParams();
    const [addPhoto, { isError, isSuccess, isUninitialized, isLoading }] = useAddPhotoToGalleryMutation();
    const [photo, setPhoto] = useState<File>();

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

    const handleFormSubmit = () => {
        // TODO: Validation
        if (!photo || !enterCode) {
            return
        }
        const formData = new FormData();
        formData.append('File', photo);
        addPhoto({ enterCode, formData })
    }

    return (
        <>
            {
                isUninitialized && <AddPhotoForm
                    handleFormSubmit={handleFormSubmit}
                    handlePhotoInputChange={handlePhotoInputChange}
                    photo={photo} />
            }
            {
                isLoading && <AddPhotoFormSkeleton />
            }
            {
                isError && <ErrorCardComponent />
            }
            {
                isSuccess && <SuccessAlertComponent />
            }
        </>
    )
}

export default AddPhotoToGalleryPage

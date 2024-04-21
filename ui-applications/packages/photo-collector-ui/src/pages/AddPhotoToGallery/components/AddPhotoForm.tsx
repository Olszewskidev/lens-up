import { ChangeEvent } from "react";
import AddPhotoFormContainer from "./AddPhotoFormContainer";
import { FormComponent, SubmitFormButtonComponent } from "@lens-up/shared-components";

interface IAddPhotoFormProps {
    handleFormSubmit: () => void,
    handlePhotoInputChange: (event: ChangeEvent<HTMLInputElement>) => void,
    isLoading: boolean
    photo?: File
}

const AddPhotoForm = ({ handleFormSubmit, handlePhotoInputChange, isLoading, photo }: IAddPhotoFormProps) => {
    return (
        <AddPhotoFormContainer>
            <FormComponent handleFormSubmit={handleFormSubmit}>
                <div className="px-4 py-6">
                    <div className="max-w-sm p-6 mb-4 border-dashed border-2 border-white rounded-lg items-center mx-auto text-center cursor-pointer">
                        {!photo && (<input type="file" accept="image/*" onChange={handlePhotoInputChange} />)}
                        {photo && (<img src={URL.createObjectURL(photo)} />)}
                    </div>
                    <div className="flex items-center justify-center">
                        <div className="text-center relative group">
                            <SubmitFormButtonComponent text="Upload" isLoading={isLoading} />
                        </div>
                    </div>
                </div>
            </FormComponent>
        </AddPhotoFormContainer>
    )
}

export default AddPhotoForm

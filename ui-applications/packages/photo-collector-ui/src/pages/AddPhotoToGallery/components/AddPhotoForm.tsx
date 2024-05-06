import { ChangeEvent } from "react";
import AddPhotoFormContainer from "./AddPhotoFormContainer";
import { FormComponent, FormInputComponent, SubmitFormButtonComponent, FormTextareaComponent } from "@lens-up/shared-components";

interface IAddPhotoFormProps {
    handleFormSubmit: () => void,
    handlePhotoInputChange: (event: ChangeEvent<HTMLInputElement>) => void
    handleAuthorInputChange: (event: ChangeEvent<HTMLInputElement>) => void
    handleWishesTextareaChange: (event: ChangeEvent<HTMLTextAreaElement>) => void
    isLoading: boolean
    photo?: File
    author?: string
    wishes?: string
}

const AddPhotoForm = ({ handleFormSubmit, handlePhotoInputChange, isLoading, photo, handleAuthorInputChange, author, handleWishesTextareaChange, wishes }: IAddPhotoFormProps) => {
    return (
        <AddPhotoFormContainer>
            <FormComponent handleFormSubmit={handleFormSubmit}>
                <div className="px-4 py-6">
                    <div className="mb-5 items-center mx-auto text-center cursor-pointer ">
                        {!photo && (
                            <>
                                <label htmlFor="dropzone-file" className="flex flex-col items-center justify-center w-full">
                                    <div className="flex flex-col items-center justify-center text-white">
                                        <svg className="w-8 h-8 mb-4" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="#ffffff" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
                                            <rect x="3" y="3" width="18" height="18" rx="2" /><circle cx="8.5" cy="8.5" r="1.5" /><path d="M20.4 14.5L16 10 4 20" />
                                        </svg>
                                        <p className="mb-2 text-sm"><span className="font-semibold">Click to upload your photo</span></p>
                                    </div>
                                    <input id="dropzone-file" type="file" className="hidden" onChange={handlePhotoInputChange} />
                                </label>
                            </>
                        )
                        }
                        {
                            photo && (
                                <div className="bg-white p-4 rounded-lg">
                                    <img className="w-full h-auto" src={URL.createObjectURL(photo)} />
                                </div>
                            )
                        }
                    </div>
                    <div className="mb-5">
                        <FormInputComponent label="Author" type="text" placeholder="Your name" required value={author} handleInputChange={handleAuthorInputChange} />
                    </div>
                    <div className="mb-5">
                        <FormTextareaComponent label="Your wishes" placeholder="..." required value={wishes} handleTextareaChange={handleWishesTextareaChange} />
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

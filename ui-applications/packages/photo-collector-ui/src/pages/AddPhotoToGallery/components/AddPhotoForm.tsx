import { ChangeEvent } from "react";
import AddPhotoFormContainer from "./AddPhotoFormContainer";

interface IAddPhotoFormProps {
    handleFormSubmit: () => void,
    handlePhotoInputChange: (event: ChangeEvent<HTMLInputElement>) => void,
    photo?: File
}

const AddPhotoForm = ({ handleFormSubmit, handlePhotoInputChange, photo }: IAddPhotoFormProps) => {
    return (
        <AddPhotoFormContainer>
            <form onSubmit={handleFormSubmit}>
                <div className="px-4 py-6">
                    <div className="max-w-sm p-6 mb-4 bg-gray-100 border-dashed border-2 border-gray-400 rounded-lg items-center mx-auto text-center cursor-pointer">
                        {!photo && (<input type="file" accept="image/*" onChange={handlePhotoInputChange} />)}
                        {photo && (<img src={URL.createObjectURL(photo)} />)}
                    </div>
                    <div className="flex items-center justify-center">
                        <div className="w-full">
                            <label className="w-full text-white bg-[#050708] hover:bg-[#050708]/90 focus:ring-4 focus:outline-none focus:ring-[#050708]/50 font-medium rounded-lg text-sm px-5 py-2.5 flex items-center justify-center mr-2 mb-2 cursor-pointer">
                                <input type="submit" />
                            </label>
                        </div>
                    </div>
                </div>
            </form>
        </AddPhotoFormContainer>
    )
}

export default AddPhotoForm

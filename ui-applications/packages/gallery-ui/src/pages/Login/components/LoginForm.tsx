import { ChangeEvent } from "react"
import { FormComponent, FormInputComponent, SubmitFormButtonComponent } from "@lens-up/shared-components";

interface ILoginFormProps {
    handleFormSubmit: () => void,
    handleEnterCodeInputChange: (event: ChangeEvent<HTMLInputElement>) => void,
    isLoading: boolean
    enterCode?: string
}

const LoginForm = ({ handleFormSubmit, handleEnterCodeInputChange, isLoading, enterCode }: ILoginFormProps) => {
    return (
        <FormComponent handleFormSubmit={handleFormSubmit}>
            <div className="mb-5">
                <FormInputComponent label="Join to your gallery" type="number" required value={enterCode} handleInputChange={handleEnterCodeInputChange} />
            </div>
            <div className="flex items-center justify-center">
                <div className="text-center relative group">
                    <SubmitFormButtonComponent text="Join" isLoading={isLoading} />
                </div>
            </div>
        </FormComponent>
    )
}

export default LoginForm
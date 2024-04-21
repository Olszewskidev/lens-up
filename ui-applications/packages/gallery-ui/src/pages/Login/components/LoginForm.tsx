import { ChangeEvent } from "react"
import Logo from "../../../images/lens-up-logo.png"
import { FormComponent, SubmitFormButtonComponent } from "@lens-up/shared-components";

interface ILoginFormProps {
    handleFormSubmit: () => void,
    handleEnterCodeInputChange: (event: ChangeEvent<HTMLInputElement>) => void,
    isLoading: boolean
    enterCode?: string
}

const LoginForm = ({ handleFormSubmit, handleEnterCodeInputChange, isLoading, enterCode }: ILoginFormProps) => {
    return (
        <div>
            <img width={379} height={147} src={Logo} alt="LensUp logo." />
            <FormComponent handleFormSubmit={handleFormSubmit}>
                <div className="mb-5">
                    <label htmlFor="code" className="block mb-2 text-sm font-medium text-gray-50 dark:text-white">Join to your gallery:</label>
                    <input type="number" id="code" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Enter Code" required value={enterCode || ''} onChange={(e) => handleEnterCodeInputChange(e)} />
                </div>
                <div className="text-center relative group">
                    <SubmitFormButtonComponent text="Join" isLoading={isLoading} />
                </div>
            </FormComponent>
        </div>
    )
}

export default LoginForm
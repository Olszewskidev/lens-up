import { ChangeEvent, FormEvent } from "react"
import Logo from "../../../images/lens-up-logo.png"

interface ILoginFormProps {
    handleFormSubmit: (event: FormEvent) => void,
    handleEnterCodeInputChange: (event: ChangeEvent<HTMLInputElement>) => void,
    enterCode?: string
}

const LoginForm = ({ handleFormSubmit, handleEnterCodeInputChange, enterCode }: ILoginFormProps) => {
    return (
        <div>
            <img width={379} height={147} src={Logo} alt="LensUp logo." />
            <form className="px-7 py-4 bg-black rounded-lg leading-none border-double border-4 border-white shadow-lg max-w-sm mx-auto" onSubmit={(e) => handleFormSubmit(e)}>
                <div className="mb-5">
                    <label htmlFor="code" className="block mb-2 text-sm font-medium text-gray-50 dark:text-white">Join to your gallery:</label>
                    <input type="number" id="code" className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Enter Code" required value={enterCode || ''} onChange={(e) => handleEnterCodeInputChange(e)} />
                </div>
                <div className="text-center relative group">
                    <button type="submit" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-full text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">Join</button>
                </div>
            </form>
        </div>
    )
}

export default LoginForm
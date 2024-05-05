import { ChangeEvent, useId } from "react"

interface IFormInputProps {
    label: string
    type: "text" | "number"
    value?: string
    handleInputChange: (event: ChangeEvent<HTMLInputElement>) => void,
    required?: boolean
}

const FormInputComponent: React.FC<IFormInputProps> = ({ type, label, value, handleInputChange, required = false }) => {
    const id = useId()
    return (
        <div>
            <label htmlFor={id} className="block mb-2 text-sm font-medium text-gray-50 dark:text-white">{label}:</label>
            <input type={type} id={id} className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Enter Code" required = {required} value={value || ''} onChange={(e) => handleInputChange(e)} />
        </div>
    )
}

export default FormInputComponent;
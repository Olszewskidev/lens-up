import { ChangeEvent, useId } from "react"
import FormLabelComponent from "./FormLabelComponent"

interface IFormInputProps {
    label: string
    type: "text" | "number"
    placeholder?: string
    value?: string
    handleInputChange: (event: ChangeEvent<HTMLInputElement>) => void,
    required?: boolean
}

const FormInputComponent: React.FC<IFormInputProps> = ({ type, label, value, handleInputChange, placeholder, required = false }) => {
    const id = useId()
    return (
        <div>
            <FormLabelComponent htmlFor={id} label={label} />
            <input type={type} id={id} className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder={placeholder || ''} required={required} value={value || ''} onChange={(e) => handleInputChange(e)} />
        </div>
    )
}

export default FormInputComponent;
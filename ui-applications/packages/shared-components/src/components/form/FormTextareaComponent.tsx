import { ChangeEvent, useId } from "react"
import FormLabelComponent from "./FormLabelComponent"

interface IFormTextareaProps {
    label: string
    placeholder?: string
    value?: string
    handleTextareaChange: (event: ChangeEvent<HTMLTextAreaElement>) => void,
    required?: boolean
}

const FormTextareaComponent: React.FC<IFormTextareaProps> = ({ label, value, handleTextareaChange, placeholder, required = false }) => {
    const id = useId()
    return (
        <div>
            <FormLabelComponent htmlFor={id} label={label} />
            <textarea id={id} rows={4} className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder={placeholder || ''} required={required} value={value || ''} onChange={(e) => handleTextareaChange(e)} />
        </div>
    )
}

export default FormTextareaComponent;
interface IFormLabelProps {
    htmlFor: string
    label: string
}

const FormLabelComponent: React.FC<IFormLabelProps> = ({ htmlFor, label }) => {
    return (
        <label htmlFor={htmlFor} className="block mb-2 text-sm font-medium text-gray-50 dark:text-white">{label}:</label>
    )
}

export default FormLabelComponent;
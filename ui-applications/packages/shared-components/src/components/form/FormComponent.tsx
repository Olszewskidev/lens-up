interface IFormProps {
    handleFormSubmit: () => void
    children: React.ReactNode
}

const FormComponent: React.FC<IFormProps> = ({ handleFormSubmit, children }) => {
    return (
        <form className="px-7 py-4 bg-black rounded-lg leading-none border-double border-4 border-white shadow-lg max-w-sm mx-auto" onSubmit={(e) => {
            e.preventDefault();
            handleFormSubmit()
        }}>
            {
                children
            }
        </form>
    )
}

export default FormComponent;
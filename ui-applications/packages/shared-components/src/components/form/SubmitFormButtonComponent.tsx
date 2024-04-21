interface ISubmitFormButtonProps {
    isLoading: boolean,
    text: string
}

const SubmitFormButtonComponent: React.FC<ISubmitFormButtonProps> = ({ isLoading, text }) => {
    return (
        <button type="submit" className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-full text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800">
            {text}
        </button>

    )
}

export default SubmitFormButtonComponent;
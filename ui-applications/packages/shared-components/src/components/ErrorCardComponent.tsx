import CardComponent from "./CardComponent";

const ErrorCardComponent = () => {
    return (
        <CardComponent>
            <div className="flex justify-center">
                <div className="rounded-full bg-red-200 p-6">
                    <div className="flex h-16 w-16 items-center justify-center rounded-full bg-red-500 p-4">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" className="h-8 w-8 text-white">
                            <path stroke-linecap="round" stroke-linejoin="round" d="M4.5 12.75l6 6 9-13.5" />
                        </svg>
                    </div>
                </div>
            </div>
            <h3 className="my-4 text-center text-3xl font-semibold text-gray-700">Error!!!</h3>
            <p className="w-[230px] text-center font-normal text-gray-600">Something went wrong, sorry.</p>
        </CardComponent>
    )
}

export default ErrorCardComponent;
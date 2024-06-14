import { FormComponent, SubmitFormButtonComponent } from "@lens-up/shared-components";

interface ILoginFormProps {
    handleFormSubmit: () => void,
}

const LoginForm = ({ handleFormSubmit }: ILoginFormProps) => {
    return (
        <FormComponent handleFormSubmit={handleFormSubmit}>
            <div className="flex items-center justify-center">
                <div className="text-center relative group">
                    <SubmitFormButtonComponent text="Log in" isLoading={false} />
                </div>
            </div>
        </FormComponent>
    )
}

export default LoginForm
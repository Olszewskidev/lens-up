import AlertCardComponent from "./AlertCardComponent";
import { AlertCardType } from "./constants";

interface IErrorCardProps {
    title?: string
    text?: string
}

const ErrorCardComponent = ({ text, title }: IErrorCardProps) => {
    return (
        <AlertCardComponent type={AlertCardType.Error} text={text} title={title} />
    )
}

export default ErrorCardComponent;
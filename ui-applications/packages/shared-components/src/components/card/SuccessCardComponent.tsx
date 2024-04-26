import AlertCardComponent from "./AlertCardComponent";
import { AlertCardType } from "./constants";

interface ISuccessCardProps {
    title?: string
    text?: string
}

const SuccessCardComponent = ({ text, title }: ISuccessCardProps) => {
    return (
        <AlertCardComponent type={AlertCardType.Success} text={text} title={title} />
    )
}

export default SuccessCardComponent;
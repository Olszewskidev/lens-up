import CardComponent from "./CardComponent"
import { AlertCardType, cardStyle } from "./constants"

interface IAlertCardProps {
    title?: string
    text?: string
    type: AlertCardType
}

const AlertCardComponent = ({ text, title, type }: IAlertCardProps) => {
    const style = cardStyle[type]
    return (
        <CardComponent>
            <div className="flex justify-center">
                <div className={`rounded-full p-6 ${style.primaryColor}`}>
                    <div className={`flex h-16 w-16 items-center justify-center rounded-full p-4 ${style.secondaryColor}`}>
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="h-8 w-8 text-white">
                            {
                                type === AlertCardType.Error && (<path strokeLinecap="round" strokeLinejoin="round" d="M4.5 12.75l6 6 9-13.5" />)
                            }
                            {
                                type === AlertCardType.Success && (<path strokeLinecap="round" strokeLinejoin="round" d="M4.5 12.75l6 6 9-13.5" />)
                            }
                        </svg>
                    </div>
                </div>
            </div>
            <h3 className="my-4 text-center text-3xl font-semibold text-gray-700">{title ? title : style.defaultTitle}</h3>
            <p className="w-[230px] text-center font-normal text-gray-600">{text ? text : style.defaultText}</p>
        </CardComponent>
    )
}

export default AlertCardComponent;
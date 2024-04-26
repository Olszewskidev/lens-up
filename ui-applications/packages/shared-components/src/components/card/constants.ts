export enum AlertCardType{
    Error = 1,
    Success = 2
}

export const cardStyle = {
    [AlertCardType.Success]: {
        defaultTitle: "Success!",
        defaultText: "Everything is ok.",
        primaryColor: "bg-green-200",
        secondaryColor: "bg-green-500",
    },
    [AlertCardType.Error]: {
        defaultTitle: "Error!",
        defaultText: "Something went wrong, sorry.",
        primaryColor: "bg-red-200",
        secondaryColor: "bg-red-500",
    }
}
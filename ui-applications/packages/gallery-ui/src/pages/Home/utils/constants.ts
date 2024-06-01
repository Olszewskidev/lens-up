export enum Position {
    Center = "center",
    Left = "left",
    Right = "right",
    Left1 = "left1",
    Right1 = "right1",
}

export type PhotoVariant = {
    x: string;
    scale: number;
    zIndex: number;
};

export const imageVariants: Record<Position, PhotoVariant> = {
    [Position.Center]: { x: "0%", scale: 1, zIndex: 5 },
    [Position.Left]: { x: "-90%", scale: 0.5, zIndex: 2 },
    [Position.Right]: { x: "90%", scale: 0.5, zIndex: 1 },
    [Position.Left1]: { x: "-50%", scale: 0.7, zIndex: 3 },
    [Position.Right1]: { x: "50%", scale: 0.7, zIndex: 3 },
};

export const pageSize = 5
export const positionIndexesArray = [0, 1, 2, 3, 4]
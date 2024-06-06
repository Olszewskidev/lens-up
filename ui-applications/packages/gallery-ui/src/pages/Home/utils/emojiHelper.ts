import { emoijs } from "./constants";

export const getRandomEmoji = () => {
    const randomIndex = Math.floor(Math.random() * emoijs.length);
    return emoijs[randomIndex];
}
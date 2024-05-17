import { Middleware, isRejected } from '@reduxjs/toolkit';

export const galleryApiMiddleware: Middleware = () => (next) => (action: Action) => {
    if (isRejected(action)) {
        console.error("API Failed!")
    }

    return next(action)
}
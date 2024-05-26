import { Middleware, isRejected } from '@reduxjs/toolkit';

export const galleryApiMiddleware: Middleware = () => (next) => (action) => {
    const hrefe = global.window.location.href;
    if (isRejected(action)) {
        console.error("API Failed!")
    }

    return next(action)
}
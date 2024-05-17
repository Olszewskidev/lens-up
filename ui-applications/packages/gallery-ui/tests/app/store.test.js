import { Middleware, isRejected } from '@reduxjs/toolkit';
import { galleryApiMiddleware } from '../../src/app/store/middlewares/GalleryApiMiddleware'

it('Middleware must shows action rejection', () => {
    galleryApiMiddleware().action
  });
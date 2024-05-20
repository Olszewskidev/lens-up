
import { galleryApiMiddleware } from '../../src/app/store/middlewares/GalleryApiMiddleware'
import {/*describe, expect, test, */jest} from '@jest/globals';
import { store } from '../../src/app/store/store';

export const create = () => {
  const next = jest.fn()

  const invoke = (action: unknown) => galleryApiMiddleware(store)(next)(action)

  return { store, next, invoke }
}
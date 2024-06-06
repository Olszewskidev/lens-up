import { galleryApiMiddleware } from '../../src/app/store/middlewares/GalleryApiMiddleware'
import { store } from '../../src/app/store/store';
import { vi } from "vitest"

export const create = () => {
  const next = vi.fn();

  const invoke = (action: unknown) => galleryApiMiddleware(store)(next)(action)

  return { store, next, invoke }
}
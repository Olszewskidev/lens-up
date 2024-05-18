//import { Middleware, isRejected } from '@reduxjs/toolkit';
import { ApiProvider } from '@reduxjs/toolkit/dist/query/react';
import { galleryApiMiddleware } from '../../src/app/store/middlewares/GalleryApiMiddleware'
import {/*describe, expect, test, */it} from '@jest/globals';
import { buildGetDefaultMiddleware } from '@reduxjs/toolkit/dist/getDefaultMiddleware';
import { Action, applyMiddleware, configureStore, createAction, createStore, PayloadAction } from '@reduxjs/toolkit';
import { store } from '../../src/app/store/store';

export const create = () => {
  const store = {
    getState: jest.fn(() => ({})),
    dispatch: jest.fn()
  }
  const next = jest.fn()

  const invoke = (action: unknown) => galleryApiMiddleware(store)(next)(action)

  return { store, next, invoke }
}
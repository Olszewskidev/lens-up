import { cleanup, fireEvent, getAllByAltText, getByAltText, getByText, render, screen, waitFor } from '@testing-library/react'
import { expect, test, describe, beforeAll, beforeEach ,afterEach, afterAll } from 'vitest';
import { setupServer } from 'msw/node';
import { loginSubmit } from '../Login/Login.tsx';
import { getQRCodeUrl } from '../../../src/utils/qRCodeHelper.ts';
import { HttpResponse, http } from 'msw';
import { LoginToGalleryResponse } from '../../../src/types/GalleryApiTypes.ts';

let handlers = [
  http.post(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/login`, async () => {
      let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
      let res = await HttpResponse.json(response);
      return res;
  })
]

let asFragment: () => DocumentFragment;
let baseElement: HTMLElement;

/**
* @vitest-environment jsdom
*/
describe("Home page with no photos", async () => {
  const server = setupServer(...handlers);

  beforeAll(async () => {
    server.listen();
    ({ asFragment, baseElement } = await loginSubmit());
  })
  beforeEach(async () => waitFor(() => {
    expect(global.window.location.pathname).contains("/gallery/0");
  }))
  afterEach(cleanup);  
  afterAll(() => {
      server.close()
  })

  test('No photo hub must show qr code card in home page', async () => {
    const firstRender = asFragment();

    expect(firstRender).toMatchSnapshot();
  });

  test('No photo hub must have qr-code url in image source', async () => {
    const firstRender = asFragment();

    expect(getByAltText(baseElement, "QR code").getAttribute("src")).toEqual(getQRCodeUrl());

    expect(firstRender).toMatchSnapshot();
  });
});

describe("Home page with photos", async () => {
  handlers.concat(http.post(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/hubs/gallery`, async (galleryId) => {
    let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
    let res = await HttpResponse.json(response);
    return res;
  }))

  const server = setupServer(...handlers);

  beforeAll(async () => {
    server.listen();
    ({ asFragment, baseElement } = await loginSubmit());
  })
  beforeEach(async () => waitFor(() => {
    expect(global.window.location.pathname).contains("/gallery/0");
  }))
  afterEach(cleanup);  
  afterAll(() => {
      server.close()
  })

  test('Photo gallery must be shown', () => {
    const firstRender = asFragment();

    expect(firstRender).toMatchSnapshot();
  });
})
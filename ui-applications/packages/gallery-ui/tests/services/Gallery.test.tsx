import { waitFor, cleanup, fireEvent, getByTestId, render, screen } from '@testing-library/react';
import { BrowserRouter, useNavigate } from 'react-router-dom';
import { Provider } from 'react-redux';
import { expect, test, beforeAll, afterEach, afterAll } from 'vitest';
import { LoginPage } from '../../src/pages/Login/LoginPage';
import { galleryApi, useLoginToGalleryMutation } from '../../src/services/GalleryApi';
import { LoginToGalleryPayload, LoginToGalleryResponse, PhotoItem } from '../../src/types/GalleryApiTypes';
import { store } from '../../src/app/store/store';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { HttpResponse, http } from 'msw';
import { setupServer } from 'msw/node';
import { loginSubmit } from '../utils/LoginEvents';

let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
let actual: LoginToGalleryResponse;

const handlers = [
  http.post(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/login`, async () => {
    let res = await HttpResponse.json(response);
    return res;
  }),
  http.post(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/gallery/0`, () => {
    let photo: PhotoItem[] = [{ id: "0", url: "url", authorName: "Author", wishesText: "Best wishes"}];
    return HttpResponse.json(photo);
  } )
]

const LoginComponent = () => {
  const [loginToGallery, { isLoading }] = useLoginToGalleryMutation();

  const handleLoginTest = () => {
      loginToGallery({ enterCode: '0' })
          .unwrap()
          .then((payload) => {
              actual = payload;
          });
  }

  return (
      <div data-testid="test-component" onClick={(e) => { e.preventDefault(); handleLoginTest() }} />
  )
}

const server = setupServer(...handlers);

beforeAll(() => {
  server.listen()
})
afterEach(cleanup);
afterAll(() => {
  server.close()
})

/**
* @vitest-environment jsdom
*/
test("Login muttation must match login api request", async () => {
  const {getByTestId} = render(
    <Provider store={store}>
      <BrowserRouter>
        <LoginComponent />
      </BrowserRouter>
    </Provider>
  );
    
  fireEvent.click(getByTestId("test-component"));
  await waitFor(() => {
    expect(actual).toEqual(response);
  });
});

/**
 * @jest-environment jsdom
 */
test("Gallery should show photos", async () => {
  /*const { asFragment, baseElement } = */await loginSubmit();
  
  /*const { getByText, asFragment } = render(
    <Provider store={store}>
      <BrowserRouter>
        <LoginPage />
      </BrowserRouter>
    </Provider>
  );*/

  //galleryApi.endpoints.getGalleryPhotos.initiate("test");

  //fireEvent.change(getByText("Login to your gallery"));

  await waitFor(() => {
    expect(0).toEqual(2);
  })
});
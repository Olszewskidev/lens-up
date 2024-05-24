import { screen, render, fireEvent } from "@testing-library/react"
import { Options, userEvent } from "@testing-library/user-event"
import { expect, test, describe, vi } from 'vitest';
import App from "../../src/App";
import { userEventApi } from "@testing-library/user-event/dist/cjs/setup/api.js";
import { loginSubmit } from "./Login";
import { LoginPage } from "../../src/pages";
import  LoginForm  from "../../src/pages/Login/components/LoginForm";
import { HttpResponse, graphql, http} from "msw";
import { setupServer } from "msw/node";
import { setupWorker } from "msw/browser";
import { saveQRCode } from "../../src/utils/qRCodeHelper";
import { LoginToGalleryPayload, LoginToGalleryResponse, PhotoItem } from '../../src/types/GalleryApiTypes';
import { galleryApiMiddleware } from "../../src/app/store/middlewares/GalleryApiMiddleware";
import { Dispatch, Middleware, Middleware, UnknownAction } from '@reduxjs/toolkit';

/**
* @vitest-environment jsdom
*/
describe("Router test", () => {
    test("Check login route", async () => {
        render(<App />)

        const user = userEvent.setup();
        expect(global.window.location.pathname).equals("/");
    })

    test("Check home route on submit", async () => {
        //render(<App />);
        const worker = setupServer(...handlers);
        await worker.listen();

        const {asFragment} = await loginSubmit();

        //vi.fn(() => (next: any) => (action: Dispatch<UnknownAction>) => {return next(action)}).mockImplementation(galleryApiMiddleware);
        
        //const user = userEvent.setup();
        expect(asFragment()).toMatchSnapshot();
        expect(global.window.location.pathname).equals("/gallery/0");
    })
});

const handlers = [
    http.post('/login', async () => {
      /*console.log(params);
      saveQRCode(params.qrCodeUrl);
      return navigate(`/gallery/${params.galleryId}`);*/
      let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
      let res = await new Response();
      (await res.formData()).set("enterCode", '0');
      (await res.formData()).set("galleryId", '0');
      (await res.formData()).set("qrCodeUrl", '0');
      return res;
    })
  ]
import { screen, render, fireEvent } from "@testing-library/react"
import { Options, userEvent } from "@testing-library/user-event"
import { expect, test, describe, vi } from 'vitest';
import App from "../../src/App";
import { loginSubmit } from "./Login";
import { HttpResponse, http } from "msw";
import { setupServer } from "msw/node";
import { LoginToGalleryResponse } from '../../src/types/GalleryApiTypes';
import { Resolver } from "dns";
import { galleryApi } from "../../src/services/GalleryApi";

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
        console.log(global.window.location.host);
        const server = setupServer(...handlers);
        server.listen();

        const { asFragment, baseElement } = await loginSubmit();

        //vi.fn(() => (next: any) => (action: Dispatch<UnknownAction>) => {return next(action)}).mockImplementation(galleryApiMiddleware);

        const url = global.window.location.href;
        expect(baseElement.baseURI).contains("/gallery/0");
        const vrib = global.window.location.href;
    })
});

const handlers = [
    http.post('https://localhost:3000/login', async () => {
        let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
        let res = await HttpResponse.json(response);
        return res;
    })
]
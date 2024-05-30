import { render, waitFor, cleanup } from "@testing-library/react"
import { expect, test, describe, vi, beforeAll, afterAll, afterEach } from 'vitest';
import App from "../../src/App";
import { loginSubmit } from '../utils/LoginEvents';
import { HttpResponse, http } from "msw";
import { setupServer } from "msw/node";
import { LoginToGalleryResponse } from '../../src/types/GalleryApiTypes';

/**
* @vitest-environment jsdom
*/
describe("Router path tests", () => {
    const server = setupServer(...handlers);

    beforeAll(() => {
        server.listen()
    })
    afterEach(cleanup);
    afterAll(() => {
        server.close()
    })

    test("Check default route", async () => {
        render(<App />);

        expect(global.window.location.pathname).equals("/");
    })

    test("Check home route on submit", async () => {
        await loginSubmit();

        await waitFor(() => {
            expect(global.window.location.pathname).contains("/gallery/0");
        });
    })
});

const handlers = [
    http.post(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/login`, async () => {
        let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
        let res = await HttpResponse.json(response);
        return res;
    })
]
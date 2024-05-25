import { screen, render, fireEvent, waitFor } from "@testing-library/react"
import { Options, userEvent } from "@testing-library/user-event"
import { expect, test, describe, vi, beforeAll, afterAll } from 'vitest';
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
    const server = setupServer(...handlers);

    beforeAll(() => {
        server.listen()
    })
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
    http.post('https://localhost:3000/login', async () => {
        let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
        let res = await HttpResponse.json(response);
        return res;
    })
]
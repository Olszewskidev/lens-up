import { render, waitFor, cleanup, screen, getByText, getByLabelText, fireEvent, getByPlaceholderText } from "@testing-library/react"
import { expect, test, describe, vi, beforeAll, afterAll, afterEach } from 'vitest';
import { HttpResponse, http } from "msw";
import { setupServer } from "msw/node";
import AddPhotoToGalleryPage from "../src/pages/AddPhotoToGallery/AddPhotoToGalleryPage"
import { store } from "../src/app/store";
import { Provider } from "react-redux";
import { BrowserRouter, Navigate, Route, RouterProvider, Routes } from "react-router-dom";
import userEvent from '@testing-library/user-event'
import App from "../src/App";
import AppRouter from "../src/pages/AppRouter";
import { SuccessCardComponent } from "@lens-up/shared-components";

let file = new File(["happy"], "happy.png", { type: "image/png" });

const handlers = [
    http.post(`${import.meta.env.VITE_PHOTO_COLLECTOR_SERVICE_URL}/upload-photo/0`, async () => {
        return new Response(null, {
            status: 200,
            headers: {
              Allow: 'GET,HEAD,POST',
            },
          })
    })
]

/**
* @vitest-environment jsdom
*/
describe("Add photo to gallery page tests", () => {
    const server = setupServer(...handlers);

    beforeAll(() => {
        server.listen()
    })
    afterEach(cleanup);
    afterAll(() => {
        server.close()
    })

    test("Photo form should be uninitialized", async () => {
        const { asFragment } = render(
            <Provider store={store}>
                <BrowserRouter>
                    <AddPhotoToGalleryPage />
                </BrowserRouter>
            </Provider>);

        expect(asFragment()).toMatchSnapshot();
    })

    test("Photo must upload and render success card", async () => {
        URL.createObjectURL = vi.fn();
        const { container, baseElement, asFragment } = render(
            <Provider store={store}>
                <BrowserRouter>
                    <Routes>
                        <Route path="/upload-photo/:enterCode" element={<AddPhotoToGalleryPage />} />
                    </Routes>
                    <Navigate to="/upload-photo/0" replace={true} />
                </BrowserRouter>
            </Provider>
        );

        const element = container.querySelector("#dropzone-file") as HTMLElement;

        fireEvent.change(element, { target: { files: [file] } });

        fireEvent.change(screen.getByPlaceholderText("Your name"), { target: { value: "author" } });

        fireEvent.change(screen.getByPlaceholderText("..."), { target: { value: "happy" } });

        fireEvent.submit(screen.getByRole('button'));

        await waitFor(() => {
            expect(asFragment()).toMatchSnapshot();
        });
    })
});
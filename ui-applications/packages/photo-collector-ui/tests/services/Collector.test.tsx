import { waitFor, cleanup, fireEvent, getByTestId, render, screen } from '@testing-library/react';
import { BrowserRouter, Navigate, Route, Routes, useNavigate } from 'react-router-dom';
import { Provider } from 'react-redux';
import { expect, test, vi, beforeAll, afterEach, afterAll } from 'vitest';
import { photoCollectorApi, useAddPhotoToGalleryMutation } from '../../src/services/PhotoCollectorApi';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { HttpResponse, http } from 'msw';
import { setupServer } from 'msw/node';
import AddPhotoToGalleryPage from '../../src/pages/AddPhotoToGallery/AddPhotoToGalleryPage';
import { store } from '../../src/app/store';
import { useParams } from "react-router-dom";
import { AddPhotoForm } from '../../src/pages/AddPhotoToGallery/components';
import { SuccessCardComponent } from '@lens-up/shared-components';

const photo = new File(["happy"], "../happy.png", { type: "image/png" });

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

const successCardRender = render(
    <SuccessCardComponent />
).asFragment();

const AddPhotoComponent = () => {
    const { enterCode } = useParams();
    const [addPhoto, { isError, isSuccess, isUninitialized, isLoading }] = useAddPhotoToGalleryMutation();

    const handleAddPhotoTest = () => {
        if (!enterCode)
            return;
        const formData = new FormData();
        formData.append('photo', photo)
        formData.append('AuthorName', "Author")
        formData.append('WishesText', "Happy")
        addPhoto({ enterCode, formData })
    }

    return (
        <>
            {
                isUninitialized && <div data-testid="test-component" onClick={(e) => { e.preventDefault(); handleAddPhotoTest() }} />
            }
            {
                isSuccess && <SuccessCardComponent />
            }
        </>
    )
}

/**
* @vitest-environment jsdom
*/
test("Add photo muttation must return successful photo addition api request", async () => {
    const server = setupServer(...handlers); server.listen();
    URL.createObjectURL = vi.fn();

    const { getByTestId, asFragment } = render(
        <Provider store={store}>
            <BrowserRouter>
                <Routes>
                    <Route path="/upload-photo/:enterCode" element={<AddPhotoComponent />} />
                </Routes>
                <Navigate to="/upload-photo/0" replace={true} />
            </BrowserRouter>
        </Provider>
    );

    fireEvent.click(getByTestId("test-component"));

    let successRender = asFragment();

    await waitFor(() => {
        successRender = asFragment();
        expect(successRender).toEqual(successCardRender);
    }).then(() => expect(successRender).toMatchSnapshot());
});
import { fireEvent, render } from '@testing-library/react'
import { expect, it } from '@jest/globals';
import { LoginPage } from '../../src/pages/Login/LoginPage';
import { galleryApi } from '../../src/services/GalleryApi';
import { LoginToGalleryPayload } from '../../src/types/GalleryApiTypes';

it('Login must be accepted for the gallery', () => {
    const {getByText, asFragment} = render(
        <LoginPage />,
    );

    let payload = { enterCode: "BCD" } as LoginToGalleryPayload;

    galleryApi.endpoints.loginToGallery.initiate(payload);

    const firstRender = asFragment();

    fireEvent.change(getByText("Login to your gallery"));
    
    expect(firstRender).toMatchSnapshot(asFragment());
  });

it('Gallery should show photos', () => {
    const {getByText, asFragment} = render(
        <LoginPage />,
    );

    let payload = { enterCode: "BCD" } as LoginToGalleryPayload;

    galleryApi.endpoints.getGalleryPhotos.initiate("test");

    const firstRender = asFragment();

    fireEvent.change(getByText("Login to your gallery"));
    
    expect(firstRender).toMatchSnapshot(asFragment());
  });
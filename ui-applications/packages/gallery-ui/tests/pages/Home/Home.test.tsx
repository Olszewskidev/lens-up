import { buildQueries, cleanup, fireEvent, getByAltText, getSuggestedQuery, render, screen, waitFor } from '@testing-library/react'
import { expect, test, describe, vi, beforeAll, beforeEach, afterEach, afterAll } from 'vitest';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { setupServer } from 'msw/node';
import { loginSubmit } from '../../utils/LoginEvents.tsx';
import { getQRCodeUrl } from '../../../src/utils/qRCodeHelper.ts';
import { HttpResponse, http as mswhttp } from 'msw';
import { LoginToGalleryResponse } from '../../../src/types/GalleryApiTypes.ts';
import { Server, Socket } from "socket.io";
import { io as ioc, Socket as ClientSocket } from "socket.io-client";
import { store } from '../../../src/app/store/store.ts';
import { store as photoStore } from '../../../../photo-collector-ui/src/app/store.ts';
import { Provider } from 'react-redux'
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import AddPhotoToGalleryPage from "../../../../photo-collector-ui/src/pages/AddPhotoToGallery/AddPhotoToGalleryPage.tsx";
import { ErrorCardComponent, SuccessCardComponent } from '@lens-up/shared-components';
import { HomePage, LoginPage } from '../../../src/pages/index.ts';
import { galleryApi, useGetGalleryPhotosQuery } from '../../../src/services/GalleryApi.tsx';
import { useCypressSignalRMock as MockHubConnectionBuild, hubPublish } from 'cypress-signalr-mock';
import PhotoGallery from '../../../src/pages/Home/components/PhotoGallery.tsx';
import PhotoGalleryWithCarousel from '../../../src/pages/Home/components/PhotoGalleryWithCarousel.tsx';
import QRCodeCard from '../../../src/pages/Home/components/QRCodeCard.tsx';

const photo = new File(["happy"], "happy.png", { type: "image/png" });

function waitForSocket(socket: Socket | ClientSocket, event: string) {
  return new Promise((resolve) => {
    socket.once(event, resolve);
  });
}

let handlers = [
  mswhttp.post(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/login`, async () => {
    let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
    let res = await HttpResponse.json(response);
    return res;
  }),
  mswhttp.post(`${import.meta.env.VITE_PHOTO_COLLECTOR_SERVICE_URL}/upload-photo/0`, async () => {
    const socket = MockHubConnectionBuild("testhub", {
      enableForVitest: true,
    });

    hubPublish(
      'testhub',
      'PhotoUploadedToGallery',
      {
        value: { id: '0', url: 'happy.png', authorName: "Author", wishesText: "happy.png" }
      }
    );

    //socket?.send("PhotoUploadedToGallery", photo);

    return new Response(null, {
      status: 200,
      headers: {
        Allow: 'GET,HEAD,POST',
      },
    })
  })
]

let asFragment: () => DocumentFragment;
let HomeasFragment: () => DocumentFragment;
let baseElement: HTMLElement;
let homeRender: DocumentFragment;

/**
* @vitest-environment jsdom
*/
describe("Home page without photos", async () => {
  const server = setupServer(...handlers);

  beforeAll(async () => {
    server.listen();
    ({ asFragment, baseElement } = await loginSubmit());
  })
  beforeEach(async () => waitFor(() => {
    expect(global.window.location.pathname).contains("/gallery/0");
    homeRender = asFragment();
  }))
  afterEach(cleanup);
  afterAll(() => {
    server.close()
  })

  test("Only qr code card must be shown in home", async () => {
    const firstRender = asFragment();

    expect(firstRender).toMatchSnapshot();
  });

  test("Only qr code card with qr-url must be shown in home", async () => {
    const firstRender = asFragment();

    expect(getByAltText(baseElement, "QR code").getAttribute("src")).toEqual(getQRCodeUrl());

    expect(firstRender).toMatchSnapshot();
  });
});

describe("Home page with photos", async () => {
  let server = setupServer(...handlers);

  beforeAll(async () => {

    server.listen();
    ({ asFragment, baseElement } = await loginSubmit());
    HomeasFragment = asFragment;
  })
  beforeEach(async () => waitFor(() => {
    expect(global.window.location.pathname).contains("/gallery/0");
    homeRender = asFragment();
  }))
  afterEach(cleanup);
  afterAll(() => {
    server.close()
  })

  test("Photo gallery must be shown in home", async () => {
    URL.createObjectURL = vi.fn();

    let homeGalleryRender = render(
      <div className="min-h-screen bg-black" >
        <PhotoGalleryWithCarousel photoItems={[{ id: '0', url: 'happy.png', authorName: "Author", wishesText: "happy.png" }]} />
        <QRCodeCard qRCodeUrl={'QRCodeUrl'} hasPhotos={true} />
      </div>
    ).asFragment();

    const socket = MockHubConnectionBuild("testhub", {
      enableForVitest: true,
    });

    hubPublish(
      'testhub',
      'PhotoUploadedToGallery',
      {
        id: '0', url: 'happy.png', authorName: "Author", wishesText: "happy.png"
      }
    );

    let rendered = asFragment();

    await waitFor(() => {
      rendered = asFragment()
      if (rendered.firstChild != null)
        expect(rendered).toEqual(homeGalleryRender);
      expect(rendered).not.toBe(null);
    }, { timeout: 50000 }).then(() => expect(rendered).toMatchSnapshot());
  });
})
import { buildQueries, cleanup, fireEvent, getByAltText, getSuggestedQuery, render, screen, waitFor } from '@testing-library/react'
import { expect, test, describe, vi, beforeAll, beforeEach, afterEach, afterAll } from 'vitest';
import { useCypressSignalRMock as MockHubConnectionBuild } from 'cypress-signalr-mock';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { setupServer } from 'msw/node';
import { loginSubmit } from '../../utils/LoginEvents.tsx';
import { getQRCodeUrl } from '../../../src/utils/qRCodeHelper.ts';
import { HttpResponse, http as mswhttp } from 'msw';
import { LoginToGalleryResponse } from '../../../src/types/GalleryApiTypes.ts';
import { Server, Socket } from "socket.io";
import { io as ioc, Socket as ClientSocket } from "socket.io-client";
import { IncomingMessage, ServerResponse } from "http";
import http from "http";
import { AddressInfo } from 'node:net';
import { startServer } from "../../webSocketTestUtils.tsx";
import { store } from '../../../src/app/store/store.ts';
import { store as photoStore } from '../../../../photo-collector-ui/src/app/store.ts';
import { Provider } from 'react-redux'
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import AddPhotoToGalleryPage from "../../../../photo-collector-ui/src/pages/AddPhotoToGallery/AddPhotoToGalleryPage.tsx";
import { ErrorCardComponent, SuccessCardComponent } from '@lens-up/shared-components';
import { HomePage, LoginPage } from '../../../src/pages/index.ts';
import { galleryApi, useGetGalleryPhotosQuery } from '../../../src/services/GalleryApi.tsx';

const photo = new File(["happy"], "happy.png", { type: "image/png" });

function waitForSocket(socket: Socket | ClientSocket, event: string) {
  return new Promise((resolve) => {
    socket.once(event, resolve);
  });
}

import cy from 'cypress-signalr-mock';

let handlers = [
  mswhttp.post(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/login`, async () => {
    let response: LoginToGalleryResponse = { enterCode: '0', galleryId: '0', qrCodeUrl: 'QRCodeUrl' };
    let res = await HttpResponse.json(response);
    return res;
  }),
  mswhttp.post(`${import.meta.env.VITE_PHOTO_COLLECTOR_SERVICE_URL}/upload-photo/0`, async () => {
    const socket = new HubConnectionBuilder().withUrl(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/hubs/gallery?galleryId=0`).build();

    await socket.start().then(() => {
      console.log("Connected to socket.")
    }).catch(() => {
      console.error("Failed during socket connection.")
    })

    socket.send("PhotoUploadedToGallery", photo);

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
describe("Home page with no photos", async () => {
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

  test("No photo hub must show qr code card in home page", async () => {
    const firstRender = asFragment();

    expect(firstRender).toMatchSnapshot();
  });

  test("No photo hub must have qr code url in image source", async () => {
    const firstRender = asFragment();

    expect(getByAltText(baseElement, "QR code").getAttribute("src")).toEqual(getQRCodeUrl());

    expect(firstRender).toMatchSnapshot();
  });
});

const successCardRender = render(
  <SuccessCardComponent title="Congratulations!" text="You just joined to the party." />
).asFragment();

const mock2 = vi.spyOn(HubConnectionBuilder.prototype, 'build');

mock2.mockImplementationOnce(() => MockHubConnectionBuild("testhub") ?? 
  new HubConnectionBuilder().withUrl(`${import.meta.env.VITE_GALLERY_SERVICE_URL}/hubs/gallery?galleryId=0`).build()
);

const mock = vi.spyOn(galleryApi.endpoints.getGalleryPhotos, 'useQuery');


describe("Home page with photos", async () => {
  let io: Server;

  let serverSocket: Socket | undefined;
  let clientSocket: ClientSocket;

  const server = setupServer(...handlers);
  var Sserver: http.Server<typeof IncomingMessage, typeof ServerResponse>;

  beforeAll(async () => {
    //Sserver = await startServer(3000);

    server.listen();
    //({ asFragment, baseElement } = await loginSubmit());
    //HomeasFragment = asFragment;
    console.log(global.window.location.href);
  })
  beforeEach(async () => waitFor(() => {
    //expect(global.window.location.pathname).contains("/gallery/0");
    //homeRender = asFragment();
  }))
  afterEach(cleanup);
  afterAll(() => {
    server.close()
    //Sserver.close()
  })

  test("Photo gallery must be shown in home", async () => {
    URL.createObjectURL = vi.fn();
    const { container, baseElement, asFragment } = render(
      <Provider store={store}>
        <BrowserRouter>
          <Routes>
            <Route path="/gallery/:enterCode" element={<HomePage />} />
          </Routes>
          <Navigate to="/gallery/0" replace={true} />
        </BrowserRouter>
      </Provider>
    );

    const hub = (new HubConnectionBuilder()).build();

    cy.hubPublish(
      'testhub', // The name of the hub
      'PhotoUploadedToGallery', // The name of the message type
      {
          message: photo, // The message payload
      },
    );
    

    hub.invoke("PhotoUploadedToGallery", photo);
    hub.send("PhotoUploadedToGallery", photo);

    expect(asFragment()).toMatchSnapshot();
  });

  test("Photo gallery must be shown", async () => {
    console.log(global.window.location.href);
    URL.createObjectURL = vi.fn();
    const { container, baseElement, asFragment } = render(

      <Provider store={photoStore}>
        <BrowserRouter>
          <Routes>
            <Route path="/upload-photo/:enterCode" element={<AddPhotoToGalleryPage />} />
          </Routes>
          <Navigate to="/upload-photo/0" replace={true} />
        </BrowserRouter>
      </Provider>

    );

    const element = container.querySelector("#dropzone-file") as HTMLElement;

    console.log(global.window.location.href);

    fireEvent.change(element, { target: { files: [photo] } });

    fireEvent.change(screen.getByPlaceholderText("Your name"), { target: { value: "author" } });

    fireEvent.change(screen.getByPlaceholderText("..."), { target: { value: "happy" } });

    fireEvent.submit(screen.getByRole('button'));

    let successRender = asFragment();

    await waitFor(() => {
      successRender = asFragment();
      expect(successRender).toEqual(successCardRender);
    }, { timeout: 50000 }).then(() => expect(asFragment()).toMatchSnapshot());

    //const [response] = await Promise.all(promises);
  }, { timeout: 50000 });
})
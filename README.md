# LensUp
![lens-up-logo](/docs/lens-up-logo.png)

---

Have you ever attended a wedding or a birthday party? If yes, you probably saw photo booth there. You get in, take a photo and paste it in the guest book - simple and fun. But what if we could bring this fun into the digital world? This is where **LensUp** comes to the rescue. **LensUp** is a web application that serves as a virtual gallery, allowing party guests to upload their photos from the event and also write down their wishes.

# :mega: Project status
The video shows the project status as of `26.04.2024` and the core functionality of the application.
[![LensUp state](https://img.youtube.com/vi/73V7og0nS38/maxresdefault.jpg)](https://www.youtube.com/watch?v=73V7og0nS38)


# :rocket: 100 days roadmap

**Conclusion:** The main goal of 100 days plan is to deliver [core functionality](#core-functionality) related to adding photos and wishes. I also want to do that without any extra cost for Azure services. So whole app should works on local environment with azure simulators like Azurite. This fact forces certain application architecture decisions, because not all Azure services can be simulated locally. Additionally MVP version doesn't focus on authentication, authorization and security.

#### Core functionality

![lens-up-core-func](/docs/lens-up-core-func.svg)

1. Guest sees `Gallery UI` with other guest photos. He also sees QR code in the application corner.

2. Guest scans QR code, which redirects him to `Photo Collector UI` and there he can upload photo and wishes.

3. `Photo Collector UI` send data to `Photo Collector Service`.

4. `Photo Collector Service` upload guest photo to blob storage.

5. `Photo Collector Service` add guest data to database.

6. `Photo Collector Service` publish event to queue.

7. `Gallery Service` receive the event and process it.

8. `Gallery Service` push notification about new photo in the gallery for `Gallery UI`, and then `Gallery UI` adds new guest photo to gallery collection.




#### LensUp big picture:

![lens-up-big-picture](/docs/lens-up-big-picture.svg)



Description:

- Backend services:

  - [Photo Collector Service (.NET Web API)](backend-services/photo-collector-service)
  - [Gallery Service (.NET Web API + Azure Functions)](backend-services/gallery-service)
  - [BackOffice Service (.NET Web API)](backend-services/back-office-service) 

- UI applications:

  - [Photo Collector UI (React.js)](ui-applications/packages/photo-collector-ui)
  - [Gallery UI (React.js)](ui-applications/packages/gallery-ui)
  - [Back Office UI (React.js)](ui-applications/packages/back-office-ui)

- Azure Services:

  - Table Storage
  - Queue Storage
  - Blob Storage




## :bomb: How to run LensUp locally

You can run the project locally on your machine using Docker. Follow the steps below to run the application locally:

1. Before we start you should generate `dev-certs` for **LensUp** on your machine. This operation is required to hosting ASP.NET Core images with Docker over HTTPS. So generate a certificate using these commands:

   ```bash
   dotnet dev-certs https -ep "%USERPROFILE%\.aspnet\https\lens-up.pfx" -p localCertPassword
   dotnet dev-certs https --trust
   ```

   **Replace `%USERPROFILE%` with your computer name.** Example `"C:\Users\Dell Precision 7520\.aspnet\https\lens-up.pfx"`

   **For local development purposes, we will use the password `localCertPassword`. Do not change this, as the same password is used in the `docker-compose.yml` file.**

   The above commands should generate a `lens-up.pfx` certificate, and should place it in the directory as shown in the screenshot below.

   ![lens-up-cert](/docs/lens-up-cert.png)

   **This is necessary step, because docker-compose refers to that certificate!**

2. Install `docker desktop` on your machine *(skip if you already done it)*.

3. Run your `docker desktop` application.

4. In the main project directory (`lens-up`), where the `docker-compose.yml` file is located, run the command `docker-compose build`. This will build 7 necessary LensUp images. **The first time build may take a few minutes (up to 10 minutes).** After completing these steps, you should see new images in the Docker Desktop application.

   ![lens-up-docker-images](/docs/lens-up-docker-images.png)

5. After the build command, run the `docker-compose up` to start the entire infrastructure. You should see in Docker Desktop that 7 containers related to LensUp have been started.

   ![lens-up-containers](/docs/lens-up-containers.png)

6. Now the entire application is running on your machine. You can use the following addresses:

   - Backend services:

     - `LensUp.BackOfficeService.API` swagger - https://localhost:8085/swagger/index.html
     - `LensUp.GalleryService.API` swagger - https://localhost:8083/swagger/index.html
     - `LensUp.GalleryService.WebhookTriggerSimulator` - http://localhost:8086/
     - `LensUp.PhotoCollectorService.API` swagger - https://localhost:8081/swagger/index.html

   - UI applications:

     - `LensUp.GalleryService.UI` - http://localhost:5001/

     - `LensUp.PhotoCollectorService.UI` - http://localhost:5002/

       *On LensUp.PhotoCollectorService.UI you will see error page, because you need to navigate to the view associated with a specific gallery, which you haven't created yet.*



**How to create your first gallery and have fun with LensUp?**

1. Go to `LensUp.BackOfficeService.API` - https://localhost:8085/swagger/index.html

2. Use `Create` endpoint to create your gallery. The endpoint returns the gallery identifier after it is created **(1)**.

   ![lens-up-create-endpoint](/docs/lens-up-create-endpoint.png)

3. Before using the gallery, we need to activate it. In that case use `Activate` endpoint and pass `galleryId` and `endDate` in request body. Remember the `endDate` is validated and must be greater than the current time. Otherwise, your gallery will be treated as expired. The endpoint returns the gallery `enterCode` after it is activated **(1)**.

   ![lens-up-activate-endpoint](/docs/lens-up-activate-endpoint.png)

4. With your gallery `enterCode` you can open your gallery using `LensUp.GalleryService.UI` - http://localhost:5001/

   Log in to your gallery using `enterCode`.

   ![lens-up-login-form](/docs/lens-up-login-form.png)

5. Now you can scan gallery QR code and upload photos to it. The code redirects to a form for adding photos to the gallery. You can use browser tool to scan QR code or if it doesn't work you can just go to `http://localhost:5002/upload-photo/{enterCode}`.

   ![lens-up-gallery-qr-code](/docs/lens-up-gallery-qr-code.png)

6. QR Code redirects you to add photo and wishes form. Now you can upload your data to gallery.

   ![lens-up-gallery-qr-code](/docs/lens-up-upload-photo-form.png)

7. After successfully completing the form, we should see success notification and the photo should appear in the gallery.

   ![lens-up-upload-flow](/docs/lens-up-upload-flow.gif)

## :clipboard: 100 days TODO list

#### General aspects:

- [x] Set up monorepo infrastructure for backend
- [x] Set up monorepo infrastructure for frontend 
- [x] Create LensUp running scripts
- [x] Create documentation how to run LensUp locally
- [ ] Create movie tutorial "How to run LensUp locally"
- [ ] Create documentation how to run LensUp locally in WLAN network.



#### Backend aspects:

- [x] Create shared common backend project
- [x] Create coding rule set for backend services
- [x] Create Queue package in shared common project
- [x] Create Table Storage package in shared common project
- [x] Crate Blob Package in shared common project

##### Back Office Service:

- [x] Create project structure
- [x] Create add user endpoint
- [x] Create add gallery endpoint
- [x] Create activate gallery endpoint

##### Gallery Service:

- [x] Create project structure
- [ ] Create get gallery info endpoint
- [x] Create gallery notification hub

##### Photo Collector Service:

- [x] Create project structure
- [x] Create add photo and wishes endpoint



#### Frontend aspects: 

- [x] Create shared common frontend project
- [ ] Create coding rule set for frontend applications
- [x] Create shared components in shared common frontend project

##### Back Office UI:

- [ ] Create Admin Panel *(optional)*

##### Gallery UI:

- [x] Create login to gallery page
- [x] Create main page with gallery
- [x] Handle notifications from backend
- [ ] Add animations to gallery
- [ ] Add carousel for photos

##### Photo Collector UI:

- [x] Create single page with add photo and wishes form

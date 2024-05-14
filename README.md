# LensUp
![lens-up-logo](/docs/lens-up-logo.png)

---

Have you ever attended a wedding or a birthday party? If yes, you probably saw photo booth there. You get in, take a photo and paste it in the guest book - simple and fun. But what if we could bring this fun into the digital world? This is where **LensUp** comes to the rescue. **LensUp** is a web application that serves as a virtual gallery, allowing party guests to upload their photos from the event and also write down their wishes.

- [Project status](#Project status)
- [100 days roadmap](#100 days roadmap)
- [How to run LensUp locally](#How to run LensUp locally)
- [TODO list (100 days)](#TODO list (100 days))

# Project status
The video shows the project status as of `26.04.2024` and the core functionality of the application.
[![LensUp state](https://img.youtube.com/vi/73V7og0nS38/maxresdefault.jpg)](https://www.youtube.com/watch?v=73V7og0nS38)


# 100 days roadmap

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




## How to run LensUp locally

You can run the project locally on your machine using Docker. All services will be hosted on your WLAN network. Follow the steps below to run the application locally.

1. Before we start you should generate `dev-certs` for LensUp on your machine. This operation is required to hosting ASP.NET Core images with Docker over HTTPS. So generate a certificate and configure the local machine:

   ```bash
   dotnet dev-certs https -ep "%USERPROFILE%\.aspnet\https\lens-up.pfx" -p localCertPassword
   dotnet dev-certs https --trust
   ```

   **Replace `%USERPROFILE%` with your computer name.** Example `"C:\Users\Kamil\.aspnet\https\lens-up.pfx"`

   **This is necessary step, because docker-compose refers to that certificate**.

2. Install `docker desktop` on your machine (skip if you already done it).

3. Run your `docker desktop` application.

4. To be continued.



## TODO list (100 days)

#### General aspects:

- [x] Set up monorepo infrastructure for backend
- [x] Set up monorepo infrastructure for frontend 
- [ ] Create LensUp running scripts
- [ ] Create documentation how to run LensUp locally



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

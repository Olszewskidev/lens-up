# LensUp

Have you ever been on wedding or birthday party? If yes, you probably saw photo booth there. You get in, take a photo and paste it in the guest book. Simple and fun. What if we wanted to move this fun to the digital world? Here **LensUp** comes to help. This web application is a virtual gallery, where party guests can add their photos from event and write down wishes as well.



# 100 days roadmap

**Conclusion:** The main goal of 100 days plan is to deliver [core functionality](#core-functionality) related to adding photos and wishes. I also want to do that without any extra cost for Azure services. So whole app should works on local environment with azure simulators like Azurite. This fact forces certain application architecture decisions, because not all Azure services can be simulated locally. Additionally MVP version doesn't focus on authentication, authorization and security.

#### LensUp big picture:

![lens-up-big-picture](/docs/lens-up-big-picture.svg)



Description:

- Backend services:
  - Photo Collector Service *(.NET Web API)*
  - Gallery Service *(.NET Web API + Azure Functions)*
  - Back Office Service *(.NET Web API)*
- UI applications:
  - Photo Collector UI *(React.js)*
  - Gallery UI *(React.js)*
  - Back Office UI *(React.js)*
- Azure Services:
  - Table Storage
  - Queue Storage
  - Blob Storage



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

   

## TODO list (100 days)

#### General aspects:

- [ ] Set up monorepo infrastructure for backend
- [ ] Set up monorepo infrastructure for frontend 



#### Backend aspects:

- [ ] Create shared common backend project
- [ ] Create coding rule set for backend services
- [ ] Create Queue package in shared common project
- [ ] Create Table Storage package in shared common project
- [ ] Crate Blob Package in shared common project

##### Back Office Service:

- [ ] Create add user endpoint
- [ ] Create add gallery endpoint
- [ ] Create activate gallery endpoint

##### Gallery Service:

- [ ] Create get gallery info endpoint
- [ ] Create gallery notification hub

##### Photo Collector Service:

- [ ] Create add photo and wishes endpoint



#### Frontend aspects: 

- [ ] Create shared common frontend project
- [ ] Create coding rule set for frontend applications
- [ ] Create shared components in shared common frontend project

##### Back Office UI:

- [ ] Create Admin Panel *(optional)*

##### Gallery UI:

- [ ] Create login to gallery page
- [ ] Create main page with gallery
- [ ] Handle notifications from backend

##### Photo Collector UI:

- [ ] Create single page with add photo and wishes form

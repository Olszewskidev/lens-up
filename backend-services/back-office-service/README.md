# LensUp.BackOfficeService

- [Intro](#intro)

- [Major responsibilities](#major-responsibilities)

---

## Intro

The project has typical .net project folder structure. Inside solution we can find 2 main catalogs- `src` and `tests`. Application code follows `Clean Architecture` approach and we can distinguish the following layers there:

​	:open_file_folder:**src** *(contains the actual source code for the application)*

​		:books:LensUp.BackOfficeService.API

​		:books:LensUp.BackOfficeService.Application

​		:books:LensUp.BackOfficeService.Domain

​		:books:LensUp.BackOfficeService.Infrastructure

​	:open_file_folder:**tests** *(contains test projects)*

​		:books:LensUp.BackOfficeService.UnitTests



**Project Overview:**

- **Technology Stack:** .NET 8

- **Architecture:** Clean Architecture design

- **Functionality:** Gallery management

  

## Major responsibilities

The primary purpose of this service is to facilitate the creation of galleries and their activation through simple processes.

**Gallery Creation:**

- Users can create a gallery by providing a name for it.
- Upon creation, the gallery is assigned to the user who initiated the action.

​	Dedicated endpoint:	

```http
curl -X 'POST' \
  'https://localhost:7205/Gallery/Create' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "string"
}'
```



**Gallery Activation:**

- Activation involves two main components: 
  - Generation of an access code for the gallery. 
  - Generation of a QR code linked to the gallery photo upload page.

```http
curl -X 'POST' \
  'https://localhost:7205/Gallery/Activate' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "galleryId": "string",
  "endDate": "2024-03-29T15:59:55.576Z"
}'
```


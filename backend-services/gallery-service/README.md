# LensUp.GalleryService

- [Intro](#intro)

- [Major responsibilities](#major-responsibilities)

---

## Intro

The project has typical .net project folder structure. Inside solution we can find 2 main catalogs- `src` and `tests`. Application code follows `Clean Architecture` approach and we can distinguish the following layers there:

​	**src** *(contains the actual source code for the application)*

​		- LensUp.GalleryService.API

​		- LensUp.GalleryService.Application

​	    - LensUp.GalleryService.Domain

​	    - LensUp.GalleryService.Infrastructure

​		- LensUp.GalleryService.WebhookTriggerSimulator

​	**tests** *(contains test projects)*

​		- LensUp.GalleryService.UnitTests



**Project Overview:**

- **Technology Stack:** .NET 8

- **Architecture:** Clean Architecture design

- **Functionality:** Gallery read side

  

## Major responsibilities

The primary purpose of this service is to read galleries data.

**Login:**

- Users can login to gallery and open it on device.

​	Dedicated endpoint:	

```http
curl -X 'POST' \
  'https://localhost:7205/Login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "enterCode": "number"
}'
```



**WebhookTriggerSimulatorFunction:**

- Azure function created in `LensUp.GalleryService.WebhookTriggerSimulator` to read events from Azure Queue storage.


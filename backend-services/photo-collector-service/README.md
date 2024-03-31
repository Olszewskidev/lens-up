# LensUp.PhotoCollectorService

- [Intro](#intro)

- [Major responsibilities](#major-responsibilities)

---

## Intro

The project is designed to allow users (guests) to upload images to a selected gallery. It is built on the .NET platform and employs a minimalist architecture to ensure efficient performance and a small footprint.

**Project Overview:**

- **Technology Stack:** .NET 8
- **Architecture:** Minimalist design to optimize performance and minimize size
- **Functionality:** Image upload to selected galleries



## Major responsibilities

The primary purpose of this service is to upload guest photos to selected gallery.

**Upload**

- Guest can upload his photo to specified gallery using `enterCode`

​		Dedicated endpoint:

```http
curl -X 'POST' \
  'https://localhost:7147/upload-photo/{galleryEnterCode}' \
  -H 'accept: */*' \
  -H 'Content-Type: multipart/form-data' \
  -F 'photoFile=@{guestPhotoFile};type=image/png'
```

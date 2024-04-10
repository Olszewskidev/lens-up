import { createBrowserRouter } from "react-router-dom";
import AddPhotoToGalleryPage from "./AddPhotoToGallery/AddPhotoToGalleryPage";
import { NotFoundComponent, PageRoot } from "../components";

enum AppRoutes {
  UPLOAD_PHOTO = "/upload-photo/:enterCode"
}

const AppRouter = createBrowserRouter([
  {
    path: AppRoutes.UPLOAD_PHOTO,
    element: <PageRoot />,
    errorElement: <NotFoundComponent />,
    children: [
      {
        path: AppRoutes.UPLOAD_PHOTO,
        element: <AddPhotoToGalleryPage />
      }
    ],
  },
]);

export default AppRouter
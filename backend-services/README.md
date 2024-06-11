# Backend-services
Welcome to the `backend-services`Monorepo! This repository houses multiple .NET projects and libraries, consolidated to streamline development and dependency management.

## Project Structure

The repository is organized into the following structure

```bash
/back-office-service
    /src
    /tests
/common
	/src
/gallery-service
    /src
    /tests
/photo-collector-service
    /src
    /tests
Directory.Build.props
Directory.Package.props
README.md
StyleCop.ruleset
```



- `back-office-service` - contains files related to LensUp.BackOfficeService project.
- `common` - contains files shared between all backend projects.
- `gallery-service`  - contains files related to LensUp.GalleryService project.
- `photo-collector-service`  - contains files related to LensUp.PhotoCollectorService project.
- `Directory.Build.props` - is used to define common MSBuild properties shared across all projects in the repository.
- `Directory.Package.props` - is used to manage common NuGet package versions across all projects.
- `StyleCop.ruleset` - is used to enforce consistent code style across the repository.

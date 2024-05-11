FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 8082
EXPOSE 8083

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY ["Directory.Build.props", "/"]
COPY ["Directory.Packages.props", "/"]
COPY ["StyleCop.ruleset", "/"]
COPY ["gallery-service/src/LensUp.GalleryService.API/LensUp.GalleryService.API.csproj", "LensUp.GalleryService.API/"]
COPY ["gallery-service/src/LensUp.GalleryService.Application/LensUp.GalleryService.Application.csproj", "LensUp.GalleryService.Application/"]
COPY ["gallery-service/src/LensUp.GalleryService.Domain/LensUp.GalleryService.Domain.csproj", "LensUp.GalleryService.Domain/"]
COPY ["gallery-service/src/LensUp.GalleryService.Infrastructure/LensUp.GalleryService.Infrastructure.csproj", "LensUp.GalleryService.Infrastructure/"]
COPY ["photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/LensUp.PhotoCollectorService.Contracts.csproj", "/photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/"]
COPY ["common/src/LensUp.Common.AzureTableStorage/LensUp.Common.AzureTableStorage.csproj", "/common/src/LensUp.Common.AzureTableStorage/"]
COPY ["common/src/LensUp.Common.AzureQueueStorage/LensUp.Common.AzureQueueStorage.csproj", "/common/src/LensUp.Common.AzureQueueStorage/"]
COPY ["common/src/LensUp.Common.Types/LensUp.Common.Types.csproj", "/common/src/LensUp.Common.Types/"]

RUN dotnet restore "LensUp.GalleryService.API/LensUp.GalleryService.API.csproj"

COPY . .

WORKDIR "/src/gallery-service/src/LensUp.GalleryService.API"
RUN dotnet build "LensUp.GalleryService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "LensUp.GalleryService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LensUp.GalleryService.API.dll"]
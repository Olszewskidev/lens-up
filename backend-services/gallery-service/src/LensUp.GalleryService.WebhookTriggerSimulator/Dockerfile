
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ["Directory.Build.props", "/"]
COPY ["Directory.Packages.props", "/"]
COPY ["StyleCop.ruleset", "/"]
COPY ["photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/LensUp.PhotoCollectorService.Contracts.csproj", "LensUp.PhotoCollectorService.Contracts/"]
COPY ["common/src/LensUp.Common.Types/LensUp.Common.Types.csproj", "/common/src/LensUp.Common.Types/"]
COPY ["gallery-service/src/LensUp.GalleryService.WebhookTriggerSimulator/LensUp.GalleryService.WebhookTriggerSimulator.csproj", "LensUp.GalleryService.WebhookTriggerSimulator/"]

RUN dotnet restore "LensUp.GalleryService.WebhookTriggerSimulator/LensUp.GalleryService.WebhookTriggerSimulator.csproj"

COPY . .

WORKDIR "/src/gallery-service/src/LensUp.GalleryService.WebhookTriggerSimulator"
RUN dotnet build "LensUp.GalleryService.WebhookTriggerSimulator.csproj" -c -o /app/build

FROM build as publish
RUN dotnet publish "LensUp.GalleryService.WebhookTriggerSimulator.csproj" -c Release -o /app/publish out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "LensUp.GalleryService.WebhookTriggerSimulator.dll"]
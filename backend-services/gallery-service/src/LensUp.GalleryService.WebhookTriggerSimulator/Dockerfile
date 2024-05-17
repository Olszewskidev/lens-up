
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS installer-env

COPY ["Directory.Build.props", "/"]
COPY ["Directory.Packages.props", "/"]
COPY ["StyleCop.ruleset", "/"]
COPY ["photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/LensUp.PhotoCollectorService.Contracts.csproj", "/photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/"]
COPY ["common/src/LensUp.Common.Types/LensUp.Common.Types.csproj", "/common/src/LensUp.Common.Types/"]
COPY ["gallery-service/src/LensUp.GalleryService.WebhookTriggerSimulator/LensUp.GalleryService.WebhookTriggerSimulator.csproj", "/gallery-service/src/LensUp.GalleryService.WebhookTriggerSimulator/"]

RUN dotnet restore "gallery-service/src/LensUp.GalleryService.WebhookTriggerSimulator/LensUp.GalleryService.WebhookTriggerSimulator.csproj"

COPY . /src/func-app

RUN cd /src/func-app && \
mkdir -p /home/site/wwwroot && \
dotnet publish gallery-service/src/LensUp.GalleryService.WebhookTriggerSimulator/LensUp.GalleryService.WebhookTriggerSimulator.csproj -c Release --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

EXPOSE 8080
EXPOSE 8086

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]
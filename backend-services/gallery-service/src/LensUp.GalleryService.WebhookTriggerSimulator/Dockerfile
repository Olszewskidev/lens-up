
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS installer-env
WORKDIR /src

COPY ["Directory.Build.props", "/"]
COPY ["Directory.Packages.props", "/"]
COPY ["StyleCop.ruleset", "/"]
COPY ["photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/LensUp.PhotoCollectorService.Contracts.csproj", "/photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/"]
COPY ["common/src/LensUp.Common.Types/LensUp.Common.Types.csproj", "/common/src/LensUp.Common.Types/"]
COPY ["gallery-service/src/LensUp.GalleryService.WebhookTriggerSimulator/LensUp.GalleryService.WebhookTriggerSimulator.csproj", "LensUp.GalleryService.WebhookTriggerSimulator/"]

RUN cd /src/LensUp.GalleryService.WebhookTriggerSimulator && \
mkdir -p /home/site/wwwroot && \
dotnet publish *.csproj --output /home/site/wwwroot

FROM mcr.microsoft.com/azure-functions/dotnet:4.0
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true

COPY --from=installer-env ["/home/site/wwwroot", "/home/site/wwwroot"]
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "/"]
COPY ["StyleCop.ruleset", "/"]
COPY ["photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/LensUp.PhotoCollectorService.Contracts.csproj", "/photo-collector-service/src/LensUp.PhotoCollectorService.Contracts/"]
COPY ["back-office-service/src/LensUp.BackOfficeService.Contracts/LensUp.BackOfficeService.Contracts.csproj", "/back-office-service/src/LensUp.BackOfficeService.Contracts/"]
COPY ["common/src/LensUp.Common.Types/LensUp.Common.Types.csproj", "/common/src/LensUp.Common.Types/"]
COPY ["webhooks-trigger-simulator/LensUp.WebhooksTriggerSimulator.csproj", "/webhooks-trigger-simulator/"]

RUN dotnet restore "/webhooks-trigger-simulator/LensUp.WebhooksTriggerSimulator.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./webhooks-trigger-simulator/LensUp.WebhooksTriggerSimulator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./webhooks-trigger-simulator/LensUp.WebhooksTriggerSimulator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true
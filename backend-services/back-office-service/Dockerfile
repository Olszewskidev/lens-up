FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 8084
EXPOSE 8085

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY ["Directory.Build.props", "/"]
COPY ["Directory.Packages.props", "/"]
COPY ["StyleCop.ruleset", "/"]
COPY ["back-office-service/src/LensUp.BackOfficeService.API/LensUp.BackOfficeService.API.csproj", "LensUp.BackOfficeService.API/"]
COPY ["back-office-service/src/LensUp.BackOfficeService.Application/LensUp.BackOfficeService.Application.csproj", "LensUp.BackOfficeService.Application/"]
COPY ["back-office-service/src/LensUp.BackOfficeService.Domain/LensUp.BackOfficeService.Domain.csproj", "LensUp.BackOfficeService.Domain/"]
COPY ["back-office-service/src/LensUp.BackOfficeService.Infrastructure/LensUp.BackOfficeService.Infrastructure.csproj", "LensUp.BackOfficeService.Infrastructure/"]
COPY ["common/src/LensUp.Common.AzureBlobStorage/LensUp.Common.AzureBlobStorage.csproj", "/common/src/LensUp.Common.AzureBlobStorage/"]
COPY ["common/src/LensUp.Common.AzureTableStorage/LensUp.Common.AzureTableStorage.csproj", "/common/src/LensUp.Common.AzureTableStorage/"]
COPY ["common/src/LensUp.Common.AzureQueueStorage/LensUp.Common.AzureQueueStorage.csproj", "/common/src/LensUp.Common.AzureQueueStorage/"]
COPY ["common/src/LensUp.Common.Types/LensUp.Common.Types.csproj", "/common/src/LensUp.Common.Types/"]

RUN dotnet restore "LensUp.BackOfficeService.API/LensUp.BackOfficeService.API.csproj"

COPY . .

WORKDIR "/src/back-office-service/src/LensUp.BackOfficeService.API"
RUN dotnet build "LensUp.BackOfficeService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "LensUp.BackOfficeService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LensUp.BackOfficeService.API.dll"]
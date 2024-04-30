FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./src/LensUp.PhotoCollectorService.API/LensUp.PhotoCollectorService.API.csproj", "LensUp.PhotoCollectorService.API/"]
COPY ["./src/LensUp.PhotoCollectorService.Contracts/LensUp.PhotoCollectorService.Contracts.csproj", "LensUp.PhotoCollectorService.Contracts/"]
COPY ["../common/src/LensUp.Common.AzureBlobStorage/LensUp.Common.AzureBlobStorage.csproj", "LensUp.Common.AzureBlobStorage/"]
COPY ["../common/src/LensUp.Common.AzureQueueStorage/LensUp.Common.AzureQueueStorage.csproj", "LensUp.Common.AzureQueueStorage/"]
COPY ["../common/src/LensUp.Common.AzureTableStorage/LensUp.Common.AzureTableStorage.csproj", "LensUp.Common.AzureTableStorage/"]
COPY ["../common/src/LensUp.Common.Types/LensUp.Common.Types.csproj", "LensUp.Common.Types/"]
RUN dotnet restore "LensUp.PhotoCollectorService.API/LensUp.PhotoCollectorService.API.csproj"
COPY . .
WORKDIR "/src/LensUp.PhotoCollectorService.API"
RUN dotent build "LensUp.PhotoCollectorService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LensUp.PhotoCollectorService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LensUp.PhotoCollectorService.API.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "Valhalla.sln" "Valhalla.sln"

COPY "Services/Identity/Valhalla.Identity.API/Valhalla.Identity.API.csproj" "Services/Identity/Valhalla.Identity.API/Valhalla.Identity.API.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

#COPY "NuGet.config" "NuGet.config"

RUN dotnet restore "Valhalla.sln"

COPY . .
WORKDIR /src/Services/Identity/Valhalla.Identity.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Valhalla.Identity.API.dll"]

# Dockerfile Test

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

# Run the app on container startup
# Use your project name for the second parameter
# e.g. MyProject.dll
# ENTRYPOINT [ "dotnet","cms_api.dll" ] 
# escape=$
CMD email="$DOCKER_EMAIL" from="$DOCKER_FROM" password="$DOCKER_PASSWORD" ASPNETCORE_URLS=http://*:$PORT  dotnet cms_api.dll  

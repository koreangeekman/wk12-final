# Use an official Node.js runtime as the base image for the client build
FROM --platform=linux/amd64 node:20-slim AS client-builder

# Set the working directory in the client builder container
WORKDIR /app/client

# Copy the client-side package.json and package-lock.json to the client builder container
COPY client/package*.json ./

# Install client application dependencies
RUN npm install

# Copy the client application source code to the client builder container
COPY client ./

# Build the client-side code
RUN npm run build


# Manually specify an amd64 build environment for the dotnet restore to complete
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine-amd64 AS build-env

# Set the working directory in the client builder container
WORKDIR /app/server

# Copy C# project dependency files
COPY server/*.csproj ./

# Restore as distinct layers
RUN dotnet restore

# Copy everything
COPY server ./

# Copy client files
COPY --from=client-builder /app/client/docs /app/server/wwwroot

# Build and publish a release
RUN dotnet publish -c Release -o out


# Set runtime image for the base container with a generic multi-arch image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

# Set the working directory in the base container
WORKDIR /app

# Copy the compiled server files from the build-env container to the server's root directory
COPY --from=build-env /app/server/out .

# Set the initial command condition for the base container when initialized
CMD ASPNETCORE_URLS=http://*:$PORT dotnet keepr.dll

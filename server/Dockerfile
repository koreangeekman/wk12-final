# Stage 1: Build the client side using Node as the base image
FROM node:20-slim AS client-builder

# Set the working directory in the client builder container
WORKDIR /app/client

# Copy the client-side package.json and package-lock.json to the client builder container
# Copied separately to reinforce cache validation depicting no change from last build [iirc?]
COPY client/package.json ./
COPY client/package-lock.json ./

# Install client application dependencies
RUN npm install

# Copy the client application source code to the client builder container
COPY client ./

# Build the client-side code - This will be stored under "client-builder" until ready to deposit it into the backend dotnet env
RUN npm run build


# Stage 2: Build the server using .NET SDK for amd64
# FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env-amd64
# Stage 3: Build the server for arm64
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine-arm64v8 AS build-env-arm64

WORKDIR /app/server

# Copy the .NET package references file
COPY server/keepr.csproj ./

# Clear NuGet cache directories
RUN dotnet nuget locals all --clear

# "Rebuild" the .NET env
RUN dotnet restore

# Copy the backend contents into the image
COPY server ./

# Copy the frontend contents into the image
COPY --from=client-builder /app/client/docs /app/server/wwwroot

# Specify architecture-specific build commands for amd64 if necessary

# Build and publish a release
RUN dotnet publish -c Release -r linux-arm64 -o out


# Stage 4: Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine-arm64v8

WORKDIR /app
COPY --from=build-env-arm64 /app/server/out .
# COPY --from=build-env-amd64 /app/server/out-amd64 ./out-amd64
# COPY --from=build-env-arm64 /app/server/out-arm64 ./out-arm64

CMD ASPNETCORE_URLS=http://*:$PORT dotnet keepr.dll

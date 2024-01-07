# Stage 1: Build the client side using Node as the base image
FROM node:20-slim AS client-builder

# Set the working directory in the client builder container
WORKDIR /app/client

# Copy the client-side package.json and package-lock.json to the client builder container
COPY client/package*.json ./

# Install client application dependencies
RUN npm install

# Copy the client application source code to the client builder container
COPY client ./

# Build the client-side code - This will be stored under "client-builder" until ready to deposit it into the backend dotnet env
RUN npm run build


# Stage 2: Build the server using .NET SDK for amd64
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env-amd64

WORKDIR /app/server

# Copy the .NET package references file
COPY server/*.csproj ./

# "Rebuild" the .NET env
RUN dotnet restore

# Copy the backend contents into the image
COPY server ./

# Copy the frontend contents into the image
COPY --from=client-builder /app/client/docs /app/server/wwwroot

# Specify architecture-specific build commands for amd64 if necessary

# Build and publish a release
RUN dotnet publish -c Release -o out


# Stage 3: Build the server for arm64
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine-composite-arm64v8 AS build-env-arm64

WORKDIR /app/server

COPY server/*.csproj ./
RUN dotnet restore

COPY server ./
COPY --from=client-builder /app/client/docs /app/server/wwwroot

# Specify architecture-specific build commands for arm64 if necessary

RUN dotnet publish -c Release -o out-arm64


# Stage 4: Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine-composite

WORKDIR /app
COPY --from=build-env-amd64 /app/server/out .
COPY --from=build-env-arm64 /app/server/out-arm64 ./out-arm64

CMD ASPNETCORE_URLS=http://*:$PORT dotnet keepr.dll

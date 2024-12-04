# Base image with .NET 8.0 runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image with .NET 8.0 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DAPM.ResourceRegistryMS.Api/DAPM.ResourceRegistryMS.Api.csproj", "DAPM.ResourceRegistryMS.Api/"]

# Restore dependencies
RUN dotnet restore "DAPM.ResourceRegistryMS.Api/DAPM.ResourceRegistryMS.Api.csproj"

# Copy the rest of the source code and build
COPY . .
WORKDIR "/src/DAPM.ResourceRegistryMS.Api"
RUN dotnet build "DAPM.ResourceRegistryMS.Api.csproj" -c Release -o /app/build

# Publish the build
FROM build AS publish
RUN dotnet publish "DAPM.ResourceRegistryMS.Api.csproj" -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Start the application
ENTRYPOINT ["dotnet", "DAPM.ResourceRegistryMS.Api.dll"]


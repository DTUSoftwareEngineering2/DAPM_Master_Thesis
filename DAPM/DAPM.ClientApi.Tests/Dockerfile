#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DAPM.ClientApi.Tests/DAPM.ClientApi.Tests.csproj", "DAPM.ClientApi.Tests/"]
RUN dotnet restore "./DAPM.ClientApi.Tests/DAPM.ClientApi.Tests.csproj"
COPY . .
WORKDIR "/src/DAPM.ClientApi"
RUN dotnet build "./DAPM.ClientApi.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DAPM.ClientApi.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN dotnet test

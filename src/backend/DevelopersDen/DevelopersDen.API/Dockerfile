#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_Kestrel__Endpoints__Https__Url=https://*:5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DevelopersDen.API/DevelopersDen.API.csproj", "DevelopersDen.API/"]
COPY ["DevelopersDen.Blanket/DevelopersDen.Blanket.csproj", "DevelopersDen.Blanket/"]
COPY ["DevelopersDen.DataAccess/DevelopersDen.DataAccess.csproj", "DevelopersDen.DataAccess/"]
COPY ["DevelopersDen.Library/DevelopersDen.Library.csproj", "DevelopersDen.Library/"]
COPY ["DevelopersDen.Interfaces/DevelopersDen.Interfaces.csproj", "DevelopersDen.Interfaces/"]
COPY ["DevelopersDen.Contracts/DevelopersDen.Contracts.csproj", "DevelopersDen.Contracts/"]
RUN dotnet restore "./DevelopersDen.API/DevelopersDen.API.csproj"
COPY . .
WORKDIR "/src/DevelopersDen.API"
RUN dotnet build "./DevelopersDen.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevelopersDen.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevelopersDen.API.dll"]
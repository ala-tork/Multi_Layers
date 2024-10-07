# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Multi_Layer.API/Multi_Layer.API.csproj", "Multi_Layer.API/"]
COPY ["Multi_Layer.Application/Multi_Layer.Application.csproj", "Multi_Layer.Application/"]
COPY ["Multi_Layer.Domain/Multi_Layer.Domain.csproj", "Multi_Layer.Domain/"]
COPY ["Multi_Layer.Infrastructure/Multi_Layer.Infrastructure.csproj", "Multi_Layer.Infrastructure/"]
RUN dotnet restore "./Multi_Layer.API/Multi_Layer.API.csproj"
COPY . .
WORKDIR "/src/Multi_Layer.API"
RUN dotnet build "./Multi_Layer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Multi_Layer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Multi_Layer.API.dll"]


# docker build -t name .
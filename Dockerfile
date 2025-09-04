# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
# Change the WORKDIR to use the standard /app path
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VONetWebsite.csproj", "."]
RUN dotnet restore "./VONetWebsite.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./VONetWebsite.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VONetWebsite.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
# Change the WORKDIR to use the standard /app path
WORKDIR /app
# Copy the published files to the new, correct directory
COPY --from=publish /app/publish .
# This is the key line to fix the "localhost" issue
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "VONetWebsite.dll"]

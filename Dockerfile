# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy solution and project files first (project is at repo root)
COPY *.sln ./
COPY *.csproj ./

# restore packages
RUN dotnet restore

# copy the rest
COPY . .

# publish to /app/publish
RUN dotnet publish -c Release -o /app/publish

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# copy published output
COPY --from=build /app/publish .

# listen on 8080 (VAP maps 80/443 to this)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "VONetWebsite.dll"]

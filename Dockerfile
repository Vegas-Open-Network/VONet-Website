# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy solution and project files first
COPY *.sln ./
COPY VONet.Web/*.csproj ./VONet.Web/

# restore packages
RUN dotnet restore

# copy the rest
COPY . .

# publish to /app/publish
WORKDIR /src/VONet.Web
RUN dotnet publish -c Release -o /app/publish

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# copy published output
COPY --from=build /app/publish .

# tell ASP.NET where to listen
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "VONet.Web.dll"]

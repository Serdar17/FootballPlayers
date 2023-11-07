#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Web/FootballPlayers.Web/FootballPlayers.Web.csproj", "src/Web/FootballPlayers.Web/"]
COPY ["src/Domain/FootballPlayers.Domain/FootballPlayers.Domain.csproj", "src/Domain/FootballPlayers.Domain/"]
RUN dotnet restore "src/Web/FootballPlayers.Web/FootballPlayers.Web.csproj"
COPY . .
WORKDIR "/src/src/Web/FootballPlayers.Web"
RUN dotnet build "FootballPlayers.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FootballPlayers.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY src/Web/FootballPlayers.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Web/FootballPlayers.Api/FootballPlayers.Api.csproj", "src/Web/FootballPlayers.Api/"]
COPY ["src/Shared/FootballPlayers.Settings/FootballPlayers.Settings.csproj", "src/Shared/FootballPlayers.Settings/"]
COPY ["src/Domain/FootballPlayers.Infrastructure.Abstractions/FootballPlayers.Infrastructure.Abstractions.csproj", "src/Domain/FootballPlayers.Infrastructure.Abstractions/"]
COPY ["src/Domain/FootballPlayers.Domain/FootballPlayers.Domain.csproj", "src/Domain/FootballPlayers.Domain/"]
COPY ["src/Shared/FootballPlayers.Common/FootballPlayers.Common.csproj", "src/Shared/FootballPlayers.Common/"]
COPY ["src/Services/FootballPlayers.Players/FootballPlayers.Players.csproj", "src/Services/FootballPlayers.Players/"]
COPY ["src/Infrastructure/FootballPlayers.Infrastructure.Context/FootballPlayers.Infrastructure.Context.csproj", "src/Infrastructure/FootballPlayers.Infrastructure.Context/"]
RUN dotnet restore "src/Web/FootballPlayers.Api/FootballPlayers.Api.csproj"
COPY . .
WORKDIR "/src/src/Web/FootballPlayers.Api"
RUN dotnet build "FootballPlayers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FootballPlayers.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FootballPlayers.Api.dll"]

# Решение тестовое задания Junior .NET Core

[Ссылка на тестовое](https://practice.66bit.ru/task/csharp.pdf)

## Как запустить проект
Запусть проект можно как локально так и в докере. Для запуска локально необходимо в `appsettings.json` вставить в `ConnectionString` свою строку подключения к СУДБ.
В качестве СУБД используется [PostgreSQL](https://www.postgresql.org/) и запустить само приложение через CLI

### DOTNET CLI
Для запуска Api
``` bash
dotnet run --project ./src/Web/FootballPlayers.Api
```

Для запуска Blazor App
``` bash
dotnet run --project ./src/Web/FootballPlayers.Web
```

### DOCKER COMPOSE
Для запуска через `docker-compose.yml` не надо менять `appsettings.json` 

Для билда

``` bash
docker-compose build
```

Для запуска

``` bash
docker-compose up -d
```

Для закрытия контейнеров

``` bash
docker-compose down
```
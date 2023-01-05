# Traiteur à tout'heure

## Contributors

- GOUPIL Alban
- GEORGES Jérôme
- DIOT Jérémy

## Requirements

- VsCode
- Docker and docker-compose
- [C# / .NET 7](https://learn.microsoft.com/fr-fr/dotnet/core/install/)
- Ports 5000, 5001 and 5002 are free

## Project Organization

- Three REST API ASP.NET projects
    - OrderApi/
    - RecipeApi/  
    - StockApi/  

- Documentation **docs/**
    - [Databases.pdf](docs/Databases.pdf) databases schema
    - [Routes.pdf](docs/Routes.pdf) api routes specification
    - [postman_collection.json](docs/postman_collection.json) postman configuration for api routes testing

## API URLs

API stock : <http://localhost:5000>  
API recipe : <http://localhost:5001>  
API order : <http://localhost:5002>  

## Start project

### All APIs

```bash
# from project root folder
docker compose up  # start APIs in docker containers
```

### One API

```bash
# from api folders OrderApi/ RecipeApi/ StockApi/
dotnet restore # install dependencies
dotnet run  # start server
```

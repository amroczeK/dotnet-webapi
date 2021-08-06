# Item Store .NET 5 REST API Service
.NET 5 REST API service to read/write items to MongoDB, containerized with Docker.

## Getting Started

You can follow these instructions to build the project.

Once built you can access the API docs via http://localhost:8080/swagger/index.html

### Initialize Secrets Manager if not running application using Docker
```
dotnet user-secrets init
dotnet user-secrets set MongoDbConfig:Password password
```

### Docker Steps

### Build image
```
docker build -t dotnet-webapi:v1 .
```

### Create network for service and db to communicate over
```
docker network create dotnet-webapi
```

### Run MongoDb container
```
docker run -d --rm --name item-store-mongo -p 27017:27017 -v itemstoredbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=admin -e MONGO_INITDB_ROOT_PASSWORD=password --network=dotnet-webapi mongo
```

### Run application container
Note: This will overwrite the Host/Password configuration in appsettings.json
```
docker run -it --rm -p 8080:80 -e MongoDbConfig:Host=item-store-mongo -e MongoDbConfig:Password=password --network=dotnet-webapi dotnet-webapi:v1
```

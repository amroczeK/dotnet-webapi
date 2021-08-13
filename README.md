# Item Store .NET 5 REST API Service
.NET 5 REST API service to read/write items to MongoDB, deployed on Docker and/or Kubernetes cluster.

## Getting Started

You can follow these instructions to build the project.

Once built you can access the API docs via http://localhost:8080/swagger/index.html

### Initialize Secrets Manager if not running application using Docker
```
dotnet user-secrets init
dotnet user-secrets set MongoDbConfig:Password password
```

## Docker deployment steps

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

## Kubernetes deployment steps
Note: Docker image build required in previous step, kubernetes deployment yaml uses that image.

### Enable Kubernetes in Docker Desktop
```
Docker Desktop -> Kubernetes -> Enable Kubernetes -> Apply & Restart

Wait for Docker to pull down all the requires images and configure Kubernetes. Once this is done, you will be able to use kubectl commands.

Validate cluster is running: kubectl cluster-info
```

### Configure secrets in Kubernetes secrets manager
```
kubectl create secret generic dotnet-webapi-secrets --from-literal=mongodb-password='password'
```

### Apply Kubernetes deployment
```
From the root directory

kubectl apply -f Kubernetes/mongodb.yaml
kubectl apply -f Kubernetes/dotnet-webapi.yaml
```

### Verify deployments
```
kubectl get pods
NAME                                        READY   STATUS    RESTARTS   AGE
dotnet-webapi-deployment-5d95fbc8d9-gwrgs   1/1     Running   0          5m22s
mongodb-statefulset-0                       1/1     Running   0          7m12s

kubectl get deployments
NAME                       READY   UP-TO-DATE   AVAILABLE   AGE
dotnet-webapi-deployment   1/1     1            1           8m31s

kubectl get statefulsets
NAME                  READY   AGE
mongodb-statefulset   1/1     9m36s

kubectl get services
NAME                    TYPE           CLUSTER-IP       EXTERNAL-IP   PORT(S)        AGE
dotnet-webapi-service   LoadBalancer   10.101.207.197   localhost     80:32559/TCP   8m11s
kubernetes              ClusterIP      10.96.0.1        <none>        443/TCP        22m
mongodb-service         ClusterIP      None             <none>        27017/TCP      10m
```

### Scaling up the deployments
```
kubectl get deployments
NAME                       READY   UP-TO-DATE   AVAILABLE   AGE
dotnet-webapi-deployment   1/1     1            1           24m

kubectl scale deployments/dotnet-webapi-deployment --replicas=3
```
# cegeka_api
demo api

## Run the application
1. clone this repo to your machine
2. navigate to this repo
3. run command: `docker-compose up`


## Run intial database migration
1. fill the Customer.cs
2. run command: `dotnet ef migrations add InitialCreate`
3. run command: `dotnet ef database update`
4. run command: `dotnet ef migrations remove` --> optional

**Note:** Make sure docker is running on your machine -> atleast that the database is running

## Tools required to deploy to ACR
1. Azure shell
2. docker

## information table:
resource-group = demoCegekaApp
acr name = democegekaappacr
acrLoginServer = democegekaappacr.azurecr.io 

# Create container registry
## commands used for setting up ACR environment
1. create a az group: `az group create --name demoCegekaApp --location westeurope --tags owner=darren.siriram@cegeka.com`
2. create an acr instance `az acr create --resource-group demoCegekaApp --name democegekaappacr --sku Basic`
3. login to the ACR: `az acr login --name democegekaappacr` **Note** use the name of the acr 
4. get login server address: `az acr list --resource-group demoCegekaApp --query "[].{acrLoginServer:loginServer}" --output table`
5. copy the addres recived from the CLI: `democegekaappacr.azurecr.io`
6. tag your docker image using this command: `docker tag <name of the image>:<tag> democegekaappacr.azurecr.io/<name of the image><tag ex: v1>`
    6a. example `docker tag api_cegeka:latest democegekaappacr.azurecr.io/cegeka_api-web:v1`
7. verify if your tag has been update by typing `docker images`
8. Push the docker image to ACR: `docker push democegekaappacr.azurecr.io/api_cegeka:<tag>`
9. list images in the registry: `az acr repository list --name democegekaappacr --output table`


# Deploy kubernetes cluster
1. create an kubernetes cluster: `az aks create --resource-group demoCegekaApp --tags owner=darren.siriram@cegeka.com --name cegekaCluster --node-count 2 --generate-ssh-keys --attach-acr democekeaapacr`
2. install kubernetes CLI: `az aks install-cli` 
3. connect to the cluster: `az aks get-credentials -- resource-group demoCegekaApp --name cegekaCluster`
4. check if there is a connection: `kubectl get nodes`

# Running the application
1. created a yml file to provision the containers in kubernetes: `kubectl apply -f cegeka_api.yml`
2. after the containers are made check for the connection to the public: `kubectl get service <name of the service just created> --watch`

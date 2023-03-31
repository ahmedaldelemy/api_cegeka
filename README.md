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
- [X] Azure Shell
- [X] Docker

## Details table:
**RGname** = `demoCegekaApp` <br>
**ACR name** = `democegekaappacr` <br>
**acrLoginServer** = `democegekaappacr.azurecr.io` <br>
**DockerSource** = `democegekaappacr.azurecr.io/cegeka_api-web:<tag>`
<br>

# Create container registry
## commands used for setting up ACR environment
1. create a az group: `az group create --name demoCegekaApp --location westeurope --tags owner=darren.siriram@cegeka.com`
2. create an acr instance `az acr create --resource-group demoCegekaApp --name democegekaappacr --sku Basic`
3. login to the ACR: `az acr login --name democegekaappacr` **Note** use the name of the acr 
4. get login server address: `az acr list --resource-group demoCegekaApp --query "[].{acrLoginServer:loginServer}" --output table`
5. copy the addres recived from the CLI: `democegekaappacr.azurecr.io`
6. tag your docker image using this command: `docker tag <name of the image>:<tag> democegekaappacr.azurecr.io/<name of the image><tag ex: v1>`
    6a. example `docker tag cegeka_api-web:latest democegekaappacr.azurecr.io/cegeka_api-web:v1`
7. verify if your tag has been update by typing `docker images`
8. Push the docker image to ACR: `docker push democegekaappacr.azurecr.io/cegeka_api-web:<tag>`
9. list images in the registry: `az acr repository list --name democegekaappacr --output table`


# Deploy kubernetes cluster
1. create an kubernetes cluster: `az aks create --resource-group demoCegekaApp --tags owner=darren.siriram@cegeka.com --name cegekaCluster --node-count 2 --generate-ssh-keys --attach-acr democekeaapacr`
2. install kubernetes CLI: `az aks install-cli` 
3. connect to the cluster: `az aks get-credentials -- resource-group demoCegekaApp --name cegekaCluster`
4. check if there is a connection: `kubectl get nodes`

# Running the application
1. created a yml file to provision the containers in kubernetes: `kubectl apply -f cegeka_api.yml`
2. after the containers are made check for the connection to the public: `kubectl get service <name of the service just created> --watch`

--------------------------------------

## Steps created , to provision it with a managed account 


### Create Resource group 
```
az group create -n <RGName> -l "westeurope" --tags 
"Owner=@cegeka.com"
```

### Create ACR
```
az acr create -n <ACRName> -g <RGName> --sku basic - for networking (so configuring network rules and firewall) sku premium is needed
```

### Create AKS
```
 az aks create -n <AKSName> -g <RGName> --generate-ssh-keys --attach-acr <ACRName> --enable-managed-identity --location "westeurope" --tags "owner=@cegeka.com"
 ```

### Attach using acr-name 
```
az aks update -n <AKSName> -g <RGName> --attach-acr <ACRName>
```

### Update aks so it create/uses its managed identity
```
az aks update -g <RGName> -n <AKSName> --enable-managed-identity
```
### Update/upgrade node-pool and ACR so it uses the managed identity
```
az aks nodepool upgrade --node-image-only --resource-group <RGName> --cluster-name <AKSName> --nodepool-name nodepool1 az aks update --attach-acr <ACRName> 
 ```
**Note** :
- after the above command assign Azure Kubernetes Service RBAC Writer to service account aks. This is possible in the IAM tab of the AKS service



### Push docker image to ACR 
```
docker push <Dockersource>
```

### login to aks pods 
```
az aks get-credentials -g <RGName> -n <AKSName>
```

### Check if AKS can pull image from ACR
```
az aks check-acr -g <RGName> -n <AKSName> --acr <ACRName>.azurecr.io
```

### Deploy image from ACR to AKS
```
az aks get-credentials -g <RGName> -n <AKSName>
```
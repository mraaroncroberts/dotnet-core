# --------------------------------------------------------------
#   Api Project in a Docker Container deployed to Kubernetes
# --------------------------------------------------------------

#
#   Local Build using dotnet core
#  --------------------------------------------------------------
# Build and Compile project (from inside src/)
cd src
dotnet build

#
# Run Project locally (http://localhost:5231/ [based on launchSettings.json])
cd src
dotnet run

#
#   Docker build 
# --------------------------------------------------------------
# build the docker image
docker build --pull -t dotnet-core-api .

# Run the docker image and host (http://localhost:8001/)
docker run --rm -it -p 8001:80 dotnet-core-api --name dotnet-core-api

# 
# Kubernetes build using minikube and yaml
# --------------------------------------------------------------

# start the minikube service
minikube start

# 
# launches the minikube dashboard UI (not necessary, but fun)
minikube dashboard

#
# Configure terminal to use the docker daemon inside minikube 
# - Now any ‘docker’ command you run in the terminal will run 
#   against the docker inside minikube cluster.
#
eval $(minikube docker-env)

# rebuild to minikube
docker build --pull -t dotnet-core-api .

# create the deployment from the yaml file
kubectl create -f k8s/deployment.yaml

# displays control plane and exposed services
kubectl cluster-info
kubectl get services

#
# creates the tunnel to the container from the host, and 
# displays the url conneciton for the service
minikube service dotnet-core-api-service

#
# In another window, start the tunnel to create a routable IP for the ‘balanced’ deployment:
# this is used for LoadBalance nodes 
#   minikube tunnel 

#
# tear it down
kubectl delete -f k8s/deployment.yaml 


# --------------------------------------------------------------
#                   Miscellaneous commands
# --------------------------------------------------------------

# Creates creditials to login to local registry
#   docker login

# > minikube docker-env
# export DOCKER_TLS_VERIFY=”1"
# export DOCKER_HOST=”tcp://172.17.0.2:2376"
# export DOCKER_CERT_PATH=”/home/user/.minikube/certs”
# export MINIKUBE_ACTIVE_DOCKERD=”minikube”
# To point your shell to minikube’s docker-daemon, run:
#   eval $(minikube -p minikube docker-env)

# To find the routable IP, run this command and examine the EXTERNAL-IP column:
#   kubectl get services balanced

# references
# 
# https://minikube.sigs.k8s.io/docs/handbook/accessing/
# https://minikube.sigs.k8s.io/docs/start/
#
# Service to service communication
# https://dev.to/narasimha1997/communication-between-microservices-in-a-kubernetes-cluster-1n41
#
# - access services through namespaces
#



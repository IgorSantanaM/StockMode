
echo "Applying Kubernetes resources..."

# 1. Namespace
echo "1. Creating namespace..."
kubectl apply -f namespace.yaml

# 2. Secrets
echo "2. Creating secrets..."
kubectl apply -f secrets.yaml

# 3. ConfigMap
echo "3. Creating configmap..."
kubectl apply -f configmap.yaml

# 4. PostgreSQL
echo "4. Deploying PostgreSQL..."
kubectl apply -f postgres.yaml

# 5. RabbitMQ
echo "5. Deploying RabbitMQ..."
kubectl apply -f rabbitmq.yaml

# 6. IDP Persistent Volume
echo "6. Creating IDP persistent volume..."
kubectl apply -f idp-persistent-volume.yaml

# 7. Identity Provider
echo "7. Deploying Identity Provider..."
kubectl apply -f stockmodeidp.yaml

# 7. Identity Provider
echo "7. Deploying Identity Provider..."
kubectl apply -f stockmodeidp.yaml

# 8. Backend API
echo "8. Deploying Backend API..."
kubectl apply -f stockmodeapi.yaml

# 9. Email Worker
echo "9. Deploying Email Worker..."
kubectl apply -f stockmodeemailworker.yaml

# 10. Frontend
echo "10. Deploying Frontend..."
kubectl apply -f stockmodefrontend.yaml

# 11. Autoscaling
echo "11. Configuring autoscaling..."
kubectl apply -f api-autoscale.yaml
kubectl apply -f frontend-autoscale.yaml

# 12. Ingress
echo "12. Configuring ingress..."
kubectl apply -f stockmode-ingress.yaml

echo ""
echo "Deployment completed!"
echo ""
echo "Check status with: kubectl get all -n stockmode"

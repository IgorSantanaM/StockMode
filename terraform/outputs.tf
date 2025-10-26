output "resource_group_name" {
  description = "The name of the resource group"
  value       = module.resource_group.resource_group_name
}

output "aks_cluster_name" {
  description = "The name of the AKS cluster"
  value       = module.aks.aks_name
}

output "aks_cluster_fqdn" {
  description = "The FQDN of the AKS cluster"
  value       = module.aks.aks_fqdn
}

output "acr_login_server" {
  description = "The login server URL for ACR"
  value       = module.acr.acr_login_server
}

output "acr_name" {
  description = "The name of the Azure Container Registry"
  value       = module.acr.acr_name
}

output "kube_config" {
  description = "Kubernetes configuration file"
  value       = module.aks.kube_config
  sensitive   = true
}

output "log_analytics_workspace_id" {
  description = "The ID of the Log Analytics workspace"
  value       = module.log_analytics.log_analytics_workspace_id
}

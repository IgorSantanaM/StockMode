output "aks_id" {
    description = "The AKS cluster ID"
    value       = azurerm_kubernetes_cluster.stockmode_aks.id
}

output "aks_name" {
    description = "The AKS cluster name"
    value       = azurerm_kubernetes_cluster.stockmode_aks.name
}

output "aks_fqdn" {
    description = "The AKS cluster FQDN"
    value       = azurerm_kubernetes_cluster.stockmode_aks.fqdn
}

output "aks_node_resource_group" {
    description = "The AKS node resource group"
    value       = azurerm_kubernetes_cluster.stockmode_aks.node_resource_group
}

output "kube_config" {
    description = "The AKS cluster kubeconfig"
    value       = azurerm_kubernetes_cluster.stockmode_aks.kube_config_raw
    sensitive   = true
}

output "client_certificate" {
    description = "The AKS client certificate"
    value       = azurerm_kubernetes_cluster.stockmode_aks.kube_config[0].client_certificate
    sensitive   = true
}

output "client_key" {
    description = "The AKS client key"
    value       = azurerm_kubernetes_cluster.stockmode_aks.kube_config[0].client_key
    sensitive   = true
}

output "cluster_ca_certificate" {
    description = "The AKS cluster CA certificate"
    value       = azurerm_kubernetes_cluster.stockmode_aks.kube_config[0].cluster_ca_certificate
    sensitive   = true
}

output "host" {
    description = "The AKS cluster host"
    value       = azurerm_kubernetes_cluster.stockmode_aks.kube_config[0].host
    sensitive   = true
}

output "kubelet_identity_object_id" {
    description = "The kubelet identity object ID"
    value       = azurerm_kubernetes_cluster.stockmode_aks.kubelet_identity[0].object_id
}


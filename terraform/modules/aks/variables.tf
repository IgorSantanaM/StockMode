variable "aks_name" {
    type        = string
    description = "The name of the AKS cluster"
    default     = "stockmode-aks"
}

variable "aks_node_count" {
    type        = number
    description = "The number of nodes in the AKS cluster"
    default     = 1
}

variable "aks_vm_size" {
    type        = string
    description = "The size of the VM for the AKS nodes"
    default     = "Standard_B1s"
}

variable "location" {
    type        = string
    description = "The Azure region location"
}

variable "resource_group_name" {
    type        = string
    description = "The name of the resource group"
}

variable "subnet_id" {
    type        = string
    description = "The ID of the subnet for AKS"
}

variable "log_analytics_workspace_id" {
    type        = string
    description = "The ID of the Log Analytics workspace"
}

variable "acr_id" {
    type        = string
    description = "The ID of the Azure Container Registry"
}

variable "environment" {
    type        = string
    description = "The environment (dev, staging, prod)"
    default     = "dev"
}
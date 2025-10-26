resource "azurerm_kubernetes_cluster" "stockmode_aks" {
    name                = var.aks_name
    location            = var.location
    resource_group_name = var.resource_group_name
    dns_prefix          = "${var.aks_name}-dns"
    
    default_node_pool {
        name            = "default"
        node_count      = var.aks_node_count
        vm_size         = var.aks_vm_size
        vnet_subnet_id  = var.subnet_id
        os_disk_size_gb = 30
    }   
    
    identity {
        type = "SystemAssigned"
    }
    
    network_profile {
        network_plugin    = "azure"
        network_policy    = "azure"
        dns_service_ip    = "10.0.1.10"
        service_cidr      = "10.0.1.0/24"
        load_balancer_sku = "standard"
    }

    oms_agent {
        log_analytics_workspace_id = var.log_analytics_workspace_id
    }

    tags = {
        Environment = var.environment
        Project     = "StockMode"
    }
}

resource "azurerm_role_assignment" "aks_acr_pull" {
    scope                = var.acr_id
    role_definition_name = "AcrPull"
    principal_id         = azurerm_kubernetes_cluster.stockmode_aks.kubelet_identity[0].object_id
}
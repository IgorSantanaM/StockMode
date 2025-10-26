terraform {
  required_providers {
    azurerm = {
        source = "hashicorp/azurerm"
        version = "4.17.0"
    }
  }
}
provider "azurerm"{
    features {
    }
    subscription_id = var.subscription_id
}

module "resource_group" {
  source = "./modules/resource-group"
}

module "network" {
  source = "./modules/network"
  
  resource_group_name = module.resource_group.resource_group_name
  location            = module.resource_group.resource_group_location
}

module "log_analytics" {
  source = "./modules/log-analytics"
  
  resource_group_name = module.resource_group.resource_group_name
  location            = module.resource_group.resource_group_location
}

module "acr" {
  source = "./modules/acr"

  acr_name            = "stockmodeacr"
  resource_group_name = module.resource_group.resource_group_name
  location            = module.resource_group.resource_group_location
}

module "aks" {
  source = "./modules/aks"

  aks_name                   = "${module.resource_group.resource_group_name}-aks"
  resource_group_name        = module.resource_group.resource_group_name
  location                   = module.resource_group.resource_group_location
  subnet_id                  = module.network.subnet_id
  acr_id                     = module.acr.acr_id
  log_analytics_workspace_id = module.log_analytics.log_analytics_workspace_id
}

module "monitoring" {
  source = "./modules/monitoring"

  target_resource_id         = module.aks.aks_id
  log_analytics_workspace_id = module.log_analytics.log_analytics_workspace_id
  resource_group_name        = module.resource_group.resource_group_name
}

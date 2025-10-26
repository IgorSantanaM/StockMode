resource "azurerm_container_registry" "acr" {
  location            = var.location
  name                = var.acr_name
  resource_group_name = var.resource_group_name
  sku                 = "Standard"
}
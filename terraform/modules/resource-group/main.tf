resource "azurerm_resource_group" "stockmode_rg" {
  name     = var.resource_group_name
  location = var.location
}
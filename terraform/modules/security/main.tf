resource "azurerm_user_assigned_identity" "stockmode_identity" {
  resource_group_name = azurerm_resource_group.stockmode_rg.name
  location            = azurerm_resource_group.stockmode_rg.location
  name                = var.stockmode_identity_name
}

resource "azurerm_role_assignment" "stockmode_identity_role" {
  scope                = azurerm_resource_group.stockmode_rg.id
  role_definition_name = var.role_definition_name
  principal_id         = azurerm_user_assigned_identity.stockmode_identity.principal_id
}
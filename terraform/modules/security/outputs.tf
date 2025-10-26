output "acr_login_server" {
  description = "The login server of the Azure Container Registry"
  value       = azurerm_container_registry.acr.login_server
}
output "acr_admin_enabled" {
  description = "Indicates whether admin user is enabled for the Azure Container Registry"
  value       = azurerm_container_registry.acr.admin_enabled
}
output "acr_admin_username" {
  description = "The admin username of the Azure Container Registry"
  value       = azurerm_container_registry.acr.admin_username
}
output "acr_admin_password" {
  description = "The admin password of the Azure Container Registry"
  value       = azurerm_container_registry.acr.admin_password
}
output "acr_id" {
  description = "The ID of the Azure Container Registry"
  value       = azurerm_container_registry.acr.id
}
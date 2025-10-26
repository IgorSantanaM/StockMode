output "log_analytics_workspace_id" {
    description = "The log analytics workspace ID"
    value       = azurerm_log_analytics_workspace.stockmode_log_analytics_workspace.id
}
output "action_group_id" {
  description = "The ID of the action group"
  value       = azurerm_monitor_action_group.stockmode_action_group.id
}

output "diagnostic_setting_id" {
  description = "The ID of the diagnostic setting"
  value       = azurerm_monitor_diagnostic_setting.stockmode_diagnostics.id
}

output "metric_alert_id" {
  description = "The ID of the metric alert"
  value       = azurerm_monitor_metric_alert.stockmode_metric_alert.id
}

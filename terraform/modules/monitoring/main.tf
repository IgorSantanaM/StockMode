resource "azurerm_monitor_action_group" "stockmode_action_group" {
  name                = "stockmode-action-group"
  resource_group_name = var.resource_group_name
  short_name          = "stockmode"

  email_receiver {
    name          = "sendtoadmin"
    email_address = var.alert_email_address
  }
}

resource "azurerm_monitor_diagnostic_setting" "stockmode_diagnostics" {
  name                       = var.diagnostics_name
  target_resource_id         = var.target_resource_id
  log_analytics_workspace_id = var.log_analytics_workspace_id

  enabled_log {
    category = "kube-apiserver"
  }

  enabled_log {
    category = "kube-controller-manager"
  }

  enabled_log {
    category = "kube-scheduler"
  }

  enabled_log {
    category = "cluster-autoscaler"
  }

  enabled_log {
    category = "guard"
  }

  metric {
    category = "AllMetrics"
  }
}

resource "azurerm_monitor_metric_alert" "stockmode_metric_alert" {
  name                = var.metric_alert_name
  resource_group_name = var.resource_group_name
  scopes              = [var.target_resource_id]
  description         = "Metric alert for StockMode"
  severity            = 3
  frequency           = "PT5M"
  window_size         = "PT5M"

  criteria {
    metric_namespace = "Microsoft.ContainerService/managedClusters"
    metric_name      = "NodeCpuUtilization"
    aggregation      = "Average"
    operator         = "GreaterThan"
    threshold        = 80
  }

  action {
    action_group_id = azurerm_monitor_action_group.stockmode_action_group.id
  }
  depends_on = [azurerm_monitor_diagnostic_setting.stockmode_diagnostics]
}

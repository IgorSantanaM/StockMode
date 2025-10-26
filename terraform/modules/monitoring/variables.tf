variable "diagnostics_name" {
    type        = string
    description = "The name of the diagnostics setting"
    default     = "stockmode-diagnostics"
}

variable "target_resource_id" {
    type        = string
    description = "The ID of the target resource"
}

variable "log_analytics_workspace_id" {
    type        = string
    description = "The ID of the Log Analytics workspace"
}

variable "metric_alert_name" {
  type        = string
  description = "The metric alert name"
  default     = "stockmode-metric-alert"
}

variable "resource_group_name" {
    type        = string
    description = "The name of the resource group"
}

variable "alert_email_address" {
    type        = string
    description = "Email address for alert notifications"
    default     = "igorsantanamedeiros@outlook.com"
}
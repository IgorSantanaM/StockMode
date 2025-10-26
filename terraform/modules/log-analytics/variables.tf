variable "log_analytics_workspace_name" {
  type = string
  description = "The log analytics workspace name"
  default = "stockmode-log-analytics-workspace"
}

variable "resource_group_name" {
  type        = string
  description = "The name of the resource group"
}

variable "location" {
  type        = string
  description = "The Azure region location"
}
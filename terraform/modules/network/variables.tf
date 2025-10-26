variable "network_name" {
  type        = string
  description = "The name of the virtual network"
  default     = "stockmode-vnet"
}

variable "subnet_name" {
  type        = string
  description = "The name of the subnet"
  default     = "stockmode-subnet"
}

variable "resource_group_name" {
  type        = string
  description = "The name of the resource group"
}

variable "location" {
  type        = string
  description = "The Azure region location"
}
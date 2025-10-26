variable "resource_group_name" {
  type        = string
  description = "The name of the resource group"
  default     = "stockmode-rg"
}

variable "location" {
  type =  string
  description = "The location of the resource group"
  default = "westus2"
}
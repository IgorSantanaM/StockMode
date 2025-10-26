variable "env_id" {
  type        = string
  default     = "dev"
  description = "The environment ID"
}

variable "subscription_id" {
  type = string
  description = "The azure subscription ID"
 }

 variable "src_key" {
  type        = string
  default     = "terraform"
  description = "The infrasctructure source"
}

variable "tenant_id" {
  description = "The tenant ID for the Azure Active Directory"
  type        = string
}

variable "object_id" {
  description = "The object ID"
  type        = string
  default = "fbeb51e1-3c90-4b7d-9a2a-a48738cd6d21"
}
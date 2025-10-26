variable "stockmode_identity_name" {
  type        = string
  description = "The name of the user assigned identity"
  default     = "stockmode-identity"
}

variable "role_definition_name" {
  type        = string
  description = "The role definition name"
  default     = "Contributor"
}
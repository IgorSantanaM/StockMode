output "subnet_name" {
  description = "The name of the subnet"
  value       = var.subnet_name
}

output "network_name" {
  description = "The name of the virtual network"
  value       = var.network_name
}

output "virtual_network_id" {
  description = "The ID of the virtual network"
  value       = azurerm_virtual_network.stockmode_vnet.id
}

output "subnet_id" {
  description = "The ID of the subnet"
  value       = azurerm_subnet.stockmode_subnet.id
}

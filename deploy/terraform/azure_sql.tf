locals {
  sql_server_name = "example-sql-server-${random_integer.example.result}"
}

# Create the SQL server
resource "azurerm_mssql_server" "example" {
  name                         = local.sql_server_name
  resource_group_name          = azurerm_resource_group.example.name
  location                     = azurerm_resource_group.example.location
  version                      = "12.0"
  administrator_login          = var.db_username
  administrator_login_password = var.db_password
}

# Create the SQL firewall rule
resource "azurerm_mssql_firewall_rule" "example" {
  name             = "AllowAllWindowsAzureIPs"
  server_id        = azurerm_mssql_server.example.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

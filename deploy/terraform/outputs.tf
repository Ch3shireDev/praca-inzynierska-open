
output "resource_group_name" {
  value = azurerm_resource_group.example.name
}

output "web_app_name" {
  value = azurerm_linux_web_app.example.name
}

output "sql_server_name" {
    value = azurerm_mssql_server.example.name
}

output "sql_server_url" {
    value = azurerm_mssql_server.example.fully_qualified_domain_name
}
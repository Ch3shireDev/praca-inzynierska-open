
locals {
  sql_server = azurerm_mssql_server.example.fully_qualified_domain_name
}

locals {
  connection_string = "Server=tcp:${local.sql_server},1433;Initial Catalog=WORLDBANK;Persist Security Info=False;User ID=${var.db_username};Password=${var.db_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

# Create the Linux App Service Plan
resource "azurerm_service_plan" "example" {
  name                = "webapp-plan-${random_integer.example.result}"
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  os_type             = "Linux"
  sku_name            = "B1"
}

# Create the web app, pass in the App Service Plan ID
resource "azurerm_linux_web_app" "example" {
  name                = local.app_name
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_service_plan.example.location
  service_plan_id     = azurerm_service_plan.example.id
  https_only          = true

  site_config {
    minimum_tls_version = "1.2"
    application_stack {
      dotnet_version = "6.0"
    }
  }

  app_settings = {
    AzureAD__TenantId                    = var.app_tenant_id
    AzureAD__ClientId                    = azuread_application.example.application_id
    AzureAD__ClientSecret                = azuread_service_principal_password.example.value
    PowerBI__ApplicationId               = var.power_bi_application_id
    PowerBI__ApplicationSecret           = var.power_bi_application_secret
    PowerBI__ReportId                    = var.power_bi_report_id
    PowerBI__WorkspaceId                 = var.power_bi_workspace_id
    PowerBI__AuthorityUrl                = var.power_bi_authority_url
    PowerBI__ResourceUrl                 = var.power_bi_resource_url
    PowerBI__ApiUrl                      = var.power_bi_api_url
    PowerBI__EmbedUrlBase                = var.power_bi_embed_url_base
    PowerBI__UserName                    = var.power_bi_username
    PowerBI__Password                    = var.power_bi_password
  }

  connection_string {
    name = "DefaultConnection"
    type = "SQLAzure"
    value = local.connection_string
  }
}

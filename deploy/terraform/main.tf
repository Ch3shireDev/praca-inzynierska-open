terraform {
  required_providers {
    azuread = {
      source  = "hashicorp/azuread"
      version = "=2.36.0"
    }
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.49.0"
    }
  }
}


provider "azurerm" {
  features {

  }
}

provider "azuread" {
  tenant_id = var.app_tenant_id
}


locals {
  app_name = "worldfacts-${random_integer.example.result}"
  app_url  = "https://worldfacts-${random_integer.example.result}.azurewebsites.net"
}


# Create the resource group
resource "azurerm_resource_group" "example" {
  name     = "worldfacts-${random_integer.example.result}"
  location = var.location
}


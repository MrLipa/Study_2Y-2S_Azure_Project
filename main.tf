terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~>2.46.0"
    }
  }
}

provider "azurerm" {
  features {}
}


# Resource Group
resource "azurerm_resource_group" "project" {
  name     = "project-resources"
  location = "Poland Central"
  tags = {
    environment = "Production"
  }
}

# App Service Plan
resource "azurerm_app_service_plan" "project" {
  name                = "project-app-service-plan"
  location            = azurerm_resource_group.project.location
  resource_group_name = azurerm_resource_group.project.name
  kind                = "Windows"

  sku {
    tier = "Standard"
    size = "S1"
  }
}

# Web App
resource "azurerm_app_service" "project_web_app" {
  name                = "project-web-app-2137"
  location            = azurerm_resource_group.project.location
  resource_group_name = azurerm_resource_group.project.name
  app_service_plan_id = azurerm_app_service_plan.project.id
  https_only          = true

  site_config {
    windows_fx_version = "DOTNET|7.0"
  }
}

# SQL Server
resource "azurerm_sql_server" "project_sql_database_server" {
  name                         = "project-sql-server-2137"
  resource_group_name          = azurerm_resource_group.project.name
  location                     = "Poland Central"
  version                      = "12.0"
  administrator_login          = "admin2137"
  administrator_login_password = "Admin123"

  tags = {
    environment = "Production"
  }
}

# SQL Database
resource "azurerm_sql_database" "project_sql_database" {
  name                = "project-sql-database"
  resource_group_name = azurerm_resource_group.project.name
  location            = azurerm_resource_group.project.location
  server_name         = azurerm_sql_server.project_sql_database_server.name

  requested_service_objective_name = "S0"

  tags = {
    environment = "Production"
  }
}
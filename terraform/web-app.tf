resource "random_string" "web_app_name" {
  length  = 8
  special = false
  upper   = false
}

# App Service Plan
resource "azurerm_service_plan" "web_app" {
  name                = "asp-web-app-${var.application_name}-${var.environment_name}"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  os_type             = "Windows"
  sku_name            = "S1"
}

# Web App
resource "azurerm_windows_web_app" "main" {
  name                = "wa-${var.application_name}-${var.environment_name}-${random_string.web_app_name.result}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  service_plan_id     = azurerm_service_plan.web_app.id
  https_only          = true

  site_config {
    application_stack {
      current_stack  = "dotnet"
      dotnet_version = "v7.0"
    }
  }
}
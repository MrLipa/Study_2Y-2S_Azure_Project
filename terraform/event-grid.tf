resource "azurerm_eventgrid_topic" "main" {
  name                = "egt-${var.application_name}-${var.environment_name}"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
}

resource "azurerm_eventgrid_event_subscription" "main" {
  name  = "eges-${var.application_name}-${var.environment_name}"
  scope = azurerm_eventgrid_topic.main.id

  azure_function_endpoint {
    function_app_id = azurerm_linux_function_app.send_mail.id
  }
}
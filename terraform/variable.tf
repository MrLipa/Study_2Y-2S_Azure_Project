# variable "application_name" {
#   type = string
# }
# variable "environment_name" {
#   type = string
# }
# variable "location" {
#   type = string
# }
# variable "sql_server_login" {
#   type = string
# }
# variable "sql_server_password" {
#   type = string
# }

variable "application_name" {
  type    = string
  default = "project123"
}
variable "environment_name" {
  type    = string
  default = "azure"
}
variable "location" {
  type    = string
  default = "West Europe"
}
variable "sql_server_login" {
  type    = string
  default = "admin2137"
}
variable "sql_server_password" {
  type    = string
  default = "Admin123"
}

resource "azuread_application" "example" {
  display_name = "example-app-registration-${random_integer.example.result}"
  
  web {
    homepage_url  = local.app_url
    logout_url    = "${local.app_url}/signout-callback-oidc"
    redirect_uris = ["${local.app_url}/signin-oidc"]

    implicit_grant {
      access_token_issuance_enabled = true
      id_token_issuance_enabled     = true
    }
  }
}

resource "azuread_service_principal" "example" {
  application_id = azuread_application.example.application_id
}

resource "time_rotating" "example" {
  rotation_days = 7
}

resource "azuread_service_principal_password" "example" {
  service_principal_id = azuread_service_principal.example.object_id
  rotate_when_changed = {
    rotation = time_rotating.example.id
  }
}
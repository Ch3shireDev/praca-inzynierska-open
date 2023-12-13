$secrets = Get-Content -Raw .\secrets.json | ConvertFrom-Json
$config = Get-Content -Raw .\config.json | ConvertFrom-Json
$report = Get-Content -Raw .\report.json | ConvertFrom-Json

$powerBiTenantId = $secrets.powerBiTenantId
$appTenantId = $secrets.appTenantId
$powerBiClientId = $secrets.powerBiClientId
$powerBiClientSecret = $secrets.powerBiClientSecret
$reportId = $report.reportId
$workspaceId = $report.workspaceId
$powerBiAuthorityUrl = "https://login.microsoftonline.com/$powerBiTenantId/oauth2/token"
$powerBiUsername = $secrets.powerBiUsername
$powerBiPassword = $secrets.powerBiPassword
$sqlUsername = $secrets.sqlUsername
$sqlPassword = $secrets.sqlPassword
$location = $config.location

$powerBiApiUrl =  "https://api.powerbi.com/"
$powerBiResourceUrl = "https://analysis.windows.net/powerbi/api"
$powerBiEmbedUrlBase = "https://app.powerbi.com/"

Set-Location -Path .\terraform

terraform init 
terraform destroy `
    -var "app_tenant_id=$appTenantId" `
    -var "power_bi_tenant_id=$powerBiTenantId" `
    -var "power_bi_application_id=$powerBiClientId" `
    -var "power_bi_application_secret=$powerBiClientSecret" `
    -var "power_bi_report_id=$reportId" `
    -var "power_bi_workspace_id=$workspaceId" `
    -var "power_bi_authority_url=$powerBiAuthorityUrl" `
    -var "power_bi_resource_url=$powerBiResourceUrl" `
    -var "power_bi_api_url=$powerBiApiUrl" `
    -var "power_bi_embed_url_base=$powerBiEmbedUrlBase" `
    -var "power_bi_username=$powerBiUsername" `
    -var "power_bi_password=$powerBiPassword" `
    -var "db_username=$sqlUsername" `
    -var "db_password=$sqlPassword" `
    -var "location=$location" `
    -auto-approve

Set-Location -Path ..
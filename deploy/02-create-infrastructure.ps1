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

try {
    terraform init 
    terraform apply `
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

    $resourceGroupName = terraform output -raw resource_group_name
    $webAppName = terraform output -raw web_app_name
    $sqlServerUrl = terraform output -raw sql_server_url
    $sqlServerName = terraform output -raw sql_server_name

    $infrastructure = @{
        resourceGroupName = $resourceGroupName
        webAppName = $webAppName
        sqlServerName = $sqlServerName
        sqlServerUrl = $sqlServerUrl
    }

    $infrastructure | ConvertTo-Json | Out-File -FilePath ..\infrastructure.json -Force
}
catch {
    Write-Host "Failed to create infrastructure" -ForegroundColor Red
}

Set-Location -Path ..
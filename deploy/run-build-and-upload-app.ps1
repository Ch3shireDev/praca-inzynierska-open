
# skip if already connected

Import-Module Az

if (-not (Get-AzContext)) {
    Connect-AzAccount
}

./07-build-app.ps1
./08-upload-app.ps1

$infrastructure = Get-Content -Raw .\infrastructure.json | ConvertFrom-Json

$resource_group_name = $infrastructure.resourceGroupName
$web_app_name = $infrastructure.webAppName 

Restart-AzWebApp `
    -ResourceGroupName $resource_group_name `
    -Name $web_app_name
    -Force

Import-Module Az

try {

    $infrastructure = Get-Content -Raw .\infrastructure.json | ConvertFrom-Json

    $resource_group_name = $infrastructure.resourceGroupName
    $web_app_name = $infrastructure.webAppName
    $archive_path = 'publish.zip'

    $result = Publish-AzWebApp `
        -ResourceGroupName $resource_group_name `
        -Name $web_app_name `
        -ArchivePath $archive_path `
        -Force

    $result

    Write-Host "ASP.NET project deployed correctly" -ForegroundColor Green
    Write-Host "Webpage: https://$($result.DefaultHostName)" -ForegroundColor Green
}
catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
}
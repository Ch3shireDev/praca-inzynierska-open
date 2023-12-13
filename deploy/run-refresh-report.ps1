./01-report-upload.ps1
./02-create-infrastructure.ps1
./06-report-set-database.ps1


$infrastructure = Get-Content -Raw .\infrastructure.json | ConvertFrom-Json
Write-Host "Webpage: https://$($infrastructure.webAppName).azurewebsites.net/" -ForegroundColor Green
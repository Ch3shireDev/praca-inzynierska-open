Import-Module Az.Accounts

$secrets = Get-Content .\secrets.json | ConvertFrom-Json
 
$appTenantId = $secrets.appTenantId
$appClientSecret = $secrets.appClientSecret
$appClientId = $secrets.appClientId

# Tworzony jest obiekt PSCredential z sekretem klienta
$secureClientSecret = ConvertTo-SecureString -String $appClientSecret -AsPlainText -Force

$credential = New-Object `
    -TypeName System.Management.Automation.PSCredential `
    -ArgumentList $appClientId, $secureClientSecret

# Autentykacja przy użyciu poświadczeń klienta
$result = Connect-AzAccount `
    -ServicePrincipal `
    -Credential $credential `
    -Tenant $appTenantId

Write-Host $result
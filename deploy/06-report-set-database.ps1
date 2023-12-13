# Otwiera połączenie z serwisem Power BI.

Import-Module MicrosoftPowerBIMgmt
Import-Module MicrosoftPowerBIMgmt.Profile

$secrets = Get-Content -Raw .\secrets.json | ConvertFrom-Json
$report = Get-Content -Raw .\report.json | ConvertFrom-Json
$infrastructure = Get-Content -Raw .\infrastructure.json | ConvertFrom-Json
$config = Get-Content -Raw .\config.json | ConvertFrom-Json

try {

    $password = $secrets.powerBiPassword | ConvertTo-SecureString -asPlainText -Force
    $username = $secrets.powerBiUsername

    $credential = New-Object -TypeName System.Management.Automation.PSCredential -argumentlist $username, $password

    $result = Connect-PowerBIServiceAccount -Credential $credential

    if ($null -eq $result) { throw "error" }

    Write-Host "Successfully connected to Power BI" -ForegroundColor Green

}
catch {
    
    Write-Host "Failed to connect to Power BI" -ForegroundColor Red
}

# Uzupełnia parametry w dataset Power BI

try {

    $body = @{
        "updateDetails" = @(
            @{
                name     = "sql-server"
                newValue = $infrastructure.sqlServerUrl
            },
            @{
                name     = "sql-database"
                newValue = $config.databaseName
            }
        )
    } 

    $url = "https://api.powerbi.com/v1.0/myorg/groups/$($report.workspaceId)/datasets/$($report.datasetId)/Default.UpdateParameters"

    Write-Host $url

    $result = Invoke-PowerBIRestMethod `
        -Url $url `
        -Method Post `
        -ContentType 'application/json' `
        -Body $($body | ConvertTo-Json)

    if ($result.error) {
        throw $result.error
    }

    Write-Host "Successfully updated the dataset parameters" -ForegroundColor Green

}
catch {
    Write-Host "Failed to update the dataset parameters" -ForegroundColor Red
    exit
}

# Pobiera id datasource oraz id gateway.

try {

    $datasetId = $report.datasetId
    $workspaceId = $report.workspaceId

    $response = Invoke-PowerBIRestMethod `
        -Url "https://api.powerbi.com/v1.0/myorg/groups/$workspaceId/datasets/$datasetId/datasources" `
        -Method Get `
        -ContentType 'application/json'

    $datasources = $response | ConvertFrom-Json

    $datasource = $datasources.value

    $datasourceId = $datasource.datasourceId
    $gatewayId = $datasource.gatewayId

    Write-Host "Successfully retrieved the datasource: $datasourceId" -ForegroundColor Green

}
catch {
    Write-Host "Failed to retrieve the datasource" -ForegroundColor Red
    exit
}

# Aktualizuje dane logowania do bazy danych w zasobie Power BI

$sqlUsername = $secrets.sqlUsername
$sqlPassword = $secrets.sqlPassword

$bodyContent = @{
    credentialDetails = @{
        credentialType              = "Basic"
        credentials                 = @{
            credentialData = @(
                @{
                    name  = "username"
                    value = $sqlUsername
                },
                @{
                    name  = "password"
                    value = $sqlPassword
                }
            )
        } | ConvertTo-Json -Compress
        encryptedConnection         = "Encrypted"
        encryptionAlgorithm         = "None"
        privacyLevel                = "None"
        useEndUserOAuth2Credentials = "False"
    }
}

$body = $bodyContent | ConvertTo-Json -Compress

$url = "https://api.powerbi.com/v1.0/myorg/gateways/$gatewayId/datasources/$datasourceId" 

try{
        
    $response = Invoke-PowerBIRestMethod `
        -Url $url `
        -Method PATCH `
        -ContentType "application/json" `
        -Body $body

    Write-Host "Credentials updated successfully" -ForegroundColor Green
}
catch {
    Write-Host "Credentials update failed" -ForegroundColor Red
}
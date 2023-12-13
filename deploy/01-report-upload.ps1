# Otwiera połączenie z serwisem Power BI.

Import-Module MicrosoftPowerBIMgmt
Import-Module MicrosoftPowerBIMgmt.Profile

try {
    $secrets = Get-Content -Raw .\secrets.json | ConvertFrom-Json

    $powerBiPassword = $secrets.powerBiPassword | ConvertTo-SecureString -asPlainText -Force
    $powerBiUsername = $secrets.powerBiUsername

    $credential = New-Object -TypeName System.Management.Automation.PSCredential -argumentlist $powerBiUsername, $powerBiPassword

    $result = Connect-PowerBIServiceAccount -Credential $credential

    if ($null -eq $result) { throw "error" }

    Write-Host "Successfully connected to Power BI" -ForegroundColor Green

}
catch {
    Write-Host "Failed to connect to Power BI" -ForegroundColor Red
    exit
}

# Pobiera bądź tworzy workspace.

try {
    $config = Get-Content -Raw .\config.json | ConvertFrom-Json

    # Get the specified workspace
    $workspace = Get-PowerBIWorkspace -Name $config.workspaceName

    # Check if the workspace exists
    if ($null -eq $workspace) {
        Write-Host "Workspace not found. Creating new workspace: $($config.workspaceName)"
        $workspace = New-PowerBIWorkspace -Name $config.workspaceName

        Write-Host $workspace
    }

    $workspaceId = $workspace.Id

    Write-Host "Successfully retrieved the workspace: $workspaceId" -ForegroundColor Green

}
catch {
    Write-Host "Failed to retrieve the workspace" -ForegroundColor Red
    exit
}

# Wysyłanie raportu do Power BI.
# Zapisuje do pliku report.json id raportu, 
# id datasetu, url raportu oraz id workspace.

try {
    $config = Get-Content -Raw .\config.json | ConvertFrom-Json

    $reportRelativePath = $config.reportPath
    
    $currentPath = (Get-Location).Path

    $reportPath = Join-Path -Path $currentPath -ChildPath $reportRelativePath

    $report = New-PowerBIReport `
        -Path $reportPath `
        -Name $config.reportName `
        -WorkspaceId $workspaceId

    $reportId = $report.Id

    $report = Get-PowerBIReport `
        -Id $reportId `
        -WorkspaceId $workspaceId

    $datasetId = $report.datasetId

    $result = @{
        reportId    = $reportId 
        datasetId   = $datasetId
        workspaceId = $workspaceId
        reportUrl   = $report.WebUrl
    }

    $result | ConvertTo-Json | Out-File -FilePath .\report.json

    Write-Host "Successfully uploaded the report: $reportId" -ForegroundColor Green
}
catch {
    Write-Host "Failed to upload the report" -ForegroundColor Red
    Write-Host $_.Exception.Message
}


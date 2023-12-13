Import-Module Az.Sql

$infrastructure = Get-Content -Raw .\infrastructure.json | ConvertFrom-Json
$secrets = Get-Content -Raw .\secrets.json | ConvertFrom-Json
$storage = Get-Content -Raw .\storage.json | ConvertFrom-Json
$config = Get-Content -Raw .\config.json | ConvertFrom-Json

$databaseName = $config.databaseName

$resourceGroupName = $infrastructure.resourceGroupName
$sqlServerName = $infrastructure.sqlServerName
$sqlServerUsername = $secrets.sqlUsername
$sqlServerPassword = $secrets.sqlPassword
$storageAccountName = $storage.storageAccountName
$storageUri = $storage.storageUri

$storageAccountKey = Get-AzStorageAccountKey `
    -ResourceGroupName $resourceGroupName `
    -StorageAccountName $storageAccountName

$storageKey = $storageAccountKey.Value[0]

$securePassword = ConvertTo-SecureString -String $sqlServerPassword -AsPlainText -Force

$importRequest = New-AzSqlDatabaseImport `
    -ResourceGroupName $resourceGroupName `
    -ServerName $sqlServerName `
    -DatabaseName $databaseName `
    -DatabaseMaxSizeBytes 1GB `
    -StorageKeyType "StorageAccessKey" `
    -StorageKey $storageKey `
    -StorageUri $storageUri `
    -Edition "Basic" `
    -ServiceObjectiveName "Basic" `
    -AdministratorLogin $sqlServerUsername `
    -AdministratorLoginPassword $securePassword

# Check import status and wait for the import to complete
$importStatus = Get-AzSqlDatabaseImportExportStatus `
    -OperationStatusLink $importRequest.OperationStatusLink

[Console]::Write("Importing")
while ($importStatus.Status -eq "InProgress") {
    $importStatus = Get-AzSqlDatabaseImportExportStatus `
        -OperationStatusLink $importRequest.OperationStatusLink
    [Console]::Write(".")
    Start-Sleep -s 60
}
[Console]::WriteLine("")
$importStatus
Import-Module Az.Storage

$storage = Get-Content -Raw .\storage.json | ConvertFrom-Json
$infrastructure = Get-Content -Raw .\infrastructure.json | ConvertFrom-Json 

$resourceGroupName = $infrastructure.resourceGroupName
$storageAccountName = $storage.storageAccountName

Remove-AzStorageAccount `
    -ResourceGroupName $resourceGroupName `
    -Name $storageAccountName `
    -Force
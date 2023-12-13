Import-Module Az.Accounts

$infrastructure = Get-Content .\infrastructure.json | ConvertFrom-Json
$config = Get-Content .\config.json | ConvertFrom-Json

$bacpacFilePath = $config.databaseBackupPath

$storageContainerName = "bacpaccontainer"
$storageAccountName = "tempstorage$(Get-Random)"
$resourceGroupName = $infrastructure.resourceGroupName

$resourceGroup = Get-AzResourceGroup -Name $resourceGroupName

# Create a temporary storage account
New-AzStorageAccount `
    -ResourceGroupName $resourceGroupName `
    -Name $storageAccountName `
    -Location $resourceGroup.Location `
    -SkuName Standard_LRS `
    -Kind StorageV2 `
    -AllowBlobPublicAccess $true

$key = Get-AzStorageAccountKey  `
    -ResourceGroupName $resourceGroupName `
    -Name $storageAccountName

$storageAccountKey = $key.Value[0]

$context = New-AzStorageContext `
    -StorageAccountName $storageAccountName `
    -StorageAccountKey $storageAccountKey

New-AzStorageContainer `
    -Name $storageContainerName `
    -Context $context

# Upload sample database into storage container
$result = Set-AzStorageBlobContent `
    -Container $storageContainerName `
    -File $bacpacFilePath `
    -Context $context

$storage = @{
    storageAccountName   = $storageAccountName
    storageContainerName = $storageContainerName
    fileName             = $result.Name
    storageUri           = "https://$storageAccountName.blob.core.windows.net/$storageContainerName/$($result.Name)"
}

$storage | ConvertTo-Json | Out-File .\storage.json
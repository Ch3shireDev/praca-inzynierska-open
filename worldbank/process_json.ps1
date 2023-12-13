$data = Get-Content -Path .\data\world_bank_indicators.json -Raw | ConvertFrom-Json

$list = $data[1]
 
foreach ($item in $list) {

    $item.name
}

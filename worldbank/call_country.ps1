
$regions = Get-Content -Path regions_metadata.json -Raw | ConvertFrom-Json

$results = @()

foreach ($region in $regions) {

$name = $region.name
$code = $region.code

 

Write-Host $name, $code


$session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
 
$result = Invoke-WebRequest -UseBasicParsing -Uri "https://databank.worldbank.org/AjaxServices/AjaxReportMethods.asmx/PopulateMetaDataJSON_SV" `
-Method POST `
-WebSession $session `
-UserAgent "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/112.0" `
-Headers @{
"Accept" = "application/json, text/javascript, */*"
} `
-ContentType "application/json; charset=utf-8" `
-Body "{cubeid: '2', name:'$name', code:'$code', dimensiontype:'C', dimensionname: 'Country', lang:'en'}"

$x = $result.Content | ConvertFrom-Json


Write-Host $x.d

$result = @{
    name = $name
    code = $code
    html = $x.d
}

$results += $result

Write-Host $name

}

$results | ConvertTo-Json | Out-File -FilePath countries_metadata_2.json -Encoding utf8
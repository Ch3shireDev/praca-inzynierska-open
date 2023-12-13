$startDirectory = Get-Location

try {

    $currentPath = (Get-Location).Path
    $appDir = Join-Path -Path $currentPath -ChildPath "../app/WorldFacts.App/"
    Set-Location $appDir

    Remove-Item publish -Recurse `
        -Force `
        -ErrorAction SilentlyContinue

    $publishPath = Join-Path -Path $currentPath -ChildPath "../app/WorldFacts.App/publish"

    dotnet build --configuration Release
    dotnet publish --framework net6.0 `
        --configuration Release `
        --output $publishPath `
        --self-contained false `
        --runtime linux-x64

    Set-Location $publishPath
    7z -r a publish.zip *

    $mainDir = Join-Path -Path $currentPath -ChildPath ".."
    Set-Location $currentPath

    if (Test-Path publish.zip) {
        Remove-Item publish.zip
    }

    $relativePath = "./app/WorldFacts.App/publish/publish.zip" 

    $packagePath = Join-Path -Path $mainDir -ChildPath $relativePath

    Move-Item $packagePath publish.zip -force
}
catch {
    Write-Host "Failed to build app" -ForegroundColor Red
}

Set-Location -Path $startDirectory
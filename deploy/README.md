# Pliki wdrożeniowe

W katalogu znajdują się następujące pliki i foldery:

-	`./terraform` - folder z plikami konfiguracji Terraform.
-	`./00-login-azure.ps1` - logowanie do Azure przez przeglądarkę.
-	`./01-report-upload.ps1` - logowanie i wysyłanie raportu do serwisu Power BI.
-	`./02-create-infrastructure.ps1` - tworzenie infrastruktury chmury z użyciem Terraform.
-	`./03-upload-backup.ps1` - wysyłanie kopii zapasowej bazy danych do Azure Storage.
-	`./04-load-backup-from-storage-to-database.ps1` - ładowanie kopii zapasowej bazy danych.
-	`./05-remove-storage.ps1` - usuwanie przestrzeni magazynowej.
-	`./06-report-set-database.ps1` - zmiana źródła danych w raporcie Power BI na Azure SQL.
-	`./07-build-app.ps1` - kompilacja plików witryny internetowej do spakowanych binariów.
-	`./08-upload-app.ps1` - wysyłanie paczki do usługi App Service.
-	`./config.json` - konfiguracja (jawna) skryptów wdrożeniowych.
-	`./secrets.json` - konfiguracja (tajna) haseł i kluczy do usług.

## Konfiguracja

Należy uzupełnić wartości w pliku `./secrets.json`:

```json
{
    // identyfikator dzierżawy Azure AD dla aplikacji webowej
    "appTenantId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    // identyfikator aplikacji webowej
    "appClientId": "085e6813-fe18-4cc3-8324-5af9283ef5f0",
    // klucz aplikacji webowej
    "appClientSecret": "xxxxx~xxxxxxxxxxxxxxxxxxxxxxxxxx.xxxxxxx",
    // identyfikator dzierżawy Azure AD dla usługi Power BI - ta sama co dla aplikacji
    "powerBiTenantId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    // identyfikator aplikacji usługi Power BI z dostępem do usługi Power BI i przyznanymi uprawnieniami administratora
    "powerBiClientId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    // klucz aplikacji usługi Power BI
    "powerBiClientSecret": "xxxxx~xxxxxxxxxxxxxxxxxxxxxxxxxx.xxxxxxx",
    // nazwa użytkownika utworzonego w dzierżawie, z dostępem do usługi Power BI i przyznanymi uprawnieniami administratora
    "powerBiUsername": "username@tenant.onmicrosoft.com",
    // hasło użytkownika
    "powerBiPassword": "**********",
    // nazwa administratora bazy danych
    "sqlUsername": "databasea-admin",
    // hasło administratora bazy danych
    "sqlPassword": "*************"
}
```
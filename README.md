# Praca inżynierska

## Temat

Projekt i implementacja systemu do wizualizacji danych w chmurze Azure z wykorzystaniem metod automatycznego wdrażania

## Cel

Celem pracy jest zaprojektowanie i zaimplementowanie platformy do wizualizacji danych społeczno-gospodarczych z publicznych baz. Portal będzie dostępny w chmurze Azure, a jego wdrożenie zostanie zautomatyzowane z wykorzystaniem narzędzi DevOps.

## Zakres

Praca obejmuje projekt, implementację i automatyczne wdrożenie systemu do wizualizacji i analizy danych społeczno-gospodarczych w chmurze Azure. Analiza danych będzie realizowana przy użyciu narzędzia Power BI, natomiast aplikacja internetowa zostanie przygotowana z użyciem frameworka ASP.NET Core w języku C#. Dane będą przechowywane w bazie danych SQL Server, z użyciem usługi Azure SQL Database. Do automatycznego wdrażania zostaną wykorzystane skrypty PowerShell wraz z biblioteką Azure PowerShell oraz narzędziem Terraform.


## Diagram zasobów

Poniżej przedstawiam uproszczony diagram zasobów wykorzystywanych w projekcie. Strzałki oznaczają kierunek zależności, tj. od zasobu podrzędnego do zasobu nadrzędnego. Diagram został wygenerowany przy użyciu narzędzia PlantUML.

```plantuml
@startuml

!define AzurePuml https://raw.githubusercontent.com/plantuml-stdlib/Azure-PlantUML/release/2-2/dist

!includeurl AzurePuml/AzureCommon.puml
!includeurl AzurePuml/AzureSimplified.puml
!includeurl AzurePuml/Management/AzureResourceGroups.puml
!includeurl AzurePuml/Compute/all.puml
!includeurl AzurePuml/Databases/all.puml
!includeurl AzurePuml/Analytics/PowerBI.puml
!includeurl AzurePuml/Storage/all.puml
!includeurl AzurePuml/Identity/all.puml
!includeurl AzurePuml/Analytics/AzureEventHub.puml
!includeurl AzurePuml/Compute/AzureFunction.puml
!includeurl AzurePuml/Databases/AzureCosmosDb.puml
!includeurl AzurePuml/Storage/AzureDataLakeStorage.puml
!includeurl AzurePuml/Analytics/AzureStreamAnalyticsJob.puml
!includeurl AzurePuml/InternetOfThings/AzureTimeSeriesInsights.puml
!includeurl AzurePuml/Identity/AzureActiveDirectoryB2C.puml
!includeurl AzurePuml/DevOps/AzureApplicationInsights.puml


LAYOUT_LEFT_RIGHT

AzureResourceGroups(resourceGroup, "Resource Group", "East US 2")
AzureAppServicePlan(appServicePlan, "App Service Plan", "East US 2")
AzureSql(azureSql, "Azure SQL", "East US 2")
AzureAppService(appService, "App Service", "East US 2")
PowerBI(powerBi, "Power BI Report", "East US 2")
AzureActiveDirectory(activeDirectory, "Azure Active Directory", "East US 2")

AzureAppRegistration(appRegistrationPowerBi, "App Registration PowerBI", "East US 2")
AzureAppRegistration(appRegistrationAppService, "App Registration AppService", "East US 2")

resourceGroup <-- appServicePlan
resourceGroup <-- azureSql
appServicePlan <-- appService
azureSql <-- powerBi
appService -> powerBi

azureSql <-- appService
powerBi --> appRegistrationPowerBi
appService --> appRegistrationPowerBi
appService --> appRegistrationAppService

appRegistrationPowerBi --> activeDirectory
appRegistrationAppService --> activeDirectory


@enduml
```
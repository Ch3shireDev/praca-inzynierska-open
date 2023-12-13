import yaml
import pyodbc

query = "select REGION_CODE, REGION_NAME_EN, REGION_NAME_PL from REGIONS"

connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=WORLDBANK;Trusted_Connection=yes;')

cursor = connection.cursor()

reader = cursor.execute(query)

countries = []

for row in reader:
    country = {'symbol': row[0], 'name_en': row[1], 'name_pl': row[2]}
    countries.append(country)



with open('regions-2.yml', 'w', encoding='utf-8') as f:
    yaml.dump(countries, f, allow_unicode=True, sort_keys=False)
    f.close()
import pyodbc
import yaml


with open('region-3.yml', 'r', encoding='utf-8') as f:
    countries = yaml.load(f, Loader=yaml.FullLoader)
    f.close()


connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=WORLDBANK;Trusted_Connection=yes;')

cursor = connection.cursor()

for country in countries:
    region_en = country['region_en']
    region_pl = country['region_pl']

    query = "update REGIONS set REGION_PL = ? where REGION = ?"

    cursor.execute(query, region_pl, region_en)

cursor.commit()
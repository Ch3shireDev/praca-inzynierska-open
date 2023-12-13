import yaml

with open('regions.yml', 'r', encoding='utf-8') as f:
    countries = yaml.load(f, Loader=yaml.FullLoader)
    f.close()



import pyodbc

query = "update REGIONS set REGION_NAME_PL = ? where REGION_CODE = ?"

connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=WORLDBANK;Trusted_Connection=yes;')

cursor = connection.cursor()

for row in countries:

    cursor.execute(query, row['name_pl'], row['symbol'])

cursor.commit()
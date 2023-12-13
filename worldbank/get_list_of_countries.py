import pyodbc

import pyodbc

connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=DEVELOPMENT_DB;Trusted_Connection=yes;')

cursor = connection.cursor()

reader = cursor.execute("SELECT * FROM REGIONS")

results = []

for row in reader:
    REGION_CODE = row[0]
    REGION_ISO_CODE = row[1]
    REGION_NAME_EN = row[2]
    REGION_IS_COUNTRY = row[3]

    name = REGION_NAME_EN
    code = REGION_ISO_CODE

    result = {'name': name, 'code': code}
    results.append(result)



cursor.commit()


import json

with open('regions_metadata.json', 'w') as outfile:
    json.dump(results, outfile)
    outfile.close()
    
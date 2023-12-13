import pyodbc
import json

with open('regions_metadata.json', 'r') as infile:
    data = json.load(infile)
    infile.close()

results = []
for region in data:

    code = region['code']

    region_data = region['data']
    if 'Income Group' not in region_data:
        income_group = None
    else:
        income_group = region_data['Income Group']

    if 'Region' not in region_data:
        region = None
    else:
        region = region_data['Region']

    if 'Special Notes' not in region_data:
        special_notes = None
    else:
        special_notes = region_data['Special Notes']

    if 'System of trade' not in region_data:
        system_of_trade = None
    else:
        system_of_trade = region_data['System of trade']

    result = {'code': code, 'region': region, 'income_group': income_group,
              'special_notes': special_notes, 'system_of_trade': system_of_trade}

    results.append(result)

connection = pyodbc.connect(
    'Driver={SQL Server Native Client 11.0};Server=localhost;Database=DEVELOPMENT_DB;Trusted_Connection=yes;')

cursor = connection.cursor()

for result in results:
    cursor.execute("update REGIONS set INCOME_GROUP = ?, REGION = ?, SPECIAL_NOTES = ?, SYSTEM_OF_TRADE = ? where REGION_ISO_CODE = ?",
                   result['income_group'], result['region'], result['special_notes'], result['system_of_trade'], result['code'])

cursor.commit()

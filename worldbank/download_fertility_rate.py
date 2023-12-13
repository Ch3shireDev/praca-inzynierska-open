import requests
import json

def download_fertility_rate():
    indicator_code = 'SP.DYN.TFRT.IN'
    base_url = "http://api.worldbank.org/v2/country/all/indicator/"
    url = f"{base_url}{indicator_code}?format=json&per_page=20000"
    response = requests.get(url)
    response.raise_for_status()
    return response.json()


fertility_rate = download_fertility_rate()

with open('fertility_rate.json', 'w') as f:
    
    json.dump(fertility_rate, f)
    f.close()

results = fertility_rate[1]

sql_data = []

for result in results:
    country_code = result['country']['id']
    year = result['date']
    value = result['value']

    data = {'region_code': country_code, 'year': year, 'value': value}
    sql_data.append(data)

import pyodbc

query = "update INDICATORS set fertility_rate_total_births_per_woman = ? where region_code = ? and year = ?"

connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=WORLDBANK;Trusted_Connection=yes;')

cursor = connection.cursor()

for row in sql_data:
    print(row['value'], row['region_code'], row['year'])
    cursor.execute(query, row['value'], row['region_code'], row['year'])

cursor.commit()
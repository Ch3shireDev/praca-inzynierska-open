import json
import pyodbc

filepath = 'download_data.json'

with open(filepath, 'r', encoding='utf-8') as f:
    json_data = json.load(f)
    f.close()



data = {}

indicator_data = {}
region_data = {}

for element in json_data:
    
    indicator_id = element["indicator_id"]
    sql_column = element["sql_column"]
    header = element["header"]
    content  = element["content"]

    print(f"Processing {indicator_id} - {sql_column}")

    for item in content:
        indicator_id = item['indicator']['id']
        indicator_value = item['indicator']['value']

        if indicator_id not in indicator_data:
            indicator_data[indicator_id] = {'indicator_description': indicator_value, 'indicator_column': sql_column}
        

        country_iso = item['countryiso3code']
        date = item['date']
        value = item['value']
        country_code = item['country']['id']
        country_name = item['country']['value']

        if country_code not in region_data:
            region_data[country_code] = {'region_iso_code': country_iso, 'region_code': country_code, 'region_name': country_name}

        key = (country_code, date)

        if key not in data:
            data[key] = {'region_iso_code': country_iso, 'region_code': country_code, 'date': date, 'region_name': country_name, 'indicator_id': indicator_id, 'indicator_value': indicator_value}
        
        data[key][sql_column] = value


sql_columns = [data['sql_column'] for data in json_data]


connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=DEVELOPMENT_DB;Trusted_Connection=yes;')

cursor = connection.cursor()

columns = ','.join(sql_columns)
question_marks = ','.join(['?'] * len(sql_columns))
query = f'INSERT INTO INDICATORS (INDICATOR_ID, REGION_ISO_CODE, REGION_CODE, YEAR, {columns}) values (?,?,?,?,{question_marks})'


indicator_id = 1

for key in data:

    values = [None] * len(sql_columns)

    for sql_column in sql_columns:
        values[sql_columns.index(sql_column)] = data[key][sql_column]

    country_iso = data[key]['region_iso_code']
    country_code = data[key]['region_code']
    date = data[key]['date']

    cursor.execute(query, (indicator_id, country_iso, country_code, date, *values))
    print("insert value for indicator_id", indicator_id, "and country_code", country_code, "and date", date)

    indicator_id += 1

for key in region_data:

    region = region_data[key]

    query = 'INSERT INTO REGIONS (REGION_ISO_CODE, REGION_CODE, REGION_NAME_EN) values (?,?,?)'
    cursor.execute(query, (region['region_iso_code'], region['region_code'], region['region_name']))
    print("insert value for region_iso_code", region['region_iso_code'], "and region_code", region['region_code'], "and region_name", region['region_name'])

indicator_id = 1
for key in indicator_data:

    indicator_id = key
    indicator_value = indicator_data[key]['indicator_description']
    indicator_column = indicator_data[key]['indicator_column']

    query = 'INSERT INTO INDICATOR_DESCRIPTIONS (INDICATOR_ID, INDICATOR_COLUMN, INDICATOR_DESCRIPTION) values (?,?,?)'
    cursor.execute(query, (indicator_id, indicator_column, indicator_value))
    print("insert value for indicator_id", indicator_id, "and indicator_value", indicator_value)

cursor.commit()
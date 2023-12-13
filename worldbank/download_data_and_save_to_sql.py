from lib import download_indicator_data

filename = 'data_to_download.txt'

with open(filename, 'r', encoding='utf-8') as f:
    content = f.readlines()
    content = [x.strip() for x in content]
    content = [x.split(' - ') for x in content]
    content = [(x[0], x[1]) for x in content]
    f.close()

results = []

for indicator_id, sql_column in content:
    print(f"Downloading {indicator_id} - {sql_column}")
    indicator_data = download_indicator_data(indicator_id)
    
    header, content = indicator_data

    result = {'indicator_id': indicator_id, 'sql_column': sql_column, 'header': header, 'content': content}

    results.append(result)



import json

with open('download_data.json', 'w', encoding='utf-8') as f:
    json.dump(results, f, ensure_ascii=False, indent=4)
    f.close()
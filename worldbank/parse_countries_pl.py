import re
import bs4

with open('countries.html', 'r', encoding='utf-8') as f:
    html = f.read()
    f.close()

soup = bs4.BeautifulSoup(html, 'html.parser')

rows = soup.find_all('tr')
 

countries = []

for row in rows:
    cells = row.find_all('td')
    if len(cells) > 0:
        symbol = cells[0].text.strip()
        name_pl = cells[1].text.strip()
        
        name_pl = re.sub(r'\s+', ' ', name_pl)
        
        name_en = cells[2].text.strip()

        name_en = re.sub(r'\s+', ' ', name_en)

        country = {'symbol': symbol, 'name_en': name_en, 'name_pl': name_pl}
        print(symbol, name_pl)
        countries.append(country)

import pyodbc

query = "update REGIONS set REGION_NAME_PL = ? where REGION_CODE = ?"

connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=WORLDBANK;Trusted_Connection=yes;')

cursor = connection.cursor()

for row in countries:

    cursor.execute(query, row['name_pl'], row['symbol'])

cursor.commit()
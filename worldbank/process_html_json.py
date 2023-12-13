import json
import bs4

def get_data(html):
    if not html:
        return None
    soup = bs4.BeautifulSoup(html, 'html.parser')
    trs = soup.find_all('tr')

    data = {}

    for tr in trs:
        tds = tr.find_all('td')
        if tds is None:
            continue
        if len(tds) == 2:
            key = tds[0].text.strip()
            value = tds[1].text.strip()
            data[key] = value

    return data


with open('countries_metadata_2.json', 'r', encoding='utf-8-sig') as infile:
    regions = json.load(infile)
    infile.close()


results = []

for region in regions:
    html = region['html']
    code = region['code']
    name = region['name']

    data = get_data(html)

    result = {'name': name, 'code': code, 'data': data}
    results.append(result)

with open('regions_metadata.json', 'w', encoding='utf-8') as outfile:
    json.dump(results, outfile)
    outfile.close()
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

with open('korea.html', 'r', encoding='utf-8') as infile:
    html = infile.read()
    infile.close()

data = get_data(html)

import json

with open('korea.json', 'w', encoding='utf-8') as outfile:
    json.dump(data, outfile)
    outfile.close()
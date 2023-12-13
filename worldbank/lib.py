import requests
import json
import re

def get_world_bank_indicators():
    base_url = "http://api.worldbank.org/v2/indicator"
    url = f"{base_url}?format=json&per_page=20000"
    response = requests.get(url)

    if response.status_code == 200:
        data = response.json()
        indicators = data[1]
        return indicators
    else:
        print("Error: Unable to fetch indicators. Status code:", response.status_code)
        return None

def download_indicator_data(indicator_code): 
    base_url = "http://api.worldbank.org/v2/country/all/indicator/"
    url = f"{base_url}{indicator_code}?format=json&per_page=20000"
    response = requests.get(url)

    if response.status_code == 200:
        data = response.json()
        return data
    else:
        print(f"Error: Unable to fetch data for {indicator_code}. Status code: {response.status_code}")
        return None

def download_all_indicators_data():
    indicators = get_world_bank_indicators()
    if indicators:
        for indicator in indicators:
            indicator_code = indicator["id"]
            indicator_name = indicator["name"]
            download_indicator_data(indicator_code, indicator_name)

if __name__ == "__main__":
    download_all_indicators_data()

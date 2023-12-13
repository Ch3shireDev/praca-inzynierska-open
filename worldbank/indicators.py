import requests
import json

def get_world_bank_indicators():
    base_url = "http://api.worldbank.org/v2/indicator"
    url = f"{base_url}?format=json&per_page=200000"
    response = requests.get(url)

    if response.status_code == 200:
        data = response.json()

        data = data[1]

        names = [x['name'] for x in data]

        with open("world_bank_indicators.json", "w") as f:
            json.dump(names, f, indent=4)
        print("Indicators downloaded and saved as world_bank_indicators.json")
    else:
        print("Error: Unable to fetch indicators. Status code:", response.status_code)

if __name__ == "__main__":
    get_world_bank_indicators()
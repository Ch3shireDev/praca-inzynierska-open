import requests
import json

def download_world_bank_data():
    indicator = "SP.DYN.IMRT.IN"  # Infant Mortality Rate (per 1,000 live births)
    base_url = "http://api.worldbank.org/v2/country/all/indicator/"
    url = f"{base_url}{indicator}?format=json&per_page=20000"
    response = requests.get(url)

    if response.status_code == 200:
        data = response.json()
        with open("world_bank_imr_data.json", "w") as f:
            json.dump(data, f, indent=4)
        print("Data downloaded and saved as world_bank_imr_data.json")
    else:
        print("Error: Unable to fetch data. Status code:", response.status_code)

if __name__ == "__main__":
    download_world_bank_data()
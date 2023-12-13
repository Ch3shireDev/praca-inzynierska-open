import wbdata
import json
import requests

def main():
    # Get countries from World Bank API
    countries = wbdata.get_country()
    
    # Filter out non-country entities
    country_list = [country for country in countries]
    
    # Print list of countries and metadata
    for country in country_list:
        print(f"Country Name: {country['name']}")
        print(f"Country ISO2 Code: {country['iso2Code']}")
        print(f"Country ISO3 Code: {country['id']}")
        print(f"Region: {country['region']['value']}")
        print(f"Income Level: {country['incomeLevel']['value']}")
        print(f"Latitude: {country['latitude']}")
        print(f"Longitude: {country['longitude']}")
        print("-------------------------------")

        # Get specialNotes using a separate API call
        special_notes_url = f"http://api.worldbank.org/v2/country/{country['id']}?format=json"
        special_notes_response = requests.get(special_notes_url)
        special_notes_data = special_notes_response.json()
        special_notes = special_notes_data[1][0]['specialNotes']

        print(f"Special Notes: {special_notes}")
        print("-------------------------------")

        # Add specialNotes to the country dictionary
        country['specialNotes'] = special_notes
        
    # Save the country data to a JSON file
    with open('countries_metadata.json', 'w') as outfile:
        json.dump(country_list, outfile)

if __name__ == "__main__":
    main()

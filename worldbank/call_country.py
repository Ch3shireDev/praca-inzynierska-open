import requests

url = "https://databank.worldbank.org/AjaxServices/AjaxReportMethods.asmx/PopulateMetaDataJSON_SV"

#{cubeid: '2', name:'Afghanistan', code:'AFG', dimensiontype:'C', dimensionname: 'Country', lang:'en'}

data = {
    "cubeid": "2",
    "name": "Afghanistan",
    "code": "AFG",
    "dimensiontype": "C",
    "dimensionname": "Country",
    "lang": "en"
}

result = requests.post(url, data=data, headers={"Accept": "application/json, text/javascript, */*; q=0.01"})

result_html = result.text

print(result_html)
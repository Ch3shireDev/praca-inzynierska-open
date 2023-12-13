tab = [
    ('High income', 'Kraje o wysokich dochodach'),
    ('Low income', 'Kraje o niskich dochodach'),
    ('Lower middle income', 'Kraje o niższych średnich dochodach'),
    ('Upper middle income', 'Kraje o wyższych średnich dochodach')
]


import pyodbc

query = "update REGIONS set INCOME_GROUP_PL = ? where INCOME_GROUP = ?"

connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=WORLDBANK;Trusted_Connection=yes;')

cursor = connection.cursor()

for income_group_en, income_group_pl in tab:
    cursor.execute(query, income_group_pl, income_group_en)

cursor.commit()
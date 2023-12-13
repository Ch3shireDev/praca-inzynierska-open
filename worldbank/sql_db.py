import pyodbc

connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=localhost;Database=DEVELOPMENT_DB;Trusted_Connection=yes;')

cursor = connection.cursor()


cursor.commit()
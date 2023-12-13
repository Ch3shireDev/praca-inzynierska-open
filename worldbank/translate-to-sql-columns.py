import re

def as_sql_column(line):
    
    line = line.strip()
    line = line.replace('$', 'USD')
    line = line.replace('%', ' PERCENT')
    line = line.replace('-', ' ')
    line = line.strip()
    line = re.sub(r'[^\sA-Za-z\d]', r'', line)
    line = re.sub(r'\s+', r'_', line)
    line = line.upper()

    return line


file = 'indicators_to_get.md'

with open(file, 'r') as f:
    lines = f.readlines()
    f.close()

lines = [line.strip() for line in lines]
lines = [line for line in lines if line != ''  and line[0] != '#']

for line in lines:
    
    line = line.split('-')

    indicator_id = line[-1].strip()
    line = line[1:-1]
    line = str.join('-', line)
    sql_column = as_sql_column(line)

    print(indicator_id, "-", sql_column)


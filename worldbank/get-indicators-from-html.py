import re
from bs4 import BeautifulSoup

def parse_html_file(file_path):
    with open(file_path, 'r', encoding='utf-8') as html_file:
        html_content = html_file.read()
        soup = BeautifulSoup(html_content, 'html.parser')
        return soup

if __name__ == "__main__":
    file_path = "a.html"
    soup = parse_html_file(file_path)
    

    for element in soup.find_all('li'):
        x = element.find('span').attrs['data-value-code']
        # print(x)
        y = element.find('span', class_='bookunit').text.strip().replace('\n', ' ')
        y = re.sub(' +', ' ', y)
        print("-" ,y, "-", x)
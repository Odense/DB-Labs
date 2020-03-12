from scrapy.crawler import CrawlerProcess
from scrapy.utils.project import get_project_settings
from lxml import etree
import os
import webbrowser


def cleanup():
    try:
        os.remove("../results/uartlib.xml")
        os.remove("../results/hozmart.xml")
        os.remove("../results/hozmart.xhtml")
    except OSError:
        pass


def scrap_data():
    process = CrawlerProcess(get_project_settings())
    process.crawl('uartlib')
    process.crawl('hozmart')
    process.start()


def task2():
    root = etree.parse("../results/uartlib.xml")
    pages = root.xpath("//page")
    count = 0
    sum_of_fragments = 0
    for page in pages:
        count = count + 1
        sum_of_fragments = sum_of_fragments + page.xpath("count(fragment[@type='text'])")
    print("Average quantity: %d" % (sum_of_fragments / count))


def task3_4():
    print("Products of internet shop hozmart.com.ua")
    transform = etree.XSLT(etree.parse("hozmart.xsl"))
    result = transform(etree.parse("../results/hozmart.xml"))
    result.write("../results/hozmart.xhtml", pretty_print=True, encoding="UTF-8")
    webbrowser.open('file://' + os.path.realpath("../results/hozmart.xhtml"))


if __name__ == '__main__':
    cleanup()
    scrap_data()
    print("Scraping completed")
    while True:
        print("*" * 50)
        print("1. Task 2 (Average value of text elements on uartlib.org)")
        print("2. Task 3 and 4")
        print("> ", end='', flush=True)
        number = input()
        if number == "1":
            task2()
        elif number == "2":
            task3_4()
        else:
            break

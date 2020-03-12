# -*- coding: utf-8 -*-
from scrapy.http.response import Response
import scrapy


class HozmartSpider(scrapy.Spider):
    name = 'hozmart'
    start_urls = ['https://www.hozmart.com.ua/uk/15-benzopili']

    def parse(self, response: Response):
        products = response.xpath("//ul[@id=\"product_list\"]/li")[:20]
        for product in products:
            yield {
                'description': product.xpath(".//a[@class='b1c-name-uk']/@title").get(),
                'price': product.xpath(".//div[@class='content_price']/span/text()").get(),
                'img': product.xpath(".//img[@class='b1c-img']/@src").get()
            }

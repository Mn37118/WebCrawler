using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Net;

namespace WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient wc = new WebClient();

            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8 //set UTF
            };

            //Load trang web, nap html vao document
            HtmlDocument document = htmlWeb.Load("https://phuclong.com.vn/category/thuc-uong");

            //Load cac tag div.product-item trong div.row
            var threadItems = document.DocumentNode.QuerySelectorAll("div.row > div.product-item").ToList();
            List<object> Objs = new List<object>();

            Console.WriteLine("Push 'yes' to crawl data from website.");
            string answer = Console.ReadLine();

            if (answer == "yes")
            {
                foreach (var item in threadItems)
                {
                    var ImageNode = item.QuerySelector("img.item-img");
                    var ImageUrl = ImageNode.Attributes["data-original"].Value;
                    string fileImg = System.IO.Path.GetFileName(ImageUrl);
                    var Name = item.QuerySelector("div.item-name").InnerText;
                    var Price = item.QuerySelector("div.item-price").InnerText.Replace(" đ", "").Replace(".", "");

                    wc.DownloadFile(ImageUrl, @"d:\Học tập\WebCrawler\images\" + fileImg);
                    Objs.Add(new { fileImg, Name, Price });
                }

                Console.WriteLine("Data List:");
                Console.WriteLine("So luong phan tu trong list la : {0}", Objs.Count);
                foreach (object item in Objs)
                {
                    Console.WriteLine(item);
                }
            }   
            Console.ReadKey();
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace Hyhrobot.WebReptile.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            CrawlerRun.Run("http://www.yhd.com/", "yhd.com", "最好");
            //var crawler = new SimpleCrawler(new Uri("http://ppdai.com"), 1, "金融", "ppdai.com");
            //crawler.CrawlerCompletedEvent += Crawler_CrawlerCompletedEvent;
            //crawler.CrawlerErrorEvent += Crawler_CrawlerErrorEvent;
            //crawler.Start();
            //Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void Crawler_CrawlerErrorEvent(Dto.CrawlerErrorDto obj)
        {
            Console.WriteLine(obj);
        }

        private static void Crawler_CrawlerCompletedEvent(Dto.CrawlerCompletedDto obj)
        {
            Console.WriteLine(obj);
       
        }
    }
}

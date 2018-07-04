using System;
using System.Text.RegularExpressions;

namespace Hyhrobot.WebReptile.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var crawler = new SimpleCrawler(new Uri("http://ppdai.com"), 1);
            crawler.CrawlerCompletedEvent += Crawler_CrawlerCompletedEvent;
            crawler.CrawlerErrorEvent += Crawler_CrawlerErrorEvent;
            crawler.Start();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        private static void Crawler_CrawlerErrorEvent(Dto.CrawlerErrorDto obj)
        {
            Console.WriteLine(obj);
        }

        private static void Crawler_CrawlerCompletedEvent(Dto.CrawlerCompletedDto obj)
        {
            Console.WriteLine(obj);
            string reg = @"<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>";

            var links = Regex.Matches(obj.PageSource, @"<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>", RegexOptions.IgnoreCase);
            foreach (Match match in links)
            {

                Console.WriteLine("Text:"+match.Groups["text"].Value+" Value:"+ match.Groups["href"].Value);
           

            }
        }
    }
}

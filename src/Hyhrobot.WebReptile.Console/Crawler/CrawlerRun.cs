using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
namespace Hyhrobot.WebReptile.Crawler
{
    public class CrawlerRun
    {
        public static Dictionary<string, bool> CrawlerVisitDict = new Dictionary<string, bool>();

        private static int MaxLeavel = 10;
        static string Domain;
        static string Key;
        public static void Run(string url, string domain, string key)
        {
            Domain = domain;
            Key = key;
            var crawler = new SimpleCrawler(new Uri(url), 1, key, domain);
            crawler.CrawlerCompletedEvent += Crawler_CrawlerCompletedEvent;
            crawler.CrawlerErrorEvent += Crawler_CrawlerErrorEvent;
            crawler.Start();
        }

        private static void Crawler_CrawlerErrorEvent(Dto.CrawlerErrorDto obj)
        {
         //   Console.WriteLine(obj);
        }

        private static void Crawler_CrawlerCompletedEvent(Dto.CrawlerCompletedDto obj)
        {
            if (obj.Level < MaxLeavel)
            {
                Console.WriteLine(obj);

                Parallel.ForEach(obj.ListUrl, (url) =>
                {
                    try
                    {
                        Uri uri = new Uri(url);
                        var crawler = new SimpleCrawler(new Uri(url), obj.Level + 1, Key, Domain);
                        crawler.CrawlerCompletedEvent += Crawler_CrawlerCompletedEvent;
                        crawler.CrawlerErrorEvent += Crawler_CrawlerErrorEvent;
                        crawler.Start();
                    }
                    catch (Exception)
                    {

                       
                    }
                
                  

                });

            }
          


        }
    }
}

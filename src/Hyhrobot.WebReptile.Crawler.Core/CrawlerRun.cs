using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyhrobot.WebReptile.Crawler.Core
{
    public class CrawlerRun
    {
        public event Action<Dto.CrawlerCompletedDto> CrawlerCompletedEvent;

        public event Action<Dto.CrawlerErrorDto> CrawlerErrorEvent;
        private static int MaxLeavel = 10;
        string Domain = "";
        List<string> Keys;

        private Crawler crawler;
        public CrawlerRun(string url, string domain, List<string> keys)
        {
            Domain = domain;
            Keys = keys;
            crawler = new Crawler(new Uri(url), 1, keys, domain);
            crawler.CrawlerCompletedEvent += Crawler_CrawlerCompletedEvent;
            crawler.CrawlerErrorEvent += Crawler_CrawlerErrorEvent;
        }

        private void Crawler_CrawlerErrorEvent(Dto.CrawlerErrorDto obj)
        {
            CrawlerErrorEvent?.Invoke(obj);
        }

        private void Crawler_CrawlerCompletedEvent(Dto.CrawlerCompletedDto obj)
        {
            if (obj.Level < MaxLeavel)
            {
                CrawlerCompletedEvent?.Invoke(obj);

                Parallel.ForEach(obj.ListUrl, (url) =>
                {
                    try
                    {
                        Uri uri = new Uri(url);
                        var crawler = new Crawler(new Uri(url), obj.Level + 1, Keys, Domain);
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

        public void Run()
        {
            crawler.Start();
        }
    }
}

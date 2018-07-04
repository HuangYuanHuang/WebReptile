using System;
using System.Collections.Generic;
using System.Text;
using Hyhrobot.WebReptile.Crawler.Dto;

namespace Hyhrobot.WebReptile.Crawler
{
    public class SimpleCrawler : BaseCrawler
    {
       
        public SimpleCrawler(Uri url, int level, string proxy = null) : base(url, level, proxy)
        {
           
        }
    }
}

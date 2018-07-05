using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyhrobot.WebReptile.Crawler.Core.Dto
{
    public class CrawlerErrorDto:BaseEventDto
    {
        public string Message { get; set; }

        public override string ToString()
        {
            return $"Url:[{Url}],Level:{Level},Exception:{Message}";
        }
    }
}

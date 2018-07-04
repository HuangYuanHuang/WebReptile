using System;
using System.Collections.Generic;
using System.Text;

namespace Hyhrobot.WebReptile.Crawler.Dto
{
    public class BaseEventDto
    {
        public string Url { get; set; }

        public int Level { get; set; }
    }
    public class CrawlerCompletedDto : BaseEventDto
    {

        public int ThreadId { get; set; }

        public long ElapsedMilliseconds { get; set; }

        public string PageSource { get; set; }

        public override string ToString()
        {
            return $"Url:[{Url}],Level:{Level}, ThreadId:{ThreadId},ElapsedMilliseconds:{ElapsedMilliseconds}";
        }
    }

    public class CrawlerErrorDto : BaseEventDto
    {

        public string Message { get; set; }

        public override string ToString()
        {
            return $"Url:[{Url}],Level:{Level},Exception:{Message}";
        }
    }
}

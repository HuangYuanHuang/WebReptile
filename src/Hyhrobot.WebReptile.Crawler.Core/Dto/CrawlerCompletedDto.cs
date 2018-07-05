using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyhrobot.WebReptile.Crawler.Core.Dto
{
    public class CrawlerCompletedDto : BaseEventDto
    {
        public List<string> ListUrl { get; set; }

        public List<MatchKeyNode> MatchKeys { get; set; }

        public int ThreadId { get; set; }

        public long ElapsedMilliseconds { get; set; }

        public string PageSource { get; set; }

        public string Result { get { return MatchKeys.Aggregate("", (s, n) => s + n); } }
        public override string ToString()
        {
            string keys = MatchKeys.Aggregate("", (s, n) => s + n);
            return $"地址:【{Url}】,层级:{Level},线程ID:{ThreadId},用时:【{ElapsedMilliseconds}】{keys}";
        }
    }

    public class MatchKeyNode
    {
        public string Key { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            return $"【{Key}】:【{Count}】";
        }
    }
}

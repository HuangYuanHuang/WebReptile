using Hyhrobot.WebReptile.Crawler.Core.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hyhrobot.WebReptile.Crawler.Core
{
    public class Crawler
    {
        static Dictionary<string, bool> CrawlerVisitDict = new Dictionary<string, bool>();

        static bool IsRun = true;
        public CookieContainer CookiesContainer { get; set; }
        public Uri CrawlerUrl { get; set; }
        public string Proxy { get; set; }

        public int Level { get; set; }

        public List<string> Keys { get; set; }
        public string Domain { set; get; }
        public event Action<Dto.CrawlerCompletedDto> CrawlerCompletedEvent;

        public event Action<Dto.CrawlerErrorDto> CrawlerErrorEvent;
        public Crawler(Uri url, int level, List<string> keys, string domain, string proxy = null)
        {
            CrawlerUrl = url;
            Keys = keys;
            Proxy = proxy;
            Level = level;
            Domain = domain;
            CookiesContainer = new CookieContainer();
        }
        public static void StopAll()
        {
            IsRun = false;
        }

        public string Start()
        {
            if (!IsRun)
            {
                return "";
            }
            var pageSource = string.Empty;
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                var request = (HttpWebRequest)WebRequest.Create(CrawlerUrl);
                request.Accept = "*/*";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");//定义gzip压缩页面支持
                request.ContentType = "application/x-www-form-urlencoded";//定义文档类型及编码
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.117 Safari/537.36";
                request.Timeout = 5000;//定义请求超时时间为5秒
                request.KeepAlive = true;//启用长连接
                request.Method = "GET";//定义请求方式为GET              
                if (Proxy != null)
                {
                    request.Proxy = new WebProxy(Proxy);//设置代理服务器IP，伪装请求地址
                }
                request.CookieContainer = this.CookiesContainer;//附加Cookie容器
                string scheme = "http";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    //获取请求响应
                    foreach (Cookie cookie in response.Cookies)
                    {
                        this.CookiesContainer.Add(cookie);//将Cookie加入容器，保存登录状态
                    }

                    if (response.ContentEncoding.ToLower().Contains("gzip"))//解压
                    {
                        using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                pageSource = reader.ReadToEnd();
                            }
                        }
                    }
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))//解压
                    {
                        using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                pageSource = reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        using (Stream stream = response.GetResponseStream())//原始
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                pageSource = reader.ReadToEnd();
                            }
                        }
                    }
                    scheme = response.ResponseUri.Scheme;
                    if (!CrawlerVisitDict.ContainsKey(response.ResponseUri.AbsoluteUri))
                    {
                        CrawlerVisitDict.Add(response.ResponseUri.AbsoluteUri, true);
                    }
                }
                request.Abort();

                watch.Stop();

                var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;//获取当前任务线程ID

                var milliseconds = watch.ElapsedMilliseconds;//获取请求执行时间

                var links = Regex.Matches(pageSource, @"<a[^>]*href=([""'])?(?<href>[^'""]+)\1[^>]*>", RegexOptions.IgnoreCase);
                List<MatchKeyNode> listNode = new List<MatchKeyNode>();
                Keys.ForEach(d =>
                {
                    var linkKey = Regex.Matches(pageSource, d, RegexOptions.IgnoreCase);
                    listNode.Add(new MatchKeyNode() { Count = linkKey.Count, Key = d });
                });

                List<string> listUrls = new List<string>();
                foreach (Match match in links)
                {
                    var value = match.Groups["href"].Value;
                    if (value.Contains(Domain))
                    {
                        if (value.StartsWith("//"))
                        {
                            value = value.Substring(2);
                        }
                        if (!(value.Contains("http")||value.Contains("https")))
                        {
                            value = scheme + "://" + value;
                        }
                        if (!CrawlerVisitDict.ContainsKey(value))
                        {
                            CrawlerVisitDict.Add(value, true);
                            listUrls.Add(value);
                        }

                    }

                }
                CrawlerCompletedEvent?.Invoke(new Dto.CrawlerCompletedDto()
                {
                    Level = Level,
                    ElapsedMilliseconds = milliseconds,
                    ThreadId = threadId,
                    Url = CrawlerUrl.AbsoluteUri,
                    ListUrl = listUrls,
                    MatchKeys = listNode
                });
            }
            catch (Exception ex)
            {
                CrawlerErrorEvent?.Invoke(new Dto.CrawlerErrorDto()
                {
                    Level = Level,
                    Message = ex.Message,
                    Url = CrawlerUrl.AbsoluteUri
                });
            }

            return pageSource;

        }
    }
}

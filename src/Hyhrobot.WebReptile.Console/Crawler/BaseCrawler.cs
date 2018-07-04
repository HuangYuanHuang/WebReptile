using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hyhrobot.WebReptile.Crawler
{
    public abstract class BaseCrawler
    {
        public CookieContainer CookiesContainer { get; set; }
        public Uri CrawlerUrl { get; set; }
        public string Proxy { get; set; }

        public int Level { get; set; }

        public event  Action<Dto.CrawlerCompletedDto> CrawlerCompletedEvent;

        public event  Action<Dto.CrawlerErrorDto> CrawlerErrorEvent;
        public BaseCrawler(Uri url, int level, string proxy = null)
        {
            CrawlerUrl = url;
            Proxy = proxy;
            Level = level;
            CookiesContainer = new CookieContainer();
        }
        public string Start()
        {
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
                }
                request.Abort();

                watch.Stop();

                var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;//获取当前任务线程ID

                var milliseconds = watch.ElapsedMilliseconds;//获取请求执行时间

                CrawlerCompletedEvent?.Invoke(new Dto.CrawlerCompletedDto()
                {
                    Level = Level,
                    ElapsedMilliseconds = milliseconds,
                    PageSource = pageSource,
                    ThreadId = threadId,
                    Url = CrawlerUrl.AbsoluteUri
                });
            }
            catch (Exception ex)
            {
                CrawlerErrorEvent?.Invoke(new Dto.CrawlerErrorDto()
                {
                    Level=Level,
                    Message=ex.Message,
                    Url=CrawlerUrl.AbsoluteUri
                });
            }

            return pageSource;

        }
    }
}

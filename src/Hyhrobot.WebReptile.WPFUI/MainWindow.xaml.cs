using Hyhrobot.WebReptile.Crawler.Core;
using Hyhrobot.WebReptile.Crawler.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hyhrobot.WebReptile.WPFUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        CrawlerRun crawlerRun;
 
        int errorCount = 0;
        int totalCount = 0;
        List<MatchKeyNode> matchList;
        int keyLength = 0;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void but_go_Click(object sender, RoutedEventArgs e)
        {
            string url = textBox_url.Text.Trim();
            string host = textBox_Host.Text.Trim();
            var keys = textBox_Key.Text.Trim().Split(',');
            keyLength = keys.Length;
            matchList = keys.Select(d => new MatchKeyNode() { Key = d, Count = 0 }).ToList();
            crawlerRun = new CrawlerRun(url, host, keys.ToList());
            crawlerRun.CrawlerCompletedEvent += CrawlerRun_CrawlerCompletedEvent;
            crawlerRun.CrawlerErrorEvent += CrawlerRun_CrawlerErrorEvent;

            Task.Factory.StartNew(() => crawlerRun.Run());
            but_go.IsEnabled = false;
        }

        private void CrawlerRun_CrawlerErrorEvent(Crawler.Core.Dto.CrawlerErrorDto obj)
        {
            errorCount++;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                listView_error.Items.Add(obj);
                textBlock_error.Text = errorCount.ToString();

            }));
        }

        private void CrawlerRun_CrawlerCompletedEvent(Crawler.Core.Dto.CrawlerCompletedDto obj)
        {
            totalCount++;
            for (int i = 0; i < keyLength; i++)
            {
                matchList[i].Count += obj.MatchKeys[i].Count;
            }
            string temp= matchList.Aggregate("", (s, n) => s + n);
        
            Dispatcher.BeginInvoke(new Action(() =>
            {
                listView_success.Items.Add(obj);
                textBlock_success.Text = temp;
                textBlock_total.Text = totalCount.ToString();
            }));
        }
    }
}

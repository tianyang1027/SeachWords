using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SeachWords
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TextWriter log = new StreamWriter(@"C:\Users\v-yangtian\Downloads\log.txt");
            //string html = await HttpRequestHelper.RequestUrl("http://www.restaurantweekboston.com/?neighborhood=all&meal=all&cuisine=all&view=all");
            //var htmlDoc = new HtmlDocument();
            //htmlDoc.LoadHtml(html);

            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlDocument htmlDoc = htmlWeb.Load("http://www.restaurantweekboston.com/?neighborhood=all&meal=all&cuisine=all&view=all");

            List<string> list = new List<string>();
            var nodes = htmlDoc.DocumentNode.SelectNodes("/html/body/div[2]/div[1]/div[2]/div[1]/div[3]/div[2]/div[3]/div/div[1]/h4/a[1]");
            foreach (var node in nodes)
            {
                var url = $"http://www.restaurantweekboston.com{node.Attributes["href"].Value}";
                log.WriteLine(url);
                list.Add(url);
            }
            log.Close();
            Console.WriteLine("output file success!");
            Console.ReadKey();
        }
    }

    public class HttpRequestHelper
    {
        public static async Task<string> RequestUrl(string url)
        {
            string content = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    content = await sr.ReadToEndAsync();
                }
            }
            return content;
        }
    }
}

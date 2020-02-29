using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ExamDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ExamDotNet.Parser
{
    public class Parser
    {
        private List<Uri> result;

        private string GetText(Url link) 
        {
            //var link = new Url("https://habr.com/ru/news/t/485626/");
            var downloaded = GetPage(link);
            if (downloaded != null)
            {
                var d = downloaded.Body.Text();
                return downloaded.Body.TextContent;
            }
            else 
            {
                return null;
            }
            //var d1 = downloaded.Body.TextContent;
            //var dow = RemoveTags(downloaded.Body.TextContent);

            //string q = null;
        }

        public LinkModel[] Parse(DtoLink model) 
        {
            LinkModel[] result = GetUrls(model);
            foreach (var item in result)
            {
                item.Text = GetText(new Url(item.Url));
            }

            return result;
        }

        private LinkModel[] GetUrls(DtoLink model) 
        {
            //var model = new DtoLink();
            //model.Link=new Uri("https://habr.com/ru/news/t/485626/");
            //model.Depth=3;
            //model.Limit = 10;
            result =new List<Uri>();           
            var dict = new Dictionary<int, List<Uri>>();
            var list = GetAllUrlsPage(model.Link,model.Limit);
            foreach (var item in list)
            {
                ItemContain(item);                
            }
            dict.Add(0, result);
            var links = new List<Uri>(dict[0]);
            for (int i = 1; i < model.Depth; i++)
            {
                dict.Add(i, new List<Uri>());
                foreach (var link in links)
                {
                    var res = GetAllUrlsPage(link, model.Limit);
                    foreach (var item in res)
                    {
                        if (ItemContain(item)) 
                        {
                            dict[i].Add(item);
                        } 
                    }
                }
                links = new List<Uri>(dict[i]);
            }
            var tresult = new LinkModel[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                tresult[i] = new LinkModel(result[i], model.Link.OriginalString);
            }

            return tresult;
        }

        private bool ItemContain(Uri item) 
        {
            if (result.Contains(item))
            {
                return false;
            }
            else
            {
                result.Add(item);
                return true;
            }


        }

        private List<Uri> GetAllUrlsPage(Uri uri,int numpage) 
        {
            var result = new List<Uri>();
            var parser = new HtmlParser();
            Url u = new Url(uri.OriginalString);
            var document = GetPage(u);

            var links = document.QuerySelectorAll("a");
            foreach (var link in links)
            {
                try 
                {
                    result.Add(new Uri(link.GetAttribute("href")));
                }
                catch 
                { 
                }
                      
            }
            //var some = result.Where(x=>x.Host==uri.Host).ToList();
            return result.Where(x => x.Host == uri.Host).Take(numpage).ToList();
        }

        private IDocument GetPage(Url url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            try
            {
                if (document.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"Bad document status: {document.StatusCode}");
                return document;
            }
            catch 
            {
                return null;
            }
        }        
    }
}

using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using ExamDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExamDotNet.Parser
{
    public class ParalelParser
    {
        public static void Parse(DtoLink model) 
        {

        }

        private static Task<IDocument> GetPage(Url url) 
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
    

        public static List<Uri> GetUrlsFromPage(Uri uri, int numpage) 
        {
            var result = new List<Uri>();
            var parser = new HtmlParser();
            Url u = new Url(uri.OriginalString);
            var document = new Task<IDocument>(GetPage(u)).Result;

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

        public static void GetTextFromPage() { }

        public static void AddLinkToListLink() { }

        public static void AddTextToListREsult() { }

    }
}

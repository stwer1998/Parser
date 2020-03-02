using AngleSharp;
using AngleSharp.Dom;
using ExamDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ExamDotNet.Parser
{
    public class ParalelParser
    {
        
        private List<Uri> allLinks;
        private DtoLink model;
        private List<LinkModel> result;
        //private object procCount;
        public ParalelParser(DtoLink model)
        {
            this.model = model;            
            allLinks = new List<Uri>();
        }
        public LinkModel[] Parse()
        {
            result = new List<LinkModel>();
            //procCount = 0;
            GetAllUrls();
            IndexPagesAsync();
            return null;

        }

        private IDocument GetPage(Uri url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url.ToString()).Result;
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

        private void GetAllUrls()
        {
            var pagelinks = new Dictionary<int, List<Uri>>();
            pagelinks.Add(0,GetUrlsFromPage(model.Link, model.Limit));
            for (int i = 1; i <= model.Depth; i++)
            {
                foreach (var uri in pagelinks[i-1])
                {
                    var links = GetUrlsFromPage(uri,model.Limit);
                    if (links != null&&links.Count>0) 
                    {
                        if (pagelinks.ContainsKey(i))
                        {
                            pagelinks[i].AddRange(links);
                        }
                        else 
                        {
                            pagelinks.Add(i, links);
                        }
                    }
                }
            }
            allLinks = pagelinks.SelectMany(x => x.Value).ToList();
            
        }

        private List<Uri> GetUrlsFromPage(Uri uri, int numpage)
        {
            if (!allLinks.Contains(uri))
            {
                allLinks.Add(uri);
                var page = GetPage(uri);
                if (page != null)
                {
                    return page
                         .QuerySelectorAll("a")
                         .Select(x => x.GetAttribute("href"))
                         .Where(x => x.StartsWith(model.Link.ToString()))
                         .Select(x => new Uri(x))
                         .Take(numpage)
                         .ToList();
                }
            }
            return null;
        }

        private async Task IndexPagesAsync() 
        {

            var result = from link in allLinks.AsParallel()
                         select link;
            result.ForAll((x) => Index(x));

        }

        private void Index(object uri) 
        {
            var link = (Uri)uri;
            var page = GetPage(link);
            if (page != null) 
            {
                result.Add(new LinkModel
                {
                    //IdGuid = Guid.NewGuid(),
                    IndexedUrl = link.ToString(),
                    Url = model.Link.ToString(),
                    Text = page.Body.TextContent
                });
            }
            //lock (procCount) 
            //{
            //    var s = (int)procCount;
            //    procCount = s--;
            //}
        }
    }
}

using Parser.Core;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace Parser
{
    class HtmlLoader
    {
        readonly IConfiguration configuration;
        readonly string url;
        public HtmlLoader(IParserSettings settings) 
        {
            configuration = Configuration.Default.WithDefaultLoader();
            url = settings.BaseUrl;
        }

        public async Task<IDocument> GetPage() 
        {
            var document = await BrowsingContext.New(configuration).OpenAsync(url);

            return document ?? null;
        }
    }
}

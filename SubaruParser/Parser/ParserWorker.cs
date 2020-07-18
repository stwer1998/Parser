using Parser.Core;
using System.Threading.Tasks;

namespace Parser
{
    class ParserWorker<T> where T : IParseble
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        HtmlLoader loader;

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings)
        {
            this.parser = parser;
            this.parserSettings = parserSettings;
            loader=new HtmlLoader(parserSettings);
        }

        public async Task<T> Worker() 
        {
            var source = await loader.GetPage();

            return parser.Parse(source);
        } 
    }
}

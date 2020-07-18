using AngleSharp.Dom;
using AngleSharp.XPath;
using Parser.Core;

namespace Parser.Subaru
{
    class SubaruParser : IParser<Car>
    {
        public IParserSettings settings { get; set; } = new SubaruSettings();

        public Car Parse(IDocument document)
        {
            var secondcar = document?.Body?.SelectSingleNode(settings.NodeName);
            return new Car(secondcar);
        }
    }
}

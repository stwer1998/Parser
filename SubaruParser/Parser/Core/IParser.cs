using AngleSharp.Dom;

namespace Parser.Core
{
    interface IParser<T> where T: IParseble
    {
        IParserSettings settings { get; set; }

        T Parse(IDocument document);
    }
}

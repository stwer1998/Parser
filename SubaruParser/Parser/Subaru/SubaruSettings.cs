using Parser.Core;

namespace Parser.Subaru
{
    class SubaruSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://www.kellysubaru.com/used-inventory/index.htm";
        public string NodeName { get; set; } = "(//li[contains(@class,'item notshared')])[2]";
    }
}

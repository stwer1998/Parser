using AngleSharp.Dom;
using Parser.Core;
using System.Linq;

namespace Parser
{
    class Car: IParseble
    {
        string ImageUrl { get; set; }

        string CarVin { get; set; }

        string CarPrice { get; set; }

        public Car()
        {

        }

        public Car(INode node)
        {
            Parse(node);
        }

        public override string ToString()
        {
            return $"Image Url : {ImageUrl} \nCar VIN : {CarVin} \nCar Price : {CarPrice} \n ";
        }

        public void Parse(INode node)
        {
            this.ImageUrl = node?.ChildNodes?
                .GetElementsByClassName("photo thumb")?
                .Select(x => x.GetAttribute("src"))?
                .FirstOrDefault()?.Split('?')[0];

            this.CarVin = node?.ChildNodes?
                .GetElementsByClassName("vin")?
                .Select(x => x.GetElementsByTagName("dd"))?
                .FirstOrDefault()?
                .Select(x => x.TextContent)?
                .FirstOrDefault();

            this.CarPrice = node?.ChildNodes?
                .GetElementsByClassName("internetPrice final-price")?
                .FirstOrDefault()?
                .GetElementsByClassName("value")?
                .FirstOrDefault()?
                .TextContent;
        }
    }
}

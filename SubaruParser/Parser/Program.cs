using Parser.Subaru;
using System;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parser start");

            var parser = new ParserWorker<Car>(new SubaruParser(), new SubaruSettings());

            var result = parser.Worker().Result;

            Console.WriteLine("Result:");

            Console.WriteLine(result.ToString());

            Console.WriteLine("Parser end");

            Console.ReadLine();
        }
    }
}

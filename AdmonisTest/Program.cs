using AdmonisTest.Admonis;
using AdmonisTest.DataConvert;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdmonisTest
{
    internal class Program
    {
        const string inputPath = @"testData\Product.xml";
        static void Main(string[] args)
        {
            Console.WriteLine("Started ...");
            DateTime before = DateTime.Now;
            ConvertProductsData();
            DateTime after = DateTime.Now;
            TimeSpan timeDiff = after.Subtract(before);
            Console.WriteLine($"Run time: {timeDiff.TotalSeconds} seconds");
        }

        private static void ConvertProductsData()
        {
            string folder = Path.Combine(AppContext.BaseDirectory, @"..\..\");
            string dataFullPath = $"{folder}{inputPath}";
            DataConverterCreator convertCreator = new DataConverterCreator();
            IInputDataConverter converter = convertCreator.GetDataConverter(DataFormats.XmlDataFormat);

            IList<AdmonisProduct> admProducts = converter.ConvertProductsData(dataFullPath);
            Console.WriteLine($"Converted {admProducts.Count} product items");
        }

    }
}

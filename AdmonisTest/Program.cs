using AdmonisTest.Admonis;
using AdmonisTest.DataConvert;
using AdmonisTest.LogHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdmonisTest
{
    internal class Program
    {
        const string inputRelativeName = @".\testdata\Product.xml";
        const int ALL_PRODUCTS = -1;
        static void Main(string[] args)
        {
            int requestedProductCount = (args.Length > 0) ? Int32.Parse(args[0]) : ALL_PRODUCTS;

            LogHelperCreator logHelperCreator = new LogHelperCreator();
            ILogHelper logHelper = logHelperCreator.CreateLogHelper(LogTypes.Console);

            logHelper.LogMessage("Started ...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ConvertProductsData(logHelper, requestedProductCount);

            stopwatch.Stop();
            TimeSpan timeDiff = stopwatch.Elapsed;

            logHelper.LogMessage($"Run time: {timeDiff.TotalSeconds} seconds");
        }

        private static void ConvertProductsData(ILogHelper logHelper, int productCount = -1)
        {
            string dataFullPath = $"{AppContext.BaseDirectory}\\{inputRelativeName}";
            DataConverterCreator convertCreator = new DataConverterCreator();

            IInputDataConverter converter = convertCreator.GetDataConverter(DataFormats.XmlDataFormat, logHelper);

            IList<AdmonisProduct> admProducts = (productCount == ALL_PRODUCTS)
                ? converter.ConvertProductsData(dataFullPath).ToList()
                : converter.ConvertProductsData(dataFullPath).Take(productCount).ToList();
            logHelper.LogMessage($"Converted {admProducts.Count} product items");
        }

    }
}

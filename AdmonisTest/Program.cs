using AdmonisTest.Admonis;
using AdmonisTest.DataConvert;
using AdmonisTest.LogHelpers;
using System;
using System.Collections.Generic;

namespace AdmonisTest
{
    internal class Program
    {
        const string inputRelativeName = @".\testdata\Product.xml";
        static void Main(string[] args)
        {
            LogHelperCreator logHelperCreator = new LogHelperCreator();
            ILogHelper logHelper = logHelperCreator.CreateLogHelper(LogTypes.Console);
            logHelper.LogMessage("Started ...");
            DateTime before = DateTime.Now;
            ConvertProductsData(logHelper);
            DateTime after = DateTime.Now;
            TimeSpan timeDiff = after.Subtract(before);
            logHelper.LogMessage($"Run time: {timeDiff.TotalSeconds} seconds");
        }

        private static void ConvertProductsData(ILogHelper logHelper)
        {
            string dataFullPath = $"{AppContext.BaseDirectory}\\{inputRelativeName}";
            DataConverterCreator convertCreator = new DataConverterCreator();

            IInputDataConverter converter = convertCreator.GetDataConverter(DataFormats.XmlDataFormat, logHelper);

            IList<AdmonisProduct> admProducts = converter.ConvertProductsData(dataFullPath);
            logHelper.LogMessage($"Converted {admProducts.Count} product items");
        }


    }
}

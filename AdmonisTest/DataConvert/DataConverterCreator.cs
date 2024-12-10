using AdmonisTest.impl;
using System;

namespace AdmonisTest.DataConvert
{
    public class DataConverterCreator
    {
        public IInputDataConverter GetDataConverter(string dataFormat)
        {
            switch (dataFormat)
            {
                case DataFormats.XmlDataFormat:
                    return new XmlDataConverter(new AdmonisProductMapper());
                default:
                    throw new ArgumentException("Unsupported data format");

            }
        }
    }
}

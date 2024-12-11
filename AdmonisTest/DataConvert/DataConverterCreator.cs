using AdmonisTest.impl;
using AdmonisTest.LogHelpers;
using System;

namespace AdmonisTest.DataConvert
{
    public class DataConverterCreator
    {
        public IInputDataConverter GetDataConverter(string dataFormat, ILogHelper logHelper)
        {
            switch (dataFormat) //Here can be added creation of converters from other formats ( e.g. CSV etc) 
            {
                case DataFormats.XmlDataFormat:
                    return new XmlDataConverter(new AdmonisProductMapper(logHelper), logHelper);
                default:
                    throw new ArgumentException("Unsupported data format");

            }
        }
    }
}

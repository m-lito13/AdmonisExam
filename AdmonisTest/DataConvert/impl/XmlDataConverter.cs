using AdmonisTest.Admonis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace AdmonisTest.impl
{
    public class XmlDataConverter : IInputDataConverter
    {
        private readonly AdmonisProductMapper mapper;
        public XmlDataConverter(AdmonisProductMapper mapper)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// Convert product data from Xml to list of AdmonisProduct items
        /// </summary>
        /// <param name="dataPath">path of file</param>
        /// <returns></returns>
        public IList<AdmonisProduct> ConvertProductsData(string dataPath)
        {
            try
            {
                IEnumerable<AdmonisProductOption> admonisProductOptions = ConvertProductOptionsDataInternal(dataPath);
                IEnumerable<AdmonisProduct> result = ConvertProductsDataInternal(dataPath, admonisProductOptions);
                return result.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot convert : {ex.Message}");
                return new List<AdmonisProduct>();
            }

        }


        /// <summary>
        /// Convert data of products which have child items 
        /// </summary>
        /// <param name="dataPath">path of file</param>
        /// <param name="optionsDetails">options data converted</param>
        /// <returns></returns>
        private IEnumerable<AdmonisProduct> ConvertProductsDataInternal(string dataPath, IEnumerable<AdmonisProductOption> optionsDetails)
        {
            Dictionary<string, AdmonisProductOption> optionsDetailsDict = optionsDetails.ToDictionary(opt => opt.optionMakat);
            using (XmlReader reader = XmlReader.Create(dataPath))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "product")
                            {
                                XElement xElem = XElement.ReadFrom(reader) as XElement;
                                bool hasVariants = xElem.Elements().Any(item => item.Name.LocalName == XmlConstants.Variations);
                                if (hasVariants)
                                {
                                    AdmonisProduct product = this.mapper.GetAdmonisProductFromXElement(xElem, optionsDetailsDict);
                                    if (product != null)
                                    {
                                        yield return product;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Convert data for child items (options) 
        /// </summary>
        /// <param name="dataPath">path of file</param>
        /// <returns></returns>
        private IEnumerable<AdmonisProductOption> ConvertProductOptionsDataInternal(string dataPath)
        {
            using (XmlReader reader = XmlReader.Create(dataPath))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "product")
                            {
                                XElement xElem = XElement.ReadFrom(reader) as XElement;
                                bool hasVariants = xElem.Elements().Any(item => item.Name.LocalName == XmlConstants.Variations);
                                if (!hasVariants)
                                {
                                    AdmonisProductOption productOption = mapper.GetAdmonisProductOptionFromXElement(xElem);
                                    if (productOption != null)
                                    {
                                        yield return productOption;
                                    }

                                }
                            }
                            break;

                    }
                }
            }
        }



    }

}

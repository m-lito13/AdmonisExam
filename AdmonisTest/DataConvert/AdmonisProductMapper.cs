using AdmonisTest.Admonis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AdmonisTest
{
    public class AdmonisProductMapper
    {
        /// <summary>
        /// Convert product data with child elements(options) from XML element to AdmonisProduct class 
        /// </summary>
        /// <param name="xElem"></param>
        /// <param name="optionsDetails"></param>
        /// <returns></returns>
        public AdmonisProduct GetAdmonisProductFromXElement(XElement xElem, Dictionary<string, AdmonisProductOption> optionsDetails)
        {
            string productMakat = string.Empty; //Used to log makat to exception
            try
            {
                AdmonisProduct product = new AdmonisProduct();
                productMakat = xElem.Attribute(XmlConstants.ProductId).Value;
                product.Makat = productMakat;
                string nameSpace = xElem.Name.NamespaceName;

                product.Brand = xElem.Element(XName.Get(XmlConstants.Brand, nameSpace))?.Value;
                product.Name = xElem.Element(XName.Get(XmlConstants.DisplayName, nameSpace))?.Value;
                product.VideoLink = xElem.Element(XName.Get(XmlConstants.F54ProductVideo, nameSpace))?.Value;
                product.DescriptionLong = xElem.Element(XName.Get(XmlConstants.LongDescription, nameSpace))?.Value;
                product.Description = xElem.Element(XName.Get(XmlConstants.ShortDescription, nameSpace))?.Value;

                XElement variationsElem = xElem.Element(XName.Get(XmlConstants.Variations, nameSpace));
                FillOptionsData(product, variationsElem, optionsDetails);
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot convert product {productMakat}");
                throw;
            }

        }

        /// <summary>
        /// Convert child product data from XML element to AdmonisProductOption class 
        /// </summary>
        /// <param name="xElem"></param>
        /// <returns></returns>
        public AdmonisProductOption GetAdmonisProductOptionFromXElement(XElement xElem)
        {
            string optionMakat = string.Empty;//Used to log makat to exception
            try
            {
                optionMakat = xElem.Attribute(XmlConstants.ProductId).Value;
                AdmonisProductOption productOption = CreateDefaultProductOption(optionMakat);

                string nameSpace = xElem.Name.NamespaceName;

                XElement customAttributes = xElem.Element(XName.Get(XmlConstants.CustomAttributes, nameSpace));
                if (customAttributes != null)
                {
                    FillProductOptionFromCustomAttributes(productOption, customAttributes);
                }
                else
                {
                    Console.WriteLine($"Custom attributes for product option {optionMakat} not found");
                }
                return productOption;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cannot convert product option {optionMakat}");
                throw;
            }

        }

        /// <summary>
        /// For converted product fill options
        /// </summary>
        /// <param name="product"></param>
        /// <param name="xElem"></param>
        /// <param name="optionsDetails"></param>
        private void FillOptionsData(AdmonisProduct product, XElement xElem, Dictionary<string, AdmonisProductOption> optionsDetails)
        {
            string nameSpace = xElem.Name.NamespaceName;
            XElement variantsElement = xElem.Element(XName.Get(XmlConstants.Variants, nameSpace));
            foreach (XElement variantElem in variantsElement.Elements())
            {
                string optionId = variantElem.Attribute(XmlConstants.ProductId).Value;
                if (optionsDetails.ContainsKey(optionId))
                {
                    AdmonisProductOption optionToAdd = optionsDetails[optionId];
                    optionToAdd.ProductMakat = product.Makat;
                    product.Options.Add(optionToAdd);
                }
                else
                {
                    Console.WriteLine($"Cannot find details for option {optionId}");
                }
            }
        }


        /// <summary>
        /// Create AdmonisProductOption item for given optionId with default values 
        /// </summary>
        /// <param name="optionId"></param>
        /// <returns></returns>
        private AdmonisProductOption CreateDefaultProductOption(string optionId)
        {
            AdmonisProductOption resultOption = new AdmonisProductOption();
            resultOption.optionMakat = optionId;
            resultOption.optionSugName1 = DefaultValues.OptionSugName1Default;
            resultOption.optionSugName1Title = DefaultValues.OptionSugName1TitleDefault;
            resultOption.optionSugName2Title = DefaultValues.OptionSugName2TitleDefault;
            return resultOption;
        }

        private void FillProductOptionFromCustomAttributes(AdmonisProductOption productOption, XElement xElem)
        {
            HashSet<string> attributesToCheck = new HashSet<string>
            {
                XmlConstants.F54ProductColor,
                XmlConstants.F54ProductSize
            };

            Dictionary<string, XElement> productRelatedElems = productRelatedElems = xElem.Elements()
                .Where(elem => attributesToCheck.Contains(elem.Attribute(XmlConstants.AttrinuteId).Value))
                .ToDictionary(elem => elem.Attribute(XmlConstants.AttrinuteId).Value);

            if (productRelatedElems.ContainsKey(XmlConstants.F54ProductColor))
            {
                productOption.optionSugName2 = productRelatedElems[XmlConstants.F54ProductColor].Value;
            }


            if (productRelatedElems.ContainsKey(XmlConstants.F54ProductSize))
            {
                productOption.optionName = productRelatedElems[XmlConstants.F54ProductSize].Value;
            }
        }
    }
}

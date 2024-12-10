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
                AdmonisProductOption productOption = new AdmonisProductOption();
                productOption.optionMakat = xElem.Attribute(XmlConstants.ProductId).Value;
                productOption.optionSugName1 = DefaultValues.OptionSugName1Default;
                productOption.optionSugName1Title = DefaultValues.OptionSugName1TitleDefault;
                productOption.optionSugName2Title = DefaultValues.OptionSugName2TitleDefault;

                string nameSpace = xElem.Name.NamespaceName;

                XElement customAttributes = xElem.Element(XName.Get(XmlConstants.CustomAttributes, nameSpace));
                FillProductOptionFromCustomAttributes(productOption, customAttributes);
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
                AdmonisProductOption optionToAdd = GetAdmonisProductOptionFromVariant(product.Makat, variantElem, optionsDetails);
                product.Options.Add(optionToAdd);
            }
        }

        /// <summary>
        /// Get option data by id from variant xml element
        /// </summary>
        /// <param name="prodMakat"></param>
        /// <param name="variantElem"></param>
        /// <param name="optionsDetails"></param>
        /// <returns></returns>
        private AdmonisProductOption GetAdmonisProductOptionFromVariant(string prodMakat, XElement variantElem, Dictionary<string, AdmonisProductOption> optionsDetails)
        {
            string optionId = variantElem.Attribute(XmlConstants.ProductId).Value;
            AdmonisProductOption optDetails = optionsDetails[optionId];
            optDetails.ProductMakat = prodMakat;
            return optDetails;
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

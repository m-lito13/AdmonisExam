using AdmonisTest.Admonis;
using System.Collections.Generic;

namespace AdmonisTest
{
    public interface IInputDataConverter
    {
        IList<AdmonisProduct> ConvertProductsData(string dataPath);
    }
}

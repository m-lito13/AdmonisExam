using AdmonisTest.Admonis;
using System.Collections.Generic;

namespace AdmonisTest
{
    public interface IInputDataConverter
    {
        IEnumerable<AdmonisProduct> ConvertProductsData(string dataPath);
    }
}

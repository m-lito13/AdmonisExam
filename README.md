
# XML to AdmonisProduct-s Converter

Application for converting products data stored in XML file into list of AdmonisProduct class items. The application was built with target .NET Framework 4.7.2
 
## Running Locally

Before running , it should be validated that  <running_folder> contains : 

1. File ```AdmonisTest.exe```

2. Subfolder named ```testData``` . This subfolder should contain file  named ```Product.xml```. 

<running_folder> - folder from which the application will run. 
In order to change input data file Product.xml should edited and replaced.

Command to run : 

```AdmonisTest.exe <number_of_products>```

Where <number_of_products> - optional parameter telling number of products to convert.For example :

After running ```AdmonisTest.exe 10```

First 10 products (having child products) will be converted. When running  ```AdmonisTest.exe``` without params - all products will be converted

##  Some Details

Convert logic implemented in class ```XmlDataConverter``` in method 
```ConvertProductsData``` - which getting full path of the xml file
returning ```IEmurable<AdmonisProduct>```. The collection is lazy loaded. 

Usage of the ```XmlDataConverter``` can be seen in method         ```ConvertProductsData``` of ```program.cs``` 


NOTE : If for some product there is some id of child product listed under ```<variants>``` element , but definion does not exists in the xml file (no product with corresponding id) , it will not be added to options list of this product 






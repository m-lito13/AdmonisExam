
# XML to AdmonisProduct-s Converter

Application for converting products data stored in XML file into list of AdmonisProduct class items. The application was built with target .NET Framework 4.7.2



## Running Locally

Before running , it should be validated that  <running_folder> contains : 

1. File ```AdmonisTest.exe```

2. Subfolder named ```testData``` . This subfolder should contain file  named ```Product.xml```. 

<running_folder> - folder from which the application will run. 
In order to change input data file Product.xml should edited and replaced.

NOTE: If for some product there is some id of child product listed under ```<variants>``` element , but definion does not exists in the xml file (no product with corresponding id) , it will not be added to options list of this product   




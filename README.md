# DynamicSSRS

DynamicSSRS is a flexible library designed for managing SQL Server Reporting Services (SSRS) dynamically in an ASP.NET Web Forms environment. It allows you to upload RDL files, configure data sources, manage report parameters, and perform other SSRS-related tasks programmatically.

---

## Features

- **Upload RDL Files**: Upload multiple RDL files dynamically to the SSRS server.
- **Create Data Sources**: Configure custom data sources with connection strings and credentials.
- **Assign Data Sources**: Assign specific data sources to RDL files programmatically.
- **Manage Report Parameters**: Update parameters of existing RDL files, including their default values and prompts.
- **Dynamic Report Management**: Perform SSRS operations on multiple servers from a single application.

---

## Requirements

- **ASP.NET Web Forms**: Targeting .NET Framework 4.8 or later.
- **SQL Server Reporting Services**: Compatible with SSRS 2016, 2019, and later versions.
- **Visual Studio**: Developed and tested with Visual Studio 2019+.

---

## Installation

### Adding **DynamicSSRS** to Your WebForms Project

1. Right-click on the **Bin** folder in your WebForms project.
2. Click on **"Add Reference"**.
3. In the opened window:
   - From the left section, select **"Projects"**.
   - From the right section, select **"DynamicSSRS"**.
   - Click **"OK"** to add the reference.

4. Add the namespace to your code:
   ```csharp
   using DynamicSSRS;
   
## Getting Started

### 1. Configure the SSRS Server

Before using the library, ensure that your SSRS server is properly configured and accessible via its web service endpoint.

Example SSRS endpoint:  
`http://your-ssrs-server/ReportServer/ReportService2010.asmx`

### 2. Initialize the Library

To use **DynamicSSRS**, you need to create an instance of the `SSRSMethods` class and provide the server URL and credentials:

```csharp
var ssrsMethods = new SSRSMethods("http://your-ssrs-server/ReportServer", "your-username", "your-password", "");
```

### 3. Perform Common Operations

#### Upload an RDL File
```csharp
SSRSResult result = ssrsMethods.UploadRdlFile("ReportName", "/", @"C:\Path\To\YourReport.rdl");
Console.WriteLine(result.Message);
```

#### Create a Data Source with Credentials
```csharp
SSRSResult result = ssrsMethods.CreateDataSourceWithCredential(
    "DataSourceName",
    "Data Source=YourServer;Initial Catalog=YourDatabase;",
    "dbUsername",
    "dbPassword",
    "/"
);
Console.WriteLine(result.Message);
```

#### set username,password and connection string to a Report
```csharp

SSRSResult result = ssrsMethods.SetCustomDataSourceToReport(
"/SalesAmarForoshGorohForosh",
   "DataSource",
   "data source=dataSourceName;initial catalog=databaseName",
   "databaseUsername",
   "databasePassword"
);
Console.WriteLine(result.Message);
```


#### Assign a Data Source to a Report
```csharp
SSRSResult result = ssrsMethods.SetSharedDataSourceToReport(
   "/DataSourceName",
   "/ReportName",
   "DataSourceNameInReport"
);
Console.WriteLine(result.Message);
```

#### Update Report Parameters
```csharp
SSRSResult result = ssrsMethods.UpdateReportParameters(
    "/ReportName",
    new List<string> { "Parameter1", "Parameter2" },
    new List<string> { "Enter Value 1", "Enter Value 2" },
    new List<string> { "DefaultValue1", "DefaultValue2" }
);
Console.WriteLine(result.Message);
```

#### List Data Sources of a Report
```csharp
SSRSResult result = ssrsMethods.GetReportDataSources("/SalesAmarForoshGorohForosh");
if (result.Status == ResultEnum.Success)
{
   foreach (string item in (List<string>)result.Data)
   {
       Console.WriteLine($"item: {item}");
   }
}
else
{
   Console.WriteLine(result.Message);
}
```

#### List Data Children of a server (folder)
```csharp
SSRSResult result = ssrsMethods.ListChildren();
if (result.Status == ResultEnum.Success)
{
	foreach (CatalogItem item in (List<CatalogItem>)result.Data)
	{
		Console.WriteLine($"item: {item.Name + " - " + item.TypeName}");
	}
}
else
{
	Console.WriteLine(result.Message);
}
```

### Folder Structure
```csharp
DynamicSSRS/
│
├── WWWPDynamicSSRS/        # Main library
│   ├── SSRSMethods.cs      # Core logic for interacting with SSRS
│   ├── SSRSResult.cs       # Result model for operations
│   ├── DynamicSSRSClient.cs # Communication with SSRS web services
│
├── WebFormsDemo/           # Sample WebForms project demonstrating usage
│   ├── Default.aspx        # Front-end UI for SSRS operations
│   ├── SSRSHandler.aspx    # Backend handler for SSRS API
│
└── README.md               # Project documentation

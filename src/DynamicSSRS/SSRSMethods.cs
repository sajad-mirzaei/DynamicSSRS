using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DynamicSSRS
{
    public class SSRSMethods
    {
        private WWWPReportingService2010 rsClient;
        public SSRSMethods(string serverUrl, string userName, string password, string domain)
        {
            rsClient = new Authorize().Connect(serverUrl, userName, password, domain);
        }

        public SSRSResult ListChildren(string folderPath = "/", string typeName = "")
        {
            try
            {
                List<CatalogItem> items = rsClient.ListChildren(folderPath, false).ToList();
                List<CatalogItem> data = new List<CatalogItem>();
                foreach (var item in items)
                {
                    if (typeName == "")
                        data.Add(item);
                    else if (item.TypeName == typeName)
                        data.Add(item);
                }

                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Connected successfully.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult CreateFolder(string folderName, string folderPath = "/")
        {
            try
            {
                rsClient.CreateFolder(folderName, folderPath, null);
                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Created successfully."
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult CreateDataSource(string name, string connectString, string parentFolder = "/")
        {
            DataSourceDefinition definition = new DataSourceDefinition();
            definition.CredentialRetrieval =
                CredentialRetrievalEnum.Integrated;
            definition.ConnectString = connectString; //"data source=(local);initial catalog=AdventureWorks";
            definition.Enabled = true;
            definition.EnabledSpecified = true;
            definition.Extension = "SQL";
            definition.ImpersonateUserSpecified = false;
            //Use the default prompt string.
            definition.Prompt = null;
            definition.WindowsCredentials = false;

            try
            {
                rsClient.CreateDataSource(name, parentFolder, false, definition, null);
                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "DataSource Connected successfully."
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult CreateDataSourceWithCredential(string name, string connectString, string userName, string password, string parentFolder = "/")
        {
            bool overwrite = true;
            DataSourceDefinition dataSourceDefinition = new DataSourceDefinition
            {
                CredentialRetrieval = CredentialRetrievalEnum.Store,
                ConnectString = connectString, // "Data Source=SERVER-SQL;Initial Catalog=SAP-Kadbanoo;"
                Enabled = true,
                EnabledSpecified = true,
                Extension = "SQL",
                ImpersonateUser = false,
                ImpersonateUserSpecified = true,
                WindowsCredentials = false,
                UserName = userName,
                Password = password
            };

            try
            {
                rsClient.CreateDataSource(
                    name,
                    parentFolder,
                    overwrite,
                    dataSourceDefinition,
                    null);
                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Connected successfully."
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult SetSharedDataSourceToReport(string dataSourceFullPath, string reportPath, string dataSourceNameInTheReport = "DataSource")
        {
            //example: reportPath = "/ReportTest1", dataSourceName = "/MyDataSource2"
            try
            {
                DataSourceReference dataSourceReference = new DataSourceReference
                {
                    Reference = dataSourceFullPath // full DataSource path
                };

                // define DataSource for report
                DataSource[] dataSources = new DataSource[]
                {
                    new DataSource
                    {
                        Name = dataSourceNameInTheReport, //It must be the exact name of the DataSource defined in the RDL file
                        Item = dataSourceReference
                    }
                };
                rsClient.SetItemDataSources(reportPath, dataSources);
                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "asigned successfully."
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult UploadRdlFile(string reportName, string targetFolder, string filePath, string description = "Uploaded via API")
        {
            Warning[] warnings;
            try
            {
                byte[] rdlFileContents = File.ReadAllBytes(filePath);
                CatalogItem report = rsClient.CreateCatalogItem(
                    "Report",         // نوع آیتم (گزارش)
                    reportName,       // نام گزارش در سرور
                    targetFolder,     // مسیر پوشه هدف
                    true,             // Overwrite (اگر گزارش وجود داشت، جایگزین شود)
                    rdlFileContents,  // محتوای فایل RDL
                    null,             // هیچ property خاصی ندارد
                    out warnings      // هشدارها (در صورت وجود)
                );

                if (warnings != null && warnings.Length > 0)
                {
                    foreach (var warning in warnings)
                    {
                        Console.WriteLine("Warning: {warning.Message}");
                    }
                }

                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Uploaded successfully.",
                    Data = warnings
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult UpdateReportParameters(string reportPath, List<string> parameters, List<string> prompts, List<string> values)
        {
            try
            {
                ItemParameter[] existingParameters = rsClient.GetItemParameters(reportPath, null, false, null, null);

                var parametersToUpdate = new List<ItemParameter>();

                var msg = "";
                for (int i = 0; i < parameters.Count; i++)
                {

                    var parameter = Array.Find(existingParameters, p => p.Name == parameters[i]);
                    if (parameter != null)
                    {
                        parameter.Prompt = prompts[i]; // تنظیم متن Prompt
                        parameter.PromptUser = true; // نمایش پارامتر به کاربر
                        parameter.PromptUserSpecified = true;

                        parameter.DefaultValues = new string[] { values[i] };
                        /*if (paramName == "ServerName")
                            parameter.DefaultValues = new string[] { "Server-Sql" }; // مقدار پیش‌فرض مشخص
                        else if (paramName == "DatabaseName")
                            parameter.DefaultValues = new string[] { "SAP-Kadbanoo" };*/

                        parameter.DefaultValuesQueryBased = false;
                        parameter.DefaultValuesQueryBasedSpecified = true;

                        parametersToUpdate.Add(parameter);
                    }
                    else
                    {
                        msg = "Parameter " + parameters[i] + " not found.";
                    }
                }

                if (parametersToUpdate.Count > 0)
                {
                    rsClient.SetItemParameters(reportPath, parametersToUpdate.ToArray());
                    msg += " Parameters updated successfully.";
                }
                else
                {
                    msg += " No parameters updated.";
                }
                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = msg
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult GetReportParameters(string reportPath = "/ReportTest1")
        {
            ItemParameter[] existingParameters = null;

            try
            {
                existingParameters = rsClient.GetItemParameters(reportPath, null, false, null, null);

                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Get Reports successfully.",
                    Data = existingParameters
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult SetCustomDataSourceToReport(
            string reportPath,
            string dataSourceName,
            string connectionString,
            string username,
            string password)
        {
            try
            {
                // تعریف DataSourceDefinition
                DataSourceDefinition dataSourceDefinition = new DataSourceDefinition
                {
                    CredentialRetrieval = CredentialRetrievalEnum.Store, // ذخیره اعتبارنامه‌ها
                    ConnectString = connectionString,                   // رشته اتصال
                    Enabled = true,
                    EnabledSpecified = true,
                    Extension = "SQL",                                  // نوع Data Source (SQL)
                    ImpersonateUser = false,
                    ImpersonateUserSpecified = true,
                    WindowsCredentials = false,
                    UserName = username,                                // نام کاربری
                    Password = password                                 // رمز عبور
                };

                // تنظیم DataSource
                DataSource[] dataSources = new DataSource[]
                {
            new DataSource
            {
                Name = dataSourceName,   // نام Data Source در گزارش RDL
                Item = dataSourceDefinition
            }
                };

                // اعمال تغییرات به گزارش
                rsClient.SetItemDataSources(reportPath, dataSources);

                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Data source for report " + reportPath + " updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error updating data source: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult GetReportDataSources(string reportPath)
        {
            try
            {
                // دریافت Data Sources
                var dataSources = rsClient.GetItemDataSources(reportPath);

                if (dataSources == null || dataSources.Length == 0)
                {
                    return new SSRSResult
                    {
                        Status = ResultEnum.NotFound,
                        Message = "No data sources found for the report " + reportPath,
                        Data = null
                    };
                }

                // استخراج لیست نام Data Sources
                List<string> dataSourceNames = dataSources.Select(ds => ds.Name).ToList();

                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Data sources retrieved successfully.",
                    Data = dataSourceNames
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error retrieving data sources: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult UpdateReportParametersWithoutValues(string reportPath, List<string> parameters, List<string> prompts)
        {
            try
            {
                ItemParameter[] existingParameters = rsClient.GetItemParameters(reportPath, null, false, null, null);

                var parametersToUpdate = new List<ItemParameter>();

                var msg = "";
                for (int i = 0; i < parameters.Count; i++)
                {

                    var parameter = Array.Find(existingParameters, p => p.Name == parameters[i]);
                    if (parameter != null)
                    {
                        parameter.Prompt = prompts[i]; // تنظیم متن Prompt
                        parameter.PromptUser = true; // نمایش پارامتر به کاربر
                        parameter.PromptUserSpecified = true;

                        //parameter.DefaultValues = new string[] { values[i] };
                        /*if (paramName == "ServerName")
                            parameter.DefaultValues = new string[] { "Server-Sql" }; // مقدار پیش‌فرض مشخص
                        else if (paramName == "DatabaseName")
                            parameter.DefaultValues = new string[] { "SAP-Kadbanoo" };*/

                        parameter.DefaultValuesQueryBased = true;
                        parameter.DefaultValuesQueryBasedSpecified = false;

                        parametersToUpdate.Add(parameter);
                    }
                    else
                    {
                        msg = "Parameter " + parameters[i] + " not found.";
                    }
                }

                if (parametersToUpdate.Count > 0)
                {
                    rsClient.SetItemParameters(reportPath, parametersToUpdate.ToArray());
                    msg += " Parameters updated successfully.";
                }
                else
                {
                    msg += " No parameters updated.";
                }
                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = msg
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult SetCustomDataSourceToReportbyConnectString(
            string reportPath,
            string dataSourceName,
            string connectionString,
            string username,
            string password)
        {
            try
            {
                // تعریف DataSourceDefinition
                DataSourceDefinition dataSourceDefinition = new DataSourceDefinition
                {
                    CredentialRetrieval = CredentialRetrievalEnum.Store, // ذخیره اعتبارنامه‌ها
                    ConnectString = connectionString,                   // رشته اتصال
                    Enabled = true,
                    EnabledSpecified = true,
                    Extension = "SQL",                                  // نوع Data Source (SQL)
                    ImpersonateUser = false,
                    ImpersonateUserSpecified = true,
                    WindowsCredentials = false,
                    UserName = username,                                // نام کاربری
                    Password = password                                 // رمز عبور
                };

                // تنظیم DataSource
                DataSource[] dataSources = new DataSource[]
                {
                    new DataSource
                    {
                        Name = dataSourceName,   // نام Data Source در گزارش RDL
                        Item = dataSourceDefinition
                    }
                };

                // اعمال تغییرات به گزارش
                rsClient.SetItemDataSources(reportPath, dataSources);

                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Data source for report " + reportPath + " updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error updating data source: " + ex.Message,
                    Data = null
                };
            }
        }

        public SSRSResult SetCustomDataSourceToReportWithoutConnectString(
            string reportPath,
            string dataSourceName,
            string username,
            string password)
        {
            try
            {
                // تعریف DataSourceDefinition
                DataSourceDefinition dataSourceDefinition = new DataSourceDefinition
                {
                    CredentialRetrieval = CredentialRetrievalEnum.Store, // ذخیره اعتبارنامه‌ها
                    UseOriginalConnectString = true,
                    Enabled = true,
                    EnabledSpecified = true,
                    Extension = "SQL",                                  // نوع Data Source (SQL)
                    ImpersonateUser = false,
                    ImpersonateUserSpecified = true,
                    WindowsCredentials = false,
                    UserName = username,                                // نام کاربری
                    Password = password                                 // رمز عبور
                };

                // تنظیم DataSource
                DataSource[] dataSources = new DataSource[]
                {
                    new DataSource
                    {
                        Name = dataSourceName,   // نام Data Source در گزارش RDL
                        Item = dataSourceDefinition
                    }
                };

                // اعمال تغییرات به گزارش
                rsClient.SetItemDataSources(reportPath, dataSources);

                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Data source for report " + reportPath + " updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "Error updating data source: " + ex.Message,
                    Data = null
                };
            }
        }

    }
}
using DynamicSSRS;
using System.Collections.Generic;
using System.IO;

public partial class SSRSUploader : System.Web.UI.Page
{
    public SSRSResult GetForm()
    {
        var reportsFolderPathFirst = ReportsFolderPath.Text;
        var rdlFolderPath = RdlFolderPath.Text;
        var serverUrl = ServerUrl.Text;
        var serverUsername = ServerUsername.Text;
        var serverPassword = ServerPassword.Text;
        var dataSourceUsername = DataSourceUsername.Text;
        var dataSourcePassword = DataSourcePassword.Text;

        if (!Directory.Exists(rdlFolderPath))
        {
            return new SSRSResult
            {
                Status = ResultEnum.Error,
                Message = "Invalid folder path! " + rdlFolderPath
            };
        }

        var rdlFiles = Directory.GetFiles(rdlFolderPath, "*.rdl");
        if (rdlFiles.Length == 0)
        {
            return new SSRSResult
            {
                Status = ResultEnum.Error,
                Message = "No RDL files found in the specified folder! " + rdlFolderPath
            };
        }

        var ssrsMethods = new SSRSMethods(serverUrl, serverUsername, serverPassword, "");
        var reportsFolderPath = ssrsMethods.CreateFolderIfNotExist(reportsFolderPathFirst);

        foreach (var filePath in rdlFiles)
        {
            var reportName = Path.GetFileNameWithoutExtension(filePath);

            var uploadResult = ssrsMethods.UploadRdlFile(reportName, reportsFolderPath.Data.ToString(), filePath);
            if (uploadResult.Status != ResultEnum.Success)
            {
                uploadResult.Message = uploadResult.Message + " " + filePath;
                return uploadResult;
            }

            var reportDataSourceName = "";
            var resultGetDS = ssrsMethods.GetReportDataSources(reportsFolderPath.Data.ToString().TrimEnd('/') + "/" + reportName);
            if (resultGetDS.Status == ResultEnum.Success && resultGetDS.Data != null && ((List<string>)resultGetDS.Data).Count > 0)
            {
                reportDataSourceName = ((List<string>)resultGetDS.Data)[0];
            }
            else
            {
                resultGetDS.Message = resultGetDS.Message + " " + reportsFolderPath.Data.ToString().TrimEnd('/') + "/" + reportName;
                return resultGetDS;
            }

            var dataSourceResult = ssrsMethods.SetCustomDataSourceToReportWithoutConnectString(
                reportsFolderPath.Data.ToString().TrimEnd('/') + "/" + reportName,
                reportDataSourceName,
                dataSourceUsername,
                dataSourcePassword);
            if (dataSourceResult.Status != ResultEnum.Success)
            {
                resultGetDS.Message = resultGetDS.Message + " " + filePath;
                return dataSourceResult;
            }
        }

        return new SSRSResult { Status = ResultEnum.Success, Message = "All RDL files processed successfully!" };
    }

    protected void SubmitForm_Click(object sender, System.EventArgs e)
    {
        GetForm();
    }
}
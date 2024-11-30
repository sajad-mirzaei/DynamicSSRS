using DynamicSSRS;
using System.Collections.Generic;
using System.IO;

public partial class UploadReportsSharedDataSource : System.Web.UI.Page
{
    public SSRSResult GetForm()
    {
        var reportsFolderPathFirst = ReportsFolderPath.Text;
        var rdlFolderPath = RdlFolderPath.Text;
        var serverUrl = ServerUrl.Text;
        var serverUsername = ServerUsername.Text;
        var serverPassword = ServerPassword.Text;
        var newDataSourceName = NewDataSourceName.Text;
        var newDataSourceConnectionString = NewDataSourceConnectionString.Text;
        var newDataSourceUsername = NewDataSourceUsername.Text;
        var newDataSourcePassword = NewDataSourcePassword.Text;
        var newDataSourceParentFolder = NewDataSourceParentFolder.Text;

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

            var resultCreateDataSource = ssrsMethods.CreateDataSourceWithCredential(
                newDataSourceName,
                newDataSourceConnectionString,
                newDataSourceUsername,
                newDataSourcePassword,
                newDataSourceParentFolder
                );
            if (resultCreateDataSource.Status != ResultEnum.Success)
            {
                resultCreateDataSource.Message = resultCreateDataSource.Message + " " + filePath;
                return resultCreateDataSource;
            }

            var reportPath = reportsFolderPath.Data.ToString().TrimEnd('/') + "/" + reportName;
            var dataSourcePath = newDataSourceParentFolder.TrimEnd('/') + "/" + newDataSourceName;
            var dataSourceResult = ssrsMethods.SetSharedDataSourceToReport(
                dataSourcePath,
                reportPath,
                reportDataSourceName);
            if (dataSourceResult.Status != ResultEnum.Success)
            {
                dataSourceResult.Message = dataSourceResult.Message + " " + filePath;
                return dataSourceResult;
            }
        }

        return new SSRSResult { Status = ResultEnum.Success, Message = "All RDL files processed successfully!" };
    }

    protected void SubmitForm_Click(object sender, System.EventArgs e)
    {
        var result = GetForm();
        if (result.Status == ResultEnum.Success)
        {
            divAlertDanger.Visible = false;
            divAlertSuccess.Visible = true;
            divAlertSuccess.InnerText = result.Message;
        }
        else
        {
            divAlertSuccess.Visible = false;
            divAlertDanger.Visible = true;
            divAlertDanger.InnerText = result.Message;
        }
    }
}
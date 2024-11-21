
using System;
using System.Web.Services.Protocols;

namespace DynamicSSRS
{
    internal class Authorize
    {
        private WWWPReportingService2010 rsClient;
        public WWWPReportingService2010 Connect(string serverUrl, string userName, string password, string domain)
        {
            rsClient = new WWWPReportingService2010(serverUrl);
            SetCredentialsByUserPass(serverUrl, userName, password, domain); //or SetCredentialsWindows
            TestConnection();
            return rsClient;
        }
        private void SetCredentialsByUserPass(string serverUrl, string userName, string password, string domain)
        {
            rsClient.Credentials = new System.Net.NetworkCredential(userName, password, domain);
        }
        private void SetCredentialsWindows(string serverUrl, string userName, string password, string domain)
        {
            rsClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        }
        private SSRSResult TestConnection()
        {
            try
            {
                CatalogItem[] items = rsClient.ListChildren("/", false);
                return new SSRSResult
                {
                    Status = ResultEnum.Success,
                    Message = "Connected successfully."
                };
            }
            catch (UnauthorizedAccessException)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.Unauthorized,
                    Message = "Access denied. Please check your credentials."
                };
            }
            catch (TimeoutException)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.Timeout,
                    Message = "The request timed out. Please try again later."
                };
            }
            catch (SoapException ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.ServerError,
                    Message = "SOAP error: " + ex.Detail.InnerText,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new SSRSResult
                {
                    Status = ResultEnum.Error,
                    Message = "An unexpected error occurred: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
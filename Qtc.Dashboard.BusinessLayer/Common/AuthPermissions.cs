using Qtc.Dashboard.BusinessLayer.ApiEntityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Qtc.Dashboard.BusinessLayer.Common
{
    public class AuthPermissions
    {
        public static AuthUserPermissions GetUserPermissionsWithMrPermissions(string user)
        {
            //Hard code permissions
            AuthUserPermissions authUserPermissions = new AuthUserPermissions();

            List<RolePermission> role = new List<RolePermission>();

            List<string> permissions = new List<string>
            {
                AuthorizationConstants.MedicalRecordsAccess,
                //AuthorizationConstants.UploadMrFile,
                //AuthorizationConstants.ScanMrFile
            };

            role.Add(new RolePermission { Name = AuthorizationConstants.MedicalRecordsRoleName, Permissions = permissions });

            authUserPermissions.User = user;
            authUserPermissions.Application = "Dashboard";
            authUserPermissions.Roles = role;

            return authUserPermissions;
        }
        public static AuthUserPermissions GetUserPermissions(string user, string password)
        {
            //Get the data to AuthrizationApi
            string baseApiUrl = "";
            string appName = "Dashboard";
            var permissionUrl = $"{baseApiUrl}/{user}/{appName}";
            string error = string.Empty;

            ApiClient client = null;

            //Hard code permissions
            AuthUserPermissions response = GetUserPermissionsWithMrPermissions(user);

            try
            {

                client = new ApiClient(null)
                {
                    Credentials = System.Net.CredentialCache.DefaultCredentials
                };

                return response;

                byte[] responseBytes = null;
                responseBytes = client.DownloadData(permissionUrl);

                if (responseBytes != null && responseBytes.Length > 0)
                {
                    //Get the response 
                    String strJsonResponse = Encoding.UTF8.GetString(responseBytes);

                    //convert the response into object AuthUserClaims  
                    //response = Json.JsonConvert.DeserializeObject<AuthUserPermissions>(strJsonResponse);
                }
            }
            catch (WebException webExcp)
            {
                string responseString = "";
                // Get the WebException status code.
                WebExceptionStatus status = webExcp.Status;
                // If status is WebExceptionStatus.ProtocolError, 
                // there has been a protocol error and a WebResponse 
                // should exist.
                if (status == WebExceptionStatus.ProtocolError)
                {
                    //"The server returned protocol error "
                    // Get HttpWebResponse so that you can check the HTTP status code.
                    HttpWebResponse httpResponse = (HttpWebResponse)webExcp.Response;
                    Stream stream = httpResponse.GetResponseStream();
                    responseString = new StreamReader(stream).ReadToEnd();

                    if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                        throw new UnauthorizedAccessException(httpResponse.StatusCode + ": " + "No access to permission API.");

                    if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                    //throw new ApplicationException(httpResponse.StatusCode.ToString());
                    {
                        AuthUserPermissions permisstions = new AuthUserPermissions();
                        List<RolePermission> role = new List<RolePermission>();
                        List<string> permission = new List<string>
                        {
                            AuthorizationConstants.NoPermission
                        };
                        role.Add(new RolePermission { Name = AuthorizationConstants.GuestUser, Permissions = permission });

                        permisstions.User = user;
                        permisstions.Application = appName;
                        permisstions.Roles = role;

                        return permisstions;

                    }
                }
                else
                {
                    error = webExcp.Message;
                    throw new ApplicationException(webExcp.Message);
                }
                return null;
            }
            catch (Exception)
            {
                //  The remote server returned an error: (401) Unauthorized. 
                //	NotFound (404) – The User/Application combination was not found in the Authorization data store.
                // BadRequest (400) – This is an error state and will be logged in the Authorization.Log table.

                throw;
            }
            finally
            {
                client.Dispose();
            }
            return response;
        }
    }

    public class ApiClient : WebClient
    {
        private readonly X509Certificate2 qtcCert;

        public ApiClient(X509Certificate2 cert)
        {
            qtcCert = cert;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = null;

            try
            {
                request = (HttpWebRequest)base.GetWebRequest(address);

                var connectionTimeOut = "300";
                int seconds;
                if (!int.TryParse(connectionTimeOut, out seconds))
                    seconds = 300;

                request.Timeout = seconds * 1000;
                request.Proxy = null;
                request.AllowAutoRedirect = true;
                request.UserAgent = "User-Agent: Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) Gecko/20100101 Firefox/8.0";
                request.KeepAlive = false;

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                        SecurityProtocolType.Tls11 |
                                                        SecurityProtocolType.Tls12 |
                                                        SecurityProtocolType.Ssl3;
                ServicePointManager.UseNagleAlgorithm = true;
                ServicePointManager.EnableDnsRoundRobin = true;
                ServicePointManager.ServerCertificateValidationCallback =
                        delegate (Object obj, X509Certificate X509certificate, X509Chain chain, SslPolicyErrors errors)
                        {
                            return true;
                        };

                if (qtcCert is X509Certificate2)
                    request.ClientCertificates.Add(qtcCert);

                return request;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
            }
        }
    }
}

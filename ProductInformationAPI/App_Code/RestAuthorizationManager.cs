using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

/// <summary>
/// Summary description for RestAuthorizationManager
/// </summary>
public class RestAuthorizationManager : ServiceAuthorizationManager
{
    protected override bool CheckAccessCore(OperationContext operationContext)
    {
        //Extract the Authorization header, and parse out the credentials converting the Base64 string:
        var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];

        if ((authHeader != null) && (authHeader != string.Empty))
        {
            var svcCredentials = System.Text.ASCIIEncoding.ASCII
                    .GetString(Convert.FromBase64String(authHeader.Substring(6))).Split(':');

            var user = new { Name = svcCredentials[0], Password = svcCredentials[1] };

            if ((user.Name == "MyUserName" && user.Password == "MyPassword"))
            {
                //User is authrized and originating call will proceed
                return true;
            }
            else
            {
                //not authorized
                return false;
            }
        }
        return false;
    }
}
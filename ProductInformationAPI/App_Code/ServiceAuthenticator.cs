using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ServiceAuthenticator
/// </summary>
public class ServiceAuthenticator : UserNamePasswordValidator
{
    public override void Validate(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentNullException("UserName");
        }
        else if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentNullException("password");
        }

        else if (userName!="MyUserName" || password!="MyPassword")
        {
            throw new UnauthorizedAccessException("Incorrect User Name or Password");
        }
    }
}
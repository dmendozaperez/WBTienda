using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
namespace WB_ClienteBata.Bll
{
    public class Utilities
    {
        public static void logout(HttpSessionState session, System.Web.HttpResponse response)
        {
            string url = "~/Comunicado/Control/LoginForm.aspx";//FormsAuthentication.LoginUrl;
            session.Clear();
            session.Abandon();
            FormsAuthentication.SignOut();
            response.Redirect(url, true);
        }
    }
}
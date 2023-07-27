using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace PerspectivaCliente
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }
    }
    //public class Global : System.Web.HttpApplication
    //{
    //    protected void Application_BeginRequest(object sender, EventArgs e)
    //    {
    //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "http://127.0.0.1:8000");
    //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
    //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
    //        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
    //    }
    //}
}
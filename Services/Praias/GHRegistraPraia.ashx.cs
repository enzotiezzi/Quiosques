using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Tell_you_story.Models;

namespace Tell_you_story.Services.Praias
{
    /// <summary>
    /// Summary description for GHRegistraPraia
    /// </summary>
    public class GHRegistraPraia : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.TrySkipIisCustomErrors = true;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            Praia praia = JsonUtils.Deserialize<Praia>(context);

            DBClass db = new DBClass();
            bool response = db.RegistraPraia(praia);

            context.Response.Write(response);
            context.Response.StatusCode = 200;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
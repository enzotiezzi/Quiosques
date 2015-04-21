using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Tell_you_story.Models;

namespace Tell_you_story.Services.Praias
{
    /// <summary>
    /// Summary description for GHRegistrarLitoral
    /// </summary>
    public class GHRegistrarLitoral : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.TrySkipIisCustomErrors = true;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            Litoral litoral = JsonUtils.Deserialize<Litoral>(context);

            DBClass db = new DBClass();
            bool response = db.RegistraLitoral(litoral);

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Tell_you_story.Models;
using System.Web.Script.Serialization;

namespace Tell_you_story.Services.Praias
{
    /// <summary>
    /// Summary description for GHListaPraias
    /// </summary>
    public class GHListaPraias : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.TrySkipIisCustomErrors = true;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            int id = int.Parse(context.Request.QueryString["id"]);

            DBClass db = new DBClass();
            List<Praia> praias = db.ListaPraias(id);

            String json = new JavaScriptSerializer().Serialize(praias);

            context.Response.Write(json);
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
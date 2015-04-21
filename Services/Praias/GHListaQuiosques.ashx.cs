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
    /// Summary description for GHListaQuiosques
    /// </summary>
    public class GHListaQuiosques : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.TrySkipIisCustomErrors = true;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            int id = int.Parse(context.Request.QueryString["id"]);

            DBClass db = new DBClass();
            List<Quiosque> quiosques = db.ListaQuiosques(id);

            String json = new JavaScriptSerializer().Serialize(quiosques);

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
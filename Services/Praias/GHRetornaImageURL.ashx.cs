using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Tell_you_story.Services.Praias
{
    /// <summary>
    /// Summary description for GHRetornaImageURL
    /// </summary>
    public class GHRetornaImageURL : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.TrySkipIisCustomErrors = true;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            int id = int.Parse(context.Request.QueryString["id"]);

            DBClass db = new DBClass();
            String ImageURL = db.RetornaImageURL(id);

            context.Response.Write(ImageURL);
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
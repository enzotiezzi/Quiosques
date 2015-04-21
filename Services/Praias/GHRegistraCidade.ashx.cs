using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Tell_you_story.Models;

namespace Tell_you_story.Services.Praias
{
    /// <summary>
    /// Summary description for GHRegistraCidade
    /// </summary>
    public class GHRegistraCidade : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.TrySkipIisCustomErrors = true;
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            Cidade cidade = JsonUtils.Deserialize<Cidade>(context);

            DBClass db = new DBClass();
            bool response = db.RegistraCidade(cidade);

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
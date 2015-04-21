using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Tell_you_story
{
    class JsonUtils
    {
        public static T Deserialize<T>(HttpContext context)
        {
            String json = new StreamReader(context.Request.InputStream).ReadToEnd();

            var obj = (T)new JavaScriptSerializer().Deserialize<T>(json);

            return obj;
        }
    }
}

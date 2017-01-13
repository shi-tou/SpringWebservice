using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Shitou.WebService
{
    /// <summary>
    /// 定义安全/验证信息
    /// </summary>
    public abstract class SecureWebService
    {
        public AuthenticationSoapHeader AuthenticationHeader
        {
            get { return HttpContext.Current.Items["AuthenticationSoapHeader"] as AuthenticationSoapHeader; }
            set { HttpContext.Current.Items["AuthenticationSoapHeader"] = value; }
        }

        public SecureWebService()
        {
        }
    }
}

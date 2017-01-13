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
using System.Web.Services.Protocols;

namespace Shitou.WebService
{
    /// <summary>
    /// Soap头（可存放一些安全/验证信息）
    /// </summary>
    public class AuthenticationSoapHeader : SoapHeader
    {
        private string _lang = "";
        private string _token;
        /// <summary>
        /// 交互令牌
        /// </summary>
        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}

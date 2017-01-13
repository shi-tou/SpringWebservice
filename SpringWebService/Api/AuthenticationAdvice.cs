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
using System.Web.Services;
using AopAlliance.Intercept;
using Shitou.Common;
using Newtonsoft.Json;

namespace Shitou.WebService
{
    /// <summary>
    /// 自定议Spring的拦截器
    /// </summary>
    public class AuthenticationAdvice : IMethodInterceptor
    {
        [WebMethod(EnableSession = true)]
        public object Invoke(IMethodInvocation invocation)
        {
            // Check if we are using it in a Web Service
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("This advice should be applied to a Web Service.");
            }

            //方法名
            String name = invocation.Method.Name;
            //参数
            object[] args = invocation.Arguments;
            //记录日志
            //LogHelper.Info(string.Format("MethodName->{0}\r\n Argument->{1}", name, JsonConvert.SerializeObject(args)));
            #region 验证Token
            // Authentication
            //AuthenticationSoapHeader ash = HttpContext.Current.Items["AuthenticationSoapHeader"] as AuthenticationSoapHeader;
            //if (ash == null || string.IsNullOrEmpty(ash.Token))
            //{
            //    throw new UnauthorizedAccessException("未授权的访问!");
            //}
            #endregion

            return invocation.Proceed();
        }
    }
}

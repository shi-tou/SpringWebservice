using Shitou.Common;
using Shitou.Task;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Shitou.WebService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //============方式1、单独配置文件=============
            log4net.Config.XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Server.MapPath("/Config/log4net.config")));
            //============方式2、配置到web.config=========
            //log4net.Config.XmlConfigurator.Configure();
            //开启任务
            TaskService.Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            string url = Request.Url.ToString();
            if (ex.GetType().Name.Equals("HttpException"))
            {
                HttpException httpEx = (HttpException)ex;
                int httpCode = httpEx.GetHttpCode();
                if (httpCode == 404)
                {
                    return;
                }
            }
            string errorDesc = "错误消息：" + ex.Message +
                              "\r\n发生时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
                              "\r\n错误源： " + ex.Source +
                              "\r\n站点地址：" + url +
                              "\r\n引发异常的方法： " + ex.TargetSite +
                              "\r\n堆栈信息： " + ex.StackTrace;
            this.Context.Server.ClearError();

            LogHelper.Error("系统出现异常:\r\n" + errorDesc);
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Response.Redirect("/webservice.asmx");
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
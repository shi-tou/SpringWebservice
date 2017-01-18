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

            LogManager.Error("系统出现异常:\r\n" + errorDesc);
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //解决IIS应用程序池自动回收的问题  
            System.Threading.Thread.Sleep(1000);
            //触发事件, 写入提示信息  
            LogManager.Info("触发Application_End事件,正在重新启动网站:" + DateTime.Now);
            //设置站内任意一个页面甚至不存在的页面，目的是要激发Application_Start  
            string url = Request.Url.ToString();
            System.Net.HttpWebRequest myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse myHttpWebResponse = (System.Net.HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流  
        }
    }
}
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Shitou.Common
{
    public class LogManager
    {
        #region 属性
        /// <summary>
        /// 日志对象
        /// </summary>
        private static ILog log;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        static LogManager()
        {
            //============方式1、单独配置文件=============
            //log4net.Config.XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Server.MapPath("/Config/log4net.config")));
            //============方式2、配置到web.config=========
            //log4net.Config.XmlConfigurator.Configure();
            //以上代码已移至Global.asax。
            log = log4net.LogManager.GetLogger(typeof(LogManager));
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static bool GetConfig(string key)
        {
            try { return Convert.ToBoolean(ConfigurationManager.AppSettings[key].ToString()); }
            catch { return false; }
        }

        #region Debug日志
        public static void Debug(string message)
        {
            log.Debug(message);
        }
        public static void Debug(string message, Exception ex)
        {

            log.Debug(message, ex);
        }
        public static void Debug(string format, object arg0)
        {
            log.Debug(string.Format(format, arg0));
        }
        public static void Debug(string format, params object[] args)
        {
            log.Debug(string.Format(format, args));
        }
        #endregion

        #region Error日志
        public static void Error(string message)
        {
            log.Error(message);
        }
        public static void Error(string message, Exception ex)
        {
            log.Error(message, ex);
        }
        public static void Error(string format, object arg0)
        {
            log.Error(string.Format(format, arg0));
        }
        public static void Error(string format, params object[] args)
        {
            log.Error(string.Format(format, args));
        }
        #endregion

        #region Info日志
        public static void Info(string message)
        {
            log.Info(message);
        }
        public static void Info(string message, Exception ex)
        {
            log.Info(message, ex);
        }
        public static void Info(string format, object arg0)
        {
            log.Info(string.Format(format, arg0));
        }
        public static void Info(string format, params object[] args)
        {
            log.Info(string.Format(format, args));
        }
        #endregion

        #region Warn日志
        public static void Warn(string message)
        {
            log.Warn(message);
        }
        public static void Warn(string message, Exception ex)
        {
            log.Warn(message, ex);
        }
        public static void Warn(string format, object arg0)
        {
            log.Warn(string.Format(format, arg0));
        }
        public static void Warn(string format, params object[] args)
        {
            log.Warn(string.Format(format, args));
        }
        #endregion
    }
}
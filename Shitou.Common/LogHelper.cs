using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Shitou.Common
{
    public class LogHelper
    {
        #region 属性
        /// <summary>
        /// 是否启用Debug日志
        /// </summary>
        private static bool IsDebugEnabled { get { return GetConfig("IsDebugEnabled"); } }
        /// <summary>
        /// 是否启用Error日志
        /// </summary>
        private static bool IsErrorEnabled { get { return GetConfig("IsErrorEnabled"); } }
        /// <summary>
        /// 是否启用Info日志
        /// </summary>
        private static bool IsInfoEnabled { get { return GetConfig("IsInfoEnabled"); } }
        /// <summary>
        /// 是否启用Warn日志
        /// </summary>
        private static bool IsWarnEnabled { get { return GetConfig("IsWarnEnabled"); } }
        /// <summary>
        /// 日志对象
        /// </summary>
        private static ILog log;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        static LogHelper()
        {
            //============方式1、单独配置文件=============
            //log4net.Config.XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Server.MapPath("/Config/log4net.config")));
            //============方式2、配置到web.config=========
            //log4net.Config.XmlConfigurator.Configure();
            //以上代码已移至Global.asax。
            log = LogManager.GetLogger(typeof(LogHelper));
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
            if (IsDebugEnabled)
            {
                log.Debug(message);
            }
        }
        public static void Debug(string message, Exception ex)
        {
            if (IsDebugEnabled)
            {
                log.Debug(message, ex);
            }
        }
        public static void Debug(string format, object arg0)
        {
            if (IsDebugEnabled)
            {
                log.Debug(string.Format(format,arg0));
            }
        }
        public static void Debug(string format, params object[] args)
        {
            if (IsDebugEnabled)
            {
                log.Debug(string.Format(format, args));
            }
        }
        #endregion

        #region Error日志
        public static void Error(string message)
        {
            if (IsErrorEnabled)
            {
                log.Error(message);
            }
        }
        public static void Error(string message, Exception ex)
        {
            if (IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }
        public static void Error(string format, object arg0)
        {
            if (IsErrorEnabled)
            {
                log.Error(string.Format(format, arg0));
            }
        }
        public static void Error(string format, params object[] args)
        {
            if (IsErrorEnabled)
            {
                log.Error(string.Format(format, args));
            }
        }
        #endregion

        #region Info日志
        public static void Info(string message)
        {
            if (IsInfoEnabled)
            {
                log.Info(message);
            }
        }
        public static void Info(string message, Exception ex)
        {
            if (IsInfoEnabled)
            {
                log.Info(message, ex);
            }
        }
        public static void Info(string format, object arg0)
        {
            if (IsInfoEnabled)
            {
                log.Info(string.Format(format, arg0));
            }
        }
        public static void Info(string format, params object[] args)
        {
            if (IsInfoEnabled)
            {
                log.Info(string.Format(format, args));
            }
        }
        #endregion

        #region Warn日志
        public static void Warn(string message)
        {
            if (IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        public static void Warn(string message, Exception ex)
        {
            if (IsWarnEnabled)
            {
                log.Warn(message, ex);
            }
        }
        public static void Warn(string format, object arg0)
        {
            if (IsWarnEnabled)
            {
                log.Warn(string.Format(format, arg0));
            }
        }
        public static void Warn(string format, params object[] args)
        {
            if (IsWarnEnabled)
            {
                log.Warn(string.Format(format, args));
            }
        }
        #endregion
    }
}
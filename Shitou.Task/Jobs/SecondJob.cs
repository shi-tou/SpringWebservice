using Common.Logging;
using Quartz;
using Quartz.Impl;
using Shitou.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Task.Jobs
{
    public class SecondJob : IJob
    {
        private static ILog logger;
        static SecondJob()
        {
            logger = global::Common.Logging.LogManager.GetLogger(typeof(SecondJob));
        }
        public void Execute(IJobExecutionContext context)
        {
            JobDetailImpl jobDetail = (JobDetailImpl)context.JobDetail;
            logger.Info(string.Format("--------------SecondJob:{0}----------------", context.FireTimeUtc));
            Log.SaveFileLog("SecondJob", string.Format("--------------SecondJob:{0}----------------", context.FireTimeUtc));
            //Console.WriteLine("SecondJob->Name：" + jobDetail.FullName);
            //Console.WriteLine("SecondJob->Group：" + jobDetail.Group);
            //Console.WriteLine("Trigger类型->：" + context.Trigger.GetType());
        }
    }
}

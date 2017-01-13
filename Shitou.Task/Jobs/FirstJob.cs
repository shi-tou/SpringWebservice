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
    public class FirstJob : IJob
    {
        private static ILog logger;
        static FirstJob()
        {
            logger = global::Common.Logging.LogManager.GetLogger(typeof(FirstJob));
        }
        public void Execute(IJobExecutionContext context)
        {
            JobDetailImpl jobDetail = (JobDetailImpl)context.JobDetail;
            logger.Info(string.Format("--------------FirstJob:{0}----------------", context.FireTimeUtc));
            Log.SaveFileLog("FirstJob", string.Format("--------------FirstJob:{0}----------------", context.FireTimeUtc));
            //Console.WriteLine("FirstJob->Name：" + jobDetail.FullName);
            //Console.WriteLine("FirstJob->Group：" + jobDetail.Group);
            //Console.WriteLine("Trigger类型->：" + context.Trigger.GetType());
        }
    }
}

using Newtonsoft.Json;
using Shitou.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shitou.Task
{
    public class TaskConfig
    {
        public static TaskConfig Instance { get; set; }
        static TaskConfig()
        {
            try
            {
                Instance = JsonConvert.DeserializeObject<TaskConfig>(File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/TaskConfig.json")));
            }
            catch(Exception ex)
            {
                LogHelper.Error("解析定时任务配置文件[Config/TaskSetting.json]出错");
                Instance = null;
            }
        }
        public List<JobItem> Jobs { get; set; }
        public List<TriggerItem> Triggers { get; set; }

    }
}


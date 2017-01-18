﻿using Quartz;
using Quartz.Impl;
using System;
using System.Reflection;
using System.Linq;
using Shitou.Common;

namespace Shitou.Task
{
    /// <summary>
    /// Task服务
    /// </summary>
    public class TaskService
    {
        /// <summary>
        /// 作业调度池
        /// </summary>
        public static IScheduler Scheduler;
        static TaskService()
        {
            //用调度工厂创建作业调度池
            Scheduler = new StdSchedulerFactory().GetScheduler();
        }

        #region 作业调度管理
        /// <summary>
        /// 开始执行
        /// </summary>
        public static void Start()
        {
            Scheduler.Start();
        }
        /// <summary>
        /// 停止执行
        /// </summary>
        public static void Pause()
        {
            Scheduler.PauseAll();
        }
        /// <summary>
        /// 恢复执行
        /// </summary>
        public static void Resume()
        {
            Scheduler.ResumeAll();
        }
        /// <summary>
        /// 线束执行
        /// </summary>
        public static void Stop()
        {
            Scheduler.Shutdown();
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            try
            {
                if (TaskConfig.Instance != null)
                {
                    Type[] types = AssemblyHandler.GetJobClass();
                    foreach (JobItem item in TaskConfig.Instance.Jobs)
                    {
                        if (item.Enabled)
                        {
                            IJobDetail jobDetail = CreateJob(item, types);
                            ICronTrigger trigger = CreateCronTrigger(TaskConfig.Instance.Triggers.Where(a => a.Name == item.Trigger).FirstOrDefault());
                            Scheduler.ScheduleJob(jobDetail, trigger);
                        }
                    }
                    Start();
                }
                else
                {
                    LogManager.Error("定时任务启动失败，TaskConfig.Instance为null。");
                }
            }
            catch(Exception ex)
            {
                LogManager.Error("定时任务启动失败，", ex);
            }
        }
        /// <summary>
        /// 创建Job
        /// </summary>
        /// <param name="jobItem"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IJobDetail CreateJob(JobItem jobItem, Type[] types)
        {
            JobDetailImpl job = new JobDetailImpl(jobItem.Name, types.Where(t => t.FullName == jobItem.Type).FirstOrDefault());
            return job;
        }
        /// <summary>
        /// 创建CronTrigger
        /// </summary>
        /// <returns></returns>
        public static ICronTrigger CreateCronTrigger(TriggerItem triggerItem)
        {
            TriggerBuilder triggerBuilder = TriggerBuilder.Create();
            if (!string.IsNullOrEmpty(triggerItem.StartTime))
            {
                triggerBuilder = triggerBuilder.StartAt(Convert.ToDateTime(triggerItem.StartTime));
            }
            if (!string.IsNullOrEmpty(triggerItem.EndTime))
            {
                triggerBuilder = triggerBuilder.EndAt(Convert.ToDateTime(triggerItem.EndTime));
            }
            triggerBuilder = triggerBuilder.WithCronSchedule(triggerItem.CronExpression);
            ICronTrigger trigger = (ICronTrigger)triggerBuilder
                .WithCronSchedule(triggerItem.CronExpression)
                .WithIdentity(triggerItem.Name, triggerItem.Group)
                .Build();
            return trigger;
        }
    }
}

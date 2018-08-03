using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using WindowsService.Task;

namespace WindowsService
{
    public class QuartzScheduleJobManager
    {
        /// <summary>
        /// 任务调度器
        /// </summary>
        private readonly IScheduler _scheduler;

        /// <summary>
        /// Ctor
        /// </summary>
        public QuartzScheduleJobManager()
        {
            _scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        /// <summary>
        /// 获取任务调度器
        /// </summary> 
        internal virtual IScheduler Scheduler
        {
            get
            {
                return _scheduler;
            }
        }

        /// <summary>
        /// 开启调度线程
        /// </summary>
        public void Start()
        {
            if (_scheduler != null)
            {
                _scheduler.Start();
            }
        }
        /// <summary>
        /// 停止调度
        /// </summary>
        /// <param name="waitForJobsToComplete">是否等待正在执行的任务完成</param>
        /// <returns></returns>
        public void Stop(bool waitForJobsToComplete)
        {
            if (_scheduler != null)
            {
                _scheduler.Shutdown();
            }
        }

        /// <summary>
        /// 注册定时任务
        /// </summary>
        public static void RegisterTask()
        {
            var typeFinder = new TypeFinder();
            //查找定时任务
            var taskTypes = typeFinder.FindClassesOfType<ITask>();
            var taskInstances = new List<ITask>();
            foreach (var taskType in taskTypes)
                taskInstances.Add((ITask)Activator.CreateInstance(taskType));

            var jobManager = new QuartzScheduleJobManager();

            foreach (var item in taskInstances)
            {
                var type = item.GetType();
                IJobDetail job = JobBuilder.Create(type).WithIdentity("job_" + type.Name, "group_" + type.Name).Build();
                ITrigger trigger = item.GetTrigger()
                                   .WithIdentity("trigger_" + type.Name, "group_" + type.Name)
                                   .Build();
                jobManager.Scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}

using NLog;
using Quartz;
using System;

namespace WindowsService.Task
{
    public class TestTask : ITask
    {
        public void Execute(IJobExecutionContext context)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info(string.Format("{0}:{1}", context.JobDetail.Key.Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")));
            LogManager.Flush();
        }

        public TriggerBuilder GetTrigger()
        {
            var trigger = TriggerBuilder.Create()
               .WithCronSchedule("0/5 * * * * ?");
            return trigger;
        }
    }
}

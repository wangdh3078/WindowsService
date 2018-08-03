using Quartz;

namespace WindowsService.Task
{
    public interface ITask : IJob
    {
        /// <summary>
        /// 获取任务定时设置
        /// </summary>
        /// <returns></returns>
        TriggerBuilder GetTrigger();
    }
}

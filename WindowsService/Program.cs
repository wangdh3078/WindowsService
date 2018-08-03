using Topshelf;

namespace WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            QuartzScheduleJobManager.RegisterTask();
            HostFactory.Run(x =>
            {
                x.Service<QuartzScheduleJobManager>(s =>
                {
                    s.ConstructUsing(name => new QuartzScheduleJobManager());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop(true));
                });
                x.RunAsLocalSystem();//表示以本地系统账号运行，可选的还有网络服务和本地服务账号
                x.StartAutomatically();//自动启动
                x.SetDescription("服务测试描述");//描述
                x.SetDisplayName("服务测试-显示名称");//显示名称
                x.SetServiceName("服务测试-服务名称");//服务名称
            });
        }
    }
}

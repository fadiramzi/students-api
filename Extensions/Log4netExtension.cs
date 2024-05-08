using log4net.Config;
using log4net;

namespace StudentsManagerMW.Extensions
{
    public static class Log4netExtension
    {
            public static void AddLog4net(this IServiceCollection services)
            {
                XmlConfigurator.Configure(new FileInfo("log4net.config"));
                services.AddSingleton(LogManager.GetLogger(typeof(Program)));
            }
    }
}

using QuartzJob.QuartzHelper;
using System;

namespace QuartzJob
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("~/App_Data/Log.config")));
            log4net.LogManager.GetLogger("RuningLogger").Error("IIS重启");
            JobManagerBase.ReAddAllJob();
        }
    }
}
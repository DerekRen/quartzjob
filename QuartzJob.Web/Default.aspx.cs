using QuartzJob.QuartzHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //var ss = JobManagerBase.AddJob("MyJob", "1", "1", "0/4 * * * * ?", "http://192.168.1.101:8005/Default2.aspx");
        JobControler qq = new JobControler("MyJob");
        qq.Remove();
    }
}
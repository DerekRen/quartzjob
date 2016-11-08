using QuartzJob.Business;
using QuartzJob.Common;
using QuartzJob.QuartzHelper;
using System;
using System.Web;
using System.Web.UI;

namespace QuartzJob
{
    public partial class JobEdit : System.Web.UI.Page
    {
        public long id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request["id"].ParseToLong();
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitPage()
        {
            var JobData = JobService.CreateInstance().GetJobData(id);
            if (JobData != null)
            {
                TriggerName.Value = JobData.TriggerName;
                TriggerUrl.Value = JobData.TriggerUrl;
                CronExpr.Value = JobData.CronExpr;
                Explain.Value = JobData.Explain;
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            string txtName = TriggerName.Value.TrimEnd();
            string txtUrl = TriggerUrl.Value.TrimEnd();
            string txtCronExpr = CronExpr.Value.TrimEnd();
            string txtExplain = Explain.Value.TrimEnd();
            if (string.IsNullOrEmpty(txtName))
            {
                data += "job名称不能为空";
            }
            if (string.IsNullOrEmpty(txtUrl))
            {
                data += "job地址不能为空";
            }
            if (string.IsNullOrEmpty(txtCronExpr))
            {
                data += "CronExpr表达式不能为空";
            }
            if (string.IsNullOrEmpty(txtExplain))
            {
                data += "job说明不能为空";
            }
            if (!string.IsNullOrEmpty(data))
            {
                Alert(data);
                return;
            }
            if (id > 0)
            {
                var JobData = JobService.CreateInstance().GetJobData(id);
                if (JobData == null) return;
                var isChange = false;
                if (JobData.TriggerName != txtName) isChange = true;
                if (JobData.TriggerUrl != txtUrl) isChange = true;
                if (JobData.CronExpr != txtCronExpr) isChange = true;
                if (JobData.Explain != txtExplain) isChange = true;
                if (!isChange)
                {
                    Alert("无任何变化，无需保存");
                    return;
                }
            }
            long backId = JobService.CreateInstance().SaveJob(txtName, txtUrl, txtCronExpr, txtExplain, id);
            if (backId > 0)
            {
                if (id > 0)
                {
                    JobControler job = new JobControler(TriggerName.Value.TrimEnd());
                    job.Remove();
                }
                if (JobManagerBase.AddJob(txtName, backId.ParseToString(), txtCronExpr, txtUrl))
                {
                    AlertFun("保存成功！", "window.close();opener.QueryDataList(1)");
                    return;
                }
            }
            Alert("保存失败！");
        }

        /// <summary>
        /// 页面加载完成后执行客户端脚本
        /// </summary>
        /// <param name="script">要执行的函数Refresh();...</param>
        public void ExecStartupScript(string script)
        {
            ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as Page, typeof(Page), Guid.NewGuid().ToString(), script, true);
        }
        /// <summary>
        /// /弹出消息
        /// </summary>
        /// <param name="msg">提示信息</param>
        public void Alert(string msg)
        {
            msg = msg.Replace("\r\n", "\\r\\n").Replace("'", "\\'");
            string info = "alert('" + msg + "');";
            ExecStartupScript(info);
        }
        /// <summary>
        /// /弹出消息
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="script">脚本</param>
        public void AlertFun(string msg, string script)
        {
            msg = msg.Replace("\r\n", "\\r\\n").Replace("'", "\\'");
            string info = "alert('" + msg + "');" + script;
            ExecStartupScript(info);
        }
    }
}
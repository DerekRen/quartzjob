using QuartzJob.Business;
using QuartzJob.Common;
using QuartzJob.QuartzHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QuartzJob
{
    /// <summary>
    /// Summary description for AjaxCall
    /// </summary>
    public class AjaxCall : IHttpHandler
    {
        /// <summary>
        /// 请求的Action名称
        /// </summary>
        public static string strActionName = string.Empty;
        public void ProcessRequest(HttpContext context)
        {
            Init(context);
            if (strActionName.Equals("GetSceneryTriggerInfo"))
            {
                GetSceneryTriggerInfo(context);
            }
            else if (strActionName.Equals("SetJobRunStatus"))//设置运行状态
            {
                SetJobRunStatus(context);
            }
            else if (strActionName.Equals("SetRowStatus"))//设置数据状态
            {
                SetRowStatus(context);
            }
            else if (strActionName.Equals("SetRestart"))//重启
            {
                SetRestart(context);
            }
            else
            {
                ResponseAjaxContent("请求Action不存在");
            }
        }

        /// <summary>
        /// 重启
        /// </summary>
        /// <param name="context"></param>
        public void SetRestart(HttpContext context)
        {
            int Id = context.Request["id"].ParseToInt();
            var data = JobService.CreateInstance().GetJobData(Id);
            if (data != null)
            {
                JobManagerBase.Restart(data.TriggerName, data.Id.ParseToString(), data.CronExpr, data.TriggerUrl);
            }
            ResponseAjaxContent("");
        }
        /// <summary>
        /// 数据状态
        /// </summary>
        /// <param name="context"></param>
        public void SetRowStatus(HttpContext context)
        {
            long Id = context.Request["id"].ParseToLong();
            int RowStatus = context.Request["rowStatus"].ParseToInt();
            string TriggerName = context.Request["triggerName"].ParseToString();
            if (!string.IsNullOrEmpty(TriggerName))
            {
                JobControler job = new JobControler(TriggerName);
                if (RowStatus == CommonEnum.ValidStatus.InValid.GetHashCode())
                {
                    job.Remove();
                    JobService.CreateInstance().SetJobRowStatus(Id, RowStatus);
                }
                else
                {
                    var data = JobService.CreateInstance().GetJobData(Id);
                    if (data != null)
                    {
                        JobManagerBase.AddJob(data.TriggerName, data.Id.ParseToString(), data.CronExpr, data.TriggerUrl);
                    }
                }
            }
            ResponseAjaxContent("1");
        }
        /// <summary>
        /// 设置job状态
        /// </summary>
        /// <param name="context"></param>
        public void SetJobRunStatus(HttpContext context)
        {
            int Id = context.Request["id"].ParseToInt();
            int RunStatus = context.Request["runStatus"].ParseToInt();
            string TriggerName = context.Request["triggerName"].ParseToString();
            if (!string.IsNullOrEmpty(TriggerName))
            {
                JobControler job = new JobControler(TriggerName);
                if (RunStatus == JobRunStatus.Run.GetHashCode())
                {
                    job.Start();
                }
                else
                {
                    job.Stop();
                }
            }
            ResponseAjaxContent("1");
        }
        /// <summary>
        /// 获取作业数据
        /// </summary>
        /// <param name="context"></param>
        public void GetSceneryTriggerInfo(HttpContext context)
        {
            JobSelectModel jobSelect = new JobSelectModel()
            {
                PageIndex = context.Request["currentPage"].ParseToInt(),
                PageSize = 1,
                TriggerName = context.Request["TriggerName"].ParseToString(),
                IsValid = context.Request["IfValid"].ParseToInt(),
                RunStaus = context.Request["RunStatus"].ParseToInt(),
            };
            StringBuilder sbhtml = new StringBuilder();
            List<string> tableTH = new List<string>();
            tableTH.Add("width:6%,顺序");
            tableTH.Add("width:15%,Job名称");
            tableTH.Add("width:20%,请求地址");
            tableTH.Add("width:15%,触发时间");
            tableTH.Add("width:15%,请求地址");
            tableTH.Add("width:8%,数据状态");
            tableTH.Add("width:8%,运行状态");
            tableTH.Add("width:18%,操作");
            var dataList = JobService.CreateInstance().GetJobDataList(jobSelect);
            if (!dataList.IsNullOrEmpty())
            {
                int i = 0;
                dataList.ForEach(q =>
                {
                    i++;
                    sbhtml.Append("<tr>");
                    sbhtml.AppendFormat("<td>{0}</td>", i);
                    sbhtml.AppendFormat("<td>{0}</td>", q.TriggerName);
                    sbhtml.AppendFormat("<td>{0}</td>", q.TriggerUrl);
                    sbhtml.AppendFormat("<td>{0}</td>", q.CronExpr);
                    sbhtml.AppendFormat("<td>{0}</td>", q.Explain);
                    sbhtml.AppendFormat("<td>{0}</td>", CommonEnum.GetValueByEnumName(typeof(CommonEnum.ValidStatus), q.IsValid));
                    sbhtml.AppendFormat("<td>{0}</td>", CommonEnum.GetValueByEnumName(typeof(JobRunStatus), q.RunStatus));
                    sbhtml.Append("<td>");
                    if (q.IsValid == CommonEnum.ValidStatus.Valid.GetHashCode())
                    {
                        string setRunStatusName = "开启";
                        int setRunStatus = JobRunStatus.Run.GetHashCode();
                        if (q.RunStatus == JobRunStatus.Run.GetHashCode())
                        {
                            setRunStatusName = "停止";
                            setRunStatus = JobRunStatus.Stop.GetHashCode();
                        }
                        sbhtml.AppendFormat("<button class=\"btn btn-sm btn-success\" onclick=\"SetJobRunStatus({0},{1},'{2}')\">{3}</button>"
                            , q.Id, setRunStatus, q.TriggerName, setRunStatusName);
                        sbhtml.AppendFormat("<button class=\"btn btn-sm btn-success\" style=\"margin-left:5px;\" onclick=\"AddOrEdit({0})\">编辑</button>", q.Id);
                        sbhtml.AppendFormat("<button class=\"btn btn-sm btn-success\" style=\"margin-left:5px;\"  onclick=\"SetRestart({0})\">重启</button>", q.Id);
                        sbhtml.AppendFormat("<button class=\"btn btn-sm btn-success\" style=\"margin-left:5px;\" onclick=\"SetInValid({0},0,'{1}')\">无效</button>"
                            , q.Id, q.TriggerName);
                    }
                    else
                    {
                        sbhtml.Append("--");
                    }
                    sbhtml.Append("</td></tr>");
                });
            }
            else
            {
                sbhtml.AppendFormat("<tr ><td colspan=\"{0}\" style=\"text-align:center;\">暂无数据</td></tr>", tableTH.Count);
            }
            ResponseAjaxContent(PageList.BindQueryListPage(tableTH, sbhtml.ParseToString(), jobSelect.PageIndex, jobSelect.TotalCount, jobSelect.PageSize));
        }

        /// <summary>
        /// 返回Ajax请求内容
        /// </summary>
        /// <param name="content"></param>
        public void ResponseAjaxContent(string content)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "text/html";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            HttpContext.Current.Response.Write(content);
            HttpContext.Current.Response.End();
        }
        public void Init(HttpContext context)
        {
            strActionName = context.Request["actionName"] ?? "";
            if (strActionName == "")
            {
                strActionName = context.Request["action"] ?? "";
                if (strActionName == "")
                {
                    ResponseAjaxContent("action请求不能为空");
                }
                return;
            }
            context.Response.Clear();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
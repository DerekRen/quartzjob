using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using System.Net;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using QuartzJob.Common;

namespace QuartzJob.QuartzHelper
{
    public class JobDetails : IJob
    {
        private static string objLock = "JobDetails";
        private delegate void AsyncDelegate(string strUrl, string strValue,string jobName);
        #region IJob 成员
        public void Execute(IJobExecutionContext context)
        {
            string execute = (string)context.JobDetail.JobDataMap.Get("JobExecute");//执行方法名称
            IDictionary<string, string> dicParams = (IDictionary<string, string>)context.JobDetail.JobDataMap.Get("JobParam");//方法参数
            try
            {
                string url = string.Empty;
                StringBuilder sb_URL = new StringBuilder();
                sb_URL.AppendFormat("JobName={0}", execute);
                if (dicParams != null && dicParams.Count > 0)
                {

                    foreach (var key in dicParams)
                    {
                        if (key.Key.Equals("TriggerUrl"))
                        {
                            url = key.Value;
                        }
                        sb_URL.AppendFormat("&{0}={1}", key.Key, key.Value);
                    }
                }
                if (string.IsNullOrEmpty(url))
                {
                    //记录错误
                    Log4NetHelper.WriteExceptionLog(string.Format("Job url 为空：JobName={0}", execute));
                }
                else
                {
                    //执行请求
                    AsyncDelegate async = new AsyncDelegate(ReadCallback);
                    async.BeginInvoke(url, sb_URL.ToString(),execute, null, null);
                }
            }
            catch (Exception e)
            {
                //记录错误
                Log4NetHelper.WriteExceptionLog(string.Format("JobDetails触发器执行异常：{0}", e));
            }
        }

        private void ReadCallback(string strUrl, string strValue,string jobName)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                byte[] content = Encoding.GetEncoding("utf-8").GetBytes(strValue);
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(strUrl);
                webRequest.Method = "Post";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                ServicePointManager.Expect100Continue = false;
                webRequest.ContentLength = content.Length;
                webRequest.Timeout = 5*60*1000;
                webRequest.ServicePoint.Expect100Continue = false;
                //string refMsg = string.Empty;
                using (Stream reqStream = webRequest.GetRequestStream())
                {
                    lock (objLock)
                    {
                        reqStream.Write(content, 0, content.Length);
                        reqStream.Close();
                    }
                }
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    //Stream receiveStream = response.GetResponseStream();
                    //// 将字节流包装为高级的字符流，以便于读取文本内容 
                    //StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    //refMsg = readStream.ReadToEnd();
                    //readStream.Close();
                    //response.Close();
                }
                //运行日志
                Log4NetHelper.WriteRunLog(string.Format("运行时间【{0}】,耗时【{1}ms】,job名称【{2}】jobURL【{3}】正常运行;", DateTime.Now, sw.ElapsedMilliseconds, jobName, strUrl));
            }
            catch (WebException ex)
            {
                //记录错误
                Log4NetHelper.WriteExceptionLog(string.Format("job名称【{0}】请求URL【{1}】,WebException【{2}】.Status【[{3}】,", jobName, strUrl, ex.ToString(), ex.Status));
            }
            catch (Exception ex)
            {
                //记录错误
                Log4NetHelper.WriteExceptionLog(string.Format("job名称【{0}】请求URL【{1}】,JobDetails触发器执行异常：{2}", jobName, strUrl,ex));
            }
        }
        #endregion
    }
}

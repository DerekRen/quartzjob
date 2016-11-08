using QuartzJob.Common;

namespace QuartzJob.QuartzHelper
{
    /// <summary>
    ///JobControler 的摘要说明
    /// </summary>
    public class JobControler : IJobManager
    {
        string JobName = string.Empty;
        /// <summary>
        /// 初始化作业名称
        /// </summary>
        /// <param name="jobName"></param>
        public JobControler(string jobName)
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
            this.JobName = jobName;
        }

        #region IJobManager 成员

        /// <summary>
        /// 运行作业
        /// </summary>
        public void Start()
        {
            Log4NetHelper.WriteRunLog("ShopQuartz job【" + this.JobName + "】启动");
            JobManagerBase.StartJob(this.JobName);
        }

        /// <summary>
        /// 停止作业
        /// </summary>
        public void Stop()
        {
            JobManagerBase.StopJob(this.JobName);
            Log4NetHelper.WriteRunLog("ShopQuartz job【" + this.JobName + "】停止");
        }

        ///// <summary>
        ///// 暂停作业
        ///// </summary>
        //public void Suspend()
        //{
        //    JobManagerBase.PauseJob(this.JobName);
        //    Log4NetHelper.WriteRunLog("ShopQuartz job【" + this.JobName + "】暂停");
        //}

        ///// <summary>
        ///// 恢复作业
        ///// </summary>
        //public void Resume()
        //{
        //    JobManagerBase.ResumeJob(this.JobName);
        //    Log4NetHelper.WriteRunLog("ShopQuartz job【" + this.JobName + "】恢复");
        //}

        /// <summary>
        /// 移除作业
        /// </summary>
        public void Remove()
        {
            JobManagerBase.RemoveJob(this.JobName);
            Log4NetHelper.WriteRunLog("ShopQuartz job【" + this.JobName + "】移除");
        }
        #endregion
    }
}
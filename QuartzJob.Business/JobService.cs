using QuartzJob.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzJob.Business
{
    public class JobService
    {
        private static JobService _instance;

        private JobService() { }

        public static JobService CreateInstance()
        {
            return _instance ?? (_instance = new JobService());
        }
        /// <summary>
        /// 获取job列表
        /// </summary>
        /// <returns></returns>
        public List<jobconfiguration> GetJobDataList(JobSelectModel jobSelect)
        {
            using (var db = new ECIRadarEntities())
            {
                jobSelect.TotalCount = db.jobconfiguration.Count(x =>
                (string.IsNullOrEmpty(jobSelect.TriggerName) || x.TriggerName == jobSelect.TriggerName)
                && (jobSelect.RunStaus == -1 || x.RunStatus == jobSelect.RunStaus)
                && (jobSelect.IsValid == -1 || x.IsValid == jobSelect.IsValid));
                var dataList = db.jobconfiguration.Where(x =>
                            (string.IsNullOrEmpty(jobSelect.TriggerName) || x.TriggerName == jobSelect.TriggerName)
                            && (jobSelect.RunStaus == -1 || x.RunStatus == jobSelect.RunStaus)
                            && (jobSelect.IsValid == -1 || x.IsValid == jobSelect.IsValid))
                            .OrderByDescending(x => x.Id)
                            .Skip(jobSelect.PageSize * (jobSelect.PageIndex - 1))
                            .Take(jobSelect.PageSize).ToList();
                return dataList;
            }
        }
        /// <summary>
        /// 获取job列表
        /// </summary>
        /// <returns></returns>
        public List<jobconfiguration> GetJobDataList()
        {
            using (var db = new ECIRadarEntities())
            {
                return db.jobconfiguration.Where(x => x.IsValid == 1).ToList();
            }
        }
        /// <summary>
        /// 获取job列表
        /// </summary>
        /// <returns></returns>
        public jobconfiguration GetJobData(long id)
        {
            using (var db = new ECIRadarEntities())
            {
                return db.jobconfiguration.Where(x => x.Id == id && x.IsValid == 1).FirstOrDefault();
            }
        }
        /// <summary>
        /// 设置job状态
        /// </summary>
        /// <returns></returns>
        public bool SetJobRowStatus(long id, int rowStatus)
        {
            using (var db = new ECIRadarEntities())
            {
                var jobData = db.jobconfiguration.Where(x => x.Id == id && x.IsValid == 1).FirstOrDefault();
                jobData.IsValid = rowStatus;
                return db.SaveChanges() > 0;
            }
        }
        /// <summary>
        /// 保存job
        /// </summary>
        /// <param name="TriggerName"></param>
        /// <param name="TriggerUrl"></param>
        /// <param name="CronExpr"></param>
        /// <param name="Explain"></param>
        /// <returns></returns>
        public long SaveJob(string TriggerName, string TriggerUrl, string CronExpr, string Explain, long Id = 0)
        {
            using (var db = new ECIRadarEntities())
            {
                var JobData = new jobconfiguration();
                if (Id == 0)
                {
                    JobData.TriggerName = TriggerName;
                    JobData.TriggerUrl = TriggerUrl;
                    JobData.CronExpr = CronExpr;
                    JobData.Explain = Explain;
                    JobData.RunStatus = 1;
                    JobData.IsValid = 1;
                    db.jobconfiguration.Add(JobData);
                }
                else
                {
                    JobData = db.jobconfiguration.Where(x => x.Id == Id).FirstOrDefault();
                    JobData.TriggerName = TriggerName;
                    JobData.TriggerUrl = TriggerUrl;
                    JobData.CronExpr = CronExpr;
                    JobData.Explain = Explain;
                }
                db.SaveChanges();
                return JobData.Id;
            }
        }
    }
    /// <summary>
    /// job查询条件
    /// </summary>
    public class JobSelectModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public JobSelectModel()
        {
            PageIndex = 1;
            PageSize = 10;
            TriggerName = string.Empty;
            RunStaus = -1;
            IsValid = -1;
        }
        /// <summary>
        /// 当前页面
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页面条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 数据总量
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// job名称
        /// </summary>
        public string TriggerName { get; set; }
        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStaus { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int IsValid { get; set; }
    }
}

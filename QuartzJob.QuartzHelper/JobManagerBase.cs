//-----------------------------------------------------------------------
// <copyright company="个人" file="JobManagerBase.cs">
//    Copyright (c)  V1.0   
//    作者：任何强 
//    功能：作业管理基类
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.ComponentModel;
using Quartz.Impl.Triggers;
using QuartzJob.Common;
using QuartzJob.DataAccess;
using System.Linq;
using QuartzJob.Business;

namespace QuartzJob.QuartzHelper
{
    /// <summary>
    /// 作业管理基类
    /// </summary>
    public class JobManagerBase
    {
        /// <summary>
        /// 作业集合，用于存储多个job
        /// </summary>
        private static IDictionary<string, JobStruct> jobs = new Dictionary<string, JobStruct>();


        /// <summary>
        /// 新增作业
        /// </summary>
        /// <param name="jobName">job名称</param>
        /// <param name="triggerId">jobId</param>
        /// <param name="cronExpr">时间表达式</param>
        /// <param name="triggerUrl">请求链接</param>
        /// <returns></returns>
        public static bool AddJob(string jobName, string triggerId, string cronExpr, string triggerUrl)
        {
            IDictionary<string, string> dicParams = new Dictionary<string, string>();
            dicParams.Add("triggerid", triggerId);
            dicParams.Add("CronExpr", cronExpr);
            dicParams.Add("TriggerUrl", triggerUrl);
            return AddJob(jobName, dicParams);
        }
        /// <summary>
        /// 新增作业
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <param name="dicParams">作业参数集合</param>
        /// <returns>是否添加成功</returns>
        public static bool AddJob(string jobName, IDictionary<string, string> dicParams)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                string jobCnName = JobStatus.None.ToString();
                if (jobs.ContainsKey(jobName))
                {
                    if (jobs[jobName] != null)
                    {
                        return true;
                    }
                    else
                    {
                        jobs.Remove(jobName);
                        JobStruct jobstruct = Instance(jobName, jobCnName, dicParams);
                        if (jobstruct != null)
                        {
                            jobs.Add(jobName, jobstruct);
                            return true;
                        }
                    }
                }
                else
                {
                    JobStruct jobstruct = Instance(jobName, jobCnName, dicParams);
                    if (jobstruct != null)
                    {
                        jobs.Add(jobName, jobstruct);
                        StartJob(jobName);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 移除作业
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <returns>是否移除</returns>
        public static bool RemoveJob(string jobName)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                return false;
            }

            bool isHasRemove = false;
            if (jobs.ContainsKey(jobName))
            {
                JobStruct jobStruct = jobs[jobName];
                if (jobStruct != null && jobStruct.sched != null && !jobStruct.sched.IsShutdown)//判断作业是否存在并且是否还活动着
                {
                    jobStruct.sched.Shutdown(false);
                }

                isHasRemove = jobs.Remove(jobName);
            }

            return isHasRemove;
        }

        ///// <summary>
        ///// 作业暂停
        ///// </summary>
        ///// <param name="jobName">作业名称</param>
        ///// <returns>是否暂停</returns>
        //public static bool PauseJob(string jobName)
        //{
        //    if (string.IsNullOrEmpty(jobName))
        //    {
        //        return false;
        //    }

        //    if (jobs.ContainsKey(jobName) && jobs[jobName] != null && jobs[jobName].sched != null)
        //    {

        //        jobs[jobName].sched.PauseJob(new JobKey(jobName + "Group"));
        //        if (jobs[jobName].sched.IsJobGroupPaused(jobName + "Group"))//检测作业是否已被暂停
        //        {
        //            jobs[jobName].JobStatus = JobStatus.Suspend.ToString();//修改当前的作业状态为暂停
        //            UpdateDataJobRunStatus(jobName, JobRunStatus.Suspend.GetHashCode());//修改当前作业在数据库的状态为暂停
        //            return true;
        //        }

        //        return false;
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// 作业恢复
        ///// </summary>
        ///// <param name="jobName">作业名称</param>
        ///// <returns>是否恢复</returns>
        //public static bool ResumeJob(string jobName)
        //{
        //    if (!string.IsNullOrEmpty(jobName))
        //    {
        //        if (jobs.ContainsKey(jobName) && jobs[jobName] != null && jobs[jobName].sched != null)
        //        {
        //            if (jobs[jobName].sched.IsJobGroupPaused(jobName + "Group"))
        //            {
        //                jobs[jobName].sched.ResumeJob(new JobKey(jobName + "Group"));
        //            }

        //            if (jobs[jobName].sched.IsStarted)//检测作业是否运行中
        //            {
        //                jobs[jobName].JobStatus = JobStatus.Running.ToString();//修改当前的作业状态为正在运行
        //                UpdateDataJobRunStatus(jobName, JobRunStatus.Run.GetHashCode());//修改当前作业在数据库的状态为运行
        //                return true;
        //            }

        //            return false;
        //        }
        //    }

        //    return false;
        //}

        /// <summary>
        /// 启动作业
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <returns>是否启动</returns>
        public static bool StartJob(string jobName)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                return false;
            }

            if (jobs.ContainsKey(jobName) && jobs[jobName] != null && jobs[jobName].sched != null)
            {
                jobs[jobName].sched.Start();//启动作业
                if (jobs[jobName].sched.IsStarted)//检测作业是否运行中
                {
                    jobs[jobName].JobStatus = JobStatus.Running.ToString();//修改当前的作业状态为正在运行
                    UpdateDataJobRunStatus(jobName, JobRunStatus.Run.GetHashCode());//修改当前作业在数据库的状态为运行
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 停止作业
        /// </summary>
        /// <param name="jobName">作业名称</param>
        /// <returns>是否停止</returns>
        public static bool StopJob(string jobName)
        {
            if (string.IsNullOrEmpty(jobName))
            {
                return false;
            }

            if (jobs.ContainsKey(jobName) && jobs[jobName] != null && jobs[jobName].sched != null)
            {
                jobs[jobName].sched.Shutdown(false);
                jobs[jobName] = Instance(jobName, JobStatus.ShutDown.ToString(), jobs[jobName].dicParams);
                UpdateDataJobRunStatus(jobName, JobRunStatus.Stop.GetHashCode());//修改当前作业在数据库的状态为未运行
                return true;
            }

            return false;
        }
        public static bool Restart(string jobName, string triggerId, string cronExpr, string triggerUrl)
        {
            if (RemoveJob(jobName))
            {
                if (AddJob(jobName, triggerId, cronExpr, triggerUrl))
                {
                    Log4NetHelper.WriteRunLog("ShopQuartz job【" + jobName + "】重启成功");
                    return true;
                }
                else
                {
                    Log4NetHelper.WriteExceptionLog("ShopQuartz job【" + jobName + "】重启时新增作业失败");
                }
            }
            else
            {
                Log4NetHelper.WriteExceptionLog("ShopQuartz job【" + jobName + "】重启时移除作业失败");
            }
            return false;
        }
        /// <summary>
        /// 更新作业在数据库中的运行状态
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="runStatus"></param>
        private static void UpdateDataJobRunStatus(string jobName, int runStatus)
        {
            using (var db = new ECIRadarEntities())
            {
                var data = db.jobconfiguration.Where(x => x.TriggerName == jobName).FirstOrDefault();
                if (data != null)
                {
                    data.RunStatus = runStatus;
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 初始化作业
        /// </summary>
        /// <param name="JobName">作业名称</param>
        /// <param name="JobStatus">作业状态</param>
        /// <param name="dicParams">作业参数结合</param>
        /// <returns>初始化作业</returns>
        private static JobStruct Instance(string JobName, string JobStatus, IDictionary<string, string> dicParams)
        {
            try
            {
                //为作业结构体赋值
                JobStruct jobStruct = new JobStruct();
                jobStruct.JobName = JobName;
                jobStruct.JobStatus = JobStatus;
                jobStruct.dicParams = dicParams;

                //初始化调度器工厂
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("quartz.scheduler.instanceName", JobName + "_SchedulerInstanceName");
                ISchedulerFactory schedulerFactory = new StdSchedulerFactory(nvc);
                jobStruct.sched = schedulerFactory.GetScheduler();
                IJobDetail jobDetail = null;
                //初始化作业器
                jobDetail = new JobDetailImpl(JobName, JobName + "Group", Type.GetType("QuartzJob.QuartzHelper.JobDetails"));
                jobDetail.JobDataMap.Add("JobExecute", JobName);
                jobDetail.JobDataMap.Add("JobParam", dicParams);
                //初始化作业触发器
                string cronExpr = dicParams["CronExpr"];//触发周期，采用配置模式，针对不同的作业新增配置不同的周期
                ITrigger trigger = new CronTriggerImpl(JobName + "Trigger", JobName + "Group", JobName, JobName + "Group", cronExpr);
                jobStruct.sched.ScheduleJob(jobDetail, trigger);
                return jobStruct;
            }
            catch (Exception ex)
            {
                //记录错误
                Log4NetHelper.WriteExceptionLog("Instance作业初始化异常：" + ex.Message.Replace("\n", ""));
                return null;
            }
        }

        /// <summary>
        /// 获取作业状态
        /// </summary>
        /// <param name="JobName">作业名称</param>
        /// <returns>作业名称</returns>
        public static string GetCnName(string JobName)
        {
            string rtnName = string.Empty;
            if (jobs.ContainsKey(JobName))
            {
                rtnName = jobs[JobName].JobStatus;
            }

            return string.IsNullOrEmpty(rtnName) ? JobName : rtnName;
        }
        /// <summary>
        /// IIS重启重新添加job
        /// </summary>
        /// <param name="JobName"></param>
        public static void ReAddAllJob()
        {
            var jobList = JobService.CreateInstance().GetJobDataList();
            if (!jobList.IsNullOrEmpty())
            {
                jobList.ForEach(q =>
                {
                    AddJob(q.TriggerName, q.Id.ToString(), q.CronExpr, q.TriggerUrl);
                    Log4NetHelper.WriteRunLog("ShopQuartz job【" + q.TriggerName + "】重启");
                });
            }
        }
    }

    /// <summary>
    /// 作业结构体
    /// </summary>
    public sealed class JobStruct
    {
        public string JobName = string.Empty;//作业名称
        public string JobStatus = string.Empty;//作业状态
        public IDictionary<string, string> dicParams = null;//参数集合
        public IScheduler sched = null;//作业调度器
    }

    /// <summary>
    /// Job状态
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// 初始
        /// </summary>
        [Description("初始")]
        None,

        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Running,

        /// <summary>
        /// 停止
        /// </summary>
        [Description("停止")]
        ShutDown,

        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Suspend
    }
}

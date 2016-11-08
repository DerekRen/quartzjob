//-----------------------------------------------------------------------
// <copyright company="个人" file="StartJob.cs">
//    Copyright (c)  V1.0   
//    作者：任何强
//    功能：
// </copyright>
//-----------------------------------------------------------------------

using QuartzJob.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace QuartzJob.QuartzHelper
{
    public class StartJob
    {
        //public static string DB_Name = GetCommonConfig.GetTCSceneryShopDataName();
        /// <summary>
        /// 默认程序启动项
        /// </summary>
        public static void AddJob()
        {
            try
            {
                DataTable dt = GetAllValidJob();
                //if (ConvertHelper.CheckDataTable(dt))
                //{
                foreach (DataRow dr in dt.Rows)
                {
                    IDictionary<string, string> dicParams = new Dictionary<string, string>();
                    dicParams.Add("type", dr["MType"].ToString());
                    dicParams.Add("triggerid", dr["Id"].ToString());
                    dicParams.Add("CronExpr", dr["CronExpr"].ToString());
                    dicParams.Add("TriggerUrl", dr["TriggerUrl"].ToString());
                    JobManagerBase.AddJob(dr["TriggerName"].ToString(), dicParams);
                }
                //}
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteExceptionLog("ShopQuartz异常", string.Format("详细信息【{0}】,发送时间【{1}】", ex.ToString(), DateTime.Now));
            }
        }

        //public static void ClearJobs()
        //{
        //    JobManagerBase.ClearJobs();
        //}

        /// <summary>
        /// 初始化所有有效的job
        /// </summary>
        /// <returns></returns>
        private static DataTable GetAllValidJob()
        {
            //StringBuilder sql = new StringBuilder();
            //sql.AppendFormat(@" SELECT TOP 1000 [Id]
            //                  ,[TriggerName]
            //                  ,[TriggerUrl]
            //                  ,[CronExpr]
            //                  ,[MType]
            //                  FROM [QuartzJob].[dbo].[JobConfiguration]  WITH(NOLOCK)
            //                  WHERE IfValid = 1 
            //                  ORDER BY Ord ASC  ");
            //return SqlHelper.ExecuteDataTable(sql.ToString());
            return null;
        }
    }
}

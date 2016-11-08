//-----------------------------------------------------------------------
// <copyright company="个人" file="JobManagerBase.cs">
//    Copyright (c)  V1.0   
//    作者：任何强
//    功能：推送类型枚举
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QuartzJob.QuartzHelper
{
    public enum PushTypeEnum
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,

        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 2,

        /// <summary>
        /// 修改
        /// </summary>
        Update = 3
    }
    /// <summary>
    /// Job运行状态
    /// </summary>
    public enum JobRunStatus
    {
        /// <summary>
        /// 已停止
        /// </summary>
        [Description("已停止")]
        Stop = 0,

        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Run = 1
    }
}

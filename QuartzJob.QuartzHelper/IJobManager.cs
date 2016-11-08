namespace QuartzJob.QuartzHelper
{
    /// <summary>
    /// 作业控制服务接口
    /// </summary>
    public interface IJobManager
    {
        /// <summary>
        /// 运行作业
        /// </summary>
        void Start();

        /// <summary>
        /// 停止作业
        /// </summary>
        void Stop();

        ///// <summary>
        ///// 挂起作业
        ///// </summary>
        //void Suspend();

        ///// <summary>
        ///// 恢复作业
        ///// </summary>
        //void Resume();
    }
}

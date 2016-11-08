using System;
using System.Text;
using Newtonsoft.Json;

namespace QuartzJob.Common
{
    public class Log4NetHelper
    {
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="message">自定义消息</param>
        /// <param name="ex">异常信息</param>
        public static void WriteExceptionLog(string message)
        {
            WriteExceptionLog("ExceptionLogger", message);
        }
        /// <summary>
        ///  记录异常日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        /// <param name="response"></param>
        public static void WriteExceptionLog(Exception ex)
        {
            string message = "\r\n异常信息:" + ex.ToString();
            WriteExceptionLog(message);
        }
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="message">自定义消息</param>
        /// <param name="ex">异常信息</param>
        public static void WriteExceptionLog(string msgName, string message)
        {
            WriteExceptionLog(msgName, message, null);
        }
        /// <summary>
        ///  记录异常日志
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="arg"></param>
        /// <param name="response"></param>
        public static void WriteExceptionLog(string msgName, string message, Exception ex, object arg = null, object response = null)
        {
            StringBuilder strBur = new StringBuilder();
            string pattern = "\r\n-------------------------------------------------------------------------------";
            strBur.AppendLine(pattern);
            strBur.AppendLine("请求时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (arg != null)
                strBur.AppendLine("\r\n请求：" + JsonConvert.SerializeObject(arg));
            if (response != null)
                strBur.AppendLine("\r\n返回：" + JsonConvert.SerializeObject(response));
            strBur.AppendLine(message);
            strBur.AppendLine(pattern);
            log4net.LogManager.GetLogger(msgName).Error(strBur.ToString());
        }

        /// <summary>
        /// 写入运行日志
        /// </summary>
        /// <param name="message">自定义消息</param>
        public static void WriteRunLog(string message)
        {
            log4net.LogManager.GetLogger("RuningLogger").Info(message);
        }
    }
}

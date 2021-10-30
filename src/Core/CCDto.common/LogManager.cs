using System;
using System.Collections.Generic;
using System.Text;

namespace CCDto.common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    public class LogManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        static LogManager()
        {
            Start();
        }

        #region 队列方法

        /// <summary>
        /// 日志队列
        /// </summary>
        private static Queue<Log> ListQueue = new Queue<Log>();

        private static object _lock = new object();

        public enum LogType
        {
            Trace,
            Warning,
            SQL,
            Error,
            DbError,
            RabbitMqError,
            RabbitMqTrace,
            RfidState,
            PLCState,

            Ng,
            LifeCycle
        }

        class Log
        {
            public LogType LogType { get; set; }
            public string Msg { get; set; }
            public string Path { get; set; }
        }


        public static void WriteLog(string msg, LogType logType,string path = "")
        {
            Log log = new Log()
            {
                Msg = msg,
                LogType = logType,
                Path = path
            };

            lock (_lock)
            {
                ListQueue.Enqueue(log);
            }
        }

        private static void Start()//启动
        {
            WriteLog("ULog", LogType.Trace);
            Thread thread = new Thread(threadStart);
            thread.IsBackground = true;
            thread.Start();

        }

        private static void threadStart()
        {
            while (true)
            {
                if (ListQueue.Count > 0)
                {
                    try
                    {
                        ScanQueue();
                    }
                    catch (Exception ex)
                    {
                        throw;
                        //LO_LogInfo.WLlog(ex.ToString());
                    }
                }
                else
                {
                    //没有任务，休息3秒钟
                    Thread.Sleep(1000);
                }
            }
        }
        //要执行的方法
        private static void ScanQueue()
        {
            while (ListQueue.Count > 0)
            {
                try
                {
                    if (ListQueue != null)
                    {
                        //从队列中取出
                        Log log = ListQueue.Dequeue();
                        ThreadLog(log);

                        //Console.WriteLine(queueinfo.feedid);
                        //取出的queueinfo就可以用了，里面有你要的东西
                        //以下就是处理程序了
                        //。。。。。。
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        #endregion

        private static string logPath = string.Empty;

        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(logPath))
                {
                    logPath = AppDomain.CurrentDomain.BaseDirectory + @"Logs\";
                }
                return logPath;
            }
            set { logPath = value; }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        private static void ThreadLog(Log log)
        {
            try
            {
                var path = LogPath + log.LogType + (string.IsNullOrWhiteSpace(log.Path) ? "" : $"/{log.Path}");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(path + "/" + DateTime.Now.ToString("yyyyMMddHH") + ".txt"))
                {
                    File.Create(path + "/" + DateTime.Now.ToString("yyyyMMddHH") + ".txt").Close();
                }
                File.AppendAllText(path + "/" + DateTime.Now.ToString("yyyyMMddHH") + ".txt", $"\r\n{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")} {log.Msg}");
            }
            catch (Exception e)
            {
                var i = e.Message;
            }
        }

    }
}

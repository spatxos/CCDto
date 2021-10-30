using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace CCDto.common
{
    public static class FileHelper 
    {

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
                    logPath = AppDomain.CurrentDomain.BaseDirectory + @"Uids\";
                }
                return logPath;
            }
            set { logPath = value; }
        }

        private static string logFielPrefix = string.Empty;
        /// <summary>
        /// 日志文件前缀
        /// </summary>
        public static string LogFielPrefix
        {
            get { return !string.IsNullOrWhiteSpace(logFielPrefix) ? "/" + logFielPrefix + "/" : ""; }
            set { logFielPrefix = value; }
        }

        public static string Read(string logFile)
        {
            var result = "";
            try
            {
                if (!Directory.Exists(LogPath + LogFielPrefix))
                {
                    Directory.CreateDirectory(LogPath + LogFielPrefix);
                }
                if (!File.Exists(LogPath + LogFielPrefix + logFile + ".json"))
                {
                    File.Create(LogPath + LogFielPrefix + logFile + ".json").Close();
                }
                result = File.ReadAllText(LogPath + LogFielPrefix + logFile + ".json");
            }
            catch (Exception e)
            {
                var i = e.Message;
            }
            return result;
        }




        /// <summary>
        /// 构造函数
        /// </summary>
        static FileHelper()
        {
            Start();
        }

        #region 队列方法

        /// <summary>
        /// 日志队列
        /// </summary>
        private static Queue<Log> ListQueue = new Queue<Log>();

        private static object _lock = new object();


        class Log
        {
            public string File { get; set; }
            public string Msg { get; set; }
        }


        public static void Write(string lodid,string msg)
        {
            Log log = new Log()
            {
                File = lodid,
                Msg = msg
            };
            lock (_lock)
            {
                ListQueue.Enqueue(log);
            }
        }

        private static void Start()//启动
        {
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
                    //从队列中取出
                    Log log = ListQueue.Dequeue();
                    ThreadLog(log.File, log.Msg);

                    //Console.WriteLine(queueinfo.feedid);
                    //取出的queueinfo就可以用了，里面有你要的东西
                    //以下就是处理程序了
                    //。。。。。。

                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion

        /// <summary>
        /// 写日志
        /// </summary>
        private static void ThreadLog(string logFile, string msg)
        {
            try
            {
                if (!Directory.Exists(LogPath + LogFielPrefix))
                {
                    Directory.CreateDirectory(LogPath + LogFielPrefix);
                }
                if (!File.Exists(LogPath + LogFielPrefix + logFile + ".json"))
                {
                    File.Create(LogPath + LogFielPrefix + logFile + ".json").Close();
                }

                File.WriteAllText(LogPath + LogFielPrefix + logFile + ".json", msg);
            }
            catch (Exception e)
            {
                var i = e.Message;
            }
        }
    }

}
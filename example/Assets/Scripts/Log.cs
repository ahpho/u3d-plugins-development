using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AuthorZhidai
{

    public class Log
    {

        private const int LOG_COUNT = 20;// �����ʱ����LOG_COUNT����־
        private const int LOG_FILE_COUNT = 10;// ��ౣ�����־�ļ���


        public static bool EnableLog = true;// �Ƿ�������־�����ɿ�����ͨ�������־��������رգ�LogError��LogWarn����ʼ�����õġ�
        public static bool EnableSave = true;// �Ƿ���������־��������־д�뵽�ļ���

        public static string LogFileDir = Application.dataPath.Replace("Assets", "") + "Log";// ��־���Ŀ¼����Assetsͬ��Ŀ¼�µ�Log
        public static string LogFileName = "";
        public static string Prefix = "> ";// ������UnityĬ�ϵ�ϵͳ��־�����֡�����־ϵͳ�������־ͷ��������������ǡ�
        public static StreamWriter LogFileWriter = null;


        //��־�б�����Infoʱ�������ʹ��
        public static List<string> ListLogs = new List<string>();

        //��һ��ִ�д�ӡlog
        private static bool FirstLogTag = true;

        public static void Init()
        {
            Application.logMessageReceived += OnLogByUnity;
        }

        #region ��־
        public static void Info(object message, bool recordStackTrace = false)
        {
            string str = "[I]" + GetLogTime() + message;
            AddListLogs(str);
            if (!EnableLog)
                return;

            Debug.Log(Prefix + str, null);
            LogToFile(str, recordStackTrace);
        }

        public static void Warning(object message)
        {
            string str = "[W]" + GetLogTime() + message;
            AddListLogs(str);
            Debug.LogWarning(Prefix + str, null);
            LogToFile(str, true);
        }

        public static void Error(object message)
        {
            string str = "[E]" + GetLogTime() + message;
            Debug.LogError(Prefix + str, null);
            if (!EnableLog)
            {
                OutputListLogs(LogFileWriter);// ����Infoʱ�����Զ�����־��¼���ļ��з������
            }
            else
            {
                AddListLogs(str);
            }
            LogToFile(str, true);
        }

        /// <summary>
        /// ����б���������־
        /// ���ܻ���ɿ��٣�����ʹ�á�
        /// </summary>
        /// <param name="sw"></param>
        public static void OutputListLogs(StreamWriter sw)
        {
            if (sw == null || ListLogs.Count < 1)
                return;
            sw.WriteLine($"---------------- Log History Start [�����Ǳ���ǰ{ListLogs.Count}����־]---------------- ");
            foreach (var i in ListLogs)
            {
                sw.WriteLine(i);
            }
            sw.WriteLine($"---------------- Log History  End  [�����Ǳ���ǰ{ListLogs.Count}����־]---------------- ");
            ListLogs.Clear();
        }
        #endregion

        public static void CloseLog()
        {
            if (LogFileWriter != null)
            {
                try
                {
                    LogFileWriter.Flush();
                    LogFileWriter.Close();
                    LogFileWriter.Dispose();
                    LogFileWriter = null;
                }
                catch (Exception)
                {
                }
            }
        }

        public static void CheckClearLog()
        {
            if (!Directory.Exists(LogFileDir))
            {
                return;
            }

            DirectoryInfo direction = new DirectoryInfo(LogFileDir);
            var files = direction.GetFiles("*");
            if (files.Length >= LOG_FILE_COUNT)
            {
                var oldfile = files[0];
                var lastestTime = files[0].CreationTime;
                foreach (var file in files)
                {
                    if (lastestTime > file.CreationTime)
                    {
                        oldfile = file;
                        lastestTime = file.CreationTime;
                    }

                }
                oldfile.Delete();
            }

        }

        private static void OnLogByUnity(string condition, string stackTrace, LogType type)
        {
            // �����Լ������
            if (type == LogType.Log || condition.StartsWith(Prefix))
            {
                return;
            }
            var str = type == LogType.Warning ? "[W]" : "[E]" + GetLogTime() + condition + "\n" + stackTrace;
            if (!EnableLog && type != LogType.Warning)
                OutputListLogs(LogFileWriter);// ����Infoʱ�����Զ�����־��¼���ļ��з������
            else
                AddListLogs(str);
            LogToFile(str);
        }

        private static void AddListLogs(string str)
        {
            if (ListLogs.Count > LOG_COUNT)
            {
                ListLogs.RemoveAt(0);
            }
            ListLogs.Add(str);
        }

        private static string GetLogTime()
        {
            string str = "";

            str = DateTime.Now.ToString("HH:mm:ss.fff") + " ";


            return str;
        }

        /// <summary>
        /// ����־д�뵽�ļ���
        /// </summary>
        /// <param name="message"></param>
        /// <param name="EnableStack"></param>
        private static void LogToFile(string message, bool EnableStack = false)
        {
            if (!EnableSave)
                return;

            if (LogFileWriter == null)
            {
                CheckClearLog();
                LogFileName = DateTime.Now.GetDateTimeFormats('s')[0].ToString();
                LogFileName = LogFileName.Replace("-", "_");
                LogFileName = LogFileName.Replace(":", "_");
                LogFileName = LogFileName.Replace(" ", "");
                LogFileName = LogFileName.Replace("T", "_");
                LogFileName = LogFileName + ".log";
                if (string.IsNullOrEmpty(LogFileDir))
                {
                    try
                    {
                        if (!Directory.Exists(LogFileDir))
                        {
                            Directory.CreateDirectory(LogFileDir);
                        }
                    }
                    catch (Exception exception)
                    {
                        Debug.Log(Prefix + "��ȡ Application.streamingAssetsPath ����" + exception.Message, null);
                        return;
                    }
                }
                string path = LogFileDir + "/" + LogFileName;

                Debug.Log("Log Path :" + LogFileDir + "\nLog Name :" + LogFileName);
                try
                {
                    if (!Directory.Exists(LogFileDir))
                    {
                        Directory.CreateDirectory(LogFileDir);
                    }
                    LogFileWriter = File.AppendText(path);
                    LogFileWriter.AutoFlush = true;
                }
                catch (Exception exception2)
                {
                    LogFileWriter = null;
                    Debug.Log("LogToCache() " + exception2.Message + exception2.StackTrace, null);
                    return;
                }
            }
            if (LogFileWriter != null)
            {
                try
                {
                    if (FirstLogTag)
                    {
                        FirstLogTag = false;
                        PhoneSystemInfo(LogFileWriter);
                    }
                    LogFileWriter.WriteLine(message);
                    if (EnableStack)
                    {
                        //���޹ص�logȥ��
                        var st = StackTraceUtility.ExtractStackTrace();
#if UNITY_EDITOR
                        for (int i = 0; i < 3; i++)
#else
                        for (int i = 0; i < 2; i++)
#endif
                        {
                            st = st.Remove(0, st.IndexOf('\n') + 1);
                        }
                        LogFileWriter.WriteLine(st);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private static void PhoneSystemInfo(StreamWriter sw)
        {
            sw.WriteLine("*********************************************************************************************************start");
            sw.WriteLine("By " + SystemInfo.deviceName);
            DateTime now = DateTime.Now;
            sw.WriteLine(string.Concat(new object[] { now.Year.ToString(), "��", now.Month.ToString(), "��", now.Day, "��  ", now.Hour.ToString(), ":", now.Minute.ToString(), ":", now.Second.ToString() }));
            sw.WriteLine();
            sw.WriteLine("����ϵͳ:  " + SystemInfo.operatingSystem);
            sw.WriteLine("ϵͳ�ڴ��С:  " + SystemInfo.systemMemorySize);
            sw.WriteLine("�豸ģ��:  " + SystemInfo.deviceModel);
            sw.WriteLine("�豸Ψһ��ʶ��:  " + SystemInfo.deviceUniqueIdentifier);
            sw.WriteLine("����������:  " + SystemInfo.processorCount);
            sw.WriteLine("����������:  " + SystemInfo.processorType);
            sw.WriteLine("�Կ���ʶ��:  " + SystemInfo.graphicsDeviceID);
            sw.WriteLine("�Կ�����:  " + SystemInfo.graphicsDeviceName);
            sw.WriteLine("�Կ���ʶ��:  " + SystemInfo.graphicsDeviceVendorID);
            sw.WriteLine("�Կ�����:  " + SystemInfo.graphicsDeviceVendor);
            sw.WriteLine("�Կ��汾:  " + SystemInfo.graphicsDeviceVersion);
            sw.WriteLine("�Դ��С:  " + SystemInfo.graphicsMemorySize);
            sw.WriteLine("�Կ���ɫ������:  " + SystemInfo.graphicsShaderLevel);
            sw.WriteLine("�Ƿ�ͼ��Ч��:  " + SystemInfo.supportsImageEffects);
            sw.WriteLine("�Ƿ�֧��������Ӱ:  " + SystemInfo.supportsShadows);
            sw.WriteLine("*********************************************************************************************************end");
            sw.WriteLine("LogInfo:");
            sw.WriteLine();
        }

    }

}

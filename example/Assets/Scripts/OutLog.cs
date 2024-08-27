using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class OutLog : MonoBehaviour
{
    private static bool mInited = false;
    private string mOutpath;
    static readonly List<string> mWriteLines = new();
    static readonly List<Tuple<Color, string>> mDisplayLines = new();

    public static void Init()
    {
        if (mInited) return;
        mInited = true;
        GameObject outLog = new("__OutLog__", new System.Type[] { typeof(OutLog) });
        DontDestroyOnLoad(outLog);
    }

    void Start()
    {
        string dir = Path.Combine(Application.persistentDataPath, "Logs");
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        string t = System.DateTime.Now.ToString("yyyyMMdd-hhmmss");
        mOutpath = string.Format("{0}/{1}.log", dir, t);

        //每次启动客户端删除之前保存的Log
        if (System.IO.File.Exists(mOutpath))
        {
            File.Delete(mOutpath);
        }

        Application.logMessageReceived += HandleLog;
        Debug.Log("OutLog Inited.");
    }

    void Update()
    {
        //因为写入文件的操作必须在主线程中完成，所以在Update中哦给你写入文件。
        if (mWriteLines.Count > 0)
        {
            string[] temp = mWriteLines.ToArray();
            foreach (string t in temp)
            {
                using (StreamWriter writer = new StreamWriter(mOutpath, true, Encoding.UTF8))
                {
                    writer.WriteLine(t);
                }
                mWriteLines.Remove(t);
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string line = string.Format($"[{System.DateTime.Now.ToString("yyyyMMdd-hhmmss")}-{PrefixFrom(type)}] {logString}");
        mWriteLines.Add(line);
        Display(type, line);

        if (type == LogType.Assert || type == LogType.Error || type == LogType.Exception)
        {
            stackTrace = "\t" + stackTrace.Replace("\n", "\n\t");
            mWriteLines.Add(stackTrace);
            //Display(type, stackTrace); // 堆栈不显示在屏幕上
        }
    }

    //将log输出在屏幕上
    static public void Display(LogType type, params object[] objs)
    {
        string text = "";
        for (int i = 0; i < objs.Length; ++i)
        {
            if (i == 0)
            {
                text += objs[i].ToString();
            }
            else
            {
                text += ", " + objs[i].ToString();
            }
        }
        if (Application.isPlaying)
        {
            if (mDisplayLines.Count > 20)
            {
                mDisplayLines.RemoveAt(0);
            }
            mDisplayLines.Add(new Tuple<Color, string>(ColorFrom(type), text));
        }
    }

    static string PrefixFrom(LogType type)
    {
        switch (type)
        {
            case LogType.Error: return "E";
            case LogType.Assert: return "A";
            case LogType.Warning: return "W";
            case LogType.Log: return "I";
            case LogType.Exception: return "X";
        }
        return "N/A";
    }

    static Color ColorFrom(LogType type)
    {
        switch (type)
        {
            case LogType.Log:
                return Color.white;
            case LogType.Warning:
                return Color.yellow;
        }
        return Color.red;
    }

    void OnGUI()
    {
        for (int i = mDisplayLines.Count - 1; i >= 0; i--)
        {
            GUI.color = mDisplayLines[i].Item1;
            GUILayout.Label(mDisplayLines[i].Item2);
        }
    }
}

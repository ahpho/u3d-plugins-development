using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class OutLog : MonoBehaviour
{
    static readonly List<string> mWriteLines = new();
    static readonly List<Tuple<Color, string>> mDisplayLines = new ();
    private string outpath;

    public static void Init()
    {
        GameObject outLog = new("OutLog", new System.Type[] { typeof(OutLog) });
        DontDestroyOnLoad(outLog);
    }

    void Start()
    {
        string t = System.DateTime.Now.ToString("yyyyMMdd-hhmmss");
        outpath = string.Format("{0}/{1}.log", Application.persistentDataPath, t);

        //每次启动客户端删除之前保存的Log
        if (System.IO.File.Exists(outpath))
        {
            File.Delete(outpath);
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
                using (StreamWriter writer = new StreamWriter(outpath, true, Encoding.UTF8))
                {
                    writer.WriteLine(t);
                }
                mWriteLines.Remove(t);
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string line = string.Format($"[{System.DateTime.Now.ToString("yyyyMMdd-hhmmss")}] {logString}");
        mWriteLines.Add(line);
        Display(type, line);

        if (type == LogType.Assert || type == LogType.Error || type == LogType.Exception)
        {
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
            mDisplayLines.Add(new Tuple<Color, string>(ColorFor(type), text));
        }
    }

    static Color ColorFor(LogType type)
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

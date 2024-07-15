using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NativeCDemo : MonoBehaviour
{
    private int m_ret1;
    private int m_ret2;
    private int m_ret3;
    private int m_ret4;
    private string m_strRD;

    private void Start()
    {
        Debug.Log($"Before NativeCDemo.Start()");
        NativeCSBinder.Register();
        Debug.Log($"After NativeCDemo.Start()");
    }

    private void CallFunction()
    {
        m_ret1 = NativeCCore.tst_add(10, 2);
        m_ret2 = NativeCCore.tst_sub(10, 2);
        m_ret3 = NativeCCore.tst_div(10, 2);
        m_ret4 = NativeCCore.tst_mul(10, 2);

        Debug.Log("10+2=" + m_ret1);
        Debug.Log("10-2=" + m_ret2);
        Debug.Log("10/2=" + m_ret3);
        Debug.Log("10*2=" + m_ret4);
    }

    private void PInvokeFunction()
    {
        NativeCCore.call_func();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

        int retHeight = 100;
        if (GUILayout.Button("返回Launcher", GUILayout.Height(retHeight))) {
            SceneManager.LoadScene("Launcher");
        }

        if (GUILayout.Button("调用C代码", GUILayout.Height((Screen.height - retHeight) >> 4))) {
            CallFunction();
        }
        if (GUILayout.Button("C调用CS", GUILayout.Height((Screen.height - retHeight) >> 4))) {
            PInvokeFunction();
        }

        if (GUILayout.Button("RD.cmd.CommitHash", GUILayout.Height((Screen.height - retHeight) >> 4)))
		{
            m_strRD = RenderDocCmdCore.RENDERDOC_CanGlobalHook().ToString();
        }
        if (GUILayout.Button("RD.cmd.usleep", GUILayout.Height((Screen.height - retHeight) >> 4)))
		{
            Debug.Log($"Before RenderDocCmdCore.usleep()");
            m_strRD = RenderDocCmdCore.usleep((IntPtr)6000000).ToString();
            Debug.Log($"After RenderDocCmdCore.usleep()"); 
        }
        if (GUILayout.Button("RD.vklayer.RENDERDOC_SetDebugLogFile", GUILayout.Height((Screen.height - retHeight) >> 4)))
        {
            VkLayerCore.RENDERDOC_SetDebugLogFile("./fuck123.log");
        }
        if (GUILayout.Button("RD.vklayer.force_include_libentry", GUILayout.Height((Screen.height - retHeight) >> 4)))
		{
            m_strRD = VkLayerCore.force_include_libentry().ToString();
        }
        if (GUILayout.Button("RD.vklayer.usleep", GUILayout.Height((Screen.height - retHeight) >> 4)))
		{
            Debug.Log($"Before VkLayerCore.usleep()");
            m_strRD = VkLayerCore.usleep((IntPtr)6000000).ToString();
            Debug.Log($"After VkLayerCore.usleep()"); 
        }

        GUILayout.TextArea(
           "10+2=" + m_ret1 + "\n" + 
           "10-2=" + m_ret2 + "\n" + 
           "10/2=" + m_ret3 + "\n" + 
           "10*2=" + m_ret4 + "\n" +
           "RDstr="+ m_strRD+ "\n", 
           GUILayout.Height((Screen.height - retHeight) >> 1)
        );

        GUILayout.EndArea();
    }
}

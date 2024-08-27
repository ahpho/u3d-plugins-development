using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    private void Awake()
    {
        OutLog.Init();
    }

    private void CallManaged()
    {
        SceneManager.LoadScene("Managed");
    }

    private void CallCDemo()
    {
        SceneManager.LoadScene("C");
    }

    private void CallSystemDemo()
    {
        SceneManager.LoadScene("System");
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        const int BtnCount = 5;
        string info = "N/A";

        if (GUILayout.Button("Demo Managed", GUILayout.Height(Screen.height / BtnCount))) {
            CallManaged();
        }

        if (GUILayout.Button("切3D场景", GUILayout.Height(Screen.height / BtnCount))) {
            info = SystemInfo.deviceModel + "," + SystemInfo.deviceName;
            Debug.LogError($"[切3D场景] info={info}");
            SceneManager.LoadScene("Main");
        }

        if (GUILayout.Button("Demo C", GUILayout.Height(Screen.height / BtnCount))) {
            CallCDemo();
        }

        if (GUILayout.Button("Demo System", GUILayout.Height(Screen.height / BtnCount))) {
            CallSystemDemo();
        }

        if (GUILayout.Button(info, GUILayout.Height(Screen.height / BtnCount))) {
            Debug.LogWarning($"Btn 5 clicked! ");
        }

        GUILayout.EndArea();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
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

        if (GUILayout.Button("Demo Managed", GUILayout.Height(Screen.height / BtnCount))) {
            CallManaged();
            //info = SystemInfo.deviceModel + "," + SystemInfo.deviceName;
        }

        if (GUILayout.Button("切3D场景", GUILayout.Height(Screen.height / BtnCount))) {
            SceneManager.LoadScene("Main");
        }

        if (GUILayout.Button("Demo C", GUILayout.Height(Screen.height / BtnCount))) {
            CallCDemo();
        }

        if (GUILayout.Button("Demo System", GUILayout.Height(Screen.height / BtnCount))) {
            CallSystemDemo();
        }

        if (GUILayout.Button(info, GUILayout.Height(Screen.height / BtnCount)))
        {
        }

        GUILayout.EndArea();
    }
}

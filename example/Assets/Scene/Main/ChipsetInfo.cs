using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipsetInfo : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Graphics Info: " + GetGraphicsDeviceInfo());
        Debug.Log("Chipset Info 1: " + GetChipsetInfo1());
        Debug.Log("Chipset Info 2: " + GetChipsetInfo2());
    }

    string GetGraphicsDeviceInfo()
    {
        string g = $"graphicsName={SystemInfo.graphicsDeviceName} vendor={SystemInfo.graphicsDeviceVendor}";
        return g;
    }

    string GetChipsetInfo1()
    {
        string chipset = "Unknown";
        try
        {
            using (AndroidJavaClass buildClass = new AndroidJavaClass("android.os.Build"))
            {
                string hardware = buildClass.GetStatic<string>("HARDWARE");
                string manufacturer = buildClass.GetStatic<string>("MANUFACTURER");
                string model = buildClass.GetStatic<string>("MODEL");
                string board = buildClass.GetStatic<string>("BOARD");
                string product = buildClass.GetStatic<string>("PRODUCT");
                

                chipset = $"manufacturer={manufacturer} model={model} product={product} board={board} Hardware={hardware}";
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to get chipset info: " + e.Message);
        }
        return chipset;
    }

    string GetChipsetInfo2()
    {
        string chipset = "Unknown";
        try
        {
            using (AndroidJavaClass buildClass = new AndroidJavaClass("android.os.Build"))
            {
                using (AndroidJavaObject buildObject = buildClass.GetStatic<AndroidJavaObject>("UNKNOWN"))
                {
                    string hardware = buildObject.Call<string>("getString", "ro.board.platform");
                    chipset = hardware;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to get chipset info: " + e.Message);
        }
        return chipset;
    }
}


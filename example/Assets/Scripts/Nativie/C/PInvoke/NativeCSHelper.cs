using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public static class NativeCSHelper
{
    public static void RegFunction(NativeCSFunction ncf)
    {
        Debug.Log($"GetFunctionPointerForDelegate={ncf}");
        IntPtr fn = Marshal.GetFunctionPointerForDelegate(ncf);
        Debug.Log($"IntPtr fn={fn}");
        NativeCCore.reg_func(fn);
        Debug.Log($"After reg_func(fn).");
    }
}

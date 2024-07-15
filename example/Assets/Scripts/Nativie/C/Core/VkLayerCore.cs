using System;
using System.Runtime.InteropServices;

public static class VkLayerCore
{
#if UNITY_IPHONE && !UNITY_EDITOR
	private const string VKLAYERDLL = "__Internal";
#else
    private const string VKLAYERDLL = "VkLayer_GLES_RenderDoc";
#endif

    [DllImport(VKLAYERDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern string RENDERDOC_GetCommitHash();
    [DllImport(VKLAYERDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool RENDERDOC_CanGlobalHook();
    [DllImport(VKLAYERDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void RENDERDOC_SetDebugLogFile(string logFile);
    [DllImport(VKLAYERDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr force_include_libentry();
    [DllImport(VKLAYERDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int usleep(IntPtr usec);
}

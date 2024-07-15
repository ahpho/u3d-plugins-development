using System;
using System.Runtime.InteropServices;

public static class RenderDocCmdCore
{
#if UNITY_IPHONE && !UNITY_EDITOR
	private const string RENDERDOCCMDDLL = "__Internal";
#else
    private const string RENDERDOCCMDDLL = "renderdoccmd";
#endif

    [DllImport(RENDERDOCCMDDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern string RENDERDOC_GetCommitHash();
    [DllImport(RENDERDOCCMDDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool RENDERDOC_CanGlobalHook();
    [DllImport(RENDERDOCCMDDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern void RENDERDOC_SetDebugLogFile(string logFile);
    [DllImport(RENDERDOCCMDDLL, CallingConvention = CallingConvention.Cdecl)]
    public static extern int usleep(IntPtr usec);
}

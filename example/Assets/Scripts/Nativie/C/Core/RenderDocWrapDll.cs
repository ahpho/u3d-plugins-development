using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RDWInterface
{
	public static class RDWDll
	{
        // 系统api
#if UNITY_EDITOR && UNITY_STANDALONE_WIN
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary(string dllToLoad);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern bool FreeLibrary(IntPtr hModule);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
#else
        //需要与loadLib/getProc等效的Posix实现
#endif

        //直接调用
#if UNITY_EDITOR
    #if UNITY_STANDALONE_WIN
        public const string RENDERDOC = "renderdoc";
    #else
		public const string RENDERDOC = "renderdoc_Mac_Bundle";
    #endif
#else
    #if UNITY_IPHONE
		public const string RENDERDOC = "__Internal";
    #else
		public const string RENDERDOC = "renderdoc";
    #endif
#endif
        [DllImport(RENDERDOC, CharSet = CharSet.Ansi)]
        public static extern void RENDERDOC_StartSelfHostCapture(string strModuleName);
        [DllImport(RENDERDOC, CharSet = CharSet.Ansi)]
        public static extern void RENDERDOC_EndSelfHostCapture(string strModuleName);


        //wrap调用
#if UNITY_EDITOR
    #if UNITY_STANDALONE_WIN
        public const string RENDERDOCWRAP = "tstunity";
    #else
        public const string RENDERDOCWRAP = "tstunity_Mac_Bundle";
    #endif
#else
    #if UNITY_IPHONE
		public const string RENDERDOCWRAP = "__Internal";
    #else
		public const string RENDERDOCWRAP = "tstunity";
    #endif
#endif
        [DllImport(RENDERDOCWRAP, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		public static extern int RdwInitModule(string szLibraryName);
        [DllImport(RENDERDOCWRAP, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern string RdwGetPathTemplate();
        [DllImport(RENDERDOCWRAP, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RdwSetPathTemplate(string szPathTemplate);
        [DllImport(RENDERDOCWRAP, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RdwStartCapture(IntPtr devicePtr, IntPtr wndHandle);
        [DllImport(RENDERDOCWRAP, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RdwEndCapture(IntPtr devicePtr, IntPtr wndHandle);
        [DllImport(RENDERDOCWRAP, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int RdwTriggerCapture();
    }
}

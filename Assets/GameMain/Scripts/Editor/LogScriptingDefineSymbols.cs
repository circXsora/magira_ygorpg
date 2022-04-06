using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// 脚本宏定义。
/// </summary>
public static class ScriptingDefineSymbols
{
    private static readonly BuildTargetGroup[] BuildTargetGroups = new BuildTargetGroup[]
    {
            BuildTargetGroup.Standalone,
            BuildTargetGroup.iOS,
            BuildTargetGroup.Android,
            BuildTargetGroup.WSA,
            BuildTargetGroup.WebGL
    };

    /// <summary>
    /// 检查指定平台是否存在指定的脚本宏定义。
    /// </summary>
    /// <param name="buildTargetGroup">要检查脚本宏定义的平台。</param>
    /// <param name="scriptingDefineSymbol">要检查的脚本宏定义。</param>
    /// <returns>指定平台是否存在指定的脚本宏定义。</returns>
    public static bool HasScriptingDefineSymbol(BuildTargetGroup buildTargetGroup, string scriptingDefineSymbol)
    {
        if (string.IsNullOrEmpty(scriptingDefineSymbol))
        {
            return false;
        }

        string[] scriptingDefineSymbols = GetScriptingDefineSymbols(buildTargetGroup);
        foreach (string i in scriptingDefineSymbols)
        {
            if (i == scriptingDefineSymbol)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 为指定平台增加指定的脚本宏定义。
    /// </summary>
    /// <param name="buildTargetGroup">要增加脚本宏定义的平台。</param>
    /// <param name="scriptingDefineSymbol">要增加的脚本宏定义。</param>
    public static void AddScriptingDefineSymbol(BuildTargetGroup buildTargetGroup, string scriptingDefineSymbol)
    {
        if (string.IsNullOrEmpty(scriptingDefineSymbol))
        {
            return;
        }

        if (HasScriptingDefineSymbol(buildTargetGroup, scriptingDefineSymbol))
        {
            return;
        }

        List<string> scriptingDefineSymbols = new List<string>(GetScriptingDefineSymbols(buildTargetGroup))
            {
                scriptingDefineSymbol
            };

        SetScriptingDefineSymbols(buildTargetGroup, scriptingDefineSymbols.ToArray());
    }

    /// <summary>
    /// 为指定平台移除指定的脚本宏定义。
    /// </summary>
    /// <param name="buildTargetGroup">要移除脚本宏定义的平台。</param>
    /// <param name="scriptingDefineSymbol">要移除的脚本宏定义。</param>
    public static void RemoveScriptingDefineSymbol(BuildTargetGroup buildTargetGroup, string scriptingDefineSymbol)
    {
        if (string.IsNullOrEmpty(scriptingDefineSymbol))
        {
            return;
        }

        if (!HasScriptingDefineSymbol(buildTargetGroup, scriptingDefineSymbol))
        {
            return;
        }

        List<string> scriptingDefineSymbols = new List<string>(GetScriptingDefineSymbols(buildTargetGroup));
        while (scriptingDefineSymbols.Contains(scriptingDefineSymbol))
        {
            scriptingDefineSymbols.Remove(scriptingDefineSymbol);
        }

        SetScriptingDefineSymbols(buildTargetGroup, scriptingDefineSymbols.ToArray());
    }

    /// <summary>
    /// 为所有平台增加指定的脚本宏定义。
    /// </summary>
    /// <param name="scriptingDefineSymbol">要增加的脚本宏定义。</param>
    public static void AddScriptingDefineSymbol(string scriptingDefineSymbol)
    {
        if (string.IsNullOrEmpty(scriptingDefineSymbol))
        {
            return;
        }

        foreach (BuildTargetGroup buildTargetGroup in BuildTargetGroups)
        {
            AddScriptingDefineSymbol(buildTargetGroup, scriptingDefineSymbol);
        }
    }

    /// <summary>
    /// 为所有平台移除指定的脚本宏定义。
    /// </summary>
    /// <param name="scriptingDefineSymbol">要移除的脚本宏定义。</param>
    public static void RemoveScriptingDefineSymbol(string scriptingDefineSymbol)
    {
        if (string.IsNullOrEmpty(scriptingDefineSymbol))
        {
            return;
        }

        foreach (BuildTargetGroup buildTargetGroup in BuildTargetGroups)
        {
            RemoveScriptingDefineSymbol(buildTargetGroup, scriptingDefineSymbol);
        }
    }

    /// <summary>
    /// 获取指定平台的脚本宏定义。
    /// </summary>
    /// <param name="buildTargetGroup">要获取脚本宏定义的平台。</param>
    /// <returns>平台的脚本宏定义。</returns>
    public static string[] GetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup)
    {
        return PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';');
    }

    /// <summary>
    /// 设置指定平台的脚本宏定义。
    /// </summary>
    /// <param name="buildTargetGroup">要设置脚本宏定义的平台。</param>
    /// <param name="scriptingDefineSymbols">要设置的脚本宏定义。</param>
    public static void SetScriptingDefineSymbols(BuildTargetGroup buildTargetGroup, string[] scriptingDefineSymbols)
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, string.Join(";", scriptingDefineSymbols));
    }
}

/// <summary>
/// 日志脚本宏定义。
/// </summary>
public static class LogScriptingDefineSymbols
{
    private const string EnableLogScriptingDefineSymbol = "ENABLE_LOG";
    private const string EnableDebugAndAboveLogScriptingDefineSymbol = "ENABLE_DEBUG_AND_ABOVE_LOG";
    private const string EnableInfoAndAboveLogScriptingDefineSymbol = "ENABLE_INFO_AND_ABOVE_LOG";
    private const string EnableWarningAndAboveLogScriptingDefineSymbol = "ENABLE_WARNING_AND_ABOVE_LOG";
    private const string EnableErrorAndAboveLogScriptingDefineSymbol = "ENABLE_ERROR_AND_ABOVE_LOG";
    private const string EnableFatalAndAboveLogScriptingDefineSymbol = "ENABLE_FATAL_AND_ABOVE_LOG";
    private const string EnableDebugLogScriptingDefineSymbol = "ENABLE_DEBUG_LOG";
    private const string EnableInfoLogScriptingDefineSymbol = "ENABLE_INFO_LOG";
    private const string EnableWarningLogScriptingDefineSymbol = "ENABLE_WARNING_LOG";
    private const string EnableErrorLogScriptingDefineSymbol = "ENABLE_ERROR_LOG";
    private const string EnableFatalLogScriptingDefineSymbol = "ENABLE_FATAL_LOG";

    private static readonly string[] AboveLogScriptingDefineSymbols = new string[]
    {
            EnableDebugAndAboveLogScriptingDefineSymbol,
            EnableInfoAndAboveLogScriptingDefineSymbol,
            EnableWarningAndAboveLogScriptingDefineSymbol,
            EnableErrorAndAboveLogScriptingDefineSymbol,
            EnableFatalAndAboveLogScriptingDefineSymbol
    };

    private static readonly string[] SpecifyLogScriptingDefineSymbols = new string[]
    {
            EnableDebugLogScriptingDefineSymbol,
            EnableInfoLogScriptingDefineSymbol,
            EnableWarningLogScriptingDefineSymbol,
            EnableErrorLogScriptingDefineSymbol,
            EnableFatalLogScriptingDefineSymbol
    };

    /// <summary>
    /// 禁用所有日志脚本宏定义。
    /// </summary>
    [MenuItem("Tools/Log/禁用所有Log", false, 30)]
    public static void DisableAllLogs()
    {
        ScriptingDefineSymbols.RemoveScriptingDefineSymbol(EnableLogScriptingDefineSymbol);

        foreach (string specifyLogScriptingDefineSymbol in SpecifyLogScriptingDefineSymbols)
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(specifyLogScriptingDefineSymbol);
        }

        foreach (string aboveLogScriptingDefineSymbol in AboveLogScriptingDefineSymbols)
        {
            ScriptingDefineSymbols.RemoveScriptingDefineSymbol(aboveLogScriptingDefineSymbol);
        }
    }

    /// <summary>
    /// 开启所有日志脚本宏定义。
    /// </summary>
    [MenuItem("Tools/Log/开启所有Log", false, 31)]
    public static void EnableAllLogs()
    {
        DisableAllLogs();
        ScriptingDefineSymbols.AddScriptingDefineSymbol(EnableLogScriptingDefineSymbol);
    }

    /// <summary>
    /// 开启调试及以上级别的日志脚本宏定义。
    /// </summary>
    [MenuItem("Tools/Log/Enable Debug And Above Logs", false, 32)]
    public static void EnableDebugAndAboveLogs()
    {
        SetAboveLogScriptingDefineSymbol(EnableDebugAndAboveLogScriptingDefineSymbol);
    }

    /// <summary>
    /// 开启信息及以上级别的日志脚本宏定义。
    /// </summary>
    [MenuItem("Tools/Log/Enable Info And Above Logs", false, 33)]
    public static void EnableInfoAndAboveLogs()
    {
        SetAboveLogScriptingDefineSymbol(EnableInfoAndAboveLogScriptingDefineSymbol);
    }

    /// <summary>
    /// 开启警告及以上级别的日志脚本宏定义。
    /// </summary>
    [MenuItem("Tools/Log/Enable Warning And Above Logs", false, 34)]
    public static void EnableWarningAndAboveLogs()
    {
        SetAboveLogScriptingDefineSymbol(EnableWarningAndAboveLogScriptingDefineSymbol);
    }

    /// <summary>
    /// 开启错误及以上级别的日志脚本宏定义。
    /// </summary>
    [MenuItem("Tools/Log/Enable Error And Above Logs", false, 35)]
    public static void EnableErrorAndAboveLogs()
    {
        SetAboveLogScriptingDefineSymbol(EnableErrorAndAboveLogScriptingDefineSymbol);
    }

    /// <summary>
    /// 开启严重错误及以上级别的日志脚本宏定义。
    /// </summary>
    [MenuItem("Tools/Log/Enable Fatal And Above Logs", false, 36)]
    public static void EnableFatalAndAboveLogs()
    {
        SetAboveLogScriptingDefineSymbol(EnableFatalAndAboveLogScriptingDefineSymbol);
    }

    /// <summary>
    /// 设置日志脚本宏定义。
    /// </summary>
    /// <param name="aboveLogScriptingDefineSymbol">要设置的日志脚本宏定义。</param>
    public static void SetAboveLogScriptingDefineSymbol(string aboveLogScriptingDefineSymbol)
    {
        if (string.IsNullOrEmpty(aboveLogScriptingDefineSymbol))
        {
            return;
        }

        foreach (string i in AboveLogScriptingDefineSymbols)
        {
            if (i == aboveLogScriptingDefineSymbol)
            {
                DisableAllLogs();
                ScriptingDefineSymbols.AddScriptingDefineSymbol(aboveLogScriptingDefineSymbol);
                return;
            }
        }
    }

    /// <summary>
    /// 设置日志脚本宏定义。
    /// </summary>
    /// <param name="specifyLogScriptingDefineSymbols">要设置的日志脚本宏定义。</param>
    public static void SetSpecifyLogScriptingDefineSymbols(string[] specifyLogScriptingDefineSymbols)
    {
        if (specifyLogScriptingDefineSymbols == null || specifyLogScriptingDefineSymbols.Length <= 0)
        {
            return;
        }

        bool removed = false;
        foreach (string specifyLogScriptingDefineSymbol in specifyLogScriptingDefineSymbols)
        {
            if (string.IsNullOrEmpty(specifyLogScriptingDefineSymbol))
            {
                continue;
            }

            foreach (string i in SpecifyLogScriptingDefineSymbols)
            {
                if (i == specifyLogScriptingDefineSymbol)
                {
                    if (!removed)
                    {
                        removed = true;
                        DisableAllLogs();
                    }

                    ScriptingDefineSymbols.AddScriptingDefineSymbol(specifyLogScriptingDefineSymbol);
                    break;
                }
            }
        }
    }
}
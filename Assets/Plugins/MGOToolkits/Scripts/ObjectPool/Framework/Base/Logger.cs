using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

/// <summary>Log级别</summary>
public enum eLogLevel
{
    Error = 0,
    Assert = 1,
    Warning = 2,
    Log = 3,
    Exception = 4,
}

/// <summary>输出类型</summary>
public enum eLogOutputType
{
    Debug,
    Txt,
    All,
}

public enum eLogColor
{
    /// <summary>默认颜色</summary>
    Default,
    /// <summary>白色</summary>
    White,
    /// <summary>红色</summary>
    Red,
    /// <summary>橘色</summary>
    Orange,
    /// <summary>黄色</summary>
    Yellow,
    /// <summary>绿色</summary>
    Green,
    /// <summary>青色</summary>
    Cyan,
    /// <summary>蓝色</summary>
    Blue,
    /// <summary>紫色</summary>
    Purple,
    /// <summary>洋红</summary>
    Magenta,
    /// <summary>粉红</summary>
    Pink,
    /// <summary>橙红色</summary>
    OrangeRed,
    /// <summary>深红色</summary>
    DarkRed,
    /// <summary>无颜色</summary>
    Max,
}

public interface ILogUnit
{
}

public struct LogUnit : ILogUnit
{
    public eLogLevel LogLevel;
    public string Log;
    public string StackTrace;
    public eLogOutputType OutputType;
    public DateTime Timer;
    public eLogColor Color;
    public string SavePath;

    public LogUnit(eLogLevel logLevel, string log, string stackTrace = null, eLogOutputType outputType = eLogOutputType.All, string savePath = null, eLogColor color = eLogColor.Default)
    {
        LogLevel = logLevel;
        Log = log;
        StackTrace = stackTrace;
        OutputType = outputType;
        Timer = DateTime.Now;
        SavePath = savePath;
        Color = color;
    }
}

public class Logger
{
    /// <summary>时间格式 年/月/日 时:分:秒.毫秒</summary>
    public const string TIME_FORMAT_YMDHMSF = "yyyy/MM/dd HH:mm:ss.fff";
    /// <summary>时间格式 年-月-日 时:分:秒.毫秒</summary>
    public const string TIME_FORMAT_YMDHMSF_1 = "yyyy-MM-dd HH:mm:ss.fff";
    /// <summary>时间格式 年_月_日_时_分_秒</summary>
    public const string TIME_FORMAT_YMDHMS = "yyyy_MM_dd_HH_mm_ss";
    /// <summary>时间格式 年月日时分秒</summary>
    public const string TIME_FORMAT_YMDHMS_1 = "yyyyMMddHHmmss";
    /// <summary>时间格式 年月日时分秒</summary>
    public const string TIME_FORMAT_YMDHMS_2 = "yyyy/MM/dd HH:mm:ss";
    /// <summary>时间格式 时:分:秒.毫秒</summary>
    public const string TIME_FORMAT_HMSF = "HH:mm:ss.fff";
    /// <summary>时间格式 年/月/日</summary>
    public const string TIME_FORMAT_YMD = "yyyy/MM/dd";
    /// <summary>时间格式 年_月_日</summary>
    public const string TIME_FORMAT_YMD_1 = "yyyy_MM_dd";
    /// <summary>时间格式 年-月-日</summary>
    public const string TIME_FORMAT_YMD_2 = "yyyy-MM-dd";
    /// <summary>时间格式 年月日</summary>
    public const string TIME_FORMAT_YMD_3 = "yyyyMMdd";

    /// <summary>Debug</summary>
    public const string TAG_LOG = "[D]";
    /// <summary>Warning</summary>
    public const string TAG_WARNING = "[W]";
    /// <summary>Error</summary>
    public const string TAG_ERROR = "[E]";
    /// <summary>左标志符</summary>
    public const string TAB_LEFT_FLAG = "[";
    /// <summary>标志位</summary>
    public const string TAB_SPACE = " ";
    /// <summary>右标志符</summary>
    public const string TAB_RIGHT_FLAG = "]";

    /// <summary>日志是否加密</summary>
    public const bool LOG_ENCRYPT = false;

    /// <summary>颜色配置字典</summary>
    public static readonly Dictionary<int, string> LogColor = new Dictionary<int, string>
        {
            { (int)eLogColor.White,     string.Intern("FFFFFF")},
            { (int)eLogColor.Red,       string.Intern("FF0000")},
            { (int)eLogColor.Orange,    string.Intern("FF9933")},
            { (int)eLogColor.Yellow,    string.Intern("FFFF00")},
            { (int)eLogColor.Green,     string.Intern("00FF00")},
            { (int)eLogColor.Cyan,      string.Intern("00FFFF")},
            { (int)eLogColor.Blue,      string.Intern("1E90FF")},
            { (int)eLogColor.Purple,    string.Intern("E066FF")},
            { (int)eLogColor.Magenta,   string.Intern("FF1493")},
            { (int)eLogColor.OrangeRed, string.Intern("FF4500")},
            { (int)eLogColor.DarkRed,   string.Intern("8B0000")},
            { (int)eLogColor.Pink,      string.Intern("FFC0CB")}
        };

    /// <summary>是否激活日志</summary>
    public static bool LogEnable { get; private set; }
    /// <summary>log文件全路径</summary>
    public static string LogPath { get; private set; }
    /// <summary>log文件夹</summary>
    public static string LogDirectory { get; private set; }

    /// <summary>线程锁定标记</summary>
    private static readonly object _thread_lock = new object();
    /// <summary>写入线程</summary>
    private static Thread _workThread;
    private static AutoResetEvent _autoResetEvent;
    /// <summary>正在写入的队列</summary>
    private static Queue<LogUnit> _logWaitingQueue = new Queue<LogUnit>();

    #region StringBuilder
    /// <summary>StringBuilder最多创建实例数</summary>
    private const int MAX_INSTANCE = 20;
    /// <summary>StringBuilder默认内容长度</summary>
    private const int DEFAULT_CAPACITY = 256;
    /// <summary>StringBuilder实例索引</summary>
    [ThreadStatic]
    private static int _stringBuilderIndex;
    /// <summary>StringBuilder数组</summary>
    [ThreadStatic]
    private static StringBuilder[] _stringBuilderArr;
    #endregion

    private static eLogOutputType _limitOutputType = eLogOutputType.Txt;

    /// <summary>初始化</summary>
    /// <param name="logDirectory">文件夹</param>
    public static void Init(string logDirectory, bool enable, eLogOutputType outputType = eLogOutputType.Txt)
    {
        _limitOutputType = outputType;
        SetEnable(enable);
        LogDirectory = logDirectory;
        LogPath = Path.Combine(LogDirectory, DateTime.Now.ToString(TIME_FORMAT_YMDHMS));
#if !CSHARP_LOGIC
        UnityEngine.Application.logMessageReceived += onLogMessageReceivedHandler;
#if UNITY_EDITOR
        UnityEngine.Debug.Log($"logPath:{LogPath}");
        UnityEditor.EditorApplication.playModeStateChanged -= onPlayModeStateChangedHandler;
        UnityEditor.EditorApplication.playModeStateChanged += onPlayModeStateChangedHandler;
#endif
#endif
    }

    public static void SetEnable(bool enable)
    {
        LogEnable = enable;
        if (_workThread != null)
        {
            try
            {
                _workThread.Abort();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                _workThread = null;
            }
        }

        if (!LogEnable)
            return;

        _autoResetEvent = new AutoResetEvent(false);

        _workThread = new Thread(new ThreadStart(onThreadLogHandler));
        _workThread.IsBackground = true;
        _workThread.Start();
    }

    private static StringBuilder GetStringBuilder()
    {
        if (_stringBuilderArr == null)
        {
            _stringBuilderArr = new StringBuilder[MAX_INSTANCE];
            _stringBuilderIndex = 0;
        }

        StringBuilder instance = _stringBuilderArr[_stringBuilderIndex];
        if (instance == null)
        {
            instance = new StringBuilder(DEFAULT_CAPACITY);
            _stringBuilderArr[_stringBuilderIndex] = instance;
        }
        instance.Length = 0;

        _stringBuilderIndex++;
        if (_stringBuilderIndex >= MAX_INSTANCE)
            _stringBuilderIndex = 0;

        return instance;
    }

    #region Debug
    public static void Debug(string log, eLogColor logColor = eLogColor.White, eLogOutputType outputType = eLogOutputType.All, string path = null, bool stackTrace = false)
    {
#if !LOG_ONLY_EXCEPTION && DEBUG_LOG || UNITY_EDITOR
        WriteLog(log, !stackTrace ? string.Empty : new System.Diagnostics.StackTrace().ToString(), eLogLevel.Log, outputType, logColor, path);
#endif
    }

    public static void Debug(string log, eLogLevel logLevel, eLogOutputType outputType = eLogOutputType.All, string path = null, bool stackTrace = false)
    {
        switch (logLevel)
        {
            case eLogLevel.Error:
                Error(log, eLogColor.Red, outputType, path, stackTrace);
                break;
            case eLogLevel.Exception:
                Error(log, eLogColor.Red, outputType, path, stackTrace);
                break;
            case eLogLevel.Log:
                Debug(log, eLogColor.White, outputType, path, stackTrace);
                break;
            case eLogLevel.Warning:
                Warning(log, eLogColor.Yellow, outputType, path, stackTrace);
                break;
            case eLogLevel.Assert:
                Error(log, eLogColor.Red, outputType, path, stackTrace);
                break;
        }
    }
    #endregion

    #region Warning
    public static void Warning(string log, eLogColor logColor = eLogColor.Yellow, eLogOutputType outputType = eLogOutputType.All, string path = null, bool stackTrace = false)
    {
#if !LOG_ONLY_EXCEPTION && DEBUG_LOG || UNITY_EDITOR
        WriteLog(log, !stackTrace ? string.Empty : new System.Diagnostics.StackTrace().ToString(), eLogLevel.Warning, outputType, logColor, path);
#endif
    }
    #endregion

    #region Error
    public static void Error(string log, eLogColor logColor = eLogColor.Red, eLogOutputType outputType = eLogOutputType.All, string path = null, bool stackTrace = false)
    {
#if DEBUG_LOG || LOG_ONLY_EXCEPTION || UNITY_EDITOR
        WriteLog(log, !stackTrace ? string.Empty : new System.Diagnostics.StackTrace().ToString(), eLogLevel.Error, outputType, logColor, path);
#endif
    }
    #endregion

    #region WriteLog
    public static void WriteLog(string log, string stackTrace = null, eLogLevel logLevel = eLogLevel.Log, eLogOutputType outputType = eLogOutputType.All, eLogColor logColor = eLogColor.Default, string savePath = null)
    {
#if DEBUG_LOG || LOG_ONLY_EXCEPTION || UNITY_EDITOR
        WriteLog(new LogUnit(logLevel, log, stackTrace, outputType, savePath, logColor));
#endif
    }

    public static void WriteLog(LogUnit logUnit)
    {
#if DEBUG_LOG || LOG_ONLY_EXCEPTION || UNITY_EDITOR
        if (!LogEnable)
            return;
        if (string.IsNullOrEmpty(logUnit.Log))
            return;
#if !CSHARP_LOGIC
        if (_limitOutputType != eLogOutputType.Txt && (logUnit.OutputType == eLogOutputType.Debug || logUnit.OutputType == eLogOutputType.All))
        {
            StringBuilder stringBuilder = GetStringBuilder();
            if (logUnit.Color != eLogColor.Default)
            {
                stringBuilder.Append("<color=#");
                stringBuilder.Append(LogColor[(int)logUnit.Color]);
                stringBuilder.Append(">");
            }

#if !UNITY_2019_1_OR_NEWER
                stringBuilder.Append("[");
                stringBuilder.Append(logUnit.Timer.ToString(TIME_FORMAT_HMSF));
                stringBuilder.Append("] ");
#else
            stringBuilder.Append("[");
            stringBuilder.Append(logUnit.Timer.ToString("fff"));
            stringBuilder.Append("] ");
#endif

            if (logUnit.Color != eLogColor.Default)
            {
                StringBuilder temp = GetStringBuilder();
                temp.Append("\r</color><color=#");
                temp.Append(LogColor[(int)logUnit.Color]);
                temp.Append(">");
                stringBuilder.Append(logUnit.Log.Replace("\r", temp.ToString()));
                if (!string.IsNullOrEmpty(logUnit.StackTrace))
                {
                    stringBuilder.AppendLine();
                    stringBuilder.Append(logUnit.StackTrace.Replace("\r", temp.ToString()));
                }
                stringBuilder.Append("</color>");
            }
            else
            {
                stringBuilder.Append(logUnit.Log);
                if (!string.IsNullOrEmpty(logUnit.StackTrace))
                {
                    stringBuilder.AppendLine();
                    stringBuilder.Append(logUnit.StackTrace);
                }
            }

            switch (logUnit.LogLevel)
            {
                case eLogLevel.Log:
                    UnityEngine.Debug.Log(stringBuilder.ToString());
                    break;
                case eLogLevel.Warning:
                    UnityEngine.Debug.LogWarning(stringBuilder.ToString());
                    break;
                case eLogLevel.Error:
                    UnityEngine.Debug.LogError(stringBuilder.ToString());
                    break;
                case eLogLevel.Exception:
                    UnityEngine.Debug.LogError(stringBuilder.ToString());
                    break;
            }
        }
#endif

        if (logUnit.OutputType == eLogOutputType.Debug)
            return;

        _logWaitingQueue.Enqueue(logUnit);
        _autoResetEvent.Set();//开启绿灯
#endif
    }
    #endregion

    #region WriteToLocalTxt
    /// <summary>将日志同步写入本地文件 **此接口直接在主线程执行，非必要不要使用** </summary>
    /// <param name="msg">日志</param>
    /// <param name="logFileName">日志文件名</param>
    public static void SyncWriteToLocalTxt(string msg, bool hasStackTrace = true, string logFileName = "logger.txt")
    {
#if DEBUG_LOG || UNITY_EDITOR
#if !CSHARP_LOGIC
        UnityEngine.Debug.Log($"[SyncWriteToLocalTxt] {msg}");
#endif
        StringBuilder stringBuilder = GetStringBuilder();
        stringBuilder.AppendLine(msg);
        if (hasStackTrace)
            stringBuilder.AppendLine(new System.Diagnostics.StackTrace().ToString());
        WriteToLocalTxt(stringBuilder.ToString(), Path.Combine(LogDirectory, logFileName));
#endif
    }

    /// <summary>将日志写入本地文件</summary>
    /// <param name="stringBuilder">日志</param>
    /// <param name="logPath">日志文件路径</param>
    public static void WriteToLocalTxt(StringBuilder stringBuilder, string logPath)
    {
        if (stringBuilder == null || stringBuilder.Length == 0)
            return;

        WriteToLocalTxt(stringBuilder.ToString(), logPath);
    }

    /// <summary>将日志写入本地文件</summary>
    /// <param name="msg">日志</param>
    /// <param name="logPath">日志文件路径</param>
    public static void WriteToLocalTxt(string msg, string logPath)
    {
        if (string.IsNullOrEmpty(logPath))
            logPath = LogPath;

        if (string.IsNullOrEmpty(logPath))
        {
            throw new System.ArgumentNullException("日志路径不能为空！");
        }

        if (!File.Exists(logPath))
        {
            string directoryPath = Path.GetDirectoryName(logPath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        using (StreamWriter writer = new StreamWriter(logPath, true, Encoding.UTF8))
        {
            writer.Write(msg);
            writer.Flush();
            writer.Close();
        }
    }
    #endregion

    private static void onThreadLogHandler()
    {
        while (LogEnable)
        {
            _autoResetEvent.WaitOne();//等待信号（开启绿灯）
            try
            {
                while (_logWaitingQueue.Count > 0)
                {
                    StringBuilder stringBuilder = GetStringBuilder();
                    LogUnit logUnit = _logWaitingQueue.Dequeue();
                    LogFormat(ref stringBuilder, logUnit.LogLevel, logUnit.Log.TrimStart(), logUnit.StackTrace, logUnit.Timer.ToString(TIME_FORMAT_HMSF));
                    WriteToLocalTxt(stringBuilder.ToString(), null);
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            _autoResetEvent.Reset();//开启红灯
            Thread.Sleep(1);
        }
    }

    /// <summary>格式化log</summary>
    /// <param name="stringBuilder">格式化日志存储器</param>
    /// <param name="logType">日志类型</param>
    /// <param name="msg">日志</param>
    /// <param name="stackTrace">调用堆栈</param>
    /// <param name="time">产生时间</param>
    private static void LogFormat(ref StringBuilder stringBuilder, eLogLevel logType, string msg, string stackTrace = null, string time = null)
    {
        if (stringBuilder == null)
            stringBuilder = GetStringBuilder();

        switch (logType)
        {
            case eLogLevel.Log:
                stringBuilder.Append(TAG_LOG);
                break;
            case eLogLevel.Warning:
                stringBuilder.Append(TAG_WARNING);
                break;
            case eLogLevel.Error:
                stringBuilder.Append(TAG_ERROR);
                break;
        }

        stringBuilder.Append(TAB_LEFT_FLAG);
        stringBuilder.Append(!string.IsNullOrEmpty(time) ? time : DateTime.Now.ToString(TIME_FORMAT_HMSF));
        stringBuilder.Append(TAB_RIGHT_FLAG);

        stringBuilder.Append(TAB_SPACE);
        stringBuilder.AppendLine(msg.TrimStart());

        if (!string.IsNullOrEmpty(stackTrace))
            stringBuilder.AppendLine(stackTrace);
        stringBuilder.AppendLine("");
    }




#if !CSHARP_LOGIC
    private static void onLogMessageReceivedHandler(string condition, string stackTrace, UnityEngine.LogType type)
    {
#if !UNITY_EDITOR
        if (type != UnityEngine.LogType.Exception && type != UnityEngine.LogType.Error)
            return;
        WriteLog(condition, stackTrace, eLogLevel.Exception, eLogOutputType.Txt, eLogColor.Default);
#else
        if (type == UnityEngine.LogType.Exception)
        {
            UnityEditor.EditorApplication.isPaused = true;
        }
#endif
    }

#if UNITY_EDITOR
    private static void onPlayModeStateChangedHandler(UnityEditor.PlayModeStateChange playModeStateChange)
    {
        if (playModeStateChange == UnityEditor.PlayModeStateChange.ExitingPlayMode)
        {
            Debug("日志线程被终止!!!!!!");
            if (_workThread != null)
            {
                _workThread.Abort();
            }
            _workThread = null;
        }
    }
#endif
#endif
}




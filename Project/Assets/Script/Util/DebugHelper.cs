using System;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


/// <summary>
/// 封装Debugger
/// </summary>

public sealed class DebugHelper
{
    public static StringBuilder strBuilder = new StringBuilder(1024);
    
    ///<summary>
    ///Logs a message to the Unity Console.
    ///</summary>
    public static void Log(string message, params object[] args)
    {
#if DEBUGER
        if (args.Length <= 0)
        {
            Debug.Log(message);
        }
        else
        {
            strBuilder.AppendFormat(message, args);
            Debug.Log(strBuilder.ToString());
            strBuilder.Length = 0;
        }
#endif
    }

    ///<summary>
    ///A variant of Debug.Log that logs an assertion message to the console.
    ///</summary>
    public static void LogAssertion(object message,UnityEngine.Object context = null)
    {
#if DEBUGER
        Debug.LogAssertion(DateTime.Now.ToString("yyyy:mm:dd HH:mm:ss:ffff") + " " + message, context);
#endif
    }

    ///<summary>
    ///Logs a formatted assertion message to the Unity console.
    ///</summary>
    public static void LogAssertionFormat(string format, params object[] args)
    {
#if DEBUGER
        Debug.LogAssertionFormat(format, args);
#endif
    }
    ///<summary>
    ///Logs a formatted assertion message to the Unity console.
    ///</summary>
    public static void LogAssertionFormat(UnityEngine.Object context, string format, params object[] args)
    {
#if DEBUGER
        Debug.LogAssertionFormat(context, format, args);
#endif
    }

    ///<summary>
    ///A variant of Debug.Log that logs an error message to the console.
    ///</summary>
    public static void LogError(string message, params object[] args)
    {
#if DEBUGER
        if (args.Length <= 0)
        {
            Debug.LogError(message);
        }
        else
        {
            strBuilder.AppendFormat(message, args);
            Debug.LogError(strBuilder.ToString());
            strBuilder.Length = 0;
        }
        
#endif
    }

    ///<summary>
    ///Logs a formatted error message to the Unity console.
    ///</summary>
    public static void LogErrorFormat(string format, params object[] args)
    {
#if DEBUGER
        Debug.LogErrorFormat(format, args);
#endif
    }
    ///<summary>
    ///Logs a formatted error message to the Unity console.
    ///</summary>
    public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
    {
#if DEBUGER
        Debug.LogErrorFormat(context, format, args);
#endif
    }

    ///<summary>
    ///A variant of Debug.Log that logs an error message to the console.
    ///</summary>
    public static void LogException(Exception exception, UnityEngine.Object context = null)
    {
#if DEBUGER
        Debug.LogException(exception, context);
#endif
    }

    ///<summary>
    ///Logs a formatted message to the Unity Console.
    ///</summary>
    public static void LogFormat(string format, params object[] args)
    {
#if DEBUGER
        Debug.LogFormat(format, args);
#endif
    }
    ///<summary>
    ///Logs a formatted message to the Unity Console.
    ///</summary>
    public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
    {
#if DEBUGER
        Debug.LogFormat(context, format, args);
#endif
    }

    ///<summary>
    ///A variant of Debug.Log that logs a warning message to the console.
    ///</summary>
    public static void LogWarning(string message, params object[] args)
    {
#if DEBUGER
        if (args.Length <= 0)
        {
            Debug.LogWarning(message);
        }
        else
        {
            strBuilder.AppendFormat(message, args);
            Debug.LogWarning(strBuilder.ToString());
            strBuilder.Length = 0;
        }
        
#endif
    }

    ///<summary>
    ///Logs a formatted warning message to the Unity Console.
    ///</summary>
    ///
    public static void LogWarningFormat(string format, params object[] args)
    {
#if DEBUGER
        Debug.LogWarningFormat(format, args);
#endif
    }
    ///<summary>
    ///Logs a formatted warning message to the Unity Console.
    ///</summary>
    public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
    {
#if DEBUGER
        Debug.LogWarningFormat(context, format, args);
#endif
    }

#if UNITY_EDITOR
    [UnityEditor.Callbacks.OnOpenAsset(0)]
    private static bool OnOpenAsset(int instanceId, int line)
    {
        string stackTrace = GetStackTrace();
        if (!lockConsole && !string.IsNullOrEmpty(stackTrace) && stackTrace.Contains("DebugHelper:Log"))
        {
            Match matches = Regex.Match(stackTrace, @"\(at (.+)\)", RegexOptions.IgnoreCase);
            while (matches.Success)
            {
                var pathline = matches.Groups[1].Value;
                if (!pathline.Contains("DebugHelper.cs"))
                {
                    int splitIndex = pathline.LastIndexOf(":");
                    string path = pathline.Substring(0, splitIndex);
                    line = Convert.ToInt32(pathline.Substring(splitIndex + 1));
                    lockConsole = true;
                    AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<MonoScript>(path), line, 0);
                    lockConsole = false;
                    break;
                }
                matches = matches.NextMatch();
            }
            return true;
        }
        return false;
    }
    //调用OpenAsset的时候会再次触发OnOpenAsset函数，从而引起StackOverFlow，所以需要一个标志位中断这个循环
    private static bool lockConsole = false;

    private static string GetStackTrace()
    {
        var consoleWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
        var fieldInfo = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
        var consoleWindowInstance = fieldInfo.GetValue(null);
        if (consoleWindowInstance != null)
        {
            if ((object)EditorWindow.focusedWindow == consoleWindowInstance)
            {
                var ListViewStateType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ListViewState");
                fieldInfo = consoleWindowType.GetField("m_ListView", BindingFlags.Instance | BindingFlags.NonPublic);
                var listView = fieldInfo.GetValue(consoleWindowInstance);
                fieldInfo = ListViewStateType.GetField("row", BindingFlags.Instance | BindingFlags.Public);
                int row = (int)fieldInfo.GetValue(listView);
                fieldInfo = consoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
                string activeText = fieldInfo.GetValue(consoleWindowInstance).ToString();
                return activeText;
            }
        }

        return null;
    }


#endif
}

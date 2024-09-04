using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEX
{
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Log(params object[] ob)
    {
        Debug.Log(GetLog(ob));
    }


    public static void LogFrameworkMsg(params object[] ob)
    {
        LogColor(new Color(1, 165f/255f, 56f/255f), "Framework ▷▶",ob);
    }

    public static void LogColor(Color color, string title, params object[] ob)
    {
        color = color == null ? Color.white : color;
        string colHtmlString = ColorUtility.ToHtmlStringRGB(color);
        string colorTagStart = "<color=#{0}>";
        string colorTagEnd = "</color>";
        var msg = string.Format(colorTagStart, colHtmlString) + title + colorTagEnd;
        var log = GetLog(ob);
        Debug.Log(msg + log);
    }

    public static void Log(object message, Color color)
    {
        color = color == null ? Color.white : color;
        string colHtmlString = ColorUtility.ToHtmlStringRGB(color);
        string msg = message.ToString();
        string colorTagStart = "<color=#{0}>";
        string colorTagEnd = "</color>";
        msg = string.Format(colorTagStart, colHtmlString) + msg + colorTagEnd;
        Debug.Log(GetLog(msg));
    }

    public static void LogSuccess(params object[] ob)
    {
        Debug.Log("<color=#88e900>√</color>" + GetLog(ob));
    }

    public static void LogError(params object[] ob)
    {
        Debug.LogError(GetLog(ob));
    }

    public static void LogWarrning(params object[] ob)
    {
        Debug.LogWarning(GetLog(ob));
    }

    public static string GetLog(params object[] ob) 
    {
        string log = "";
        for (int i = 0; i < ob.Length; i++)
        {
            if (ob[i] is string[])
            {
                for (int j = 0; j < (ob[i] as string[]).Length; j++)
                {
                    log += ("" + " <color=#797673>□</color> " + (ob[i] as string[])[j]);
                }
            }
            else if (ob[i] is int[])
            {
                for (int j = 0; j < (ob[i] as int[]).Length; j++)
                {
                    log += (" " + " <color=#797673>□</color> " + (ob[i] as int[])[j]);
                }
            }
            else if (ob[i] is float[])
            {
                for (int j = 0; j < (ob[i] as float[]).Length; j++)
                {
                    log += ("" + " <color=#797673>□</color> " + (ob[i] as float[])[j]);
                }
            }
            else if (ob[i] is List<string>)
            {
                for (int j = 0; j < (ob[i] as List<string>).Count; j++)
                {
                    log += ("" + " <color=#797673>□</color> " + (ob[i] as List<string>)[j]);
                }
            }
            else if (ob[i] is List<int>)
            {
                for (int j = 0; j < (ob[i] as List<int>).Count; j++)
                {
                    log += ("" + " <color=#797673>□</color> " + (ob[i] as List<int>)[j]);
                }
            }
            else if (ob[i] is List<float>)
            {
                for (int j = 0; j < (ob[i] as List<float>).Count; j++)
                {
                    log += ("" + " <color=#797673>□</color> " + (ob[i] as List<float>)[j]);
                }
            }
            else
            {
                if (i == 0)
                { 
                    log += " " + ob[i]+" ";
                    continue;
                }

                log += (" <color=#797673>□</color> " + ob[i]);
            }
        }

        return log;
    }


}
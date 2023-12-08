using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEX
{
    public static void Log(params object[] ob)
    {
        Debug.Log(GetLog(ob));
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
                    log += (" >>>" + "[" + j + "] " + (ob[i] as string[])[j]);
                }
            }
            else if (ob[i] is int[])
            {
                for (int j = 0; j < (ob[i] as int[]).Length; j++)
                {
                    log += (" >>>" + "[" + j + "] " + (ob[i] as int[])[j]);
                }
            }
            else if (ob[i] is float[])
            {
                for (int j = 0; j < (ob[i] as float[]).Length; j++)
                {
                    log += (" >>>" + "[" + j + "] " + (ob[i] as float[])[j]);
                }
            }
            else if (ob[i] is List<string>)
            {
                for (int j = 0; j < (ob[i] as List<string>).Count; j++)
                {
                    log += (" >>>" + "[" + j + "] " + (ob[i] as List<string>)[j]);
                }
            }
            else if (ob[i] is List<int>)
            {
                for (int j = 0; j < (ob[i] as List<int>).Count; j++)
                {
                    log += (" >>>" + "[" + j + "] " + (ob[i] as List<int>)[j]);
                }
            }
            else if (ob[i] is List<float>)
            {
                for (int j = 0; j < (ob[i] as List<float>).Count; j++)
                {
                    log += (" >>>" + "[" + j + "] " + (ob[i] as List<float>)[j]);
                }
            }
            else
            {
                log += (" >>>" + ob[i]);
            }
        }

        return log;
    }


}
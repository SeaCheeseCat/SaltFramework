using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonMethod
{

    /// <summary> 
    /// 获取时间戳 
    /// </summary> 
    /// <returns>UTC</returns> 
    public static long GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds);
    }


    /// <summary>
    /// 秒数转换为XX小时XX分钟XX秒
    /// </summary>
    /// <param name="ts"></param>
    /// <returns></returns>
   /* public static string SecondToString(int sec)
    {
        TimeSpan ts = new TimeSpan(0, 0, sec);
        if (sec < 60)
        {
            return sec + TextManager.GetText(TextNameToID.Second);
        }
        else if (sec >= 60 && sec < 3600)
        {
            if(ts.Seconds>0)
            {
                return ts.Minutes + TextManager.GetText(TextNameToID.Minute) + ts.Seconds + TextManager.GetText(TextNameToID.Second);
            }else
            {
                return ts.Minutes + TextManager.GetText(TextNameToID.Minute);
            }
        }
        else if (sec >= 3600)
        {
            if (ts.Minutes > 0)
            {
                return ts.Hours + TextManager.GetText(TextNameToID.Hour) + ts.Minutes + TextManager.GetText(TextNameToID.Minute);
            }
            else
            {
                return ts.Hours + TextManager.GetText(TextNameToID.Hour);
            }
        }
        return ts.Seconds + TextManager.GetText(TextNameToID.Minute);
    }*/



    /// <summary>
    /// 获取指定范围内不重复的随机数
    /// </summary>
    /// <param name="num"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Hashtable GetHashtableRandomNum(int num, int min, int max)
    {
        System.Random random = new System.Random();
        Hashtable hashtable = new Hashtable();
        for (int i = 0; hashtable.Count < num; i++)
        {
            int nValue = random.Next(min, max);

            if (!hashtable.ContainsValue(nValue))
            {
                hashtable.Add(i, nValue);
            }
        }
        return hashtable;
    }
}

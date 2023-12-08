
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static void RandomSortArray <T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int index = Random.Range (0, array.Length);
            T tmp = array[i];
            array[i] = array[index];
            array[index] = tmp;
        }
    }

    public static void RandomSortList<T> (List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int index = Random.Range (0, list.Count);
            T tmp = list[i];
            list[i] = list[index];
            list[index] = tmp;
        }
    }

    public static T RandomInArray<T> (T[] array)
    {
        int index = Random.Range (0, array.Length);
        return array[index];
    }

    public static T RandomInList<T> (List<T> list)
    {
        int index = Random.Range (0, list.Count);
        return list[index];
    }

    public static int RandomByWeight (int[] weights)
    {
        int total = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            total += weights[i];
        }

        int ran = Random.Range (0, total);

        int sum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
            if (ran < sum)
            {
                return i;
            }
        }
        return weights.Length - 1;
    }

    /// <summary>
    /// 数值向上浮动
    /// </summary>
    /// <param name="v"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static int RandomCeilingRT(int v,int c)
    {
        return v + Random.Range(0, c);
    }

    /// <summary>
    /// 随机值浮动百分比
    /// </summary>
    /// <param name="v"></param>
    /// <param name="perc"></param>
    /// <returns></returns>
    public static int RandomFRT(float v,float perc)
    {
        v = v * (1 + Random.Range(-perc, perc));
        return Mathf.RoundToInt(v);
    }

    /// <summary>
    /// 将大数字转为带k的数字
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetHugeValueNum(int value)
    {
        if (value >= 9999)
        {
            return Mathf.Round(value / 100) / 10f + "<color=FFE700>K</color>";
        }
        else
        {
            return value.ToString();
        }
    }

    /// <summary>
    /// 判断数字是否在某个区间内
    /// </summary>
    /// <param name="v"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool ValueInRange(float v,float min,float max)
    {
        return v >= min && v <= max;
    }

    /// <summary>
    /// 判断数字是否在某个区间内，单项浮动版
    /// </summary>
    /// <param name="v"></param>
    /// <param name="center"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool ValueInRangeCenter(float v, float center, float p)
    {
        return v >= center - p && v <= center + p;
    }
}
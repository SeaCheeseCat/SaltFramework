using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ToolEx
{

    /// <summary>
    /// 截取字符串的一段字符
    /// 比如要找 “今天是星期三” 字符串中的“星期”两个字符
    /// s = “今天是星期三”；
    /// s1 = “是”；
    /// s2 = “三” ；
    /// 输出结果就是 “星期”
    /// </summary>
    /// <param name="s"></param>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static (string content,int start,int end) Search_string(string s, string s1, string s2)
    {
        DebugEX.Log(s, s1, s2);
        int n1, n2;
        n1 = s.IndexOf(s1, 0) + s1.Length;
        n2 = s.IndexOf(s2, n1);
        return (s.Substring(n1, n2 - n1), s.IndexOf(s1, 0), s.IndexOf(s2, 0)+s2.Length);
    }


    public static float[] ConvertArray(float[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = -array[i];
        }
        return array;
    
    }

    /// <summary>
    /// ��ȡԤ������Դ·����
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static string GetPrefabAssetPath(this GameObject gameObject)
    {
        // Project�е�Prefab��Asset����Instance


#if UNITY_EDITOR
        if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gameObject))
        {
            // Ԥ������Դ��������
            return UnityEditor.AssetDatabase.GetAssetPath(gameObject);
        }

        // Scene�е�Prefab Instance��Instance����Asset
        if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(gameObject))
        {
            // ��ȡԤ������Դ
            var prefabAsset = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
            return UnityEditor.AssetDatabase.GetAssetPath(prefabAsset);
        }

        // PrefabMode�е�GameObject�Ȳ���InstanceҲ����Asset
        var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetPrefabStage(gameObject);
        if (prefabStage != null)
        {
            // Ԥ������Դ��prefabAsset = prefabStage.prefabContentsRoot
            return prefabStage.prefabAssetPath;
        }
#endif
        // ����Ԥ����  //�Ǳ༭��״̬�� ����
        return null;
    }

    /// <summary>
    /// ֻ���������Scale�е�X��ֵ
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void SetScaleX(this Transform transform, float x) {
        transform.localScale = new Vector3(x,transform.localScale.y,transform.localScale.z); 
    
    }


    public static string ToStringFech(this string[] strs)
    {
        var nstr = "";
        foreach (var st in strs)
        {
            nstr = nstr+"    " + st;
        
        }

        return nstr;
    
    }




    
    ///颜色解析
    public static Color CreateColor(int r, int g, int b, float alpha = 1)
    {
        return new Color(r / 255f, g / 255f, b / 255f, alpha);
    }

    public static Color CreateColor(int hex, float alpha = 1)
    {
        hex &= 0xFFFFFF;
        return CreateColor(hex >> 16, (hex & 0xFFFF) >> 8, hex & 0xFF, alpha);
    }

    /// <summary>
    /// 返回  通过十六进制的  数额  比如#2D34D  会返回正常能解析的color
    /// </summary>
    /// <param name="hex"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public static Color IntoColor(this int hex, float alpha = 1)
    {
        hex &= 0xFFFFFF;
        return CreateColor(hex >> 16, (hex & 0xFFFF) >> 8, hex & 0xFF, alpha);
    }



    /// <summary>
    /// 传入一个文本  替换参数   比如  #008ED4  它会回传 拼接好的字符     <color=#008ED4>内容 </color>
    /// </summary>
    /// <param name="content"></param>
    /// <param name="hexcolor"></param>
    /// <returns></returns>
    public static string ToHtmlColor(this string content,string hexcolor) {

        return "<color=" + hexcolor + ">"+content+"</color>";
    
    }


    /// <summary>
    /// 获取某个字符串中 某个字符的个数
    /// </summary>
    /// <param name="content">总字符串</param>
    /// <param name="str">字符</param>
    /// <returns></returns>
    public static int GetNumChara(string content,char str) {
        var num = 0;
        foreach (char c in content)
        {
            if (c==str)
                num++;
        }
        return num;
    }




    public static int[] ToIntArray(this string[] str) {
        int[] intarray = new int[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            var item = str[i];
            intarray[i] = int.Parse(item);
        }
        return intarray;
    }






    /// <summary>
    /// 判断任何两个疑点是否  符合目标值
    /// 比如 1 2  和  2 1   是否符合  1 2
    /// </summary>
    /// <param name="point1">疑点1</param>
    /// <param name="point2">疑点2</param>
    /// <param name="trigger1">目标值1</param>
    /// <param name="trigger2">目标值2</param>
    /// <returns></returns>
    public static bool IsTriggerNum(int point1, int point2, int trigger1, int trigger2)
    {
        if ((point1 == trigger1 && point2 == trigger2) || (point2 == trigger1 && point1 == trigger2))
            return true;
        return false;
    }


    public static void IncreaseAnimInit(int startValue, float targetValue, Text useTxt)
    {
        var se = DOTween.Sequence();
        se.Append(DOTween.To(delegate (float value)
        {
            var temp = Mathf.FloorToInt(value);
            useTxt.text = temp.ToString();
        }, startValue, targetValue, 2f));
    }

    public static void IncreaseAnimProgress(int startValue, int targetValue, Text useTxt, Action action, float time = 1f)
    {
        var se = DOTween.Sequence();
        se.Append(DOTween.To(delegate (float value)
        {
            var temp = Mathf.FloorToInt(value);
            useTxt.text = temp.ToString() + "%";
        }, startValue, targetValue, time)).OnComplete(()=> {
            action?.Invoke();
        });
    }

    public static void IncreaseAnimFloat(float startValue, float targetValue, Text useTxt)
    {
        var se = DOTween.Sequence();
        se.Append(DOTween.To(delegate (float value)
        {
            //var temp = Mathf.FloorToInt(value);
            useTxt.text = value.ToString("0.00");

        }, startValue, targetValue, 2f));
    }

    /// <summary>
    /// 两个点的角度
    /// 0°正右
    /// 90°正上
    /// -90°正下
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static float PointToAngle(Vector2 p1, Vector2 p2)
    {
        Vector2 p;
        p.x = p2.x - p1.x;
        p.y = p2.y - p1.y;
        return Mathf.Atan2(p.y, p.x) * 180 / Mathf.PI;
    }

    /// <summary>
    /// 将一个 0-180度 以及 0- -180度的坐标转换为 360度
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float ConvertTo360(float angle)
    {
        if (angle > 0 && angle <= 180)
        {
            return 360 - angle ;
        }
        else if (angle <= 0 && angle >= -180)
        {
            return MathF.Abs(angle);
        }
        else
        {
            throw new ArgumentOutOfRangeException("angle", "Angle must be between -180 and 180 degrees.");
        }
    }

    /// <summary>
    /// 将一个  360度    转回去0-180度 以及 0- -180度的坐标
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float ConvertTo180(float angle)
    {
        if (angle >= 0 && angle <= 180)
        {
            return -angle;
        }
        else if (angle < 360 && angle > 180)
        {
            return 180 -angle;
        }
        else
        {
            throw new ArgumentOutOfRangeException("angle", "Angle must be between -180 and 180 degrees.");
        }
    }


    /// <summary>
    /// 将一个  360度    转回去0-180度 以及 0- -180度的坐标
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float ConvertToAmount180(float angle)
    {
        return angle - 90;
    }


    /// <summary>
    /// 将一个 360度的坐标  转换为30%含量的数值
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float ConvertTo30Percentage(float angle)
    {
        if (angle >= 0 && angle <= 360)
        {
            float percentage = (angle / 360) * 30;
            return float.Parse(percentage.ToString("#0.0"));
        }
        else
        {
            throw new ArgumentOutOfRangeException("angle", "Angle must be between 0 and 360 degrees.");
        }
    }



}

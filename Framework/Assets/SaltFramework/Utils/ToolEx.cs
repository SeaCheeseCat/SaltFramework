using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
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

    /// <summary>
    /// Conver:
    /// 将一组数据统一转换为负值
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static float[] ConvertArray(float[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = -array[i];
        }
        return array;
    
    }

    public static GameObject FindChildByName(Transform parentTransform, string targetName)
    {
        foreach (Transform child in parentTransform)
        {
            if (child.name == targetName)
            {
                return child.gameObject;
            }
            GameObject result = FindChildByName(child, targetName);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }



    /// <summary>
    /// Conver:
    /// 将一组数据统一转换为负值
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public static void SetAlpha(this Image img, float alpha)
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b,alpha);
    }

    /// <summary>
    /// Get:
    /// 获取一个物体的Asset路径
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static string GetPrefabAssetPath(this GameObject gameObject)
    {
#if UNITY_EDITOR
        if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gameObject))
        {
            return UnityEditor.AssetDatabase.GetAssetPath(gameObject);
        }
        if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(gameObject))
        {
            var prefabAsset = UnityEditor.PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject);
            return UnityEditor.AssetDatabase.GetAssetPath(prefabAsset);
        }
        var prefabStage = UnityEditor.SceneManagement.PrefabStageUtility.GetPrefabStage(gameObject);
        if (prefabStage != null)
        {
            return prefabStage.assetPath;
        }
#endif
        return null;
    }

    /// <summary>
    /// Set:
    /// 仅改变Scale的x大小
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="x"></param>
    public static void SetScaleX(this Transform transform, float x) {
        transform.localScale = new Vector3(x,transform.localScale.y,transform.localScale.z); 
    
    }



    public static GameObject[] GetChildFromName(this Transform transform, string name, bool fuzzySearch = false)
    {
        List<GameObject> matchingChildren = new List<GameObject>();

        // 内部方法，用于递归搜索
        void SearchChildren(Transform currentTransform)
        {
            // 检查当前 transform 的名称是否精确匹配或包含指定名称
            if ((fuzzySearch && currentTransform.name.Contains(name)) || currentTransform.name == name)
            {
                matchingChildren.Add(currentTransform.gameObject);
            }

            // 递归搜索所有子 transform
            foreach (Transform child in currentTransform)
            {
                SearchChildren(child);
            }
        }

        // 开始搜索
        SearchChildren(transform);

        // 返回匹配的子对象数组
        return matchingChildren.ToArray();
    }



    /// <summary>
    /// 启动文本打字机动画
    /// </summary>
    /// <param name="texts">要显示的文本数组</param>
    /// <param name="typingSpeed">打字速度</param>
    /// <param name="fadeOutDuration">淡出时长</param>
    /// <param name="onComplete">所有文本打字完成后的回调</param>
    public static void DoTextTypeAnimation(this Text descText, string[] texts, float typingSpeed, float fadeOutDuration, Action onComplete = null)
    {
        GameBase.Instance.StartCoroutine(TextTypeAnimRoutine(texts, descText, typingSpeed, fadeOutDuration, onComplete));
    }

    private static IEnumerator TextTypeAnimRoutine(string[] texts, Text descText, float typingSpeed, float fadeOutDuration, Action onComplete)
    {
        int currentIndex = 0;
        descText.DOFade(1, fadeOutDuration);
        while (currentIndex < texts.Length)
        {
            if (currentIndex != 0)
                yield return new WaitForSeconds(0.5f);

            string text = texts[currentIndex];
            yield return GameBase.Instance.StartCoroutine(TypeText(descText, text, typingSpeed));

            currentIndex++;
        }

        yield return new WaitForSeconds(0.1f);
        OnCardDescComplete(descText, fadeOutDuration, onComplete);
    }

    private static IEnumerator TypeText(Text textComponent, string fullText, float typingSpeed)
    {
        textComponent.text = "";

        foreach (char character in fullText)
        {
            textComponent.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private static void OnCardDescComplete(Text descText, float fadeOutDuration, Action onComplete)
    {
        descText.DOFade(0, fadeOutDuration).OnComplete(() =>
        {
            descText.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }



    private static Coroutine currentTypeCoroutine;

    /// <summary>
    /// Do:
    /// 开启文本框打字机效果
    /// </summary>
    /// <param name="textComponent">Unity文本组件</param>
    /// <param name="fullText">内容</param>
    /// <param name="typingSpeed">速度</param>
    public static void DoTypeText(this Text textComponent, string fullText, float typingSpeed, Action action)
    {
        if(currentTypeCoroutine != null)
            GameBase.Instance.StopCoroutine(currentTypeCoroutine);
        currentTypeCoroutine = GameBase.Instance.StartCoroutine(TypeText(textComponent, fullText, typingSpeed, action));
    }

    /// <summary>
    /// Coroutine:
    /// 打字机的协程
    /// </summary>
    /// <param name="textComponent"></param>
    /// <param name="fullText"></param>
    /// <param name="typingSpeed"></param>
    /// <returns></returns>
    public static IEnumerator TypeText(this Text textComponent, string fullText, float typingSpeed, Action endAction)
    {
        textComponent.text = "";

        foreach (char character in fullText)
        {
            textComponent.text += character;
            yield return new WaitForSeconds(typingSpeed);
        }
        currentTypeCoroutine = null;
        endAction?.Invoke();
    }

    /// <summary>
    /// To:
    /// 在字符串数组中加入空格规范
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Change:
    /// 更改Fill图片的进度值
    /// </summary>
    public static void ChangeFillAmount(this Image targetImage, float pressTimer,float pressDuration)
    {
        if (targetImage == null)
            return;

        if (pressTimer == 0)
        {
            targetImage.fillAmount = 0;
        }
        else
        {
            float fillPercentage = pressTimer / pressDuration;
            targetImage.fillAmount = Mathf.Clamp01(fillPercentage);
        }
    }


    /// <summary>
    /// Parse:
    /// 将关卡文件名字解析成为章节几关卡几
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static int[] ParseAndPrintLevelData(string fileName)
    {
        // 使用正则表达式解析文件名
        Match match = Regex.Match(fileName, @"mapconfig_(\d+)_(\d+)");

        if (match.Success)
        {
            int chapter = int.Parse(match.Groups[1].Value);
            int level = int.Parse(match.Groups[2].Value);
            return new int[] { chapter, level };
        }
        return new int[] { 0, 0 };
    }

    /// <summary>
    /// Get:
    /// 获取当前的Rotate面板值
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public static Vector3 GetInspectorRotationValueMethod(this Transform transform)
    {
        // 获取原生值
        System.Type transformType = transform.GetType();
        PropertyInfo m_propertyInfo_rotationOrder = transformType.GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        object m_OldRotationOrder = m_propertyInfo_rotationOrder.GetValue(transform, null);
        MethodInfo m_methodInfo_GetLocalEulerAngles = transformType.GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        object value = m_methodInfo_GetLocalEulerAngles.Invoke(transform, new object[] { m_OldRotationOrder });
        string temp = value.ToString();
        //将字符串第一个和最后一个去掉
        temp = temp.Remove(0, 1);
        temp = temp.Remove(temp.Length - 1, 1);
        //用‘，’号分割
        string[] tempVector3;
        tempVector3 = temp.Split(',');
        //将分割好的数据传给Vector3
        Vector3 vector3 = new Vector3(float.Parse(tempVector3[0]), float.Parse(tempVector3[1]), float.Parse(tempVector3[2]));
        return vector3;
    }

    public static string Formatting(this string str)
    {
        str = str.Replace("\\n", "\n");
        return str;
    }

}


[Serializable]
public struct DoubleVector3
{
    public double x;
    public double y;
    public double z;

    public DoubleVector3(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static DoubleVector3 operator +(DoubleVector3 a, DoubleVector3 b)
    {
        return new DoubleVector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static DoubleVector3 operator -(DoubleVector3 a, DoubleVector3 b)
    {
        return new DoubleVector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static DoubleVector3 operator *(DoubleVector3 a, double d)
    {
        return new DoubleVector3(a.x * d, a.y * d, a.z * d);
    }

    public static DoubleVector3 operator /(DoubleVector3 a, double d)
    {
        return new DoubleVector3(a.x / d, a.y / d, a.z / d);
    }

    public double Magnitude()
    {
        return Math.Sqrt(x * x + y * y + z * z);
    }

    public double SqrMagnitude()
    {
        return x * x + y * y + z * z;
    }

    public DoubleVector3 Normalized()
    {
        double mag = Magnitude();
        return new DoubleVector3(x / mag, y / mag, z / mag);
    }

    public override string ToString()
    {
        return $"({x}, {y}, {z})";
    }
}

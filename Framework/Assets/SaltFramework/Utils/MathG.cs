using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Google.Protobuf.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 扩展方法组
/// </summary>
public static class MathG {
    /// <summary>
    /// 检查数值是否在某个区间
    /// </summary>
    /// <param name="value"></param>
    /// <param name="limits"></param>
    /// <returns></returns>
    public static bool IsValueBetween(int value, int[] limits)
    {
        return value >= limits[0] && value <= limits[1];
    }

    /// <summary>
    /// 字符超出边框部分显示为省略号
    /// </summary>
    /// <param name="textComponent"></param>
    /// <param name="value"></param>
    public static void SetTextWithEllipsis(this Text textComponent, string value)
    {
        var generator = new TextGenerator();
        var rectTransform = textComponent.GetComponent<RectTransform>();
        var settings = textComponent.GetGenerationSettings(rectTransform.rect.size);
        generator.Populate(value, settings);

        // 删除后续文字，并添加省略号
        var characterCountVisible = generator.characterCountVisible;
        var updatedText = value;
        if (value.Length > characterCountVisible)
        {
            updatedText = value.Substring(0, characterCountVisible - 1);
            updatedText += "…";
        }

        textComponent.text = updatedText;
    }

    /// <summary>
    /// 根据权重随机抽取
    /// </summary>
    /// <param name="values"></param>
    /// <returns></returns>
    public static T RandomDic<T>(Dictionary<T, int> values)
    {
        var sub = values.Values.Sub();
        var rd = UnityEngine.Random.Range(0, sub);
        foreach (var v in values)
        {
            rd -= v.Value;
            if (rd <= 0) return v.Key;
        }
        return default;
    }

    /// <summary>
    /// 时间戳转化为时间
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    public static DateTime ConvertLongToDateTime(long timeStamp)
    {
        DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1, 0, 0, 0, 0), TimeZoneInfo.Local);
        long lTime = long.Parse(timeStamp + "0000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }

    /// <summary>
    /// 检查是否同一位置
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool IsSamePos(this Transform trans,Transform target)
    {
        if (Mathf.RoundToInt(trans.position.x) != Mathf.RoundToInt(target.position.x)) return false;
        if (Mathf.RoundToInt(trans.position.y) != Mathf.RoundToInt(target.position.y)) return false;
        if (Mathf.RoundToInt(trans.position.z) != Mathf.RoundToInt(target.position.z)) return false;
        return true;
    }

    /// <summary>
    ///  获取单位的坐标
    /// </summary>
    /// <param name="trans"></param>
    /// <returns></returns>
    public static Vector3Int PositionInt(this Transform trans)
    {
        return new Vector3Int(Mathf.RoundToInt(trans.position.x), Mathf.RoundToInt(trans.position.y), Mathf.RoundToInt(trans.position.z));
    }

    /// <summary>
    /// 获取时间戳
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static long ConvertDateTimeToLong(DateTime time)
    {
        DateTime dd = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        TimeSpan ts = (time - dd);
        return (Int64)ts.TotalMilliseconds;
    }


    /// <summary>
    /// 字符串解析为时间
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str)
    {
        int[] rt = new int[6];
        str = str.Replace("-", "|");
        str = str.Replace(" ", "|");
        str = str.Replace(":", "|");
        try
        {
            string[] Times = str.Split('|');
            if (Times.Length == 6)
            {
                for (int i = 0; i < rt.Length; i++)
                {
                    rt[i] = int.Parse(Times[i]);
                }
                return new DateTime(rt[0], rt[1], rt[2], rt[3], rt[4], rt[5]);
            }
            else
            {
                return DateTime.Now;
            }
        }
        catch (Exception e)
        {
            Debug.Log("非法时间格式" + e);
            return DateTime.Now;
        }
    }

    /// <summary>
    /// 整型数组解析时间
    /// </summary>
    /// <param name="Dates"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this int[] array)
    {
        return new DateTime(array[0], array[1], array[2], array[3], array[4], array[5]);
    }

    /// <summary>
    /// 序列化时间
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static int[] ToArray(this DateTime dateTime)
    {
        int[] rt = new int[]
        {
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second
        };
        return rt;
    }

    /// <summary>
    /// 分割字符串并返回随机一个单元
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="split">分割符号</param>
    /// <returns>随机分割单元</returns>
    public static string RandomSplitToString(this string str,char split=';')
    {
        string[] strs = str.Split(split);
        return strs[UnityEngine.Random.Range(0, strs.Length - 1)];
    }

    /// <summary>
    /// 解析字符串范围
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="split">分割符号</param>
    /// <returns>范围值</returns>
    public static int[] SplitRangeToInt(this string str,char split=';')
    {
        string[] strs = str.Split(split);
        if (strs.Length == 1)
        {
            return new int[] { int.Parse(strs[0]), int.Parse(strs[0]) };
        }
        return new int[] { int.Parse(strs[0]), int.Parse(strs[1]) };
    }

    /// <summary>
    /// 解析字符串范围
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="split">分割符号</param>
    /// <returns>范围值</returns>
    public static float[] SplitRangeToFloat(this string str,char split=';')
    {
        string[] strs = str.Split(split);
        return new float[] { float.Parse(strs[0]), float.Parse(strs[1]) };
    }

    /// <summary>
    /// 整型转化为时间字符串
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    /*public static string ToTimeString(this int v)
    {
        var s = Mathf.FloorToInt(v * 1f);   //总秒数
        var h = Mathf.FloorToInt(s * 1f / 3600);    //总小时数
        var m = Mathf.FloorToInt((s * 1f / 60) % 60); //显示分钟数
        var realSecond = Mathf.RoundToInt(s % 60);    //显示秒数
        var rt = "";
        if (h > 0)
        {
            rt += h + TextManager.GetText(TextNameToID.Hour) + "";
        }
        if (m > 0)
        {
            rt += m + TextManager.GetText(TextNameToID.Minute) + "";
        }
        if (realSecond > 0)
        {
            rt += realSecond + TextManager.GetText(TextNameToID.Second);
        }
        
        return rt;
    }*/

    public static string TimeToString(this int v)
    {
        int minutes = v / 60;
        string mm = minutes < 10f ? "0" + minutes : minutes.ToString();
        int seconds = v - (minutes * 60);
        string ss = seconds < 10 ? "0" + seconds : seconds.ToString();
        return string.Format("{0}:{1}", mm, ss);
    }

    /// <summary>
    /// 修正数字
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string FixNum(int num)
    {
        if (num <= 9999) return num.ToString();
        var k = Math.Round(num / 1000f,2);
        if (k >= 9999) return "9999k";
        return k + "k";
    }

    /// <summary>
    /// 列表交叉
    /// </summary>
    /// <param name="list"></param>
    /// <param name="check"></param>
    /// <returns></returns>
    public static bool Catch<T>(this ICollection<T> list,ICollection<T> check)
    {
        foreach(var v in list)
        {
            if (check.Contains(v))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 创建一个唯一id
    /// </summary>
    /// <returns></returns>
    public static int MakeUid()
    {
        var buffer = Guid.NewGuid().ToByteArray();
        var uid = BitConverter.ToInt32(buffer, 0);
        return uid;
    }

    public static List<int>[] ToSplitList(this string[] list,char split =':')
    {
        List<int>[] rt = new List<int>[list.Length];
        for(int i = 0; i < list.Length; i++)
        {
            var l = new List<int>();
            l.AddRange(list[i].Split(split).ToInt());
            rt[i] = l;
        }
        return rt;
    }

    public static int TryGetValue(this Dictionary<int,int> p,int t)
    {
        if (p.ContainsKey(t))
        {
            return p[t];
        }
        return 0;
    }

    public static int GetRandomIntByRange(int[] range, int seed = 0) //获取随机区间
    {
        if (seed == 0) { seed = UnityEngine.Random.Range(0, 100); } //生成随机数种子
        int rt = 0;
        int count = range.Length;
        int length = range[count - 1] - range[0] + 1;
        rt = range[0] + seed % length;
        return rt;
    }

    public static int GetRandomIntByRange(int min, int max, int seed = 0) //获取随机区间
    {
        if (seed == 0) { seed = UnityEngine.Random.Range(0, 100); } //生成随机数种子
        return min + seed % (max - min);
    }

    /// <summary>
    /// 随机抽取
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static T Random<T>(this T[] data)
    {
        return data[UnityEngine.Random.Range(0, data.Length)];
    }

    /// <summary>
    /// 随机抽取一定数量的对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static List<T> RandomList<T>(this ICollection<T> data,int count)
    {
        var rt = new List<T>();
        var pool = new List<T>(data);
        for(int i = 0; i < count; i++)
        {
            if (pool.Count <= 0) break;
            var t = pool[UnityEngine.Random.Range(0, pool.Count)];
            rt.Add(t);
            pool.Remove(t);
        }
        return rt;
    }

    /// <summary>
    /// 判断概率
    /// </summary>
    /// <param name="rate"></param>
    /// <param name="seed"></param>
    /// <returns></returns>
    public static bool Rate(this float rate, int seed = 0)
    {
        if (seed == 0) { seed = UnityEngine.Random.Range(0, 100); } //生成随机数种子
        return rate >= seed % 100;
    }

    /// <summary>
    /// 解析时间转为字符串
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string ToString(this int[] data)
    {
        return string.Format("{0}/{1}/{2} {3}:{4}:{5}", data[0], data[1], data[2], data[3], data[4], data[5]);
    }

    /// <summary>
    /// 获取相对向量
    /// </summary>
    /// <param name="point"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static Vector3 MatrixTurn(this Vector3 point, Vector3 target)
    {
        target = new Vector3(target.x, 0f, target.z);
        target.Normalize();
        float cos = Vector3.Dot(target, Vector3.forward);
        float h = (point.x * target.z - point.z * target.x) * cos;
        float v = (point.z * target.z + point.x * target.x) * cos;

        return new Vector3(h, 0, v);
    }

    /// <summary>
    /// 获取一个随机向量
    /// </summary>
    /// <param name="length">向量长度</param>
    /// <returns></returns>
    public static Vector3 ToRandomVector3(this float length)    //获取随机向量
    {
        return new Vector3(UnityEngine.Random.Range(-length, length), 0f, UnityEngine.Random.Range(-length, length));
    }

    /// <summary>
    /// 分割为整数数组
    /// </summary>
    /// <param name="num"></param>
    /// <param name="count"></param>
    /// <param name="maxCurve"></param>
    /// <returns></returns>
    public static List<int> ToArrayList(this int num, int count, int maxCurve)
    {
        List<int> rt = new List<int>();
        if (count < 1)
        {
            Debug.LogError("分割数字不得小于1");
            count = 1;
        }
        int baseNumber = num / count;
        int result = 0;
        for (int i = 0; i < count - 1; i++)
        {
            result = UnityEngine.Random.Range(baseNumber - maxCurve, baseNumber + maxCurve);
            if (result < 0)
            {
                result = 0;
            }
            if (result > num)
            {
                result = num;
            }
            rt.Add(result);
            num -= result;
        }
        rt.Add(num);
        return rt;
    }

    //尝试添加数值
    public static void TryAddValue(this MapField<int,int> dic,int key,int value)
    {
        if (dic.ContainsKey(key))
        {
            dic[key] += value;
        }
        else
        {
            dic[key] = value;
        }
    }

    //尝试添加数值
    public static void TryAddValue(this MapField<long, int> dic, long key, int value)
    {
        if (dic.ContainsKey(key))
        {
            dic[key] += value;
        }
        else
        {
            dic[key] = value;
        }
    }

    /// <summary>
    /// 分割数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static List<List<T>> ToGroupList<T>(this List<T> list, int count, int maxCurve)
    {
        var rt = new List<List<T>>();
        var cacheList = new List<T>(list);
        var index = cacheList.Count.ToArrayList(count, maxCurve);
        for (int i = 0; i < index.Count; i++)
        {
            rt.Add(new List<T>());
            for (int j = 0; j < index[i]; j++)
            {
                rt[i].Add(cacheList[j]);
                cacheList.Remove(cacheList[j]);
            }
        }
        return rt;
    }

    /// <summary>
    /// 分割字典
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="Tvalue"></typeparam>
    /// <param name="pairs"></param>
    /// <param name="count"></param>
    /// <param name="maxCurve"></param>
    /// <returns></returns>
    public static List<Dictionary<Tkey, Tvalue>> ToGroupDictionary<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> pairs, int count, int maxCurve)
    {
        var rt = new List<Dictionary<Tkey, Tvalue>>();
        var cacheList = new List<Tkey>();
        foreach(var v in pairs)
        {
            cacheList.Add(v.Key);
        }
        var splitList = cacheList.Count.ToArrayList( count, maxCurve);
        for(int i = 0; i < count; i++)
        {
            var d = new Dictionary<Tkey, Tvalue>();
            for (int j = 0; j < splitList[i] && j < cacheList.Count; j++)
            {
                Tkey tk = cacheList[j];
                Tvalue tv = pairs[tk];
                d.Add(tk, tv);
                cacheList.Remove(tk);
            }
            rt.Add(d);
        }
        Debug.Log("分割字典长度" + rt.Count);
        return rt;
    }
    
    /// <summary>
    /// 字符串转字典
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Dictionary<string,int> ToDictionary(this string str,char split=';',char splitSec=',')
    {
        Dictionary<string, int> value = new Dictionary<string, int>();
        string[] strs = str.Split(split);
        foreach (string s in strs)
        {
            string[] kv = s.Split(splitSec);
            if (kv.Length != 2)
            {
                Debug.Log("错误配置");
            }
            value[kv[0]] = int.Parse(kv[1]);
        }
        return value;
    }

    /// <summary>
    /// 解析为字典
    /// </summary>
    /// <param name="list"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    public static Dictionary<int,int> ToDictionary(this string[] list,char split = ':')
    {
        var rt = new Dictionary<int, int>();
        foreach(var v in list)
        {
            var strs = v.Split(split).ToInt();
            rt.Add(strs[0], strs[1]);
        }

        return rt;
    }

    public static Dictionary<int,int> Combine(this Dictionary<int,int> dic,Dictionary<int,int> target)
    {
        foreach(var v in target)
        {
            if (dic.ContainsKey(v.Key))
            {
                dic[v.Key] += v.Value;
            }
            else
            {
                dic[v.Key] = v.Value;
            }
        }
        return dic;
    }

    public static Dictionary<int,int> ToDictionaryInt(this string str, char split = ';', char splitSec = ',')
    {
        Dictionary<int, int> value = new Dictionary<int, int>();
        string[] strs = str.Split(split);
        foreach (var s in strs)
        {
            string[] kv = s.Split(splitSec);
            if (kv.Length != 2)
            {
                Debug.Log("错误配置");
            }
            value[int.Parse(kv[0])] = int.Parse(kv[1]);
        }
        return value;
    }

    public static List<Tvalue> ToValueList<Tkey,Tvalue>(this MapField<Tkey,Tvalue> map)
    {
        var rt = new List<Tvalue>();
        foreach (var v in map.Values)
        {
            rt.Add(v);
        }
        return rt;
    }

    /// <summary>
    /// 泛型转化
    /// </summary>
    /// <typeparam name="Tkey">字段类型</typeparam>
    /// <typeparam name="Tvalue">值类型</typeparam>
    /// <param name="str"></param>
    /// <param name="split"></param>
    /// <param name="splitSec"></param>
    /// <returns></returns>
    public static Dictionary<Tkey, Tvalue> ToDictionary<Tkey, Tvalue>(this string str, char split = ';', char splitSec = ',') where Tkey : IConvertible where Tvalue :IConvertible
    {
        Dictionary<Tkey, Tvalue> value = new Dictionary<Tkey, Tvalue>();
        string[] strs = str.Split(split);
        foreach (var s in strs)
        {
            string[] kv = s.Split(splitSec);
            if (kv.Length != 2)
            {
                Debug.Log("错误配置");
            }
            var k = (Tkey)Convert.ChangeType(kv[0], typeof(Tkey));
            var v = (Tvalue)Convert.ChangeType(kv[1], typeof(Tvalue));
            value.Add(k, v);
        }
        return value;
    }

    /// <summary>
    /// 改变物体层级，包括子物体
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="layer"></param>
    public static void ChangeLayer(this GameObject obj ,int layer,bool withChildren=true)
    {
        if (withChildren)
        {
            foreach (var v in obj.GetComponentsInChildren<Transform>())
            {
                v.gameObject.layer = layer;
            }
        }
        else
        {
            obj.layer = layer;
        }
        
    }

    /// <summary>
    /// 取所有值的负数
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <param name="dic"></param>
    /// <returns></returns>
    public static Dictionary<Tkey,int> Negative<Tkey>(this IDictionary<Tkey,int> dic)
    {
        var rt = new Dictionary<Tkey, int>();
        foreach(var v in dic)
        {
            rt.Add(v.Key, -v.Value);
        }
        return rt;
    }

    /// <summary>
    /// 深度查找子物体
    /// </summary>
    /// <returns>The find child.</returns>
    /// <param name="parent">Parent.</param>
    /// <param name="targetName">Target name.</param>
    public static Transform FindDeep(this Transform parent, string targetName)
    {
        Transform _result = null;
        _result = parent.Find(targetName);
        if (_result == null)
        {
            foreach (Transform t in parent)
            {
                _result = t.FindDeep(targetName);
                if (_result != null)
                {
                    return _result;
                }
            }
        }
        return _result;
    }

    /// <summary>
    /// 清空对象组
    /// </summary>
    /// <param name="list"></param>
    public static void ClearMonoList<T>(this List<T> list) where T:MonoBehaviour
    {
        if (list == null) return;
        foreach(var v in list)
        {
            UnityEngine.Object.Destroy(v.gameObject);
        }
        list.Clear();
    }

    /// <summary>
    /// 获取一组互不重复的整数
    /// </summary>
    /// <returns>The random int.</returns>
    /// <param name="min">Minimum.</param>
    /// <param name="max">Max.</param>
    public static List<int> GetRandomInt(int min,int max,int count)
    {
        List<int> ret =new List<int>();
        if (min > max) return ret;
        int num = max - min;

        if (count > num) count = num;

        List<int> pool = new List<int>();
        for (int i = 0; i <= num;i++)
        {
            pool.Add(i);
        }

        List<int> sum = new List<int>();
        for (int i = 0; i <= num;i++)
        {
            int g = pool[UnityEngine.Random.Range(0, pool.Count)];
            sum.Add(g);
            pool.Remove(g);
        }

        for (int i = 0; i < count;i++)
        {
            ret.Add(min + sum[i]);
        }

        return ret;
    }

    /// <summary>
    /// 查询字典中最大键
    /// </summary>
    /// <typeparam name="Tvalue"></typeparam>
    /// <param name="dic"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int GetMaxKey<Tvalue>(this Dictionary<int, Tvalue> dic, int max)
    {
        if (dic.ContainsKey(max))
        {
            return max;
        }

        int km = 0;
        foreach (int i in dic.Keys)
        {
            if (max >= i && i >= km)
            {
                km = i;
            }
        }
        if (dic.ContainsKey(km))
            return km;
        return 0;
    }

    /// <summary>
    /// 字符串转化为整型
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static int[] ToInt(this string[] list)
    {
        //var rt = new int[list.Length];
        var rt = new List<int>();
        for(int i = 0; i < list.Length; i++)
        {
            var data = 0;
            if (int.TryParse(list[i], out data))
            {
                rt.Add(data);
            }
         
           /* rt[i] = int.TryParse(list[i]);*/


        }
        return rt.ToArray();
    }

    /// <summary>
    /// 重置节点
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="node"></param>
    public static void ResetNode(this Transform trans,Transform node)
    {
        trans.SetParent(node);
        trans.localScale = Vector3.one;
        trans.position = Vector3.zero;
        trans.rotation = Quaternion.identity;
    }

    //重置节点带位置信息
    public static void ResetNode(this Transform trans,Transform node, Vector3 pos,Quaternion rota)
    {
        trans.SetParent(node);
        trans.localScale = Vector3.one;
        trans.position = pos;
        trans.rotation = rota;
    }

    public static void AddEventTrigger(this GameObject Node, EventTriggerType eventID, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = Node.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = Node.AddComponent<EventTrigger>();
        }

        if (trigger.triggers.Count == 0)
        {
            trigger.triggers = new List<EventTrigger.Entry>();
        }

        UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(action);
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventID;
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
    }

    public static void ClearList<T>(this List<T> list) where T:IClearbale
    {
        foreach (var v in list)
        {
            v.Clear();
        }
    }

    public static void TryAddValue(this Dictionary<int,int> dic,int id,int value,bool isCover=false)
    {
        if (dic.ContainsKey(id)&& !isCover)
        {
            dic[id] += value;
        }
        else
        {
            dic[id] = value;
        }
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="data"></param>
    /// <param name="dic"></param>
    public static void TryAddDictionary(this Dictionary<int,int> data,Dictionary<int,int> dic)
    {
        foreach(var v in dic)
        {
            if (data.ContainsKey(v.Key))
            {
                data[v.Key] += v.Value;
            }
            else
            {
                data[v.Key] = v.Value;
            }
        }
    }

    /// <summary>
    /// 总和
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static int Sub(this ICollection<int> list)
    {
        var rt = 0;
        foreach(var v in list)
        {
            rt += v;
        }
        return rt;
    }

    /// <summary>
    /// 获取贝塞尔点
    /// </summary>
    /// <param name="t"></param>
    /// <param name="start"></param>
    /// <param name="center"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static Vector3 GetBezirePoint(float t, Vector3 start, Vector3 center, Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    }

    /// <summary>
    /// 无限阶贝塞尔曲线
    /// </summary>
    /// <param name="t"></param>
    /// <param name="vecs"></param>
    /// <returns></returns>
    public static Vector3 BezierCurve(float t, List<Vector3> vecs)
    {
        int count = vecs.Count;
        int rank = count - 1;
        Vector3 ret = Vector3.zero;
        List<int> pts = CacluPascalTriangle(count);

        List<Formula> fs = new List<Formula>();
        for (int i = 0; i < count; i++)
        {
            Formula f = new Formula(vecs[i], rank, i, pts[i], t);
            fs.Add(f);
        }
        for (int i = 0; i < fs.Count; i++)
        {
            ret += fs[i].Caclu();
        }
        return ret;
    }


    /// <summary>
    /// 无限阶贝塞尔曲线
    /// </summary>
    /// <param name="t"></param>
    /// <param name="vecs"></param>
    /// <returns></returns>
    public static Vector3 BezierCurve(float t, params Vector3[] vecs)
    {
        int count = vecs.Length;
        int rank = count - 1;
        Vector3 ret = Vector3.zero;
        List<int> pts = CacluPascalTriangle(count);

        List<Formula> fs = new List<Formula>();
        for (int i = 0; i < count; i++)
        {
            Formula f = new Formula(vecs[i], rank, i, pts[i], t);
            fs.Add(f);
        }
        for (int i = 0; i < fs.Count; i++)
        {
            ret += fs[i].Caclu();
        }
        return ret;
    }

    public static List<int> CacluPascalTriangle(int n)
    {
        List<int> ret = new List<int>();

        int[,] array = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                if (j == 0 || i == j)
                {
                    array[i, j] = 1;
                }
                else
                {
                    array[i, j] = array[i - 1, j - 1] + array[i - 1, j];
                }
            }
        }
        for (int i = 0; i < n; i++)
        {
            ret.Add(array[n - 1, i]);
        }
        return ret;
    }

    public class Formula
    {
        //点
        public Vector3 point;
        //阶层
        public int rank;
        //索引
        public int index;
        //杨辉三角
        public int PTValue;

        public float t;

        public Formula(Vector3 point, int rank, int index, int pTValue, float t)
        {
            this.point = point;
            this.rank = rank;
            this.index = index;
            this.PTValue = pTValue;
            this.t = t;
        }

        public float Exponentiation(float num, int power)
        {
            float n = 1;
            if (power == 0)
            {
                return n;
            }
            for (int i = 0; i < power; i++)
            {
                n *= num;
            }
            return n;
        }

        public Vector3 Caclu()
        {
            Vector3 P = Vector3.zero;
            float t1 = Exponentiation((1 - t), (rank - index));
            float t2 = Exponentiation(t, index);
            P = PTValue * point * t1 * t2;
            return P;
        }
    }

    public static T Random<T>(this List<T> data)
    {
        if (data.Count <= 0)
        {
            Debug.LogError("错误，数据为0");
        }
        return data[UnityEngine.Random.Range(0, data.Count)];
    }

    /// <summary>
    /// 尝试添加元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="t"></param>
    public static void TryAdd<T>(this List<T> list,T t)
    {
        if(!list.Contains(t))
        {
            list.Add(t);
        }
    }

    /// <summary>
    /// 转化为带逗号的字符串
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToStringSpecial(this int value)
    {
        var list = new List<char>();
        var chars = value.ToString().ToCharArray();
        for (int i = chars.Length-1; i >= 0; i--)
        {
            list.Add(chars[i]);
        }

        var late = "";
        string rt = string.Empty;
        if (list.Count > 4)    //超过10000
        {
            list.RemoveRange(0, 3);
            late = " K";
        }
        int index = 0;
        foreach (var v in list)
        {
            if (index >= 3)
            {
                rt = "," + rt;
                index -= 3;
            }
            rt = v + rt;
            index += 1;
        }

        return rt+late;
    }

    /// <summary>
    /// 添加按钮高亮
    /// </summary>
    /// <param name="button"></param>
    public static void SetLightBtn(this Button button,float size)
    {
        var obj = ResourceManager.AllocObjectSync("System/Misc/LightBtn");
        obj.transform.ResetParent(button.transform);
        var rect = obj.transform as RectTransform;
        rect.sizeDelta = new Vector2(size, size);
        bool removeObj = false;
        button.onClick.AddListener(delegate
        {
            if (!removeObj)
            {
                GameObject.Destroy(obj);
                removeObj = true;
            }
        });
    }

    /// <summary>
    /// 随机播放音效
    /// </summary>
    /// <param name="list"></param>
    public static void Play(this List<AudioClip> list,float delay, float perc=1)
    {
        if (list.Count <= 0) return;
        var rd = UnityEngine.Random.Range(0, 1f);
        if (rd <= perc)
        {
            var clip = list.Random();
            //AudioManager.Instance.PlayOnceSingle(clip,list.GetHashCode().ToString(), delay);
        }
    }

    /// <summary>
    /// 获取数据
    /// </summary>
    /// <param name="values"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static int GetValue(this IDictionary<int,int> values,int index)
    {
        if (!values.ContainsKey(index))
        {
            return 0;
        }
        return values[index];
    }

    /// <summary>
    /// 随机权重
    /// </summary>
    /// <returns></returns>
    public static T RandomDicRights<T>(this Dictionary<T,int> dic)
    {
        var rd = UnityEngine.Random.Range(0, dic.Values.Sub());
        foreach (var v in dic)
        {
            if (rd <= v.Value)
            {
                return v.Key;
            }
            else
            {
                rd -= v.Value;
            }
        }

        return new List<T>(dic.Keys).Random();
    }
}

public interface IClearbale
{
    void Clear();
}

/// <summary>
/// 配置表读取工具
/// </summary>
public static class ConfigLoader
{
    public static CfgData<Config> LoadConfigData(string path)
    {
        XmlNodeList xmlNodeList = LoadXml(path);
        if (xmlNodeList == null)
        {
            return null;
        }
        return new CfgData<Config>(xmlNodeList);
    }

    //加载xml节点
    public static XmlNodeList LoadXml(string path)
    {
        Debug.Log("读取配置路径" + path);
        XmlDocument xml = new XmlDocument();
        TextAsset newTdata = (TextAsset)Resources.Load(path);
        if (newTdata == null)
        {
            return null;
        }
        xml.LoadXml(newTdata.text);
        return xml.SelectSingleNode("root").ChildNodes;
    }

    /// <summary>
    /// 加载XML配置表
    /// </summary>
    /// <returns>配置表</returns>
    /// <param name="obj">读取文件</param>
    public static CfgData<Config> LoadConfig(UnityEngine.Object obj){
        TextAsset t = (TextAsset)obj;
        if (t == null)
        {
            return null;
        }
        XmlDocument xml = new XmlDocument(); 
        xml.LoadXml(t.text);
        return new CfgData<Config>(xml.SelectSingleNode("root").ChildNodes);
    }
}

public delegate void StrDelegate(string f);

public delegate void VoidDelegate();

/// <summary>
/// 泛型文件配置
/// </summary>
public interface ICfgData
{
    void InitCfgId(string f);
    void InitValue(string fname, string f);
    string GetCfgId();
}

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
    public SerializableDictionary() { }
    public void WriteXml(XmlWriter write)       // Serializer  
    {
        XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
        XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

        foreach (KeyValuePair<TKey, TValue> kv in this)
        {
            write.WriteStartElement("SerializableDictionary");
            write.WriteStartElement("key");
            KeySerializer.Serialize(write, kv.Key);
            write.WriteEndElement();
            write.WriteStartElement("value");
            ValueSerializer.Serialize(write, kv.Value);
            write.WriteEndElement();
            write.WriteEndElement();
        }
    }
    public void ReadXml(XmlReader reader)       // Deserializer  
    {
        reader.Read();
        XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
        XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

        while (reader.NodeType != XmlNodeType.EndElement)
        {
            reader.ReadStartElement("SerializableDictionary");
            reader.ReadStartElement("key");
            TKey tk = (TKey)KeySerializer.Deserialize(reader);
            reader.ReadEndElement();
            reader.ReadStartElement("value");
            TValue vl = (TValue)ValueSerializer.Deserialize(reader);
            reader.ReadEndElement();
            reader.ReadEndElement();
            this.Add(tk, vl);
            reader.MoveToContent();
        }
        reader.ReadEndElement();

    }
    public XmlSchema GetSchema()
    {
        return null;
    }
}
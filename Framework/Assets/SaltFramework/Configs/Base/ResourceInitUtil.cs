using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 资源字段初始化工具
/// </summary>
class ResourceInitUtil
{
    public static void InitField (string data, out int value)
    {
        int.TryParse (data, out value);
    }

    public static void InitField (string data, out float value)
    {
        float.TryParse (data, out value);
    }

    public static void InitField (string data, out short value)
    {
        short.TryParse (data, out value);
    }

    public static void InitField (string data, out string value, out Dictionary<Language, string> languageval)
    {
        string val = data;
        if (val.Contains("{"))
        {
            var vals = val.Split("{");
            value = vals[0];
            languageval = LanguageManager.Instance.GetConfigLanguageString("{"+vals[1]);
            foreach (var item in languageval)
            {
                DebugEX.Log("多语言", item.Key, item.Value);
            }

            return;
        }
        value = data;
        languageval = new Dictionary<Language, string>(); ;
    }

    public static void InitField(string data, out string value)
    {
        value = data;
    }


    public static void InitField (string data, out bool value)
    {
        value = data != "" && data != "False";
    }

    public static void InitDefaultArray (string[] data, int start, int end, out int[] value)
    {
        while (string.IsNullOrEmpty (data[end]))
        {
            end -= 1;
        }
        if (data != null && start < data.Length && end < data.Length && start <= end)
        {
            value = new int[end - start + 1];
            for (int k = start; k < end + 1; k++)
            {
                InitField (data[k], out value[k - start]);
            }
        }
        else
        {
            value = null;
        }
    }

    public static void InitDefaultArray (string[] data, int start, int end, out float[] value)
    {
        while (string.IsNullOrEmpty (data[end]))
        {
            end -= 1;
        }
        if (data != null && start < data.Length && end < data.Length && start <= end)
        {
            value = new float[end - start + 1];
            for (int k = start; k < end + 1; k++)
            {
                InitField (data[k], out value[k - start]);
            }
        }
        else
        {
            value = null;
        }
    }

    public static void InitDefaultArray (string[] data, int start, int end, out short[] value)
    {
        while (string.IsNullOrEmpty (data[end]))
        {
            end -= 1;
        }
        if (data != null && start < data.Length && end < data.Length && start <= end)
        {
            value = new short[end - start + 1];
            for (int k = start; k < end + 1; k++)
            {
                InitField (data[k], out value[k - start]);
            }
        }
        else
        {
            value = null;
        }
    }

    public static void InitDefaultArray (string[] data, int start, int end, out string[] value)
    {
        while (string.IsNullOrEmpty (data[end]))
        {
            end -= 1;
        }
        if (data != null && start < data.Length && end < data.Length && start <= end)
        {

            value = new string[end - start + 1];
            for (int k = start; k < end + 1; k++)
            {
                InitField (data[k], out value[k - start]);
            }
        }
        else
        {
            value = null;
        }
    }

    public static void InitDefaultArray (string[] data, int start, int end, out bool[] value)
    {
        while (string.IsNullOrEmpty (data[end]))
        {
            end -= 1;
        }
        if (data != null && start < data.Length && end < data.Length && start <= end)
        {
            value = new bool[end - start + 1];
            for (int k = start; k < end + 1; k++)
            {
                InitField (data[k], out value[k - start]);
            }
        }
        else
        {
            value = null;
        }
    }

    public static void InitShortArray (string data, out int[] value)
    {
        if (string.IsNullOrWhiteSpace (data))
        {
            value = new int[0];
            return;
        }
        string[] arr = data.Split (',');
        value = new int[arr.Length];
        for (int k = 0; k < arr.Length; k++)
        {
            InitField (arr[k], out value[k]);
        }
    }

    public static void InitShortArray (string data, out float[] value)
    {
        if (string.IsNullOrWhiteSpace (data))
        {
            value = new float[0];
            return;
        }
        string[] arr = data.Split (',');
        value = new float[arr.Length];
        for (int k = 0; k < arr.Length; k++)
        {
            InitField (arr[k], out value[k]);
        }
    }

    public static void InitShortArray (string data, out short[] value)
    {
        if (string.IsNullOrWhiteSpace (data))
        {
            value = new short[0];
            return;
        }

        string[] arr = data.Split (',');
        value = new short[arr.Length];
        for (int k = 0; k < arr.Length; k++)
        {
            InitField (arr[k], out value[k]);
        }
    }

    public static void InitShortArray (string data, out bool[] value)
    {
        if (string.IsNullOrWhiteSpace (data))
        {
            value = new bool[0];
            return;
        }

        string[] arr = data.Split (',');
        value = new bool[arr.Length];
        for (int k = 0; k < arr.Length; k++)
        {
            InitField (arr[k], out value[k]);
        }
    }

    public static void InitShortArray (string data, out string[] value)
    {
        if (string.IsNullOrWhiteSpace (data))
        {
            value = new string[0];
            return;
        }

        string[] arr = data.Split ('&');
        value = new string[arr.Length];
        for (int k = 0; k < arr.Length; k++)
        {
            InitField (arr[k], out value[k]);
        }
    }

    public static void InitShortArray(string data, out string[] value, out Dictionary<Language, string[]> arrays)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            value = new string[0];
            arrays = new Dictionary<Language, string[]>();
            return;
        }

        if (!data.Contains("{"))
        {
            string[] arr = data.Split('&');
            value = new string[arr.Length];
            arrays = new Dictionary<Language, string[]>();
            for (int k = 0; k < arr.Length; k++)
            {
                InitField(arr[k], out value[k]);
            }
        }
        else
        {
            var vals = data.Split("{");
            string[] arr = vals[0].Split('&');
            value = new string[arr.Length];
            for (int k = 0; k < arr.Length; k++)
            {
                InitField(arr[k], out value[k]);
            }

            arrays = LanguageManager.Instance.GetConfigLanguageArray("{"+vals[1]);

        }
      
    }

    public static void InitUserField<T> (string[] data, int start, int end, int length, out T value) where T : BaseUserType, new ()
    {
        if (data != null && start < data.Length && end < data.Length && start <= end && end - start + 1 == length)
        {
            bool isEmpty = true;
            for (int k = start; k < end + 1; k++)
            {
                if (data[k] != null && data[k].Trim () != "")
                {
                    isEmpty = false;
                }
            }
            if (!isEmpty)
            {
                value = new T ();
                value.Init (data, start, end);
            }
            else
            {
                value = null;
            }
        }
        else
        {
            value = null;
        }
    }

    public static void InitUserArray<T> (string[] data, int start, int end, int length, out T[] value) where T : BaseUserType, new ()
    {
        if (data != null && start < data.Length && end < data.Length && start <= end && (end - start + 1) % length == 0)
        {
            List<T> tmplist = new List<T> ();
            for (int k = start; k < end + 1; k += length)
            {
                T tmp;
                InitUserField<T> (data, k, k + length - 1, length, out tmp);
                if (tmp != null)
                {
                    tmplist.Add (tmp);
                }
            }
            if (tmplist.Count > 0)
            {
                value = tmplist.ToArray ();
            }
            else
            {
                value = null;
            }
        }
        else
        {
            value = null;
        }
    }
}
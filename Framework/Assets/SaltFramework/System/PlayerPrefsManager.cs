using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : Manager<PlayerPrefsManager>
{
    public void Add(string key, string value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, value);
        }
        else
        {
            PlayerPrefs.SetString(key, value);
            SaveKey(key);
        }
        PlayerPrefs.Save();
    }

    public void Add(string key, int value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, value+"");
        }
        else
        {
            PlayerPrefs.SetString(key, value+"");
            SaveKey(key);
        }
        PlayerPrefs.Save();
    }

    public void Add(string key, float value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, value + "");
        }
        else
        {
            PlayerPrefs.SetString(key, value + "");
            SaveKey(key);
        }
        PlayerPrefs.Save();
    }

    public void Add(string key, bool value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetString(key, (value ? 1 : 0)+"");
        }
        else
        {
            PlayerPrefs.SetString(key, (value ? 1 : 0)+"");
            SaveKey(key);
        }
        PlayerPrefs.Save();
    }

    public T Get<T>(string key, T defaultValue = default)
    {
        if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(key, defaultValue?.ToString());
        }
        else if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue));
        }
        else if (typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(key, Convert.ToSingle(defaultValue));
        }
        else if (typeof(T) == typeof(bool))
        {
            return (T)(object)(PlayerPrefs.GetInt(key, (bool)(object)defaultValue ? 1 : 0) == 1);
        }
        else
        {
            Debug.LogError("Unsupported type.");
            return defaultValue;
        }
    }


    public void Delete(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            RemoveKey(key);
            PlayerPrefs.Save();
        }
    }

    void SaveKey(string key)
    {
        var keys = PlayerPrefs.GetString("PlayerPrefsKeys", "").Split(';');
        List<string> keyList = new List<string>(keys);
        if (!keyList.Contains(key))
        {
            keyList.Add(key);
            PlayerPrefs.SetString("PlayerPrefsKeys", string.Join(";", keyList.ToArray()));
        }
    }

    void RemoveKey(string key)
    {
        var keys = PlayerPrefs.GetString("PlayerPrefsKeys", "").Split(';');
        List<string> keyList = new List<string>(keys);
        if (keyList.Contains(key))
        {
            keyList.Remove(key);
            PlayerPrefs.SetString("PlayerPrefsKeys", string.Join(";", keyList.ToArray()));
        }
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}

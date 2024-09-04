using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

//配置抽象类
public abstract class ConfigCtr
{
    //配置基本类
    protected void LoadCfg(XmlNodeList xmlNodeList)
    {
        foreach (XmlElement xl1 in xmlNodeList)
        {
            LoadFunc(xl1);
        }
    }
    protected abstract void LoadFunc(XmlElement xl1);
    protected delegate void LoadDelegate(XmlElement xl1);

    public ConfigCtr(){ }
}

/// <summary>
/// 泛型配置结构
/// </summary>
public class CfgData<T>:ConfigCtr
   where T :ICfgData, new()
{
    public CfgData(XmlNodeList cfg)
    {
        cfgDatas = new Dictionary<string, T>();
        LoadCfg(cfg);
    }

    public CfgData(List<string[]> lines)
    {
        cfgDatas = new Dictionary<string, T>();

        List<string> keys = new List<string>();

        for(int i = 0; i < lines[0].Length; i++)
        {
            if (lines[0][i] == "")
            {
                continue;
            }
            keys.Add(lines[0][i]);
        }

        //Debug.Log(string.Format("配置表长度:{0}", keys.Count));

        for (int i = 1; i < lines.Count; i++)
        {
            string[] line = lines[i];
            if (line[0] == ""||line[0]==" ")
            {
                continue;
            }

            T nt = new T();
            nt.InitCfgId(line[0]);

            //Debug.Log(string.Format("配置数据长度:{0}", line.Length));

            for (int j = 0; j < line.Length; j++)
            {
                nt.InitValue(keys[j], line[j]);
            }

            cfgDatas.Add(line[0], nt);
        }
    }

    private Dictionary<string, T> cfgDatas;
    protected override void LoadFunc(XmlElement xl1)
    {
        T f = new T();
        foreach (XmlAttribute xl2 in xl1.Attributes)
        {
            f.InitValue(xl2.Name, xl2.Value);
        }
        f.InitCfgId(xl1.GetAttribute("KeyName"));
        cfgDatas.Add(f.GetCfgId(), f);
    }
    public T GetCfgData(string f)
    {
        if(f == null)
        {
            return default;
        }
        if (!cfgDatas.ContainsKey(f)) Debug.Log("不存在参数:" + f);
        return cfgDatas[f];
    }
    public bool ContainsCfg(string f)
    {
        return cfgDatas.ContainsKey(f);
    }
    public ICollection Keys
    {
        get { return cfgDatas.Keys; }
    }
    public ICollection Values
    {
        get { return cfgDatas.Values; }
    }
    public void Remove(string str)
    {
        if (cfgDatas.ContainsKey(str))
        {
            cfgDatas.Remove(str);
        }
    }
}

//配置
public class Config:ICfgData
{
    private string _id;
    public string Id { get { return _id; } }
    Dictionary<string, string> data = new Dictionary<string, string>();
    public void InitCfgId(string f)
    {
        _id = f;
    }

    public void InitValue(string fname, string f)
    {
        if (data.ContainsKey(fname))
        {
            Debug.Log("存在相同id" + fname);
            return;
        }
        data.Add(fname, f);
    }

    public string GetData(string fname)
    {
        if (!data.ContainsKey(fname))
            Debug.Log("不存在参数：" + fname);
        return data[fname];
    }

    public string GetCfgId()
    {
        return Id;
    }
}

////资源对象
//public class RscData<T>where T: Object
//{
//    private Dictionary<string, T> rscDatas;
//    public RscData(string path)
//    {
//        rscDatas = new Dictionary<string, T>();
//        T[] rscs = Resources.LoadAll<T>(path);
//        Debug.Log("读取资源" + path + " : " + rscs.Length);
//        foreach(T t in rscs)
//        {
//            rscDatas.Add(t.name, t);
//        }
//    }

//    public T GetRsc(string f)
//    {
//        if (rscDatas.ContainsKey(f))
//            return rscDatas[f];
//        else
//        {
//            Debug.Log("不存在资源:" + f);
//            if (rscDatas.ContainsKey("NoRsc"))
//            {
//                return rscDatas["NoRsc"];
//            }
//            else
//                return null;
//        }
//    }
//}

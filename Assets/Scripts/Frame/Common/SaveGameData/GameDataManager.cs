using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 玩家本地数据存储
/// </summary>
public class GameDataManager : Manager<GameDataManager>
{
    public XmlSaver xs = new XmlSaver();
    public string edition = "0.0.1";    //版本号
    public bool IsLoadingCloud;

    /// <summary>
    /// 本地存档默认路径
    /// </summary>
    public string FilePath
    {
        get
        {
            return GetDataPath() + "/Saves/save.dat";
        }
    }

    /// <summary>
    /// 云存档路径
    /// </summary>
    /// <returns></returns>
    public string FileCloudLocalPath
    {
        get
        {
            return GetDataPath() + "/Saves/cloud.dat";
        }
    }

    /// <summary>
    /// 读取存档
    /// </summary>
    /// <returns></returns>
    public byte[] Load(string name)
    {
        var localPath = GetDataPath() + "/Saves/" + name + ".dat";
        if (xs.HasFile(localPath))
        {
            var msg = xs.LoadXmlBytes(localPath);
            return msg; 
        }
        return null;
    }

    /// <summary>
    /// 读取存档
    /// </summary>
    /// <returns></returns>
    public string LoadByString(string name)
    {
        var localPath = GetDataPath() + "/Saves/" + name + ".dat";
        if (xs.HasFile(localPath))
        {
            var msg = xs.LoadXML(localPath);
            return msg;
        }
        return null;
    }

    /// <summary>
    /// 本地存储
    /// </summary>
    public void Save<T>(T data, string name) where T : IMessage<T>
    {
        string dataFilePath = GetDataPath() + "/Saves";
        if (!Directory.Exists(dataFilePath))
        {
            Directory.CreateDirectory(dataFilePath);
        }
        var p = data.ToByteArray();

        xs.CreateXML(dataFilePath + "/" + name + ".dat", p);
        //Debug.Log("存储成功:" + name);
    }

    /// <summary>
    /// 删除存档
    /// </summary>
    public void Delete(string name)
    {
        string dataFilePath = GetDataPath() + "/Saves/" + name + ".dat";
        if (xs.HasFile(dataFilePath))
        {
            Debug.Log("成功删除存档" + name);
            File.Delete(dataFilePath);
        }
    }

    /// <summary>
    /// 获取存档路径
    /// </summary>
    /// <returns>The data path.</returns>
    string GetDataPath()
    {
        string path = Application.persistentDataPath + "/saves";
        Debug.Log("存档位置是" + path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return path;
    }

    /// <summary>
    /// 通过url下载文件到本地路径
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public IEnumerator DownLoadFile(string url, string localPath)
    {
        IsLoadingCloud = true;
        if (xs.HasFile(localPath))
        {
            Debug.Log("成功删除本地文件" + localPath);
            File.Delete(localPath);
        }

        string dataFilePath = GetDataPath() + "/Saves";
        if (!Directory.Exists(dataFilePath))
        {
            Directory.CreateDirectory(dataFilePath);
        }

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse HWR = (HttpWebResponse)request.GetResponse();
        Debug.Log("localPath" + localPath);
        FileStream fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write);
        Stream stream = HWR.GetResponseStream();
        long length = HWR.ContentLength;
        long currentNum = 0;

        decimal currentProgress = 0;

        while (currentNum < length)
        {
            byte[] buffer = new byte[length];
            currentNum += stream.Read(buffer, 0, buffer.Length);
            fileStream.Write(buffer, 0, buffer.Length);
            if (currentNum % 1024 == 0)
            {
                currentProgress = Math.Round(Convert.ToDecimal(Convert.ToDouble(currentNum) / Convert.ToDouble(length) * 100), 4);
                Debug.Log("当前下载文件大小:" + length.ToString() + "字节   当前下载大小:" + currentNum + "字节 下载进度" + currentProgress.ToString() + "%");
            }
            else
            {
                Debug.Log("当前下载文件大小:" + length.ToString() + "字节   当前下载大小:" + currentNum + "字节" + "字节 下载进度" + 100 + "%");
            }
            yield return false;
        }

        HWR.Close();
        stream.Close();
        fileStream.Close();
        IsLoadingCloud = false;
    }


    /// <summary>
    /// 通过url下载文件到本地路径
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public IEnumerator CoverLoadFile(string context, string localPath)
    {
        IsLoadingCloud = true;
        if (xs.HasFile(localPath))
        {
            Debug.Log("成功删除本地文件" + localPath);
            File.Delete(localPath);
        }

        string dataFilePath = GetDataPath() + "/Saves";
        if (!Directory.Exists(dataFilePath))
        {
            Directory.CreateDirectory(dataFilePath);
        }

        xs.CreateXMLNoEn(localPath, context);
        yield return null;
    }





   /* /// <summary>
    /// 将数据保存在云端
    /// </summary>
    /// <param name="ac"></param>
    public void SavaCloudAccount(Account ac, Action<bool, string> action = null)
    {
        var p = ac.ToByteArray();
        string xxx = xs.GetString(p);
        CloudMrg.Instance.save(xxx, action);

    }
    public void GetCloudAccount(Action ac)
    {
        CloudMrg.Instance.load((success, data) =>
        {
            if (success)
            {
                var dt = LitJson.JsonMapper.ToObject<loadData>(data);
               
                if (dt.status == 0)
                {
                    try
                    {
                        var xxx = xs.GetBytes(dt.data);
                        var rt = Account.Parser.ParseFrom(xxx);
                        GameBase.Instance.Account = rt;
                    }
                    catch 
                    {
                        GameBase.Instance.Account = null;
                    }
                }
                else
                {
                    GameBase.Instance.Account = null;
                    DebugEX.Log("网络失败");
                }
            }
            else
            {
                DebugEX.Log("网络失败");
            }
            ac?.Invoke();
        });
    }

    public void GetPayItem()
    {
        CloudMrg.Instance.GetPurchase_items((success, data) =>
        {
            
            if (success)
            {
                var dt = LitJson.JsonMapper.ToObject<PurchaseData>(data);
                if (dt.items== null) 
                {
                    return;
                }
                for (int i = 0; i < dt.items.Count; i++)
                {
                    ShopManger.Instance.AddPurchaseItem(dt.items[i]);
                   // DebugEX.LogError("内购数据", dt.items[i].product_type);
                }
            }
            else
            {
                DebugEX.LogError("内购数据err", data);
            }
        });
    }*/

}

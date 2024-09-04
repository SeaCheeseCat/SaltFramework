using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResConst
{
    public const string ASSETS_LOAD_PREFIX = "assets.resources.";            //资源加载前缀
    public const string ExtName = ".ab";                                     //资源扩展名
    public const string AssetDir = "StreamingAssets/AssetBundles";           //资源加载目录 
    public const string manifastName = "AssetBundles";

    /// <summary>
    /// 取得数据存放目录
    /// </summary>
    public static string DataPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return Application.streamingAssetsPath + "/" + manifastName + "/";
            }
            if(Application.platform == RuntimePlatform.Android)
            {
                return Application.dataPath + "!assets/" + manifastName + "/";
            }
            if (Application.platform == RuntimePlatform.OSXEditor)
            {
                string dir = System.Environment.CurrentDirectory;
                return dir + "/Assets/" + AssetDir + "/";
            }
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                string dir = System.Environment.CurrentDirectory;
                return dir + "/Assets/" + AssetDir + "/";
            }
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return Application.dataPath + "/" + AssetDir + "/";
            }
            return "";
        }
    }

    public static string GetRelativePath()
    {
        if (Application.isEditor)
            return "file://" + DataPath;
        else if (Application.isMobilePlatform || Application.isConsolePlatform)
            return "file:///" + DataPath;
        else // For standalone player.
            return "file://" + DataPath;
    }
}

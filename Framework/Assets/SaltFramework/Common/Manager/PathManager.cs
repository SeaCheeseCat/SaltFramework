using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public static class PathManager
{
    public const string ProjectPath = "..";
    public const string JsonScriptsPath = "Scripts/Game/Configs/Types/ResType/";
    public const string JsonPath = "Resources/Config/Table/";
    public const string MapSavePath = "Resources/Scene/Map";
    public const string UIScriptsPath = "Scripts/UI";
    public const string UIprefab = "UI/";


    public const string Data = "Data";
    public static string GetPath
    {
        get
        {
            return Application.dataPath + "/";
        }
    }


    #region AB资源相关
    /// <summary>
    /// 资源根目录
    /// </summary>
    public const string ResourceRoot = "/AssetBundles/";

    public const string FileListName = "FileList.txt";

    public const string AssetDir = "StreamingAssets/AssetBundles";           //资源加载目录 
    public const string manifastName = "AssetBundles";

    public const string ASSETS_LOAD_PREFIX = "assets.resources.";            //资源加载前缀
    public const string ExtName = ".ab";                                     //资源扩展名


    public static string PlatformPath
    {
        get
        {
            string path =
#if UNITY_ANDROID 
    "android/";
#elif UNITY_IPHONE
    "ios/";
#elif UNITY_STANDLONE_WIN|| UNITY_EDITOR
        "win64/";

#else
    string.Empty;
#endif
            return path;
        }
    }





    public static string StreamingAssets
    {
        get
        {

            string path =
#if DouYin&&!UNITY_EDITOR
FileTools.dyFilePath;
#else
Application.streamingAssetsPath;
#endif

            return path;
        }
    }


    public static string PersistentDataPath
    {
        get
        {
            string path =

#if DouYin&&!UNITY_EDITOR
FileTools.dyFilePath;
#else
Application.persistentDataPath;
#endif


            return path;
        }
    }
    #endregion


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
            if (Application.platform == RuntimePlatform.Android)
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

    /// <summary>
    /// 获取数据持久化地址(可读可写)
    /// </summary>
    public static string PersistentPath
    {
        get
        {
            return Application.persistentDataPath + "/";
        }
    }


    public static string PlatformSwitchingUrl(string url)
    {
        string Rurl =
            new System.Uri(Path.Combine(url)).AbsoluteUri;
        Application.HasProLicense();
        //#if UNITY_ANDROID&& !UNITY_EDITOR
        //         url;
        //#elif UNITY_IPHONE && !UNITY_EDITOR
        //        "file://"+ url;
        //#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
        //        "file:///" + url;
        //#else
        //        string.Empty;
        //#endif
        return Rurl;
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
    public static void Init()
    {
        var path = PersistentPath + Data;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            DebugEX.Log("创建数据存储文件夹");
        }
        else
        {
            DebugEX.Log("数据存储文件夹已存在");
        }
#if UNITY_EDITOR
        //System.Diagnostics.Process.Start(path);
#endif
    }
}

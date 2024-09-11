using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UObject = UnityEngine.Object;
using System;

public class AssetBundleInfo
{
    public AssetBundle m_AssetBundle;
    public int m_ReferencedCount;

    public AssetBundleInfo (AssetBundle assetBundle)
    {
        m_AssetBundle = assetBundle;
        m_ReferencedCount = 0;
    }
}

public class ResourceManager : Manager<ResourceManager>
{
    // private GameBase m_gameBase;

    private string m_BaseDownloadingURL = "";
    private string[] m_AllManifest = null;
    private AssetBundleManifest m_AssetBundleManifest = null;
    private Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]> ();
    private Dictionary<string, AssetBundleInfo> m_LoadedAssetBundles = new Dictionary<string, AssetBundleInfo> ();
    private Dictionary<string, AsyncLoadRequest> m_LoadRequests = new Dictionary<string, AsyncLoadRequest> ();
 

    public override IEnumerator Init(MonoBehaviour obj)
    {
        if (GameBase.Instance != null)
        {
            if (GameBase.Instance.LoadAb)
            {
                Debug.Log("从ab包中加载");
                m_BaseDownloadingURL = ResConst.GetRelativePath();
                m_AssetBundleManifest = LoadAssetSync<AssetBundleManifest>(ResConst.manifastName, "AssetBundleManifest");
                m_AllManifest = m_AssetBundleManifest.GetAllAssetBundles();
            }
        }
       
        return base.Init(obj);
    }

    #region 工具方法
    private string GetRealAssetPath (string abName)
    {
        abName = abName.Replace ('/', '.');
        if (abName.Equals (ResConst.manifastName))
        {
            return abName;
        }
        abName = abName.ToLower ();
        if (!abName.EndsWith (ResConst.ExtName, StringComparison.Ordinal))
        {
            abName += ResConst.ExtName;
        }

        // for (int i = 0; i < m_AllManifest.Length; i++)
        // {
        //     int index = m_AllManifest[i].LastIndexOf('/');
        //     string path = m_AllManifest[i].Remove(0, index + 1);    //字符串操作函数都会产生GC
        //     if (path.Equals(abName))
        //     {
        //         return m_AllManifest[i];
        //     }
        // }
        // Debug.Log ("GetRealAssetPath " + abName);
        return abName;
    }

    private AssetBundleInfo GetLoadedAssetBundle (string abName)
    {
        AssetBundleInfo bundle = null;
        if (abName == null)
        {
            Debug.LogError ("ab name error");
        }

        m_LoadedAssetBundles.TryGetValue (abName, out bundle);
        if (bundle == null) return null;

        // No dependencies are recorded, only the bundle itself is required.
        string[] dependencies = null;
        if (!m_Dependencies.TryGetValue (abName, out dependencies))
            return bundle;

        // Make sure all dependencies are loaded
        foreach (var dependency in dependencies)
        {
            AssetBundleInfo dependentBundle;
            m_LoadedAssetBundles.TryGetValue (dependency, out dependentBundle);
            if (dependentBundle == null) return null;
        }
        return bundle;
    }
    #endregion

    #region 同步公共接口

    /// <summary>
    /// 同步生成目标资源的GameObject
    /// </summary>
    /// <returns>The object sync.</returns>
    /// <param name="assetPath">Asset path.</param>
    public static GameObject AllocObjectSync (string assetPath)
    {
        int index = assetPath.LastIndexOf ('/');
        return Instance.AllocObjectSyncInner(assetPath, assetPath.Substring(index + 1, assetPath.Length - index - 1));
    }

    /// <summary>
    /// 同步加载目标资源
    /// </summary>
    /// <returns>The prefab sync.</returns>
    /// <param name="assetPath">Asset path.</param>
    public static UObject LoadPrefabSync (string assetPath)
    {
        int index = assetPath.LastIndexOf ('/');
        return Instance.LoadPrefabSyncInner (assetPath, assetPath.Substring (index + 1, assetPath.Length - index - 1));
    }

    /// <summary>
    /// 同步加载目标资源
    /// </summary>
    /// <returns>The prefab sync.</returns>
    /// <param name="abName">Ab name.</param>
    /// <param name="assetNames">Asset names.</param>
    public static UObject LoadPrefabSync (string abName, string assetNames)
    {
        return Instance.LoadPrefabSyncInner (abName, assetNames);
    }

    #endregion

    #region 异步公共接口

    /// <summary>
    /// 异步生成目标资源的GameObject
    /// </summary>
    /// <returns>The object async.</returns>
    /// <param name="assetPath">Asset path.</param>
    public static void AllocObjectAsync(string assetPath, Action<GameObject> action)
    {
        int index = assetPath.LastIndexOf('/');
        Instance.AllocObjectAsyncInner(assetPath, assetPath.Substring(index + 1, assetPath.Length - index - 1), action);
    }

    /// <summary>
    /// 异步加载目标资源
    /// </summary>
    /// <returns>The prefab async.</returns>
    /// <param name="assetPath">Asset path.</param>
    public static void LoadPrefabAsync (string assetPath, Action<UObject> action)
    {
        int index = assetPath.LastIndexOf('/');
        Instance.LoadPrefabAsyncInner(assetPath, assetPath.Substring(index + 1, assetPath.Length - index - 1), action);
    }

    /// <summary>
    /// 异步加载目标资源
    /// </summary>
    /// <returns>The prefab sync.</returns>
    /// <param name="abName">Ab name.</param>
    /// <param name="assetNames">Asset names.</param>
    public static void LoadPrefabAsync (string abName, string assetNames, Action<UObject> action)
    {
        Instance.LoadPrefabAsyncInner (abName, assetNames, action);
    }

    public static IEnumerator CheckPrefabAsync(string abName)
    {
        bool onLoad = true;
        ResourceManager.LoadPrefabAsync(abName, delegate (UnityEngine.Object o) {
            onLoad = false;
            if (o == null)
            {
                Debug.LogError("未找到资源" + abName);
            }
        });
        while (onLoad)
        {
            yield return null;
        }
    }

    #endregion

    #region 同步加载逻辑
    /// <summary>
    /// 同步从Prefab创建Gameobject
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    private GameObject AllocObjectSyncInner (string abName, string assetName)
    {
        UObject obj = LoadPrefabSyncInner (abName, assetName);
        if (obj != null)
        {
            return GameObject.Instantiate(obj) as GameObject;
        }
        else
        {
            Debug.LogError("生成对象为空" + abName + " " + assetName);
            return null;
        }
    }

    /// <summary>
    /// 同步加载Prefab
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="assetNames"></param>
    /// <returns></returns>
    private UObject LoadPrefabSyncInner(string abName, string assetNames)
    {
        //return Resources.Load(abName) as UObject;

/*        if (GameBase.Instance != null && GameBase.Instance.LoadAb)
        {
            return LoadAssetSync<UObject>(ResConst.ASSETS_LOAD_PREFIX + abName, assetNames);
        }
        else
        {*/
            return Resources.Load(abName) as UObject;
        //}
    }

    /// <summary>
    /// 同步载入素材入口
    /// </summary>
    private T LoadAssetSync<T> (string abname, string assetname) where T : UnityEngine.Object
    {
        AssetBundle bundle = LoadAssetBundleSync<T> (abname);
        if (bundle == null) return null;
        return bundle.LoadAsset<T> (assetname);
    }

    /// <summary>
    /// 载入AssetBundle
    /// </summary>
    /// <param name="abname"></param>
    /// <returns></returns>
    private AssetBundle LoadAssetBundleSync<T> (string abname)
    {
        abname = GetRealAssetPath (abname);
        AssetBundleInfo bundle = GetLoadedAssetBundle (abname);
        if (bundle == null)
        {
            //byte[] stream = null;
            string uri = ResConst.DataPath + abname;
            // Debug.Log ("LoadFile::>> " + abname);

            //stream = File.ReadAllBytes(uri);
            //bundle = new AssetBundleInfo(AssetBundle.LoadFromMemory(stream)); //关联数据的素材绑定
            var ab = AssetBundle.LoadFromFile (uri);
            if (ab == null) return null;

            bundle = new AssetBundleInfo (ab); //关联数据的素材绑定
            m_LoadedAssetBundles.Add (abname, bundle);
            bundle.m_ReferencedCount++;

            if (typeof (T) != typeof (AssetBundleManifest))
            {
                LoadDependenciesSync<T> (abname);
            }
        }
        return bundle.m_AssetBundle;
    }

    /// <summary>
    /// 载入依赖
    /// </summary>
    /// <param name="name"></param>
    private void LoadDependenciesSync<T> (string name)
    {
        if (m_AssetBundleManifest == null)
        {
            Debug.LogError ("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
            return;
        }

        // Get dependecies from the AssetBundleManifest object..
        string[] dependencies = m_AssetBundleManifest.GetAllDependencies (name);
        if (dependencies.Length > 0)
        {
            if (!m_Dependencies.ContainsKey (name))
            {
                m_Dependencies.Add (name, dependencies);
            }
            for (int i = 0; i < dependencies.Length; i++)
            {
                string depName = dependencies[i];
                AssetBundleInfo bundleInfo = null;
                if (m_LoadedAssetBundles.TryGetValue (depName, out bundleInfo))
                {
                    bundleInfo.m_ReferencedCount++;
                }
                else
                {
                    LoadAssetBundleSync<T> (dependencies[i]);
                }
            }
        }
    }
    #endregion

    #region 异步加载逻辑

    /// <summary>
    /// 异步从Prefab创建Gameobject
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    private void AllocObjectAsyncInner (string abName, string assetName, Action<GameObject> action)
    {
        LoadPrefabAsyncInner (abName, assetName, (uobj) =>
        {
            if (uobj != null)
            {
                var val = GameObject.Instantiate (uobj) as GameObject;
                action.Invoke (val);
            }
            else
            {
                action.Invoke (null);
            }
        });

    }

    /// <summary>
    /// 异步加载Prefab
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="assetNames"></param>
    /// <returns></returns>
    private void LoadPrefabAsyncInner(string abName, string assetNames, Action<UObject> action)
    {
        if (GameBase.Instance.LoadAb)
        {
            Debug.Log("加载数据"+ ResConst.ASSETS_LOAD_PREFIX + abName);
            LoadAssetAsync<UObject>(ResConst.ASSETS_LOAD_PREFIX + abName, assetNames, action);
        }
        else
        {
            var req = Resources.LoadAsync<UObject>(abName);
            req.completed += (op) =>
            {
                if (req.isDone)
                {
                    if (action != null)
                    {
                        action.Invoke(req.asset);
                    }
                }
            };
        }
    }

    /// <summary>
    /// 异步载入素材入口
    /// </summary>
    private void LoadAssetAsync<T> (string abname, string assetname, Action<T> action) where T : UnityEngine.Object
    {
        LoadAssetBundleAsync<T> (abname, (bundle) =>
        {
            if (bundle == null)
            {
                if (action != null)
                {
                    action.Invoke (null);
                }
            }
            else
            {
                var req = bundle.LoadAssetAsync<T> (assetname);
                req.completed += (op) =>
                {
                    if (action != null)
                    {
                        action.Invoke (req.asset as T);
                    }
                };
            }
        });

    }

    /// <summary>
    /// 载入AssetBundle
    /// </summary>
    /// <param name="abname"></param>
    /// <returns></returns>
    private void LoadAssetBundleAsync<T> (string abname, Action<AssetBundle> action)
    {
        abname = GetRealAssetPath (abname);
        AssetBundleInfo bundle = GetLoadedAssetBundle (abname);
        if (bundle == null)
        {
            string uri = ResConst.DataPath + abname;

            // var req =
            if (m_LoadRequests.ContainsKey (abname))
            {
                if (action != null)
                {
                    m_LoadRequests[abname].action += action;
                }
            }
            else
            {
                AsyncLoadRequest req = new AsyncLoadRequest ();
                req.request = AssetBundle.LoadFromFileAsync (uri);
                req.action = action;
                m_LoadRequests.Add (abname, req);

                req.request.completed += (op) =>
                {
                    if (m_LoadedAssetBundles.ContainsKey (abname))
                    {
                        Debug.Log ("异步加载被同步加载打断,直接获取读取同步加载的资源 " + abname);
                        //处理资源被同步加载完成的情况
                        if (action != null)
                        {
                            action.Invoke (m_LoadedAssetBundles[abname].m_AssetBundle);
                        }
                    }
                    else
                    {
                        if (req.request.assetBundle == null)
                        {
                            if (req.action != null)
                            {
                                req.action.Invoke (null);
                            }
                            return;
                        }

                        bundle = new AssetBundleInfo (req.request.assetBundle);
                        m_LoadedAssetBundles.Add (abname, bundle);
                        bundle.m_ReferencedCount++;

                        if (typeof (T) != typeof (AssetBundleManifest))
                        {
                            LoadDependenciesAsync<T> (abname, (finished) =>
                            {
                                m_LoadRequests.Remove (abname);
                                if (req.action != null)
                                {
                                    req.action.Invoke (bundle.m_AssetBundle);
                                }
                            });
                        }
                        else
                        {
                            if (action != null)
                            {
                                action.Invoke (bundle.m_AssetBundle);
                            }
                        }
                    }
                };
            }
        }
        else
        {
            if (action != null)
            {
                action.Invoke (bundle.m_AssetBundle);
            }
        }
    }

    /// <summary>
    /// 载入依赖
    /// </summary>
    /// <param name="name"></param>
    private void LoadDependenciesAsync<T> (string name, Action<bool> complete)
    {
        if (m_AssetBundleManifest == null)
        {
            complete.Invoke (false);
            return;
        }

        // Get dependecies from the AssetBundleManifest object..
        string[] dependencies = m_AssetBundleManifest.GetAllDependencies (name);
        if (dependencies.Length > 0)
        {
            if (!m_Dependencies.ContainsKey (name))
            {
                m_Dependencies.Add (name, dependencies);
            }
            int dpLen = dependencies.Length;
            for (int i = 0; i < dependencies.Length; i++)
            {
                string depName = dependencies[i];
                AssetBundleInfo bundleInfo = null;
                if (m_LoadedAssetBundles.TryGetValue (depName, out bundleInfo))
                {
                    bundleInfo.m_ReferencedCount++;
                    dpLen--;
                }
                else if (!m_LoadRequests.ContainsKey (depName))
                {
                    LoadAssetBundleAsync<T> (depName, (loadAsset) =>
                    {
                        dpLen--;
                        if (dpLen <= 0)
                        {
                            complete.Invoke (true);
                        }
                    });
                }
                else
                {
                    dpLen--;
                }
            }

            if (dpLen <= 0)
            {
                complete.Invoke (true);
            }
        }
        else
        {
            complete.Invoke (true);
        }
    }

    private class AsyncLoadRequest
    {
        public AssetBundleCreateRequest request;
        public Action<AssetBundle> action;
    }

    #endregion

    #region 资源卸载逻辑
    public static void UnloadAllAssetBundles (bool isThorough = false)
    {
        var list = Instance.m_LoadedAssetBundles;
        var remove = new List<string> ();
        foreach (var info in list)
        {
            if (info.Key == "AssetBundles")
            {
                continue;
            }
            else if (isThorough)
            {
                info.Value.m_AssetBundle.Unload (true);
                remove.Add (info.Key);
            }
            else if (info.Value.m_ReferencedCount <= 0)
            {
                info.Value.m_AssetBundle.Unload (true);
                remove.Add (info.Key);
            }
            else
            {
                Debug.Log (info.Key + "引用数" + info.Value.m_ReferencedCount);
                continue;
            }

            Debug.Log ("卸载了" + info.Key);
        }

        foreach (var del in remove)
        {
            Instance.m_LoadedAssetBundles.Remove (del);
        }
    }

    /// <summary>
    /// 此函数交给外部卸载专用，自己调整是否需要彻底清除AB
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="isThorough"></param>
    public static void UnloadAssetBundle (string abName, bool isThorough = false)
    {
        Instance.UnloadAssetBundleInner (ResConst.ASSETS_LOAD_PREFIX + abName, isThorough);
    }

    public void UnloadAssetBundleInner (string abName, bool isThorough = false)
    {
        if (GameBase.Instance.LoadAb) {
            abName = GetRealAssetPath (abName);
            if (UnloadAssetBundleInternal (abName, isThorough))
            {
                UnloadDependencies (abName, isThorough);
            }
        }
    }

    private void UnloadDependencies (string abName, bool isThorough)
    {
        string[] dependencies = null;
        if (!m_Dependencies.TryGetValue (abName, out dependencies))
            return;

        // Loop dependencies.
        foreach (var dependency in dependencies)
        {
            UnloadAssetBundle (dependency, isThorough);
        }
        m_Dependencies.Remove (abName);
    }

    private bool UnloadAssetBundleInternal (string abName, bool isThorough)
    {
        AssetBundleInfo bundle = GetLoadedAssetBundle (abName);
        if (bundle == null) return false;

        if (--bundle.m_ReferencedCount <= 0)
        {
            if (m_LoadRequests.ContainsKey (abName))
            {
                return false; //如果当前AB处于Async Loading过程中，卸载会崩溃，只减去引用计数即可
            }
            bundle.m_AssetBundle.Unload (isThorough);
            m_LoadedAssetBundles.Remove (abName);
            // Debug.Log ("[" + abName + "] has been unloaded successfully");
        }
        return true;
    }

    #endregion
}

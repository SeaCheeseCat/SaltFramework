

# **资源管理器 - ResourceManager**



*`ResourceManager` 类实现了 Unity 的资源管理系统，主要负责资源的加载、异步加载、依赖处理和卸载*



## 类结构

### ResourceManager

`ResourceManager` 继承自 `Manager<>`



#### 成员变量

- **`_loadedObjects`**：用于缓存已经加载的资源对象。
- **`_loadedAssetBundles`**：用于缓存已经加载的 AssetBundle。

#### 方法

- **`AllocObjectSync<T>`**：同步加载指定路径的资源对象。

  - **参数**：`path`：资源的路径。
  - **返回**：类型为 `T` 的资源对象。

- **`AllocObjectAsync<T>`**：异步加载指定路径的资源对象。

  - 参数：- `path`：资源的路径。- `callback`：加载完成后的回调函数。
  - **返回**：无返回值。

- **`LoadPrefabSync`**：同步加载指定路径的 Prefab。

  - **参数**：`path`：Prefab 的路径。
  - **返回**：加载的 `GameObject`。

- **`LoadPrefabAsync`**：异步加载指定路径的 Prefab。

  - 参数： - `path`：Prefab 的路径。
         - `callback`：加载完成后的回调函数。
    
  - **返回**：无返回值。

- **`LoadAssetBundleSync`**：同步加载指定 AssetBundle。

  - **参数**：`bundleName`：AssetBundle 的名称。
  - **返回**：加载的 `AssetBundle`。

- **`LoadAssetBundleAsync`**：异步加载指定 AssetBundle。

  - 参数 ：- `bundleName`：AssetBundle 的名称。- `callback`：加载完成后的回调函数。

  - **返回**：无返回值。

- **`LoadDependenciesSync`**：同步加载指定资源的所有依赖。

  - **参数**：`path`：资源的路径。
  - **返回**：无返回值。

- **`LoadDependenciesAsync`**：异步加载指定资源的所有依赖。

  - 参数：- `path`：资源的路径。 - `callback`：加载完成后的回调函数。
  - **返回**：无返回值。

- **`UnloadAllAssetBundles`**：卸载所有 AssetBundle。

  - **参数**：`unloadAllLoadedObjects`：是否卸载所有已加载的对象。
  - **返回**：无返回值。





## 示例

```c#
// 资源路径
private string prefabPath = "Assets/Prefabs/MyPrefab.prefab";
private string assetBundleName = "myassetbundle";

void Start()
{
	// 示例：同步加载 Prefab
	GameObject prefab = ResourceManager.Instance.LoadPrefabSync(prefabPath);
	if (prefab != null)
    {
        Instantiate(prefab);
        Debug.Log("Prefab loaded and instantiated.");
	}
    else
    {
    	Debug.LogError("Failed to load Prefab.");
    }

    // 示例：异步加载 Prefab
    ResourceManager.Instance.LoadPrefabAsync(prefabPath, (loadedPrefab) =>
    {
    	if (loadedPrefab != null)
    	{
        	Instantiate(loadedPrefab);
        	Debug.Log("Async Prefab loaded and instantiated.");
    	}
    else
    {
        Debug.LogError("Failed to load Prefab asynchronously.");
    }
    });

    // 示例：异步加载 AssetBundle
    ResourceManager.Instance.LoadAssetBundleAsync(assetBundleName, 	(loadedBundle) =>
    {
    if (loadedBundle != null)
    {
    	Debug.Log("AssetBundle loaded.");
    }
    else
    {
    Debug.LogError("Failed to load AssetBundle.");
    }
    });

    // 示例：同步加载资源的所有依赖
    ResourceManager.Instance.LoadDependenciesSync(prefabPath);

    // 示例：卸载所有 AssetBundle
    ResourceManager.Instance.UnloadAllAssetBundles(true);
}
```




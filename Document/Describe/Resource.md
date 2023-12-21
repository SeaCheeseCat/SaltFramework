# **资源管理**

Hello，这里是资源管理ResourceManager

是对原有Unity本身自带的Resource类的进一步封装



## 内置模块

- 同步加载资源

- 异步加载资源

  

**如何使用ResourceManager**

​	在我们常用的Unity内置的Resource类中，只能进行较为简单的加载预制体等需求。

​	而在ResourceManager我为你封装了以下几个方法

```
/// 同步生成目标资源的GameObject
public static GameObject AllocObjectSync (string assetPath)

/// 同步加载目标资源
public static UObject LoadPrefabSync (string assetPath)

/// 异步加载目标资源
public static void LoadPrefabAsync (string assetPath, Action<UObject> action)
```

>   同步生成方法中你将不需要重新实例化该资源目标，直接拿到该物体
>

  

 




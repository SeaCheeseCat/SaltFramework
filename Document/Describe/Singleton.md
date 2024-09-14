

# **单例模式- Singleton**



*`EventManager` 类用于管理和触发游戏中的各种事件。它继承自 `Manager<EventManager>` 并提供了一系列方法来处理事件的触发和管理*



## 类结构

### `SingleMono<T>`

**`T Instance`**

- **描述**: 获取当前 MonoBehaviour 类型 `T` 的单例实例。
- **返回值**: `T` 类型的单例实例。

**`void Awake()`**

- **描述**: 初始化 `instance`，在第一次调用时获取当前组件的实例。
- **备注**: 该方法是虚拟方法，允许子类重写。

### `Singleton<T>`

**`T Instance`**

- **描述**: 获取当前类型 `T` 的单例实例。如果实例不存在，则创建一个新的实例。
- **返回值**: `T` 类型的单例实例。

### 示例：

```c#
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 实体管理类，继承自SingleMono
public class EntityManager : SingleMono<EntityManager>
{
    public List<GameObject> entities = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();  // 确保单例实例化
    }

    // 添加实体
    public void AddEntity(GameObject entity)
    {
        entities.Add(entity);
    }

    // 移除实体
    public void RemoveEntity(GameObject entity)
    {
        entities.Remove(entity);
    }

    // 获取所有实体
    public List<GameObject> GetAllEntities()
    {
        return entities;
    }
}

// 示例使用EntityManager的地方
public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 添加一个新实体到实体管理器
        GameObject newEntity = new GameObject("Entity1");
        EntityManager.Instance.AddEntity(newEntity);

        // 打印当前实体列表数量
        Debug.Log("当前实体数量: " + EntityManager.Instance.GetAllEntities().Count);
    }
}

```




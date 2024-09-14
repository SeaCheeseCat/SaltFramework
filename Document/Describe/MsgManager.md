

# **消息传递机制 - MsgManager**



*`MsgManager` 是一个消息管理器，用于在系统中发送和接收消息。它实现了消息的注册、注销和发送功能，支持不同类型的消息参数。通过使用 `MsgManager`，你可以有效地管理游戏中的消息通信。*



## 类结构

### MsgManager

`MsgManager` 继承自 `Manager<MsgManager>`

#### 方法

- `Init(MonoBehaviour obj)`：初始化对象池系统，包括加载池根对象和创建各种对象池。

- `InitData()`：根据配置初始化对象池。

- `InitByPoolObj()`：通过配置文件初始化对象池。

- `InitByScript()`：通过脚本手动初始化对象池（目前未实现）。

  

### ClassPool<T>

`ClassPool<T>` 是一个通用的类对象池，提供了对类对象的池化管理。

#### 成员变量

- `pool`：存储池中对象的栈。
- `maxCount`：池的最大缓存数量。

#### 方法

- `Spawn()`：从池中获取一个对象，如果池为空，则创建新对象。
- `Recycle(T obj)`：将对象回收到池中。
- `Release()`：释放所有池中的对象。





### GameObjectPool

`GameObjectPool` 负责管理 `GameObject` 类型的对象池。

#### 成员变量

- `pool`：存储池中对象的数据字典。
- `node`：对象池的根节点。
- `limit`：对象池的最大数量。
- `index`：对象池的索引。

#### 方法

- `InitPerfab(string path, int num)`：初始化对象池中的预制体。
- `SetPoolLimit(string name, int count)`：设置对象池的数量限制。
- `Spawn(string path)`：从池中获取一个 `GameObject` 对象。
- `Spawn(GameObject obj)`：从池中获取一个 `GameObject` 对象（通过预制体路径）。
- `Recycle(GameObject obj)`：将 `GameObject` 对象回收到池中。
- `Release()`：释放所有池中的 `GameObject` 对象。

### IPoolable

`IPoolable` 是一个接口，定义了对象池可用对象的标准接口。

#### 方法

- `OnSpawn()`：在对象被从池中取出时调用。
- `OnRecycle()`：在对象被回收到池中时调用。

### InitPoolEnum

`InitPoolEnum` 是一个枚举类型，用于定义对象池的初始化方式。

- `Off`：关闭对象池初始化。
- `OnByPoolObj`：通过配置文件初始化对象池。
- `OnByScript`：通过脚本初始化对象池



## API 方法

#### `void Spawn(string path, int num)`

- **描述**: 从对象池中预加载指定数量的对象。

- 参数:

  - `path` (`string`): 预制体的资源路径。
  - `num` (`int`): 预加载的对象数量。
  
- **返回值**: 无



#### `bool Recycle(GameObject obj)`

- **描述**: 将对象回收到对象池中。

- 参数:

  - `obj` (`GameObject`): 要回收的对象。

- **返回值**: `bool` - 如果对象成功回收则返回 `true`，否则返回 `false`。

#### `void Release()`

- **描述**: 释放对象池中的所有对象。
- **参数**: 无
- **返回值**: 无

### 示例：

```c#
// 创建消息管理器实例
MsgManager msgManager = MsgManager.Instance;
// 发送消息，类型为 "PlayerDied"
msgManager.SendMessage("PlayerDied");
// 创建消息管理器实例
MsgManager msgManager = MsgManager.Instance;
// 发送消息，类型为 "PlayerHealthChanged"，布尔值为 true，整型参数为 100
msgManager.SendMessage("PlayerHealthChanged", true, 100);
// 创建消息管理器实例
MsgManager msgManager = MsgManager.Instance;
// 发送消息，类型为 "EnemyHit"，整型参数为 50，消息类型为 eInfoType.normal
msgManager.SendMessage("EnemyHit", 50);
// 创建消息管理器实例
MsgManager msgManager = MsgManager.Instance;
// 创建一个示例对象
object exampleObject = new { Name = "Example", Value = 123 };
// 发送消息，类型为 "ItemPickedUp"，整型参数为 1，对象参数为 exampleObject
msgManager.SendMessage("ItemPickedUp", 1, exampleObject);
// 创建消息管理器实例
MsgManager msgManager = MsgManager.Instance;
// 创建一个示例对象
object data = new { Score = 1000 };
// 发送消息，类型为 "ScoreUpdated"，对象参数为 data，消息类型为 eInfoType.normal
msgManager.SendMessage("ScoreUpdated", data);



// 创建消息管理器实例
MsgManager msgManager = MsgManager.Instance;
// 定义处理函数
void OnPlayerHealthChanged(MsgData msg)
{
    Debug.Log("Player health changed. New health: " + msg.arg1);
}

void OnEnemyHit(MsgData msg)
{
    Debug.Log("Enemy hit with damage: " + msg.arg1);
}
// 注册多个消息监听
msgManager.AddObserver("PlayerHealthChanged", OnPlayerHealthChanged);
msgManager.AddObserver("EnemyHit", OnEnemyHit);

// 发送测试消息
msgManager.SendMessage("PlayerHealthChanged", 80);
msgManager.SendMessage("EnemyHit", 50);
```






# **对象池管理 - PoolManager**



*`PoolManager` 类负责管理不同类型的对象池，包括 UI 对象池、系统对象池和其他游戏对象池。通过对象池系统可以有效管理和复用游戏对象，提高性能并减少内存开销。*



## 类结构

### PoolManager

`PoolManager` 继承自 `Manager<PoolManager>`，是对象池系统的核心管理类。它负责初始化和管理各种对象池，并提供对象池的创建和回收功能。





#### 成员变量

- **`m_table`**: `Dictionary<string, List<MsgHandler>>`
  存储消息类型及其对应的消息处理委托列表。
- **`m_invoking`**: `HashSet<List<MsgHandler>>`
  记录正在处理中的消息列表，用于避免在处理消息时修改列表。

#### 方法

- **`Init(MonoBehaviour obj)`**
  初始化 `MsgManager`，清空消息表，并调用基类初始化方法。
- **`SendMessage(string msgType)`**
  发送消息（无参数版本）。创建一个 `MsgData` 实例，并调用 `SendMessage(msgType, msg)` 进行发送。
- **`SendMessage(string msgType, bool boolean, int arg1)`**
  发送消息（布尔值和一个整型参数版本）。创建一个 `MsgData` 实例，并设置 `boolArg` 和 `arg1` 属性。
- **`SendMessage(string msgType, int arg, eInfoType type = eInfoType.normal)`**
  发送消息（单个整型参数版本）。创建一个 `MsgData` 实例，并设置 `arg1` 和 `type` 属性。
- **`SendMessage(string msgType, int arg, object obj, eInfoType type = eInfoType.normal)`**
  发送消息（整型和对象参数版本）。创建一个 `MsgData` 实例，并设置 `arg1`、`data` 和 `type` 属性。
- **`SendMessage(string msgType, int arg, int arg2, eInfoType type = eInfoType.normal)`**
  发送消息（两个整型参数版本）。创建一个 `MsgData` 实例，并设置 `arg1`、`arg2` 和 `type` 属性。
- **`SendMessage(string msgType, int arg, int arg2, string arg3, eInfoType type = eInfoType.normal)`**
  发送消息（三个参数版本，包括一个字符串）。创建一个 `MsgData` 实例，并设置 `arg1`、`arg2`、`msg1` 和 `type` 属性。
- **`SendMessage(string msgType, int arg, int arg2, int arg3, eInfoType type = eInfoType.normal)`**
  发送消息（三个整型参数版本）。创建一个 `MsgData` 实例，并设置 `arg1`、`arg2`、`arg3` 和 `type` 属性。
- **`SendMessage(string msgType, bool boolean, Vector3 vec3, eInfoType type = eInfoType.normal)`**
  发送消息（布尔值和 `Vector3` 参数版本）。创建一个 `MsgData` 实例，并设置 `boolArg`、`vec3` 和 `type` 属性。
- **`SendMessage(string msgType, bool arg, eInfoType type = eInfoType.normal)`**
  发送消息（布尔值版本）。创建一个 `MsgData` 实例，并设置 `boolArg` 和 `type` 属性。
- **`SendMessage(string msgType, string arg, eInfoType type = eInfoType.normal)`**
  发送消息（字符串版本）。创建一个 `MsgData` 实例，并设置 `msg1` 和 `type` 属性。
- **`SendMessage(string msgType, string msg1, object data, eInfoType type = eInfoType.normal)`**
  发送消息（字符串和对象参数版本）。创建一个 `MsgData` 实例，并设置 `msg1`、`data` 和 `type` 属性。
- **`SendMessage(string msgType, object data, eInfoType type = eInfoType.normal)`**
  发送消息（通用对象版本）。创建一个 `MsgData` 实例，并设置 `data` 和 `type` 属性。
- **`SendMessage(string msgType, float float1, eInfoType type = eInfoType.normal)`**
  发送消息（浮点数版本）。创建一个 `MsgData` 实例，并设置 `float1` 和 `type` 属性。
- **`SendMessage(string msgtype, int arg1, float float1, eInfoType type = eInfoType.normal)`**
  发送消息（整型和浮点数参数版本）。创建一个 `MsgData` 实例，并设置 `arg1`、`float1` 和 `type` 属性。
- **`SendMessage(string msgType, MsgData msg)`**
  发送消息（使用 `MsgData` 实例）。检查消息类型是否为空，如果不为空，则从消息表中找到对应的处理委托并执行，同时从对象池中获取消息数据并释放。
- **`AddObserver(string msgType, MsgHandler handler)`**
  注册消息监听器。如果消息类型不存在，则创建新的列表，并添加处理委托。
- **`RemoveObserver(string msgType, MsgHandler handler)`**
  注销消息监听器。如果消息类型存在，则从对应的列表中移除处理委托。

#### 消息处理委托

- **`MsgHandler`**
  消息处理委托类型，用于处理消息内容。定义为 `delegate void MsgHandler(MsgData msg);`。
- **`OldMsgHandler`**
  旧版消息处理委托类型，用于处理消息内容。定义为 `delegate void OldMsgHandler(params object[] msg);`。

#### 消息内容

- **`MsgData`**
  消息数据类，包含各种消息参数（如整型、布尔值、字符串、对象、浮点数等），并提供对象池管理。包括以下属性：
  - `type`：消息类型（`eInfoType`）。
  - `arg1`, `arg2`, `arg3`：整型参数。
  - `boolArg`, `boolArg2`：布尔值参数。
  - `msg1`, `msg2`：字符串参数。
  - `float1`, `float2`：浮点数参数。
  - `data`：通用对象参数。
  - `vec3`：`Vector3` 参数。
- **`Obtain()`**
  获取一个 `MsgData` 实例。如果消息池中有可用实例，则从池中获取；否则创建新的实例。
- **`Release()`**
  释放 `MsgData` 实例，将其返回消息池，以便重复利用。

#### 枚举

- `eInfoType`

  消息类型枚举，包含以下值：

  - `normal`：普通消息。
  - `error`：错误消息。
  - `good`：良好消息。
  - `bigError`：严重错误消息。



## API 方法

#### `void SendMessage(string msgType)`

- **描述**: 发送消息，使用默认的 `MsgData` 实例。
- **参数**:
  - `msgType` (`string`): 消息类型，用于指定消息的目的地。
- **返回值**: 无

#### `void SendMessage(string msgType, bool boolean, int arg1)`

- **描述**: 发送消息，包含布尔值和一个整型参数。
- **参数**:
  - `msgType` (`string`): 消息类型，用于指定消息的目的地。
  - `boolean` (`bool`): 布尔值参数，用于消息的附加数据。
  - `arg1` (`int`): 整型参数，用于消息的附加数据。
- **返回值**: 无

#### `void SendMessage(string msgType, int arg, eInfoType type = eInfoType.normal)`

- **描述**: 发送消息，包含一个整型参数和消息类型。
- **参数**:
  - `msgType` (`string`): 消息类型，用于指定消息的目的地。
  - `arg` (`int`): 整型参数，用于消息的附加数据。
  - `type` (`eInfoType`): 消息的附加信息类型，默认为 `eInfoType.normal`。
- **返回值**: 无

### 示例：

```c#
private void Start()
{
    // 预加载10个对象
    objectPool.InitPerfab(prefab.GetPrefabAssetPath(), 10);

    // 生成一个对象
    GameObject obj = objectPool.Spawn(prefab.GetPrefabAssetPath());
    obj.transform.position = new Vector3(0, 1, 0);

    // 回收对象
    bool success = objectPool.Recycle(obj);
    Debug.Log("Recycle success: " + success);
}
```




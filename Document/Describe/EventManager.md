

# **事件管理- EventManager**



*`UIManager` 类是一个单例管理器，负责处理游戏中的 UI 界面。它提供了 UI 界面的打开、关闭、销毁以及管理功能。*



## 类结构

### 字段

- **`completedEvents`** (`List<int>`): 存储已经完成的事件 ID 列表。
- **`eventDatas`** (`List<BaseGameEvent>`): 存储当前活动的事件数据列表。

### 方法

**`void TriggerEvent(string eventName)`**

- **描述**: 触发指定名称的事件。如果事件存在，将其触发。
- 参数:
  - `eventName` (`string`): 事件的名称。
- **返回值**: 无

**`void TriggerEvent<T>(int id, string[] args)`**

- **描述**: 触发指定类型的事件，并传递参数。事件类型由泛型 `T` 决定。
- 参数:
  - `id` (`int`): 事件的 ID。
  - `args` (`string[]`): 传递给事件的参数。
- **返回值**: 无

**`bool TriggerEvent(int id, string eventName, string[] args, bool isLoad)`**

- **描述**: 触发指定名称的事件，并传递参数。如果事件已经被触发过一次，则不会重复触发。
- 参数:
  - `id` (`int`): 事件的 ID。
  - `eventName` (`string`): 事件的名称。
  - `args` (`string[]`): 传递给事件的参数。
  - `isLoad` (`bool`): 标志是否加载事件。
- **返回值**: `bool`，如果事件成功触发则返回 `true`，否则返回 `false`。

**`void Update()`**

- **描述**: 更新当前活动的事件数据。
- **返回值**: 无

**`bool GetCompleteEvent(int id)`**

- **描述**: 检查指定 ID 的事件是否已经完成。
- 参数:
  - `id` (`int`): 事件的 ID。
- **返回值**: `bool`，如果事件已经完成则返回 `true`，否则返回 `false`。

**`BaseGameEvent InstanceGameEvent(string eventName)`**

- **描述**: 通过反射实例化一个事件类。
- 参数:
  - `eventName` (`string`): 事件的名称。
- **返回值**: `BaseGameEvent`，返回实例化的事件对象。

**`void Recycle()`**

- **描述**: 清除已经完成的事件列表。
- **返回值**: 无

## API 方法

### `void TriggerEvent(string eventName)`

- **描述**: 触发指定名称的事件。如果事件存在，则会执行该事件。
- 参数:
  - `eventName` (`string`): 事件的名称。
- **返回值**: 无

### `void TriggerEvent<T>(int id, string[] args)`

- **描述**: 触发指定类型的事件，并传递参数。事件类型由泛型 `T` 决定。
- 参数:
  - `id` (`int`): 事件的 ID。
  - `args` (`string[]`): 传递给事件的参数。
- **返回值**: 无

### `bool TriggerEvent(int id, string eventName, string[] args, bool isLoad)`

- **描述**: 触发指定名称的事件，并传递参数。如果事件已经被触发过一次，则不会重复触发。
- 参数:
  - `id` (`int`): 事件的 ID。
  - `eventName` (`string`): 事件的名称。
  - `args` (`string[]`): 传递给事件的参数。
  - `isLoad` (`bool`): 标志是否加载事件。
- **返回值**: `bool`，如果事件成功触发则返回 `true`，否则返回 `false`。

### `void Update()`

- **描述**: 更新当前活动的事件数据。
- **返回值**: 无

### `bool GetCompleteEvent(int id)`

- **描述**: 检查指定 ID 的事件是否已经完成。
- 参数:
  - `id` (`int`): 事件的 ID。
- **返回值**: `bool`，如果事件已经完成则返回 `true`，否则返回 `false`。

### `BaseGameEvent InstanceGameEvent(string eventName)`

- **描述**: 通过反射实例化一个事件类。
- 参数:
  - `eventName` (`string`): 事件的名称。
- **返回值**: `BaseGameEvent`，返回实例化的事件对象。

### `void Recycle()`

- **描述**: 清除已经完成的事件列表。
- **返回值**: 无

### 示例：

```c#
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        // 触发名为 "PlayerScored" 的事件
        EventManager.Instance.TriggerEvent("PlayerScored");

        // 触发具有特定 ID 和参数的事件
        EventManager.Instance.TriggerEvent<PlayerScoredEvent>(1, new string[] { "100" });

        // 检查事件是否已经完成
        bool isCompleted = EventManager.Instance.GetCompleteEvent(1);
        Debug.Log("Event completed: " + isCompleted);
    }
}
```




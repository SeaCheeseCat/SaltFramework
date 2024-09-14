

# **按键管理- InputKeyManager**



*`InputKeyManager` 类是一个单例管理器，负责处理游戏中的按键输入。它提供了管理待执行按键操作、缓存 Esc 按键状态和处理特定按键的功能*



## 类结构

### 成员变量

- **`public List<int> escCache`**
  - **描述**: 存储 Esc 按键的优先级缓存。用于缓存是否打开 SetUI。
- **`public bool browseMode`**
  - **描述**: 浏览模式标志。用于标识当前是否处于浏览模式。
- **`public List<KeyCode> tobeKeyRun`**
  - **描述**: 待执行的按键集合。存储待执行的按键操作。

### 方法

- **`void AddTobeKey(KeyCode code)`**
  - **描述**: 添加一个待执行操作的按键。
  - 参数:
    - `code` (`KeyCode`): 待添加的按键。
- **`void RemoveTobeKey(KeyCode code)`**
  - **描述**: 移除一个待执行操作的按键。
  - 参数:
    - `code` (`KeyCode`): 要移除的按键。
- **`void AddEscCache(int id)`**
  - **描述**: 添加一个 Esc 缓存。
  - 参数:
    - `id` (`int`): 要添加的 Esc 缓存的 ID。
- **`void RemoveEscCache(int id)`**
  - **描述**: 移除一个 Esc 缓存。
  - 参数:
    - `id` (`int`): 要移除的 Esc 缓存的 ID。
- **`bool IsEscCache(int id)`**
  - **描述**: 检查指定的 ID 是否在 Esc 缓存中。
  - 参数:
    - `id` (`int`): 要检查的 ID。
  - **返回值**: 如果 ID 在 Esc 缓存中，返回 `true`；否则返回 `false`。
- **`int GetEscCacheCount()`**
  - **描述**: 获取 Esc 缓存的数量。
  - **返回值**: 返回 Esc 缓存的数量。
- **`void OnAngKeyDown()`**
  - **描述**: 处理按键按下事件的回调（待实现）。
- **`void OnConditionsKeyDown()`**
  - **描述**: 处理存在条件的按键按下事件的回调。
- **`void OnGameInitKeyDown()`**
  - **描述**: 处理关卡中的按键按下事件的回调（例如 KeyCode.M）。
- **`void OnGameLevelKeyDown()`**
  - **描述**: 处理关卡中的按键按下事件的回调（例如 KeyCode.Escape、KeyCode.Space）。
- **`void OnGameLevelLongKeyDown()`**
  - **描述**: 处理关卡中的长按操作的回调（待实现）。
- **`bool IsCanDoRun(KeyCode code)`**
  - **描述**: 从待执行操作集中检查是否存在指定的按键。
  - 参数:
    - `code` (`KeyCode`): 要检查的按键。
  - **返回值**: 如果按键在待执行操作集中，返回 `true`；否则返回 `false`

## API 方法

### `void AddTobeKey(KeyCode code)`

- **描述**: 将一个待执行的按键操作添加到待操作集中。
- 参数:
  - `code` (`KeyCode`): 要添加的按键代码。
- **返回值**: 无

### `void RemoveTobeKey(KeyCode code)`

- **描述**: 从待操作集中移除一个按键操作。
- 参数:
  - `code` (`KeyCode`): 要移除的按键代码。
- **返回值**: 无

### `void AddEscCache(int id)`

- **描述**: 将一个 Esc 缓存添加到缓存列表中。
- 参数:
  - `id` (`int`): 要添加的 Esc 缓存 ID。
- **返回值**: 无

### `void RemoveEscCache(int id)`

- **描述**: 从 Esc 缓存中移除一个缓存 ID。
- 参数:
  - `id` (`int`): 要移除的 Esc 缓存 ID。
- **返回值**: 无

### `bool IsEscCache(int id)`

- **描述**: 检查指定的 Esc 缓存 ID 是否在缓存列表中。
- 参数:
  - `id` (`int`): 要检查的 Esc 缓存 ID。
- **返回值**: `bool`，如果缓存列表中包含指定的 ID，则返回 `true`，否则返回 `false`。

### `int GetEscCacheCount()`

- **描述**: 获取当前 Esc 缓存列表中的缓存数量。
- **返回值**: `int`，返回缓存列表中的数量。

### `void OnAngKeyDown()`

- **描述**: 处理特殊的按键按下事件（具体实现未提供）。
- **返回值**: 无

### `void OnConditionsKeyDown()`

- **描述**: 处理条件按键按下事件（具体实现未提供）。
- **返回值**: 无

### `void OnGameInitKeyDown()`

- **描述**: 处理游戏初始化时的按键按下事件。如果按下 `KeyCode.M`，则执行相应操作（具体实现未提供）。
- **返回值**: 无

### `void OnGameLevelKeyDown()`

- **描述**: 处理游戏关卡中的按键按下事件。如果按下 `KeyCode.Escape` 且 Esc 缓存列表为空，则执行相应操作。如果 `browseMode` 为 `true`，则执行浏览模式下的操作。如果按下 `KeyCode.Space`，则播放声音效果（ID 为 1004）。
- **返回值**: 无

### `void OnGameLevelLongKeyDown()`

- **描述**: 处理游戏关卡中的长按操作（具体实现未提供）。
- **返回值**: 无

### `bool IsCanDoRun(KeyCode code)`

- **描述**: 检查待操作集中是否包含指定的按键操作。

- 参数

  :

  - `code` (`KeyCode`): 要检查的按键代码。

- **返回值**: `bool`，如果待操作集中包含指定的按键操作，则返回 `true`，否则返回 `false`。

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




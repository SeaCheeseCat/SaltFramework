

# **数据管理- GameBaseData**



*``GameBaseData` 类是一个静态类，用于管理和存储游戏中的基础数据，包括章节、关卡、关卡类型、事件以及语言等信息。它提供了一些静态属性，方便全局访问和修改这些基础数据。*



## 类结构

### 成员变量

#### `Chapter`

- **描述**: 当前的章节编号。
- **类型**: `int`
- **访问权限**: 读/写

#### `Level`

- **描述**: 当前的关卡编号。
- **类型**: `int`
- **访问权限**: 读/写

#### `levelType`

- **描述**: 当前关卡的类型，枚举类型 `LevelType`。
- **类型**: `LevelType`
- **访问权限**: 读/写

#### `Event`

- **描述**: 传递的事件，使用 `Action` 类型。
- **类型**: `Action`
- **访问权限**: 读/写

#### `EventName`

- **描述**: 事件的名称，表示要传递的事件名。
- **类型**: `string`
- **访问权限**: 读/写

#### `language`

- **描述**: 当前使用的语言。
- **类型**: `Language` (枚举)
- **访问权限**: 读/写

### 枚举

#### `LevelType`

- **描述**: 表示关卡类型的枚举。

- 枚举值

  :

  - `COMMON`: 常用的关卡类型。
  - `HAVESUBTITLE`: 包含字幕的关卡类型。

### 示例：

```c#
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // 设置当前章节和关卡
        GameBaseData.Chapter = 1;
        GameBaseData.Level = 2;

        // 设置关卡类型
        GameBaseData.levelType = LevelType.HAVESUBTITLE;

        // 设置事件和事件名称
        GameBaseData.Event = OnGameEvent;
        GameBaseData.EventName = "LevelComplete";

        // 设置当前语言
        GameBaseData.language = Language.English;

        // 执行事件
        GameBaseData.Event?.Invoke();
    }

    void OnGameEvent()
    {
        Debug.Log("Event triggered: " + GameBaseData.EventName);
    }
}
```




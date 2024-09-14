

# **Log模块- DebugEX**



*`DebugEX` 类是一个工具类，提供了丰富的日志记录功能，适用于 Unity 编辑器中。它支持彩色日志、成功消息、错误消息、警告消息以及格式化的日志输出。*

## 类结构

### 成员变量

- 无

### 方法

- **`void Log(params object[] ob)`**

  - **描述**: 输出普通日志，仅在 Unity 编辑器中可见。

  - 参数

    :

    - `ob` (`object[]`): 要记录的日志内容。

  - **返回值**: 无

- **`void LogFrameworkMsg(params object[] ob)`**

  - **描述**: 输出带有“Framework ▷▶”标题的框架消息，并以指定颜色显示。
  - 参数:
    - `ob` (`object[]`): 要记录的日志内容。
  - **返回值**: 无

- **`void LogColor(Color color, string title, params object[] ob)`**

  - **描述**: 输出带有标题和指定颜色的日志消息。
  - 参数:
    - `color` (`Color`): 日志标题的颜色。
    - `title` (`string`): 日志标题。
    - `ob` (`object[]`): 要记录的日志内容。
  - **返回值**: 无

- **`void Log(object message, Color color)`**

  - **描述**: 输出指定颜色的单条日志消息。
  - 参数:
    - `message` (`object`): 要记录的日志消息。
    - `color` (`Color`): 日志消息的颜色。
  - **返回值**: 无

- **`void LogSuccess(params object[] ob)`**

  - **描述**: 输出带有绿色对勾的成功日志消息。
  - 参数:
    - `ob` (`object[]`): 要记录的日志内容。
  - **返回值**: 无

- **`void LogError(params object[] ob)`**

  - **描述**: 输出错误日志消息。
  - 参数:
    - `ob` (`object[]`): 要记录的日志内容。
  - **返回值**: 无

- **`void LogWarrning(params object[] ob)`**

  - **描述**: 输出警告日志消息。
  - 参数:
    - `ob` (`object[]`): 要记录的日志内容。
  - **返回值**: 无

- **`string GetLog(params object[] ob)`**

  - **描述**: 格式化日志内容，将不同类型的日志内容转换为字符串。
  - 参数:
    - `ob` (`object[]`): 要格式化的日志内容。
  - **返回值**: `string`，格式化后的日志字符串。

## API 方法

### `void Log(params object[] ob)`

- **描述**: 输出普通日志，仅在 Unity 编辑器中可见。
- 参数:
  - `ob` (`object[]`): 要记录的日志内容。
- **返回值**: 无

### `void LogFrameworkMsg(params object[] ob)`

- **描述**: 输出带有“Framework ▷▶”标题的框架消息，并以指定颜色显示。
- 参数:
  - `ob` (`object[]`): 要记录的日志内容。
- **返回值**: 无

### `void LogColor(Color color, string title, params object[] ob)`

- **描述**: 输出带有标题和指定颜色的日志消息。
- 参数:
  - `color` (`Color`): 日志标题的颜色。
  - `title` (`string`): 日志标题。
  - `ob` (`object[]`): 要记录的日志内容。
- **返回值**: 无

### `void Log(object message, Color color)`

- **描述**: 输出指定颜色的单条日志消息。
- 参数:
  - `message` (`object`): 要记录的日志消息。
  - `color` (`Color`): 日志消息的颜色。
- **返回值**: 无

### `void LogSuccess(params object[] ob)`

- **描述**: 输出带有绿色对勾的成功日志消息。
- 参数:
  - `ob` (`object[]`): 要记录的日志内容。
- **返回值**: 无

### `void LogError(params object[] ob)`

- **描述**: 输出错误日志消息。
- 参数:
  - `ob` (`object[]`): 要记录的日志内容。
- **返回值**: 无

### `void LogWarrning(params object[] ob)`

- **描述**: 输出警告日志消息。
- 参数:
  - `ob` (`object[]`): 要记录的日志内容。
- **返回值**: 无

### `string GetLog(params object[] ob)`

- **描述**: 格式化日志内容，将不同类型的日志内容转换为字符串。
- 参数:
  - `ob` (`object[]`): 要格式化的日志内容。
- **返回值**: `string`，格式化后的日志字符串。

### 示例：

```
c#复制代码using UnityEngine;

public class TestLogger : MonoBehaviour
{
    void Start()
    {
        // 输出普通日志
        DebugEX.Log("This is a normal log.");

        // 输出带有颜色和标题的框架消息
        DebugEX.LogFrameworkMsg("Initializing Framework...");

        // 输出红色错误日志
        DebugEX.LogError("An error occurred:", "Error Code: 123");

        // 输出成功消息
        DebugEX.LogSuccess("Operation successful!");

        // 输出警告消息
        DebugEX.LogWarrning("Warning: Check your configuration.");
    }
}
```


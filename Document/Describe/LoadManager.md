

# **场景加载模块- LoadManager**



*`LoadManager` 类是一个场景加载管理器，负责场景的同步和异步加载操作。它支持显示加载进度和执行加载完成后的操作。*

## 类结构

### 成员变量

- 无

### 方法

- **`void LoadScene(string scenename)`**
  - **描述**: 加载指定名称的场景（同步加载）。
  - 参数:
    - `scenename` (`string`): 目标场景的名称。
  - **返回值**: 无
- **`void LoadSceneAsync(string scene, LoadProgressCallback loadcallback = null, Action action = null)`**
  - **描述**: 异步加载指定名称的场景，并在加载过程中显示进度和执行完成后的操作。
  - 参数:
    - `scene` (`string`): 目标场景的名称。
    - `loadcallback` (`LoadProgressCallback`): 加载进度的回调函数。
    - `action` (`Action`): 加载完成后执行的操作。
  - **返回值**: 无
- **`void LoadAsyncScene(string sceneName, LoadProgressCallback loadcallback, Action<AsyncOperation> loadCompleteAction)`**
  - **描述**: 异步加载指定名称的场景，返回 `AsyncOperation` 对象以便进行更细致的控制。
  - 参数:
    - `sceneName` (`string`): 目标场景的名称。
    - `loadcallback` (`LoadProgressCallback`): 加载进度的回调函数。
    - `loadCompleteAction` (`Action<AsyncOperation>`): 加载完成后执行的操作，并传入 `AsyncOperation` 对象。
  - **返回值**: 无
- **`IEnumerator LoadSceneAsyncCoroutine(string sceneName, LoadProgressCallback loadProgressCallback, Action loadCompleteAction)`**
  - **描述**: 异步加载指定名称的场景，显示加载进度，并在加载完成后执行操作。
  - 参数:
    - `sceneName` (`string`): 目标场景的名称。
    - `loadProgressCallback` (`LoadProgressCallback`): 加载进度的回调函数。
    - `loadCompleteAction` (`Action`): 加载完成后执行的操作。
  - **返回值**: `IEnumerator`，用于协程
- **`IEnumerator LoadSceneAsyncNoInCoroutine(string sceneName, LoadProgressCallback loadProgressCallback, Action<AsyncOperation> loadCompleteAction)`**
  - **描述**: 异步加载指定名称的场景，返回 `AsyncOperation` 对象并显示加载进度，不自动进入场景。
  - 参数:
    - `sceneName` (`string`): 目标场景的名称。
    - `loadProgressCallback` (`LoadProgressCallback`): 加载进度的回调函数。
    - `loadCompleteAction` (`Action<AsyncOperation>`): 加载完成后执行的操作，并传入 `AsyncOperation` 对象。
  - **返回值**: `IEnumerator`，用于协程

## API 方法

### `void LoadScene(string scenename)`

- **描述**: 加载指定名称的场景（同步加载）。
- 参数:
  - `scenename` (`string`): 目标场景的名称。
- **返回值**: 无

### `void LoadSceneAsync(string scene, LoadProgressCallback loadcallback = null, Action action = null)`

- **描述**: 异步加载指定名称的场景，并在加载过程中显示进度和执行完成后的操作。
- 参数:
  - `scene` (`string`): 目标场景的名称。
  - `loadcallback` (`LoadProgressCallback`): 加载进度的回调函数。
  - `action` (`Action`): 加载完成后执行的操作。
- **返回值**: 无

### `void LoadAsyncScene(string sceneName, LoadProgressCallback loadcallback, Action<AsyncOperation> loadCompleteAction)`

- **描述**: 异步加载指定名称的场景，返回 `AsyncOperation` 对象以便进行更细致的控制。
- 参数:
  - `sceneName` (`string`): 目标场景的名称。
  - `loadcallback` (`LoadProgressCallback`): 加载进度的回调函数。
  - `loadCompleteAction` (`Action<AsyncOperation>`): 加载完成后执行的操作，并传入 `AsyncOperation` 对象。
- **返回值**: 无

### `IEnumerator LoadSceneAsyncCoroutine(string sceneName, LoadProgressCallback loadProgressCallback, Action loadCompleteAction)`

- **描述**: 异步加载指定名称的场景，显示加载进度，并在加载完成后执行操作。
- 参数:
  - `sceneName` (`string`): 目标场景的名称。
  - `loadProgressCallback` (`LoadProgressCallback`): 加载进度的回调函数。
  - `loadCompleteAction` (`Action`): 加载完成后执行的操作。
- **返回值**: `IEnumerator`，用于协程

### `IEnumerator LoadSceneAsyncNoInCoroutine(string sceneName, LoadProgressCallback loadProgressCallback, Action<AsyncOperation> loadCompleteAction)`

- **描述**: 异步加载指定名称的场景，返回 `AsyncOperation` 对象并显示加载进度，不自动进入场景。
- 参数:
  - `sceneName` (`string`): 目标场景的名称。
  - `loadProgressCallback` (`LoadProgressCallback`): 加载进度的回调函数。
  - `loadCompleteAction` (`Action<AsyncOperation>`): 加载完成后执行的操作，并传入 `AsyncOperation` 对象。
- **返回值**: `IEnumerator`，用于协程

### 示例：

```
using UnityEngine;

public class TestLoadManager : MonoBehaviour
{
    void Start()
    {
        // 同步加载场景
        LoadManager.Instance.LoadScene("MainMenu");

        // 异步加载场景并显示进度
        LoadManager.Instance.LoadSceneAsync("GameLevel1", 
            progress => DebugEX.Log($"Loading Progress: {progress * 100}%"), 
            () => DebugEX.Log("Scene Loaded Successfully"));

        // 异步加载场景，不自动进入场景，使用 AsyncOperation
        LoadManager.Instance.LoadAsyncScene("GameLevel2",
            progress => DebugEX.Log($"Loading Progress: {progress * 100}%"),
            op => DebugEX.Log("Scene Load Complete. Ready to activate."));
    }
}
```



# **UI模块- UIManager**



*`UIManager` 类是一个单例管理器，负责处理游戏中的 UI 界面。它提供了 UI 界面的打开、关闭、销毁以及管理功能。*



## 类结构

### 方法

**`void Init()`**

- **描述**: 初始化 UI 框架，通过自动搜索 Canvas 对象进行初始化。

**`void Init(Transform Canvas, Transform parentCanvas)`**

- **描述**: 使用指定的 Canvas 和父级 Canvas 对象进行初始化。
- **参数**:
  - `Canvas` (`Transform`): 当前的 Canvas 对象。
  - `parentCanvas` (`Transform`): 父级 Canvas 对象。

**`UIBase OpenUI(string UiName, params object[] dialogArgs)`**

- **描述**: 打开指定名称的 UI，如果 UI 不存在则加载并初始化。
- **参数**:
  - `UiName` (`string`): UI 的名称。
  - `dialogArgs` (`object[]`): 传递给 UI 的参数。
- **返回值**: `UIBase` 对象，表示打开的 UI 实例。

**`T OpenUI<T>(params object[] dialogArgs) where T : UIBase`**

- **描述**: 打开指定类型的 UI，如果 UI 不存在则加载并初始化。
- **参数**:
  - `dialogArgs` (`object[]`): 传递给 UI 的参数。
- **返回值**: 指定类型的 `UIBase` 对象，表示打开的 UI 实例。

**`void RegisterUI(UIBase Uibase, params object[] dialogArgs)`**

- **描述**: 注册场景中已经存在的 UI 实例。
- **参数**:
  - `Uibase` (`UIBase`): 已经存在的 UI 实例。
  - `dialogArgs` (`object[]`): 传递给 UI 的参数。

**`void CloseUI<T>()`**

- **描述**: 关闭指定类型的 UI。
- **返回值**: 无

**`UIBase GetUI(string UiName)`**

- **描述**: 获取指定名称的 UI 实例。
- **参数**:
  - `UiName` (`string`): UI 的名称。
- **返回值**: `UIBase` 对象，表示获取的 UI 实例。

**`T GetUI<T>() where T : UIBase`**

- **描述**: 获取指定类型的 UI 实例。
- **返回值**: 指定类型的 `UIBase` 对象，表示获取的 UI 实例。

**`void CloseUI(string UiName)`**

- **描述**: 关闭指定名称的 UI。
- **参数**:
  - `UiName` (`string`): UI 的名称。
- **返回值**: 无

**`void CloseAllUI()`**

- **描述**: 关闭所有管理的 UI 实例。
- **返回值**: 无

**`void DestroyAllUI()`**

- **描述**: 销毁所有管理的 UI 实例。
- **返回值**: 无

**`void DestroyUI(string UiName)`**

- **描述**: 销毁指定名称的 UI 实例。
- **参数**:
  - `UiName` (`string`): UI 的名称。
- **返回值**: 无

**`GameObject LoadUiWithRes(string name)`**

- **描述**: 从资源中加载 UI 资源。
- **参数**:
  - `name` (`string`): UI 资源的名称。
- **返回值**: `GameObject`，表示加载的 UI 对象。

## API 方法

#### `void Init()`

- **描述**: 初始化 UI 框架，通过自动搜索 Canvas 对象进行初始化。
- **返回值**: 无

#### `void Init(Transform Canvas, Transform parentCanvas)`

- **描述**: 使用指定的 Canvas 和父级 Canvas 对象进行初始化。
- 参数:
  - `Canvas` (`Transform`): 当前的 Canvas 对象。
  - `parentCanvas` (`Transform`): 父级 Canvas 对象。
- **返回值**: 无

#### `UIBase OpenUI(string UiName, params object[] dialogArgs)`

- **描述**: 打开指定名称的 UI，如果 UI 不存在则加载并初始化。
- 参数:
  - `UiName` (`string`): UI 的名称。
  - `dialogArgs` (`object[]`): 传递给 UI 的参数。
- **返回值**: `UIBase` 对象，表示打开的 UI 实例。

#### `T OpenUI<T>(params object[] dialogArgs) where T : UIBase`

- **描述**: 打开指定类型的 UI，如果 UI 不存在则加载并初始化。
- 参数:
  - `dialogArgs` (`object[]`): 传递给 UI 的参数。
- **返回值**: 指定类型的 `UIBase` 对象，表示打开的 UI 实例。

#### `void RegisterUI(UIBase Uibase, params object[] dialogArgs)`

- **描述**: 注册场景中已经存在的 UI 实例。
- 参数:
  - `Uibase` (`UIBase`): 已经存在的 UI 实例。
  - `dialogArgs` (`object[]`): 传递给 UI 的参数。
- **返回值**: 无

#### `void CloseUI<T>()`

- **描述**: 关闭指定类型的 UI。
- **返回值**: 无

#### `UIBase GetUI(string UiName)`

- **描述**: 获取指定名称的 UI 实例。
- 参数:
  - `UiName` (`string`): UI 的名称。
- **返回值**: `UIBase` 对象，表示获取的 UI 实例。

#### `T GetUI<T>() where T : UIBase`

- **描述**: 获取指定类型的 UI 实例。
- **返回值**: 指定类型的 `UIBase` 对象，表示获取的 UI 实例。

#### `void CloseUI(string UiName)`

- **描述**: 关闭指定名称的 UI。
- 参数:
  - `UiName` (`string`): UI 的名称。
- **返回值**: 无

### 示例：

```c#
 public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        // 初始化 UI 管理器
        UIManager.Instance.Init();

        // 打开名为 "MainMenu" 的 UI
        UIBase mainMenu = UIManager.Instance.OpenUI("MainMenu");

        // 注册已经存在于场景中的 UI 实例
        UIBase settingsUI = GameObject.Find("SettingsUI").GetComponent<UIBase>();
        UIManager.Instance.RegisterUI(settingsUI);

        // 获取 UI 实例并进行操作
        var inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.Open();
        }

        // 关闭指定名称的 UI
        UIManager.Instance.CloseUI("MainMenu");

        // 关闭所有 UI
        UIManager.Instance.CloseAllUI();

        // 销毁指定名称的 UI
        UIManager.Instance.DestroyUI("SettingsUI");

        // 销毁所有 UI
        UIManager.Instance.DestroyAllUI();

        // 打开带有自定义按钮文本的弹出框
        UIManager.Instance.OpenTipUI(
            title: "Warning",
            content: "Are you sure you want to delete this item?",
            yesContent: "Yes",
            cancelContent: "No",
            yesAction: () => Debug.Log("Item deleted"),
            cancelAction: () => Debug.Log("Deletion canceled")
        );

        // 打开简单的弹出框
        UIManager.Instance.OpenTipUI(
            title: "Info",
            content: "This is a simple info message.",
            yesAction: () => Debug.Log("Info acknowledged")
        );

        // 打开带有参数的 UI
        var dialogueUI = UIManager.Instance.OpenUI<DialogueUI>("Hello", "Welcome to the game!");

        // 获取并操作 UI 实例
        var gameUI = UIManager.Instance.GetUI<GameUI>();
        if (gameUI != null)
        {
            gameUI.UpdateScore(100);
        }
    }
}
```






# **FSM有限状态机 - AIStateBase**



*```LanguageManager` 负责管理和处理游戏中的多语言文本和数据。*



## 类结构

#### 成员变量

- **`List<LanguageText> languageTexts`**: 存储各种语言文本的列表。
- **`List<LanguageItem> languageDatas`**: 存储全部语言数据的列表。

#### 方法

###### `IEnumerator Init(MonoBehaviour obj)`

- **描述**: 初始化 `LanguageManager`，加载语言配置数据并通知语言加载完成。
- 参数:
  - `obj` (`MonoBehaviour`): 需要初始化的对象。
- **返回值**: 返回一个 `IEnumerator`，用于协程的初始化过程。

###### `LanguageItem AddLanguagerText(LanguageText val)`

- **描述**: 添加一个语言文本到 `languageTexts` 列表。如果 `languageTexts` 中不存在该文本，则添加它。
- 参数:
  - `val` (`LanguageText`): 需要添加的语言文本。
- **返回值**: 返回添加的 `LanguageItem` 对象。如果没有找到对应的语言数据，返回 `null`。

###### `void UpdateLanguage(Language language)`

- **描述**: 更新所有语言文本以反映新的语言设置。
- 参数:
  - `language` (`Language`): 当前选择的语言。
- **返回值**: 无

###### `Dictionary<Language, string> GetConfigLanguageString(string val)`

- **描述**: 处理配置表中的多语言设置，提取并返回语言和对应内容的字典。
- 参数:
  - `val` (`string`): 包含多语言设置的字符串。
- **返回值**: 返回包含语言和内容的字典。字典的键为 `Language`，值为对应的文本内容。如果配置字符串格式不正确，返回 `null`。

###### `Dictionary<Language, string[]> GetConfigLanguageArray(string val)`

- **描述**: 处理配置表中的多语言设置，提取并返回语言和对应内容数组的字典。
- 参数:
  - `val` (`string`): 包含多语言设置的字符串。
- **返回值**: 返回包含语言和内容数组的字典。字典的键为 `Language`，值为对应的内容数组。如果配置字符串格式不正确，返回 `null`。



##### 私有静态方法

###### `static Dictionary<Language, string> ExtractContents(string input)`

- **描述**: 从输入字符串中提取语言和对应的内容。

- 参数:

  - `input` (`string`): 包含语言标签和内容的字符串。

- **返回值**: 返回一个包含语言和内容的字典。字典的键为 `Language`，值为对应的内容。如果没有匹配项，返回空字典。



## API 方法

#### `void SetUp(UnitAI ai)`

- **描述**: 初始化状态机，设置与当前状态关联的 AI 单元。
- **参数**:
  - `ai` (`UnitAI`): 需要关联的 AI 单元。
- **返回值**: 无

#### `void OnEnter()`

- **描述**: 状态机进入该状态时调用。子类可以重写此方法以实现进入状态时的逻辑。
- **参数**: 无
- **返回值**: 无

#### `void Update()`

- **描述**: 状态机在每帧更新时调用。子类可以重写此方法以实现状态更新的逻辑。
- **参数**: 无
- **返回值**: 无

#### `void OnExit()`

- **描述**: 状态机退出该状态时调用。子类可以重写此方法以实现退出状态时的逻辑。
- **参数**: 无
- **返回值**: 无

### 示例：

```c#
public class PatrolState : AIStateBase
{
    public override void OnEnter()
    {
        // 开始巡逻
        Debug.Log("开始巡逻");
    }

    public override void Update()
    {
        // 执行巡逻逻辑
        if (AI.IsEnemyInSight())
        {
            AI.ChangeState(new AttackState());
        }
    }

    public override void OnExit()
    {
        // 结束巡逻
        Debug.Log("结束巡逻");
    }
}
```




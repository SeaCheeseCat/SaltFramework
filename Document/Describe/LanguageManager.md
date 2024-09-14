

# **多语言模块 - LanguageManager **



*`AudioManager` 类负责统一管理游戏中的音效，包括背景音乐和音效播放。它提供了初始化音效设置、播放音效、设置音量、以及处理音效渐变等功能。*



## 类结构

### AudioManager

`AudioManager` 继承自 `Manager<AudioManager>`



## 成员变量

- **`AudioSource[] MusicAudio`**: 背景音乐组件的数组。
- **`AudioSource EfficAudio`**: 音效组件。
- **`const string MainBGM`**: 主界面的背景音乐名称。
- **`private const string DEFALUTCLICK`**: 默认点击音效名称。
- **`private const string DEFALUTFAIL`**: 默认失败音效名称（目前未使用）。
- **`private const string MUSICVOLUMEKEY`**: 背景音乐音量的 PlayerPrefs 键。
- **`private const string EFFICVOLUEKEY`**: 音效音量的 PlayerPrefs 键。
- **`private const string PATH`**: 音效资源的统一路径。
- **`private const string MUSICPATH`**: 背景音乐资源的统一路径。
- **`private float MusicMax`**: 背景音乐最大音量。
- **`private float EfficMax`**: 音效最大音量。
- **`private Dictionary<int, string> PageMusicConfig`**: 章节背景音乐配置表，存储每个章节对应的背景音乐名称。

## 方法

### `void InitCompentConfig()`

**描述**: 初始化音效组件，包括加载背景音乐和音效组件，并设置音量。设置完毕后，确保对象在场景切换时不会被销毁，并注册音频配置更改事件。

### `void Init()`

**描述**: 初始化音效设置，包括检查并设置背景音乐和音效的默认音量值。

### `void PlayMusic(string[] names, bool isLoop, bool grad)`

**描述**: 播放指定的背景音乐。如果 `grad` 为 `true`，则执行渐变效果。

**参数**:

- `names` (`string[]`): 背景音乐的名称数组。
- `isLoop` (`bool`): 是否循环播放。
- `grad` (`bool`): 是否使用渐变效果。

### `void PlayMainMusic()`

**描述**: 播放主界面的背景音乐。

### `void PlayPageMusic(int page)`

**描述**: 播放章节背景音乐。根据章节编号从配置中获取对应的背景音乐名称。

**参数**:

- `page` (`int`): 章节编号。

### `void PlayDefaultClickAudio()`

**描述**: 播放默认的点击音效。

### `float GetMusicVolum()`

**描述**: 获取当前背景音乐的音量。

**返回值**: `float` 当前背景音乐的音量。

### `float GetEffectVolum()`

**描述**: 获取当前音效的音量。

**返回值**: `float` 当前音效的音量。

### `void PlayEffic(int id, bool grad = false, float time = 0)`

**描述**: 播放指定 ID 的音效。如果 `grad` 为 `true`，则执行渐变效果。

**参数**:

- `id` (`int`): 音效的 ID。
- `grad` (`bool`): 是否使用渐变效果（默认 `false`）。
- `time` (`float`): 播放时间（默认 `0`）。

### `void PlayEffic(string name, bool grad = false, float time = 0)`

**描述**: 播放指定名称的音效。如果 `grad` 为 `true`，则执行渐变效果。

**参数**:

- `name` (`string`): 音效名称。
- `grad` (`bool`): 是否使用渐变效果（默认 `false`）。
- `time` (`float`): 播放时间（默认 `0`）。

### `IEnumerator StopEffic(float time)`

**描述**: 停止音效播放。

**参数**:

- `time` (`float`): 播放持续时间。

**返回值**: `IEnumerator` 协程对象。

### `IEnumerator GradEffic(float cliplength)`

**描述**: 对音效进行渐变处理。

**参数**:

- `cliplength` (`float`): 音效的持续时间。

**返回值**: `IEnumerator` 协程对象。

### `IEnumerator CrossfadeBackgroundMusic(AudioClip[] nextClips, float fadeDuration)`

**描述**: 切换背景音乐并进行渐变效果。

**参数**:

- `nextClips` (`AudioClip[]`): 下一首背景音乐的音频片段数组。
- `fadeDuration` (`float`): 渐变持续时间（秒）。

**返回值**: `IEnumerator` 协程对象。

### `void SetMusicVolume(float num)`

**描述**: 设置背景音乐的音量，并保存设置到 PlayerPrefs。

**参数**:

- `num` (`float`): 音量大小（最大值为 1.0）。

### `void SetEfficVolume(float num)`

**描述**: 设置音效的音量，并保存设置到 PlayerPrefs。

**参数**:

- `num` (`float`): 音量大小（最大值为 1.0）。

### `IEnumerator AudioPlayFinished(float time, UnityAction callback)`

**描述**: 播放音效后执行回调函数。

**参数**:

- `time` (`float`): 音效播放时间。
- `callback` (`UnityAction`): 播放完毕后的回调方法。

**返回值**: `IEnumerator` 协程对象。

### `void OnAudioConfigurationChanged(bool deviceWasChanged)`

**描述**: 处理音频配置更改事件，例如切换设备（蓝牙）。重新播放背景音乐。

**参数**:

- `deviceWasChanged` (`bool`): 是否发生了设备更改。



## API 方法

#### `IEnumerator Init(MonoBehaviour obj)`

- **描述**: 初始化 `LanguageManager`，加载语言配置数据，并发送语言加载完成消息。
- 参数:
  - `obj` (`MonoBehaviour`): 初始化所需的对象。
- **返回值**: `IEnumerator`：用于协程。



#### `LanguageItem AddLanguagerText(LanguageText val)`

- **描述**: 向 `languageTexts` 列表中添加一个语言文本。如果 `languageTexts` 中没有该文本，则将其添加；同时返回对应的 `LanguageItem` 对象。
- 参数:
  - `val` (`LanguageText`): 需要添加的语言文本。
- **返回值**: `LanguageItem`：返回添加的 `LanguageItem` 对象。如果没有找到对应的语言数据，则返回 `null`。



#### `void UpdateLanguage(Language language)`

- **描述**: 更新所有语言文本以反映新的语言设置。
- 参数:
  - `language` (`Language`): 当前选择的语言。
- **返回值**: 无



#### `Dictionary<Language, string> GetConfigLanguageString(string val)`

- **描述**: 处理配置表中的多语言设置，提取并返回语言和对应内容的字典。
- 参数:
  - `val` (`string`): 包含多语言设置的字符串，例如 `{[en:Hello][jp:こんにちは]}`。
- **返回值**: `Dictionary<Language, string>`：包含语言和对应的文本内容。如果配置字符串格式不正确，返回 `null`。



#### `Dictionary<Language, string[]> GetConfigLanguageArray(string val)`

- **描述**: 处理配置表中的多语言设置，提取并返回语言和对应内容数组的字典。
- 参数:
  - `val` (`string`): 包含多语言设置的字符串，例如 `{[en:Hello&World][jp:こんにちは&世界]}`。
- **返回值**: `Dictionary<Language, string[]>`：包含语言和对应的内容数组。如果配置字符串格式不正确，返回 `null`。



#### `static Dictionary<Language, string> ExtractContents(string input)`

- **描述**: 从输入字符串中提取语言和对应的内容。
- 参数:
  - `input` (`string`): 包含语言标签和内容的字符串，例如 `{[en:Hello][jp:こんにちは]}`。
- **返回值**: `Dictionary<Language, string>`：包含语言和对应的内容。如果没有匹配项，返回空字典。

### 示例：

```c#
// 添加语言文本
var languageText = new LanguageText { id = 1, content = "Hello World" };
var languageItem = LanguageManager.Instance.AddLanguagerText(languageText);

// 更新语言设置
LanguageManager.Instance.UpdateLanguage(Language.English);

// 获取配置中的语言字符串
string configString = "{[en:Hello][jp:こんにちは]}";
var languageDict = LanguageManager.Instance.GetConfigLanguageString(configString);

// 获取配置中的语言数组
string configArray = "{[en:Hello&World][jp:こんにちは&世界]}";
var languageArrayDict = LanguageManager.Instance.GetConfigLanguageArray(configArray);
```




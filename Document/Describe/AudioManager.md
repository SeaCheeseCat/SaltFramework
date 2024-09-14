

# **游戏音频管理 - AudioManager**



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

- #### `void SetMusicVolume(float num)`

  - **描述**: 设置背景音乐的音量大小。
  - **参数**:
    - `num` (`float`): 音量大小，范围从 0 到 1。
  - **返回值**: 无

  #### `void SetEfficVolume(float num)`

  - **描述**: 设置音效的音量大小。
  - **参数**:
    - `num` (`float`): 音量大小，范围从 0 到 1。
  - **返回值**: 无

  #### `void PlayMusic(string[] names, bool isLoop, bool grad)`

  - **描述**: 播放背景音乐，可以设置是否循环播放和是否进行渐变效果。
  - **参数**:
    - `names` (`string[]`): 音乐名称数组。
    - `isLoop` (`bool`): 是否循环播放背景音乐。
    - `grad` (`bool`): 是否使用渐变效果切换音乐。
  - **返回值**: 无

  #### `void PlayMainMusic()`

  - **描述**: 播放主界面的背景音乐。
  - **参数**: 无
  - **返回值**: 无

  #### `void PlayPageMusic(int page)`

  - **描述**: 播放指定章节的背景音乐。
  - **参数**:
    - `page` (`int`): 章节编号，用于查找对应的背景音乐。
  - **返回值**: 无

  #### `void PlayDefaultClickAudio()`

  - **描述**: 播放默认的点击音效。
  - **参数**: 无
  - **返回值**: 无

  #### `void PlayEffic(string name, bool grad = false, float time = 0)`

  - **描述**: 播放指定名称的音效，可以设置是否使用渐变效果以及播放时间。
  - **参数**:
    - `name` (`string`): 音效名称。
    - `grad` (`bool`): 是否使用渐变效果。
    - `time` (`float`): 播放时间（秒），0 表示播放完整音效。
  - **返回值**: 无

### 示例：

```c#
// 初始化音效管理器
AudioManager.Instance.Init();

// 设置背景音乐音量和音效音量
AudioManager.Instance.SetMusicVolume(0.7f); // 设置背景音乐音量为 70%
AudioManager.Instance.SetEfficVolume(0.5f); // 设置音效音量为 50%

// 播放主界面背景音乐
AudioManager.Instance.PlayMainMusic();

// 播放指定章节的背景音乐
AudioManager.Instance.PlayPageMusic(1); // 播放章节 1 的背景音乐

// 播放默认点击音效
AudioManager.Instance.PlayDefaultClickAudio();

// 播放自定义音效
AudioManager.Instance.PlayEffic("buttonClick", grad: true, time: 1.0f);

// 播放背景音乐，并进行渐变效果
string[] musicTracks = { "track1", "track2" };
AudioManager.Instance.PlayMusic(musicTracks, isLoop: true, grad: true);
```




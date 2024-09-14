

# **存档模块- ArchiveManager **



*`ArchiveManager` 是用于处理游戏存档和配置文件的管理模块。它提供了保存、加载、删除、检查文件存在等功能，以支持游戏中对地图数据、多语言配置和游戏配置的操作。*



## 类结构

### 方法

- **`void SaveMapConfigToJsonFile<T>(T data, int Chapter, int Level)`**:

  - **描述**: 保存地图数据到指定路径的 JSON 文件中。

  - 参数

    :

    - `data` (`T`): 要保存的地图数据。
    - `Chapter` (`int`): 章节编号。
    - `Level` (`int`): 关卡编号。

  - **返回值**: 无

- **`void SaveLanguageConfigToJsonFile<T>(T data)`**:

  - **描述**: 保存多语言数据到指定路径的 JSON 文件中。

  - 参数

    :

    - `data` (`T`): 要保存的多语言数据。

  - **返回值**: 无

- **`void SaveGameConfigToJsonFile<T>(T data)`**:

  - **描述**: 保存游戏配置数据到指定路径的 JSON 文件中。

  - 参数

    :

    - `data` (`T`): 要保存的游戏配置数据。

  - **返回值**: 无

- **`void SaveDataToJsonFile()`**:

  - **描述**: 自动保存游戏数据到 JSON 文件中。
  - **参数**: 无
  - **返回值**: 无

- **`void SaveDataToJsonFile<T>(T data)`**:

  - **描述**: 保存游戏数据到 JSON 文件中。

  - 参数

    :

    - `data` (`T`): 要保存的游戏数据。

  - **返回值**: 无

- **`void DeleteMapConfig(int Chapter, int Level)`**:

  - **描述**: 删除指定章节和关卡的地图配置文件。

  - 参数

    :

    - `Chapter` (`int`): 章节编号。
    - `Level` (`int`): 关卡编号。

  - **返回值**: 无

- **`void DeleteData()`**:

  - **描述**: 删除游戏存档文件。
  - **参数**: 无
  - **返回值**: 无

- **`T LoadMapConfigFromJson<T>(int Chapter, int Level)`**:

  - **描述**: 从 JSON 文件中加载地图配置数据。

  - 参数

    :

    - `Chapter` (`int`): 章节编号。
    - `Level` (`int`): 关卡编号。

  - **返回值**: `T` 类型的地图配置数据

- **`T LoadLanguageConfigFromJson<T>()`**:

  - **描述**: 从 JSON 文件中加载多语言数据。
  - **参数**: 无
  - **返回值**: `T` 类型的多语言数据

- **`T LoadGameConfigFromJson<T>()`**:

  - **描述**: 从 JSON 文件中加载游戏配置数据。
  - **参数**: 无
  - **返回值**: `T` 类型的游戏配置数据

- **`T LoadDataFromJson<T>()`**:

  - **描述**: 从 JSON 文件中加载游戏存档数据。
  - **参数**: 无
  - **返回值**: `T` 类型的游戏存档数据

- **`bool ExistDataFile()`**:

  - **描述**: 检查存档文件是否存在。
  - **参数**: 无
  - **返回值**: `bool`，指示存档文件是否存在

- **`string[] LaodMapConfigDic()`**:

  - **描述**: 加载并返回地图配置文件列表。
  - **参数**: 无
  - **返回值**: `string[]`，包含地图配置文件的名称

- **`void DeleteFile(string path)`**:

  - **描述**: 删除指定路径的文件。

  - 参数

    :

    - `path` (`string`): 要删除的文件路径。

  - **返回值**: 无

- **`T LoadFromJsonFile<T>(string val)`**:

  - **描述**: 从 JSON 字符串中加载并反序列化数据。

  - 参数

    :

    - `val` (`string`): 包含 JSON 数据的字符串。

  - **返回值**: `T` 类型的反序列化数据

- **`List<LevelData> GetDataFile()`**:

  - **描述**: 获取所有关卡存档数据。
  - **参数**: 无
  - **返回值**: `List<LevelData>`，包含所有关卡存档数据的列表

- **`LevelData GetLevelData(int chapter, int level)`**:

  - **描述**: 获取指定章节和关卡的存档数据。

  - 参数

    :

    - `chapter` (`int`): 章节编号。
    - `level` (`int`): 关卡编号。

  - **返回值**: `LevelData`，指定章节和关卡的存档数据。如果没有找到对应数据，则返回一个新的 `LevelData` 实例



## API 方法

#### `void SaveMapConfigToJsonFile<T>(T data, int Chapter, int Level)`

- **描述**: 保存地图数据到指定路径的 JSON 文件中。

- 参数

  :

  - `data` (`T`): 要保存的地图数据。
  - `Chapter` (`int`): 章节编号。
  - `Level` (`int`): 关卡编号。

- **返回值**: 无



#### `void SaveLanguageConfigToJsonFile<T>(T data)`

- **描述**: 保存多语言数据到指定路径的 JSON 文件中。

- 参数

  :

  - `data` (`T`): 要保存的多语言数据。

- **返回值**: 无



#### `void SaveGameConfigToJsonFile<T>(T data)`

- **描述**: 保存游戏配置数据到指定路径的 JSON 文件中。

- 参数

  :

  - `data` (`T`): 要保存的游戏配置数据。

- **返回值**: 无



#### `void SaveDataToJsonFile()`

- **描述**: 自动保存游戏数据到 JSON 文件中。
- **参数**: 无
- **返回值**: 无



#### `void SaveDataToJsonFile<T>(T data)`

- **描述**: 保存游戏数据到 JSON 文件中。

- 参数

  :

  - `data` (`T`): 要保存的游戏数据。

- **返回值**: 无



#### `void DeleteMapConfig(int Chapter, int Level)`

- **描述**: 删除指定章节和关卡的地图配置文件。

- 参数

  :

  - `Chapter` (`int`): 章节编号。
  - `Level` (`int`): 关卡编号。

- **返回值**: 无



#### `void DeleteData()`

- **描述**: 删除游戏存档文件。
- **参数**: 无
- **返回值**: 无



#### `T LoadMapConfigFromJson<T>(int Chapter, int Level)`

- **描述**: 从 JSON 文件中加载地图配置数据。

- 参数

  :

  - `Chapter` (`int`): 章节编号。
  - `Level` (`int`): 关卡编号。

- **返回值**: `T` 类型的地图配置数据



#### `T LoadLanguageConfigFromJson<T>()`

- **描述**: 从 JSON 文件中加载多语言数据。
- **参数**: 无
- **返回值**: `T` 类型的多语言数据



#### `T LoadGameConfigFromJson<T>()`

- **描述**: 从 JSON 文件中加载游戏配置数据。
- **参数**: 无
- **返回值**: `T` 类型的游戏配置数据



#### `T LoadDataFromJson<T>()`

- **描述**: 从 JSON 文件中加载游戏存档数据。
- **参数**: 无
- **返回值**: `T` 类型的游戏存档数据



#### `bool ExistDataFile()`

- **描述**: 检查存档文件是否存在。
- **参数**: 无
- **返回值**: `bool`，指示存档文件是否存在

### 示例：

```c#
ArchiveManager.Instance.SaveMapConfigToJsonFile(mapConfig, 1, 2);

var saveData = new SaveData
{
    // 数据属性
};

ArchiveManager.Instance.SaveDataToJsonFile(saveData);

//删除存档
ArchiveManager.Instance.DeleteData();
```




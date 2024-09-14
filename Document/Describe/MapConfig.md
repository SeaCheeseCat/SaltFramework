

# **地图编辑器模块- MapConfig**

*`MapConfig` 类是一个地图配置管理器，负责管理地图元素（如NPC、模型、粒子效果、地面）的数据。它提供了初始化、保存和获取场景数据的方法。*

## 类结构

### 成员变量

- **`Transform npcsTrans`**
  - **描述**: 存储NPC对象的Transform。
- **`Transform modelsTrans`**
  - **描述**: 存储场景模型对象的Transform。
- **`Transform particleTrans`**
  - **描述**: 存储粒子效果对象的Transform。
- **`Transform landTran`**
  - **描述**: 存储地面对象的Transform。
- **`const string npcTr`**
  - **描述**: NPC对象的字符串标识。
- **`const string sceneTr`**
  - **描述**: 场景模型对象的字符串标识。
- **`const string particleTr`**
  - **描述**: 粒子效果对象的字符串标识。
- **`const string landTrName`**
  - **描述**: 地面对象的字符串标识。
- **`MapConfigData configData`**
  - **描述**: 地图配置数据。
- **`Dictionary<int, Transform> tempEditNpcs`**
  - **描述**: 临时存储编辑器中NPC数据的字典。
- **`Dictionary<int, Transform> tempEditModel`**
  - **描述**: 临时存储编辑器中模型数据的字典。

## 方法

- **`void InitData()`**
  - **描述**: 初始化地图配置数据，从场景中查找并设置NPC、模型、粒子效果和地面的Transform。
  - **参数**: 无
  - **返回值**: 无
- **`MapDialogData Getmapdialogdatas(Transform item, ItemBase npc)`**
  - **描述**: 获取与指定NPC相关的对话数据。
  - 参数:
    - `item` (`Transform`): NPC的Transform。
    - `npc` (`ItemBase`): NPC对象。
  - **返回值**: `MapDialogData` 对象，包含NPC对话数据。
- **`DecryptObjectData[] GetChildModels(Transform parent)`**
  - **描述**: 递归获取指定父对象下所有子物体的位置信息、缩放信息和旋转信息。
  - 参数:
    - `parent` (`Transform`): 父对象的Transform。
  - **返回值**: `DecryptObjectData[]` 数组，包含所有子物体的信息。
- **`void SavePartitleData()`**
  - **描述**: 保存场景中的粒子效果数据到 `configData`。
  - **参数**: 无
  - **返回值**: 无
- **`void SaveLandData()`**
  - **描述**: 保存场景中的地面数据到 `configData`。
  - **参数**: 无
  - **返回值**: 无
- **`List<MapParticleData> Getmapparticledatas()`**
  - **描述**: 获取场景中所有粒子效果的位置信息、缩放信息、旋转信息及ID。
  - **参数**: 无
  - **返回值**: `List<MapParticleData>`，包含所有粒子效果的数据。
- **`MapLandData Getmaplanddata()`**
  - **描述**: 获取场景中地面的位置信息、缩放信息、旋转信息以及地面下的解密对象数据。
  - **参数**: 无
  - **返回值**: `MapLandData` 对象，包含地面数据。

## API 方法

### `void InitData()`

- **描述**: 初始化地图配置数据，从场景中查找并设置NPC、模型、粒子效果和地面的Transform。
- **参数**: 无
- **返回值**: 无

### `MapDialogData Getmapdialogdatas(Transform item, ItemBase npc)`

- **描述**: 获取与指定NPC相关的对话数据。
- 参数:
  - `item` (`Transform`): NPC的Transform。
  - `npc` (`ItemBase`): NPC对象。
- **返回值**: `MapDialogData` 对象，包含NPC对话数据。

### `DecryptObjectData[] GetChildModels(Transform parent)`

- **描述**: 递归获取指定父对象下所有子物体的位置信息、缩放信息和旋转信息。
- 参数:
  - `parent` (`Transform`): 父对象的Transform。
- **返回值**: `DecryptObjectData[]` 数组，包含所有子物体的信息。

### `void SavePartitleData()`

- **描述**: 保存场景中的粒子效果数据到 `configData`。
- **参数**: 无
- **返回值**: 无

### `void SaveLandData()`

- **描述**: 保存场景中的地面数据到 `configData`。
- **参数**: 无
- **返回值**: 无

### `List<MapParticleData> Getmapparticledatas()`

- **描述**: 获取场景中所有粒子效果的位置信息、缩放信息、旋转信息及ID。
- **参数**: 无
- **返回值**: `List<MapParticleData>`，包含所有粒子效果的数据。

### `MapLandData Getmaplanddata()`

- **描述**: 获取场景中地面的位置信息、缩放信息、旋转信息以及地面下的解密对象数据。
- **参数**: 无
- **返回值**: `MapLandData` 对象，包含地面数据。

### 示例：

```
c#复制代码using UnityEngine;

public class TestMapConfig : MonoBehaviour
{
    public MapConfig mapConfig;

    void Start()
    {
        // 初始化数据
        mapConfig.InitData();

        // 获取并打印粒子数据
        var particleData = mapConfig.Getmapparticledatas();
        foreach (var data in particleData)
        {
            Debug.Log($"Particle ID: {data.ID}, Position: ({data.x}, {data.y}, {data.z})");
        }

        // 获取并打印地面数据
        var landData = mapConfig.Getmaplanddata();
        Debug.Log($"Land Position: ({landData.x}, {landData.y}, {landData.z})");
    }
}
```

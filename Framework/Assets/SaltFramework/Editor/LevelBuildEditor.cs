using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuildEditor 
{
    [FoldoutGroup("详细配置")]
    [EnumToggleButtons, LabelText("文件储存类型")]
    public SaveDataType SomeEnumField;
}

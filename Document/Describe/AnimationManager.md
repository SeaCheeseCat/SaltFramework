

# **动画模块- AnimationManager**

*`AnimationManager` 类是一个动画管理器，负责管理和更新动画。它允许添加动画并在每帧更新时处理动画状态。*

## 类结构

### 成员变量

- `List<AnimationBase> animations`
  - **描述**: 存储所有当前管理的动画对象。

### 方法

- **`void AddAnimation(AnimationBase animation)`**
  - **描述**: 向动画管理器中添加一个动画并播放该动画。
  - 参数:
    - `animation` (`AnimationBase`): 要添加并播放的动画对象。
  - **返回值**: 无
- **`void Update()`**
  - **描述**: 在每帧更新时，更新所有动画的状态，并从管理器中移除已完成的动画。
  - **参数**: 无
  - **返回值**: 无

## API 方法

### `void AddAnimation(AnimationBase animation)`

- **描述**: 向动画管理器中添加一个动画并播放该动画。
- 参数:
  - `animation` (`AnimationBase`): 要添加并播放的动画对象。
- **返回值**: 无

### `void Update()`

- **描述**: 在每帧更新时，更新所有动画的状态，并从管理器中移除已完成的动画。
- **参数**: 无
- **返回值**: 无

### 示例：

```
c#复制代码using UnityEngine;

public class TestAnimationManager : MonoBehaviour
{
    public AnimationManager animationManager;

    void Start()
    {
        // 创建并添加一个动画
        AnimationBase animation = new SampleAnimation(); // 假设 SampleAnimation 继承自 AnimationBase
        animationManager.AddAnimation(animation);
    }

    void Update()
    {
        // 更新动画管理器
        animationManager.Update();
    }
}
```

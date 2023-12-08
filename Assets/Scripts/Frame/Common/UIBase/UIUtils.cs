using UnityEngine;

/// <summary>
/// UI相关工具类
/// </summary>
public static class UIUtils
{
    /// <summary>
    /// 将世界坐标转换为UI坐标
    /// </summary>
    /// <returns>The point world to user interface.</returns>
    /// <param name="worldPos">World position.</param>
    public static Vector2 TransPointWorldToUI(RectTransform parent, Vector3 worldPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, Camera.main.WorldToScreenPoint(worldPos), UIManager.Instance.UICamera, out Vector2 pos);
        return pos;
    }
}

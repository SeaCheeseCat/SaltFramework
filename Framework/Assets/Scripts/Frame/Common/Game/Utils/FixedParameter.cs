using UnityEngine;
/// <summary>
/// 游戏固定参数获取,比如禁止颜色,边框颜色等等
/// </summary>
public class FixedParameter
{
    private static Color normalColor = new Color (0.91015625f, 0.86328125f, 0.6875f, 1f);

    /// <summary>
    /// 获取价格文字颜色
    /// </summary>
    /// <param name="isEnougth"></param>
    /// <returns></returns>
    public static Color GetPriceColor (bool isEnougth)
    {
        if (isEnougth)
        {
            return normalColor;
        }
        else
        {
            return Color.red;
        }
    }
}
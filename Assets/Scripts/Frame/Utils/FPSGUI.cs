using UnityEngine;
using System.Collections;
/// <summary>
/// ONGUI帧显示
/// </summary>
public class FPSGUI : MonoBehaviour
{
    private float currentTime = 0;
    private float lateTime = 0;
    private float framesNum = 0;
    private float fpsTime = 0;
    
    GUIStyle labelFont;
    private const int fontsize = 40;  //GUI显示的  字体大小
    private Color fontcolor = new Color(1, 1, 1, 1);   //GUI显示的  字体颜色

    private void Awake()
    {

        //定义一个GUIStyle的对象
        labelFont = new GUIStyle();
        //设置文本颜色
        labelFont.normal.textColor = fontcolor;
        //设置字体大小
        labelFont.fontSize = fontsize;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        framesNum++;
        if (currentTime - lateTime >= 1.0f)
        {
            fpsTime = framesNum / (currentTime - lateTime);
            lateTime = currentTime;
            framesNum = 0;
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 100, 100), "帧率（FPS） : " + string.Format("{0:F}", fpsTime),labelFont);
    }
}
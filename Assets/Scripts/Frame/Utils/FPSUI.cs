using UnityEngine;
using UnityEngine.UI;
public class FPSUI : UIBase {

    public float updateInterval = 0.5f; //测定间隔

    private float lastInterval; //下一次测定间隔
    private int frames = 0;
    private float fps;  //帧率
    public Text Txt;

    protected override void Awake()
    {
        //base.Awake();
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow >= lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
            Txt.text = "FPS:" + Mathf.Round(fps);
        }
      
    }
}

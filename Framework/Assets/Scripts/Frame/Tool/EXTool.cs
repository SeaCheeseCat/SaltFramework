
using UnityEngine;

public class ExTool: MonoBehaviour
{
    public static Vector3 ScreenToWorldVector(Vector3 v3) 
    {
       return Camera.main.ScreenToWorldPoint(v3);
    }

  


}

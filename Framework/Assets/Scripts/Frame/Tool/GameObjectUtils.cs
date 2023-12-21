using UnityEngine;

public static class GameObjectUtils
{
    public static void ResetObjectLayer2UI(GameObject obj)
    {
        obj.layer = 5;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            ResetObjectLayer2UI(obj.transform.GetChild(i).gameObject);
        }
    }

    public static void ResetObjectLayerModel(GameObject obj)
    {
        obj.layer = 9;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            ResetObjectLayerModel(obj.transform.GetChild(i).gameObject);
        }
    }

    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        T res = obj.GetComponent<T>();
        if (res == null)
        {
            return obj.AddComponent<T>();
        }
        else
        {
            return res;
        }
    }

    public static void ResetParent(this Transform trans, Transform parent)
    {
        trans.SetParent(parent);
        trans.localPosition = Vector3.zero;
        trans.localScale = Vector3.one;
        trans.localRotation = Quaternion.identity;
    }

    public static void Reset(this Transform trans)
    {
        trans.localPosition = Vector3.zero;
        trans.localScale = Vector3.one;
        trans.localRotation = Quaternion.identity;
    }


    public static void SetPosZ(this Transform trans, float Z)
    {
        trans.position = new Vector3(trans.position.x, trans.position.y, Z);
    }

    public static void SetLocalPosZ(this Transform trans, float Z)
    {
        trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, Z);
    }


    public static void SetPosX(this Transform trans, float X)
    {
        trans.position = new Vector3(X, trans.position.y, trans.position.z);
    }

    public static void SetLocalPosX(this Transform trans, float X)
    {
        trans.localPosition = new Vector3(X, trans.localPosition.y, trans.localPosition.z);
    }


    public static void SetPosY(this Transform trans, float Y)
    {
        trans.position = new Vector3(trans.position.x, Y, trans.position.z);
    }


    public static void SetLoaclPosY(this Transform trans, float Y)
    {
        trans.localPosition = new Vector3(trans.localPosition.x, Y, trans.localPosition.z);
    }
}
using System;

public class BaseResData
{
    public int ID;

    public virtual void FillData(string data)
    {
    }

    public static bool HasKey { get { return false; } }
}

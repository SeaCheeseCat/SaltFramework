using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNpcData
{
    public int ID;   //NpcID
    public double x;
    public double y;
    public double z;
    public double scalex;
    public double scaley;
    public double scalez;
    public double rotatex;
    public double rotatey;
    public double rotatez;
    public DecryptObjectData[] decryptObjectdatas;
}


public class MapModelData 
{
    public int ID;
    public double x;
    public double y;
    public double z;
    public double scalex;
    public double scaley;
    public double scalez;
    public double rotatex;
    public double rotatey;
    public double rotatez;
    public DecryptObjectData[] decryptObjectdatas;
}


public class MapParticleData
{
    public int ID;
    public double x;
    public double y;
    public double z;
    public double scalex;
    public double scaley;
    public double scalez;
    public double rotatex;
    public double rotatey;
    public double rotatez;
    public DecryptObjectData[] decryptObjectdatas;
}

public class DecryptObjectData
{
    public int OwnId;
    public int DecryptId;
    public double x;
    public double y;
    public double z;
    public double scalex;
    public double scaley;
    public double scalez;
    public double rotatex;
    public double rotatey;
    public double rotatez;
    public DecryptObjectData[] childs;
}

public class MapLandData
{
    public int ID;
    public double x;
    public double y;
    public double z;
    public double scalex;
    public double scaley;
    public double scalez;
    public double rotatex;
    public double rotatey;
    public double rotatez;
    public DecryptObjectData[] decryptObjectdatas;
}

public class MapDialogData
{
    public int OwnerNpcID;
    public int ID;
    public string content;
    public double x;
    public double y;
    public double z;
    public double scalex;
    public double scaley;
    public double scalez;
    public double rotatex;
    public double rotatey;
    public double rotatez;
}
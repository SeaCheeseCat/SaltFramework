using System.Collections.Generic;
using Beebyte.Obfuscator;
using System.Text;
using UnityEngine;

[Skip]
public class {0} : ResponseData
{
{1}

    public override void FillDataFromJson (object val)
    {
        Dictionary<string, object> _rawdata = val as Dictionary<string, object>;

{2}
    }

    public override string ToString (int offset)
    {
        string space = base.GetOffsetSpace (offset);
        StringBuilder builder = new StringBuilder ();

{3}
        return builder.ToString();
    }

}
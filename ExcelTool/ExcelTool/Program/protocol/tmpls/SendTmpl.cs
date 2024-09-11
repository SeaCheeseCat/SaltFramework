using System.Collections.Generic;
using Beebyte.Obfuscator;
using System.Text;
using UnityEngine;

[Skip]
public class {0} : RequestData
{
{1}

    public override Dictionary<string, object> GetJsonObj ()
    {
        var _resData = base.GetJsonObj ();
    	
{2}
        return _resData;
    }

    public override string ToString (int offset)
    {
        string space = base.GetOffsetSpace (offset);
        StringBuilder builder = new StringBuilder ();
		builder.Append (space + "[channelID:" + channelID + "]\n");
        builder.Append (space + "[uid:" + uid + "]\n");

{3}

        builder.Append (space + "[timestamp:" + timestamp + "]\n");
		return builder.ToString();
    }

}
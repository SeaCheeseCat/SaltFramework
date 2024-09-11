using System.Collections.Generic;

public class Protocols
{
    private static Dictionary<string, ProtocolCfg> s_dict = new Dictionary<string, ProtocolCfg> ();

{0}
    public static void Setup ()
    {
        s_dict.Clear ();
{1}
    }

    public static ProtocolCfg GetProtocol (string name)
    {
        return s_dict[name];
    }
}

public class ProtocolCfg
{
    public string name;
    public string url;
    public float blockTime;
    public string request;
    public string response;

    public ProtocolCfg (string name, string url, string request, string response, float blockTime)
    {
        this.name = name;
        this.url = url;
        this.request = request;
        this.response = response;
        this.blockTime = blockTime;
    }
}
using System.Collections;

namespace echo.common.structures
{
    public interface ILoader
    {
        IEnumerator OnLoad();
        int LoadOrder { get; }
    }
}

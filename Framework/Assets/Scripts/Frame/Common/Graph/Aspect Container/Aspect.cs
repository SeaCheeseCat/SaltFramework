using UnityEngine;

namespace echo.common.container
{
    public interface IAspect
    {
        IContainer Container { get; set; }
    }

    public class Aspect : IAspect
    {
        public IContainer Container { get; set; }
    }
}
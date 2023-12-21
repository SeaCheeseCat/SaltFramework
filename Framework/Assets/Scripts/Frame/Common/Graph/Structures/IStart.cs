using System;
using echo.common.container;

namespace echo.common.structures
{
    public interface IStart
    {
        void OnStart();
    }

    public static class StartExtensions
    {
        public static void OnStart(this IContainer container)
        {
            foreach (IAspect aspect in container.Aspects())
            {
                if (aspect is IStart item)
                    item.OnStart();
            }
        }
    }
}


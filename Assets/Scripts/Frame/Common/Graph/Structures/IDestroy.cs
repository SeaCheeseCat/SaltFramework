using System;
using echo.common.container;

namespace echo.common.structures
{
    public interface IDestroy
    {
        void OnDestroy();
    }

    public static class DestroyExtensions
    {
        public static void OnDestroy(this IContainer container)
        {
            foreach (IAspect aspect in container.Aspects())
            {
                if (aspect is IDestroy item)
                    item.OnDestroy();
            }
        }
    }
}

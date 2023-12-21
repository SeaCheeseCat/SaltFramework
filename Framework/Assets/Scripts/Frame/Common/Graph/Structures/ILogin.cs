using System;
using echo.common.container;

namespace echo.common.structures
{
    public interface ILogin
    {
        void OnLogin();
    }

    public static class LoginExtensions
    {
        public static void OnLogin(this IContainer container)
        {
            foreach (IAspect aspect in container.Aspects())
            {
                if (aspect is ILogin item)
                    item.OnLogin();
            }
        }
    }
}

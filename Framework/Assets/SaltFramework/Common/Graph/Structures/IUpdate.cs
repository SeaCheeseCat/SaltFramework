using System;
using echo.common.container;

namespace echo.common.structures
{
    public interface IUpdate
    {
        void OnUpdate(float deltaTime);
    }

    public interface IUpdateLogic
    {
        void OnUpdateLogic(int deltaTime);
        void OnLateUpdateLogic();
        int LogicOrder { get; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace echo.common.graph
{
    /// <summary>
    /// 导航节点路径
    /// 携带了路径地形信息和与之相关的其他信息
    /// </summary>
    public class NavGraphEdge : GraphEdge
    {

        public NavGraphEdge(int from, int to, float cost = 1)
            : base(from, to, cost)
        {
        }

        public override object Clone()
        {
            return new NavGraphEdge(From, To, Cost);
        }
    }
}

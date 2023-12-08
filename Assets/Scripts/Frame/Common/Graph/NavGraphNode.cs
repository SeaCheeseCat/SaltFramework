using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace echo.common.graph
{
    /// <summary>
    /// 导航图节点
    /// </summary>
    public class NavGraphNode : GraphNode
    {
        public Vector3 Position { get; protected set; }
        public object ExtraInfo { get; set; }

        public NavGraphNode(Vector3 pos): base()
        {
            Position = pos;
        }
    }
}

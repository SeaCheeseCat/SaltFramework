using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.common.graph
{
    public interface INode : ICloneable
    {
        int Index { get; set; }
    }

    public class GraphNode: INode
    {
        public const int INVALID_NODE_INDEX = -1;

        public int Index { get; set; }

        public GraphNode(int index = INVALID_NODE_INDEX)
        {
            Index = index;
        }

        public object Clone()
        {
            return new GraphNode(Index);
        }
    }
}

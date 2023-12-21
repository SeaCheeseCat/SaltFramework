using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.common.graph
{
    public interface IEdge : ICloneable
    {
        int From { get; set; }
        int To { get; set; }
        float Cost { get; set; }
    }

    public class GraphEdge : IEdge
    {
        public int From { get; set; }
        public int To { get; set; }
        public float Cost { get; set; }

        public GraphEdge(int from, int to, float cost = 1)
        {
            From = from;
            To = to;
            Cost = cost;
        }

        public GraphEdge(float cost) : this(GraphNode.INVALID_NODE_INDEX, GraphNode.INVALID_NODE_INDEX, cost)
        {

        }

        public GraphEdge() :this(1)
        {

        }

        public virtual object Clone()
        {
            return new GraphEdge(From, To ,Cost);
        }
    }
}

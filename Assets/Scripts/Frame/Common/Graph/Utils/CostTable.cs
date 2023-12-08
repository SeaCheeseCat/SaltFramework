using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.common.graph
{
    /// <summary>
    /// 路径映射表
    /// </summary>
    public class CostTable
    {
        public static float[,] GetCostTabel<N, E>(IGraph<N, E> m_graph) where N : NavGraphNode where E : GraphEdge
        {
            float[,] res = new float[m_graph.NodesCount, m_graph.NodesCount];
            Dijkstra<N, E> dijkstra = new Dijkstra<N, E>(m_graph);

            foreach (var node in m_graph.Nodes)
            {
                dijkstra.DijkstraSearch(node.Index);

                for (int k = 0; k < dijkstra.CostToThisNode.Length; k++)
                {
                    res[node.Index, k] = dijkstra.CostToThisNode[k];
                }
            }
            return res;
        }
    }
}

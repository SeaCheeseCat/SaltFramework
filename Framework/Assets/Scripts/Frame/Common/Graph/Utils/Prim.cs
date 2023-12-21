using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.common.graph
{
    public partial class GraphUtils
    {
        /// <summary>
        /// 求最小生成树
        /// </summary>
        /// <typeparam name="N"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static List<N> PrimTree<N, E>(List<N> graph) where N : GraphNode, new() where E : GraphEdge, new()
        {
            Dictionary<int, N> nodesDict = new Dictionary<int, N>();
            for (int n = 0; n < graph.Count; n++)
            {
                nodesDict.Add(graph[n].Index, graph[n]);
            }

            List<N> visitedNodes = new List<N>();

            //N temp = new N();
            //temp.Copy(graph[0]);
            //visitedNodes.Add(temp);

            //for (int n = 0; n < graph.Count; n++)
            //{
            //    temp = new N();
            //    E tempEdge = null;

            //    foreach (N v in graph)
            //    {
            //        foreach (E e in v.Edges)
            //        {
            //            if (NodeContains(visitedNodes, e.From) && !NodeContains(visitedNodes, e.To))
            //            {
            //                if (tempEdge == null || e.Cost < tempEdge.Cost)
            //                {
            //                    temp.Copy(nodesDict[e.To]);
            //                    tempEdge = e;
            //                }
            //            }
            //        }
            //    }

            //    if (temp.Index != GraphNode.INVALID_NODE_INDEX && !NodeContains(visitedNodes, temp.Index))
            //    {
            //        temp.Edges.Add(tempEdge);
            //        visitedNodes.Add(temp);
            //    }
            //}
            return visitedNodes;
        }

        private static bool NodeContains<N>(List<N> list, int index) where N : GraphNode
        {
            foreach (var node in list)
            {
                if (node.Index == index)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

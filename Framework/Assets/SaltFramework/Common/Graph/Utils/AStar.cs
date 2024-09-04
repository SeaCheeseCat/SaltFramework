using echo.queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace echo.common.graph
{

    public partial class GraphUtils<N, E> where N : NavGraphNode where E : GraphEdge
    {
        /// <summary>
        /// 启发函数
        /// </summary>
        /// <typeparam name="N">节点类型</typeparam>
        /// <typeparam name="E">边类型</typeparam>
        /// <param name="graph">导航图</param>
        /// <param name="index1">计算节点1</param>
        /// <param name="index2">计算节点2</param>
        /// <returns>启发距离</returns>
        public delegate float HeuristicFunc(IGraph<N, E> graph, int index1, int index2);

        /// <summary>
        /// 欧几里得启发函数
        /// </summary>
        public static readonly HeuristicFunc euclid = (graph, index1, index2) =>
        {
            return Vector3.Distance(graph.GetNode(index1).Position, graph.GetNode(index2).Position);
        };

        /// <summary>
        /// 曼哈顿启发函数
        /// </summary>
        public static readonly HeuristicFunc manhattan = (graph, index1, index2) =>
        {
            return Mathf.Abs(graph.GetNode(index1).Position.x - graph.GetNode(index2).Position.x) + Mathf.Abs(graph.GetNode(index1).Position.y - graph.GetNode(index2).Position.y);
        };

        public static bool AStar(IGraph<N, E> graph, int source, int target, out List<N> path)
        {
            return AStar(graph, source, target, euclid, out path);
        }

        /// <summary>
        /// A星寻路
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="heuristic">启发函数</param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool AStar(IGraph<N, E> graph, int source, int target, HeuristicFunc heuristic,  out List<N> path)
        {
            int nodeCount = graph.NodesCount;

            float[] fCost = new float[nodeCount];
            float[] gCost = new float[nodeCount];

            Dictionary<int, E> shortestPathTree = new Dictionary<int, E>();
            Dictionary<int, E> searchFrontier = new Dictionary<int, E>();

            IndexedPriorityQueue priorityQueue = new IndexedPriorityQueue(fCost);
            priorityQueue.Insert(source);

            while (!priorityQueue.IsEmtpy())
            {
                int nextClosestNodeIndex = priorityQueue.Pop();

                if (searchFrontier.ContainsKey(nextClosestNodeIndex))
                {
                    shortestPathTree[nextClosestNodeIndex] = searchFrontier[nextClosestNodeIndex];
                }

                if (nextClosestNodeIndex == target)
                {
                    path = new List<N>();

                    int nd = target;
                    path.Add(graph.GetNode(nd));

                    while ((nd != source) && (shortestPathTree.ContainsKey(nd)))
                    {
                        nd = shortestPathTree[nd].From;
                        path.Add(graph.GetNode(nd));
                    }

                    return true;
                }

                foreach (var edge in graph.GetEdgesByNode(nextClosestNodeIndex))
                {
                    float HCost = heuristic(graph, target, edge.To);
                    float GCost = gCost[nextClosestNodeIndex] + edge.Cost;

                    if (!searchFrontier.ContainsKey(edge.To))
                    {
                        fCost[edge.To] = HCost + GCost;
                        gCost[edge.To] = GCost;
                        priorityQueue.Insert(edge.To);
                        searchFrontier.Add(edge.To, edge);
                    }
                    else if (GCost < fCost[edge.To] && !shortestPathTree.ContainsKey(edge.To))
                    {
                        fCost[edge.To] = HCost + GCost;
                        gCost[edge.To] = GCost;
                        priorityQueue.ChangePriority(edge.To);
                        searchFrontier[edge.To] = edge;
                    }
                }
            }

            path = null;
            return false;
        }
    }
}

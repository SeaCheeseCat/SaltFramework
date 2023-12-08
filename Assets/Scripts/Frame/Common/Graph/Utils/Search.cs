using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.common.graph
{
    public partial class GraphUtils
    {
        private static Dictionary<int,int> m_route = new Dictionary<int, int>();
        private static Dictionary<int, bool> m_visited = new Dictionary<int, bool>();

        /// <summary>
        /// 深度优先搜索
        /// </summary>
        /// <typeparam name="N">节点类型</typeparam>
        /// <typeparam name="E">边类型</typeparam>
        /// <param name="graph">目标图</param>
        /// <param name="source">源节点</param>
        /// <param name="target">目标节点</param>
        /// <param name="path">搜索得到的路径</param>
        /// <returns>是否成功搜索到目标点</returns>
        public static bool SearchDFS<N, E>(IGraph<N,E> graph, int source, int target, out List<N> path) where N : GraphNode where E : GraphEdge, new()
        {
            m_route.Clear();
            m_visited.Clear();

            Stack<E> stack = new Stack<E>();

            E dummy = new E
            {
                From = source,
                To = source,
                Cost = 1
            };
            stack.Push(dummy);

            while (stack.Count > 0)
            {
                var nextEdge = stack.Pop();

                if (m_route.ContainsKey(nextEdge.To))
                {
                    m_route[nextEdge.To] = nextEdge.From;
                }
                else
                {
                    m_route.Add(nextEdge.To, nextEdge.From);
                }

                if (!m_visited.ContainsKey(nextEdge.To))
                {
                    m_visited.Add(nextEdge.To, true);
                }

                if (nextEdge.To == target)
                {
                    path = new List<N>();
                    int curNode = target;

                    path.Add(graph.GetNode(target));
                    while (curNode != source)
                    {
                        path.Add(graph.GetNode(m_route[curNode]));
                        curNode = m_route[curNode];
                    }
                    return true;
                }

                var es = graph.GetEdgesByNode(nextEdge.To);
                foreach (var edge in es)
                {
                    if (!m_visited.ContainsKey(edge.To))
                    {
                        stack.Push(edge);
                    }
                }
            }

            path = null;
            return false;
        }

        /// <summary>
        /// 广度优先搜索
        /// </summary>
        /// <typeparam name="N">节点类型</typeparam>
        /// <typeparam name="E">边类型</typeparam>
        /// <param name="graph">目标图</param>
        /// <param name="source">源节点</param>
        /// <param name="target">目标节点</param>
        /// <param name="path">搜索得到的路径</param>
        /// <returns>是否成功搜索到目标点</returns>
        public static bool SearchBFS<N, E>(IGraph<N, E> graph, int source, int target, out List<N> path) where N : GraphNode where E : GraphEdge, new()
        {
            m_route.Clear();
            m_visited.Clear();

            Queue<E> queue = new Queue<E>();

            E dummy = new E
            {
                From = source,
                To = source,
                Cost = 1
            };
            queue.Enqueue(dummy);
            m_visited.Add(source, true);

            while (queue.Count > 0)
            {
                var nextEdge = queue.Dequeue();

                if (m_route.ContainsKey(nextEdge.To))
                {
                    m_route[nextEdge.To] = nextEdge.From;
                }
                else
                {
                    m_route.Add(nextEdge.To, nextEdge.From);
                }

                if (nextEdge.To == target)
                {
                    path = new List<N>();
                    int curNode = target;

                    path.Add(graph.GetNode(target));
                    while (curNode != source)
                    {
                        path.Add(graph.GetNode(m_route[curNode]));
                        curNode = m_route[curNode];
                    }
                    return true;
                }

                var es = graph.GetEdgesByNode(nextEdge.To);
                foreach (var edge in es)
                {
                    if (!m_visited.ContainsKey(edge.To))
                    {
                        queue.Enqueue(edge);
                        m_visited.Add(edge.To, true);
                    }
                }
            }

            path = null;
            return false;
        }
    }
}

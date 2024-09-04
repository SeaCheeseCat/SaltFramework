using echo.queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.common.graph
{
    public class Dijkstra<N, E> where N : NavGraphNode where E : GraphEdge
    {
        private IGraph<N, E> m_graph;
        private float[] m_costToThisNode;

        private Dictionary<int, E> m_shortestPathTree;
        private Dictionary<int, E> m_searchFrontier;

        public float[] CostToThisNode { get { return m_costToThisNode; } }

        public Dijkstra(IGraph<N, E> graph)
        {
            m_graph = graph;
            int nodeCount = graph.NodesCount;
            m_costToThisNode = new float[nodeCount];
            m_shortestPathTree = new Dictionary<int, E>();
            m_searchFrontier = new Dictionary<int, E>();
        }

        /// <summary>
        /// 迪杰斯特拉搜索
        /// </summary>
        public bool DijkstraSearch( int source, int target = -1)
        {
            m_costToThisNode = new float[m_graph.NodesCount];
            m_shortestPathTree.Clear();
            m_searchFrontier.Clear();

            IndexedPriorityQueue m_priorityQueue = new IndexedPriorityQueue(m_costToThisNode);
            m_priorityQueue.Insert(source);

            while (!m_priorityQueue.IsEmtpy())
            {
                int nextClosestNodeIndex = m_priorityQueue.Pop();

                if (m_searchFrontier.ContainsKey(nextClosestNodeIndex))
                {
                    m_shortestPathTree[nextClosestNodeIndex] = m_searchFrontier[nextClosestNodeIndex];
                }

                if (nextClosestNodeIndex == target)
                {
                    return true;
                }

                foreach (var edge in m_graph.GetEdgesByNode(nextClosestNodeIndex))
                {
                    float newCost = m_costToThisNode[nextClosestNodeIndex] + edge.Cost;

                    if (!m_searchFrontier.ContainsKey(edge.To))
                    {
                        m_costToThisNode[edge.To] = newCost;
                        m_priorityQueue.Insert(edge.To);
                        m_searchFrontier.Add(edge.To, edge);
                    }
                    else if (newCost < m_costToThisNode[edge.To] && !m_shortestPathTree.ContainsKey(edge.To))
                    {
                        m_costToThisNode[edge.To] = newCost;
                        m_priorityQueue.ChangePriority(edge.To);
                        m_searchFrontier[edge.To] = edge;
                    }
                }
            }
            return false;
        }
    }
}

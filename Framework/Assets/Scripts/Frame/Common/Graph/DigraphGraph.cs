using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.common.graph
{
    /// <summary>
    /// 单向图
    /// </summary>
    public class DigraphGraph<N, E> : IGraph<N, E> where N : INode where E : IEdge
    {

        private List<N> m_nodes = new List<N>();
        private List<List<E>> m_edges = new List<List<E>>();
        private List<List<E>> m_edgesTo = new List<List<E>>();

        public int AllNodeCount { get { return m_nodes.Count; } }

        public IEnumerable<N> Nodes
        {
            get
            {
                foreach (N node in m_nodes)
                {
                    if (node != null && node.Index != GraphNode.INVALID_NODE_INDEX)
                    {
                        yield return node;
                    }
                }
            }
        }

        public IEnumerable<E> Edges
        {
            get
            {
                foreach (var edgeList in m_edges)
                {
                    foreach (var edge in edgeList)
                    {
                        yield return edge;
                    }
                }
            } 
        }

        public int NodesCount {
            get {
                return m_nodes.Count;
            }
        }

        public int EdgesCount {
            get
            {
                int count = 0;
                for (int k = 0; k < m_edges.Count; k++)
                {
                    count += m_edges[k].Count;
                }
                return count;
            }
        }

        public bool IsEmpty { get { return m_nodes.Count <= 0; } }

        public int NextFreeNodeIndex { get; protected set; }

        public bool IsDigraph {get {return true;}}

        public void AddEdge(E edge)
        {
            if (edge.From >= NextFreeNodeIndex || edge.To >= NextFreeNodeIndex)
            {
                throw new Exception("AddEdge invalid index");
            }
            if (edge.From != GraphNode.INVALID_NODE_INDEX && edge.To != GraphNode.INVALID_NODE_INDEX)
            {
                if (!IsEdgePresent(edge.From, edge.To))
                {
                    m_edges[edge.From].Add(edge);
                    m_edgesTo[edge.To].Add(edge);
                }
            }
        }

        public void AddNodeWithIndex(int index, N node)
        {
            if (IsNodePresent(index))
            {
                throw new Exception("Attempting to add a node with a duplicate ID");
            }
            if (index >= 0 )
            {
                if(index < m_nodes.Count)
                {
                    node.Index = index;
                    m_nodes[index] = node;
                }
                else
                {
                    while(index >= m_nodes.Count)
                    {
                        NextFreeNodeIndex++;
                        m_nodes.Add(default(N));
                        m_edges.Add(new List<E>());
                        m_edgesTo.Add(new List<E>());
                    }
                    node.Index = index;
                    m_nodes[index] = node;
                }
            }
        }

        public int AddNode(N node)
        {
            if (IsNodePresent(node.Index))
            {
                throw new Exception("Attempting to add a node with a duplicate ID");
            }
            if (node.Index >= 0 && node.Index < m_nodes.Count)
            {
                m_nodes[node.Index] = node;
            }
            else
            {
                node.Index = NextFreeNodeIndex++;
                m_nodes.Add(node);
                m_edges.Add(new List<E>());
                m_edgesTo.Add(new List<E>());
            }

            return NextFreeNodeIndex;
        }

        public E GetEdge(int from, int to)
        {
            if (IsNodePresent(from) && IsNodePresent(to))
            {
                for (int k = 0; k < m_edges[from].Count; k++)
                {
                    if (m_edges[from][k].To == to)
                    {
                        return m_edges[from][k];
                    }
                }
            }
            return default(E);
        }

        public IEnumerable<E> GetEdgesByNode(int nodeIndex)
        {
            if (IsNodePresent(nodeIndex))
            {
                return m_edges[nodeIndex];
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<E> GetToEdgesByNode(int nodeIndex)
        {
            if (IsNodePresent(nodeIndex))
            {
                return m_edgesTo[nodeIndex];
            }
            else
            {
                return null;
            }
        }

        public N GetNode(int index)
        {
            if (IsNodePresent(index))
            {
                return m_nodes[index];
            }
            else
            {
                UnityEngine.Debug.Log("invalid index");
                return default(N);
            }
        }

        public bool IsNodePresent(int nodeIndex)
        {
            return nodeIndex < m_nodes.Count && nodeIndex >= 0 && m_nodes[nodeIndex] != null && m_nodes[nodeIndex].Index != GraphNode.INVALID_NODE_INDEX;
        }

        public bool IsEdgePresent(int from, int to)
        {
            if (IsNodePresent(from) && IsNodePresent(to))
            {
                for (int k = 0; k < m_edges[from].Count; k++)
                {
                    if (m_edges[from][k].To == to)
                    {
                        return true;
                    }
                }
                return false;
            }
            else return false;
        }

        public void RemoveEdge(int from, int to)
        {
            if (from < m_nodes.Count && from >= 0 && to < m_nodes.Count && to >= 0)
            {
                for (int k = 0; k < m_edgesTo[to].Count; k++)
                {
                    if (m_edgesTo[to][k].From == from)
                    {
                        m_edgesTo[to].RemoveAt(k);
                        break;
                    }
                }

                for (int k = 0; k < m_edges[from].Count; k++)
                {
                    if (m_edges[from][k].To == to)
                    {
                        m_edges[from].RemoveAt(k);
                        break;
                    }
                }
            }
        }

        public void RemoveNode(int nodeIndex)
        {
            if (IsNodePresent(nodeIndex))
            {
                m_nodes[nodeIndex].Index = GraphNode.INVALID_NODE_INDEX;
            }

            CullInvalidEdges();
        }

        public void Clear()
        {
            m_nodes.Clear();
            m_edges.Clear();
        }

        public void CullInvalidEdges()
        {
            for (int nodeIndex = 0; nodeIndex < m_edges.Count; nodeIndex++)
            {
                for (int index = 0; index < m_edges[nodeIndex].Count; index++)
                {
                    var curEdge = m_edges[nodeIndex][index];
                    if (m_nodes[curEdge.From].Index == GraphNode.INVALID_NODE_INDEX ||
                        m_nodes[curEdge.To].Index == GraphNode.INVALID_NODE_INDEX)
                    {
                        m_edges[nodeIndex].RemoveAt(index--);
                    }
                }
            }

            for (int nodeIndex = 0; nodeIndex < m_edgesTo.Count; nodeIndex++)
            {
                for (int index = 0; index < m_edgesTo[nodeIndex].Count; index++)
                {
                    var curEdge = m_edgesTo[nodeIndex][index];
                    if (m_nodes[curEdge.From].Index == GraphNode.INVALID_NODE_INDEX ||
                        m_nodes[curEdge.To].Index == GraphNode.INVALID_NODE_INDEX)
                    {
                        m_edgesTo[nodeIndex].RemoveAt(index--);
                    }
                }
            }
        }
    }
}

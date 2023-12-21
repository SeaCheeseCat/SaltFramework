using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.common.graph
{
    /// <summary>
    /// 图结构接口
    /// </summary>
    /// <typeparam name="N"></typeparam>
    /// <typeparam name="E"></typeparam>
    public interface IGraph<N,E> where N:INode where E: IEdge
    {
        N GetNode(int index);
        E GetEdge(int from, int to);

        void AddEdge(E edge);
        void RemoveEdge(int from, int to);

        int AddNode(N node);
        void RemoveNode(int nodeIndex);

        int NodesCount { get; }
        int EdgesCount { get; }

        IEnumerable<N> Nodes { get; }
        IEnumerable<E> Edges { get; }

        IEnumerable<E> GetEdgesByNode(int nodeIndex);

        bool IsDigraph { get; }
        bool IsEmpty { get; }

        bool IsNodePresent(int nodeIndex);
        bool IsEdgePresent(int from, int to);

        void Clear();
    }
}

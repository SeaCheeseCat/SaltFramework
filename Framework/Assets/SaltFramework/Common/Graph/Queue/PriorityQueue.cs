using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.queue
{
    /// <summary>
    /// 加权序列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> where T : IComparable
    {
        protected T[] m_heap;
        protected int m_count;
        protected int m_maxSize;

        public int Count { get { return m_count; } }
        public int MaxSize { get { return m_maxSize; } }

        public PriorityQueue(int maxSize)
        {
            m_heap = new T[maxSize + 1];
            m_maxSize = maxSize;
        }

        public bool IsEmtpy()
        {
            return m_count <= 0;
        }

        public void Insert(T item)
        {
            if (m_count + 1 <= m_maxSize)
            {
                ++m_count;
                m_heap[m_count] = item;

                ReorderUpwards(m_count);
            }
            else
            {
                throw new Exception("Queue is full!!");
            }
        }

        public T Pop()
        {
            Swap(1, m_count);
            ReorderDownwards(1, m_count - 1);
            return m_heap[m_count--];
        }

        public T Peek()
        {
            return m_heap[1];
        }

        /// <summary>
        /// 上浮
        /// </summary>
        /// <param name="heap"></param>
        /// <param name="node"></param>
        protected void ReorderUpwards(int node)
        {
            while ((node > 1) && (m_heap[node / 2].CompareTo(m_heap[node]) > 0))
            {
                Swap(node / 2, node);

                node /= 2;
            }
        }

        /// <summary>
        /// 下沉
        /// </summary>
        /// <param name="heap"></param>
        /// <param name="node"></param>
        /// <param name="heapSize"></param>
        protected void ReorderDownwards(int node, int heapSize)
        {
            while (2 * node <= heapSize)
            {
                int child = 2 * node;
                if ((child < heapSize) && (m_heap[child]).CompareTo(m_heap[child + 1]) > 0)
                {
                    ++child;
                }

                if (m_heap[node].CompareTo(m_heap[child]) > 0)
                {
                    Swap(node, child);
                    node = child;
                }
                else
                {
                    break;
                }
            }
        }

        protected void Swap(int x, int y)
        {
            T tmp = m_heap[x];
            m_heap[x] = m_heap[y];
            m_heap[y] = tmp;
        }
    }
}

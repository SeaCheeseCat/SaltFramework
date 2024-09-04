using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace echo.queue
{
    public class IndexedPriorityQueue
    {
        protected int[] m_heap;
        protected int m_count;
        protected int m_maxSize;

        private float[] m_priorityArray;
        private int[] m_invHeap;

        public int Count { get { return m_count; } }
        public int MaxSize { get { return m_maxSize; } }

        public IndexedPriorityQueue(float[] priorityArray)
        {
            m_maxSize = priorityArray.Length;
            m_heap = new int[m_maxSize + 1];

            m_invHeap = new int[m_maxSize + 1];
            m_priorityArray = priorityArray;
        }

        public bool IsEmtpy()
        {
            return m_count <= 0;
        }

        public void Insert(int item)
        {
            if (m_count + 1 <= m_maxSize)
            {
                ++m_count;
                m_heap[m_count] = item;
                m_invHeap[item] = m_count;

                ReorderUpwards(m_count);
            }
            else
            {
                throw new Exception("Queue is full!!");
            }
        }

        public int Pop()
        {
            Swap(1, m_count);
            ReorderDownwards(1, m_count - 1);
            return m_heap[m_count--];
        }

        public int Peek()
        {
            return m_heap[1];
        }

        public void ChangePriority(int item)
        {
            ReorderUpwards(m_invHeap[item]);
        }

        /// <summary>
        /// 上浮
        /// </summary>
        /// <param name="heap"></param>
        /// <param name="node"></param>
        protected void ReorderUpwards(int node)
        {
            while ((node > 1) && (m_priorityArray[m_heap[node / 2]].CompareTo(m_priorityArray[m_heap[node]]) > 0))
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
                if ((child < heapSize) && (m_priorityArray[m_heap[child]]).CompareTo(m_priorityArray[m_heap[child + 1]]) > 0)
                {
                    ++child;
                }

                if (m_priorityArray[m_heap[node]].CompareTo(m_priorityArray[m_heap[child]]) > 0)
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
            int tmp = m_heap[x];
            m_heap[x] = m_heap[y];
            m_heap[y] = tmp;

            m_invHeap[m_heap[x]] = x;
            m_invHeap[m_heap[y]] = y;
        }
    }
}

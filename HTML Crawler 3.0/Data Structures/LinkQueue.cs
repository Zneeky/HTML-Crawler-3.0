using System;

namespace HTML_Crawler_3._0.Data_Structures
{
    public class LinkQueue<T>
    {
        public NLinkedList<T> queue = new NLinkedList<T>();

        public void EnQueue(T element)
        {
            queue.AddFirst(element);
        }
        public T DeQueue()
        {
            var treeNode = queue.Last().Value;
            queue.Remove(queue.Last());
            return treeNode;
        }
        public bool IsEmpty()
        {
            if (queue.First() == null)
                return true;
            else
                return false;
        }
    }
}

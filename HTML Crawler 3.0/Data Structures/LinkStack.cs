using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Crawler_3._0.Data_Structures
{
    public class LinkStack<T>
    {
        public NLinkedList<T> stack = new NLinkedList<T>();

        public void Push(T treeNode)
        {
            stack.AddFirst(treeNode);
        }

        public T Pop()
        {
            var treeNode = stack.First().Value;
            stack.Remove(stack.First());
            return treeNode;
        }

        public bool IsEmpty()
        {
            if (stack.First() == null)
                return true;
            else
                return false;
        }

        public int Size()
        {
            return stack.Count();
        }
    }
}

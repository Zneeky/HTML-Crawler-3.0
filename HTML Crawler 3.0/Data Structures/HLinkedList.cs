using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace HTML_Crawler_3._0.Data_Structures
{
    public class Node
    {
        public HTreeNode Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }


    }
    public class HLinkedList<T> : IEnumerable<HTreeNode>
    {

        // null<-node1(head)<val,prev,next=node2> <-> node2<val1,prev=node1,next=node3> -> node3(tail)<val3, prev=node2,next> -> null


        private Node head, tail;

        public HLinkedList()
        {
            head = new Node();
            tail = head;
        }
        public IEnumerator<HTreeNode> GetEnumerator()
        {
            return new LinkedListEnumerator(head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public Node AddFirst(HTreeNode value)
        {
            var newNode = new Node
            {
                Value = value,
                PrevNode = null,
                NextNode = head
            };

            //If there already is a first element in the list,
            //change its prevNode to the newly made newNode,
            //otherwise it becomes both the tail and the head as the list is empty
            if (head != null)
                head.PrevNode = newNode;
            else
                tail = newNode;

            head = newNode;

            return newNode;
        }

        //Just adding an element
        public Node Add(HTreeNode value)
        {
            var newNode = new Node
            {
                Value = value,
                PrevNode = tail,
                NextNode = null
            };

            if (tail != null)
                tail.NextNode = newNode;
            else
                head = newNode;

            tail = newNode;

            return newNode;
        }

        public Node First() { return head; }
        public Node Last() { return tail; }
        public Node AddBefore(Node next, HTreeNode value)
        {
            if (next == null && head != null)
                throw new ArgumentNullException("next is null");

            var newNode = new Node
            {
                Value = value,
                PrevNode = next != null ? next.PrevNode : null,
                NextNode = next
            };
            if (head == null)
            {
                head = tail = newNode;
                return newNode;
            }
            if (next.PrevNode != null)
                next.PrevNode.NextNode = newNode;
            else
                head = newNode;

            next.PrevNode = newNode;
            return newNode;

        }

        public Node AddAfter(Node prev, HTreeNode value)
        {
            if (prev == null && tail != null)
                throw new ArgumentNullException("before is null");

            var newNode = new Node
            {
                Value = value,
                PrevNode = prev,
                NextNode = prev?.NextNode
            };

            if (tail == null)
            {
                head = tail = null;
                return newNode;
            }
            if (prev.NextNode != null)
                prev.NextNode.PrevNode = newNode;
            else
                tail = newNode;

            prev.NextNode = newNode;

            return newNode;

        }

        public void Remove(Node node)
        {
            if (node.NextNode != null)
                node.NextNode.PrevNode = node.PrevNode;

            else
                tail = node.PrevNode;

            if (node.PrevNode != null)
                node.PrevNode.NextNode = node.NextNode;

            else
                head = node.NextNode;
        }
        public bool Search(Node head, HTreeNode value)
        {
            Node current = head; // Initialize current
            while (current != null)
            {
                if (current.Value.Tag.Equals(value))//Maybe CHEAT
                    return true; // data found
                current = current.NextNode;
            }
            return false; // data not found
        }
    }

    public class LinkedListEnumerator : IEnumerator<HTreeNode>
    {
        private Node current;

        public LinkedListEnumerator(Node current)
        {
            this.current = current;
        }

        public HTreeNode Current { get { return current.Value; } }

        object IEnumerator.Current { get { return Current; } }

        public bool MoveNext()
        {
            if (current == null) return false;
            current = current.NextNode;
            return (current != null);
        }

        public void Dispose()
        {
           GC.Collect();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}

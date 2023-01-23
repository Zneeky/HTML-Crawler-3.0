using System;

namespace HTML_Crawler_3._0.Data_Structures
{
    public class HashNode<T>
    {
        public HashNode<T> Next { get; set; }

        public string Key { get; set; }

        public T Value { get; set; }
    }
    class Hashtable<T>
    {
        private readonly HashNode<T>[] _buckets;

        public Hashtable(int size)
        {
            _buckets = new HashNode<T>[size];
        }

        public T Get(string key)
        {

            var node = MyGetNodeByKey(key);
            if (node == null) return default(T);

            return node.Value;
        }

        public void Add(string key, T item)
        {


            var valueNode = new HashNode<T> { Key = key, Value = item, Next = null };
            int position = GetBucketByKey(key);
            HashNode<T> listNode = _buckets[position];

            if (null == listNode)
            {
                _buckets[position] = valueNode;
            }
            else
            {
                while (null != listNode.Next)
                {
                    listNode = listNode.Next;
                }
                listNode.Next = valueNode;
            }

        }

        public bool Remove(string key)
        {

            int position = GetBucketByKey(key);
            var (previous, currnet) = GetNodeByKey(key);
            if (null == currnet) return false;
            if (null == previous)
            {
                _buckets[position] = null;
                return true;
            }
            previous.Next = currnet.Next;
            return true;
        }


        public int GetBucketByKey(string key)
        {
            int total = 0;
            for (int k = 0; k < key.Length; k++)
            {
                total += (int)key[k];
            }

            return total % _buckets.Length;
        }

        protected HashNode<T> MyGetNodeByKey(string key)
        {
            int position = GetBucketByKey(key);
            HashNode<T> listNode = _buckets[position];

            while (null != listNode)
            {
                if (listNode.Key == key) return (listNode);
                listNode = listNode.Next;
            }

            return null;
        }
        protected (HashNode<T> previous, HashNode<T> current) GetNodeByKey(string key)
        {
            int position = GetBucketByKey(key);
            HashNode<T> listNode = _buckets[position];
            HashNode<T> previous = null;

            while (null != listNode)
            {
                if (listNode.Key == key) return (previous, listNode);
                previous = listNode;
                listNode = listNode.Next;
            }

            return (null, null);
        }

    }
}

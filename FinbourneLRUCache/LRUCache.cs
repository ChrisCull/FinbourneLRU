using System.Collections.Generic;
using System.Xml.Linq;

namespace FinbourneLRUCache
{
    public class LRUCache<K, V>
    {
        public int capacity;
        private Dictionary<K, LinkedListNode<Node<K, V>>> map = new Dictionary<K, LinkedListNode<Node<K, V>>>();
        private LinkedList<Node<K, V>> lruLinkedList = new LinkedList<Node<K, V>>();
        private readonly object cacheLock = new object();

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
        }

        public V? get(K key)
        {
            try
            {
                LinkedListNode<Node<K, V>> node;

                lock (cacheLock)
                {

                    // try get the value associated with key passed in
                    if (map.TryGetValue(key, out node))
                    {
                        // if a value is already associated with this key remove it from the start of the cache and put it at the end (most recently used)
                        V val = node.Value.value;
                        lruLinkedList.Remove(node);
                        lruLinkedList.AddLast(node);

                        return val;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return default;
        }

        public void put(K key, V value)
        {
            try
            {
                LinkedListNode<Node<K, V>> node;

                lock (cacheLock)
                {
                    // if value is already in cache, reorder the cache
                    if (map.TryGetValue(key, out node))
                    {
                        lruLinkedList.Remove(node);
                    }

                    // if the cache is full, remove the first element in the cache
                    if (capacity == map.Count)
                    {
                        var firstElement = lruLinkedList.First.Value.key;
                        map.Remove(firstElement);
                        lruLinkedList.RemoveFirst();
                    }

                    Node<K, V> newItem = new Node<K, V>(key, value);
                    node = new LinkedListNode<Node<K, V>>(newItem);

                    // add the new node to the end of the cache
                    lruLinkedList.AddLast(node);
                    map[key] = node;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        class Node<K, V>
        {
            // node object, represents each item within the cache
            public Node (K key, V value)
            {
                this.key = key;
                this.value = value;
            }

            public K key;
            public V value;
        }

        public int CurrentCacheSize
        {
            get { return lruLinkedList.Count; }
        }
    }
}

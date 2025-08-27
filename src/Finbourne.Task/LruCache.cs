using System.Runtime.CompilerServices;

namespace Finbourne.Task;

public class LRUCache<TKey, TValue>
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> _cacheMap;
    private readonly LinkedList<KeyValuePair<TKey, TValue>> _lruList;

    public LRUCache(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
        }

        _capacity = capacity;
        _cacheMap = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>();
        _lruList = new LinkedList<KeyValuePair<TKey, TValue>>();
    }

    public TValue? GetOrDefault(TKey key)
    {
        if (_cacheMap.TryGetValue(key, out LinkedListNode<KeyValuePair<TKey, TValue>> node))
        {
            _lruList.Remove(node);
            _lruList.AddFirst(node); // Move the most recently used item to the front
            return node.Value.Value;
        }
        return default;
    }

    public void Set(TKey key, TValue value)
    {
        if (_cacheMap.TryGetValue(key, out var existingNode))
        {
            _lruList.Remove(existingNode);
            _cacheMap.Remove(key);
        }

        var newNode = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
        _lruList.AddFirst(newNode);
        _cacheMap[key] = newNode;


        if (_cacheMap.Count > _capacity)
        {
            RemoveFirst();
        }

    }

    private void RemoveFirst()
    {
        var lastNode = _lruList.Last;
        if (lastNode != null)
        {
            _cacheMap.Remove(lastNode.Value.Key);
            _lruList.RemoveLast();
        }
    }

}
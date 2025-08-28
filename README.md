This project implements a **Least Recently Used (LRU) cache** in C#. 

The cache is generic (LRUCache<TKey, TValue>), reusable, and easy to integrate into systems that require efficient memory management and fast data access.
The cache stores key-value pairs and automatically evicts the least recently used item when its capacity is exceeded. 
It also has a notification mechanism for evictions, making it suitable for scenarios where cache needs to be monitored.

Trade-offs / future improvements have been noted (for a real-world scenario) 

Approximate time spent: 3-4 hours.

using Xunit;
using System.Collections.Generic;
using LRUCache;

namespace LRUCache.Tests;

public class LRUCacheTests
{
    [Fact]
    public void ReturnsCorrectValue()
    {
        var cache = new LRUCache<string, string>(1);
        cache.Set("A", "Apple");

        var result = cache.GetOrDefault("A");

        Assert.Equal("Apple", result);
    }

    [Fact]
    public void UpdatesExistingKey()
    {
        var cache = new LRUCache<string, string>(2);
        // Should update value from "Equity" to "Bond"
        cache.Set("A", "Apple");
        cache.Set("A", "Banana"); 

        var result = cache.GetOrDefault("A");

        Assert.Equal("Banana", result);
    }

    [Fact]
    public void EvictsLRUItem()
    {
        var cache = new LRUCache<string, string>(2);
        // Should evict "ID123"
        cache.Set("A", "Apple");
        cache.Set("B", "Banana");
        cache.Set("C", "Cherry"); 

        Assert.Null(cache.GetOrDefault("A"));
        Assert.Equal("Banana", cache.GetOrDefault("B"));
        Assert.Equal("Cherry", cache.GetOrDefault("C"));
    }

    [Fact]
    public void CountNumberOfItems()
    {
        var cache = new LRUCache<string, string>(2);
         // Should be count = empty initially, then after adding third item, count = 2
       
        Assert.Equal(0, cache.Count);

        cache.Set("A", "Apple");
        Assert.Equal(1, cache.Count);

        cache.Set("B", "Banana");
        Assert.Equal(2, cache.Count);

        cache.Set("C", "Cherry");
        Assert.Equal(2, cache.Count); 
    }

    [Fact]
    public void ThrowsInvalidCapacity()
    {
        Assert.Throws<ArgumentException>(() => new LRUCache<string, string>(0));
        Assert.Throws<ArgumentException>(() => new LRUCache<string, string>(-1));
    }
}

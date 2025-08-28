using Xunit;
using System.Collections.Generic;
using Finbourne.Task;

namespace Finbourne.Task.Tests;

public class LRUCacheTests
{
    [Fact]
    public void ReturnsCorrectValue()
    {
        var cache = new LRUCache<string, string>(1);
        cache.Set("ID123", "Equity");

        var result = cache.GetOrDefault("ID123");

        Assert.Equal("Equity", result);
    }

    [Fact]
    public void UpdatesExistingKey()
    {
        var cache = new LRUCache<string, string>(2);
        // Should update value from "Equity" to "Bond"
        cache.Set("ID123", "Equity");
        cache.Set("ID123", "Bond"); 

        var result = cache.GetOrDefault("ID123");

        Assert.Equal("Bond", result);
    }

    [Fact]
    public void EvictsLRUItem()
    {
        var cache = new LRUCache<string, string>(2);
        // Should evict "ID123"
        cache.Set("ID123", "Equity");
        cache.Set("ID456", "Bond");
        cache.Set("ID789", "Fund"); 

        Assert.Null(cache.GetOrDefault("ID123"));
        Assert.Equal("Bond", cache.GetOrDefault("ID456"));
        Assert.Equal("Fund", cache.GetOrDefault("ID789"));
    }

    [Fact]
    public void CountNumberOfItems()
    {
        var cache = new LRUCache<string, string>(2);
         // Should be count = empty initially, then after adding third item, count = 2
       
        Assert.Equal(0, cache.Count);

        cache.Set("ID123", "Equity");
        Assert.Equal(1, cache.Count);

        cache.Set("ID456", "Bond");
        Assert.Equal(2, cache.Count);

        cache.Set("ID789", "Fund");
        Assert.Equal(2, cache.Count); 
    }

    [Fact]
    public void ThrowsInvalidCapacity()
    {
        Assert.Throws<ArgumentException>(() => new LRUCache<string, string>(0));
        Assert.Throws<ArgumentException>(() => new LRUCache<string, string>(-1));
    }
}

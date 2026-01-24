using System;
using System.Collections.Generic;

/// <summary>
/// Implements a priority queue where higher priority items are dequeued first.
/// If multiple items have the same highest priority, the one that was added first (FIFO) is removed first.
/// </summary>
public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.
    /// Always added to the back of the queue regardless of priority.
    /// </summary>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode);
    }

    /// <summary>
    /// Remove and return the value with the highest priority.
    /// If multiple items share the highest priority, remove the first one (FIFO).
    /// Throws InvalidOperationException if queue is empty.
    /// </summary>
    public string Dequeue()
    {
        if (_queue.Count == 0)
            throw new InvalidOperationException("The queue is empty.");

        // Start by assuming the first item is the highest priority
        int highPriorityIndex = 0;

        // Search through the queue to find the highest priority item
        for (int i = 1; i < _queue.Count; i++)
        {
            // Only replace if strictly higher priority (FIFO tie-breaker)
            if (_queue[i].Priority > _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = i;
            }
        }

        // Save the value to return
        var value = _queue[highPriorityIndex].Value;

        // Remove the item from the queue
        _queue.RemoveAt(highPriorityIndex);

        return value;
    }

    // DO NOT MODIFY
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

/// <summary>
/// Internal class representing a value with priority
/// </summary>
internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    // DO NOT MODIFY
    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}

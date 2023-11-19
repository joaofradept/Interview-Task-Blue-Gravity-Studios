using System.Collections.Generic;
using UnityEngine;

public class Inventory<T>
{
    [SerializeField] protected List<T> list;

    int itemLimit;

    public Inventory(T[] items, int itemLimit = 0)
    {
        list = new List<T>(items);

        this.itemLimit = itemLimit;
    }

    public bool AddToList(T item)
    {
        // If item has reached limit,
        // leave method and return false
        if (itemLimit > 0 && list.Count == itemLimit)
            return false;

        list.Add(item);

        return true;
    }

    public void RemoveFromList(T item)
    {
        list.Remove(item);
    }

    public List<T> List
    {
        get { return list; }
    }
}

using System.Threading;
using UnityEngine;

public class InventoryItem
{
    public readonly string name;
    public int count;

    public InventoryItem(string newName, int newCount = 1)
    {
        name = newName;
        count = newCount;
    }

    public void addCount(int increment)
    {
        count += increment;
    }

    public void removeCount(int decrement)
    {
        count -= decrement;
    }
}

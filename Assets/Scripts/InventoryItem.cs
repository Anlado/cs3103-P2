using UnityEngine;

public class InventoryItem
{
    public readonly string name;
    public int count;

    public InventoryItem(string newName)
    {
        name = newName;
        count = 1;
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

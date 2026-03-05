using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    List<InventoryItem> Inventory;

    Stats playerStats;

    void Start()
    {
        Inventory = new List<InventoryItem>();
        playerStats = new Stats(100f);
    }

    private bool addItem(InventoryItem item)
    {
        if (Inventory.Count < 12)
        {
            foreach (InventoryItem inv in Inventory)
            {
                if (inv.name == item.name)
                {
                    inv.addCount(item.count);
                    return true;
                }
            }

            Inventory.Add(item);
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "SmallHealth")
        {
            if (addItem(new InventoryItem("SmallHealth")))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
            }
        }
        else if (collision.gameObject.tag == "MedHealth")
        {
            if (addItem(new InventoryItem("MedHealth")))
            {
                Destroy(collision.gameObject);
                printInventoryContents();

            }
        }
        else if (collision.gameObject.tag == "LargeHealth")
        {
            if (addItem(new InventoryItem("LargeHealth")))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
            }
        }
    }

    private void useItem(string itemName)
    {
        switch (itemName)
        {
            case "SmallHealth":
                playerStats.changeHealth(10);
                break;
            case "MedHealth":
                playerStats.changeHealth(20);
                break;
            case "LargeHealth":
                playerStats.changeHealth(30);
                break;
        }
    }



    private void printInventoryContents()
    {
        foreach (InventoryItem item in Inventory)
        {
            print(item.name);
        }
    }
}

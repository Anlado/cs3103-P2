using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    List<InventoryItem> Inventory;
    public Button[] invButtons;
    public Button[] QSButtons;
    private InventoryItem[] QSPointers;

    private bool quickSlotSelection;
    private int tempButtonID;

    Stats playerStats;

    public TextMeshProUGUI invText;

    void Start()
    {
        QSPointers = new InventoryItem[4];
        System.Array.Fill(QSPointers, null);
        Inventory = new List<InventoryItem>();
        playerStats = new Stats(100f);
        updateInvUI();
        quickSlotSelection = false;
        tempButtonID = -1;
        clearText();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            QSButtonPress(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            QSButtonPress(1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            QSButtonPress(2);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            QSButtonPress(3);

        }
        */

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
                updateInvUI();

            }
        }
        else if (collision.gameObject.tag == "MedHealth")
        {
            if (addItem(new InventoryItem("MedHealth")))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();

            }
        }
        else if (collision.gameObject.tag == "LargeHealth")
        {
            if (addItem(new InventoryItem("LargeHealth")))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();

            }
        }
        else
        {
            invText.text = "Inventory Full";
            CancelInvoke(nameof(clearText)); 
            Invoke(nameof(clearText), 2f);
        }
    }

    private void clearText()
    {
        invText.text = "";
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

    private void updateInvUI()
    {
        //Update Inv UI
        for (int i = 0; i < 12; i++)
        {
            TextMeshProUGUI tmp = invButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (i < 6)
            {
                if (i >= Inventory.Count)
                {
                    tmp.text = "Empty";
                }
                else
                {
                    tmp.text = Inventory[i].name + ": " + Inventory[i].count;
                }
            }
            else
            {
                if (i >= Inventory.Count)
                {
                    tmp.text = "Empty";
                    invButtons[i].gameObject.SetActive(false);
                }
                else
                {
                    tmp.text = Inventory[i].name + ": " + Inventory[i].count;
                    invButtons[i].gameObject.SetActive(true);
                }
            }
        }

        //Update QS UI
        for (int i = 0; i < 4; i++)
        {
            TextMeshProUGUI QStmp = QSButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            if (QSPointers[i] == null)
            {
                QStmp.text = "QS: " + (i+1);
            }
            else
            {
                string itemName = QSPointers[i].name;
                int itemCount = QSPointers[i].count;
                QStmp.text = itemName + ": " + itemCount;
            }
        }
    }

    public void startQuickSelect()
    {
        quickSlotSelection = !quickSlotSelection;

        if (quickSlotSelection)
        {
            invText.text = "Press Inventory Slot then press QuickSelect slot";
            tempButtonID = -1;
        }
        else
        {
            clearText();
        }
    }

    public void invButtonPress(int buttonID)
    {
        if (!quickSlotSelection)
        {
            if (buttonID >= Inventory.Count)
            {
                return;
            }

            useItem(Inventory[buttonID].name);

            InventoryItem item = Inventory[buttonID];
            if (item.count <= 1)
            {
                Inventory.Remove(item);
            }
            else
            {
                item.count--;
            }
            updateInvUI();
            Debug.Log(item.name);
        }
        else
        {
            tempButtonID = buttonID;
        }
    }

    public void QSButtonPress(int QSButtonID)
    {
        if (!quickSlotSelection)
        {
            if (QSPointers[QSButtonID] == null)
            {
                return;
            }

            int buttonID = Inventory.IndexOf(QSPointers[QSButtonID]);
            invButtonPress(buttonID);

            if (!Inventory.Contains(QSPointers[QSButtonID]))
            {
                QSPointers[QSButtonID] = null;
            }
        }
        else
        {
            if (tempButtonID < Inventory.Count)
            {
                QSPointers[QSButtonID] = Inventory[tempButtonID];
            }

            quickSlotSelection = !quickSlotSelection;
            clearText();
        }

        updateInvUI();
    }

}
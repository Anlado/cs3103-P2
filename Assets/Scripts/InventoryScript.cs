using System.Collections.Generic;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    List<InventoryItem> Inventory;
    public Button[] invButtons;
    public Button[] QSButtons;
    private InventoryItem[] QSPointers;

    private bool quickSlotSelection;
    private int tempButtonID;

    public TextMeshProUGUI invText;
    public TextMeshProUGUI FullText;

    public GameObject player;
    StatsManager statsManager;

    void Start()
    {
        FullText.text = "";
        statsManager = player.GetComponent<StatsManager>();
        QSPointers = new InventoryItem[4];
        System.Array.Fill(QSPointers, null);
        Inventory = new List<InventoryItem>();
        updateInvUI();
        quickSlotSelection = false;
        tempButtonID = -1;
        clearText();
    }

    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            QSButtonPress(0);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            QSButtonPress(1);

        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            QSButtonPress(2);

        }
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            QSButtonPress(3);

        }
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

    private void OnTriggerStay(Collider collision)
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
        else if (collision.gameObject.tag == "FullHealth")
        {
            if (addItem(new InventoryItem("FullHealth")))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();

            }
        }
        else if (collision.gameObject.tag == "LargShotPickup")
        {
            if (addItem(new InventoryItem("LargeShot", 5)))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();
            }
        }
        else if (collision.gameObject.tag == "MedShotPickup")
        {
            if (addItem(new InventoryItem("MediumShot", 5)))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();
            }
        }
        else if (collision.gameObject.tag == "SmallShotPickup")
        {
            if (addItem(new InventoryItem("SmallShot", 5)))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();
            }
        }
        else if (collision.gameObject.tag == "LargeSpeed")
        {
            if (addItem(new InventoryItem("LargeSpeed", 1)))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();
            }
        }
        else if (collision.gameObject.tag == "MedSpeed")
        {
            if (addItem(new InventoryItem("MediumSpeed", 1)))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();
            }
        }
        else if (collision.gameObject.tag == "SmallSpeed")
        {
            if (addItem(new InventoryItem("SmallSpeed", 1)))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();
            }
        }
        else if (collision.gameObject.tag == "ShotgunPickup")
        {
            if (addItem(new InventoryItem("Shotgun", 5)))
            {
                Destroy(collision.gameObject);
                printInventoryContents();
                updateInvUI();
            }
        }
        else
        {
            if (Inventory.Count >= 12)
            {
                invText.text = "Inventory Full";
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        FullText.text = "";
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
                {
                    statsManager.GetComponent<StatsManager>().changeHealth(10);
                    statsManager.changeItemsUsed(1);
                    break;
                }
            case "MedHealth":
                {
                    statsManager.GetComponent<StatsManager>().changeHealth(20);
                    statsManager.changeItemsUsed(1);
                    break;
                }
            case "LargeHealth":
                {
                    statsManager.GetComponent<StatsManager>().changeHealth(30);
                    statsManager.changeItemsUsed(1);
                    break;
                }
            case "FullHealth":
                {
                    statsManager.GetComponent<StatsManager>().changeHealth(100);
                    statsManager.changeItemsUsed(1);
                    break;
                }
            case "LargeSpeed":
                {
                    player.GetComponent<MovementScript>().ApplySpeedBoost(20);
                    statsManager.changeItemsUsed(1);
                    break;
                }
            case "MediumSpeed":
                {
                    player.GetComponent<MovementScript>().ApplySpeedBoost(15);
                    statsManager.changeItemsUsed(1);
                    break;
                }
            case "SmallSpeed":
                {
                    player.GetComponent<MovementScript>().ApplySpeedBoost(10);
                    statsManager.changeItemsUsed(1);
                    break;
                }
            case "LargeShot":
                {
                    //Usage of Resources.Load
                    GameObject projectilePrefab = Resources.Load<GameObject>("BigShot");
                    GameObject instantiatedProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    Rigidbody rb = instantiatedProjectile.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = transform.forward * 5;
                    }

                    Destroy(instantiatedProjectile, 5f);
                    statsManager.changeItemsUsed(1);
                    statsManager.changeShotsFired(1);
                    break;
                }
            case "MediumShot":
                {
                    GameObject projectilePrefab = Resources.Load<GameObject>("MedShot");
                    GameObject instantiatedProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    Rigidbody rb = instantiatedProjectile.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = transform.forward * 10;
                    }

                    Destroy(instantiatedProjectile, 5f);
                    statsManager.changeItemsUsed(1);
                    statsManager.changeShotsFired(1);
                    break;
                }
            case "SmallShot":
                {
                    GameObject projectilePrefab = Resources.Load<GameObject>("SmallShot");
                    GameObject instantiatedProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    Rigidbody rb = instantiatedProjectile.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = transform.forward * 15;
                    }

                    Destroy(instantiatedProjectile, 5f);
                    statsManager.changeItemsUsed(1);
                    statsManager.changeShotsFired(1);
                    break;
                }
            case "Shotgun":
                {
                    GameObject prefab = Resources.Load<GameObject>("ShotgunPellet");
                    if (prefab == null) return;

                    int pelletCount = 6;
                    float spreadAngle = 15f;
                    float projectileSpeed = 20f;

                    for (int i = 0; i < pelletCount; i++)
                    {
                        Quaternion rot = transform.rotation * Quaternion.Euler(
                            Random.Range(-spreadAngle, spreadAngle),
                            Random.Range(-spreadAngle, spreadAngle),
                            0f
                        );

                        GameObject proj = Instantiate(prefab, transform.position + transform.forward, rot);

                        Rigidbody rb = proj.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.useGravity = false;
                            rb.linearVelocity = proj.transform.forward * projectileSpeed;
                        }

                        Destroy(proj, 5f);
                    }
                    statsManager.changeItemsUsed(1);
                    statsManager.changeShotsFired(1);
                    break;
                }
                
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
            Debug.Log(tempButtonID);
            if (tempButtonID != -1 && tempButtonID < Inventory.Count)
            {
                QSPointers[QSButtonID] = Inventory[tempButtonID];
                tempButtonID = -1;
            }
            quickSlotSelection = !quickSlotSelection;
            clearText();
        }

        updateInvUI();
    }

}
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    StatsManager statsmanager;
    public GameObject player;

    void Start()
    {
        statsmanager = player.GetComponent<StatsManager>();
    }

    void Update()
    {
        healthText.text = "Health: " + statsmanager.currentHealth.ToString();
        healthBar.value = statsmanager.currentHealth / statsmanager.maxHealth;
    }
}

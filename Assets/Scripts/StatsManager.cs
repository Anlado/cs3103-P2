using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class StatsManager : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float totalTime;
    public int itemsUsed;
    public int shotsFired;
    public int timesHit;

    public TextMeshProUGUI timePlayedText;
    public TextMeshProUGUI shotsFiredText;
    public TextMeshProUGUI itemsUsedText;
    public TextMeshProUGUI timeHitText;


    private void Awake()
    {
        itemsUsed = 0;
        totalTime = 0;
        shotsFired = 0;
        timesHit = 0;

        currentHealth = maxHealth;
        loadPrefs();
    }

    void Update()
    {
        totalTime += Time.deltaTime;
        string formatedTime = formatTime(totalTime);

        timePlayedText.text = "Time Played: " + formatedTime;
        shotsFiredText.text = "Shots Fired: " + shotsFired.ToString();
        itemsUsedText.text = "Items Used: " + itemsUsed.ToString();
        timeHitText.text = "Times Hit: " + timesHit.ToString();
    }

    string formatTime(float time)
    {
        int hours = (int)(time / 3600);
        int minutes = (int)((time % 3600) / 60);
        int seconds = (int)(time % 60);
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    public void changeHealth(float change)
    {
        currentHealth += change;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void changeItemsUsed(int change)
    {
        itemsUsed += change;
    }

    public void changeShotsFired(int change)
    {
        shotsFired += change;
    }

    public void changeTimesHit(int change)
    {
        timesHit += change;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyShot")
        {
            changeHealth(-10f);
        }
    }


    public void savePrefs()
    {
        PlayerPrefs.SetFloat("totalTime", totalTime);
        PlayerPrefs.SetInt("itemsUsed", itemsUsed);
        PlayerPrefs.SetInt("shotsFired", shotsFired);
        PlayerPrefs.SetInt("timesHit", timesHit);
        PlayerPrefs.Save();
    }

    void loadPrefs()
    {
        totalTime = PlayerPrefs.GetFloat("totalTime", 0f);
        itemsUsed = PlayerPrefs.GetInt("itemsUsed", 0);
        shotsFired = PlayerPrefs.GetInt("shotsFired", 0);
        timesHit = PlayerPrefs.GetInt("timesHit", 0);
    }
}

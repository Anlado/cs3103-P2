using UnityEngine;

public class Stats
{
    float maxHealth; 
    float health;

    public Stats(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void changeHealth(float healthChange)
    {
        health += healthChange;
        health = Mathf.Clamp(health, 0, 100);
    }


}

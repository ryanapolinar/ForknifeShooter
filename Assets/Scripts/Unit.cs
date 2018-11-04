using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    const int DEFAULT_HEALTH = 3;
    private int health;
    private int maxHealth;

    public Unit()
    {
        maxHealth = DEFAULT_HEALTH;
        health = DEFAULT_HEALTH;
        
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    protected const int DEFAULT_HEALTH = 3;
    protected int health;
    protected int maxHealth;

    protected const int DEFAULT_DAMAGE = 1;
    protected int damage;

    public Unit()
    {
        maxHealth = DEFAULT_HEALTH;
        health = DEFAULT_HEALTH;
        damage = DEFAULT_DAMAGE;
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

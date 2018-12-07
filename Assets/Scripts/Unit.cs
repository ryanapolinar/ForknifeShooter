using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    protected const int DEFAULT_HEALTH = 3;
    protected int health;
    protected int maxHealth;

    protected const int DEFAULT_DAMAGE = 1;
    protected int damage;

    virtual protected void Start()
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

    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public void setMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public int getDamage()
    {
        return this.damage;
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    virtual public void Damage(int damage)
    {
        health -= damage;
        Debug.Log("health: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

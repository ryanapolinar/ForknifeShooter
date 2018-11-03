using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    const int DEFAULT_HEALTH = 3;
    private int health;

    public Unit()
    {
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

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

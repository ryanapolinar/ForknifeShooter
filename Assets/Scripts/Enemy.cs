using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit {

    Rigidbody2D rb;

    public GameObject healthBar;
    

	void Start () {
        rb = GetComponent<Rigidbody2D>();      
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public Vector3 GetWorldToScreenPoint()
    {
        Vector3 enemyScreenPosition = Camera.main.WorldToScreenPoint(rb.position);
        return enemyScreenPosition;
    }

    public GameObject CreateHealthBar()
    {
        GameObject hb = Instantiate(healthBar, transform);
        hb.GetComponent<Slider>().maxValue = this.getMaxHealth();
        return hb;
    }
    
}

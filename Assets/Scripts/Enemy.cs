using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit {

    Rigidbody2D rb;

    void Start () {
        rb = GetComponent<Rigidbody2D>();      
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                // Damage the player
                Player player = collision.gameObject.GetComponent<Player>();
                if (player.getInvincibility() <= 0)
                {
                    player.Damage(this.damage);

                    // Activate player's invincibility frames
                    player.setInvincibility(player.getInvincibilityFrameMax());
                    
                }

                break;
        }
    }

}

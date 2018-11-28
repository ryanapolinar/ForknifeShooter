using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile {

	// Use this for initialization
	override protected void Start () {
        base.Start();
        totalSpeed = 10f;
	}

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Player":
                Player player = collision.gameObject.GetComponent<Player>();
                player.Damage(this.damage);
                Destroy(gameObject);
                break;
        }
    }
}

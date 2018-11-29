using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Unit {

    public const float DEFAULT_DETECTION_RADIUS = 15f;
    protected float detectionRadius;
    bool detected;

    protected Rigidbody2D rb;
    

    override protected void Start () {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        this.detectionRadius = DEFAULT_DETECTION_RADIUS;
        this.detected = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                // Damage the player
                Player player = collision.gameObject.GetComponent<Player>();
                if (player.getInvincibility() <= 0)
                {
                    player.Damage(this.damage);
                }
                break;
        }
    }

    /**
     * Returns a Vector2 of the distance to player, and updates if the player is detected
     */
    public Vector2 DirectionToPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector2 playerPosition2D = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 enemyPosition2D = new Vector2(this.transform.position.x, this.transform.position.y);
        this.detected = Mathf.Abs(Vector2.Distance(playerPosition2D, enemyPosition2D)) <= this.detectionRadius;

        return playerPosition2D - enemyPosition2D;
    }

    /**
     * Returns true if the Player is detected
     */
    public bool PlayerDetected()
    {
        DirectionToPlayer();
        return detected;
    }

}

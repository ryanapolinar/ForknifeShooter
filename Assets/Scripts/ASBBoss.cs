using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASBBoss : Enemy
{

    public int shootCooldown = 0;
    public int shootCooldownMax = 10;
    public int dashCooldown = 0;
    public int dashCooldownMax = 180;
    public int generalCooldown = 0;
    public int generalCooldownMax = 180;
    public int numShot = 0;
    public float randomNum;
    private GameObject player;
    private Vector2 playerPos;
    Vector2 movementVelocity;

    public EnemyProjectile enemyProjectile;

    protected override void Start()
    {
        base.Start();
        detectionRadius = 10f;
        health = 30;
        maxHealth = 30;
    }

    private void Update()
    {
        // Update shootCoolodown
        if (shootCooldown > 0)
        {
            shootCooldown--;
        }
        // Update dashCooldown
        if (dashCooldown > 0)
        {
            dashCooldown--;
        }

        if (generalCooldown > 0)
        {
            generalCooldown--;
        }
    }

    void FixedUpdate()
    {
        // SHOOT
        randomNum = Random.Range(0.0f, 1.0f);
        if (randomNum < 0.5f && generalCooldown <= 0)
        {
            Shoot();
        }

        else if (randomNum >= 0.5f && generalCooldown <= 0)
        {
            Dash();
        }
    }

    private void Shoot()
    {
        if (this.PlayerDetected() && shootCooldown <= 0)
        {
            // Generate a vector from the enemy to the player
            Vector2 randomizer = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            Vector2 projectileVector = (DirectionToPlayer() + randomizer).normalized * enemyProjectile.totalSpeed;

            // Create the enemy projectile
            EnemyProjectile newProjectile = Instantiate(enemyProjectile);
            newProjectile.transform.position = this.transform.position;
            newProjectile.setSpeed(projectileVector.x, projectileVector.y);


            // Reset the cooldown
            shootCooldown = shootCooldownMax;
            numShot++;

            if(numShot >= 3)
            {
                shootCooldown = 180;
                numShot = 0;
            }
            
        }
    }

    private void Dash()
    {
        if (this.PlayerDetected() && dashCooldown <= 0)
        {
            player = GameObject.Find("Player");
            playerPos = player.transform.position;
            this.movementVelocity = this.DirectionToPlayer().normalized * 40;
            rb.MovePosition(rb.position + movementVelocity * Time.deltaTime);
            //rb.MovePosition(playerPos + movementVelocity * Time.deltaTime); // teleports to player
        }
    }

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        // Damage player and reset chaseCooldown

        if (collision.tag == "Player")
        {
            base.OnTriggerEnter2D(collision);
            dashCooldown = dashCooldownMax;
        }
    }
}

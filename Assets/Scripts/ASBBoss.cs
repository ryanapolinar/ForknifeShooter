using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASBBoss : Enemy
{

    public int shootCooldown = 0;
    public int shootCooldownMax = 10;
    public int generalCooldown = 0;
    public int generalCooldownMax = 180;
    public int numShot = 0;

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

        // Update generalCooldown
        //if (generalCooldown > 0)
        //{
        //    generalCooldown--;
        //}
    }

    void FixedUpdate()
    {
        // SHOOT
            Shoot();
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

}

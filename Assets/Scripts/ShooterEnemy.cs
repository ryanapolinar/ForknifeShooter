using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{

    public int shootCooldown = 0;
    public int shootCooldownMax = 180;

    public EnemyProjectile enemyProjectile;

    protected override void Start()
    {
        base.Start();
        detectionRadius = 10f;
    }

    private void Update()
    {
        // Update shootCoolodown
        if (shootCooldown > 0)
        {
            shootCooldown--;
        }
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
        }
    }
}

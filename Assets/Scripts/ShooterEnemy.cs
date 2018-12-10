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
        detectionRadius = 12f;
    }

    private void Update()
    {
        //frozen code
        if (isFrozen)
        {
            this.enabled = false; //supposed to freeze
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(FreezeTimer()); //wait few seconds, remove freeze

            rb.constraints = RigidbodyConstraints2D.None;
        }

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

    IEnumerator FreezeTimer()
    {
        Debug.Log("Frozen");
        yield return new WaitForSeconds(2.0f);
        this.enabled = true;
        isFrozen = false;
    }
}

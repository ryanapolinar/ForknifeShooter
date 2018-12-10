using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASBBoss : Enemy {

    public int shootCooldown = 0;

    public int rapidFireCount = 1;
    public int rapidFireCountMax = 5;
    public int rapidFireCooldownMax = 5;
    public bool isFiring = false;

    public int generalCooldown = 0;
    public int generalCooldownMax = 120;

    public EnemyProjectile enemyProjectile;

    public GameObject WallGrid;
    public GameObject ContinueGrid;

    protected override void Start()
    {
        base.Start();

        health = 100 * 2;
        maxHealth = 100 * 2;

        isFiring = false;
        rapidFireCount = 1;

        generalCooldown = generalCooldownMax;
    }

    private void Update()
    {
        // Update generalCooldown
        if (generalCooldown > 0 && !isFiring)
        {
            generalCooldown--;
        }

        // Update shootCoolodown
        if (shootCooldown > 0 && isFiring)
        {
            shootCooldown--;
        }
        
    }

    void FixedUpdate()
    {
        // Basic AI and move selection
        if (generalCooldown == 0)
        {
            float action = Random.Range(0.0f, 1.0f);
            if (action < 0.5f || isFiring)
            {
                Shoot();
            }
            else if (action >= 0.5f)
            {
                Dash();
            }
        }
    }

    private void Shoot()
    {
        isFiring = true;

        if (shootCooldown <= 0)
        {
            // Generate a vector from the enemy to the player
            Vector2 randomizer = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            Vector2 projectileVector = (DirectionToPlayer() + randomizer).normalized * enemyProjectile.totalSpeed;

            // Create the enemy projectile
            EnemyProjectile newProjectile = Instantiate(enemyProjectile);
            newProjectile.totalSpeed = 20;
            newProjectile.transform.position = this.transform.position;
            newProjectile.setSpeed(projectileVector.x, projectileVector.y);

            // Reset the cooldown
            if (rapidFireCount < rapidFireCountMax)
            {
                shootCooldown = rapidFireCooldownMax;
                rapidFireCount++;
            }
            else
            {
                shootCooldown = 0;
                rapidFireCount = 1;
                isFiring = false;
                generalCooldown = generalCooldownMax;
            }
        }
    }

    private void Dash()
    {
        Vector2 randomizer = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        Vector2 dashVector = (DirectionToPlayer() + randomizer).normalized * 550;

        rb.AddForce(dashVector);

        generalCooldown = generalCooldownMax;

    }

    public override void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            generalCooldown = generalCooldownMax * 5;
            StartCoroutine(Continue());
        }
    }

    public IEnumerator Continue()
    {
        yield return new WaitForSeconds(3);
        WallGrid.SetActive(false);
        ContinueGrid.SetActive(true);
        Destroy(gameObject);
    }
}

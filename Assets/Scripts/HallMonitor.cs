using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallMonitor : Enemy {

    public int shootCooldown = 0;
    public int shootCooldownMax = 180;

    public int waveCount;
    public int waveCountMax = 3;
    public int waveCooldownMax = 30;

    public int invincibility;
    public int invincibilityMax = 180;

    public EnemyProjectile enemyProjectile;

    protected override void Start () {
        base.Start();
        health = 30;
        maxHealth = 30;
        waveCount = 1;
        shootCooldown = shootCooldownMax;

        invincibility = 0;
	}
	
	void Update () {
        // Update invincibility
        if (invincibility > 0)
        {
            invincibility--;
        }
        else
        {
            invincibility = 0;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        // Update shootCoolodown
        if (shootCooldown > 0)
        {
            shootCooldown--;
        }
    }

    private void FixedUpdate()
    {
        if (shootCooldown <= 0)
        {
            float action = Random.Range(0.0f, 1.0f);
            if (action <= 0.75f)
            {
                Shoot();
            } else
            {
                Shield();
            }
            
        }
        
    }

    public override void Damage(int damage)
    {
        if (invincibility <= 0)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Shoot()
    {
        invincibility = 0;

        // Generate a vector from the enemy to the player
        Vector2 randomizer = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        Vector2 projectileVector = (DirectionToPlayer() + randomizer).normalized * enemyProjectile.totalSpeed;

        Vector2 test = new Vector2(projectileVector.x, projectileVector.y);
        test = Quaternion.Euler(0, 0, -45) * test;

        // Create the projectile towards the player
        EnemyProjectile newProjectile = Instantiate(enemyProjectile);
        newProjectile.transform.position = this.transform.position;
        newProjectile.setSpeed(projectileVector.x, projectileVector.y);


        // Create the spread shot
        for (int i = 1; i <= 3; i++)
        {
            int angleSpread = 20;
            Vector2 baseShot = new Vector2(projectileVector.x, projectileVector.y);
            Vector2 positiveSpreadVector = Quaternion.Euler(0, 0, angleSpread * i) * baseShot;
            Vector2 negativeSpreadVector = Quaternion.Euler(0, 0, angleSpread * -i) * baseShot;

            EnemyProjectile positiveSpreadShot = Instantiate(enemyProjectile);
            positiveSpreadShot.transform.position = this.transform.position;
            positiveSpreadShot.setSpeed(positiveSpreadVector.x, positiveSpreadVector.y);

            EnemyProjectile negativeSpreadShot = Instantiate(enemyProjectile);
            negativeSpreadShot.transform.position = this.transform.position;
            negativeSpreadShot.setSpeed(negativeSpreadVector.x, negativeSpreadVector.y);
        }

        // Reset the cooldown
        if (waveCount < waveCountMax)
        {
            shootCooldown = waveCooldownMax;
            waveCount++;
        } else
        {
            shootCooldown = shootCooldownMax;
            waveCount = 1;
        }
    }

    private void Shield()
    {
        invincibility = invincibilityMax;

        // Fade while invincible
        Color fadedColor = gameObject.GetComponent<SpriteRenderer>().color;
        fadedColor.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = fadedColor;

        shootCooldown = shootCooldownMax;
    }
}

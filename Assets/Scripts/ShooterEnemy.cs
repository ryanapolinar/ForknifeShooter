using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{

    public int shootCooldown = 0;
    public int shootCooldownMax = 9;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public EnemyProjectile enemyProjectile;

    protected override void Start()
    {
        base.Start();
        detectionRadius = 12f;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //frozen code
        if (isFrozen)
        {
            this.enabled = false; //supposed to freeze
            StartCoroutine(FreezeTimer()); //wait few seconds, remove freeze

        }

        if (isPoisoned)
        {
            SpriteRenderer m_SpriteRenderer;
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = Color.green;
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
            animator.SetBool("cheerleaderThrow", true);
            //animator.Play("cheerleaderThrow", 0, 4.0f / 18.0f);

            // Generate a vector from the enemy to the player
            Vector2 randomizer = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            Vector2 projectileVector = (DirectionToPlayer() + randomizer).normalized * enemyProjectile.totalSpeed;

            if (projectileVector.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }

            // Create the enemy projectile
            EnemyProjectile newProjectile = Instantiate(enemyProjectile);
            newProjectile.transform.position = this.transform.position;
            newProjectile.setSpeed(projectileVector.x, projectileVector.y);

            // Reset the cooldown
            shootCooldown = shootCooldownMax;
        }
        else if (!PlayerDetected())
        {
            animator.SetBool("cheerleaderThrow", false);
        }
    }

    IEnumerator FreezeTimer()
    {
        SpriteRenderer m_SpriteRenderer;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.blue;
        Debug.Log("Frozen");
        yield return new WaitForSeconds(2.0f);
        this.enabled = true;
        isFrozen = false;
        m_SpriteRenderer.color = Color.white;
    }
}

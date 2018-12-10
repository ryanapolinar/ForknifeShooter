using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy {

    public float speed = 3f;
    public int chaseCooldown = 0;
    public int chaseCooldownMax = 60;
    Vector2 movementVelocity;

    public int shootCooldown = 0;
    public int shootCooldownMax = 24;

    public EnemyProjectile enemyProjectile;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        health *= 2;

        shootCooldown = shootCooldownMax;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //frozen code
        if (isFrozen)
        {
            //m_Blue = GUI.HorizontalSlider(new Rect(35, 95, 200, 30), m_Blue, 0, 1);
            SpriteRenderer m_SpriteRenderer;
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = Color.blue;

            this.enabled = false; //supposed to freeze
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(FreezeTimer()); //wait few seconds, remove freeze

            rb.constraints = RigidbodyConstraints2D.None;
            m_SpriteRenderer.color = Color.clear;
        }

        // Update chaseCooldown
        if (chaseCooldown > 0)
        {
            chaseCooldown--;
        }

        // Update shootCooldown
        if (shootCooldown > 0)
        {
            shootCooldown--;
        }
    }

    void FixedUpdate()
    {
        // MOVEMENT AND SHOOTING
        Chase();
        Shoot();
    }

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        // Damage player and reset chaseCooldown
        
        if (collision.tag == "Player")
        {
            base.OnTriggerEnter2D(collision);
            chaseCooldown = chaseCooldownMax;
        }
    }

    private void Chase()
    {
        if (this.PlayerDetected() && chaseCooldown <= 0)
        {
            // Run animation
            animator.SetBool("bullyRunThrow", true);

            this.movementVelocity = this.DirectionToPlayer().normalized * speed;
            rb.MovePosition(rb.position + movementVelocity * Time.deltaTime);

            // Set direction
            if (movementVelocity.x > 0)
            {
                spriteRenderer.flipX = false;
            } else
            {
                spriteRenderer.flipX = true;
            }
        } else
        {
            // Idle animation
            animator.SetBool("bullyRunThrow", false);
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

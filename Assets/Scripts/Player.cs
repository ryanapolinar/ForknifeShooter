using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Unit {

    public float speed = 7f;
    float fireCooldown;
    public float fireCooldownMax = .75f / 2;
    bool isDead = false;

    int invincibility = 0;
    int invincibilityFrameMax = 120;

    public bool bigProjectile = false;
    public bool freezeProjectile = false;
    public bool fasterProjectile = false;
    public bool spreadShot = false;
    public bool moreDamage = false;

    // COMPONENTS
    PlayerController playerController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    // GAMEOBJECTS
    public Projectile projectile;
    public Text healthText;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        playerController = GetComponent<PlayerController>();
        fireCooldown = fireCooldownMax;
        fireCooldownMax = .75f / 2;
        bigProjectile = false;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // UPDATE INVINCIBILITY
        if (invincibility > 0)
        {
            invincibility--;
        }
        else
        {
            invincibility = 0;
            spriteRenderer.color = Color.white;
        }

        if (!isDead)
        {
            // MOVEMENT
            Vector2 mv = playerController.GetMovementVelocity(speed);

            // Set player walking animation
            if (mv.x != 0 || mv.y != 0)
            {
                animator.SetBool("playerWalk", true);
            }
            else
            {
                animator.SetBool("playerWalk", false);
            }
            if (mv.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (mv.x < 0)
            {
                spriteRenderer.flipX = true;
            }

            // FIRING
            fireCooldown = playerController.GetProjectileInput(projectile, fireCooldown, fireCooldownMax);
            if (playerController.isFiring())
            {
                animator.SetBool("playerShoot", true);

                if (playerController.facingRight())
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }
            }
            else
            {
                animator.SetBool("playerShoot", false);
            }

        }
        // UI UPDATING
        healthText.text = "" + this.getHealth();
    }

    override public void Damage(int damage)
    {
        if (invincibility <= 0)
        {
            this.health -= damage;
        }

        if (health < 0)
        {
            health = 0;
        }

        // Activate player's invincibility frames
        this.setInvincibility(this.invincibilityFrameMax);

        // Fade while invincible
        Color fadedColor = spriteRenderer.color;
        fadedColor.a = 0.5f;
        spriteRenderer.color = fadedColor;

        if (health <= 0)
        {
            playerController.movementVelocity = new Vector2(0, 0);
            health = 0;
            isDead = true;
            StartCoroutine(GameOver());
        }
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("gameOver");
    }

    public int getInvincibility()
    {
        return this.invincibility;
    }

    public void setInvincibility(int frames)
    {
        this.invincibility = frames;
    }

    public int getInvincibilityFrameMax()
    {
        return this.invincibilityFrameMax;
    }
}

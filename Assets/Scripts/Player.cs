using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : Unit {

    public float speed = 7f;
    float fireCooldown;
    public float fireCooldownMax = .75f;
    bool isDead = false;

    int invincibility = 0;
    int invincibilityFrameMax = 120;

    public bool bigProjectile = false;
    public bool freezeProjectile = false;
    public bool fasterProjectile = false;
    public bool poisonProjectile = false;
    public bool spreadShot = false;
    public bool moreDamage = false;

    // COMPONENTS
    PlayerController playerController;

    // GAMEOBJECTS
    public Projectile projectile;
    public Text healthText;

    // Use this for initialization
    override protected void Start () {
        base.Start();
        playerController = GetComponent<PlayerController>();
        fireCooldown = fireCooldownMax;
        bigProjectile = false;
        health = 5;
    }
	
	// Update is called once per frame
	void Update () {
        // UPDATE INVINCIBILITY
        if (invincibility > 0)
        {
            invincibility--;
        } else
        {
            invincibility = 0;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (!isDead)
        {
            // MOVEMENT
            playerController.GetMovementVelocity(speed);

            // FIRING
            fireCooldown = playerController.GetProjectileInput(projectile, fireCooldown, fireCooldownMax);
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
        Color fadedColor = gameObject.GetComponent<SpriteRenderer>().color;
        fadedColor.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = fadedColor;

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

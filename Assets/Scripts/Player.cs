using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit {

    public float speed = 7f;
    float fireCooldown;
    public float fireCooldownMax = 0.25f;

    int invincibility = 0;
    int invincibilityFrameMax = 60;

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

        // MOVEMENT
        playerController.GetMovementVelocity(speed);

        // FIRING
        fireCooldown = playerController.GetProjectileInput(projectile, fireCooldown, fireCooldownMax);

        // UI UPDATING
        healthText.text = "Lives: " + this.getHealth();
	}

    override public void Damage(int damage)
    {
        this.health -= damage;

        // Activate player's invincibility frames
        this.setInvincibility(this.invincibilityFrameMax);

        // Fade while invincible
        Color fadedColor = gameObject.GetComponent<SpriteRenderer>().color;
        fadedColor.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = fadedColor;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
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

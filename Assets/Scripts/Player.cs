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
    void Start () {
        playerController = GetComponent<PlayerController>();
        fireCooldown = fireCooldownMax;
    }
	
	// Update is called once per frame
	void Update () {
        // UPDATE INVINCIBILITY
        if (invincibility > 0)
        {
            // Fade while invincible
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
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

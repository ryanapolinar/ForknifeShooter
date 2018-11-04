using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit {

    public float speed = 7f;
    float fireCooldown;
    public float fireCooldownMax = 0.25f;

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
        // MOVEMENT
        playerController.GetMovementVelocity(speed);

        // FIRING
        fireCooldown = playerController.GetProjectileInput(projectile, fireCooldown, fireCooldownMax);

        // UI UPDATING
        healthText.text = "Lives: " + this.getHealth();
	}
}

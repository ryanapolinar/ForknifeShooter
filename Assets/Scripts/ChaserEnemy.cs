using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy {

    public float speed = 3f;
    public int chaseCooldown = 0;
    public int chaseCooldownMax = 60;
    Vector2 movementVelocity;

    protected override void Start()
    {
        base.Start();
        health *= 2;
    }

    private void Update()
    {
        // Update chaseCooldown
        if (chaseCooldown > 0)
        {
            chaseCooldown--;
        }
    }

    void FixedUpdate()
    {
        // MOVEMENT
        Chase();
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
            this.movementVelocity = this.DirectionToPlayer().normalized * speed;
            rb.MovePosition(rb.position + movementVelocity * Time.deltaTime);
        }
    }

}

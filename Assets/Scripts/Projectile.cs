using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    // PROJECTILE FIELDS
    protected float xSpeed = 0f;
    protected float ySpeed = 0f;
    public float totalSpeed = 20f;
    protected Vector2 movementVelocity;
    protected int damage = 1;

    // COMPONENTS
    Rigidbody2D rb;

    virtual protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementVelocity = new Vector2(xSpeed, ySpeed);
        movementVelocity = movementVelocity.normalized * totalSpeed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + this.movementVelocity * Time.deltaTime);
    }

    public float getXSpeed()
    {
        return this.xSpeed;
    }

    public float getYSpeed()
    {
        return this.ySpeed;
    }

    public void setSpeed (float newXSpeed, float newYSpeed)
    {
        this.xSpeed = newXSpeed;
        this.ySpeed = newYSpeed;
    }

    public int getDamage()
    {
        return this.damage;
    }

    public void setDamage(int newDamage)
    {
        this.damage = newDamage;
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.Damage(this.damage);
                Destroy(gameObject);
                break;
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}

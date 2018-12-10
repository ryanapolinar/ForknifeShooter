using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    virtual protected void Start()
    {
        this.totalSpeed = 10f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        damage = 1;
        if (player.GetComponent<Player>().bigProjectile)
        {
            this.transform.localScale += new Vector3(4.0f, 4.0f, 0);
        }

        if (player.GetComponent<Player>().fasterProjectile)
        {
            this.totalSpeed += 10f;
        }

        if (player.GetComponent<Player>().moreDamage)
        {
            this.damage += 1;
        }

        if (player.GetComponent<Player>().freezeProjectile)
        {
            SpriteRenderer m_SpriteRenderer;
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = Color.blue;
        }

        if (player.GetComponent<Player>().poisonProjectile)
        {
            SpriteRenderer m_SpriteRenderer;
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = Color.green;
        }

        if (player.GetComponent<Player>().freezeProjectile && player.GetComponent<Player>().poisonProjectile)
        {
            SpriteRenderer m_SpriteRenderer;
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = Color.black;
        }
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

    public void setSpeed(float newXSpeed, float newYSpeed)
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
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player.GetComponent<Player>().freezeProjectile)
                {
                    enemy.isFrozen = true;
                }

                if (player.GetComponent<Player>().poisonProjectile)
                {
                    StartCoroutine(WaitTimer());
                    enemy.Damage(1);
                    enemy.isPoisoned = false;
                }
                break;
        }
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(3.0f);
        SpriteRenderer m_SpriteRenderer;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    // PROJECTILE FIELDS
    float xSpeed = 0f;
    float ySpeed = 0f;
    public float totalSpeed = 20f;
    Vector2 movementVelocity;

    // COMPONENTS
    Rigidbody2D rb;

    void Start()
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
        rb.MovePosition(rb.position + movementVelocity * Time.deltaTime);
    }

    public void setSpeed (float newXSpeed, float newYSpeed)
    {
        xSpeed = newXSpeed;
        ySpeed = newYSpeed;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}

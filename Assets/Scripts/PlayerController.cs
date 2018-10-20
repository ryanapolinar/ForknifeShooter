using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // PLAYER FIELDS
    public float speed = 7f;
    float fireCooldown;
    public float fireCooldownMax = 1f;
    Vector2 movementVelocity;

    // COMPONENTS
    Rigidbody2D rb;
    public Projectile projectile;

	/*
     * Use start to initialize variables
     */
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        fireCooldown = fireCooldownMax;
    }
	
	/*
     * Use Update to update variables and inputs
     */
	void Update () {

        // MOVEMENT
        GetMovementVelocity();

        // FIRING
        GetProjectileInput();
    }

    /*
     * Use FixedUpdate for visual things
     */
    void FixedUpdate ()
    {
        // MOVEMENT
        rb.MovePosition(rb.position + movementVelocity * Time.deltaTime);
    }

    void GetMovementVelocity ()
    {
        // MOVEMENT
        // Get the movement inputs, making sure they are using WASD
        float horizontalInput = 0;
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1;
        }
        float verticalInput = 0;
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1;
        }

        // Set the velocity 
        movementVelocity = new Vector2(horizontalInput, verticalInput);
        movementVelocity = movementVelocity.normalized * speed;
    }

    void GetProjectileInput ()
    {
        // Reduce the fireCooldown
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0.0f)
        {
            fireCooldown = 0.0f;
        }

        // Get the firing inputs, making sure they use the arrow keys
        bool fireUpInput = Input.GetKey(KeyCode.UpArrow) && fireCooldown <= 0.0f;
        bool fireRightInput = Input.GetKey(KeyCode.RightArrow) && fireCooldown <= 0.0f;
        bool fireDownInput = Input.GetKey(KeyCode.DownArrow) && fireCooldown <= 0.0f;
        bool fireLeftInput = Input.GetKey(KeyCode.LeftArrow) && fireCooldown <= 0.0f;

        // Instantiate projectile based on input
        float xSpeed = 0f;
        float ySpeed = 0f;
        if (fireUpInput)
        {
            // FIRE UP
            ySpeed = 1f;
            CreateProjectile(xSpeed, ySpeed);
        }
        else if (fireRightInput)
        {
            // FIRE RIGHT
            xSpeed = 1f;
            CreateProjectile(xSpeed, ySpeed);
        }
        else if (fireDownInput)
        {
            // FIRE DOWN
            ySpeed = -1f;
            CreateProjectile(xSpeed, ySpeed);
        }
        else if (fireLeftInput)
        {
            // FIRE LEFT
            xSpeed = -1f;
            CreateProjectile(xSpeed, ySpeed);
        }
        
        //rigidbody
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 tempVect = new Vector2(h, v);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        //transform.Translate(tempVect);
        rb.MovePosition(rb.position + (tempVect * speed) * Time.deltaTime);
    }

    void CreateProjectile (float xSpeed, float ySpeed)
    {
        Projectile newProjectile = Instantiate(projectile);
        newProjectile.transform.position = this.transform.position;
        newProjectile.setSpeed(xSpeed, ySpeed);

        fireCooldown = fireCooldownMax;
    }
}

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

        // FIRE PROJECTILES WITH THE MOUSE
        // Get the mouse click input
        bool mouseInput = Input.GetMouseButton(0) && fireCooldown <= 0.0f;
        if (mouseInput) {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseWorldPosition2d = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
            Vector2 playerPosition = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 direction = (mouseWorldPosition2d - playerPosition);

            CreateProjectile(direction.x, direction.y);
        }


        // FIRE PROJECTILES WITH THE ARROW KEYS
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
    }

    void CreateProjectile (float xSpeed, float ySpeed)
    {
        Projectile newProjectile = Instantiate(projectile);
        newProjectile.transform.position = this.transform.position;
        newProjectile.setSpeed(xSpeed, ySpeed);

        fireCooldown = fireCooldownMax;
    }
}

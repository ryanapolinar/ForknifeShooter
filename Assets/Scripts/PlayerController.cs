using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // PLAYERCONTROLLER FIELDS
    public Vector2 movementVelocity;

    // PLAYERCONTROLLER COMPONENTS
    Rigidbody2D rb;
    public PlayerProjectile playerProjectile;

    Vector2 DIRECTION;
	/*
     * Use start to initialize variables
     */
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    /*
     * Use FixedUpdate for visual things
     */
    void FixedUpdate ()
    {
        // MOVEMENT
        rb.MovePosition(rb.position + movementVelocity * Time.deltaTime);
    }

    /**
     * Gets the movement velocity of the player and stores it in movementVelocity
     */
    public Vector2 GetMovementVelocity (float speed)
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

        return movementVelocity;
    }

    /**
     * Gets input from the user to create projectiles and makes them.
     * 
     * Returns the current projectile cooldown
     */
    public float GetProjectileInput (Projectile projectile, float fireCooldown, float fireCooldownMax)
    {
        // Reduce the fireCooldown
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0.0f)
        {
            fireCooldown = 0.0f;
        }

        // FIRE PROJECTILES WITH THE MOUSE
        // Get the mouse click input
        bool mouseInput = Input.GetMouseButton(0);
        if (mouseInput && fireCooldown <= 0.0f) {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseWorldPosition2d = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
            Vector2 playerPosition = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 direction = (mouseWorldPosition2d - playerPosition);
            DIRECTION = direction;

            // Fire the projectile
            CreateProjectile(projectile, direction.x, direction.y);
            fireCooldown = fireCooldownMax;
        }


        // FIRE PROJECTILES WITH THE ARROW KEYS
        // Get the firing inputs, making sure they use the arrow keys
        bool fireUpInput = Input.GetKey(KeyCode.UpArrow);
        bool fireRightInput = Input.GetKey(KeyCode.RightArrow);
        bool fireDownInput = Input.GetKey(KeyCode.DownArrow);
        bool fireLeftInput = Input.GetKey(KeyCode.LeftArrow);

        // Instantiate projectile based on input
        float xSpeed = 0f;
        float ySpeed = 0f;
        if (fireUpInput && fireCooldown <= 0.0f)
        {
            // FIRE UP
            ySpeed = 1f;
            CreateProjectile(projectile, xSpeed, ySpeed);
            fireCooldown = fireCooldownMax;
        }
        else if (fireRightInput && fireCooldown <= 0.0f)
        {
            // FIRE RIGHT
            xSpeed = 1f;
            CreateProjectile(projectile, xSpeed, ySpeed);
            fireCooldown = fireCooldownMax;
        }
        else if (fireDownInput && fireCooldown <= 0.0f)
        {
            // FIRE DOWN
            ySpeed = -1f;
            CreateProjectile(projectile, xSpeed, ySpeed);
            fireCooldown = fireCooldownMax;
        }
        else if (fireLeftInput && fireCooldown <= 0.0f)
        {
            // FIRE LEFT
            xSpeed = -1f;
            CreateProjectile(projectile, xSpeed, ySpeed);
            fireCooldown = fireCooldownMax;
        }

        return fireCooldown;
    }

    void CreateProjectile (Projectile projectile, float xSpeed, float ySpeed)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //
        Vector2 projectileVector = DIRECTION.normalized * 8f;
        //

        //projectile at the player
        Projectile newProjectile = Instantiate(projectile);
        newProjectile.transform.position = this.transform.position;
        newProjectile.setSpeed(xSpeed, ySpeed);

        ///*
        if (player.GetComponent<Player>().spreadShot)
        {
            for (int i = 1; i <= 3; i++)
            {
                int angleSpread = 20;
                Vector2 baseShot = new Vector2(projectileVector.x, projectileVector.y);
                Vector2 positiveSpreadVector = Quaternion.Euler(0, 0, angleSpread * 2) * baseShot;
                Vector2 negativeSpreadVector = Quaternion.Euler(0, 0, angleSpread * -2) * baseShot;
                //Vector2 positiveSpreadVector = Quaternion.Euler(0, 0, angleSpread * i) * baseShot;
                //Vector2 negativeSpreadVector = Quaternion.Euler(0, 0, angleSpread * -i) * baseShot;

                PlayerProjectile positiveSpreadShot = Instantiate(playerProjectile);
                positiveSpreadShot.transform.position = this.transform.position;
                positiveSpreadShot.setSpeed(positiveSpreadVector.x, positiveSpreadVector.y);

                PlayerProjectile negativeSpreadShot = Instantiate(playerProjectile);
                negativeSpreadShot.transform.position = this.transform.position;
                negativeSpreadShot.setSpeed(negativeSpreadVector.x, negativeSpreadVector.y);
            }
        }
        //*/
    }

    public bool isFiring()
    {
        bool mouseInput = Input.GetMouseButton(0);
        bool fireUpInput = Input.GetKey(KeyCode.UpArrow);
        bool fireRightInput = Input.GetKey(KeyCode.RightArrow);
        bool fireDownInput = Input.GetKey(KeyCode.DownArrow);
        bool fireLeftInput = Input.GetKey(KeyCode.LeftArrow);

        return mouseInput || fireUpInput || fireRightInput || fireDownInput || fireLeftInput;
    }

    public bool facingRight()
    {
        if (DIRECTION.x > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
